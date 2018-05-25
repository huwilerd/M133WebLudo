using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Zusammenfassungsbeschreibung für AppConst
/// </summary>
public class AppConst
{ 
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

    //PERSON
    public static int MIN_AGE = 16;
}