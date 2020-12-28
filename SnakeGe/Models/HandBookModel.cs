using System;
using System.Collections.Generic;

namespace 格蛇社.Models
{
    public class HandBookModel
    {
        public Snake snake;
        public List<string> snakes;
    }

    public class Snake
    {
        public int snake_ID { get; set; }
        public string order { get; set; }
        public bool is_Poisonous { get; set; }
        public string genus { get; set; }
        public string family { get; set; }
        public string speciesName { get; set; }
        public string names { get; set; }
        public string province { get; set; }
        public string snakeIntro { get; set; }
    }

    public class Venom
    {
        public int snake_ID { get; set; }
        public string snakeName { get; set; }
        public string LD50 { get; set; }
        public string venomAmount { get; set; }
        public string venomType { get; set; }
        public string killMice { get; set; }
        public string fatalityRate { get; set; }
    }
}
