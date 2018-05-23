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
        /*UserDetail detail = DataProvider.getInstance().getUserDetail(session.userId);
        username.InnerHtml = detail.anrede + " " + detail.vorname + " " + detail.nachname;

        //default page
        showCurrentHires(null, null);*/
    }

    protected override void handlePostback()
    {
        Console.Write("Es ist postback!");
    }

    public virtual void showCreateHire(Object sender, EventArgs e)
    {
        flexContainer.InnerHtml = "jetzt vermietungsdialog zeigen";
    }

    public virtual void showNewestGames(Object sender, EventArgs e)
    {
        String newestGamesHtml = loadNewestGamesIntoContainer();
        flexContainer.InnerHtml = newestGamesHtml;
        setSelectedFilter("new");
    }

    public virtual void showClosedHires(Object sender, EventArgs e)
    {
        String newestGamesHtml = loadClosedHiresIntoContainer();
        flexContainer.InnerHtml = newestGamesHtml;
        setSelectedFilter("closed");
    }

    public virtual void showCurrentHires(Object sender, EventArgs e)
    {
        String newestGamesHtml = loadCurrentHiresIntoContainer();
        flexContainer.InnerHtml = newestGamesHtml;
        setSelectedFilter("current");
    }

    private void setSelectedFilter(String filtername)
    {
        newestGamesLink.CssClass = "";
        currentHiresLink.CssClass = "";
        closedHiresLink.CssClass = "";

        switch (filtername)
        {
            case "new":
                newestGamesLink.CssClass = "selected";
                break;
            case "closed":
                closedHiresLink.CssClass = "selected";
                break;
            case "current":
                currentHiresLink.CssClass = "selected";
                break;
        }
    }

}