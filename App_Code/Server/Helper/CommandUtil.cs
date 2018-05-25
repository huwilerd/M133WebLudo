using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Zusammenfassungsbeschreibung für CommandUtil
/// </summary>
public class CommandUtil
{

    public static CommandUtil create(SqlConnection connection)
    {
        return new CommandUtil(connection);
    }

    private SqlConnection connection;

    private CommandUtil(SqlConnection connection)
    {
        this.connection = connection;
    }

    public List<Dictionary<String, Object>> executeReaderSP(String spName, String[] paramNames, Object[] paramValues)
    {
        handleBeforeAction();

        SqlCommand command = new SqlCommand(spName, connection);
        command.CommandType = CommandType.StoredProcedure;
        setParametersToSqlCommand(command, paramNames, paramValues);
        SqlDataReader reader = command.ExecuteReader();

        List<String> columns = getColumnsFromReader(reader);

        List<Dictionary<String, Object>> dataList = new List<Dictionary<String, Object>>();

        while (reader.Read())
        {
            dataList.Add(fillDictionaryWithRow(reader, columns));
        }

        reader.Close();

        handleAfterAction();

        return dataList;
    }

    public List<Dictionary<String, Object>> executeReader(String query, String[] paramNames, Object[] paramValues)
    {
        handleBeforeAction();

        SqlCommand command = new SqlCommand(query, connection);
        setParametersToSqlCommand(command, paramNames, paramValues);
        SqlDataReader reader = command.ExecuteReader();

        List<String> columns = getColumnsFromReader(reader);

        List<Dictionary<String, Object>> dataList = new List<Dictionary<String, Object>>();

       while (reader.Read())
        {
            dataList.Add(fillDictionaryWithRow(reader, columns));
        }

        reader.Close();

        handleAfterAction();

        return dataList;
    }

    private Dictionary<String, Object> fillDictionaryWithRow(SqlDataReader reader, List<String> columns) 
    {
        Dictionary<String, Object> row = new Dictionary<String, Object>();
        columns.ForEach(delegate (String colname)
        {
            row.Add(colname, reader[colname]);
        }); 
        return row;
    }

    private List<String> getColumnsFromReader(SqlDataReader reader)
    {
        return Enumerable.Range(0, reader.FieldCount).Select(reader.GetName).ToList();
    }

    public bool executeSingleQuery(String query, String[] paramNames, Object[] paramValues)
    {
        handleBeforeAction();

        SqlCommand command = new SqlCommand(query, connection);
        setParametersToSqlCommand(command, paramNames, paramValues);
        int affectedRows = command.ExecuteNonQuery();
        handleAfterAction();

        return affectedRows == 1; //Single Datarow is affected
    }

    public bool executeSingleSPQuery(String spName, String[] paramNames, Object[] paramValues)
    {
        handleBeforeAction();

        SqlCommand command = new SqlCommand(spName, connection);
        command.CommandType = CommandType.StoredProcedure;
        setParametersToSqlCommand(command, paramNames, paramValues);
        int affectedRows = command.ExecuteNonQuery();
        handleAfterAction();

        return affectedRows == 1; //Single Datarow is affected
    }

    private void setParametersToSqlCommand(SqlCommand command, String[] columns, Object[] values)
    {
        if(columns == null && values == null)
        {
            return;
        }
        if(columns.Length != values.Length)
        {
            throw new Exception("Column and Valuelist must have the same amount of items");
        }
        for(int i = 0; i < columns.Length; i++)
        {
            command.Parameters.AddWithValue(columns[i], values[i]);
        }
    }

    private void handleBeforeAction()
    {
        if (connection.State == ConnectionState.Closed)
        {
            connection.Open();
        }
    }

    private void handleAfterAction()
    {
        if (connection.State == ConnectionState.Open)
        {
            connection.Close();
        }
    }

}