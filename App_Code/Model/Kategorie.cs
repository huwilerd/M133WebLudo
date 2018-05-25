using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Zusammenfassungsbeschreibung für Kategorie
/// </summary>
public class Kategorie
{
    public int ID_Kategorie;
    public String Name;
    public int Altersfreigabe;

    public Kategorie(int iD_Kategorie, string name, int altersfreigabe)
    {
        ID_Kategorie = iD_Kategorie;
        Name = name;
        Altersfreigabe = altersfreigabe;
    }
}