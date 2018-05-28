using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MainMenu : SecureMasterPage
{

    protected override void setupPageWithSession(Session session)
    {
        Person currentPerson = getCurrentPerson();
        if(currentPerson!=null)
        {
            String greetingString = currentPerson.Name;
            username.InnerHtml = greetingString;
        }

        handleSessionRole();
        setDefaultPage();
        handleParameter();

    }

    private void handleParameter()
    {
        handlePageParam();
        handleActionParam();
    }

    private void handleActionParam()
    {
        String actionParam = getStringFromParameter("action");

        if (actionParam != null)
        {
            if (getCurrentSession().sessionRole.Equals(SessionRole.Administrator) ||
                getCurrentSession().sessionRole.Equals(SessionRole.Employee))
            {


                switch (actionParam)
                {
                    case "delete":
                        int hireId = getIntFromParameter("hire");
                        if (hireId > 0)
                        {
                            ServerResponse deletedHireResponse = GetViewletProvider().GetEmployeeInterface(getCurrentSession()).removeHire(hireId);
                            if (deletedHireResponse.getResponseStatus())
                            {
                                Response.Redirect("MainMenu.aspx?page=3");
                            }
                            else
                            {
                                flexContainer.InnerText = "Aktion konnte nicht durchgeführt werden: " + deletedHireResponse.getResponseMessage();
                            }
                        }
                        break;
                    case "close":
                        int toCloseHireId = getIntFromParameter("hire");
                        if (toCloseHireId > 0)
                        {
                            ServerResponse closedHire = GetViewletProvider().GetEmployeeInterface(getCurrentSession()).closeHire(toCloseHireId);
                            if (closedHire.getResponseStatus())
                            {
                                Response.Redirect("MainMenu.aspx?page=3");
                            } else
                            {
                                flexContainer.InnerText = "Aktion konnte nicht durchgeführt werden: " + closedHire.getResponseMessage();

                            }
                        }
                        break;
                    case "reopen":
                        int toReopenHireId = getIntFromParameter("hire");
                        if (toReopenHireId > 0)
                        {
                            ServerResponse openedHire = GetViewletProvider().GetEmployeeInterface(getCurrentSession()).reopenHire(toReopenHireId);
                            if (openedHire.getResponseStatus())
                            {
                                Response.Redirect("MainMenu.aspx?page=3");
                            }
                            else
                            {
                                flexContainer.InnerText = "Aktion konnte nicht durchgeführt werden: " + openedHire.getResponseMessage();

                            }
                        }
                        break;
                    case "upgrade":
                        int emplId = getIntFromParameter("empl");
                        if (emplId > 0)
                        {
                            if (getCurrentSession().sessionRole.Equals(SessionRole.Administrator))
                            {
                                ServerResponse upgradeClientToClient = GetViewletProvider().GetAdminInterface(getCurrentSession()).makeEmployee(emplId);
                                if (upgradeClientToClient.getResponseStatus())
                                {
                                    Response.Redirect("MainMenu.aspx?page=4");
                                }
                                else
                                {
                                    flexContainer.InnerHtml = "Aktion konnte nicht durchgeführt werden: " + upgradeClientToClient.getResponseMessage();
                                }
                            } else
                            {
                                flexContainer.InnerHtml = "Keine Zugriffsrechte für diese Funktion";
                            }
                        }
                        break;
                    case "upgradeMitg":
                        int emplMitgId = getIntFromParameter("empl");
                        if (emplMitgId > 0)
                        {
                            if (getCurrentSession().sessionRole.Equals(SessionRole.Administrator))
                            {
                                ServerResponse upgradeClientToMitgClient = GetViewletProvider().GetEmployeeInterface(getCurrentSession()).createMitgliedsschaftForPerson(emplMitgId);
                                if (upgradeClientToMitgClient.getResponseStatus())
                                {
                                    Response.Redirect("MainMenu.aspx?page=4");
                                }
                                else
                                {
                                    flexContainer.InnerHtml = "Aktion konnte nicht durchgeführt werden: " + upgradeClientToMitgClient.getResponseMessage();
                                }
                            }
                            else
                            {
                                flexContainer.InnerHtml = "Keine Zugriffsrechte für diese Funktion";
                            }
                        }
                        break;
                    case "downgrade":
                        int downgradeEmplId = getIntFromParameter("empl");
                        if (downgradeEmplId > 0)
                        {
                            if (getCurrentSession().sessionRole.Equals(SessionRole.Administrator))
                            {
                                ServerResponse downgradeEmployeeToClient = GetViewletProvider().GetAdminInterface(getCurrentSession()).removeEmployee(getCurrentSession(), downgradeEmplId);
                                if (downgradeEmployeeToClient.getResponseStatus())
                                {
                                    Response.Redirect("MainMenu.aspx?page=7");
                                } else
                                {
                                    flexContainer.InnerHtml = "Aktion konnte nicht durchgeführt werden: " + downgradeEmployeeToClient.getResponseMessage();
                                }
                            } else
                            {
                                flexContainer.InnerHtml = "Keine Zugriffsrechte für diese Funktion";
                            }
                        }
                        break;
                    case "logout":
                        int toLogoutUserId = getIntFromParameter("user");
                        if (toLogoutUserId > 0)
                        {
                            if (getCurrentSession().sessionRole.Equals(SessionRole.Administrator) ||
                                getCurrentSession().sessionRole.Equals(SessionRole.Employee))
                            {
                                ServerResponse logoutUserFromPage = GetViewletProvider().GetEmployeeInterface(getCurrentSession()).logoutUser(toLogoutUserId);
                                if (logoutUserFromPage.getResponseStatus())
                                {
                                    Response.Redirect("MainMenu.aspx?page=6");
                                } else
                                {
                                    flexContainer.InnerHtml = logoutUserFromPage.getResponseMessage();
                                }
                            } else
                            {
                                flexContainer.InnerHtml = "Keine Zugriffsrechte für diese Funktion";
                            }
                        }
                        break;
                    case "removeGame":
                        int toDeleteGameId = getIntFromParameter("game");
                        if (toDeleteGameId > 0)
                        {
                            if (getCurrentSession().sessionRole.Equals(SessionRole.Administrator) ||
                                getCurrentSession().sessionRole.Equals(SessionRole.Employee))
                            {
                                ServerResponse deleteGameResp = GetViewletProvider().GetEmployeeInterface(getCurrentSession()).removeGame(toDeleteGameId);
                                if(deleteGameResp.getResponseStatus())
                                {
                                    Response.Redirect("MainMenu.aspx?page=5");
                                } else
                                {
                                    flexContainer.InnerHtml = deleteGameResp.getResponseMessage();
                                }
                            } else
                            {
                                flexContainer.InnerHtml = "Keine Zugriffsrechte für diese Funktion";
                            }
                        }
                        break;
                    case "removeClient":
                        int toDeleteClientId = getIntFromParameter("user");
                        if(toDeleteClientId > 0)
                        {
                            if (getCurrentSession().sessionRole.Equals(SessionRole.Administrator) ||
                                getCurrentSession().sessionRole.Equals(SessionRole.Employee))
                            {
                                ServerResponse deleteUserResp = GetViewletProvider().GetEmployeeInterface(getCurrentSession()).deleteUser(toDeleteClientId);
                                if (deleteUserResp.getResponseStatus())
                                {
                                    Response.Redirect("MainMenu.aspx?page=4");
                                }
                                else
                                {
                                    flexContainer.InnerHtml = deleteUserResp.getResponseMessage();
                                }
                            }
                            else
                            {
                                flexContainer.InnerHtml = "Keine Zugriffsrechte für diese Funktion";
                            }
                        }
                        break;
                }
            }
            else
            {
                flexContainer.InnerText = "Keine Zugriffsberechtigung für diese Aktion.";
            }
        }
    }

    private void handlePageParam()
    {
        int pageParam = getIntFromParameter("page");

        if (pageParam > 0)
        {
            switch (pageParam)
            {
                case 0:
                    showNewestGames(null, null);
                    break;
                case 1:
                    showCurrentHires(null, null);
                    break;
                case 2:
                    showClosedHires(null, null);
                    break;
                case 3:
                    showAllHires(null, null);
                    break;
                case 4:
                    showAllClients(null, null);
                    break;
                case 5:
                    /* Client doesn't have permission to see manage page of all games */
                    if (!getCurrentSession().sessionRole.Equals(SessionRole.Client))
                    {
                        showAllGames(null, null);
                    } else
                    {
                        showNewestGames(null, null);
                    }
                    break;
                case 6:
                    showAllUsers(null, null);
                    break;
                case 7:
                    showAllEmployees(null, null);
                    break;
                case 8:
                    showLudotheken(null, null);
                    break;
                case 9:
                    showDashboard(null, null);
                    break;
                default:
                    showCurrentHires(null, null);
                    break;
            }
        }
    }

    private void setDefaultPage()
    {
        //showCurrentHires(null, null);
    }

    private void handleSessionRole()
    {
        SessionRole role = getCurrentSession().sessionRole;
        Console.WriteLine("SessionRolle ist: " + role.ToString());
        allEmployees.Visible = false;
        allClients.Visible = false;
        allGames.Visible = false;
        dashboard.Visible = false;
        allHires.Visible = false;
        manageTitle.Visible = false;
        ludotheken.Visible = false;
        allUsers.Visible = false;

        switch (role)
        {
            case SessionRole.Client:
                Console.WriteLine("Ich bin Client");
                break;
            case SessionRole.Employee:
                allClients.Visible = true;
                allGames.Visible = true;
                allHires.Visible = true;
                manageTitle.Visible = true;
                allUsers.Visible = true;
                Console.WriteLine("Ich bin Employee");
                break;
            case SessionRole.Administrator:
                allClients.Visible = true;
                allGames.Visible = true;
                allEmployees.Visible = true;
                allHires.Visible = true;
                dashboard.Visible = true;
                manageTitle.Visible = true;
                allUsers.Visible = true;
                ludotheken.Visible = true;
                Console.WriteLine("Ich bin Administrator");
                break;
        }
    }

    protected override void handlePostback()
    {
        Console.Write("Es ist postback!");
    }

    private void setError(String errorMessage)
    {
        flexContainer.InnerHtml = errorMessage;
    }

    public virtual void showCreateHire(Object sender, EventArgs e)
    {
        flexContainer.InnerHtml = "jetzt vermietungsdialog zeigen";
    }

    public virtual void showNewestGames(Object sender, EventArgs e)
    {
        String newestGamesHtml = GetViewletProvider().GetHtmlViewlet().getAllGames(getCurrentSession(), false);
        flexContainer.InnerHtml = newestGamesHtml;
        setSelectedFilter("new");
    }

    public virtual void showClosedHires(Object sender, EventArgs e)
    {
        String closedHires = GetViewletProvider().GetHtmlViewlet().getOwnHires(getCurrentSession());
        flexContainer.InnerHtml = closedHires;
        setSelectedFilter("closed");
    }

    public virtual void showCurrentHires(Object sender, EventArgs e)
    {
        String currentHires = GetViewletProvider().GetHtmlViewlet().getOwnOpenHires(getCurrentSession());
        flexContainer.InnerHtml = currentHires;
        setSelectedFilter("current");
    }

    public virtual void showAllUsers(Object sender, EventArgs e)
    {
        String allUsers = GetViewletProvider().GetHtmlViewlet().getAllUsers(getCurrentSession());
        flexContainer.InnerHtml = allUsers;
        setSelectedFilter("allUsers");
    }

    public virtual void showLudotheken(Object sender, EventArgs e)
    {
        String allLudotheken = GetViewletProvider().GetHtmlViewlet().getAllLudotheken(getCurrentSession());
        flexContainer.InnerHtml = allLudotheken;
        setSelectedFilter("ludotheken");
    }

    /**
     * Manage all Games 
     **/
    public virtual void showAllGames(Object sender, EventArgs e)
    {
        String newestGamesHtml = GetViewletProvider().GetHtmlViewlet().getAllGames(getCurrentSession(), true);
        flexContainer.InnerHtml = newestGamesHtml;
        setSelectedFilter("allGames");
    }

    public virtual void showAllEmployees(Object sender, EventArgs e)
    {
        String allEmployeesHtml = GetViewletProvider().GetHtmlViewlet().getAllEmployees(getCurrentSession());
        flexContainer.InnerHtml = allEmployeesHtml;
        setSelectedFilter("allEmpl");
    }

    public virtual void showAllClients(Object sender, EventArgs e)
    {
        String allClientsHtml = GetViewletProvider().GetHtmlViewlet().getAllClients(getCurrentSession());
        flexContainer.InnerHtml = allClientsHtml;
        setSelectedFilter("allClients");
    }

    public virtual void showAllHires(Object sender, EventArgs e)
    {
        String allHiresHtml = GetViewletProvider().GetHtmlViewlet().getAllHires(getCurrentSession());
        flexContainer.InnerHtml = allHiresHtml;
        setSelectedFilter("allHires");
    }

    public virtual void showDashboard(Object sender, EventArgs e)
    {
        String dashboardHtml = GetViewletProvider().GetHtmlViewlet().getDashboard(getCurrentSession());
        flexContainer.InnerHtml = dashboardHtml;
        setSelectedFilter("dashboard");
    }

    private void setSelectedFilter(String filtername)
    {
        newestGamesLink.CssClass = "";
        currentHiresLink.CssClass = "";
        closedHiresLink.CssClass = "";
        allClients.CssClass = "";
        allEmployees.CssClass = "";
        allGames.CssClass = "";
        dashboard.CssClass = "";
        allHires.CssClass = "";
        ludotheken.CssClass = "";
        allUsers.CssClass = "";

        String selectedCssClass = "selected";
        switch (filtername)
        {
            case "new":
                newestGamesLink.CssClass = selectedCssClass;
                break;
            case "closed":
                closedHiresLink.CssClass = selectedCssClass;
                break;
            case "current":
                currentHiresLink.CssClass = selectedCssClass;
                break;
            case "allEmpl":
                allEmployees.CssClass = selectedCssClass;
                break;
            case "allGames":
                allGames.CssClass = selectedCssClass;
                break;
            case "allClients":
                allClients.CssClass = selectedCssClass;
                break;
            case "dashboard":
                dashboard.CssClass = selectedCssClass;
                break;
            case "allHires":
                allHires.CssClass = selectedCssClass;
                break;
            case "allUsers":
                allUsers.CssClass = selectedCssClass;
                break;
            case "ludotheken":
                ludotheken.CssClass = selectedCssClass;
                break;

        }
        sessionActivity();
    }

}