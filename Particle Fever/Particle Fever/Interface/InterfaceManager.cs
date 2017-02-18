using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenTK;
using OpenTK.Input;
using OpenTK.Graphics.OpenGL;
using System.Drawing;

namespace Particle_Fever
{
    public class InterfaceManager
    {
        // Gwen variables
        private Gwen.Input.OpenTK _input;
        private Gwen.Renderer.OpenTK _renderer;
        private Gwen.Skin.Base _skin;
        private Gwen.Control.Canvas _canvas;

        // Interface variables
        private Matrix4 _canvasMatrix;

        public Gwen.Control.Canvas Canvas
        {
            get
            {
                return _canvas;
            }

            set
            {
                _canvas = value;
            }
        }

        public Gwen.Renderer.OpenTK Renderer
        {
            get
            {
                return _renderer;
            }

            set
            {
                _renderer = value;
            }
        }

        public InterfaceManager()
        {

        }

        public void OnLoad(EventArgs e)
        {
            Renderer = new Gwen.Renderer.OpenTK();
            _skin = new Gwen.Skin.TexturedBase(Renderer, "Content/DefaultSkin.png");
            Canvas = new Gwen.Control.Canvas(_skin);

            _input = new Gwen.Input.OpenTK(Program.Game);
            _input.Initialize(Canvas);

            Canvas.SetSize((int)Program.Game.windowSize.X, (int)Program.Game.windowSize.Y);
            Canvas.ShouldDrawBackground = false;
            Canvas.BackgroundColor = Color.FromArgb(255, 150, 170, 170);

            SetupInputEvents();

            // add interface items
            Gwen.Control.ImagePanel _image = new Gwen.Control.ImagePanel(_canvas);
            _image.ImageName = "Content/image.png";
            _image.SetUV(0, 0, 1, 1);
            _image.SetSize(64, 64);
            _image.Position(Gwen.Pos.Center);
            _image.SetToolTipText("Image");
        }

        public void OnRenderFrame(FrameEventArgs e)
        {
            Canvas.RenderCanvas();
        }

        public void OnUpdateFrame(FrameEventArgs e)
        {

        }

        public void SetupInputEvents()
        {
            Program.Game.Keyboard.KeyDown += Keyboard_KeyDown;
            Program.Game.Keyboard.KeyUp += Keyboard_KeyUp;

            Program.Game.Mouse.ButtonDown += Mouse_ButtonDown;
            Program.Game.Mouse.ButtonUp += Mouse_ButtonUp;
            Program.Game.Mouse.Move += Mouse_Move;
            Program.Game.Mouse.WheelChanged += Mouse_Wheel;
        }

        void Keyboard_KeyDown(object sender, KeyboardKeyEventArgs e)
        {
            _input.ProcessKeyDown(e);
        }

        void Keyboard_KeyUp(object sender, KeyboardKeyEventArgs e)
        {
            _input.ProcessKeyUp(e);
        }

        void Mouse_ButtonDown(object sender, MouseButtonEventArgs args)
        {
            _input.ProcessMouseMessage(args);
        }

        void Mouse_ButtonUp(object sender, MouseButtonEventArgs args)
        {
            _input.ProcessMouseMessage(args);
        }

        void Mouse_Move(object sender, MouseMoveEventArgs args)
        {
            _input.ProcessMouseMessage(args);
        }

        void Mouse_Wheel(object sender, MouseWheelEventArgs args)
        {
            _input.ProcessMouseMessage(args);
        }

        public void OnResize(EventArgs e)
        {
            float w = Program.Game.windowSize.X;
            float h = Program.Game.windowSize.Y;

            _canvasMatrix = Matrix4.CreateTranslation(new Vector3(-w / 2.0f, -h / 2.0f, 0)) * 
                Matrix4.CreateScale(new Vector3(1, -1, 1)) * 
                Matrix4.CreateOrthographic(w, h, -1, 1);

            Renderer?.Resize(ref _canvasMatrix, (int)w, (int)h);
            Canvas?.SetSize((int)w, (int)h);
        }
    }
}
