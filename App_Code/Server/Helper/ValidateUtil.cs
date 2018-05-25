using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Zusammenfassungsbeschreibung für ValidateUtil
/// </summary>
public class ValidateUtil : MasterViewlet
{
    private static ValidateUtil instance;

    public static ValidateUtil getInstance()
    {
        if(instance==null)
        {
            instance = new ValidateUtil();
        }
        return instance;
    }

    private ValidateUtil() { }

    public ValidateResult validateNewHire(Hire hire)
    {
        Spiel hireGame = ServerUtil.getGameForId(hire.FK_Spiel, getOpenConnection());
        if(hireGame.lagerbestand==0)
        {
            return createResult("Leider ist kein Spiel mehr an Lager.", false);
        }

        return createResult("Ok", true);
    }

    public ValidateResult validateClosingHire(Hire hire)
    {
        if(hire==null)
        {
            return createResult("Ausleihe existiert nicht", false);
        }
        if(hire.Bezahlt)
        {
            return createResult("Ausleihe ist bereits abgeschlossen", false);
        }
        return createResult("Ok", true);
    }

    public ValidateResult validateBeforeExtendingHire(Hire hire)
    {
        if (hire == null)
        {
            return createResult("Ausleihe existiert nicht", false);
        }
        if (hire.Bezahlt)
        {
            return createResult("Ausleihe ist bereits abgeschlossen", false);
        }
        TimeSpan span = hire.BisDatum - hire.VonDatum;
        int amountHiredWeeks = Convert.ToInt32(span.TotalDays / AppConst.HIRE_AMOUNT_DAYS) - 1;

        if(amountHiredWeeks > AppConst.HIRE_MAX_AMOUNT_OF_REHIRES)
        {
            return createResult("Die Ausleihe kann nicht mehr als " + AppConst.HIRE_MAX_AMOUNT_OF_REHIRES + " Wochen verlängert werden", false);
        }
        return createResult("Ok", true);
    }

    public ValidateResult validateBeforeOpeningHire(Hire hire)
    {
        if (hire == null)
        {
            return createResult("Ausleihe existiert nicht", false);
        }
        if (!hire.Bezahlt)
        {
            return createResult("Ausleihe ist bereits offen", false);
        }
        return createResult("Ok", true);
    }

    public ValidateResult validateGettingHire(Hire hire, Session session)
    {
        if(session.sessionRole.Equals(SessionRole.Client)) {
            if (hire.FK_Person != session.FK_Person)
            {
                return createResult("Es ist nicht erlaubt auf andere Ausleihen zuzugreifen.", false);
            }
        }
        return createResult("Ok", true);
    }

    public ValidateResult validatePostleitzahl(String plzText)
    {
       
            int plzResult;
            bool isNumeric = int.TryParse(plzText, out plzResult);
            if(isNumeric)
            {
                if(plzResult > 1000 && plzResult < 10000)
                {
                    return createResult("Ok", true);
                }
            }
        return createResult("Bitte geben Sie eine gültige Postleitzahl an", false);
    }

    public ValidateResult validateEmail(String emailText)
    {
        var mailChecker = new System.ComponentModel.DataAnnotations.EmailAddressAttribute();
        if(mailChecker.IsValid(emailText))
        {
            return createResult("OK", true);
        }
        return createResult("Bitte geben Sie eine gültige E-Mail an", false);
    }

    public ValidateResult validatePerson(Person person)
    {
        TimeSpan age = DateTime.Now - person.Geburtsdatum;

        if (age.TotalDays < AppConst.MIN_AGE * 365.25)
        {
            return createResult("Man muss älter als " + AppConst.MIN_AGE + " Jahre sein", false);
        }
        if (person.land.ToLower() != "schweiz")
        {
            return createResult("Kunden müssen aus der Schweiz sein", false);
        }

        return createResult("OK", true);
    }

    private ValidateResult createResult(String message, bool status)
    {
        return new ValidateResult(message, status);
    }
}