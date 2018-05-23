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

    public ServerResponse registerUser(string mail, string password)
    {
        SqlConnection connection = getConnection();
        connection.Open();

        password = ServerUtil.hashPassword(password);
        SqlCommand cmd = checkUser(mail, password);

        SqlDataReader reader = cmd.ExecuteReader();

        if (reader.Read())
        {
            //Login
            return createResponse(1, "User bereits vorhanden", null, true);
        }
        else
        {
            //Register
            reader.Close();


            SqlCommand cme = new SqlCommand("INSERT INTO Benutzer(FK_Person, password, mail) VALUES (@id , @mail, @password)", connection);
            cme.Parameters.AddWithValue("@mail", mail);
            cme.Parameters.AddWithValue("@password", password);
            cme.Parameters.AddWithValue("@id", createPerson());

            int affectedRows = cme.ExecuteNonQuery();

            if (affectedRows == 1)
            {
                return createResponse(1, "User wurde erstellt", null, true);
            }
            else
            {
                return createResponse(1, "User wurde nicht erstellt", null, true);
            }

        }
    }

    private int createPerson()
    {
        SqlConnection connection = getConnection();
        if(connection.State != ConnectionState.Open)
        {
            connection.Open();
        }

        SqlCommand cmd = new SqlCommand("INSERT INTO Person(ID_Person, Name, Geschlecht, Geburtsdatum, Einstiegsdatum, FK_Mitgliedschaft) VALUES (1, '', 'Männlich', '2099-12-20', '2098-12-20', null)", connection);
        int affectedRows = cmd.ExecuteNonQuery();

        if (affectedRows == 1)
        {
            return 1;
        }
        else
        {
            return -1;
        }
    }


    public ServerResponse tryLogIn(string mail, string password)
    {
        SqlConnection connection = getConnection();
        if (connection.State != ConnectionState.Open)
        {
            connection.Open();
        }

        password = ServerUtil.hashPassword(password);

        SqlCommand cmd = checkUser(mail, password);

        SqlDataReader reader = cmd.ExecuteReader();

        string foundUser = "";

        if (reader.Read())
        {
            foundUser = "lauft";
        }
        else
        {
            foundUser = "lauft ned";
        }

        connection.Close();

        return createResponse(1, "test", null, true);
    }


    public SqlCommand checkUser(string mail, string password)
    {
        SqlCommand cmd = new SqlCommand("SELECT * FROM Benutzer WHERE mail=@mail AND password=@password", getConnection());
        cmd.Parameters.AddWithValue("@mail", mail);
        cmd.Parameters.AddWithValue("@password", password);

        return cmd;
    }

    public ServerResponse updatePassword(string password)
    {
        throw new NotImplementedException();
    }
}
