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
        private List<Element> _elements;
        private List<Element> _elementsToRemove;

        public List<Element> Elements
        {
            get
            {
                return _elements;
            }

            set
            {
                _elements = value;
            }
        }

        public Simulation()
        {
            _parts = new Particle[(int)(Program.Game.windowSize.X * Program.Game.windowSize.Y)];

            _elements = new List<Element>();
            _elementsToRemove = new List<Element>();
        }

        public void OnUpdateFrame(FrameEventArgs e)
        {
            foreach(Element elem in _elements)
            {
                elem.OnUpdateFrame(e);
                if(elem.NeedsRemove)
                {
                    _elementsToRemove.Add(elem);
                }
            }

            foreach(Element elem in _elementsToRemove)
            {
                _elements.Remove(elem);
            }

            _elementsToRemove.Clear();
        }

        public void OnRenderFrame(FrameEventArgs e)
        {
            foreach (Element elem in _elements)
            {
                elem.OnRenderFrame(e);
            }
        }
    }
}
