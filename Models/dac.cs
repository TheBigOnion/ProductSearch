using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public static class dac
{
    /// <summary>
    /// Gets a connection object
    /// </summary>
    /// <returns>A generic connection object</returns>
    public static SqlConnection getConnection()
    {
        SqlConnection objConnection = new SqlConnection();
        string connectionString = null;

        try
        {
            connectionString = ConfigurationManager.ConnectionStrings["ProductSearchConnectionString"].ConnectionString;
        }
        catch (Exception ex)
        {
            return null;
        }

        objConnection.ConnectionString = connectionString;
        return objConnection;
    }
}