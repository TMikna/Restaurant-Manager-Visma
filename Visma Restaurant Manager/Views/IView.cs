﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Visma_Restaurant_Manager.Views
{
    interface IView
    {
        void Write(string str = "");
    }
}
