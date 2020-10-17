using System.Threading;

namespace pizzeria
{
    public class Plate
    {
        public int size;
        public int units;

        public Plate(int size)
        {
            this.size = size;
            this.units = 0;
        }

        public void fill()
        {
            this.units = this.size;
        }

        public void eat()
        {
            this.units--;
        }
    }
}