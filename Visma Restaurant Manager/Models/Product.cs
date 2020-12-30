using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Visma_Restaurant_Manager.Models
{
    class Product
    {
        private static int nextId = 1;
        private int _id;
        private string _name;
        private int _portionCount;
        private string _unit;     // Or enum would be better?
        private double _portionSize;

        public Product(int id, string name, int portionCount, string unit, double portionSize) {
            _id = id;     // not good allows duplicate ID's, but needed for reading csv
            _name = name;
            _portionCount = portionCount;
            _unit = unit;
            _portionSize = portionSize;
        }

        public Product(string name, int portionCount, string unit, double portionSize) {
            _id = nextId;
            nextId++;
            _name = name;
            _portionCount = portionCount;
            _unit = unit;
            _portionSize = portionSize;
        }

        public int id {
            get {
                return _id;
            }
            set {
                _id = value;
            }
        }

        public string name {
            get {
                return _name;
            }
            set {
                _name = value;
            }
        }

        public int portionCount {
            get {
                return _portionCount;
            }
            set {
                _portionCount = value;
            }
        }

        public string unit {
            get {
                return _unit;
            }
            set {
                _unit = value;
            }
        }

        public double portionSize {
            get {
                return _portionSize;
            }
            set {
                _portionSize = value;
            }
        }
    }
}
