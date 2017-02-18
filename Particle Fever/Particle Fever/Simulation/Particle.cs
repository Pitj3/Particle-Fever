using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Particle_Fever
{

    public struct Particle
    {
        public int x, y, vx, vy;
        public int oldx, oldy;
        public uint color;
        public int life;
    }
}
