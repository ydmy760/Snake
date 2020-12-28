using System;
using System.Collections.Generic;
using System.Data;
using 格蛇社;
using Microsoft.AspNetCore.Mvc;
using 格蛇社.Models;
using System.Diagnostics;
using System.Threading;
using Microsoft.AspNetCore.Http;

namespace 格蛇社.Controllers
{
    public class HandBookController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            var snake = Sql.Read("SELECT speciesName FROM Snake");//查找所有蛇名
            var snakes = new List<string>();
            var model = new HandBookModel();
            foreach(DataRow SNAKE in snake)
            { 
                snakes.Add(SNAKE[0].ToString());
            }
            model.snakes = snakes;
            return View(model);
        }

        public IActionResult Snake(string SnakeName)
        {
            var snake = Sql.Read("SELECT * FROM Snake WHERE speciesName=@0", SnakeName);
            Snake mysnake=new Snake();
            var model = new HandBookModel();
            foreach(DataRow SNAKE in snake)
            {
                var ID = Convert.ToInt32(SNAKE[0]);
                mysnake.snake_ID = ID;
                mysnake.order = SNAKE[1].ToString();
                mysnake.family = SNAKE[2].ToString();
                mysnake.genus = SNAKE[3].ToString();
                mysnake.speciesName = SNAKE[4].ToString();
                mysnake.names = SNAKE[5].ToString();
                var isP = Convert.ToInt32(SNAKE[6]);
                if (isP == 1)
                    mysnake.is_Poisonous = true;
                else
                    mysnake.is_Poisonous = false;
                mysnake.province = SNAKE[7].ToString();
                mysnake.snakeIntro = SNAKE[8].ToString();
            }
            model.snake = mysnake;
            return View(model);
        }

        public IActionResult Venom(string SnakeID)
        {
            Venom myvenom = new Venom();
            var venom = Sql.Read("SELECT * FROM Venom WHERE idSnake=@0", SnakeID);
            var Name = Sql.Read("SELECT speciesName FROM Snake WHERE idSnake=@0", SnakeID);
            var name = Name[0][0].ToString();
            foreach (DataRow VENOM in venom)
            {
                myvenom.snakeName = name;
                myvenom.LD50 = VENOM[1].ToString();
                myvenom.venomAmount = VENOM[2].ToString();
                myvenom.venomType = VENOM[3].ToString();
                myvenom.killMice = VENOM[4].ToString();
                myvenom.fatalityRate = VENOM[5].ToString();
            }
            return View(myvenom);
        }
    }
}
