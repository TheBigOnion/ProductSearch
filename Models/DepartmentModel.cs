using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ProductSearch.Models
{
    public class departmentModel
    {
        public int dept_id { get; set; }
        public string dept_name { get; set; }
    }


    public class departmentList : List<departmentModel>
    {
        public departmentList()
        { }
    }

    public class departments
    {
        /// <summary>
        /// Searches for a Country business object.
        /// </summary>
        /// <param name="myCountryBo">The Country business object with search parameters.</param>
        /// <returns>A generic list of Countries.</returns>
        public static departmentList getDepartments()
        {
            departmentModel mydepartments = new departmentModel();
            departmentList myDepartmentList = null;

            using (SqlConnection myConn = dac.getConnection())
            {
                SqlCommand myCommand = new SqlCommand("getDepartments", myConn);
                myCommand.CommandType = CommandType.StoredProcedure;

                myConn.Open();
                try
                {
                    myDepartmentList = new departmentList();

                    SqlDataReader sqlReader = myCommand.ExecuteReader();
                    while (sqlReader.Read())
                    {
                        myDepartmentList.Add(FillList(sqlReader));
                    }
                }
                catch (Exception ex)
                {
                }
                finally
                {
                    myConn.Close();
                }

                return myDepartmentList;
            }
        }

        /// <summary>
        /// Fills a business object with data from the database.
        /// </summary>
        /// <param name="rec">An IDataRecord from an IDataReader</param>
        /// <returns>The filled out business object.</returns>
        protected static departmentModel FillList(IDataRecord rec)
        {
            departmentModel tempDepartmentModel = new departmentModel();

            if (!rec.IsDBNull(rec.GetOrdinal("dept_id")))
                tempDepartmentModel.dept_id = rec.GetInt32(rec.GetOrdinal("dept_id"));

            if (!rec.IsDBNull(rec.GetOrdinal("dept_name")))
                tempDepartmentModel.dept_name = rec.GetString(rec.GetOrdinal("dept_name"));


            return tempDepartmentModel;
        }
    }

}