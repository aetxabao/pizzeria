using System.Threading;

namespace pizzeria
{
    public class Waiter
    {
        private Log log;
        public int pizzas_x_table = 3;
        public int time_for_serving_a_pizza = 400;
        private Thread _thread;
        public Plate plate;
        private int wakeups = 0;
        public Waiter(Log log, int pizzas_x_table, int time_for_serving_a_pizza, Plate plate)
        {
            this.log = log;
            this.pizzas_x_table = pizzas_x_table;
            this.time_for_serving_a_pizza = time_for_serving_a_pizza;
            this.plate = plate;
        }

        public void Start()
        {
            this._thread = new Thread(() => this._Serve());
            this._thread.Start();
        }

        private void _Serve()
        {
            int i = 0;
            lock (plate)
            {
                while (i < pizzas_x_table)
                {
                    if (plate.units == 0)
                    {
                        log.Trace("Wtr\t!\tHa sido avisado");
                        Thread.Sleep(time_for_serving_a_pizza);
                        plate.fill();
                        i++;
                        log.Trace("Wtr\t" + i + "\tHa servido la pizza");
                        Monitor.PulseAll(plate);
                        if (i < pizzas_x_table)
                        {
                            Monitor.Wait(plate);
                        }
                    }
                    else
                    {
                        log.Trace("Wtr\t?\tHa sido molestado");
                        wakeups++;
                        Monitor.Wait(plate);
                    }
                }
            }
        }

        public void Finish()
        {
            this._thread.Join();
            log.Info("Wtr\t***\tHa servido " + pizzas_x_table + " pizzas y le han molestado " + wakeups + " veces");
        }

    }
}