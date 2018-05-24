using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

/// <summary>
/// Zusammenfassungsbeschreibung für ServerUtil
/// </summary>
public class ServerUtil
{

    public static String hashPassword(String password)
    {
        MD5 md5 = MD5.Create();
        byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(password);
        byte[] hash = md5.ComputeHash(inputBytes);

        StringBuilder sb = new StringBuilder();

        for (int i = 0; i < hash.Length; i++)
        {
            sb.Append(hash[i].ToString("X2"));
        }

        return sb.ToString();
    }

    public static int createEmptyPerson(SqlConnection openConnection)
    {
        int generatedPersonId = generateNewIdForTable("Person", "ID_Person", openConnection);

        bool executionState = CommandUtil.create(openConnection).executeSingleQuery(ServerConst.INSERT_PERSON_QUERY,
            new string[] { "@id_person", "@name", "@geschlecht", "@geburtsdatum", "@einstiegsdatum", "@mitgliedschaft" },
            new object[] { generatedPersonId, "", User.MAENNLICH_KEY, "1900-02-10", DateTime.Now, DBNull.Value });

        return executionState ? generatedPersonId : -1;
    }

    public static Session getSessionFromId(String sessionId, SqlConnection openConnection)
    {
        List<Dictionary<String, Object>> sessionResult = CommandUtil.create(openConnection).executeReader("SELECT * FROM Session WHERE sessionID=@sessionID",
            new string[] { "@sessionID" }, new object[] { sessionId });
        if(sessionResult.Count == 0)
        {
            return null;
        }
        Session session = ConvertUtil.getSession(sessionResult[0]);
        session.user = getUserFromId(session.FK_Person, openConnection);
        return session;
    }

    public static Person getPersonFromId(int personID, SqlConnection openConnection)
    {
        List<Dictionary<String, Object>> personResult = CommandUtil.create(openConnection).executeReader("SELECT * FROM Person WHERE ID_Person=@idPerson",
            new string[] { "@idPerson" }, new object[] { personID });
        if (personResult.Count == 0)
        {
            return null;
        }
        return ConvertUtil.getPerson(personResult[0]);
    }

    public static User getUserFromId(int personID, SqlConnection openConnection)
    {
        List<Dictionary<String, Object>> userResult = CommandUtil.create(openConnection).executeReader("SELECT * FROM Benutzer WHERE FK_Person=@idPerson",
            new string[] { "@idPerson" }, new object[] { personID });
        if (userResult.Count == 0)
        {
            return null;
        }
        return ConvertUtil.getUser(userResult[0]);
    }

    public static bool doesUserExist(String mail, SqlConnection openConnection)
    {
        List<Dictionary<String, object>> readResult = CommandUtil.create(openConnection).executeReader(ServerConst.SELECT_BENUTZER_ByName,
            new string[] { "@mail" }, new object[] { mail });
        return readResult.Count >= 1;
    }

    public static int generateNewIdForTable(String tableName, String idColName, SqlConnection openConnection)
    {
        List<Dictionary<String, object>> tableData = CommandUtil.create(openConnection).executeReader("SELECT " + idColName + " FROM " + tableName, null, null);
        int generatedId = tableData.Count + 1;
        while(isIdInTable(tableData, idColName, generatedId)) {
            generatedId++;
        }
        return generatedId;
    }

    private static bool isIdInTable(List<Dictionary<String, object>> tableData, String idColName, int id)
    {
        for(int i = 0; i < tableData.Count; i++)
        {
            int currentId = Convert.ToInt32(tableData[i][idColName]);
            if (currentId == id)
            {
                return true;
            }
        }
       
        return false;
    }


}
