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
            builder.DataSource = ".";
            builder.InitialCatalog = "Ludothek";
            builder.IntegratedSecurity = true;
            string connectionString = builder.ConnectionString;
            SqlConnection connection = new SqlConnection(connectionString);
        }
        return connection;
    }


}