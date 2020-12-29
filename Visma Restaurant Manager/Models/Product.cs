using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Visma_Restaurant_Manager.Models
{
    class Product
    {
        private int _id;
        private string _name;
        private int _portionCount;
        private string _unit;     // Or enum would be better?
        private double _portionSize;

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
