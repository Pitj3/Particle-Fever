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
        private Gwen.Control.Label _currentElementLabel;

        // Menu variables
        private Gwen.Control.GroupBox _bottomMenuBox;
        private Gwen.Control.GroupBox _sideMenuBox;

        public delegate void ButtonFunction();

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

            _sideMenuBox = new Gwen.Control.GroupBox(_canvas);
            _sideMenuBox.SetSize(80, 128);
            _sideMenuBox.Position(Gwen.Pos.Center);
            _sideMenuBox.Position(Gwen.Pos.Right, 5);

            LoadElementCatagories();

            // Current element text
            _currentElementLabel = new Gwen.Control.Label(_canvas);
            _currentElementLabel.SetPosition(10, 10);
            _currentElementLabel.SetSize(100, 40);
            _currentElementLabel.SetText("");

            // Bottom menu
            _bottomMenuBox = new Gwen.Control.GroupBox(_canvas);
            _bottomMenuBox.SetSize(800, 36);
            
            _bottomMenuBox.Position(Gwen.Pos.Center);
            _bottomMenuBox.Position(Gwen.Pos.Bottom);
            _bottomMenuBox.ShouldDrawBackground = true;

        }

        private void LoadElementCatagories()
        {
            Gwen.Control.Button _powdersButton = new Gwen.Control.Button(_sideMenuBox);
            _powdersButton.Font = _powdersButton.Font.Copy();
            _powdersButton.Font.Size = 8;
            _powdersButton.SetSize(80, 20);
            _powdersButton.SetToolTipText("Powders");
            _powdersButton.SetText("Powders");
            _powdersButton.SetPosition(0, 0);
            _powdersButton.Name = "PowdersMenu";
            _powdersButton.Clicked += OnButtonClick;

            Gwen.Control.Button _liquidsButton = new Gwen.Control.Button(_sideMenuBox);

            _liquidsButton.Font = _liquidsButton.Font.Copy();
            _liquidsButton.Font.Size = 8;
            _liquidsButton.SetSize(80, 20);
            _liquidsButton.SetToolTipText("Liquids");
            _liquidsButton.SetText("Liquids");
            _liquidsButton.SetPosition(0, 22);
            _liquidsButton.Name = "LiquidsMenu";
            _liquidsButton.Clicked += OnButtonClick;
        }

        public void OnRenderFrame(FrameEventArgs e)
        {
            Canvas.RenderCanvas();
        }

        public void OnUpdateFrame(FrameEventArgs e)
        {

        }

        public void OnButtonClick(Gwen.Control.Base sender, Gwen.Control.ClickedEventArgs e)
        {
            if(sender.Name.Contains("Menu"))
            {
                OpenMenu(sender.Name);
                return;
            }

            if(sender.Name.Contains("Element"))
            {
                SetElement(sender.Name);
            }
        }

        private void SetElement(string name)
        {
            string elementName = name.Substring(0, name.Length - "Element".Length);
            _currentElementLabel.SetText(elementName);

            Program.Game.Controller.SetElement(elementName);
        }

        private void OpenMenu(string name)
        {
            switch(name)
            {
                case "PowdersMenu":
                {
                    LoadPowders();
                    break;
                }

                case "LiquidsMenu":
                {
                    LoadLiquids();
                    break;
                }
            }

            _canvas.NeedsRedraw = true;
        }

        private void LoadLiquids()
        {
            throw new NotImplementedException();
        }

        private void LoadPowders()
        {
            _bottomMenuBox.DeleteAllChildren();
            Gwen.Control.Button _sandButton = new Gwen.Control.Button(_bottomMenuBox);
            _sandButton.Font.Size = 12;
            _sandButton.SetSize(80, 28);
            _sandButton.SetText("Sand");
            _sandButton.SetToolTipText("Sand");
            _sandButton.SetPosition(2, 7);
            _sandButton.Name = "SandElement";
            _sandButton.Clicked += OnButtonClick;
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
