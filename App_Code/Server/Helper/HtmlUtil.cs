using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

/// <summary>
/// Zusammenfassungsbeschreibung für HtmlUtil
/// </summary>
public class HtmlUtil
{
    
    /**
     * For small overview page 
     **/
    public static String generateGameOverviewListHtml(List<Spiel> gameList)
    {
        StringBuilder builder = new StringBuilder();

        gameList.ForEach(delegate (Spiel spiel)
        {
            builder.Append(createOverviewGameElement(spiel));
        });

        return builder.ToString();
    }

    /**
     * For flexbox overview with large boxes 
     **/
    public static String generateGameFlexListHtml(List<Spiel> gameList)
    {
        StringBuilder builder = new StringBuilder();
        gameList.ForEach(delegate (Spiel spiel)
        {
            builder.Append(createFlexGameElement(spiel, "Ja was", true));
        });
        return builder.ToString();
    }

    public static String createOverviewGameElement(Spiel game)
    {
        return "<div><table><tr><td>" + game.ID_Spiel + "</td><td>" + game.name + "</td><td>" + game.verlag + "</td></tr></table></div>";
    }

    public static String createFlexGameElement(Spiel game, String buttonText, bool active)
    {
        StringBuilder html = new StringBuilder();
        html.Append("<div class=\"flexItem\">");
        html.Append("<div class=\"mainItemImage\"> <img src=\"https://wwwtalks.com/wp-content/uploads/2018/04/default-blog.jpg\" /></div>");
        html.Append("<div class=\"mainItemTitle\"><h1>" + game.name + "</h1><p>" + game.tarifkategorie + "</p></div>");
        if (active)
        {
            html.Append("<a href=\"Detail.aspx?id=" + game.ID_Spiel + "&action=0\" class=\"button\" style=\"vertical-align:middle\"><span>" + buttonText + "</span></a>");
        } else
        {
            html.Append("<a href=\"#!\" class=\"button notactive\" style=\"vertical-align:middle\"><span>Nicht verfügbar</span></a>");
        }
        html.Append("</div></a>");
        return html.ToString();
    }

    public static String generateErrorMessage(String errorText)
    {
        return "<div class='error'>" + errorText + "</div>";
    }

}