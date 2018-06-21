using System;
using System.Collections;
using System.Collections.Generic;
using static System.Console;
using System.Diagnostics;

namespace Snake
{


    class Element{
        
        private int xPos;
        private int yPos;
        private string type;

        public Element(int x, int y, string type){
            xPos = x;
            yPos = y;
            this.type = type;
        }

        public int GetX() { return xPos; }
        public int GetY() { return yPos; }
        public string GetElementType() { return type; }
        public void SetX(int x) { xPos = x; }
        public void SetY(int y) { yPos = y; }
        public void SetElementType(string type) { this.type = type; }
    }

    class MainClass
    {
        public static void Main(string[] args)
        {
            
            int borderWidth = 40;
            int borderHeigth = 20;
            int speed = 300;
            int acceleration = 5;
        
            Element head = new Element(borderWidth / 2, borderHeigth / 2, "head");
            List<Element> body = new List<Element>();
            Random rand = new Random();
            Element food = new Element(rand.Next(1,borderWidth-1),rand.Next(2,borderHeigth-1),"food");

            string movement = "Right";

            int size = 3;


            while(true){
                
                Clear();
                WriteLine("Hello Snek!   Score: " + (size-3));
                DrawBorder(borderWidth, borderHeigth);
                DrawElement(food);
                DrawElement(head);

                if(head.GetX() == food.GetX() && head.GetY() == food.GetY()){
                    size++;
                    food = new Element(rand.Next(1, borderWidth - 1), rand.Next(2, borderHeigth - 1), "food");
                    speed = speed - acceleration;
                }

                Boolean dead = false;
                if(head.GetX() == 0 || head.GetX() == borderWidth || head.GetY() == 1 || head.GetY() == borderHeigth-1){
                    dead = true;
                }
                for (int i = 0; i < body.Count; i++){
                    DrawElement(body[i]);
                    if(head.GetX() == body[i].GetX() && head.GetY() == body[i].GetY()){
                        dead = true;
                    }
                }
                if(dead){
                    break;
                }




                Stopwatch timer = Stopwatch.StartNew();
                while (timer.ElapsedMilliseconds <= speed)
                {
                    movement = movingDirection(movement);
                }

                body.Add(new Element(head.GetX(), head.GetY(), "body"));
                if (body.Count > size)
                    body.RemoveAt(0);

                ChangePosition(head, movement);


            }
            Clear();
            SetCursorPosition(0, 0);
            WriteLine("Snek You Died!!!");
            ReadKey();


        }




        static void ChangePosition(Element element, string direction){
            switch (direction)
            {
                case "Up":
                    element.SetY(element.GetY() - 1);
                    break;
                case "Down":
                    element.SetY(element.GetY() + 1);
                    break;
                case "Left":
                    element.SetX(element.GetX() - 1);
                    break;
                case "Right":
                    element.SetX(element.GetX() + 1);
                    break;
            }
        }

        static string movingDirection(string direction){
            if(KeyAvailable){
                
                ConsoleKey key = ReadKey(true).Key;

                if (key == ConsoleKey.UpArrow && direction != "Down")
                {
                    direction = "Up";
                }
                else if (key == ConsoleKey.DownArrow && direction != "Up")
                {
                    direction = "Down";
                }
                else if (key == ConsoleKey.LeftArrow && direction != "Right")
                {
                    direction = "Left";
                }
                else if (key == ConsoleKey.RightArrow && direction != "Left")
                {
                    direction = "Right";
                }

            }
            return direction;
        }

        static void DrawElement(Element element){
            SetCursorPosition(element.GetX(), element.GetY());
            if(element.GetElementType() == "head"){
                Write('O');
            }else if(element.GetElementType() == "food"){
                Write('*');
            }else{
                Write('o');
            }

            SetCursorPosition(0, 20);
        }

        static void DrawBorder(int width, int heigth){
            for (int i = 0; i< width; i++){
                SetCursorPosition(i, 1);
                Write('#');
                SetCursorPosition(i, heigth - 1);
                Write('#');
            }
            for (int i = 1; i < heigth; i++){
                SetCursorPosition(0, i);
                Write('#');
                SetCursorPosition(width, i);
                Write('#');
            }
        }
    }
}
