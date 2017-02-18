using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace Particle_Fever
{
    public static class GLRenderer
    {
        // Camera variables
        private static Matrix4 _projection = Matrix4.Identity;
        private static Matrix4 _view = Matrix4.Identity;

        public static void Initialize()
        {
            _projection = Matrix4.CreateOrthographicOffCenter(0, Program.Game.windowSize.X, Program.Game.windowSize.Y, 0, -10, 10);
            _view = Matrix4.Identity;

            GL.Disable(EnableCap.DepthTest);
            GL.Enable(EnableCap.Texture2D);
            GL.Disable(EnableCap.CullFace);
        }

        public static void OnBeginRenderFrame()
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
        }

        public static void OnEndRenderFrame()
        {
            Program.Game.SwapBuffers();
        }

        public static void OnResize(EventArgs e)
        {
            GL.Viewport(0, 0, Program.Game.ClientRectangle.Width, Program.Game.ClientRectangle.Height);
            Projection = Matrix4.CreateOrthographicOffCenter(0, Program.Game.ClientRectangle.Width, Program.Game.ClientRectangle.Height, 0, -10, 10);
        }

        public static Matrix4 Projection
        {
            get
            {
                return _projection;
            }

            set
            {
                _projection = value;
            }
        }

        public static Matrix4 View
        {
            get
            {
                return _view;
            }

            set
            {
                _view = value;
            }
        }
    }
}
