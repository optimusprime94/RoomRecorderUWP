﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Input;
using Windows.UI.Xaml.Input;

namespace CodebustersAppWMU3.Models
{
    class SurfaceOptions
    {
        public static string SurfaceSide(int side)
        {
            switch (side)
            {
                case 0:
                    return "Front";
                case 1:
                    return "Left";
                case 2:
                    return "Back";
                case 3:
                    return "Right";
                case 4:
                    return "Bottom";
                case 5:
                    return "Top";
                default:
                    return "";
            }
        }

    }
}
