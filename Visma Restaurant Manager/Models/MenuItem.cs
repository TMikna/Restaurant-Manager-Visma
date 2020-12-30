using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Visma_Restaurant_Manager.Models
{
    class MenuItem {
        private static int nextId = 1;
        private int _id;
        private string _name;
        private List<int> _products;

        public int id {
            get => _id;
            set => _id = value;
        }

        public string name {
            get => _name;
            set => _name = value;
        }

        public List<int> products {
            get => _products;
            set => _products = value;
        }

        public MenuItem(int id, string name, List<int> products) {
            _id = id;
            _name = name;
            _products = products;
        }

        public MenuItem(string name, List<int> products) {
            _id = nextId;
            nextId++;
            _name = name;
            _products = products;
        }

    }
}
