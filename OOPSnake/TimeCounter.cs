using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Diagnostics;

namespace OOPSnake
{
    public class TimeCounter
    {
        static int seconds = 0;
        string time = "";
        public static bool status = true;// по-умолчанию счётчик сразу же включен, т.к. наша змейка при включении игры сразу же двигается

        //получаем значение bool  из Program.cs и останавливаем счётчик времени когда игра закончена, значение статус идёт в OnTimedEvent для остановки счётчика
        //этот же bool используется в Snake.cs и при включении паузы останавливает таймер
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
            aTimer.Enabled = status; //останавливаем таймер при проигрыше или при включении паузы и запускаем снова, если игра продолжилась после паузы
        }

        public void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            if (status == true) //секунды "бегут" только если таймер включен, во время паузы время так же останавливается
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
