using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;
using System.Xml;
using System.Xml.Linq;

/// <summary>
/// Summary description for CatholicDB
/// </summary>
public class CatholicDB
{
    public static string sqlResult;
    static string PrefixToFilter = ConfigurationManager.AppSettings.Get("PrefixToFilter");

    static string connectionString = string.Format
            ("Server={0};Database={1};User Id={2};password={3}",
            System.Configuration.ConfigurationManager.AppSettings["AutomationDB_Server"],
             System.Configuration.ConfigurationManager.AppSettings["AutomationDB_Name"],
             System.Configuration.ConfigurationManager.AppSettings["Automation_dbUser"],
             System.Configuration.ConfigurationManager.AppSettings["Automation_dbPwd"]);

    #region Queries Execution

    public static DataSet ExecuteQuery(string query, string serverName, string dbName, bool LogError = true)
    {
        DataSet queryInfo = new DataSet();
        connectionString = string.Format(connectionString, serverName, dbName);
        try
        {
            //using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.AppSettings.Get("ConnectionString")))
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(query, sqlConnection);
                adapter.Fill(queryInfo);
            }
        }
        catch (Exception e)
        {
           //throw new System.ApplicationException(e.ToString());
        }

        return queryInfo;
    }

    public static object ExecuteScalar(string query, string serverName, string dbName)
    {
        object result = null;
        connectionString = string.Format(connectionString, serverName, dbName);
        try
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, sqlConnection) { CommandType = CommandType.Text };
                sqlConnection.Open();
                result = cmd.ExecuteScalar();
            }
        }
        catch (Exception e)
        {
        }

        return result;
    }

    public static int ExecuteNonQuery(string query, string serverName, string dbName)
    {
        int result = 0;
        connectionString = string.Format(connectionString, serverName, dbName);
        try
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {

                SqlCommand cmd = new SqlCommand(query, sqlConnection) { CommandType = CommandType.Text };
                sqlConnection.Open();
                result = cmd.ExecuteNonQuery();
            }
        }
        catch (Exception e)
        {
        }

        return result;
    }

    public static List<EventDetail> Get_Events(DateTime _date)
    {
        List<EventDetail> result = new List<EventDetail>();
        
        SqlConnection conn = null;
		SqlDataReader rdr  = null;
			
		try
		{
			conn = new SqlConnection(connectionString);
			conn.Open();
            SqlCommand cmd = new SqlCommand("GetEvents", conn);
			cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@StartDate", _date);
				
			rdr = cmd.ExecuteReader();
				
			while (rdr.Read())
			{
                result.Add(
                    new EventDetail
                    {
                        ID = rdr.GetInt32(0),
                        Name = rdr.GetString(1),
                        startDate = rdr[2] == DBNull.Value ? (DateTime?)null : rdr.GetDateTime(2),
                        endDate = rdr[3] == DBNull.Value ? (DateTime?)null : rdr.GetDateTime(3),
                        detail = rdr.GetString(4),
                        userID = rdr.GetInt32(5),
                        externalURL = rdr.GetString(6)
                    }
                );
            }
        }
        catch(Exception ex)
        {
        }

        return result;
    }

    public static Dictionary<string, string> GetImagesByEventID(int _eventID)
    {
        Dictionary<string, string> ImagesArray = new Dictionary<string,string>();
        
        SqlConnection conn = null;
		SqlDataReader rdr  = null;
			
		try
		{
			conn = new SqlConnection(connectionString);
			conn.Open();
            SqlCommand cmd = new SqlCommand("GetImagesByEvent", conn);
			cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@EventID", _eventID);
				
			rdr = cmd.ExecuteReader();
				
			while (rdr.Read())
			{
                ImagesArray.Add(
                        rdr.GetString(0),
                        rdr.GetString(1)
                    );
            }
        }
        catch(Exception ex)
        {
        }

        return ImagesArray;
    }

    #endregion
}