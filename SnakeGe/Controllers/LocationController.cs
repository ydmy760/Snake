using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using EpidemicManager;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using SnakeGe.Models;

namespace SnakeGe.Controllers
{
    public class LocationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public List<List<string>> Getinfo(string province)
        {
            LocationModel model = new LocationModel();
            List<string> list1;
            List<string> a = new List<string>();
            List<List<string>> list2 = new List<List<string>>();
            var snake = Sql.Read("SELECT * FROM distribute WHERE province=@0",province);
            if (snake.Count == 0)
            {
                snake = Sql.Read("SELECT * FROM distribute WHERE city=@0",province);
            }
            if (snake.Count == 0)
            {
                List<List<string>> vs = new List<List<string>>();
                List<string> vs1 = new List<string>();
                vs1.Add("wrong");
                vs.Add(vs1);
                return  vs;
            }
            foreach (DataRow ss in snake)
            {
                list1 = new List<string>();
                a.Add(ss[0].ToString());
                list1.Add(ss[0].ToString());
                list1.Add(ss[1].ToString());
                list1.Add(ss[2].ToString());
                list1.Add(ss[3].ToString());
                list1.Add(ss[4].ToString());
                list1.Add(ss[5].ToString());
                list1.Add(ss[6].ToString());
                list1.Add(ss[7].ToString());
                if (ss[8].ToString() == "0")
                {
                    list1.Add("无毒");
                }
                else
                {
                    list1.Add("有毒");
                }
                list1.Add(ss[9].ToString());
                list2.Add(list1);
            }
            model.snake = list2;

            return list2;
        }
        public List<string> GetSnake(string province)
        {
            List<string> list = new List<string>();
            var snake = Sql.Read("SELECT * FROM distribute WHERE province= '"+province+"'");
            if (snake.Count == 0) {
                snake = Sql.Read("SELECT * FROM distribute WHERE city= '" + province + "'");
            }
            foreach(DataRow sna in snake)
            {
                list.Add(sna[2].ToString());
                
            }
            return list;//返回一个内容是在这个省蛇的信息
        }

        public IActionResult Correct(string province)
        {
            LocationModel model = new LocationModel();
            List<string> list1;
            List<string> a = new List<string>();
            var snake = Sql.Read("SELECT * FROM distribute WHERE province=@0", province);
            if (snake.Count == 0)
            {
                snake = Sql.Read("SELECT * FROM distribute WHERE city= '" + province + "'");
            }
            foreach (DataRow ss in snake)
            {
                list1 = new List<string>();
                a.Add(ss[0].ToString());
            }
            model.numbers = a;
            return View(model);
        }

        [HttpPost]
        public List<List<string>> Getinfo2(string id)
        {
            LocationModel model = new LocationModel();
            List<string> list1;
            List<string> a = new List<string>();
            List<List<string>> list2 = new List<List<string>>();
            var snake = Sql.Read("SELECT * FROM distribute WHERE idSnake=@0", id);
            
            foreach (DataRow ss in snake)
            {
                list1 = new List<string>();
                a.Add(ss[0].ToString());
                list1.Add(ss[0].ToString());
                list1.Add(ss[1].ToString());
                list1.Add(ss[2].ToString());
                list1.Add(ss[3].ToString());
                list1.Add(ss[4].ToString());
                list1.Add(ss[5].ToString());         
                list1.Add(ss[6].ToString());
                list1.Add(ss[7].ToString());
                if (ss[8].ToString() == "0")
                {
                    list1.Add("无毒");
                }
                else
                {
                    list1.Add("有毒");
                }
                list1.Add(ss[9].ToString());
                list2.Add(list1);
            }
            model.snake = list2;

            return list2;
        }
    }

}
