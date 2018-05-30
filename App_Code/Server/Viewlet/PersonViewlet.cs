using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Zusammenfassungsbeschreibung für PersonViewlet
/// </summary>
public class PersonViewlet : MasterViewlet, PersonFunctionInterface, ClientInterface
{
    public ServerResponse createHire(Session session, Hire hire)
    {
        ValidateResult validateResult = ValidateUtil.getInstance().validateNewHire(hire);
        if (validateResult.validateStatus)
        {
            hire.ID_Ausleihe = ServerUtil.generateNewIdForTable("Ausleihe", "ID_Ausleihe", getOpenConnection());
            if (ServerUtil.addHire(hire, getOpenConnection()))
            {
                return createResponse(1, "Ausleihe erfolgreich erstellt", null, true);
            }
            return createResponse(1, "Ausleihe konnte nicht erstellt werden", null, false);
        } else
        {
            return createResponse(1, validateResult.validateMessage, null, false);
        }
    }

    public ServerResponse getAllGames()
    {
        return createResponse(1, "Gesamte Spielliste", ServerUtil.getAllGames(getOpenConnection()), true);
    }

    public ServerResponse getOwnHires(Session session)
    {
        List<Dictionary<String, Object>> hireData = CommandUtil.create(getOpenConnection()).executeReader("SELECT * FROM Ausleihe WHERE FK_Person=@fkperson",
                new string[] { "@fkperson"}, new object[] { session.FK_Person});
        
        List<Hire> ownHireList = new List<Hire>();
        hireData.ForEach(delegate (Dictionary<String, Object> row)
        {
            ownHireList.Add(ConvertUtil.getHire(row));
        });
        return createResponse(1, "Eigene Ausleihen", ownHireList, true);
    }

    public ServerResponse getOwnClosedHires(Session session)
    {
        List<Dictionary<String, Object>> hireData = CommandUtil.create(getOpenConnection()).executeReader("SELECT * FROM Ausleihe WHERE FK_Person=@fkperson AND Bezahlt=1",
                new string[] { "@fkperson" }, new object[] { session.FK_Person });

        List<Hire> ownHireList = new List<Hire>();
        hireData.ForEach(delegate (Dictionary<String, Object> row)
        {
            ownHireList.Add(ConvertUtil.getHire(row));
        });
        return createResponse(1, "Eigene abgeschlossene Ausleihen", ownHireList, true);
    }

    public ServerResponse getOwnOpenHires(Session session)
    {
        List<Dictionary<String, Object>> hireData = CommandUtil.create(getOpenConnection()).executeReader("SELECT * FROM Ausleihe WHERE FK_Person=@fkperson AND Bezahlt=0",
                new string[] { "@fkperson" }, new object[] { session.FK_Person });

        List<Hire> ownHireList = new List<Hire>();
        hireData.ForEach(delegate (Dictionary<String, Object> row)
        {
            ownHireList.Add(ConvertUtil.getHire(row));
        });
        return createResponse(1, "Eigene bevorstehende Ausleihen", ownHireList, true);
    }

    public ServerResponse updateHire(Hire hire)
    {
        ValidateResult result = ValidateUtil.getInstance().validateBeforeExtendingHire(hire);
        if (result.validateStatus)
        {
            bool executeState = CommandUtil.create(getOpenConnection()).executeSingleQuery("UPDATE Ausleihe SET BisDatum=@bisDatum WHERE ID_Ausleihe=@idAusleihe",
                new string[] { "@bisDatum", "@idAusleihe" },
                new object[] { hire.BisDatum, hire.ID_Ausleihe });
            return executeState ? createResponse(1, "Ausleihe wurde geupdated", null, true) : createResponse(1, "Ausleihe konnte nicht geupdated werden", null, false);
        }
        return createResponse(1, result.validateMessage, null, result.validateStatus);
    }

    public ServerResponse updatePerson(Person person)
    {
        bool executeState = CommandUtil.create(getOpenConnection()).executeSingleQuery("UPDATE Person SET Name=@Name, Geschlecht=@Geschlecht, Geburtsdatum=@Geburtsdatum, FK_Mitgliedschaft=@fkmitgliedschaft, Strasse=@Strasse, Postleitzahl=@Postleitzahl, Ort=@Ort, Land=@Land WHERE ID_Person=@idPerson",
            new string[] { "@Name","@Geschlecht", "@Geburtsdatum", "@fkmitgliedschaft", "@Strasse", "@Postleitzahl", "@Ort", "@Land", "@idPerson"},
            new object[] { person.Name, person.Geschlecht, person.Geburtsdatum, person.getMitgliedschaftsId(), person.strasse, person.postleitzahl, person.ort, person.land, person.ID_Person });
        return executeState ? createResponse(1, "Person wurde geupdated", null, true) : createResponse(1, "Person konnte nicht geupdated werden", null, false);
    }

    public ServerResponse updateUser(User user)
    {
        bool executeState = CommandUtil.create(getOpenConnection()).executeSingleQuery("UPDATE Benutzer SET mail=@mail, password=@password WHERE FK_Person=@idPerson",
           new string[] { "@idPerson", "@mail", "@password" },
           new object[] { user.userId, user.email, user.password });
        return executeState ? createResponse(1, "Benutzer wurde geupdated", null, true) : createResponse(1, "Person konnte nicht geupdated werden", null, false);
    }

    public ServerResponse getSingleGame(int gameId)
    {
        Spiel spiel = ServerUtil.getGameForId(gameId, getOpenConnection());
        if(spiel != null)
        {
            return createResponse(1, "Spiel gefunden", spiel, true);
        }
        return createResponse(1, "Spiel nicht gefunden", null, false);
    }

    public ServerResponse getSingleHire(int hireId, Session session)
    {
        Hire hire = ServerUtil.getHireFromId(hireId, getOpenConnection());
        ValidateResult validateResult = ValidateUtil.getInstance().validateGettingHire(hire, session);
        if (!validateResult.validateStatus)
        {
            return createResponse(1, validateResult.validateMessage, null, false);   
        }

        if(hire==null)
        {
            return createResponse(1, "Keine Ausleihe gefunden", null, false);
        }

        return createResponse(1, "Ausleihe gefunden", hire, true);
    }

    public ServerResponse deleteAccount(Session session)
    {
        Person person = ServerUtil.getPersonFromId(session.FK_Person, getOpenConnection());
        if(person==null)
        {
            return createResponse(1, "Keine Person gefunden", null, false);
        }

        return ServerUtil.deletePerson(person, session, getOpenConnection());
    }

    public ServerResponse updateUserPassword(User user)
    {
        User userFromMail = ServerUtil.getUserFromMail(user.email, getOpenConnection());
        if(userFromMail!= null)
        {
            userFromMail.password = ServerUtil.hashPassword(user.password);
            return updateUser(userFromMail);
        }
        return createResponse(1, "Es wurde kein Benutzer mit der angegebenen E-Mail gefunden", null, false);
    }
}