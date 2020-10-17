using System;
using System.Collections.Generic;

namespace pizzeria
{
    class Pizzeria
    {
        static void Main(string[] args)
        {
            Pizzeria pizzeria = new Pizzeria();
            // Para que no se bloquee:
            // porciones servidas = porciones comidas
            // slices_x_pizza * pizzas_x_table = eaters_x_table * slices_to_be_full
            // Ej. 8 * 3 = 6 * 4
            // Ej. 20 * 90 = 3 * 600
            pizzeria.run(1, 8, 6, 4, 600, 3, 500);
            pizzeria.run(1, 8, 6, 4, 600, 3, 500);
            pizzeria.run(2, 8, 6, 4, 600, 3, 500);
            pizzeria.run(1, 20, 3, 600, 3, 90, 5);
        }
        public void run(int log_level, int slices_x_pizza, int eaters_x_table,
                        int slices_to_be_full, int time_for_eating_a_slice,
                        int pizzas_x_table, int time_for_serving_a_pizza)
        {
            Log log = new Log(log_level);
            Console.WriteLine("INICIO");
            Plate plate = new Plate(slices_x_pizza);
            Waiter waiter = new Waiter(log, pizzas_x_table, time_for_serving_a_pizza, plate);
            var table = new List<Eater>();
            Console.WriteLine("Se sientan los comensales.");
            for (int i = 1; i <= eaters_x_table; i++)
            {
                table.Add(new Eater(log, i, slices_to_be_full, time_for_eating_a_slice, plate));
            }
            Console.WriteLine("El camarero empieza a servir.");
            waiter.Start();
            Console.WriteLine("Los comensales empiezan a comer.");
            foreach (Eater eater in table)
            {
                eater.Start();
            }
            waiter.Finish();
            Console.WriteLine("El camarero ha terminado.");
            foreach (Eater eater in table)
            {
                eater.Finish();
            }
            Console.WriteLine("Los comensales han terminado de comer.");
            Console.WriteLine("FIN");
        }
    }
}
