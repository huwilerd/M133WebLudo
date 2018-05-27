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

    private ValidateResult createResult(String message, bool status)
    {
        return new ValidateResult(message, status);
    }
}