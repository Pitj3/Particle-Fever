using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenTK;
using OpenTK.Input;

namespace Particle_Fever
{
    public class Controller
    {
        private ElementType _currentElement = ElementType.NONE;

        public Controller()
        {
            
        }

        public void OnRenderFrame(FrameEventArgs e)
        {

        }

        public void OnUpdateFrame(FrameEventArgs e)
        {
            if(Input.IsMouseButtonDown(MouseButton.Left))
            {
                switch(_currentElement)
                {
                    case ElementType.SAND:
                    {
                        Program.Game.Simulation.Elements.Add(new SandElement((uint)Input.MousePosition.X, (uint)Input.MousePosition.Y));
                        break;
                    }
                }
            }
        }

        public void SetElement(string name)
        {
            switch (name)
            {
                case "Sand":
                {
                    _currentElement = ElementType.SAND;
                    break;
                }                   
            }
        }
    }
}
