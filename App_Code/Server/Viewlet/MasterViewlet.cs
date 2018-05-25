using System;
using System.Data.SqlClient;


/// <summary>
/// Zusammenfassungsbeschreibung für AppConst
/// </summary>
public abstract class MasterViewlet
{

    private SqlConnection connection;
    
    protected SqlConnection getConnection()
    {
        if (connection == null)
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = "DESKTOP-QC9NDF8\\SQLEXPRESS";
            builder.InitialCatalog = "Ludothek";
            builder.IntegratedSecurity = true;
            string connectionString = builder.ConnectionString;
            connection = new SqlConnection(connectionString);
        }
       
        return connection;
    }

    protected SqlConnection getOpenConnection()
    {
        getConnection();
        if (connection.State != System.Data.ConnectionState.Open)
        {
            connection.Open();
        }
        return connection;
    }

    protected ServerResponse createResponse(int id, string message, object respObject, bool status)
    {
        return new ServerResponse(id, message, respObject, status);
    }


}