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
            Convert.ToDateTime(data["Einstiegsdatum"]));
    }

    public static Session getSession(Dictionary<String, Object> data)
    {
        return new Session(Convert.ToString(data["sessionID"]),
            Convert.ToInt32(data["FK_Person"]),
            Convert.ToBoolean(data["activeSession"]),
            Convert.ToDateTime(data["lastActivity"]));
    }

}