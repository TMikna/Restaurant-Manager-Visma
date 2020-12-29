using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Visma_Restaurant_Manager.Models
{
    class Order
    {
        public int id { get; set; }
        public DateTime _dateTime { get; set; }
        public List<int> products { get; set; }

    }
}
