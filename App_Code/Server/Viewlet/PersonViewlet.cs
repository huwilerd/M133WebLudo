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
        throw new NotImplementedException();
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

    public ServerResponse getOwnOpenHires(Session session)
    {
        List<Dictionary<String, Object>> hireData = CommandUtil.create(getOpenConnection()).executeReader("SELECT * FROM Ausleihe WHERE FK_Person=@fkperson AND Bezahlt=1",
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
        throw new NotImplementedException();
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
        throw new NotImplementedException();
    }
}