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
}