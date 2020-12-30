using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Visma_Restaurant_Manager.Models;
using Visma_Restaurant_Manager.ViewModels;

namespace Visma_Restaurant_Manager.Views
{
    class ConsoleUI : IView
    {
        ProductViewModel _productVM;
        MenuItemViewModel _meniuVM;
        OrderViewModel _orderVm;

        public ConsoleUI(ProductViewModel productVM, MenuItemViewModel meniuVM, OrderViewModel orderVM) {
            _productVM = productVM;
            _orderVm = orderVM;
            _meniuVM = meniuVM;
        }

        public void run() {
            Write("Restaurant Manager started!");
            bool on = true;
            while (on)
            {
                WriteInstructions();
                int choice;
                if (!ReadInt(out choice))
                {
                    WriteInvalidChoice();
                    continue;
                }
                switch (choice)
                {
                    case 1:
                        runStocks();
                        break;
                    case 2:
                        runMenuItems();
                        break;
                    case 3:
                        runOrders();
                        break;
                    case 0:
                        on = false;
                        continue;
                    default:
                        WriteInvalidChoice();
                        continue;
                }
            }
        }



       

        #region MenuItems
        private void runMenuItems() {
            Write("You are now in Menu section");
            bool on = true;
            while (on)
            {
                WriteMenuCommands();
                int choice;
                if (!ReadInt(out choice))
                {
                    WriteInvalidChoice();
                    continue;
                }
                switch (choice)
                {
                    case 1:
                        List<MenuItem> menuItems = _meniuVM.getMenuItems();
                        WriteMenuItems(menuItems);
                        break;
                    case 2:
                        addProductsIfNeeded();
                        if (GetMenuItemFromConsole())
                            WriteItemAdded();
                        else
                            WriteAddFail();
                        break;
                    case 3:
                        if (UpdateMenuItem())
                            WriteUpdateSucess();
                        else
                            WriteNumberFormatError();
                        break;
                    case 4:
                        if (RemoveMenuItem())
                            WriteRemoveSucess();
                        else
                            WriteNumberFormatError();
                        break;
                    case 5:
                        _meniuVM.WriteMenuItemsToCsv();
                        Write("CSV created!");
                        Write();
                        break;
                    case 6:
                        _meniuVM.LoadMenuItemsFromCsv();
                        Write("Products Loaded!");
                        Write();
                        break;
                    case 0:
                        on = false;
                        continue;
                    default:
                        WriteInvalidChoice();
                        continue;
                }
            }
        }

        private void WriteMenuItems(List<MenuItem> menuItems) {
            Write("Current stocks:");
            foreach (MenuItem meniuItem in menuItems)
            {
                var prods = new StringBuilder();
                foreach (int prod in meniuItem.products)
                {
                    var prodStr = prod.ToString();
                    prods.Append(prodStr + " ");
                }
                Write($"Id: {meniuItem.id,-5} Name: {meniuItem.name,-20} Products: {prods,-10}");
            }
            Write();
        }
        private void addProductsIfNeeded() {
            bool need = true;
            Write("Will this Menu Item recipe needs to add new products?");

            while (need)
            {
                Write("Press 1 for yes or 0 for no");
                int choice;
                if (!ReadInt(out choice))
                {
                    WriteInvalidChoice();
                    continue;
                }
                if(choice == 0)
                {
                    need = false;
                    continue;
                }
                else if(choice == 1)
                {
                    if (GetProductFromConsole())
                        WriteItemAdded();
                    else
                        WriteAddFail();
                }
                else
                {
                        Write("Illegal choice");
                        continue;
                }
                Write("Do you need to add another Product?");
            }
        }
        private bool GetMenuItemFromConsole() {
            try
            {
                Console.Write("Write name: ");
                string name = Console.ReadLine();

                Console.Write("Write products (integers, separated by space): ");
                string prodsStr = Console.ReadLine();
                var prodStrArray = prodsStr.Split(' ');
                List<int> prods = new List<int>();
                foreach (string pr in prodStrArray)
                {
                    prods.Add(Int32.Parse(pr));
                }
                //TODO chech if all needed products exists
                _meniuVM.addMenuItem(name, prods);
                return true;
            }

            catch (ArgumentException)
            {
                return false;
            }
            catch (FormatException e)
            {
                return false;
            }
            catch (OverflowException)
            {
                return false;
            }
        }

        private bool UpdateMenuItem() {
            try
            {
                Console.Write("Write manu item to update id: ");
                string idStr = Console.ReadLine();
                int id = Int32.Parse(idStr);
                MenuItem prod = _meniuVM.GetMenuItem(id);
                if (prod == null)
                {
                    Write("Menu item not found.");
                    return false;
                }

                Console.Write("Write new name or press enter to keep: ");
                string name = Console.ReadLine();
                if (name.Length <= 0)
                    name = prod.name;
                Console.Write("Write new products list or press enter to keep (integers, separated by space): ");
                string prodsStr = Console.ReadLine();
                List<int> prods = new List<int>();
                if (prodsStr.Length <= 0)
                    prods = prod.products;
                else
                {
                    var prodStrArray = prodsStr.Split(' ');
                    foreach (string pr in prodStrArray)
                    {
                        prods.Add(Int32.Parse(pr));
                    }

                }
                _meniuVM.UpdateMenuItem(prod.id, name, prods);
                return true;
            }
            catch (FormatException e)
            {
                return false;
            }
            catch (OverflowException)
            {
                return false;
            }

        }
        private bool RemoveMenuItem() {
            try
            {
                Console.Write("Write manu item to remove id: ");
                string idStr = Console.ReadLine();
                int id = Int32.Parse(idStr);
                _meniuVM.RemoveMenuItem(id);
                return true;
            }
            catch (FormatException e)
            {
                return false;
            }
            catch (OverflowException)
            {
                return false;
            }

        }

        #endregion
        #region products
        private void runStocks() {
            Write("You are now in Stocks section");
            bool on = true;
            while (on)
            {
                WriteProductsCommands();
                int choice;
                if (!ReadInt(out choice))
                {
                    WriteInvalidChoice();
                    continue;
                }
                switch (choice)
                {
                    case 1:
                        List<Product> products = _productVM.getProducts();
                        WriteProducts(products);
                        break;
                    case 2:
                        if (GetProductFromConsole())
                            WriteItemAdded();
                        else
                            WriteAddFail();
                        break;
                    case 3:
                        if (UpdateProduct())
                            WriteUpdateSucess();
                        else
                            WriteNumberFormatError();
                        break;
                    case 4:
                        if (RemoveProduct())
                            WriteRemoveSucess();
                        else
                            WriteNumberFormatError();
                        break;
                    case 5:
                        _productVM.WriteProductsToCsv();
                        Write("CSV created!");
                        Write();
                        break;
                    case 6:
                        _productVM.LoadProductsFromCsv();
                        Write("Products Loaded!");
                        Write();
                        break;
                    case 0:
                        on = false;
                        continue;
                    default:
                        WriteInvalidChoice();
                        continue;
                }
            }
        }

        private bool RemoveProduct() {
            try
            {
                Console.Write("Write product to remove id: ");
                string idStr = Console.ReadLine();
                int id = Int32.Parse(idStr);
                _productVM.RemoveProduct(id);
                return true;
            }
            catch (FormatException e)
            {
                return false;
            }
            catch (OverflowException)
            {
                return false;
            }

        }
        private void WriteProducts(List<Product> products) {
            Write("Current stocks:");
            foreach (Product prod in products)
            {
                Write($"Id: {prod.id,-5} Name: {prod.name,-20} Portion Count:{prod.portionCount,-10} Unit: {prod.unit,-10} Portion Size: {prod.portionSize,-10}");
            }
            Write();
        }
        private bool UpdateProduct() {
            try
            {
                Console.Write("Write product to update id: ");
                string idStr = Console.ReadLine();
                int id = Int32.Parse(idStr);
                Product prod = _productVM.GetProduct(id);
                if (prod == null)
                {
                    Write("Product not found.");
                    return false;
                }

                Console.Write("Write new name or press enter to keep: ");
                string name = Console.ReadLine();
                if (name.Length <= 0)
                    name = prod.name;
                Console.Write("Write new portion count or press enter to keep (integer): ");
                string countStr = Console.ReadLine();
                int count;
                if (countStr.Length <= 0)
                    count = prod.portionCount;
                else
                    count = Int32.Parse(countStr);
                Console.Write("Write new unit or press enter to keep: ");
                string unit = Console.ReadLine();
                if (unit.Length <= 0)
                    unit = prod.unit;
                Console.Write("Write new portion size press enter to keep (decimal): ");
                string pSizeStr = Console.ReadLine();
                double pSize;
                if (pSizeStr.Length <= 0)
                    pSize = prod.portionSize;
                else
                    pSize = Convert.ToDouble(pSizeStr);
                _productVM.UpdateProduct(prod.id, name, count, unit, pSize);
                return true;
            }
            catch (FormatException e)
            {
                return false;
            }
            catch (OverflowException)
            {
                return false;
            }

        }

        private bool GetProductFromConsole() {
            try
            {
                Console.Write("Write name: ");
                string name = Console.ReadLine();
                Console.Write("Write portion count (integer): ");
                string countStr = Console.ReadLine();
                int count = Int32.Parse(countStr);
                Console.Write("Write unit: ");
                string unit = Console.ReadLine();
                Console.Write("Write portion size (decimal): ");
                string pSizeStr = Console.ReadLine();
                double pSize = Convert.ToDouble(pSizeStr);
                _productVM.addProduct(name, count, unit, pSize);
                return true;
            }

            catch (ArgumentException)
            {
                return false;
            }
            catch (FormatException e)
            {
                return false;
            }
            catch (OverflowException)
            {
                return false;
            }
        }
        #endregion
        #region orders
        private void runOrders() {
            Write("You are now in Orders section");
            bool on = true;
            while (on)
            {
                WriteOrderCommands();
                int choice;
                if (!ReadInt(out choice))
                {
                    WriteInvalidChoice();
                    continue;
                }
                switch (choice)
                {
                    case 1:
                        List<Order> orders = _orderVm.getOrders();
                        WriteOrders(orders);
                        break;
                    case 2:
                        if (CreateOrder())
                            WriteOrderCreated();
                        else
                            WriteAddOrderFail();
                        break;
                    case 5:
                        _orderVm.WriteOrdersToCsv();
                        Write("CSV created!");
                        Write();
                        break;
                    case 6:
                        _orderVm.LoadOrdersFromCsv();
                        Write("Products Loaded!");
                        Write();
                        break;
                    case 0:
                        on = false;
                        continue;
                    default:
                        WriteInvalidChoice();
                        continue;
                }
            }
        }


        private void WriteOrders(List<Order> orders) {
            Write("Previous orders:");
            foreach (Order order in orders)
            {
                var menuItems = new StringBuilder();
                foreach (int menuItem in order.menuItems)
                {
                    var menuItemStr = menuItem.ToString();
                    menuItems.Append(menuItemStr + " ");
                }
                Write($"Id: {order.id,-5} Name: {order.dateTime,-20} Products: {menuItems,-10}");
            }
            Write();
        }

        private bool CreateOrder() {
            try
            {
                Console.Write("Write Menu Items (integers, separated by space): ");
                string menuItemsStr = Console.ReadLine();
                var menuStrArray = menuItemsStr.Split(' ');
                List<int> menuItems = new List<int>();
                foreach (string pr in menuStrArray)
                {
                    menuItems.Add(Int32.Parse(pr));
                }
                //TODO chech if all needed products exists
                return _orderVm.addOrder(menuItems);
            }

            catch (ArgumentException)
            {
                return false;
            }
            catch (FormatException e)
            {
                return false;
            }
            catch (OverflowException)
            {
                return false;
            }
        }
        #endregion
        #region helpers
        public void Write(string str = "") {
            Console.WriteLine(str);
        }

        private bool ReadInt(out int i) {
            string input = Console.ReadLine();
            return Int32.TryParse(input, out i);
        }
        #endregion
        #region messages
        private void WriteAddFail() {
            Write("Failed to add. Possibly because if wrong numbers formatting (make sure to use comma, NOT a dot for decimals");
            Write();
        }
        private void WriteAddOrderFail() {
            Write("Failed to create the order.");
            Write();
        }
        private void WriteOrderCreated() {
            Write("Order accepted!");
            Write();
        }


        private void WriteItemAdded() {
            Write("Item successfuly added!");
            Write();
        }
        private void WriteUpdateSucess() {
            Write("Item was updated");
            Write();
        }

        private void WriteRemoveSucess() {
            Write("Item, if existing, was removed");
            Write();
        }

        private void WriteInstructions() {
            Write("Press 0 to exit");
            Write("Press 1 for stocks");
            Write("Press 2 for menu items");
            Write("Press 3 for customer orders");
            Write();
        }

        private void WriteInvalidChoice() {
            Write("Invalid selection, please try again.");
            Write();
        }
        private void WriteNumberFormatError() {
            Write("Failed because of invalid or wrong format numbers");
            Write();
        }

        private void WriteProductsCommands() {
            Write("Press 0 to exit to main menu");
            Write("Press 1 to see all stocks");
            Write("Press 2 to add an item");
            Write("Press 3 to update an item");
            Write("Press 4 to remove an item");
            Write("Press 5 to write items to csv (overwrites current csv if any)");
            Write("Press 6 to load items from csv (deletes current items if any)");
            Write();
        }
        private void WriteMenuCommands() {
            Write("Press 0 to exit to main menu");
            Write("Press 1 to see all menu options");
            Write("Press 2 to add an item");
            Write("Press 3 to update an item");
            Write("Press 4 to remove an item");
            Write("Press 5 to write items to csv (overwrites current csv if any)");
            Write("Press 6 to load items from csv (deletes current items if any)");
            Write();
        }
        private void WriteOrderCommands() {
            Write("Press 0 to exit to main menu");
            Write("Press 1 to see all orders");
            Write("Press 2 to create order");
            Write("Press 5 to write items to csv (overwrites current csv if any)");
            Write("Press 6 to load items from csv (deletes current items if any)");
            Write();
        }

        #endregion
    }
}
