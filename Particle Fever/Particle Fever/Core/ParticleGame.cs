using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace Particle_Fever
{
    public class ParticleGame : GameWindow
    {
        // Window variables
        private Vector2 _windowSize = new Vector2(0, 0);
        private string _title = "";
        private bool _isRunning = false;

        // Camera variables
        private Matrix4 _projection = Matrix4.Identity;
        private Matrix4 _view = Matrix4.Identity;

        // Screen variables
        private ScreenBuffer _screenBuffer;
        private QuadPrimitive _quad;

        // Shaders
        private Shader _defaultShader;
        private int mvpLoc;

        public ParticleGame(uint width, uint height, string title)
        {
            Logger log = new Logger();

            _windowSize = new Vector2((float)width, (float)height);
            _title = title;
        }
        
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            Logger.log(LogLevel.INFO, "Initializing game");

            Title = _title;
            Size = new Size((int)_windowSize.X, (int)_windowSize.Y);

            _projection = Matrix4.CreateOrthographic(_windowSize.X, _windowSize.Y, -10, 10);
            _view = Matrix4.Identity;

            _screenBuffer = new ScreenBuffer((uint)_windowSize.X, (uint)_windowSize.Y, (uint)TextureMinFilter.Linear, 4);

            GL.Disable(EnableCap.DepthTest);
            GL.Enable(EnableCap.Texture2D);
            GL.Disable(EnableCap.CullFace);

            // load content

            _defaultShader = new Shader("default");
            _quad = new QuadPrimitive(0, 0, (uint)_windowSize.X, (uint)_windowSize.Y);

            mvpLoc = _defaultShader.getVariableLocation("mvp");

            // end load content
            isRunning = true;

            Logger.log(LogLevel.INFO, "Done initializing game");
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            if (Input.isPressed(SharpDX.DirectInput.Key.Escape)) // TODO: Move to OpenTK input
                Close();
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.ClearColor(1, 1, 1, 1);

            _defaultShader.bind();

            Matrix4 mvp = _projection;
            GL.UniformMatrix4(mvpLoc, false, ref mvp);

            _quad.render();

            _defaultShader.unbind();
        

            SwapBuffers();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            windowSize = new Vector2(ClientRectangle.Width, ClientRectangle.Height);
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
                GL.Viewport(0, 0, ClientRectangle.Width, ClientRectangle.Height);
                _projection = Matrix4.CreateOrthographicOffCenter(0, ClientRectangle.Width, ClientRectangle.Height, 0, -10, 10);
                _windowSize = value;
            }
        }
    }
}
