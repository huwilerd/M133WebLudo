using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

/// <summary>
/// Zusammenfassungsbeschreibung für HtmlViewlet
/// </summary>
public class HtmlViewlet : MasterViewlet, HtmlInterface
{
    public string getAllClients(Session session)
    {
        if(!session.sessionRole.Equals(SessionRole.Client))
        {
            ServerResponse clientsData = ServerViewletProvider.getInstance().GetEmployeeInterface(session).getAllClients();
            if(clientsData.getResponseStatus())
            {
                List<Person> clientList = (List<Person>)clientsData.getResponseObject();
                return HtmlUtil.generateClientTable(clientList, session.sessionRole.Equals(SessionRole.Administrator), getOpenConnection());
            } else
            {
                return HtmlUtil.generateErrorMessage("Kunden konnten nicht geladen werden.");
            }
        } else
        {
            return HtmlUtil.generateErrorMessage("Keine Zugriffsrechte auf alle Kunden");
        }
    }

    public string getAllEmployees(Session session)
    {
        if (session.sessionRole.Equals(SessionRole.Administrator))
        {
            AdminInterface adminInterface = ServerViewletProvider.getInstance().GetAdminInterface(session);
            ServerResponse employeeData = adminInterface.getAllEmployees();
            if (employeeData.getResponseStatus())
            {
                List<Person> employeeList = (List<Person>)employeeData.getResponseObject();
                return HtmlUtil.generateEmployeeList(employeeList);
            }
            return HtmlUtil.generateErrorMessage("Es konnten keine Mitarbeiter geladen werden");
        } else
        {
            return HtmlUtil.generateErrorMessage("Keine Zugriffsrechte auf die Mitarbeiter");
        }
    }

    public string getAllGames(Session session, bool manageView)
    {
        ServerResponse allGamesResponse = ((ClientInterface)ServerViewletProvider.getInstance().GetPersonViewlet()).getAllGames();
        if(allGamesResponse.getResponseStatus())
        {
            List<Spiel> gameList = (List<Spiel>)allGamesResponse.getResponseObject();
            if (manageView)
            {
                return HtmlUtil.generateGameOverviewListHtml(gameList, getOpenConnection());
            }
            return HtmlUtil.generateGameFlexListHtml(gameList);
        }
        return HtmlUtil.generateErrorMessage(allGamesResponse.getResponseMessage());
    }

    public string getAllHires(Session session)
    {
        if (!session.sessionRole.Equals(SessionRole.Client))
        {
            EmployeeInterface employeeInterface = ServerViewletProvider.getInstance().GetEmployeeInterface(session);
            ServerResponse allHires = employeeInterface.getAllHires();
            if (allHires.getResponseStatus())
            {
                StringBuilder builder = new StringBuilder();

                List<Hire> hireList = (List<Hire>)allHires.getResponseObject();
                List<Hire> openHires = hireList.Where(hire => !hire.Bezahlt).ToList();
                List<Hire> closedHires = hireList.Where(hire => hire.Bezahlt).ToList();
                if (openHires.Count > 0)
                {
                    builder.Append(HtmlUtil.generateOverviewTable(openHires, "Offene Ausleihen", getOpenConnection()));
                }
                if (closedHires.Count > 0)
                {
                    builder.Append(HtmlUtil.generateOverviewTable(closedHires, "Abgeschlossene Ausleihen", getOpenConnection()));
                }
                if(closedHires.Count == 0 && openHires.Count==0)
                {
                    builder.Append("Es wurden keine Ausleihen gefunden.");
                }
                return builder.ToString();
            }
            return HtmlUtil.generateErrorMessage(allHires.getResponseMessage());
        } else
        {
            return HtmlUtil.generateErrorMessage("Keine Zugriffsrechte auf alle Ausleihen");
        }
    }

    public string getAllLudotheken(Session session)
    {
        if(session.sessionRole.Equals(SessionRole.Administrator))
        {
            ServerResponse allLudothekenData = ServerViewletProvider.getInstance().GetAdminInterface(session).getAllLudotheken();
            if (allLudothekenData.getResponseStatus())
            {
                List<Ludothek> allLudotheken = (List<Ludothek>)allLudothekenData.getResponseObject();
                if(allLudotheken.Count > 0)
                {
                    return HtmlUtil.generateLudothekenListHtml(allLudotheken);
                }
                return HtmlUtil.generateErrorMessage("Es wurden keine Ludotheken gefunden");
            }
            return HtmlUtil.generateErrorMessage("Es konnten keine Ludotheken geladen werden");
        }
        return HtmlUtil.generateErrorMessage("Keine Zugriffsrechte");
    }

    public string getAllUsers(Session session)
    {
        if(session.sessionRole.Equals(SessionRole.Administrator) || session.sessionRole.Equals(SessionRole.Employee))
        {
            ServerResponse allUserData = ServerViewletProvider.getInstance().GetEmployeeInterface(session).getAllUsers();
            if (allUserData.getResponseStatus())
            {
                List<User> allUser = (List<User>)allUserData.getResponseObject();
                if (allUser.Count > 0)
                {
                    return HtmlUtil.generateUserOverview(allUser, getOpenConnection());
                }
                return HtmlUtil.generateErrorMessage("Keine Benutzer zum anzeigen");
            }
            return HtmlUtil.generateErrorMessage("Es konnten keine Benutzer geladen werden: "+allUserData.getResponseMessage());
        }
        return HtmlUtil.generateErrorMessage("Keine Zugriffsrechte auf alle Benutzer");
    }

    public string getDashboard(Session session)
    {
        if (session.sessionRole.Equals(SessionRole.Administrator))
        {
            int amountHires = -1;
            List<Hire> allHires = new List<Hire>();
            ServerResponse hireData = ServerViewletProvider.getInstance().GetEmployeeInterface(session).getAllHires();
            if (hireData.getResponseStatus())
            {
                allHires = (List<Hire>)hireData.getResponseObject();
                amountHires = allHires.Count;
            }

            int amountEmployees = -1;
            ServerResponse employeeData = ServerViewletProvider.getInstance().GetAdminInterface(session).getAllEmployees();
            if (employeeData.getResponseStatus())
            {
                List<Person> allEmployees = (List<Person>)employeeData.getResponseObject();
                amountEmployees = allEmployees.Count;
            }

            int amountClients = -1;
            ServerResponse clientData = ServerViewletProvider.getInstance().GetEmployeeInterface(session).getAllClients();
            if (clientData.getResponseStatus())
            {
                List<Person> allClients = (List<Person>)clientData.getResponseObject();
                amountClients = allClients.Count;
            }

            StringBuilder builder = new StringBuilder();
            builder.Append(HtmlUtil.generateDashboard(amountHires, amountEmployees, amountClients));
            if (allHires.Count > 0)
            {
                builder.Append(HtmlUtil.generateOverviewTable(allHires, "Alle Ausleihen", getOpenConnection()));
            }
            return builder.ToString();
        } else
        {
            return HtmlUtil.generateErrorMessage("Keine Zugriffsrechte auf die Gesamtübersicht");
        }
    }

    /**
     * Closed Hires 
     **/
    public string getOwnHires(Session session)
    {
        ClientInterface clientInterface = (ClientInterface) ServerViewletProvider.getInstance().GetPersonViewlet();
        ServerResponse ownHires = clientInterface.getOwnClosedHires(session);
        if(ownHires.getResponseStatus())
        {
            List<Hire> ownHireList = (List<Hire>) ownHires.getResponseObject();
            return HtmlUtil.generateOwnClosedHireOverviewListHtml(ownHireList, getOpenConnection());
        }
        return HtmlUtil.generateErrorMessage(ownHires.getResponseMessage());
    }

    public string getOwnOpenHires(Session session)
    {
        ClientInterface clientInterface = (ClientInterface)ServerViewletProvider.getInstance().GetPersonViewlet();
        ServerResponse ownOpenHires = clientInterface.getOwnOpenHires(session);
        if (ownOpenHires.getResponseStatus())
        {
            List<Hire> ownHireList = (List<Hire>)ownOpenHires.getResponseObject();
            return HtmlUtil.generateOwnHireOverviewListHtml(ownHireList, getOpenConnection());
        }
        return HtmlUtil.generateErrorMessage(ownOpenHires.getResponseMessage());
    }
}