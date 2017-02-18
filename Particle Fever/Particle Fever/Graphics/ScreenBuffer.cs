using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenTK;
using OpenTK.Graphics.OpenGL;
using GlmNet;

namespace Particle_Fever
{
    public class ScreenBuffer
    {
        private ushort[] _imageData;
        private uint _bpp;
        private uint _width, _height;
        private uint _id;
        private uint _type;

        public ScreenBuffer(uint width, uint height, uint flag, uint bpp)
        {
            _width = width;
            _height = height;
            _bpp = bpp;

            Logger.log(LogLevel.DEBUG, "Creating empty screen buffer of (" + width + " * " + height + ")");

            _imageData = new ushort[_width * _height * _bpp];

            _id = (uint)GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2D, _id);
            if(_bpp == 4) // has alpha
            { 
                GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, (int)_width, (int)_height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, _imageData);
            }
            else
            {
                GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgb, (int)_width, (int)_height, 0, PixelFormat.Rgb, PixelType.UnsignedByte, _imageData);
            }

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, flag);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, flag);

            Logger.log(LogLevel.DEBUG, "Finished creating screen buffer");
        }

        public vec4 getPixel(uint x, uint y)
        {
            vec4 r = new vec4();

            GL.BindTexture(TextureTarget.Texture2D, _id);

            ushort[] buffer = new ushort[_width * _height * _bpp];
            GL.GetTexImage(TextureTarget.Texture2D, 0, PixelFormat.Bgra, PixelType.UnsignedByte, buffer);

            int startAddressOfPixel = (int)(((y * _width) + x) * _bpp);

            r.x = buffer[startAddressOfPixel + 2];
            r.y = buffer[startAddressOfPixel + 1];
            r.z = buffer[startAddressOfPixel + 0];
            r.w = buffer[startAddressOfPixel + 3];

            return r;
        }

        public void beginRender()
        {
            GL.Viewport(0, 0, (int)_width, (int)_height);
        }

        public void endRender()
        {
            GL.BindTexture(TextureTarget.Texture2D, _id);
            GL.CopyTexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgb, 0, 0, (int)_width, (int)_height, 0);

            GL.Viewport(0, 0, (int)Program.game.windowSize.X, (int)Program.game.windowSize.Y);
        }
    }
}
