using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Zusammenfassungsbeschreibung für EmployeeViewlet
/// </summary>
public class EmployeeViewlet : MasterViewlet, EmployeeInterface
{
    public ServerResponse addNewGame(Spiel newSpiel)
    {
        int generatedGameId = ServerUtil.generateNewIdForTable("Spiel", "ID_Spiel", getOpenConnection());
        bool addState = CommandUtil.create(getOpenConnection()).executeSingleQuery("INSERT INTO Spiel VALUES(@gameId, @name, @verlag, @lagerbestand, @fktarifkategorie, @fkkategorie)",
            new string[] {"@gameId", "@name", "@verlag", "@lagerbestand", "@fktarifkategorie", "@fkkategorie" },
            new object[] { generatedGameId, newSpiel.name, newSpiel.verlag, newSpiel.lagerbestand, newSpiel.tarifkategorie, newSpiel.kategorie});
        return addState ? createResponse(1, "Spiel erfolgreich hinzugefügt", null, true) : createResponse(1, "Spiel konnte nicht hinzugefügt werden", null, false);
    }

    public ServerResponse closeHire(Hire hire)
    {
        throw new NotImplementedException();
    }

    public ServerResponse getAllClients()
    {
        throw new NotImplementedException();
    }

    public ServerResponse getAllGames()
    {
        throw new NotImplementedException();
    }

    public ServerResponse getAllHires()
    {
        throw new NotImplementedException();
    }

    public ServerResponse removeGame(Spiel spiel)
    {
        throw new NotImplementedException();
    }
}