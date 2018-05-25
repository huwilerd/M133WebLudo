using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Zusammenfassungsbeschreibung für Verband
/// </summary>
public class Verband
{
    public int ID_Verband;
    public String Name;

    public Verband(int iD_Verband, string name)
    {
        ID_Verband = iD_Verband;
        Name = name;
    }
}