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
        List<Dictionary<String, Object>> clientData = CommandUtil.create(getOpenConnection()).executeReader("Select * From Person", null, null);
        List<Person> clientList = new List<Person>();
        clientData.ForEach(delegate (Dictionary<String, Object> row)
        {
            clientList.Add(ConvertUtil.getPerson(row));
        });
        return createResponse(1, "Alle Kunden", clientList, true);
    }

    public ServerResponse getAllGames()
    {
        return createResponse(1, "Alle Spiele", ServerUtil.getAllGames(getOpenConnection()), true);
    }

    public ServerResponse getAllHires()
    {
        List<Dictionary<String, Object>> hireData = CommandUtil.create(getOpenConnection()).executeReader("Select * From Ausleihe", null, null);
        List<Hire> hireList = new List<Hire>();
        hireData.ForEach(delegate (Dictionary<String, Object> row)
        {
            hireList.Add(ConvertUtil.getHire(row));
        });
        return createResponse(1, "Alle Ausleihen", hireList, true);
    }

    public ServerResponse removeGame(Spiel spiel)
    {
        throw new NotImplementedException();
    }
}