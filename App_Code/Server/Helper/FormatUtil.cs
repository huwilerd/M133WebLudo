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

    public static String formatDateInRelationToCurrentDateTimeAsText(DateTime dateTime)
    {
        TimeSpan difference = DateTime.Now - dateTime;
        
        if(difference.TotalSeconds < 60)
        {
            return Convert.ToInt32(difference.TotalSeconds) + " Sekunden";
        } else if(difference.TotalMinutes < 60)
        {
            return Convert.ToInt32(difference.TotalMinutes) + " Minuten";
        } else if(difference.TotalHours < 24)
        {
            return Convert.ToInt32(difference.TotalHours) + " Stunden";
        } else if(difference.TotalDays < 7)
        {
            return Convert.ToInt32(difference.TotalDays) + " Tagen";
        } else
        {
            return formatDate(dateTime, false);
        }
    }
}