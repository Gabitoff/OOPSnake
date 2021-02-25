using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace OOPSnake
{
    public class TimeCounter
    {
        static int seconds = 0;
        string time = "";
        bool status = true;

        //получаем значение bool  из Program.cs и останавливаем счётчик времени когда игра закончена, значение статус идёт в OnTimedEvent для остановки счётчика
        public bool CounterStatus(bool _status)
        {
            status = _status;
            return status;
        }
        public void Counter()
        {
            System.Timers.Timer aTimer = new System.Timers.Timer();
            aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            aTimer.Interval = 1000;
            aTimer.Enabled = true;
        }

        public void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            ((System.Timers.Timer)source).Enabled = status;
            if (status == true)
            {
                seconds++;
                if (seconds < 10)
                {
                    time = "0" + Convert.ToString(seconds);
                }
                else
                {
                    time = Convert.ToString(seconds);
                }
                WriteText(time, 93, 1);
            }
        }
        static void WriteText(String text, int xOffset, int yOffset)
        {
            Console.SetCursorPosition(xOffset, yOffset);
            Console.WriteLine(text);
        }
    }
}
