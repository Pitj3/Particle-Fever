using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Particle_Fever
{
    public class GameTime
    {
        private static double _deltaTime = 0.0;

        public static double DeltaTime
        {
            get
            {
                return _deltaTime;
            }

            set
            {
                _deltaTime = value;
            }
        }
    }
}
