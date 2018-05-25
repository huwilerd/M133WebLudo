using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Zusammenfassungsbeschreibung für HtmlViewlet
/// </summary>
public class HtmlViewlet : MasterViewlet, HtmlInterface
{
    public string getAllClients(Session session)
    {
        return "Alle Kunden";
    }

    public string getAllGames(Session session, bool manageView)
    {
        ServerResponse allGamesResponse = ServerViewletProvider.getInstance().GetEmployeeInterface().getAllGames();
        if(allGamesResponse.getResponseStatus())
        {
            List<Spiel> gameList = (List<Spiel>)allGamesResponse.getResponseObject();
            if (manageView)
            {
                return HtmlUtil.generateGameOverviewListHtml(gameList);
            }
            return HtmlUtil.generateGameFlexListHtml(gameList);
        }
        return HtmlUtil.generateErrorMessage(allGamesResponse.getResponseMessage());
    }

    public string getAllHires(Session session)
    {
        return "Alle Ausleihen";
    }

    public string getDashboard(Session session)
    {
        return "Dashboard";
    }

    public string getOwnHires(Session session)
    {
        return "Eigene Vermietungen";
    }

    public string getOwnOpenHires(Session session)
    {
        return "Eigene offenen Vermietungen";
    }
}