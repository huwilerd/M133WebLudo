using System;
using System.Collections.Generic;

/// <summary>
/// Zusammenfassungsbeschreibung für ConvertUtil
/// </summary>
public class ConvertUtil
{
    
    public static Person getPerson(Dictionary<String, Object> data)
    {
        return new Person(Convert.ToInt32(data["ID_Person"]),
            Convert.ToString(data["Name"]),
            Convert.ToString(data["Geschlecht"]),
            Convert.ToDateTime(data["Geburtsdatum"]),
            Convert.ToDateTime(data["Einstiegsdatum"]),
            new Mitgliedschaft(data["FK_Mitgliedschaft"] == DBNull.Value ? -1 : Convert.ToInt32(data["FK_Mitgliedschaft"]), null, null, DateTime.Now, DateTime.Now),
            Convert.ToString(convertDBNullToEmpty(data["Strasse"])),
            Convert.ToInt32(convertDBNullToZero(data["Postleitzahl"])),
            Convert.ToString(convertDBNullToEmpty(data["Ort"])),
            Convert.ToString(convertDBNullToEmpty(data["Land"])));
    }

    public static Spiel getSpiel(Dictionary<String, Object> data)
    {
        return new Spiel(Convert.ToInt32(data["ID_Spiel"]),
            Convert.ToString(data["Name"]),
            Convert.ToString(data["Verlag"]),
            Convert.ToInt32(data["Lagerbestand"]),
            Convert.ToInt32(data["FK_Tarifkategorie"]),
            Convert.ToInt32(data["FK_Kategorie"]));
    }

    public static Hire getHire(Dictionary<String, Object> data)
    {
        return new Hire(Convert.ToInt32(data["ID_Ausleihe"]),
            Convert.ToInt32(data["FK_Person"]),
            Convert.ToInt32(data["FK_Spiel"]),
            Convert.ToDateTime(data["VonDatum"]),
            Convert.ToDateTime(data["BisDatum"]),
            Convert.ToBoolean(data["Bezahlt"]));
    }

    private static object convertDBNullToEmpty(object value)
    {
        if(value==DBNull.Value)
        {
            return "";
        }
        return value;
    }

    private static object convertDBNullToZero(object value)
    {
        if (value == DBNull.Value)
        {
            return 0;
        }
        return value;
    }

    public static Session getSession(Dictionary<String, Object> data)
    {
        return new Session(Convert.ToString(data["sessionID"]),
            Convert.ToInt32(data["FK_Person"]),
            Convert.ToBoolean(data["activeSession"]),
            Convert.ToDateTime(data["lastActivity"]));
    }

    public static User getUser(Dictionary<String, Object> data)
    {
        return new User(Convert.ToInt32(data["FK_Person"]),
            Convert.ToString(data["mail"]),
            Convert.ToString(data["password"]));
    }

}