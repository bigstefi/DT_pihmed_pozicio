﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DT_pihmed_pozicio.Data
{
    internal class CartesianCoordinate // could be Point, but that is marked sealed (cannot inherit, so wrapping it)
    {
        private Point _point;

        public double X { get { return _point.X; } }
        public double Y { get { return _point.Y; } }

        public CartesianCoordinate(double x, double y)
        {
            _point = new Point(x, y);
        }
    }
}
