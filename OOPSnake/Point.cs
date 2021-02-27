using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPSnake
{
	class Point
	{

		public int x;
		public int y;
		public char sym;
		public string color = "";

		public Point()
		{
		}

		public Point(int x, int y, char sym)
		{
			this.x = x;
			this.y = y;
			this.sym = sym;
		}

		public Point(Point p)
		{
			x = p.x;
			y = p.y;
			sym = p.sym;
		}

		public void Move(int offset, Direction direction)
		{
			if (direction == Direction.RIGHT)
			{
				x = x + offset;
			}
			else if (direction == Direction.LEFT)
			{
				x = x - offset;
			}
			else if (direction == Direction.UP)
			{
				y = y - offset;
			}
			else if (direction == Direction.DOWN)
			{
				y = y + offset;
			}
			/*else if (direction == Direction.PAUSE)
			{
				y = y;
				x = x;
			}*/
		}

		public bool IsHit(Point p)
		{
			return p.x == this.x && p.y == this.y;
		}

		public void Draw(string _color)//с помощью переменной _color сможем раскрашивать еду разными цветами
		{
			color = _color;
			Console.ForegroundColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), color);
			Console.SetCursorPosition(x, y);
			Console.Write(sym);
			Console.ForegroundColor = ConsoleColor.Gray;//после того, как нарисуем цветную еду снова задаём стандартный серый цвет, чтобы все остальные символы на экране остались стандартного цвета
		}

		public void Clear()
		{
			sym = ' ';
			Draw("Black");
		}

		public override string ToString()
		{
			return x + ", " + y + ", " + sym;
		}
	}
}
