using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameModel
{
    public class Pad
    {
        private int _y;

        public Pad(int y)
        {
            Y = y;
        }

        public int Y { get => _y; set => _y = value; }
    }
}
