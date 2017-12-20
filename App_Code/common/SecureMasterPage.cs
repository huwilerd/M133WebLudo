using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

/// <summary>
/// Zusammenfassungsbeschreibung für SecureMasterPage
/// </summary>
public abstract class SecureMasterPage : MasterPage
{
    public SecureMasterPage()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    protected override bool isSecurePage()
    {
        return true;
    }

    protected String loadNewestGamesIntoContainer()
    {
        StringBuilder htmlBuilder = new StringBuilder();
        List<Game> games = DataProvider.getInstance().getAllGame();
        foreach(Game game in games)
        {
            htmlBuilder.Append(createGameElement(game, "Ausleihe reservieren"));
        }
        if(games.Count == 0)
        {
            htmlBuilder.Append("<b>Es sind aktuell leider keine Spiele vorhanden.");
        }
        return htmlBuilder.ToString();
    }

    protected String loadCurrentHiresIntoContainer()
    {
        int userId = DataProvider.getInstance().getUserFromToken(getSessionKey()).userId;
        StringBuilder htmlBuilder = new StringBuilder();
        List<Hire> hires = DataProvider.getInstance().getAllOpenHiresOfClient(userId);
        foreach(Hire hire in hires)
        {
            htmlBuilder.Append(createHireElement(hire, "Jetzt verlängern"));
        }
        if (hires.Count == 0)
        {
            htmlBuilder.Append("<b>Es sind noch keine Ausleihen vorhanden.</b>");
        }
        return htmlBuilder.ToString();
    }

    protected String loadClosedHiresIntoContainer()
    {
        int userId = DataProvider.getInstance().getUserFromToken(getSessionKey()).userId;
        StringBuilder htmlBuilder = new StringBuilder();
        List<Hire> hires = DataProvider.getInstance().getAllClosedHiresOfClient(userId);
        foreach (Hire hire in hires)
        {
            htmlBuilder.Append(createHireElement(hire, "Abgeschlossen"));
        }
        if (hires.Count == 0)
        {
            htmlBuilder.Append("<b>Es sind noch keine abgeschlossenen Ausleihen vorhanden.</b>");
        }
        return htmlBuilder.ToString();
    }

    private String createHireElement(Hire hire, String buttonText)
    {
        Game game = DataProvider.getInstance().getGameFromId(hire.gameId);
        StringBuilder html = new StringBuilder();
        html.Append("<div class=\"flexItem\">");
        html.Append("<div class=\"mainItemImage\"> <img src=\"" + game.imageLink + "\" /></div>");
        html.Append("<div class=\"mainItemTitle\"><h1>" + game.name + "</h1><p>Vermietung gültig von <b>"+ hire.fromDate.ToString("dd.MM.yyyy") +"</b> bis <b>"+ hire.toDate.ToString("dd.MM.yyyy") +"</b></p></div>");
        html.Append("<a href=\"Detail.aspx?id=" + game.gameId + "&hire="+hire.hireId+"&action=1\" class=\"button\" style=\"vertical-align:middle\"><span>" + buttonText + "</span></button>");
        html.Append("</div></a>");
        return html.ToString();
    }

    private String createGameElement(Game game, String buttonText)
    {
        StringBuilder html = new StringBuilder();
        html.Append("<div class=\"flexItem\">");
        html.Append("<div class=\"mainItemImage\"> <img src=\"" + game.imageLink + "\" /></div>");
        html.Append("<div class=\"mainItemTitle\"><h1>" + game.name + "</h1><p>" + game.description + "</p></div>");
        html.Append("<a href=\"Detail.aspx?id="+game.gameId+"&action=0\" class=\"button\" style=\"vertical-align:middle\"><span>" + buttonText + "</span></button>");
        html.Append("</div></a>");
        return html.ToString();
    }
}