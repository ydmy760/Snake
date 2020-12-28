using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EpidemicManager;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SnakeGe.Models;

namespace SnakeGe.Controllers
{
    public class LoginController : Controller
    {
/*        public IActionResult Index()
        {
            return View();
        }*/


        public IActionResult Logon(string user_name,string password,string password_next,string adictor,string expert)
        {
            var session = HttpContext.Session;
            LogonModel logon = new LogonModel();
            string userkind;
            if (user_name == null && password == null && password_next == null && adictor==null && expert==null)
            {
                logon.Logon = "null";
                return View(logon);
            }
            if(user_name == null || password == null || password_next == null|| adictor == null && expert == null)
            {
                logon.Logon = "empty";
                return View(logon);
            }
            if (password != password_next)
            {
                logon.Logon = "wrong";
                return View(logon);
            }
            if (adictor == "on") { userkind = "用户"; }
            else { userkind = "专家"; }

            if (userkind == "用户")
            {
                Sql.Execute("INSERT INTO normuser(Username,passwd) VALUES (?,?)", user_name, password);
                var line = Sql.Read("SELECT LAST_INSERT_ID()");
                string id = line[0][0].ToString();
                logon.id = id;
                logon.Logon = "success";
                session.SetString("ID", id);
                session.SetString("UserKind", userkind);
                session.SetString("UserName", user_name);
            }
            else
            {
                Sql.Execute("INSERT INTO expert(Name,Passwd) VALUES (?,?)", user_name, password);
                var line = Sql.Read("SELECT LAST_INSERT_ID()");
                string id = line[0][0].ToString();
                logon.id = id;
                logon.Logon = "success";
                session.SetString("ID", id);
                session.SetString("UserKind", userkind);
                session.SetString("UserName", user_name);
            }
            
            return View(logon);
        }


        public IActionResult Index(string id, string password, string adictor,string expert,string path)
        {
            LoginModel model = new LoginModel();
            var jumppath = "null";
            if (path != null)
            {
                jumppath = path.StartsWith("/") ? path : "/" + path;
            }
          
            if (id == null && password == null)
            {
                model.State = null;
                return View(model);
            }
            else
            {
                if (id == null || password == null || adictor == null && expert == null)
                {
                    model.State = "empty";
                }
                else
                {
                    string userkind;
                    var session = HttpContext.Session;
                    int id1 = int.Parse(id);
                    if (adictor == "on")
                    {
                        userkind = "用户";
                    }
                    else
                    {
                        userkind = "专家";
                    }

                    var line = userkind switch
                    {
                        "用户" => Sql.Read("SELECT passwd,Username FROM normuser WHERE ID = @0", id1),
                        "专家" => Sql.Read("SELECT Passwd,Name FROM expert WHERE ID = @0", id1),
                        _ => throw new NotImplementedException(),
                    };
                    if (line.Count == 0)
                    {
                        model.State = "wrong_db";
                        return View(model);
                    }
                    string password_re = line[0][0].ToString();
                    string username = line[0][1].ToString();
                    if (password == password_re)
                    {
                        model.State = "success";
                        session.SetString("ID", id);
                        session.SetString("UserKind", userkind);
                        session.SetString("UserName", username);
                        model.path = jumppath;
                    }
                    else
                    {
                        model.State = "wrong_pass";
                        session.SetString("UserName", "请登录");
                    }
                }
                return View(model);
            }
        }

        public void Logout()
        {
            var session = HttpContext.Session;
            session.Clear();
        }
    }
}
