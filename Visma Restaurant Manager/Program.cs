using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Visma_Restaurant_Manager.DB;

namespace Visma_Restaurant_Manager
{
    class Program
    {
        static void Main(string[] args)
        {
            DataBase db = new DataBase();

            db.addTest(1);
            db.addTest(2);
            db.addTest(3);

            db.test.Add(4);

            foreach(int el in db.test) {
                Console.WriteLine(el);
            }

            Console.ReadLine();
        }
    }
}
