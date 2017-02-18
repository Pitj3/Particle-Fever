using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenTK;

namespace Particle_Fever
{
    public enum ElementType
    {
        NONE = 0,
        SAND = 1
    };

    public class Element
    {
        private string _name;
        private uint _color;

        private Particle _part;

        private bool _hasMoved = false;
        private bool _needsRemove = false;

        public Element(uint x, uint y)
        {
            _part.x = (int)x;
            _part.y = (int)y;              
        }


        public void OnRenderFrame(FrameEventArgs e)
        {
            _part.color = Color;

            Program.Game.ScreenBuffer.SetPixel((uint)_part.x, (uint)_part.y, Color);
            Program.Game.ScreenBuffer.ClearPixel((uint)_part.oldx, (uint)_part.oldy);

            _part.oldx = _part.x;
            _part.oldy = _part.y;
        }

        public void OnUpdateFrame(FrameEventArgs e)
        {
            _part.y++;

            _hasMoved = true; // should change ofcourse

            if(_hasMoved)
            {
                if(_part.y >= Program.Game.windowSize.Y || _part.y < 0 || _part.x < 0 || _part.x >= Program.Game.windowSize.X)
                {
                    _needsRemove = true;
                }
            }
        }

        public string Name
        {
            get
            {
                return _name;
            }

            set
            {
                _name = value;
            }
        }

        public uint Color
        {
            get
            {
                return _color;
            }

            set
            {
                _color = value;
            }
        }

        public bool NeedsRemove
        {
            get
            {
                return _needsRemove;
            }

            set
            {
                _needsRemove = value;
            }
        }
    }

    public class SandElement : Element
    {
        public SandElement(uint x, uint y) : base(x, y)
        {
            Color = 0xFFFF00FF;
            Name = "Sand";
        }
    }
}
