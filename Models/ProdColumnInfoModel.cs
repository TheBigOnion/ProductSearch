using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ProductSearch.Models
{
    public class ProdColumnInfoModel
    {
        public string column_name { get; set; }
        public string data_type { get; set; }
        public int max_length { get; set; }
    }


    public class ProdColumnInfoList : List<ProdColumnInfoModel>
    {
        public ProdColumnInfoList()
        { }
    }

    public class ProdColumnInfo
    {
        /// <summary>
        /// Selects the column names and information about each column for the products table.
        /// </summary>
        /// <returns>A list of type ProdColumnInfoList.</returns>
        public static ProdColumnInfoList getProdColumnInfo()
        {
            ProdColumnInfoModel myProdColumnInfo = new ProdColumnInfoModel();
            ProdColumnInfoList myProdColumnInfoList = null;

            using (SqlConnection myConn = dac.getConnection())
            {
                SqlCommand myCommand = new SqlCommand("getProductColumnInfo", myConn);
                myCommand.CommandType = CommandType.StoredProcedure;

                myConn.Open();
                try
                {
                    myProdColumnInfoList = new ProdColumnInfoList();

                    SqlDataReader sqlReader = myCommand.ExecuteReader();
                    while (sqlReader.Read())
                    {
                        myProdColumnInfoList.Add(FillList(sqlReader));
                    }
                }
                catch (Exception ex)
                {
                }
                finally
                {
                    myConn.Close();
                }

                return myProdColumnInfoList;
            }
        }

        /// <summary>
        /// Fills a ProdColumnInfoModel object with data from the database.
        /// </summary>
        /// <param name="rec">An IDataRecord from an IDataReader</param>
        /// <returns>The filled out Product Column Info Model.</returns>
        protected static ProdColumnInfoModel FillList(IDataRecord rec)
        {
            ProdColumnInfoModel tempProdColumnInfoModel = new ProdColumnInfoModel();
            string tempColumnName = "";

            if (!rec.IsDBNull(rec.GetOrdinal("Column Name"))) {
                tempColumnName = rec.GetString(rec.GetOrdinal("Column Name"));
                tempProdColumnInfoModel.column_name = tempColumnName.Replace("_", " ");
            }
            if (!rec.IsDBNull(rec.GetOrdinal("Data Type")))
                tempProdColumnInfoModel.data_type = rec.GetString(rec.GetOrdinal("Data Type"));

            if (!rec.IsDBNull(rec.GetOrdinal("Max Length")))
                tempProdColumnInfoModel.max_length = Convert.ToInt32(rec.GetValue(rec.GetOrdinal("Max Length")));

            return tempProdColumnInfoModel;
        }
    }
}