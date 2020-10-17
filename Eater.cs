using System.Threading;

namespace pizzeria
{
    public class Eater
    {
        private Log log;
        public int slices_to_be_full = 4;
        public int time_for_eating_a_slice = 600;
        private Thread _thread;
        public int id;
        public Plate plate;
        private int complaints = 0;
        public Eater(Log log, int id, int slices_to_be_full, int time_for_eating_a_slice, Plate plate)
        {
            this.log = log;
            this.id = id;
            this.plate = plate;
            this.slices_to_be_full = slices_to_be_full;
            this.time_for_eating_a_slice = time_for_eating_a_slice;
        }

        public void Start()
        {
            this._thread = new Thread(() => this._Eat());
            this._thread.Start();
        }

        private void _Eat()
        {
            bool b = false;
            int i = 0;
            while (i < slices_to_be_full)
            {
                lock (plate)
                {
                    if (plate.units > 0)
                    {
                        plate.eat();
                        log.Trace("E_" + id + "\tA\tHa cogido una porción, quedan " + plate.units);
                        Monitor.PulseAll(plate);
                        i++;
                        b = true;
                    }
                    else
                    {
                        log.Trace("E_" + id + "\t-\tQuiere comer");
                        complaints++;
                        Monitor.Wait(plate);
                        b = false;
                    }
                }
                // Sólo si ha cogido una porción simulamos que se la come.
                if (b == true) Thread.Sleep(time_for_eating_a_slice);
            }
        }

        public void Finish()
        {
            this._thread.Join();
            log.Info("E_" + id + "\tx\tHa comido " + slices_to_be_full + " pedazos de pizza y se ha quejado " + complaints + " veces");
        }

    }
}