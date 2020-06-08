using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using CurrencyWebApp.Models;
using System.Web.Mvc;
using System.Data.SQLite;
using System.Data;

namespace CurrencyWebApp.Controllers
{
    public class HomeController : Controller
    {
        HomeModel model = new HomeModel();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Search()
        {
            string startDate = "07/06/2020";
            SQLiteConnection conn = model.CreateConnection();
            DataTable dt = model.ReadData(conn, startDate, startDate);
            return View("Search", dt);
        }

        [HttpPost]
        public ActionResult Index(double input, string currency) {
            //if statement to ensure user has entered all neccessary values
            if (input != 0 && currency != null)
            {
                ViewBag.Input = model.Convert(input, currency);
                input = ViewBag.Input;
                SQLiteConnection conn = model.CreateConnection();
                model.InsertData(conn, currency, input);
                return View("Index");
            }
            else {
                return View("Index");
            }
        }

        [HttpPost]
        public ActionResult Search(String start, String end) {
            //connect to database
            SQLiteConnection conn = model.CreateConnection();
            //convert dates to the appropriate format
            start = Convert.ToDateTime(start).ToString("dd/MM/yyyy");
            end = Convert.ToDateTime(end).ToString("dd/MM/yyyy");
            //collect data
            DataTable dt = model.ReadData(conn, start, end);
            //reutrn data
            return View("Search", dt);
        }

    }
}