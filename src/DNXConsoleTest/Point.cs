using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DNXConsoleTest{
    public class Point{
        public int X { get; set; }
        public int Y { get; set; }

        public Point Move(Point Step) {
            Point result = (Point)this.MemberwiseClone();
            result.X += Step.X;
            result.Y += Step.Y;
            return result;      
        }
    }
}
