using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Zusammenfassungsbeschreibung für Ludothek
/// </summary>
public class Ludothek
{
    public int ID_Ludothek;
    public String name;
    public String strasse;
    public int postleitzahl;
    public String ort;
    public int verband;

    public Ludothek(int iD_Ludothek, string name, string strasse, int postleitzahl, string ort, int verband)
    {
        ID_Ludothek = iD_Ludothek;
        this.name = name;
        this.strasse = strasse;
        this.postleitzahl = postleitzahl;
        this.ort = ort;
        this.verband = verband;
    }
}