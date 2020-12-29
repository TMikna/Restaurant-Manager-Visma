using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Visma_Restaurant_Manager.Models
{
    class MenuItem
    {
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

    }
}
