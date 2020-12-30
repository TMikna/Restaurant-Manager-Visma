using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Visma_Restaurant_Manager.Models;

namespace Visma_Restaurant_Manager.DB
{
    class FileImput
    {
        const string productsFilePath = "Products.csv";
        const string menuItemsFilePath = "MenuItems.csv";
        const string ordersFilePath = "Orders.csv";

        public void WriteProductsToCsv(List<Product> products) {

            var csv = new StringBuilder();
            foreach (Product prod in products)
            {
                var idStr = prod.id.ToString();
                var pCountStr = prod.portionCount.ToString();
                var pSizeStr = prod.portionSize.ToString();

                var line = string.Format("{0},{1},{2},{3},{4}", idStr, prod.name, pCountStr, prod.unit, pSizeStr);
                csv.AppendLine(line);
            }
            File.WriteAllText(productsFilePath, csv.ToString());
        }

        //TODO wrong reading of defimals, because separated with comma, so read only integer part
        public List<Product> ReadProductsFromCsv(string path = productsFilePath) {

            using (var reader = new StreamReader(path))
            {
                List<Product> products = new List<Product>();

                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');

                    string idStr = values[0];
                    int id = Int32.Parse(idStr);
                    string name = values[1];
                    string countStr = values[2];
                    int count = Int32.Parse(countStr);
                    string unit = values[3];
                    string pSizeStr = values[4];
                    double pSize = Convert.ToDouble(pSizeStr);

                    products.Add(new Product(id, name, count, unit, pSize));
                }
                return products;
            }
        }


        public void WriteMenuItemsToCsv(List<MenuItem> menuItems) {

            var csv = new StringBuilder();
            foreach (MenuItem menuItem in menuItems)
            {
                var idStr = menuItem.id.ToString();
                StringBuilder prods = new StringBuilder();
                foreach (int prodInt in menuItem.products)
                {
                    prods.Append(prodInt).Append(" ");
                }

                var line = string.Format("{0},{1},{2}", idStr, menuItem.name, prods);
                csv.AppendLine(line);
            }
            File.WriteAllText(menuItemsFilePath, csv.ToString());
        }

        //TODO wrong reading of defimals, because separated with comma, so read only integer part
        public List<MenuItem> ReadMenuItemsFromCsv(string path = menuItemsFilePath) {

            using (var reader = new StreamReader(path))
            {
                List<MenuItem> menuItems = new List<MenuItem>();

                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');

                    string idStr = values[0];
                    int id = Int32.Parse(idStr);
                    string name = values[1];
                    string prodsStr = values[2];
                    List<int> prods = prodsStr.Trim().Split(' ').Select(Int32.Parse).ToList();

                    menuItems.Add(new MenuItem(id, name, prods));
                }
                return menuItems;
            }
        }

        public void WriteOrdersToCsv(List<Order> orders) {

            var csv = new StringBuilder();
            foreach (Order order in orders)
            {
                var idStr = order.id.ToString();
                string dateTime = order.dateTime.ToString();
                StringBuilder prods = new StringBuilder();
                foreach (int menuInt in order.menuItems)
                {
                    prods.Append(menuInt).Append(" ");
                }

                var line = string.Format("{0},{1},{2}", idStr, dateTime, prods);
                csv.AppendLine(line);
            }
            File.WriteAllText(ordersFilePath, csv.ToString());
        }

        //TODO wrong reading of defimals, because separated with comma, so read only integer part
        public List<Order> ReadOrdersFromCsv(string path = ordersFilePath) {

            using (var reader = new StreamReader(path))
            {
                List<Order> orders = new List<Order>();

                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');

                    string idStr = values[0];
                    int id = Int32.Parse(idStr);
                    string dateTimeStr = values[1];
                    DateTime dateTime = DateTime.Parse(dateTimeStr);
                    string menuStr = values[2];
                    List<int> menuItems = menuStr.Trim().Split(' ').Select(Int32.Parse).ToList();

                    orders.Add(new Order(id, dateTime, menuItems));
                }
                return orders;
            }
        }
    }
}
