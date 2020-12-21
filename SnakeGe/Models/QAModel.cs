using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EpidemicManager.Models
{
    public class QAModel
    {
        public string ID_user { get; set; }
        public string ID_exp { get; set; }
        public string answer { get; set; }
        public string question { get; set; }
        public string date_a { get; set; }
        public string date_q { get; set; }
        public string time_a { get; set; }
        public string time_q { get; set; }
        public string ID_question { get; set; }
    }
    public class AModel
    {
        public List<string>  question{ get; set; }
        public List<string> ID_user { get; set; }
        public List<string> ID_question { get; set; }
        public int n { get; set; }
    }
    public class QAListModel
    {
        public string ID_user { get; set; }
        public List<string> ID_exp { get; set; }
        public List<string> answer { get; set; }
        public List<string> question_a { get; set; }
        public List<string> question_q { get; set; }
        public List<string> date_a { get; set; }//时间
        public List<string> date_q_a { get; set; }//被回答的问题
        public List<string> time_a { get; set; }
        public List<string> time_q_a { get; set; }
        public List<string> ID_question_a { get; set; }
        public List<string> ID_question_q { get; set; }//没被回答的
        public List<string> date_q_q { get; set; }
        public List<string> time_q_q { get; set; }
        public int n_q { get; set; }//没回答的
        public int n_a { get; set; }//回答的
}
}
