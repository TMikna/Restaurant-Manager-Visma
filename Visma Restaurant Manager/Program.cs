using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Visma_Restaurant_Manager.DB;
using Visma_Restaurant_Manager.ViewModels;
using Visma_Restaurant_Manager.Views;

namespace Visma_Restaurant_Manager
{
    class Program
    {
        public static DataBase DataBase { get; private set; }

        static void Main(string[] args)
        {
            DataBase database = new DataBase();
            FileImput csvReader = new FileImput();
            ProductViewModel productVM = new ProductViewModel(database, csvReader);
            OrderViewModel orderVm = new OrderViewModel(database, csvReader);
            MenuItemViewModel meniuVM = new MenuItemViewModel(database, csvReader);

            ConsoleUI consoleUI = new ConsoleUI(productVM, meniuVM, orderVm);

            productVM.consoleUI = consoleUI;
            orderVm.consoleUI = consoleUI;
            meniuVM.consoleUI = consoleUI;

            consoleUI.run();
        }
    }
}
