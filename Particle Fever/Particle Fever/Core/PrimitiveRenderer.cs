using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace Particle_Fever
{
    public class QuadPrimitive
    {
        uint vao;
        uint vbo;
        public QuadPrimitive(uint x, uint y, uint width, uint height)
        {
            float[] data = new float[12]
            {
                (float)x, (float)y, 0,
                (float)(x + width), (float)y, 0,
                (float)(x + width), (float)(y + height), 0,
                (float)x, (float)(y + height), 0
            };

            vao = (uint)GL.GenVertexArray();
            GL.BindVertexArray(vao);
            vbo = (uint)GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
            GL.BufferData(BufferTarget.ArrayBuffer, sizeof(float) * 12, data, BufferUsageHint.StaticDraw);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.BindVertexArray(0);
        }

        public void render()
        {
            GL.BindVertexArray(vao);
            GL.EnableVertexAttribArray(0);
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 0, 0);
            GL.DrawArrays(PrimitiveType.Quads, 0, 4);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.DisableVertexAttribArray(0);
            GL.BindVertexArray(0);
        }
    }

    public static class PrimitiveRenderer
    {
        public static void renderBox(uint x, uint y, uint width, uint height)
        {
            GL.PushMatrix();
            GL.Translate((double)x, (double)y, 0.0);

            GL.Begin(PrimitiveType.Quads);
                GL.TexCoord2(0, 0);             GL.Vertex2(0, 0);
                GL.TexCoord2(1, 0);             GL.Vertex2(width, 0);
                GL.TexCoord2(1, 1);             GL.Vertex2(width, height);
                GL.TexCoord2(0, 1);             GL.Vertex2(0, height);
            GL.End();

            GL.PopMatrix();
        }
    }
}
