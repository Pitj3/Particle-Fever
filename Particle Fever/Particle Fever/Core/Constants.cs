using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Particle_Fever
{
    public class Constants
    {
        private static long _toSeconds = 1000000000;

        public static long ToSeconds
        {
            get
            {
                return _toSeconds;
            }

            set
            {
                _toSeconds = value;
            }
        }
    }
}
