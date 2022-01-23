using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using testDataGame.Models;

namespace testDataGame.Controllers
{
    public class DashboardController : Controller
    {
        private DataGmaeEntities dc = new DataGmaeEntities();
        // GET: Dashboard
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult VisualizeDB()
        {
            return Json(Result(), JsonRequestBehavior.AllowGet);
        }
        public List<Datagame> Result()
        {
            List<Datagame> datagames = new List<Datagame>();
            return datagames;
        }

        public JsonResult GetData()
        {
            {
                //var v = dc.Datagames;
                var v = (from a in dc.Datagames
                         group a by a.TypeGame into g
                         select new
                         {
                             TypeGame = g.Key,
                             Count = g.Count(),
                         });
                if (v != null)
                {
                    var chartData = new object[v.Count() + 1];
                    chartData[0] = new object[]
                    {
                         "Type Game",
                         "Des",
                    };
                    int j = 0;

                    foreach (var i in v)
                    {
                        j++;
                        //chartData[j] = new object[] { i.NameGame.ToString(), i.TypeGame, i.Description_Game, i.url };
                        chartData[j] = new object[] { i.TypeGame.ToString(), i.Count.ToString() };
                    }
                    return new JsonResult { Data = chartData, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                }
            }
            return new JsonResult { Data = null, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
    }
}