using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

class DataConnect
{
    public DirectorysFiles GetData()
    {
        LogError log = new LogError();
        DirectorysFiles directoy = new DirectorysFiles();
        SqlConnection conn = GetDBConnection();
        try
        {         
            Console.WriteLine("Connection Data Open");
            conn.Open();
            SqlCommand command = new SqlCommand("SELECT * FROM sys_param_hold" + GetFilter("IN"), conn);
            SqlDataReader dataReader = command.ExecuteReader();

            while (dataReader.Read())
            {
                directoy.InDirectory = (string)dataReader["param_value"];
            }
            dataReader.Close();
            command.Dispose();

            command = new SqlCommand("SELECT * FROM sys_param_hold" + GetFilter("Log"), conn);
            dataReader = command.ExecuteReader();

            while (dataReader.Read())
            {
                directoy.LogDirectory = (string)dataReader["param_value"];
            }
            dataReader.Close();
            command.Dispose();

            command = new SqlCommand("SELECT * FROM sys_param_hold" + GetFilter("Move"), conn);
            dataReader = command.ExecuteReader();

            while (dataReader.Read())
            {
                directoy.MoveDirectory = (string)dataReader["param_value"];
            }
            dataReader.Close();
            command.Dispose();

            conn.Close();
            Console.WriteLine("Connection Data Closed");
        }
        catch (Exception ex)
        {
            if (conn.State == ConnectionState.Open) {
                conn.Close();
            }
            log.Generar(this, ex);
            Console.WriteLine("Error SQL Function: " + ex.Message);
        }
        return directoy;
    }

    private SqlConnection GetDBConnection()
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["BDConnect"].ConnectionString);
        return conn;
    }

    private string GetFilter(string name) {
        string filter = ConfigurationManager.AppSettings.Get(name);
        return " where " + String.Join(" and ", filter.Split(new string[] { "#" }, StringSplitOptions.RemoveEmptyEntries));
    }
}

