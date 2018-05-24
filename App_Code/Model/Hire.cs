using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Zusammenfassungsbeschreibung für Hire
/// </summary>
public class Hire
{
    public int ID_Ausleihe;
    public int FK_Person;
    public int FK_Spiel;
    public DateTime VonDatum;
    public DateTime BisDatum;
    public bool Bezahlt;

    public Hire(int iD_Ausleihe, int fK_Person, int fK_Spiel, DateTime vonDatum, DateTime bisDatum, bool bezahlt)
    {
        ID_Ausleihe = iD_Ausleihe;
        FK_Person = fK_Person;
        FK_Spiel = fK_Spiel;
        VonDatum = vonDatum;
        BisDatum = bisDatum;
        Bezahlt = bezahlt;
    }
}