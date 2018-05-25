using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Zusammenfassungsbeschreibung für TarifKategorie
/// </summary>
public class TarifKategorie
{
    public int ID_TarifKategorie;
    public String Name;
    public float NormalPreis;
    public float MitgliedschaftsAuflage;

    public TarifKategorie(int iD_TarifKategorie, string name, float normalPreis, float mitgliedschaftsAuflage)
    {
        ID_TarifKategorie = iD_TarifKategorie;
        Name = name;
        NormalPreis = normalPreis;
        MitgliedschaftsAuflage = mitgliedschaftsAuflage;
    }
}