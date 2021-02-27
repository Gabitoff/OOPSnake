using System;
using System.Collections.Generic;
using System.IO;

namespace OOPSnake
{
    public class Score
    {
        Params settings = new Params();
        private static string pathToRecordFile;
        private static string pathToResultsFile;
        private int currentPoints = 0;
        string uName = "";
        public Score(string _pathToResources)
        {
            pathToRecordFile = _pathToResources + "record.txt";
            pathToResultsFile = _pathToResources + "results.txt";

            WriteText("Время игры", 89, 0);
            WriteText("Счёт", 92, 3);

            ShowCurrentPoints();

            WriteText("Рекорд:", 89, 6);

            WriteText(DisplayBestResult(), 98, 6);

            WriteText("-----------------------------", 80, 7);

            WriteText("5 последних результатов:", 82, 8);

            ShowLastFiveResults();
        }

        //Тут берём из файла record.txt рекорд и имя игрока, который его установил
        public string DisplayBestResult()
        {
            // Read from file
            StreamReader streamReader = new StreamReader(pathToRecordFile);
            string record = streamReader.ReadToEnd();
            streamReader.Close();

            return record;
        }

        //Тут берём из файла record.txt только сумму очков рекорда, чтобы сравнить с набранным количеством очков в текущей игре 
        public string GetBestResult()
        {
            // Read from file
            StreamReader streamReader = new StreamReader(pathToRecordFile);
            string record = streamReader.ReadToEnd();
            streamReader.Close();
            string[] rec = record.Split(',');
            string intrecord = rec[0];
            if (intrecord == "")
            {
                intrecord = "0";
            }

            return intrecord;
        }

        public string userName() // Спрашиваем имя игрока и через переменную uName будем записывать его имя в файлы с рекордом/результатами ниже
        {
            int xOffset = 25;
            int yOffset = 15;
            Console.SetCursorPosition(xOffset, yOffset++);
            Console.Write("Введите своё имя: ", xOffset, yOffset++);
            string usName = Console.ReadLine();
            //если вводится имя короче 3х символов, выводится ошибка и функция запускается заново до тех пор, пока не будет введено имя длиннее 3х символов
            //если вводится имя длиной в 3 или больше символов, сохраняем результат и уведомляем об этом пользователя
            try
            { 
                if (usName.Length < 3)
                {
                    throw new FormatException();
                }
                else
                {
                    uName = usName;
                    Console.SetCursorPosition(xOffset, yOffset++);
                    Console.WriteLine("Ваш результат успешно сохранён в базе данных!", xOffset, yOffset++);
                }
            }
            catch (FormatException)
            {
                Console.SetCursorPosition(xOffset, yOffset++);
                Console.WriteLine("Имя должно быть длиннее 3х символов", xOffset, yOffset++);
                userName();
            }
            return uName;
        }
        public void WriteBestResult()
        {
            if (currentPoints > Convert.ToInt32(GetBestResult()))
            {
                // Write in file
                StreamWriter streamWriter = new StreamWriter(pathToRecordFile);
                streamWriter.Write(Convert.ToString(currentPoints) + ", " + uName);
                streamWriter.Close();

                // Write in file
                StreamWriter streamWriter1 = new StreamWriter(pathToResultsFile, true);
                streamWriter1.WriteLine(Convert.ToString(currentPoints) + ", " + uName);
                streamWriter1.Close();
            }
            else
            {
                // Write in file
                StreamWriter streamWriter = new StreamWriter(pathToResultsFile, true);
                streamWriter.WriteLine(Convert.ToString(currentPoints) + ", " + uName);
                streamWriter.Close();
            }
        }

        public void UpCurrentPoints(int _extra)//extra принимает значение 1 или 2, при значении 1 начисляются стандартные 10 очков, при значении 2 случайно от 20 до 90
        {
            if (_extra == 1)
            {
                currentPoints += 10;
            }
            else if (_extra == 2)
            {
                Random rnd = new Random();
                int extrapoints = rnd.Next(2, 9) * 10;
                currentPoints += extrapoints;
            }
        }

        public void ShowCurrentPoints()
        {
            //Console.ForegroundColor = ConsoleColor.Red;
            if (currentPoints == 0)
            {
                Console.SetCursorPosition(94, 4);
            }
            else if (currentPoints > 0)
            {
                Console.SetCursorPosition(93, 4);
            }

            Console.WriteLine(currentPoints.ToString());
        }
        public int currentFood()
        {
            int currentFood = currentPoints;
            return currentFood;
        }
        public void ShowLastFiveResults()
        {
            List<string> res = new List<string>();
            string line;
            // Read file
            StreamReader streamReader = new StreamReader(pathToResultsFile);
            while ((line = streamReader.ReadLine()) != null)
            {
                res.Add(line);
            }

            streamReader.Close();


            // Вывод последних 5 результатов
            for (int i = res.Count - 1, j = 1; i > res.Count - 6; i--, j++)
            {
                Console.SetCursorPosition(80, 9 + j);
                Console.WriteLine(j + ") " + res[i]);
            }
        }

        public void WriteGameOver()
        {
            int xOffset = 25;
            int yOffset = 8;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(xOffset, yOffset++);
            WriteText("============================", xOffset, yOffset++);
            WriteText("G A M E    O V E R", xOffset + 5, yOffset++);
            WriteText("============================", xOffset, yOffset++);
        }

        static void WriteText(String text, int xOffset, int yOffset)
        {
            Console.SetCursorPosition(xOffset, yOffset);
            Console.WriteLine(text);
        }
    }
}