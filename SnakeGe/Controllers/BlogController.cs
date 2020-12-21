using EpidemicManager;
using EpidemicManager.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;


namespace EpidemicManager.Controllers
{
    public class BlogController:Controller
    {
        public IActionResult Create()
        {
            if (HttpContext.Session.GetString("userKind") != "user")
            {
                return RedirectToAction("Index", "Login", new { path = "Blog /Create" });
            }
            return View();
        }
        public IActionResult Create_()
        {
            BlogModel m = new BlogModel();
            m.userID  = HttpContext.Session.GetString("userId");
            m.content = Request.Form["content"];
            m.title= Request.Form["title"];
            m.date = DateTime.Now.ToString("yyyy-MM-dd");
            m.time = DateTime.Now.ToString("T");

            Sql.Execute("INSERT INTO Blog(ID,date,time,detail.title)  VALUES(@0,@1,@2,@3)", m.userID, m.date, m.time, m.content,m.title);
            return RedirectToAction("Index");//不知道这个有没有这个，应该是主页面
        }

        public IActionResult Comment( )
        {
            if (HttpContext.Session.GetString("userKind") != "user")
            {
                return RedirectToAction("Index", "Login", new { path = "Blog /Comment" });
            }
            return View();
        }

        public IActionResult Comment_()
        {
            CommentModel m = new CommentModel();
            var userID = HttpContext.Session.GetString("userId");
            m.content = Request.Form["content"];
            m.BlogID = Request.Form["blog_id"];
            m.date = DateTime.Now.ToString("yyyy-MM-dd");
            m.time = DateTime.Now.ToString("T");

            Sql.Execute("INSERT INTO Comment(ID,date,time,detail,blog_id)  VALUES(@0,@1,@2,@3)",userID, m.date, m.time, m.content,m.BlogID);
            return RedirectToAction("Index");//不知道这个有没有这个
        }
        //blogID怎么得到没有搞明白//搞明白了

        //所有的博客目录

        public IActionResult Blog_Index()
        {
            if (HttpContext.Session.GetString("userKind") != "user")
            {
                return RedirectToAction("Index", "Login", new { path = "Blog /Blog_Index" });
            }
            var list_t = new List<string>();
            var list_d = new List<string>();
            var list_u = new List<string>();
            var list_b = new List<string>();
            var list_tt = new List<string>();
            var name = Sql.Read("SELECT ID,date,time,title,blog_id FROM Blog ");
            var Con = 0;
            foreach (DataRow n in name)
            {
                list_u.Add(n[0].ToString());
                list_d.Add(n[1].ToString());
                list_t.Add(n[2].ToString());
                list_tt.Add(n[3].ToString());
                list_b.Add(n[4].ToString());
                Con++;
            }
            BlogListModel m = new BlogListModel();
            m.userID = list_u;
            m.date = list_d;
            m.time = list_t;
            m.title = list_tt;
            m.BlogID = list_b;
            m.n = Con;
            return View(m);
        }
        //应该显示用户名不是用户Id

        public IActionResult Blog()
        {

            BlogModel m = new BlogModel();
            m.userID = HttpContext.Session.GetString("userId");
            m.BlogID = Request.Form["blog_id"];
            var name = Sql.Read("SELECT ID, detail,date,time FROM Blog WHERE blog_id=@0", m.BlogID);
            foreach (DataRow n in name)
            {
                m.userID = n[0].ToString();
                m.content = n[1].ToString();
                m.date = n[2].ToString();
                m.time = n[3].ToString();
            }
            var com = Sql.Read("SELECT ID, detail,date,time FROM Comment WHERE blog_id=@0", m.BlogID);
            foreach (DataRow n in com)
            {
                CommentModel c = new CommentModel();
                c.BlogID = m.BlogID;
                c.userID = n[0].ToString();
                c.content = n[1].ToString();
                c.date = n[2].ToString();
                c.time = n[3].ToString();
                m.comment.Add(c);
            }
            return View(m);
        }

        public IActionResult Blog_self()
        {
            if (HttpContext.Session.GetString("userKind") != "user")
            {
                return RedirectToAction("Index", "Login", new { path = "Blog /Blog_self" });
            }
            var user = HttpContext.Session.GetString("userId");
            var list_t = new List<string>();
            var list_d = new List<string>();
            var list_u = new List<string>();
            var list_b = new List<string>();
            var list_tt = new List<string>();
            var name = Sql.Read("SELECT ID,date,time,title,blog_id FROM Blog WHREE ID=@0",user);
            var Con = 0;
            foreach (DataRow n in name)
            {
                list_u.Add(n[0].ToString());
                list_d.Add(n[1].ToString());
                list_t.Add(n[2].ToString());
                list_tt.Add(n[3].ToString());
                list_b.Add(n[4].ToString());
                Con++;
            }
            BlogListModel m = new BlogListModel();
            m.userID = list_u;
            m.date = list_d;
            m.time = list_t;
            m.title = list_tt;
            m.BlogID = list_b;
            m.n = Con;
            return View(m);
        }



    }
}
