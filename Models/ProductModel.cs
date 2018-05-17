using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ProductSearch.Models
{
    public class productModel
    {
        public string Product_ID { get; set; }
        public string Description { get; set; }
        public string Last_Sold { get; set; }
        public string Shelf_Life { get; set; }
        public string Department_ID { get; set; }
        public string Price { get; set; }
        public string Unit { get; set; }
        public string xFor { get; set; }
        public string Cost { get; set; }
        public string Department_Name { get; set; }


    }


    public class productList : List<productModel>
    {
        public productList()
        { }
    }

    public class products
    {
        /// <summary>
        /// Searches the product table for all products that match the search criteria.
        /// </summary>
        /// <param name="myproducts">The productModel with search parameters.</param>
        /// <returns>A generic list of products.</returns>
        public static productList getProducts(productModel myproducts)
        {
            
            productList myProductList = null;

            using (SqlConnection myConn = dac.getConnection())
            {
                SqlCommand myCommand = new SqlCommand("searchProducts", myConn);
                myCommand.CommandType = CommandType.StoredProcedure;
                
                if (myproducts.Product_ID == null)
                    myCommand.Parameters.AddWithValue("@Product_ID", DBNull.Value);
                else
                    myCommand.Parameters.AddWithValue("@Product_ID", myproducts.Product_ID);

                if (myproducts.Description == null)
                    myCommand.Parameters.AddWithValue("@Description", DBNull.Value);
                else
                    myCommand.Parameters.AddWithValue("@Description", myproducts.Description);

                if (myproducts.Department_Name == null)
                    myCommand.Parameters.AddWithValue("@Department_Name", DBNull.Value);
                else
                    myCommand.Parameters.AddWithValue("@Department_Name", myproducts.Department_Name);

                if (myproducts.Last_Sold == null)
                    myCommand.Parameters.AddWithValue("@Last_Sold", DBNull.Value);
                else
                    myCommand.Parameters.AddWithValue("@Last_Sold", myproducts.Last_Sold);

                if (myproducts.Shelf_Life == null)
                    myCommand.Parameters.AddWithValue("@Shelf_Life", DBNull.Value);
                else
                    myCommand.Parameters.AddWithValue("@Shelf_Life", myproducts.Shelf_Life);

                if (myproducts.Price == null)
                    myCommand.Parameters.AddWithValue("@Price", DBNull.Value);
                else
                    myCommand.Parameters.AddWithValue("@Price", myproducts.Price);

                if (myproducts.Unit == null)
                    myCommand.Parameters.AddWithValue("@Unit", DBNull.Value);
                else
                    myCommand.Parameters.AddWithValue("@Unit", myproducts.Unit);

                if (myproducts.xFor == null)
                    myCommand.Parameters.AddWithValue("@xFor", DBNull.Value);
                else
                    myCommand.Parameters.AddWithValue("@xFor", myproducts.xFor);

                if (myproducts.Cost == null)
                    myCommand.Parameters.AddWithValue("@Cost", DBNull.Value);
                else
                    myCommand.Parameters.AddWithValue("@Cost", myproducts.Cost);

                myConn.Open();
                try
                {
                    myProductList = new productList();

                    SqlDataReader sqlReader = myCommand.ExecuteReader();
                    while (sqlReader.Read())
                    {
                        myProductList.Add(FillList(sqlReader));
                    }
                }
                catch (Exception ex)
                {
                }
                finally
                {
                    myConn.Close();
                }

                return myProductList;
            }
        }

        /// <summary>
        /// Fills a productModel with data from the database.
        /// </summary>
        /// <param name="rec">An IDataRecord from an IDataReader</param>
        /// <returns>The filled out productModel.</returns>
        protected static productModel FillList(IDataRecord rec)
        {
            productModel tempProductModel = new productModel();

            if (!rec.IsDBNull(rec.GetOrdinal("Product_ID")))
                tempProductModel.Product_ID = Convert.ToString(rec.GetValue(rec.GetOrdinal("Product_ID")));

            if (!rec.IsDBNull(rec.GetOrdinal("Description")))
                tempProductModel.Description = rec.GetString(rec.GetOrdinal("Description"));

            if (!rec.IsDBNull(rec.GetOrdinal("Last_Sold")))
                tempProductModel.Last_Sold = Convert.ToString(rec.GetValue(rec.GetOrdinal("Last_Sold")));

            if (!rec.IsDBNull(rec.GetOrdinal("Shelf_Life")))
                tempProductModel.Shelf_Life = Convert.ToString(rec.GetValue(rec.GetOrdinal("Shelf_Life")));

            if (!rec.IsDBNull(rec.GetOrdinal("Department_ID")))
                tempProductModel.Department_ID = Convert.ToString(rec.GetValue(rec.GetOrdinal("Department_ID")));

            if (!rec.IsDBNull(rec.GetOrdinal("Price")))
                tempProductModel.Price = Convert.ToString(rec.GetValue(rec.GetOrdinal("Price")));

            if (!rec.IsDBNull(rec.GetOrdinal("Unit")))
                tempProductModel.Unit = rec.GetString(rec.GetOrdinal("Unit"));

            if (!rec.IsDBNull(rec.GetOrdinal("xFor")))
                tempProductModel.xFor = Convert.ToString(rec.GetValue(rec.GetOrdinal("xFor")));

            if (!rec.IsDBNull(rec.GetOrdinal("Cost")))
                tempProductModel.Cost = Convert.ToString(rec.GetValue(rec.GetOrdinal("Cost")));

            if (!rec.IsDBNull(rec.GetOrdinal("Department_Name")))
                tempProductModel.Department_Name = rec.GetString(rec.GetOrdinal("Department_Name"));

            return tempProductModel;
        }
    }
}