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
        bool addState = CommandUtil.create(getOpenConnection()).executeSingleQuery("INSERT INTO Spiel VALUES(@gameId, @name, @verlag, @lagerbestand, @fktarifkategorie, @fkkategorie, @imageLink, @description)",
            new string[] {"@gameId", "@name", "@verlag", "@lagerbestand", "@fktarifkategorie", "@fkkategorie", "@imageLink", "@description" },
            new object[] { newSpiel.ID_Spiel, newSpiel.name, newSpiel.verlag, newSpiel.lagerbestand, newSpiel.tarifkategorie, newSpiel.kategorie, newSpiel.imageLink, newSpiel.description});
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

    public ServerResponse deleteUser(int userId)
    {
        Person person = ServerUtil.getPersonFromId(userId, getOpenConnection());
        if(person == null)
        {
            return createResponse(1, "Benutzer ist nicht vorhanden", null, false);
        }

        Session sessionOfPerson = ServerUtil.getSessionFromPerson(person.ID_Person, getOpenConnection());
        if(sessionOfPerson==null)
        {
            return createResponse(1, "Benutzer besitzt keine Session", null, false);
        }


        if(ServerUtil.deletePerson(person, sessionOfPerson, getOpenConnection()))
        {
            return createResponse(1, "Person wurde gelöscht", null, true);
        }

        return createResponse(1, "Person konnte nicht gelöscht werden", null, false);
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

    public ServerResponse getAllUsers()
    {
        List<User> userList = ServerUtil.getAllUsers(getOpenConnection());
        return createResponse(1, "Alle Nutzer", userList, true);
    }

    public ServerResponse logoutUser(int userId)
    {
        if(ServerUtil.isFilialleiter(userId, getOpenConnection()))
        {
            return createResponse(1, "Filialleiter können nicht von Mitarbeitern ausgeloggt werden.", null, false);
        }

        Session session = ServerUtil.getSessionFromPerson(userId, getOpenConnection());
        if(session!=null)
        {
            bool logoutSuceeded = ServerUtil.logoutUser(userId, getOpenConnection());
            return logoutSuceeded ? createResponse(1, "User erfolgreich abgemeldet", null, true) : createResponse(1, "User konnte nicht abgemeldet werden", null, false);
        }

        return createResponse(1, "Session existiert nicht", null, false);
    }

    public ServerResponse removeGame(int idSpiel)
    {
        bool isGameInUse = ServerUtil.checkIfGameIsUsed(idSpiel, getOpenConnection());
        if (isGameInUse)
        {
            return createResponse(1, "Spiel kann nicht gelöscht werden, da es in Verwendung ist.", null, false);
        }
        bool gameDeletedState = ServerUtil.deleteGame(idSpiel, getOpenConnection());
        return createResponse(1, "Spiel Löschstatus: " + gameDeletedState, null, gameDeletedState);
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

    public ServerResponse reopenHire(int idHire)
    {
        Hire toOpenHire = ServerUtil.getHireFromId(idHire, getOpenConnection());
        ValidateResult validateResult = ValidateUtil.getInstance().validateBeforeOpeningHire(toOpenHire);
        if (validateResult.validateStatus)
        {
            bool closeState = CommandUtil.create(getOpenConnection()).executeSingleQuery("Update Ausleihe Set Bezahlt=0 Where ID_Ausleihe=@idAusleihe",
            new string[] { "@idAusleihe" },
            new object[] { toOpenHire.ID_Ausleihe });
            return closeState ? createResponse(1, "Ausleihe erfolgreich wiedereröffnet", null, true) : createResponse(1, "Ausleihe konnte nicht wiedereröffnet werden", null, false);
        }
        else
        {
            return createResponse(1, validateResult.validateMessage, null, false);
        }
    }

    public ServerResponse updateGame(Spiel updatedSpiel)
    {
        //Todo validate with Future ValidateUtil
        bool executionState = CommandUtil.create(getOpenConnection()).executeSingleQuery("Update Spiel Set Name=@name, Verlag=@verlag, Lagerbestand=@lagerbestand, FK_Tarifkategorie=@fktarifkategorie, FK_Kategorie=@fkkategorie, Image_Link=@imageLink, Description=@description WHERE ID_Spiel=@idSpiel",
            new string[] { "@name", "@verlag", "@lagerbestand", "@fktarifkategorie","@fkkategorie","@idSpiel", "@imageLink", "@description"},
            new object[] { updatedSpiel.name, updatedSpiel.verlag, updatedSpiel.lagerbestand, updatedSpiel.tarifkategorie, updatedSpiel.kategorie, updatedSpiel.ID_Spiel, updatedSpiel.imageLink, updatedSpiel.description});
        return createResponse(1, "Spiel geupdated", null, executionState);
    }
}