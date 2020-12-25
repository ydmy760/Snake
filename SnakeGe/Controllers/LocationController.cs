using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using EpidemicManager;
using Microsoft.AspNetCore.Mvc;
using SnakeGe.Models;

namespace SnakeGe.Controllers
{
    public class LocationController : Controller
    {
        public IActionResult Index(string province)
        {
            LocationModel model = new LocationModel();
            List<string> list1 = new List<string>();
            List<List<string>> list2 = new List<List<string>>();
            var snake = Sql.Read("SELECT * FROM snake WHERE province=@0",province);
            foreach(DataRow ss in snake)
            {
                list1.Add(ss[0].ToString());
                list1.Add(ss[1].ToString());
                list1.Add(ss[2].ToString());
                list1.Add(ss[3].ToString());
                list1.Add(ss[4].ToString());
                list1.Add(ss[5].ToString());
                list1.Add(ss[6].ToString());
                list1.Add(ss[7].ToString());
                list1.Add(ss[8].ToString());
                list2.Add(list1);
            }
            model.snake = list2;
            return View(model);
        }
        public List<string> GetSnake(string province)
        {
            List<string> list = new List<string>();
            var snake = Sql.Read("SELECT * FROM snake WHERE province= '"+province+"'");
            foreach(DataRow sna in snake)
            {
                list.Add(sna[7].ToString());
                
            }
            return list;//返回一个内容是在这个省蛇的信息
        }
    }

}
