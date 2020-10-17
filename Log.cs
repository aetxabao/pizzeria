using System;

namespace pizzeria
{
    public class Log
    {

        private long inicio;
        private int log_level;
        public Log(int log_level)
        {
            this.log_level = log_level;
            inicio = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
        }

        public void Info(string txt)
        {
            if (log_level >= 1) Msg(txt);
        }
        public void Trace(string txt)
        {
            if (log_level >= 2) Msg(txt);
        }
        private void Msg(string txt)
        {
            long when = (DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() - inicio) / 100;
            Console.WriteLine("{0}   {1}", when.ToString().PadLeft(6), txt);
        }
    }
}