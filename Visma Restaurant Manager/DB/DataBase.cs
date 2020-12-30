using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Visma_Restaurant_Manager.Models;

namespace Visma_Restaurant_Manager.DB
{
    class DataBase {
        private List<Product> _products;
        private List<MenuItem> _menuItems;
        private List<Order> _orders;

        public DataBase() {
            _products = new List<Product>();
            _menuItems = new List<MenuItem>();
            _orders = new List<Order>();
        }

        public List<Product> products {
            get {
                return _products;
            }
        }
        public void addProduct(Product product) {
            _products.Add(product);
        }
        public void addProducts(IEnumerable<Product> products) {
            _products.AddRange(products);
        }
        public void loadProducts(IEnumerable<Product> products) {
            _products.Clear();
            _products.AddRange(products);
        }
        public Product getProduct(int id) {
            return _products.Find(item => item.id == id);
        }
        public void removeProduct(int id) {
            _products.RemoveAll(item => item.id == id);
        }

        public List<MenuItem> menuItems {
            get {
                return _menuItems;
            }
        }
        public void addMenuItem(MenuItem menuItem) {
            _menuItems.Add(menuItem);
        }
        public void addMenuItems(IEnumerable<MenuItem> menuItems) {
            _menuItems.AddRange(menuItems);
        }
        public void loadMenuItems(IEnumerable<MenuItem> menuItems) {
            _menuItems.Clear();
            _menuItems.AddRange(menuItems);
        }
        public MenuItem GetMenuItem(int id) {
            return _menuItems.Find(item => item.id == id);
        }
        public void removeMenuItem(int id) {
            _menuItems.RemoveAll(item => item.id == id);    
        }

        public List<Order> orders {
            get {
                return _orders;
            }
        }
        public void addOrder(Order order) {
            _orders.Add(order);
        }
        public void addOrders(IEnumerable<Order> orders) {
            _orders.AddRange(orders);
        }
        public void loadOrders(IEnumerable<Order> orders) {
            _orders.Clear();
            _orders.AddRange(orders);
        }
        public Order getOrder(int id) {
            return _orders.Find(item => item.id == id);
        }
    }
}
