using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Zusammenfassungsbeschreibung für Game
/// </summary>
public class Game
{
    public int gameId;
    public String name;
    public String description;
    public String imageLink;
    public int anzahl;

    public Game(int gameId, string name, string description, string imageLink, int anzahl)
    {
        this.gameId = gameId;
        this.name = name;
        this.description = description;
        this.imageLink = imageLink;
        this.anzahl = anzahl;
    }
}