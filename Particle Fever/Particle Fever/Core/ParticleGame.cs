using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

namespace Particle_Fever
{
    public class ParticleGame : GameWindow
    {
        // Window variables
        private Vector2 _windowSize = new Vector2(0, 0);
        private string _title = "";
        private bool _isRunning = false;

        // Screen variables
        private ScreenBuffer _screenBuffer;
        private QuadPrimitive _screenQuad;

        // Shader variables
        private Shader _defaultShader;
        private int _mvpLoc;
        private int _diffuseLoc;

        // Simulation variables
        private Simulation _simulation;

        // Timing variables
        private Timer _timer = new Timer();

        // Controller variables
        private Controller _controller;

        // Interface variables
        private InterfaceManager _interface;

        public ParticleGame(uint width, uint height, string title) : base((int)width, (int)height)
        {
            Logger log = new Logger();

            _windowSize = new Vector2((float)width, (float)height);
            _title = title;
        }
        
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            Logger.Log(LogLevel.INFO, "Initializing game");

            Title = _title;

            ScreenBuffer = new ScreenBuffer((uint)_windowSize.X, (uint)_windowSize.Y, (uint)TextureMinFilter.Nearest, 4);

            GL.ClearColor(Color.MidnightBlue);
            GLRenderer.Initialize();

            // load content

            _defaultShader = new Shader("Content/default");
            _mvpLoc = _defaultShader.GetVariableLocation("mvp");
            _diffuseLoc = _defaultShader.GetVariableLocation("diffuse");

            GL.ActiveTexture(TextureUnit.Texture0);
            GL.Uniform1(_diffuseLoc, 0);

            Simulation = new Simulation();

            _screenQuad = new QuadPrimitive(0, 0, (uint)_windowSize.X, (uint)_windowSize.Y);

            Controller = new Controller();

            _interface = new InterfaceManager();
            _interface.OnLoad(e);

            Input.OnLoad(e);

            // end load content
            isRunning = true;

            Logger.Log(LogLevel.INFO, "Done initializing game");
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            Input.OnUpdateFrame(e);

            _interface.OnUpdateFrame(e);
            Controller.OnUpdateFrame(e);
            Simulation.OnUpdateFrame(e);

            if (Input.IsKeyPressed(Key.Escape))
                Close();
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GLRenderer.OnBeginRenderFrame();

            Controller.OnRenderFrame(e);
            Input.OnRenderFrame(e);

            // Begin rendering to screenbuffer
            ScreenBuffer.OnBeginRenderFrame();

            Simulation.OnRenderFrame(e);

            ScreenBuffer.OnEndRenderFrame();
            // End rendering to screenbuffer

            // Render screenquad with screenbuffer texture
            _defaultShader.Bind();

            Matrix4 mvp = GLRenderer.Projection;
            GL.UniformMatrix4(_mvpLoc, false, ref mvp);

            _screenQuad.OnRenderFrame();

            _defaultShader.Unbind();
            // End render screenquad

            // Render interface
            _interface.OnRenderFrame(e);

            GLRenderer.OnEndRenderFrame();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            GLRenderer.OnResize(e);
            _interface.OnResize(e);

            windowSize = new Vector2(Width, Height);
        }

        public bool isRunning
        {
            get
            {
                return _isRunning;
            }
            set
            {
                _isRunning = value;
            }
        }

        public Vector2 windowSize
        {
            get
            {
                return _windowSize;
            }
            set
            {
                _windowSize = value;
            }
        }

        public Controller Controller
        {
            get
            {
                return _controller;
            }

            set
            {
                _controller = value;
            }
        }

        public ScreenBuffer ScreenBuffer
        {
            get
            {
                return _screenBuffer;
            }

            set
            {
                _screenBuffer = value;
            }
        }

        public Simulation Simulation
        {
            get
            {
                return _simulation;
            }

            set
            {
                _simulation = value;
            }
        }
    }
}
