using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Zusammenfassungsbeschreibung für Mitgliedschaft
/// </summary>
public class Mitgliedschaft
{
    public int ID_Mitgliedschaft;
    public String Status;
    public String Rechnungsstatus;
    public DateTime Erstellungsdatum;
    public DateTime AuslaufDatum;

    public Mitgliedschaft(int iD_Mitgliedschaft, string status, string rechnungsstatus, DateTime erstellungsdatum, DateTime auslaufDatum)
    {
        ID_Mitgliedschaft = iD_Mitgliedschaft;
        Status = status;
        Rechnungsstatus = rechnungsstatus;
        Erstellungsdatum = erstellungsdatum;
        AuslaufDatum = auslaufDatum;
    }
}