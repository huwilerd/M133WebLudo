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
        hideUnrequiredElements();
        setDefaultPage();

    }

    private void setDefaultPage()
    {
        showCurrentHires(null, null);
    }

    private void hideUnrequiredElements()
    {
        addGameForm.Visible = false;
    }

    private void handleSessionRole()
    {
        SessionRole role = getCurrentSession().sessionRole;
        Console.WriteLine("SessionRolle ist: " + role.ToString());
        switch (role)
        {
            case SessionRole.Client:
                allEmployees.Visible = false;
                allClients.Visible = false;
                allGames.Visible = false;
                dashboard.Visible = false;
                Console.WriteLine("Ich bin Client");
                break;
            case SessionRole.Employee:
                allClients.Visible = true;
                allGames.Visible = true;
                Console.WriteLine("Ich bin Employee");
                break;
            case SessionRole.Administrator:
                dashboard.Visible = true;
                Console.WriteLine("Ich bin Administrator");
                break;
        }
    }

    protected override void handlePostback()
    {
        Console.Write("Es ist postback!");
    }

    public virtual void addGame(Object sender, EventArgs e)
    {

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

        setSelectedFilter("allEmpl");
    }

    public virtual void showAllClients(Object sender, EventArgs e)
    {

        setSelectedFilter("allClients");
    }

    public virtual void showDashboard(Object sender, EventArgs e)
    {

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
        addGameForm.Visible = false;


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
                addGameForm.Visible = true;
                break;
            case "allClients":
                allClients.CssClass = selectedCssClass;
                break;
            case "dashboard":
                dashboard.CssClass = selectedCssClass;
                break;

        }
    }

}