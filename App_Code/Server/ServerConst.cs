using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Zusammenfassungsbeschreibung für ServerConst
/// </summary>
public class ServerConst
{
    public static int DEFAULT = -1;

    //Commands
    public static readonly String INSERT_PERSON_QUERY = "INSERT INTO Person(ID_Person, Name, Geschlecht, Geburtsdatum, Einstiegsdatum, FK_Mitgliedschaft) VALUES (@id_person, @name, @geschlecht, @geburtsdatum, @einstiegsdatum, @mitgliedschaft)";
    public static readonly String INSERT_BENUTZER_QUERY = "INSERT INTO Benutzer(FK_Person, password, mail) VALUES (@id , @mail, @password)";
    public static readonly String INSERT_SESSION_QUERY = "INSERT INTO Session (SessionID, FK_Person, activeSession, lastActivity) VALUES (@sessionID, @FkPerson, @activeSession, @lastActivity)";

    public static readonly String SELECT_BENUTZER_ByNameAndPw = "SELECT * FROM Benutzer WHERE mail=@mail AND password=@password";
    public static readonly String SELECT_BENUTZER_ByName = "SELECT * FROM Benutzer WHERE mail=@mail";

}