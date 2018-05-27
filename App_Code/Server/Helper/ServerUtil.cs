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

    public static bool addHire(Hire hire, SqlConnection openConnection)
    {
        bool executionState = CommandUtil.create(openConnection).executeSingleQuery("INSERT INTO Ausleihe VALUES(@id, @fkPerson, @fkSpiel, @vonDate, @bisDate, @bezahlt)",
            new string[] { "@id", "@fkPerson", "@fkSpiel", "@vonDate", "@bisDate", "@bezahlt" },
            new object[] { hire.ID_Ausleihe, hire.FK_Person, hire.FK_Spiel, hire.VonDatum, hire.BisDatum, hire.Bezahlt });
        return executionState;
    }

    public static Spiel getGameForId(int gameId, SqlConnection openConnection)
    {
        List<Dictionary<String, Object>> gameData = CommandUtil.create(openConnection).executeReader("Select * From Spiel WHERE ID_Spiel=@idSpiel", new string[] { "@idSpiel" }, new object[] { gameId });
        if(gameData.Count == 1)
        {
            return ConvertUtil.getSpiel(gameData[0]);
        }
        return null;
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

    public static Hire getHireFromId(int hireId, SqlConnection openConnection)
    {
        List<Dictionary<String, Object>> hireResult = CommandUtil.create(openConnection).executeReader("SELECT * FROM Ausleihe WHERE ID_Ausleihe=@idAusleihe",
            new string[] { "@idAusleihe" }, new object[] { hireId });
        if (hireResult.Count == 0)
        {
            return null;
        }
        return ConvertUtil.getHire(hireResult[0]);
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

    public static List<Spiel> getAllGames(SqlConnection openConnection)
    {
        List<Dictionary<String, Object>> gameData = CommandUtil.create(openConnection).executeReader("SELECT * FROM Spiel Order By ID_Spiel DESC", null, null);
        List<Spiel> spielList = new List<Spiel>();
        gameData.ForEach(delegate (Dictionary<String, Object> row)
        {
            spielList.Add(ConvertUtil.getSpiel(row));
        });
        return spielList;
    }

    public static bool isFilialleiter(int fkPerson, SqlConnection openConnection)
    {
        List<Dictionary<String, Object>> filialleiterData = CommandUtil.create(openConnection).executeReader("SELECT * FROM Filialleiter WHERE FK_Mitarbeiter=@fkPerson",
            new string[] { "@fkPerson" }, new object[] { fkPerson});
        return filialleiterData.Count == 1;
    }

    public static bool addLudothek(Ludothek ludothek, SqlConnection openConnection)
    {
        bool executionState = CommandUtil.create(openConnection).executeSingleQuery("INSERT INTO Ludothek VALUES(@idLudothek, @name, @strasse, @plz, @ort, @fkVerband)",
            new string[] { "@idLudothek", "@name", "@strasse", "@plz", "@ort", "@fkVerband" },
            new object[] { ludothek.ID_Ludothek, ludothek.name, ludothek.strasse, ludothek.postleitzahl, ludothek.ort, ludothek.verband });

        return executionState;
    }

    public static bool addEmployee(int fkPerson, int fkLudothek, SqlConnection openConnection)
    {
        bool executionState = CommandUtil.create(openConnection).executeSingleQuery("INSERT INTO Mitarbeiter VALUES(@idPerson, @idLudothek)",
            new string[] { "@idPerson", "@idLudothek" },
            new object[] { fkPerson, fkLudothek });

        return executionState;
    }

    public static bool addVerband(Verband verband, SqlConnection openConnection)
    {
        bool executionState = CommandUtil.create(openConnection).executeSingleQuery("INSERT INTO Verband VALUES(@idVerband, @verbandName)",
            new string[] { "@idVerband", "@verbandName" },
            new object[] { verband.ID_Verband, verband.Name });

        return executionState;
    }

    public static bool addFilialleiter(int fkPerson, SqlConnection openConnection)
    {
        int generatedId = generateNewIdForTable("Filialleiter", "ID_Filialleiter", openConnection);
        bool executionState = CommandUtil.create(openConnection).executeSingleQuery("INSERT INTO Filialleiter VALUES(@idFilialleiter, @idPerson, @idStellvertretung)",
            new string[] { "@idFilialleiter", "@idPerson", "@idStellvertretung" },
            new object[] { generatedId, fkPerson, fkPerson});

        return executionState;
    }

    public static bool isEmployee(int fkPerson, SqlConnection openConnection)
    {
        List<Dictionary<String, Object>> mitarbeiterData = CommandUtil.create(openConnection).executeReader("SELECT * FROM Mitarbeiter WHERE FK_Person=@fkPerson",
            new string[] { "@fkPerson" }, new object[] { fkPerson });
        return mitarbeiterData.Count == 1;
    }

    public static bool addKategorie(Kategorie kategorie, SqlConnection openConnection)
    {
        bool executionState = CommandUtil.create(openConnection).executeSingleQuery("INSERT INTO Kategorie VALUES(@idKategorie, @name, @altersfreigabe)",
            new string[] { "@idKategorie", "@name", "@altersfreigabe" },
            new object[] { kategorie.ID_Kategorie, kategorie.Name, kategorie.Altersfreigabe });

        return executionState;
    }

    public static bool addTarifKategorie(TarifKategorie tarifKategorie, SqlConnection openConnection)
    {
        bool executionState = CommandUtil.create(openConnection).executeSingleQuery("INSERT INTO TarifKategorie VALUES(@idKategorie, @name, @normalPreis, @mitgliedschaftsauflage)",
            new string[] { "@idKategorie", "@name", "@normalPreis", "@mitgliedschaftsauflage" },
            new object[] { tarifKategorie.ID_TarifKategorie, tarifKategorie.Name, tarifKategorie.NormalPreis, tarifKategorie.MitgliedschaftsAuflage });

        return executionState;
    }

    public static bool addMitgliedschaft(Mitgliedschaft mitgliedschaft, SqlConnection openConnection)
    {
        bool executionState = CommandUtil.create(openConnection).executeSingleQuery("INSERT INTO Mitgliedschaft VALUES(@idMitgliedschaft, @status, @rechnungsstatus, @erstellungsdatum, @auslaufdatum)",
            new string[] { "@idMitgliedschaft", "@status", "@rechnungsstatus", "@erstellungsdatum", "@auslaufdatum" },
            new object[] { mitgliedschaft.ID_Mitgliedschaft, mitgliedschaft.Status, mitgliedschaft.Rechnungsstatus, mitgliedschaft.Erstellungsdatum, mitgliedschaft.AuslaufDatum});

        return executionState;
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
