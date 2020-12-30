using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Visma_Restaurant_Manager.Models
{
    class Order
    {
        private static int nextId = 1;
        public int id { get; set; }
        public DateTime dateTime { get; set; }
        public List<int> menuItems { get; set; }

        public Order(int id, DateTime dateTime, List<int> menuItems) {
            this.id = id;
            this.dateTime = dateTime;
            this.menuItems = menuItems;
        }
        public Order(DateTime dateTime, List<int> menuItems) {
            this.id = nextId++;
            this.dateTime = dateTime;
            this.menuItems = menuItems;
        }
    }
}
