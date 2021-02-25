using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace OOPSnake
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.SetWindowSize(120, 25);

			Walls walls = new Walls(80, 25);
			walls.Draw();


			// Отрисовка точек			
			Point p = new Point(4, 5, '*');
			Snake snake = new Snake(p, 4, Direction.RIGHT);
			snake.Draw();

			FoodCreator foodCreator = new FoodCreator(80, 25, '$');
			Point food = foodCreator.CreateFood();
			FoodCreator foodCreator2 = new FoodCreator(80, 25, 'R');//второй тип еды, который даёт случайное количество очков от 20 до 90
			Point food2 = foodCreator2.CreateFood();

			food.Draw();

			//пути и настройки
			Params settings = new Params();

			//Audio player

			Sounds sound = new Sounds(settings.GetResourceFolder());
			sound.Play();
			Sounds sound1 = new Sounds(settings.GetResourceFolder()); //eat
																	  //Score
			Score score = new Score(settings.GetResourceFolder());

			//Timer
			TimeCounter timer = new TimeCounter();
			timer.Counter(); //вызываем счётчик времени который сразу же начинает считать продолжительность игры

			while (true)
			{
				if (walls.IsHit(snake) || snake.IsHitTail())
				{
					break;
				}
				if (snake.Eat(food))
				{
					food = foodCreator.CreateFood();
					food2 = foodCreator2.CreateFood();
					sound1.PlayEat();
					int check = score.currentFood();
					int n;
					if (check > 0 && ((check + 10) % 100) == 0)//когда количество очков заканчивается на 100, генерируется еда R
					{
						food2.Draw();
						n = 1;

					}
					else if (check > 0 && check % 100 == 0)//когда съедаем еду R, генерируется обычная еда, но начисляется рандомное количество очков 20-90
					{
						food.Draw();
						n = 2;
					}
					else
					{
						food.Draw();
						n = 1;
					}
					//food.Draw();
					score.UpCurrentPoints(n);//при помощи n определяем, начисляются ли стандартные 10 очков либо рандомные 20-90
					score.ShowCurrentPoints();
				}
				else
				{
					snake.Move();
				}

				Thread.Sleep(100);
				if (Console.KeyAvailable)
				{
					ConsoleKeyInfo key = Console.ReadKey();
					snake.HandleKey(key.Key);
				}
			}
			sound.Play("gameover"); // проигрываем звук Game Over
			timer.CounterStatus(false); //останавливаем счётчик времени
			score.WriteGameOver();
			score.userName(); // спрашиваем имя игрока
			score.WriteBestResult();
			Console.ReadLine();
		}

		/*static void WriteText( String text, int xOffset, int yOffset )
		{
			Console.SetCursorPosition( xOffset, yOffset );
			Console.WriteLine( text );
		}*/

	}
}
		