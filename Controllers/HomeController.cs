using ProductSearch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace ProductSearch.Controllers
{
    public class HomeController : Controller
    {

        // GET: Home
        public ActionResult Index()
        {            
            return View();
        }

        public JsonResult GetData(string type)
        {
            switch (type)
            {
                case "prod_column_info":
                    return GetProdColumnInfo();
                default:
                    return GetProdColumnInfo();
            }
        }


        //ajax calls this function which will return json object  
        public JsonResult GetProdColumnInfo()
        {
            ProdColumnInfoList resultData = ProdColumnInfo.getProdColumnInfo();
            return Json(new { result = resultData }, JsonRequestBehavior.AllowGet);
        }

        //ajax calls this function which will return json object  
        public JsonResult GetProducts(string Department_Name
            , string Cost
            , string xFor
            , string Department_ID
            , string Description
            , string Last_Sold
            , string Price
            , string Product_ID
            , string Shelf_Life
            , string Unit
            )
        {
            productModel myproducts = new productModel();
            myproducts.Department_Name = Department_Name;
            myproducts.Cost = Cost;
            myproducts.Department_ID = Department_ID;
            myproducts.Description = Description;
            myproducts.Last_Sold = Last_Sold;
            myproducts.Price = Price;
            myproducts.Product_ID = Product_ID;
            myproducts.Shelf_Life = Shelf_Life;
            myproducts.Unit = Unit;
            myproducts.xFor = xFor;
            
            productList resultData = products.getProducts(myproducts);
            return Json(new { result = resultData }, JsonRequestBehavior.AllowGet);
        }


    }
}