﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Alle relevanten Konstanten sind in dieser Klasse vorhanden
/// </summary>
public class ServerConst
{
    //Connection
    public static String CONNECTION_STRING = "DESKTOP-QC9NDF8\\SQLEXPRESS";
    public static String CONNECTION_DATABASE = "Ludothek";

    public static int DEFAULT = -1;

    //Session
    public static readonly int SESSION_TIMEOUT_MINUTES = 10;

    //Commands
    public static readonly String INSERT_PERSON_QUERY = "INSERT INTO Person(ID_Person, Name, Geschlecht, Geburtsdatum, Einstiegsdatum, FK_Mitgliedschaft) VALUES (@id_person, @name, @geschlecht, @geburtsdatum, @einstiegsdatum, @mitgliedschaft)";
    public static readonly String INSERT_BENUTZER_QUERY = "INSERT INTO Benutzer(FK_Person, password, mail) VALUES (@id , @mail, @password)";
    

    
    public static readonly String SELECT_BENUTZER_ByName = "SELECT * FROM Benutzer WHERE mail=@mail";

    //Stored Procedures
    public static readonly String SESSION_ACTIVITY_PROCEDURE = "Update_Session";
    public static readonly String LOGIN_STORED_PROCEDURE = "Login_Proc";
    public static readonly String INSERT_SESSION_PROCEDURE = "Insert_Session";
    public static readonly String SELECT_SESSION_PROCEDURE = "Select_Session";

}