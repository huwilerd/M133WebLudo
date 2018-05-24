using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Zusammenfassungsbeschreibung für Game
/// </summary>
public class Spiel
{
    public int ID_Spiel;
    public String name;
    public String verlag;
    public int lagerbestand;
    public int tarifkategorie;
    public int kategorie;

    public Spiel(int iD_Spiel, string name, string verlag, int lagerbestand, int tarifkategorie, int kategorie)
    {
        ID_Spiel = iD_Spiel;
        this.name = name;
        this.verlag = verlag;
        this.lagerbestand = lagerbestand;
        this.tarifkategorie = tarifkategorie;
        this.kategorie = kategorie;
    }
}