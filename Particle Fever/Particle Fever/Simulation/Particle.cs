using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Particle_Fever
{
    public enum ParticleType
    {
        PARTICLE_SAND = 0
    }

    public struct Particle
    {
        int x, y, vx, vy;
        int oldx, oldy;
        uint color;
        int life;
        ParticleType type;
    }
}
