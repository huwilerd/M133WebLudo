using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Zusammenfassungsbeschreibung für AppConst
/// </summary>
public class AppConst
{

    public static String USER_FILE = "C:\\Users\\"+ getUsername() + "\\Documents\\Visual Studio 2015\\Ludothek\\users.txt";
    public static String SESSION_FILE = "C:\\Users\\" + getUsername() + "\\Documents\\Visual Studio 2015\\Ludothek\\sessions.txt";
    public static String DETAIL_FILE = "C:\\Users\\" + getUsername() + "\\Documents\\Visual Studio 2015\\Ludothek\\details.txt";
    public static String GAME_FILE = "C:\\Users\\" + getUsername() + "\\Documents\\Visual Studio 2015\\Ludothek\\games.txt";
    public static String HIRE_FILE = "C:\\Users\\" + getUsername() + "\\Documents\\Visual Studio 2015\\Ludothek\\hires.txt";

    private static String getUsername()
    {
        return Environment.UserName;
    }

    //WEB CONST
    public static String SESSION_KEY = "SESSIONKEYUSER";
    public static String DETAIL_FORM_PAGE_NAME = "PersonalienPAGEID";

    //LOGIC CONST
    public static int HIRE_AMOUNT_DAYS = 7;
    public static int HIRE_MAX_AMOUNT_OF_REHIRES = 3;
}