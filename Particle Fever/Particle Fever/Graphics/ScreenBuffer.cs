using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace Particle_Fever
{
    public class ScreenBuffer
    {
        private ushort[] _imageData;
        private uint _bpp;
        private uint _width, _height;
        private uint _id;

        public ScreenBuffer(uint width, uint height, uint flag, uint bpp)
        {
            _width = width;
            _height = height;
            _bpp = bpp;

            Logger.Log(LogLevel.DEBUG, "Creating empty screen buffer of (" + width + " * " + height + ")");

            _imageData = new ushort[_width * _height * _bpp];

            for(uint i = 0; i < _width * _height * _bpp; i++)
            {
                _imageData[i] = 0;
            }

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

            Logger.Log(LogLevel.DEBUG, "Finished creating screen buffer");
        }

        public Vector4 GetPixel(uint x, uint y)
        {
            Vector4 r = new Vector4();

            GL.BindTexture(TextureTarget.Texture2D, _id);

            ushort[] buffer = new ushort[_width * _height * _bpp];
            GL.GetTexImage(TextureTarget.Texture2D, 0, PixelFormat.Bgra, PixelType.UnsignedByte, buffer);

            int startAddressOfPixel = (int)(((y * _width) + x) * _bpp);

            r.X = buffer[startAddressOfPixel + 2];
            r.Y = buffer[startAddressOfPixel + 1];
            r.Z = buffer[startAddressOfPixel + 0];
            r.W = buffer[startAddressOfPixel + 3];

            return r;
        }

        public void SetPixel(uint x, uint y, uint color)
        {
            int r = ((int)color >> 16) & 0xFF;
            int g = ((int)color >> 8) & 0xFF;
            int b = ((int)color >> 0) & 0xFF;

            if (x >= _width || x < 0 || y < 0 || y >= _height)
            {
                return;
            }

            _imageData[y * (_width * _bpp) + x * _bpp + 0] = (ushort)r;
            _imageData[y * (_width * _bpp) + x * _bpp + 1] = (ushort)g;
            _imageData[y * (_width * _bpp) + x * _bpp + 2] = (ushort)b;
            _imageData[y * (_width * _bpp) + x * _bpp + 3] = (ushort)255;
        }

        public void OnBeginRenderFrame()
        {
            GL.Viewport(0, 0, (int)_width, (int)_height);
        }

        public void OnEndRenderFrame()
        {
            GL.BindTexture(TextureTarget.Texture2D, _id);
            if (_bpp == 4) // has alpha
            {
                GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, (int)_width, (int)_height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, _imageData);
            }
            else
            {
                GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgb, (int)_width, (int)_height, 0, PixelFormat.Rgb, PixelType.UnsignedByte, _imageData);
            }
        }
    }
}
