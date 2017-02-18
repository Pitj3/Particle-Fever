using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Glfw3;
using SharpDX.DirectInput;

namespace Particle_Fever
{
    public class Input
    {
        private static DirectInput _directInput;
        private static Keyboard _keyboard;

        public Input()
        {
            _directInput = new DirectInput();
            _keyboard = new Keyboard(_directInput);

            _keyboard.Properties.BufferSize = 128;
            _keyboard.Acquire();
        }

        public static void poll()
        {
            _keyboard.Poll();
        }

        public static bool isPressed(Key key)
        {
            return _keyboard.GetCurrentState().IsPressed(key);
        }
    }
}
