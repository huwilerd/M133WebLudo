using System;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// Zusammenfassungsbeschreibung für AuthenticationViewlet
/// </summary>
public class AuthenticationViewlet : MasterViewlet, LoginInterface
{

    public ServerResponse logout()
    {
        throw new NotImplementedException();
    }

    public ServerResponse registerUser(string username, string password)
    {
        throw new NotImplementedException();
    }

    public ServerResponse tryLogIn(string username, string password)
    {
        return tryUnconnectedWay(username);

        var connectionString = "Data Source=DESKTOP-QC9NDF8\\SQLEXPRESS; Initial Catalog=Ludothek; Integrated Security=true;";
        System.Data.SqlClient.SqlConnection connection = new System.Data.SqlClient.SqlConnection(connectionString);

        connection.Open();

        SqlTransaction transaction = connection.BeginTransaction("Überweisung");

        SqlCommand cmd = new SqlCommand("SELECT * FROM Person WHERE Name=@username", connection, transaction);
        cmd.Parameters.AddWithValue("@username", username);

        SqlDataReader reader = cmd.ExecuteReader();

        string foundUser = "";
        int count = 0;
        while(reader.Read())
        {
            foundUser = Convert.ToString(reader["Name"]);
            count++;
        }

        connection.Close();

        return createResponse(-1, "Serverseite noch nicht implementiert aber "+foundUser+" gefunden. ("+count+")", null, false);
    }

    private ServerResponse tryUnconnectedWay(String username)
    {
        SqlConnection connection = getConnection();

        //Bindeglied von verbunden zu unverbundenen Objekten
        SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Person", connection);

        DataSet dataSet = new DataSet();
        adapter.Fill(dataSet);

        Console.WriteLine("Es hat " + dataSet.Tables.Count + " Tables gefunden");

        DataTable personTable = dataSet.Tables[0];
        Console.WriteLine("Es hat " + personTable.Rows.Count + " Personen in der Tabelle");

        bool userExists = false;

        for(int i=0; i < personTable.Rows.Count; i ++)
        {
            DataRow row = personTable.Rows[i];
            string name = Convert.ToString(row["Name"]);
            if(name.Equals(username))
            {
                userExists = true;
                break;
            }
        }

        return createResponse(-1, "Unverbundene Objekte", null, userExists);
    }

    public ServerResponse updatePassword(string password)
    {
        throw new NotImplementedException();
    }
}