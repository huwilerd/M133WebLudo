﻿using System;
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

    public static Person anonymizePerson(Person person)
    {
        person.Name = "Unbekannt";
        person.strasse = "Unbekannt";
        person.ort = "Unbekannt";
        person.postleitzahl = 0;
        person.land = "Unbekannt";
        person.Geburtsdatum = DateTime.Now;
        return person;
    }

    public static ServerResponse deletePerson(Person person, Session session, SqlConnection openConnection)
    {
        if (!isAngestellt(person.ID_Person, openConnection))
        {
            ServerResponse usersHires = ((ClientInterface)ServerViewletProvider.getInstance().GetPersonViewlet()).getOwnHires(session);
            if (usersHires.getResponseStatus())
            {
                List<Hire> usersHireList = (List<Hire>)usersHires.getResponseObject();
                if (usersHireList.Count > 0)
                {
                    // Check if hire is still open, this isn't good
                    for (int i = 0; i < usersHireList.Count; i++)
                    {
                        Hire hire = usersHireList[i];
                        if (!hire.Bezahlt)
                        {
                            return new ServerResponse(1, "Person hat noch offene Ausleihen.", null, false);
                        }
                    }
                    person = anonymizePerson(person);
                    ServerResponse updateResponse = ServerViewletProvider.getInstance().GetPersonViewlet().updatePerson(person);
                    if (!updateResponse.getResponseStatus())
                    {
                        return new ServerResponse(1, "Person konnte nicht aktualisiert werden: " + updateResponse.getResponseMessage(), null, false);
                    }
                    deletePersonLoginInformation(person.ID_Person, openConnection);
                }
                else
                {
                    deletePersonLoginInformation(person.ID_Person, openConnection);
                    deletePerson(person.ID_Person, openConnection);
                }
                return new ServerResponse(1, "Person wurde erfolgreich entfernt", null, true);


            }
        }
        return new ServerResponse(1, "Mitarbeiter können nicht entfernt werden. Diese müssen zuerst entlassen werden.", null, false);
    }

    private static bool isAngestellt(int personId, SqlConnection openConnection)
    {
        return ServerUtil.isFilialleiter(personId, openConnection) || ServerUtil.isEmployee(personId, openConnection);
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
        List<Dictionary<String, Object>> sessionResult = CommandUtil.create(openConnection).executeReaderSP(ServerConst.SELECT_SESSION_PROCEDURE,
            new string[] { "@sessionID" }, new object[] { sessionId });
        if(sessionResult.Count == 0)
        {
            return null;
        }
        Session session = ConvertUtil.getSession(sessionResult[0]);
        session.user = getUserFromId(session.FK_Person, openConnection);
        return session;
    }

    public static Session getSessionFromPerson(int fkPerson, SqlConnection openConnection)
    {
        List<Dictionary<String, Object>> sessionResult = CommandUtil.create(openConnection).executeReader("SELECT * FROM Session WHERE FK_Person=@fkPerson",
            new string[] { "@fkPerson" }, new object[] { fkPerson });
        if (sessionResult.Count == 0)
        {
            return null;
        }
        Session session = ConvertUtil.getSession(sessionResult[0]);
        session.user = getUserFromId(session.FK_Person, openConnection);
        return session;
    }

    public static String getKategorieNameFromId(int kategorieId, SqlConnection openConnection)
    {
        List<Dictionary<String, Object>> kategorieResult = CommandUtil.create(openConnection).executeReader("SELECT * FROM Kategorie WHERE ID_Kategorie=@idKategorie",
            new string[] { "@idKategorie" }, new object[] { kategorieId });
        if (kategorieResult.Count == 0)
        {
            return null;
        }
        return Convert.ToString(kategorieResult[0]["Name"]);
    }

    public static String getTarifKategorieNameFromId(int tarifKategorieId, SqlConnection openConnection)
    {
        List<Dictionary<String, Object>> kategorieResult = CommandUtil.create(openConnection).executeReader("SELECT * FROM TarifKategorie WHERE ID_TarifKategorie=@idKategorie",
            new string[] { "@idKategorie" }, new object[] { tarifKategorieId });
        if (kategorieResult.Count == 0)
        {
            return null;
        }
        return Convert.ToString(kategorieResult[0]["Name"]);
    }

    public static TarifKategorie GetTarifKategorie(int tarifKategorieId, SqlConnection openConnection)
    {
        List<Dictionary<String, Object>> kategorieResult = CommandUtil.create(openConnection).executeReader("SELECT * FROM TarifKategorie WHERE ID_TarifKategorie=@idtk",
            new string[] { "@idtk" }, new object[] { tarifKategorieId });

        if (kategorieResult.Count == 0)
        {
            return null;
        }

        return ConvertUtil.GetTarifKategorie(kategorieResult[0]);
    }

    public static List<TarifKategorie> GetAllTarifKategories(SqlConnection openConnection)
    {
        List<Dictionary<String, Object>> kategorieResult = CommandUtil.create(openConnection).executeReader("SELECT * FROM TarifKategorie",
            null, null);
        List<TarifKategorie> tarifKategories = new List<TarifKategorie>();

        kategorieResult.ForEach(delegate (Dictionary<String, Object> row)
        {
            tarifKategories.Add(ConvertUtil.GetTarifKategorie(row));
        });

        return tarifKategories;
    }

    public static List<Kategorie> GetAllKategories(SqlConnection openConnection)
    {
        List<Dictionary<String, Object>> kategorieResult = CommandUtil.create(openConnection).executeReader("SELECT * FROM Kategorie",
            null, null);
        List<Kategorie> kategories = new List<Kategorie>();

        kategorieResult.ForEach(delegate (Dictionary<String, Object> row)
        {
            kategories.Add(ConvertUtil.GetKategorie(row));
        });

        return kategories;
    }

    public static bool worksForLudothek(int personId, int ludothekId, SqlConnection openConnection)
    {
        if (ServerUtil.isAngestellt(personId, openConnection))
        {
            List<Dictionary<String, Object>> jobsResult = CommandUtil.create(openConnection).executeReader("SELECT * FROM Mitarbeiter WHERE FK_Person=@fkPerson AND FK_Ludothek=@fkLudothek",
                new string[] { "@fkPerson", "@fkLudothek" }, new object[] { personId, ludothekId });
            return jobsResult.Count == 1;
        }
        return false;
    }

    public static bool updateLudothek(Ludothek ludothek, SqlConnection openConnection)
    {
        return CommandUtil.create(openConnection).executeSingleQuery("UPDATE Ludothek Set Name=@name, Strasse=@strasse, PLZ=@plz, Ort=@ort WHERE ID_Ludothek=@idLudothek",
            new string[] { "@name","@strasse", "@plz", "@ort", "@idLudothek" },
            new object[] { ludothek.name, ludothek.strasse, ludothek.postleitzahl, ludothek.ort, ludothek.ID_Ludothek });
    }

    public static Ludothek GetLudothek(int fkLudothek, SqlConnection openConnection)
    {
        List<Dictionary<String, Object>> jobsResult = CommandUtil.create(openConnection).executeReader("SELECT * FROM Ludothek WHERE ID_Ludothek=@idLudothek",
                new string[] { "@idLudothek" }, new object[] { fkLudothek });
        if (jobsResult.Count == 0)
        {
            return null;
        }
        return ConvertUtil.GetLudothek(jobsResult[0]);
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

    public static List<User> getAllUsers(SqlConnection openConnection)
    {
        List<Dictionary<String, Object>> userData = CommandUtil.create(openConnection).executeReader("Select * from Benutzer", null, null);
        List<User> userList = new List<User>();
        userData.ForEach(delegate (Dictionary<String, Object> row)
        {
            userList.Add(ConvertUtil.getUser(row));
        });
        return userList;
    }

    public static User getUserFromMail(String mail, SqlConnection openConnection)
    {
        List<Dictionary<String, Object>> userResult = CommandUtil.create(openConnection).executeReader("SELECT * FROM Benutzer WHERE mail=@mail",
            new string[] { "@mail" }, new object[] { mail });
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

    public static bool logoutUser(int userId, SqlConnection openConnection)
    {
        return CommandUtil.create(openConnection).executeSingleQuery("UPDATE Session Set activeSession=0 WHERE FK_Person=@fkPerson",
            new string[] { "@fkPerson" },
            new object[] { userId });
    }

    public static bool checkIfGameIsUsed(int gameId, SqlConnection openConnection)
    {
        List<Dictionary<String, Object>> hireData = CommandUtil.create(openConnection).executeReader("Select * From Ausleihe Where FK_Spiel=@fkSpiel",
            new string[] { "@fkSpiel" },
            new object[] { gameId });
        return hireData.Count > 0;
    }

    public static bool deleteGame(int gameId, SqlConnection openConnection)
    {
        return CommandUtil.create(openConnection).executeSingleQuery("DELETE FROM Spiel Where ID_Spiel=@idSpiel",
            new string[] { "@idSpiel" },
            new object[] { gameId });
    }

    public static bool deletePerson(int personId, SqlConnection openConnection)
    {
        return CommandUtil.create(openConnection).executeSingleQuery("DELETE FROM Person Where ID_Person=@idPerson",
            new string[] { "@idPerson" },
            new object[] { personId });
    }

    public static bool deletePersonLoginInformation(int personId, SqlConnection openConnection)
    {
        if(CommandUtil.create(openConnection).executeSingleQuery("DELETE FROM Benutzer Where FK_Person=@idPerson",
            new string[] { "@idPerson" },
            new object[] { personId }))
        {
            if (CommandUtil.create(openConnection).executeSingleQuery("DELETE FROM Session Where FK_Person=@idPerson",
            new string[] { "@idPerson" },
            new object[] { personId }))
            {
                return true;
            }
        }
        return false;
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
