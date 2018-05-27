using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Zusammenfassungsbeschreibung für InstallViewlet
/// </summary>
public class InstallViewlet : MasterViewlet
{
    private readonly String ADMIN_USER = "Admin@Ludothek.ch";
    private readonly String ADMIN_PW = "gibbiX12345";

    private readonly String CLIENT_USER = "Client@Ludothek.ch";
    private readonly String EMPLOYEE_USER = "Employee@Ludothek.ch";

    /**
     * Initial Data 
     **/
    public static List<Kategorie> kategories = new List<Kategorie> { new Kategorie(100, "Kategorie eins", 0),
                                                                     new Kategorie(200, "Kategorie zwei", 6),
                                                                     new Kategorie(300, "Kategorie dreo", 16)  };
    public static List<TarifKategorie> tarifKategories = new List<TarifKategorie> { new TarifKategorie(100, "Familientarif", 10, 5),
                                                                                    new TarifKategorie(150, "Fantarif", 8, 10),
                                                                                    new TarifKategorie(200, "Normaltarif", 15, 10)  };
    public static List<Spiel> spiele = new List<Spiel> { new Spiel(1000, "Monopoly", "Orell Füssli", 10, tarifKategories[0].ID_TarifKategorie, kategories[0].ID_Kategorie),
                                                         new Spiel(1012, "Mensch ärgere dich nicht", "Hug Verlag", 5, tarifKategories[1].ID_TarifKategorie, kategories[1].ID_Kategorie),
                                                         new Spiel(1015, "Schach", "GBSSG", 5, tarifKategories[2].ID_TarifKategorie, kategories[2].ID_Kategorie),
                                                         new Spiel(1080, "Easy Coding", "GBSSG", 0, tarifKategories[1].ID_TarifKategorie, kategories[1].ID_Kategorie) };
    public static Verband verband = new Verband(20, "St. Galler Ludothekverband");
    public static Ludothek ludothek = new Ludothek(10, "Ludothek Gbs", "Demutsstrasse 42", 4200, "Weinfelden", verband.ID_Verband);

    /**
     * Initial Database 
     **/
    public ServerResponse installNullerdata()
    {
        if (checkIfInstallIsRequired())
        {
            installInitialKategories();
            installInitialGames();
            installLudothek();

            installInitialUsers();
            installMitgliedschaftToUser(EMPLOYEE_USER, ADMIN_PW);
            installMitgliedschaftToUser(ADMIN_USER, ADMIN_PW);

            return createResponse(1, "Erfolgreich alle Nullerdaten installiert", null, /*no successful login*/false);
        }

        return createResponse(1, "Nullerdaten sind bereits installiert", null, false);

    }

    private void installInitialKategories()
    {
        ServerViewletProvider provider = ServerViewletProvider.getInstance();
        kategories.ForEach(delegate (Kategorie kategorie)
        {
            bool executionState = ServerUtil.addKategorie(kategorie, getOpenConnection());
            if (!executionState)
            {
                throw new Exception("Initial-Kategorie konnte nicht hinzugefügt werden");
            }
        });
        tarifKategories.ForEach(delegate (TarifKategorie tarifKategorie)
        {
            bool executionState = ServerUtil.addTarifKategorie(tarifKategorie, getOpenConnection());
            if (!executionState)
            {
                throw new Exception("Initial-TarifKategorie konnte nicht hinzugefügt werden");
            }
        });
    }

    private void installInitialGames()
    {
        ServerViewletProvider provider = ServerViewletProvider.getInstance();
        spiele.ForEach(delegate (Spiel spiel)
        {
            ServerResponse sampleGameOne = provider.GetEmployeeInterface(getSession(EMPLOYEE_USER)).addNewGame(spiel);
            checkResponses(new ServerResponse[] { sampleGameOne });
        });
    }

    private Session getSession(String user)
    {
        ServerResponse response = ServerViewletProvider.getInstance().getAuthenticationViewlet().tryLogIn(user, ADMIN_PW);
        if (response.getResponseStatus())
        {
            return (Session)response.getResponseObject();
        }
        return null;
    }

    private void installInitialUsers()
    {
        ServerViewletProvider provider = ServerViewletProvider.getInstance();
        ServerResponse adminResponse = provider.getAuthenticationViewlet().registerUser(ADMIN_USER, ADMIN_PW);
        ServerResponse employeeResponse = provider.getAuthenticationViewlet().registerUser(EMPLOYEE_USER, ADMIN_PW);
        ServerResponse clientResponse = provider.getAuthenticationViewlet().registerUser(CLIENT_USER, ADMIN_PW);

        checkResponses(new ServerResponse[] { adminResponse, employeeResponse, clientResponse });

        installPersonInformation(ADMIN_USER, "Administrator");
        installPersonInformation(EMPLOYEE_USER, "Mitarbeiter");
        installEmployee(EMPLOYEE_USER);
        installEmployee(ADMIN_USER);
        installFilialleiter(ADMIN_USER);
        installInitialHires(CLIENT_USER);
    }

    private void installEmployee(String userEmail) 
    {
        Person employee = getPersonFromSession(userEmail);
        bool executionState = ServerUtil.addEmployee(employee.ID_Person, ludothek.ID_Ludothek, getOpenConnection());
        if (!executionState)
        {
            throw new Exception("Employee konnte nicht erstellt werden");
        }
    }

    private void installFilialleiter(String userEmail)
    {
        Person employee = getPersonFromSession(userEmail);
        bool executionState = ServerUtil.addFilialleiter(employee.ID_Person, getOpenConnection());
        if (!executionState)
        {
            throw new Exception("Filialleiter konnte nicht erstellt werden");
        }
    }

    private Person getPersonFromSession(String userEmail)
    {
        ServerResponse response = ServerViewletProvider.getInstance().getAuthenticationViewlet().tryLogIn(userEmail, ADMIN_PW);
        if (response.getResponseStatus())
        {
            Session session = (Session)response.getResponseObject();
            if (session.activeSession)
            {
                ServerResponse personResponse = ServerViewletProvider.getInstance().GetSessionInterface().getPersonFromSession(session);
                if (personResponse.getResponseStatus())
                {
                    return (Person)personResponse.getResponseObject();
                }
                else
                {
                    throw new Exception("Keine Person zur Session gefunden");
                }
            }
            else
            {
                throw new Exception("Keine aktive Session gefunden");
            }
        } else
        {
            throw new Exception("Login fehlgeschlagen");
        }
    }

    private void installPersonInformation(String userEmail, String name)
    {
        Person person = getPersonFromSession(userEmail);
        person.Name = name;
        person.strasse = "Musterstrasse 69";
        person.postleitzahl = 9300;
        person.ort = "Wittenbach";
        person.land = "Schweiz";
        person.Geburtsdatum = DateTime.Now.AddYears(-18);
        ServerResponse personUpdateResponse = ServerViewletProvider.getInstance().GetPersonViewlet().updatePerson(person);
        if(!personUpdateResponse.getResponseStatus())
        {
            throw new Exception("Person konnte nicht aktualisiert werden");
        }
    }

    private void installLudothek()
    {
        bool verbandState = ServerUtil.addVerband(verband, getOpenConnection());
        if(!verbandState)
        {
            throw new Exception("Verband konnte nicht gespeichert werden");
        }
        bool ludothekState = ServerUtil.addLudothek(ludothek, getOpenConnection());
        if(!ludothekState)
        {
            throw new Exception("Ludothek konnte nicht gespeichert werden");
        }
    }

    private void installInitialHires(String userEmail)
    {
        ServerResponse response = ServerViewletProvider.getInstance().getAuthenticationViewlet().tryLogIn(CLIENT_USER, ADMIN_PW);
        if(response.getResponseStatus())
        {
            Session session = (Session) response.getResponseObject();
            if (session.activeSession) {
                ServerViewletProvider.getInstance().GetPersonViewlet().createHire(session, new Hire(1, session.FK_Person, spiele[0].ID_Spiel, DateTime.Now, DateTime.Now.AddDays(7), false));
                ServerViewletProvider.getInstance().GetPersonViewlet().createHire(session, new Hire(2, session.FK_Person, spiele[1].ID_Spiel, DateTime.Now.AddDays(-14), DateTime.Now.AddDays(-7), false));
                ServerViewletProvider.getInstance().GetPersonViewlet().createHire(session, new Hire(3, session.FK_Person, spiele[2].ID_Spiel, DateTime.Now.AddDays(-54), DateTime.Now.AddDays(-21), true));

            } else
            {
                throw new Exception("Keine aktive Session gefunden");
            }
        } else
        {
            throw new Exception("Es konnten keine Initial-Vermietungen gespeichert werden: "+response.getResponseMessage());
        }
        
    }

    private void installMitgliedschaftToUser(String mail, String pw) {
        ServerResponse response = ServerViewletProvider.getInstance().getAuthenticationViewlet().tryLogIn(mail, pw);
        if(response.getResponseStatus())
        {
            Session session = (Session) response.getResponseObject();
            ServerResponse personResponse = ServerViewletProvider.getInstance().GetSessionInterface().getPersonFromSession(session);
            if(personResponse.getResponseStatus() && personResponse.getResponseObject()!=null)
            {
                int generatedID = ServerUtil.generateNewIdForTable("Mitgliedschaft", "ID_Mitgliedschaft", getOpenConnection());
                Mitgliedschaft newMitgliedschaft = new Mitgliedschaft(generatedID, "In Benutzung", "Bezahlt", DateTime.Now.AddMonths(-3), DateTime.Now.AddMonths(9));
                bool newMtgState = ServerUtil.addMitgliedschaft(newMitgliedschaft, getOpenConnection());
                if (!newMtgState)
                {
                    throw new Exception("Could not insert new Mitgliedschaft");
                }
                else
                {
                    Person person = (Person)personResponse.getResponseObject();
                    person.mitgliedschaft = newMitgliedschaft;
                    ServerResponse updateResponse = ServerViewletProvider.getInstance().GetPersonViewlet().updatePerson(person);
                    if (!updateResponse.getResponseStatus())
                    {
                        throw new Exception("Person could not be updated with new Mitgliedschaft");
                    }
                }
            } else
            {
                throw new Exception("No Person associated with current session");
            }

        } else
        {
            throw new Exception("Could not login as employee");
        }
    }

    /**
     * Check if initial Installation worked successfully 
     **/
    private void checkResponses(ServerResponse[] responses)
    {
        for(int i=0; i < responses.Length; i++)
        {
            ServerResponse response = responses[i];
            if (!response.getResponseStatus())
            {
                throw new Exception(response.getResponseMessage());
            }
        }
    }

    /**
     * Install is only required when zero users are found
     **/
     private bool checkIfInstallIsRequired()
    {
        List<Dictionary<String, Object>> userList = CommandUtil.create(getOpenConnection()).executeReader("SELECT * FROM Benutzer", null, null);
        return userList.Count == 0;
    }
}