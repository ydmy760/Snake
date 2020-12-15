using System.Collections.Generic;
using System.Data;

using Microsoft.AspNetCore.Mvc;
using EpidemicManager.Models;
using System.Diagnostics;
using System.Threading;
using Microsoft.AspNetCore.Http;

namespace EpidemicManager.Controllers
{
    public class TemplateController : Controller
    {
        public IActionResult Index()
        {
            TestSql();

            var session = HttpContext.Session;
            var userKind = session.GetString("userKind");
            var userId = session.GetString("userId");

            var people = Sql.Read("SELECT idSnake FROM snake");
            var list = new List<string>();
            if (people.Count == 0)
            {
               
            }
            else
            {
                foreach (DataRow person in people)
                {
                    list.Add(person[0].ToString());
                }
            }

            var model = new TemplateModel
            {
                Ids = list,
            };

            return View(model);
        }


        [HttpPost]
        public JsonResult Click(string name, int number)
        {
            return Json(new
            {
                name,
                num = number + 1,
            });
        }

        [Conditional("DebugSql")]
        private void TestSql()
        {
            for (var i = 0; i < 40; i++)
            {
                if (i == 20) Thread.Sleep(500);
                var thread = new Thread(() =>
                {
                    Sql.Read("SELECT idSnake FROM snake");
                });
                thread.Start();
            }
        }
    }
}
