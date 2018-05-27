using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Zusammenfassungsbeschreibung für FormatUtil
/// </summary>
public class FormatUtil
{
    public static String formatDate(DateTime date, bool withTime)
    {
        if(date==null)
        {
            return "Nicht verfügbar.";
        }
        if(withTime)
        {
            return date.ToString("HH:mm:ss dd.MM.yyyy");
        }
        return date.ToString("dd.MM.yyyy");
    }
}