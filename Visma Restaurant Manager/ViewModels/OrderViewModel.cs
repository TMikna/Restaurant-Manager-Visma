using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Visma_Restaurant_Manager.DB;
using Visma_Restaurant_Manager.Models;
using Visma_Restaurant_Manager.Views;

namespace Visma_Restaurant_Manager.ViewModels
{
    class OrderViewModel
    {
        DataBase _dataBase;
        FileImput _fIO;

        private IView _consoleUI;
        public IView consoleUI {
            set { _consoleUI = value; }
        }

        public OrderViewModel(DataBase dataBase, FileImput fileImput) {
            _dataBase = dataBase;
            _fIO = fileImput;
        }

        public List<Order> getOrders() {
            return _dataBase.orders;     // Not so good since UI gets acess to products
        }
        public bool addOrder(List<int> menuItemIds) {
            List<MenuItem> menuItems = new List<MenuItem>();
            List<Product> products = new List<Product>();

            // collect needed products and menu items
            foreach (int item in menuItemIds)
            {
                MenuItem menuItem = _dataBase.GetMenuItem(item);
                if (menuItem == null)
                {
                    _consoleUI.Write("Order contains non-existing menu item.");
                    return false;
                }
                foreach(int prod in menuItem.products)
                {
                    Product product = _dataBase.getProduct(prod);
                    if (product == null)
                    {
                        _consoleUI.Write("Order contains menu item which has non-exisiting product.");
                        return false;
                    }
                    products.Add(product);
                }
            }
            // update products amounts
            for (int i = 0; i < products.Count; i++)
            {
                if (products[i].portionCount <= 0)
                {
                    _consoleUI.Write("Order contains menu item which lacks products.");
                    for (int j = i; j > 0; --j)
                        products[i].portionCount++; //Cancel the order
                    return false;
                }

                products[i].portionCount--;
            }

            _dataBase.addOrder(new Order(DateTime.Now, menuItemIds));
            return true;
        }

        public void WriteOrdersToCsv() {
            _fIO.WriteOrdersToCsv(_dataBase.orders);
        }
        public void LoadOrdersFromCsv() {
            _dataBase.loadOrders(_fIO.ReadOrdersFromCsv());
        }
    }
}
