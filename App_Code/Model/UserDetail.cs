using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Zusammenfassungsbeschreibung für Class1
/// </summary>
public class UserDetail
{

    public int userId;
    public String anrede;
    public String vorname;
    public String nachname;
    public String geburtsdatum;
    public String telefon;
    public String strasse;
    public String postleitzahl;
    public String ort;
    public String land;

    public UserDetail(int userId, string anrede, string vorname, string nachname, string geburtsdatum, string telefon, string strasse, string postleitzahl, string ort, string land)
    {
        this.userId = userId;
        this.anrede = anrede;
        this.vorname = vorname;
        this.nachname = nachname;
        this.geburtsdatum = geburtsdatum;
        this.telefon = telefon;
        this.strasse = strasse;
        this.postleitzahl = postleitzahl;
        this.ort = ort;
        this.land = land;
    }
}