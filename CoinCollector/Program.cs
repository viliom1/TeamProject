using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CoinCollector
{
    class Program
    {
        static void Main(string[] args)
        {
            int points = 0;
            int wallHits = 0;
            int steps = 0;
            bool isCoin = false;
            string[,] matrix = new string[20, 20];
            int[] position = { 0, 0 };
            matrix = BoardGen(matrix);
            string[,] board = WallGenerator(matrix);
            board = CoinGen(board);
            int[] consolePos = { 1, 1 };
            
            BoardPrint(board, points, wallHits, steps);
            while (true)
            {
                ConsoleKeyInfo movement = Console.ReadKey();
                
                if (movement.Key == ConsoleKey.RightArrow)
                {
                    bool wall = IsWallRight(board,position);
                    StepsUpdate(steps);
                    steps++;
                    if (wall == true)
                    {
                        WallHitsUpdate(wallHits);
                        wallHits++;
                        //BoardPrint(board,points,wallHits,steps);
                    }
                    else
                    {
                        isCoin = IsCoinRight(board, position);
                        if (isCoin == true)
                        {
                            CoinCollected(points);
                            points += 100;
                        }

                        MoveRight(position, consolePos);

                        isCoin = false;
                    }
                }
                else if (movement.Key == ConsoleKey.LeftArrow)
                {
                    
                    bool wall = IsWallLeft(board, position);
                    StepsUpdate(steps);
                    if (wall == true)
                    {
                        WallHitsUpdate(wallHits);
                        wallHits++;
                    }
                    else
                    {
                        isCoin = IsCoinLeft(board, position);
                        if (isCoin == true)
                        {
                            CoinCollected(points);
                            points += 100;
                        }


                        MoveLeft(position,consolePos);
                        //BoardPrint(board, points, wallHits, steps);
                    }
                }
                else if (movement.Key == ConsoleKey.DownArrow)
                {
                    bool wall = IsWallDown(board, position);
                    StepsUpdate(steps);
                    steps++;
                    if (wall == true)
                    {
                        WallHitsUpdate(wallHits);
                        wallHits++;
                    }
                    
                    else
                    {
                        isCoin = IsCoinDown(board, position);
                        if (isCoin == true)
                        {
                            CoinCollected(points);
                            points += 100; ;
                        }
                    
                    
                        MoveDown(position,consolePos);
                        //BoardPrint(board, points, wallHits, steps);
                    }
                }
                else if (movement.Key == ConsoleKey.UpArrow)
                {
                    bool wall = IsWallUp(board, position);
                    StepsUpdate(steps);
                    steps++;
                    if (wall == true)
                    {
                        WallHitsUpdate(wallHits);
                        wallHits++;
                    }
                    else
                    {
                        isCoin = IsCoinUp(board, position);
                        if (isCoin == true)
                        {
                            CoinCollected(points);
                            points += 100;
                        }


                        MoveUp(position,consolePos);
                        //BoardPrint(board, points, wallHits, steps);
                    }
                }
                if (points == 500)
                {
                    break;
                }
            }
            
            int finalScore = points - wallHits * 10 - steps;
            if (finalScore >0)
            {
                Console.WriteLine("GEEEEGEEEEE!!!!!! Your score is: {0}",finalScore);
            }
            else if (finalScore<0)
            {
                Console.WriteLine("YOU SUCK !!! Your score is: {0}", finalScore);
            }
            else
            {
                Console.WriteLine("GAME OVER !!! Your score is 0");
            }

            Console.ReadKey();
        }
        static void BoardPrint(string[,] matrix,int points,int wallHits,int steps)
        {
            Console.WriteLine("Points:{0}   Hits:{1}   Steps:{2}", points, wallHits, steps);
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    Console.Write("{0,2}", matrix[i, j]);
                }
                Console.WriteLine();
            }
        }
        static string[,] CoinGen(string[,] matrix)
        {
            Random generator = new Random();
            for (int i = 0; i < 5; i++)
            {
                
                int rndRow = generator.Next(1,20);
                int rndCol = generator.Next(0, 20);
                if (matrix[rndRow, rndCol] == "$")
                {
                    matrix[rndCol, rndRow] = "$";
                }
                else
                {
                    matrix[rndRow, rndCol] = "$";
                }
            }
            return matrix;
        }
        static string[,] BoardGen(string[,] matrix)
        {
            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 20; j++)
                {
                    if (i == 0 && j == 0)
                    {
                        matrix[i, j] = "@";
                    }
                    else
                    {
                        matrix[i, j] = ".";
                    }
                }
            }
            return matrix;
        }
        static void MoveRight(int[]position, int[] consolePos)
        {
            Console.SetCursorPosition(consolePos[0], consolePos[1]);
            Console.Write(". @");
            Console.SetCursorPosition(0, 22);
            position[1]++;
            consolePos[0] += 2;
            
        }
        static void MoveLeft(int[] position, int[] consolePos)
        {
            Console.SetCursorPosition(consolePos[0]-2, consolePos[1]);
            Console.Write("@ .");
            Console.SetCursorPosition(0, 22);
            position[1]--;
            consolePos[0] -= 2;
        }
        static void MoveDown(int[] position, int[] consolePos)
        {
            Console.SetCursorPosition(consolePos[0], consolePos[1]);
            Console.Write(".");
            Console.SetCursorPosition(consolePos[0], consolePos[1]+1);
            Console.Write("@");
            Console.SetCursorPosition(0, 22);
            position[0]++;
            consolePos[1]++; ;
        }
        static void MoveUp(int[] position, int[] consolePos)
        {
            Console.SetCursorPosition(consolePos[0], consolePos[1]);
            Console.Write(".");
            Console.SetCursorPosition(consolePos[0], consolePos[1] -1);
            Console.Write("@");
            Console.SetCursorPosition(0, 22);
            position[0]--;
            consolePos[1]--; 
        }
        static bool IsCoinRight(string[,]matrix,int[] positon)
        {
            bool coin = false;
            if (matrix[positon[0],positon[1]+1] == "$")
            {
                matrix[positon[0], positon[1] + 1] = ".";
                coin = true;
            }
            
            return coin;
        }
        static bool IsCoinLeft (string[,]matrix,int[] positon)
        {
            bool coin = false;
            if (matrix[positon[0], positon[1] -1] == "$")
            {
                matrix[positon[0], positon[1] - 1] = ".";
                coin = true;
            }
            return coin;
        }
        static bool IsCoinDown (string[,]matrix,int[] positon)
        {
            bool coin = false;
            if (matrix[positon[0]+1, positon[1]] == "$")
            {
                matrix[positon[0] + 1, positon[1]] = ".";
                coin = true;
            }
            return coin;
        }
        static bool IsCoinUp (string[,]matrix,int[] positon)
        {
            bool coin = false;
            if (matrix[positon[0]-1, positon[1]] == "$")
            {
                matrix[positon[0] - 1, positon[1]] = ".";
                coin = true;
            }
            return coin;
        }
        static bool IsWallRight(string[,]matrix,int[] position)
        {
            bool wall = false;
            if (matrix.GetLength(0) == position[1]+1 || matrix[position[0],position[1]+1] == "|")
            {
                wall = true;
            }
            return wall;
        }
        static bool IsWallLeft(string[,] matrix, int[] position)
        {
            bool wall = false;
            if (0 > position[1] - 1 || matrix[position[0],position[1]-1] == "|")
            {
                wall = true;
            }
            return wall;
        }
        static bool IsWallDown(string[,]matrix,int[] position)
        {
            bool wall = false;
            if (matrix.GetLength(0) == position[0] + 1 || matrix[position[0]+1,position[1]] == "|")
            {
                wall = true;
            }
            return wall;
        }
        static bool IsWallUp(string[,]matrix,int[] position)
        {
            bool wall = false;
            if (0> position[0] - 1 || matrix[position[0]-1,position[1]] == "|")
            {
                wall = true;
            }
            return wall;
        }
        static string [,] WallGenerator (string[,] matrix)
        {
            
            Random generator = new Random();
            for (int i = 0; i < 15; i++)
            {

                int rndRow = generator.Next(1, 20);
                int rndCol = generator.Next(0, 20);
                if (matrix[rndRow, rndCol] == "$" || matrix[rndRow, rndCol] == "|")
                {
                    matrix[rndRow + 1, rndCol + 1] = "|";
                }
                else
                {
                    matrix[rndRow, rndCol] = "|";
                }
            }
            return matrix;

        }   
        static void CoinCollected (int points)
        {
            points += 100;
            Console.SetCursorPosition(7, 0);
            Console.Write(points);
        }
        static void StepsUpdate (int steps)
        {
            steps++;
            Console.SetCursorPosition(26, 0);
            Console.Write(steps);
        }
        static void WallHitsUpdate(int wallHits)
        {
            wallHits++;
            Console.SetCursorPosition(16, 0);
            Console.Write(wallHits);
            Console.SetCursorPosition(0, 22);
        }

    }
}