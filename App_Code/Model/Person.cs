using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Zusammenfassungsbeschreibung für Person
/// </summary>
public class Person
{

    public int ID_Person;
    public String Name;
    public String Geschlecht;
    public DateTime Geburtsdatum;
    public DateTime Einstiegsdatum;
    public Mitgliedschaft mitgliedschaft;
    public String strasse;
    public int postleitzahl;
    public String ort;
    public String land;

    public Person(int iD_Person, string name, string geschlecht, DateTime geburtsdatum, DateTime einstiegsdatum)
    {
        ID_Person = iD_Person;
        Name = name;
        Geschlecht = geschlecht;
        Geburtsdatum = geburtsdatum;
        Einstiegsdatum = einstiegsdatum;
    }

    public Person(int iD_Person, string name, string geschlecht, DateTime geburtsdatum, DateTime einstiegsdatum, Mitgliedschaft mitgliedschaft) : this(iD_Person, name, geschlecht, geburtsdatum, einstiegsdatum)
    {
        ID_Person = iD_Person;
        Name = name;
        Geschlecht = geschlecht;
        Geburtsdatum = geburtsdatum;
        Einstiegsdatum = einstiegsdatum;
        this.mitgliedschaft = mitgliedschaft;
    }

    public Person(int iD_Person, string name, string geschlecht, DateTime geburtsdatum, DateTime einstiegsdatum, Mitgliedschaft mitgliedschaft, string strasse, int postleitzahl, string ort, string land) : this(iD_Person, name, geschlecht, geburtsdatum, einstiegsdatum, mitgliedschaft)
    {
        ID_Person = iD_Person;
        Name = name;
        Geschlecht = geschlecht;
        Geburtsdatum = geburtsdatum;
        Einstiegsdatum = einstiegsdatum;
        this.mitgliedschaft = mitgliedschaft;
        this.strasse = strasse;
        this.postleitzahl = postleitzahl;
        this.ort = ort;
        this.land = land;
    }

    public object getMitgliedschaftsId()
    {
        if(mitgliedschaft==null || mitgliedschaft.ID_Mitgliedschaft == -1)
        {
            return DBNull.Value;
        }
        return mitgliedschaft.ID_Mitgliedschaft;
    }
}