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
}