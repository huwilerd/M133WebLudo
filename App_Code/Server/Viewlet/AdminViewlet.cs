using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Zusammenfassungsbeschreibung für AdminViewlet
/// </summary>
public class AdminViewlet : MasterViewlet, AdminInterface
{

    public ServerResponse makeEmployee(int fkPerson)
    {
        if (!ServerUtil.isEmployee(fkPerson, getOpenConnection()))
        {
            if (ServerUtil.addEmployee(fkPerson, InstallViewlet.ludothek.ID_Ludothek, getOpenConnection()))
            {
                return createResponse(1, "Kunde erfolgreich zu Mitarbeiter gemacht", null, true);
            }
            return createResponse(1, "Kunde konnte nicht zu Mitarbeiter gemacht werden", null, false);
        }
        return createResponse(1, "Kunde ist bereits Mitarbeiter", null, false);
    }

    public ServerResponse getAllEmployees()
    {
        List<Dictionary<String, Object>> employeeData = CommandUtil.create(getOpenConnection()).executeReader("SELECT * FROM Mitarbeiter",
                null, null);
        List<int> employeeIds = new List<int>();
        employeeData.ForEach(delegate (Dictionary<String, Object> row)
        {
            employeeIds.Add(Convert.ToInt32(row["FK_Person"]));
        });

        List<Person> employeeList = new List<Person>();
        employeeIds.ForEach(delegate (int fkPerson)
        {
            employeeList.Add(ServerUtil.getPersonFromId(fkPerson, getOpenConnection()));
        });

        return createResponse(1, "Mitarbeiterliste", employeeList, true);
    }

    public ServerResponse getStellvertretung(int idFilialleiter)
    {
        List<Dictionary<String, Object>> stellvertretungData = CommandUtil.create(getOpenConnection()).executeReader("SELECT * FROM Filialleiter WHERE FK_Mitarbeiter=@idFilialleiter",
            new string[] { "@idFilialleiter" }, new object[] { idFilialleiter});
        if(stellvertretungData.Count == 1)
        {
            int idStellvertretung = Convert.ToInt32(stellvertretungData[0]["FK_Stellvertretung"]);
            Person person = ServerUtil.getPersonFromId(idStellvertretung, getOpenConnection());
            if(person!=null)
            {
                return createResponse(1, "Stellvertretung gefunden", person, true);
            }
            return createResponse(1, "Keine Stellvertretung gefunden", null, false);
        }
        return createResponse(1, "Keine Stellvertretung gefunden", null, false);
    }

    public ServerResponse updateStellvertretung(int idFilialleiterPerson, int newStellvertretungId)
    {
        bool executionState = CommandUtil.create(getOpenConnection()).executeSingleQuery("UPDATE Filialleiter Set FK_Stellvertretung=@idStellvertretung WHERE FK_Mitarbeiter=@idFilialleiter",
            new string[] { "@idStellvertretung", "@idFilialleiter" }, new object[] { newStellvertretungId, idFilialleiterPerson });
        return executionState ? createResponse(1, "Stellvertretung aktualisiert", null, true) : createResponse(1, "Stellvertretung konnte nicht aktualisiert werden", null, false);
    }

    public ServerResponse removeEmployee(Session session, int fkPerson)
    {
        bool isFilialleiter = ServerUtil.isFilialleiter(fkPerson, getOpenConnection());
        if ( (isFilialleiter && session.FK_Person == fkPerson) || !isFilialleiter)
        {
            if(isFilialleiter)
            {
                bool executionStateFilialleiter = CommandUtil.create(getOpenConnection()).executeSingleQuery("DELETE FROM Filialleiter WHERE FK_Mitarbeiter=@fkPerson",
                    new string[] { "@fkPerson"},
                    new object[] { fkPerson});
                if(!executionStateFilialleiter)
                {
                    return createResponse(1, "Filialleiter konnte nicht gelöscht werden", null, false);
                }
            }

            bool executionState = CommandUtil.create(getOpenConnection()).executeSingleQuery("DELETE FROM Mitarbeiter WHERE FK_PERSON=@fkPerson",
                new string[] { "@fkPerson" },
                new object[] { fkPerson });
            return executionState ? createResponse(1, "Mitarbeiter erfolgreich entlassen", null, true) : createResponse(1, "Mitarbeiter konnte nicht gelöscht werden", null, false);
        }
        return createResponse(1, "Filialleiter können sich nur selber kündigen", null, false);
    }

    public ServerResponse getAllLudotheken()
    {
        List<Dictionary<String, Object>> ludothekenList = CommandUtil.create(getOpenConnection()).executeReader("SELECT * FROM Ludothek", null, null);
        List<Ludothek> ludothekList = new List<Ludothek>();

        ludothekenList.ForEach(delegate (Dictionary<String, Object> row)
        {
            ludothekList.Add(ConvertUtil.GetLudothek(row));
        });

        return createResponse(1, "Alle Ludotheken", ludothekList, true);
    }

    public ServerResponse updateLudothek(Session session, Ludothek ludothek)
    {
        if(ServerUtil.worksForLudothek(session.FK_Person, ludothek.ID_Ludothek, getOpenConnection()))
        {
            if(ServerUtil.updateLudothek(ludothek, getOpenConnection()))
            {
                return createResponse(1, "Ludothek wurde erfolgreich aktualisiert", null, true);
            }
            return createResponse(1, "Ludothek konnte nicht aktualisiert werden", null, false);
        }
        return createResponse(1, "Um eine Ludothek zu bearbeiten muss man Leiter deren sein", null, false);
    }

    public ServerResponse getSingleLudothek(int fkLudothek)
    {
        Ludothek ludothek = ServerUtil.GetLudothek(fkLudothek, getOpenConnection());
        if(ludothek!= null)
        {
            return createResponse(1, "Ludothek erfolgreich gefunden", ludothek, true);
        }
        return createResponse(1, "Ludothek konnte nicht gefunden werden", null, false);
    }
}