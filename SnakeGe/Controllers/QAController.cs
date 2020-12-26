using EpidemicManager;
using EpidemicManager.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;

namespace EpidemicManager.Controllers
{
    public class QAController : Controller
    {
        public IActionResult Create()
        {
            if (HttpContext.Session.GetString("userKind") != "user")
            {
                return RedirectToAction("Index", "Login", new { path = "QA /Create" });
            }
            return View();
        }
        [HttpPost]
        public IActionResult Create_()
        {
            QAModel m = new QAModel();
            m.ID_user = HttpContext.Session.GetString("userId");
            m.question = Request.Form["question"];
            m.date_q = DateTime.Now.ToString("yyyy-MM-dd");
            m.time_q = DateTime.Now.ToString("T");
            
            Sql.Execute("INSERT INTO OnlineQ(ID,date,time,detail,isAns)  VALUES(@0,@1,@2,@3,@4)", m.ID_user, m.date_q, m.time_q, m.question,0);
            return RedirectToAction("Index_QA");//这个东西还没有

        }
        //12.10创建问题应该写好了

        //需要在问题里面加个标志位表示是否是已经回答isAns=0/1
        //已经在sql里写了
        //12.10回答索引也完成了
        public IActionResult Answer_Index()
        {
            if (HttpContext.Session.GetString("userKind") != "exp")
            {
                return RedirectToAction("Index", "Login", new { path = "QA /Answer_Index" });
            }
            var list_q = new List<string>();
            var list_d = new List<string>();
            var list_u = new List<string>();
            var name = Sql.Read("SELECT ID, detail,id_number FROM OnlineQ WHERE isAns=@0", 0);
            var Con = 0;
            foreach (DataRow n in name)
            {
                list_u.Add(n[0].ToString());
                list_d.Add(n[1].ToString());
                list_q.Add(n[2].ToString());
                Con++;
            }
            AModel m = new AModel();
            m.ID_user = list_u;
            m.question = list_d;
            m.ID_question = list_q;
            m.n = Con;
            return View(m);
        }//最好存用户昵称不存ID，加一个查询就行了
        public IActionResult Answer()
        {
            QAModel m = new QAModel();
            m.ID_exp = HttpContext.Session.GetString("userId");
            m.ID_question = Request.Form["question_id"];
            var name = Sql.Read("SELECT ID, detail,date,time FROM OnlineQ WHERE id_number=@0", m.ID_question);
            foreach (DataRow n in name)
            {
                m.ID_user = n[0].ToString();
                m.question = n[1].ToString();
                m.date_q = n[2].ToString();
                m.time_q = n[3].ToString();
            }
            return View(m);//没有显示问题本身，在View里面
        }
        //问题显示粗暴的加进去了，没有调页面布局
        public IActionResult Answer_()
        {
            QAModel m = new QAModel();
            m.ID_exp = HttpContext.Session.GetString("userId");
            m.question = Request.Form["question"];
            m.date_q = DateTime.Now.ToString("yyyy-MM-dd");
            m.time_q = DateTime.Now.ToString("T");
            m.ID_question= Request.Form["question_ID"];
            Sql.Execute("INSERT INTO ExperAnswer(ID,date,time,detail,ID_Q)  VALUES(@0,@1,@2,@3,@4)", m.ID_user, m.date_q, m.time_q, m.question, m.ID_question);
            //上面这句有问题
            //12.10最后那个isAns改了问题的ID属性，不知道数据库里有没有
            //我不知道这个表叫啥
            Sql.Execute("UPDATE OnlineQ SET isAns = @0 WHERE id_number = @1", m.ID_question,1);
            return RedirectToAction("Answer_Index");
        }




        //12.10用户查看自己回答的目录
        public IActionResult Index_QA()
        {
            if (HttpContext.Session.GetString("userKind") != "user")
            {
                return RedirectToAction("Index", "Login", new { path = "QA /Answer_Index" });
            }
            var list_q = new List<string>();
            var list_d = new List<string>();
            var list_t = new List<string>();
            var list_dd = new List<string>();
            var ID_user = HttpContext.Session.GetString("userId");
            var name = Sql.Read("SELECT date,time detail,id_number FROM OnlineQ WHERE ID=@0 AND isAns=@1", ID_user,0);
            var Con = 0;
            foreach (DataRow n in name)
            {
                list_dd.Add(n[0].ToString());
                list_t.Add(n[1].ToString());
                list_d.Add(n[2].ToString());
                list_q.Add(n[3].ToString());
                Con++;
            }
            QAListModel m = new QAListModel();
            m.ID_user = ID_user;
            m.question_q = list_d;
            m.ID_question_q = list_q;
            m.time_q_q = list_t;
            m.date_q_q = list_dd;
            m.n_q = Con;
            //刚结束了没回答的，回答的在后面


            var list_a = new List<string>();
            var list_exp = new List<string>();
            var list_t_a = new List<string>();
            var list_d_a = new List<string>();
            name = Sql.Read("SELECT date,time detail,id_number FROM OnlineQ WHERE ID=@0 AND isAns=@1", ID_user, 1);
            Con = 0;
            foreach (DataRow n in name)
            {
                list_dd.Add(n[0].ToString());
                list_t.Add(n[1].ToString());
                list_d.Add(n[2].ToString());
                list_q.Add(n[3].ToString());
                //从回答的表里找回答
                var ans = Sql.Read("SELECT ID, detail,date,time FROM ExperAnswer WHERE id_number=@0", n[3].ToString());
                foreach(DataRow a in ans)
                {
                    list_exp.Add(a[0].ToString());
                    list_a.Add(a[1].ToString());
                    list_d_a.Add(a[2].ToString());
                    list_t_a.Add(a[3].ToString());
                }
                
                Con++;
            }
            m.question_a = list_d;
            m.ID_question_a = list_q;
            m.time_q_a = list_t;
            m.date_q_a = list_dd;
            m.answer = list_a;
            m.time_a = list_t_a;
            m.date_a = list_d_a;
            m.ID_exp = list_exp;
            m.n_a = Con;

            return View(m);
        }
    }
}

//查看就搞一个页面吧