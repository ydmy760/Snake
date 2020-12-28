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
           if (HttpContext.Session.GetString("userKind") != "用户")
            {
                return RedirectToAction("Index", "Login", new { path = "Blog /Create" });
            }
            return View();
        }
        public IActionResult Create_()
        {
            BlogModel m = new BlogModel();
            m.userID = HttpContext.Session.GetString("ID"); //"2020000";//
            m.content = Request.Form["content"];
            m.title= Request.Form["title"];
            m.date = DateTime.Now.ToString("yyyy-MM-dd");
            m.time = DateTime.Now.ToString("T");

            Sql.Execute("INSERT INTO Blog(ID,date,time,detail,title)  VALUES(@0,@1,@2,@3,@4)", m.userID, m.date, m.time, m.content,m.title);
            return RedirectToAction("Blog_Index");//不知道这个有没有这个，应该是主页面
        }

        public IActionResult Comment( )
        {
            if (HttpContext.Session.GetString("userKind") != "用户")
            {
                return RedirectToAction("Index", "Login", new { path = "Blog /Comment" });
            }
            CommentModel m = new CommentModel();
            m.BlogID= Request.Form["blog_id"];
            return View(m);
        }

        public IActionResult Comment_()
        {
            CommentModel m = new CommentModel();
            var userID = HttpContext.Session.GetString("ID");//"2020000";//
            m.content = Request.Form["content"];
            m.BlogID = Request.Form["blog_id"];
            m.date = DateTime.Now.ToString("yyyy-MM-dd");
            m.time = DateTime.Now.ToString("T");

            Sql.Execute("INSERT INTO Comment(ID,date,time,detail,blog_id)  VALUES(@0,@1,@2,@3,@4)", userID, m.date, m.time, m.content,m.BlogID);
            return RedirectToAction("Blog_",new { id=m.BlogID});
        }
        //blogID怎么得到没有搞明白//搞明白了

        //所有的博客目录

        public IActionResult Blog_Index()
        {
            if (HttpContext.Session.GetString("userKind") != "用户")
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
            m.userID = HttpContext.Session.GetString("ID"); //"2020000";//HttpContext.Session.GetString("ID");
            m.BlogID = Request.Form["blog_id"];
            var name = Sql.Read("SELECT ID, detail,date,time FROM Blog WHERE blog_id=@0", m.BlogID);
            foreach (DataRow n in name)
            {
                m.userID = n[0].ToString();
                m.content = n[1].ToString();
                m.date = n[2].ToString();
                m.time = n[3].ToString();
            }
            List<CommentModel> comments=new List<CommentModel>(); 
            var com = Sql.Read("SELECT ID, detail,date,time FROM Comment WHERE blog_id=@0", m.BlogID);
            int conc = 0;
            foreach (DataRow n in com)
            {
                CommentModel c = new CommentModel();
                c.BlogID = m.BlogID;
                c.userID = n[0].ToString();
                c.content = n[1].ToString();
                c.date = n[2].ToString();
                c.time = n[3].ToString();
                comments.Add(c);
                conc++;
            }
            m.comment = comments;
            m.n = conc;
            return View(m);
        }
        public IActionResult Blog_(string id)
        {

            BlogModel m = new BlogModel();
            m.userID = HttpContext.Session.GetString("ID"); //"2020000";//HttpContext.Session.GetString("ID");
            m.BlogID = id;
            var name = Sql.Read("SELECT ID, detail,date,time FROM Blog WHERE blog_id=@0", m.BlogID);
            foreach (DataRow n in name)
            {
                m.userID = n[0].ToString();
                m.content = n[1].ToString();
                m.date = n[2].ToString();
                m.time = n[3].ToString();
            }
            List<CommentModel> comments = new List<CommentModel>();
            var com = Sql.Read("SELECT ID, detail,date,time FROM Comment WHERE blog_id=@0", m.BlogID);
            int conc = 0;
            foreach (DataRow n in com)
            {
                CommentModel c = new CommentModel();
                c.BlogID = m.BlogID;
                c.userID = n[0].ToString();
                c.content = n[1].ToString();
                c.date = n[2].ToString();
                c.time = n[3].ToString();
                comments.Add(c);
                conc++;
            }
            m.comment = comments;
            m.n = conc;
            return View(m);
        }

        public IActionResult Blog_self()
        {
            if (HttpContext.Session.GetString("userKind") != "用户")
            {
                return RedirectToAction("Index", "Login", new { path = "Blog /Blog_self" });
            }
            var user = HttpContext.Session.GetString("ID"); // 2020000;//HttpContext.Session.GetString("ID");
            var list_t = new List<string>();
            var list_d = new List<string>();
            var list_u = new List<string>();
            var list_b = new List<string>();
            var list_tt = new List<string>();
            var name = Sql.Read("SELECT ID,date,time,title,blog_id FROM Blog WHERE ID=@0",user);
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
