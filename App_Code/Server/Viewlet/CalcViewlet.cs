using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Zusammenfassungsbeschreibung für CalcViewlet
/// </summary>
public class CalcViewlet : MasterViewlet
{

    public static CalcViewlet create()
    {
        return new CalcViewlet();
    }
    
    public double calculatePrice(Session session, Spiel spiel, Person person)
    {
        if(spiel==null)
        {
            throw new Exception("Game can't be null in a price calculation");
        }

        if(session.sessionRole.Equals(SessionRole.Client) && session.FK_Person != person.ID_Person)
        {
            throw new Exception("Client is restricted to calculate price of other hires");
        }

        TarifKategorie tarifKategorie = GetTarifKategorie(spiel);

        bool isMitglied = !(person.mitgliedschaft == null || person.mitgliedschaft.ID_Mitgliedschaft == -1);

        double priceUserHasToPay = isMitglied ? tarifKategorie.MitgliedschaftsAuflage : tarifKategorie.NormalPreis;

        return priceUserHasToPay;
    }

    public List<TarifKategorie> GetTarifKategories()
    {
        return ServerUtil.GetAllTarifKategories(getOpenConnection());
    }

    public List<Kategorie> GetKategories()
    {
        return ServerUtil.GetAllKategories(getOpenConnection());
    }

    public String getKategorieName(int kategorieId)
    {
        return ServerUtil.getKategorieNameFromId(kategorieId, getOpenConnection());
    }

    public static String wrapInCurrency(double price)
    {
        return "CHF " + Math.Round(price, 2);
    }

    private Person getPerson(Hire hire)
    {
        Person person = ServerUtil.getPersonFromId(hire.FK_Person, getOpenConnection());
        if (person == null)
        {
            throw new Exception("Can't calculate hire without a person");
        }
        return person;
    }

    private TarifKategorie GetTarifKategorie(Spiel spiel)
    {
        TarifKategorie tarifKategorie = ServerUtil.GetTarifKategorie(spiel.tarifkategorie, getOpenConnection());
        if (tarifKategorie == null)
        {
            throw new Exception("Tarifkategorie can't be null in a calculation");
        }
        return tarifKategorie;
    }

}