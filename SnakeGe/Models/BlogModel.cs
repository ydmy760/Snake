using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EpidemicManager.Models
{
    public class BlogModel
    {
        public string content { get; set; }
        public string userID { get; set; }
        public string date { get; set; }
        public string time { get; set; }
        public string title { get; set; }
        public string BlogID { get; set; }
        public List<CommentModel> comment { get; set; }
        public int n = 0;
    }
    public class BlogListModel
    {
        public List<string> BlogID { get; set; }
        public List<string> title { get; set; }
        public List<string> date { get; set; }
        public List<string> time { get; set; }
        public List<string> userID { get; set; }
        public int n = 0;

    }
    public class CommentModel {
        public string BlogID { get; set; }
        public string content { get; set; }
        public string date { get; set; }
        public string time { get; set; }
        public string userID { get; set; }

    }

}
