using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

/// <summary>
/// Zusammenfassungsbeschreibung für HtmlUtil
/// </summary>
public class HtmlUtil
{
    
    public static String generateOwnHireOverviewListHtml(List<Hire> hireList, SqlConnection openConnection)
    {
        StringBuilder builder = new StringBuilder();

        if (hireList.Count > 0)
        {
            hireList.ForEach(delegate (Hire hire)
            {
                Spiel currentGame = ServerUtil.getGameForId(hire.FK_Spiel, openConnection);
                String description = "Ausgeliehen von " + FormatUtil.formatDate(hire.VonDatum, false) + " bis " + FormatUtil.formatDate(hire.BisDatum, false) + ".";
                builder.Append(createFlexGameElement(hire, currentGame, description, "Verlängern", true));
            });
        } else
        {
            builder.Append("Keine offenen Ausleihen zur Auflistung vorhanden.");
        }

        return builder.ToString();
    }

    public static String generateOwnClosedHireOverviewListHtml(List<Hire> hireList, SqlConnection openConnection)
    {
        StringBuilder builder = new StringBuilder();
        if (hireList.Count > 0)
        {
            hireList.ForEach(delegate (Hire hire)
            {
                Spiel currentGame = ServerUtil.getGameForId(hire.FK_Spiel, openConnection);
                String description = "Ausgeliehen von " + FormatUtil.formatDate(hire.VonDatum, false) + " bis " + FormatUtil.formatDate(hire.BisDatum, false) + ".";
                builder.Append(createFlexGameElement(hire, currentGame, description, "Abgeschlossen", false));
            });
        } else
        {
            builder.Append("Keine abgeschlossenen Ausleihen zur Auflistung vorhanden.");
        }

        return builder.ToString();
    }

    public static String generateEmployeeList(List<Person> employeeList)
    {
        StringBuilder builder = new StringBuilder();

        if (employeeList.Count > 0)
        {
            builder.Append("<table>");
            builder.Append("<tr><th colspan='3'><h2>Alle Mitarbeiter</h2></th></tr>");
            builder.Append("<tr><th>ID</th><th>Name</th><th>Adresse</th><th>Einstiegsdatum</th></tr>");
            employeeList.ForEach(delegate (Person employee)
            {
                builder.Append(createManageTableRowEmployee(employee));
            });
            builder.Append("</table>");
        }
        else
        {
            builder.Append("Keine Mitarbeiter zur Auflistung vorhanden.");
        }

        return builder.ToString();
    }

    /**
     * For small overview page 
     **/
    public static String generateGameOverviewListHtml(List<Spiel> gameList)
    {
        StringBuilder builder = new StringBuilder();

        builder.Append("<table>");
        builder.Append("<tr><th colspan='2'><h2>Alle Spiele</h2></th></tr>");
        builder.Append("<tr><th>Spiel-Nr.</th><th>Spielname</th><th>Verlang</th><th>Kategorie</th><th>Tarifkategorie</th><th>Lagerbestand</th><th>Aktionen</th></tr>");
        gameList.ForEach(delegate (Spiel spiel)
        {
            builder.Append(createOverviewGameElement(spiel));
        });
        builder.Append("</table>");

        builder.Append("<div><a href=\"EditGame.aspx?action=new\">Neues Spiel erfassen</a></div>");

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
            bool activeStatus = spiel.lagerbestand > 0;
            String buttonText = activeStatus ? "Jetzt ausleihen" : "Nicht verfügbar";
            builder.Append(createFlexGameElement(null, spiel, Convert.ToString(spiel.tarifkategorie)/*Todo: Description game*/, buttonText, activeStatus));
        });
        return builder.ToString();
    }

    public static String createManageTableRowEmployee(Person employee)
    {
        return "<tr><td>" + employee.ID_Person + "</td><td>" + employee.Name + "</td><td>" + employee.strasse + ", "+employee.postleitzahl+" "+employee.ort+", "+employee.land+"</td><td>"+ FormatUtil.formatDate(employee.Einstiegsdatum, false)+"</td></tr>";
    }

    public static String createManageTableRowClient(Person client)
    {
        String mitgliedschaftsText = client.mitgliedschaft == null || client.mitgliedschaft.ID_Mitgliedschaft == -1 ? "Nicht vorhanden" : "Vorhanden";
        return "<tr><td>" + client.ID_Person + "</td><td>" + client.Name + "</td><td>"+client.Geschlecht+"</td><td>"+ FormatUtil.formatDate(client.Geburtsdatum, false)+"</td><td>" + client.strasse + ", " + client.postleitzahl + " " + client.ort + ", " + client.land + "</td><td>"+ FormatUtil.formatDate(client.Einstiegsdatum, false)+"</td><td>"+mitgliedschaftsText+"</td></tr>";
    }

    public static String createOverviewGameElement(Spiel game)
    {
        String actionLinkHtml = "<a href=\"EditGame.aspx?action=edit&page=5&game="+game.ID_Spiel+ "\">Bearbeiten</a>";
        return "<tr><td>" + game.ID_Spiel + "</td><td>" + game.name + "</td><td>" + game.verlag + "</td><td>"+game.kategorie+"</td><td>"+game.tarifkategorie+"</td><td>"+game.lagerbestand+"</td><td>"+actionLinkHtml+"</td></tr>";
    }

    public static String createFlexGameElement(Hire hire, Spiel game, String descriptionText, String buttonText, bool active)
    {
        String actionParams = hire == null ? "action=0" : "action=1&hire=" + hire.ID_Ausleihe;
        StringBuilder html = new StringBuilder();
        html.Append("<div class=\"flexItem\">");
        html.Append("<div class=\"mainItemImage\"> <img src=\"https://wwwtalks.com/wp-content/uploads/2018/04/default-blog.jpg\" /></div>");
        html.Append("<div class=\"mainItemTitle\"><h1>" + game.name + "</h1><p>" + descriptionText + "</p></div>");
        if (active)
        {
            html.Append("<a href=\"Detail.aspx?id=" + game.ID_Spiel + "&"+ actionParams + "\" class=\"button\" style=\"vertical-align:middle\"><span>" + buttonText + "</span></a>");
        } else
        {
            html.Append("<a href=\"#!\" class=\"button notactive\" style=\"vertical-align:middle\"><span>"+buttonText+"</span></a>");
        }
        html.Append("</div></a>");
        return html.ToString();
    }

    public static String generateErrorMessage(String errorText)
    {
        return "<div class='error'>" + errorText + "</div>";
    }

    public static String generateDashboard(int amountHires, int amountEmployees, int amountClients)
    {
        StringBuilder builder = new StringBuilder();

        builder.Append("<h2>Ausleihen: "+amountHires+"</h2><br>");
        builder.Append("<h2>Mitarbeiter: "+amountEmployees+"</h2><br>");
        builder.Append("<h2>Kunden: "+amountClients+"</h2>");

        return builder.ToString();
    }

    public static String generateClientTable(List<Person> clientList)
    {
        StringBuilder builder = new StringBuilder();

        if (clientList.Count > 0)
        {
            builder.Append("<table>");
            builder.Append("<tr><th colspan='3'><h2>Alle Kunden</h2></th></tr>");
            builder.Append("<tr><th>ID</th><th>Name</th><th>Geschlecht</th><th>Geburtsdatum</th><th>Adresse</th><th>Einstiegsdatum</th><th>Mitgliedschaft</th></tr>");
            clientList.ForEach(delegate (Person client)
            {
                builder.Append(createManageTableRowClient(client));
            });
            builder.Append("</table>");
        }
        else
        {
            builder.Append("Keine Kunden zur Auflistung vorhanden.");
        }

        return builder.ToString();
    }

    public static String generateOverviewTable(List<Hire> hireList, String title, SqlConnection openConnection)
    {
        StringBuilder builder = new StringBuilder();

        builder.Append("<table>");
        builder.Append("<tr><th colspan='5'><h2>"+title+"</h2></th></tr>");
        builder.Append("<tr><th>Ausleih-Nr.</th><th>Kunden-Nr.</th><th>Kundenname</th><th>Spielname</th><th>Zeitspanne</th><th>Status</th><th>Aktionen</th></tr>");
        hireList.ForEach(delegate (Hire hire)
        {
            Person person = ServerUtil.getPersonFromId(hire.FK_Person, openConnection);
            Spiel spiel = ServerUtil.getGameForId(hire.FK_Spiel, openConnection);
            builder.Append(getHireOverviewTableRow(hire, person, spiel));
        });
        builder.Append("</tabel>");
        return builder.ToString();
    }

    private static String getHireOverviewTableRow(Hire hire, Person person, Spiel spiel)
    {
        String hireStatus = hire.Bezahlt ? "Abgeschlossen" : "Offen";
        StringBuilder builder = new StringBuilder();
        builder.Append("<tr>");
        builder.Append("<td>").Append(hire.ID_Ausleihe).Append("</td>");
        builder.Append("<td>").Append(person.ID_Person).Append("</td>");
        builder.Append("<td>").Append(person.Name).Append("</td>");
        builder.Append("<td>").Append(spiel.name).Append("</td>");
        builder.Append("<td>").Append(FormatUtil.formatDate(hire.VonDatum, false)+" - "+ FormatUtil.formatDate(hire.BisDatum, false)).Append("</td>");
        builder.Append("<td>").Append(hireStatus).Append("</td>");
        builder.Append("<td>");
        if(!hire.Bezahlt)
        {
            builder.Append("<a href=\"MainMenu.aspx?action=close&hire="+hire.ID_Ausleihe+"\">Abschliessen</a>&nbsp;");
        }
        builder.Append("<a href=\"MainMenu.aspx?action=delete&hire="+hire.ID_Ausleihe+"\">Löschen</a></td>");
        builder.Append("</tr>");
        return builder.ToString();
    }

}