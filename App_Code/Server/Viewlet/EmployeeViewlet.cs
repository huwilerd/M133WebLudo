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
        if(newSpiel.ID_Spiel == -1)
        {
            newSpiel.ID_Spiel = ServerUtil.generateNewIdForTable("Spiel", "ID_Spiel", getOpenConnection());
        }
        bool addState = CommandUtil.create(getOpenConnection()).executeSingleQuery("INSERT INTO Spiel VALUES(@gameId, @name, @verlag, @lagerbestand, @fktarifkategorie, @fkkategorie)",
            new string[] {"@gameId", "@name", "@verlag", "@lagerbestand", "@fktarifkategorie", "@fkkategorie" },
            new object[] { newSpiel.ID_Spiel, newSpiel.name, newSpiel.verlag, newSpiel.lagerbestand, newSpiel.tarifkategorie, newSpiel.kategorie});
        return addState ? createResponse(1, "Spiel erfolgreich hinzugefügt", null, true) : createResponse(1, "Spiel konnte nicht hinzugefügt werden", null, false);
    }

    public ServerResponse closeHire(int idHire)
    {
        Hire toCloseHire = ServerUtil.getHireFromId(idHire, getOpenConnection());
        ValidateResult validateResult = ValidateUtil.getInstance().validateClosingHire(toCloseHire);
        if(validateResult.validateStatus)
        {
            bool closeState = CommandUtil.create(getOpenConnection()).executeSingleQuery("Update Ausleihe Set Bezahlt=1 Where ID_Ausleihe=@idAusleihe",
            new string[] { "@idAusleihe" },
            new object[] { toCloseHire.ID_Ausleihe});
            return closeState ? createResponse(1, "Ausleihe erfolgreich geschlossen", null, true) : createResponse(1, "Ausleihe konnte nicht geschlossen werden", null, false);
        } else
        {
            return createResponse(1, validateResult.validateMessage, null, false);
        }
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

    public ServerResponse removeHire(int idHire)
    {
        Hire foundHire = ServerUtil.getHireFromId(idHire, getOpenConnection());
        if(foundHire==null)
        {
            return createResponse(1, "Keine Vermietung mit ID " + idHire + " gefunden.", null, false);
        }
        bool executionState = CommandUtil.create(getOpenConnection()).executeSingleQuery("Delete From Ausleihe Where ID_Ausleihe=@idAusleihe",
            new string[] { "@idAusleihe" },
            new object[] { idHire });
        return createResponse(1, "Ausleihe gelöscht", null, executionState);
    }

    public ServerResponse updateGame(Spiel updatedSpiel)
    {
        //Todo validate with Future ValidateUtil
        bool executionState = CommandUtil.create(getOpenConnection()).executeSingleQuery("Update Spiel Set Name=@name, Verlag=@verlag, Lagerbestand=@lagerbestand, FK_Tarifkategorie=@fktarifkategorie, FK_Kategorie=@fkkategorie WHERE ID_Spiel=@idSpiel",
            new string[] { "@name", "@verlag", "@lagerbestand", "@fktarifkategorie","@fkkategorie","@idSpiel"},
            new object[] { updatedSpiel.name, updatedSpiel.verlag, updatedSpiel.lagerbestand, updatedSpiel.tarifkategorie, updatedSpiel.kategorie, updatedSpiel.ID_Spiel});
        return createResponse(1, "Spiel geupdated", null, executionState);
    }
}