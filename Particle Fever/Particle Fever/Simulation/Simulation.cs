using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenTK;

namespace Particle_Fever
{
    public class Simulation
    {
        private Particle[] _parts;

        public Simulation()
        {
            _parts = new Particle[(int)(Program.Game.windowSize.X * Program.Game.windowSize.Y)];
        }

        public void OnUpdateFrame(FrameEventArgs e)
        {

        }

        public void OnRenderFrame(FrameEventArgs e)
        {

        }
    }
}
