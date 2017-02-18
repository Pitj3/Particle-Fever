using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SharpDX.DirectInput;

namespace Particle_Fever
{
    class Program
    {
        private static ParticleGame _game = null;

        public static ParticleGame Game
        {
            get
            {
                return _game;
            }

            set
            {
                _game = value;
            }
        }

        static void Main(string[] args)
        {
            Game = new ParticleGame(1280, 720, "Particle Fever");
            Game.Run(60);
        }
    }
}
