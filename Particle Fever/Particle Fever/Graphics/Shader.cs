using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace Particle_Fever
{
    public class Shader
    {
        private uint _program;

        public Shader(string filepath)
        {
            string vsSource = "";
            string fsSource = "";

            if (!File.Exists(filepath + ".vs"))
            {
                Logger.Log(LogLevel.ERROR, "Shader not found at location: " + filepath);
            }
            else
            {
                using (StreamReader sr = File.OpenText(filepath + ".vs"))
                {
                    vsSource = sr.ReadToEnd();
                }
            }
            if (!File.Exists(filepath + ".fs"))
            {
                Logger.Log(LogLevel.ERROR, "Shader not found at location: " + filepath);
            }
            else
            {
                using (StreamReader sr = File.OpenText(filepath + ".fs"))
                {
                    fsSource = sr.ReadToEnd();
                }
            }

            uint vertShader = (uint)GL.CreateShader(ShaderType.VertexShader);
            uint fragShader = (uint)GL.CreateShader(ShaderType.FragmentShader);

            GL.ShaderSource((int)vertShader, vsSource);
            GL.CompileShader(vertShader);

            int vertLogLength = 0;
            StringBuilder outVertLog = new StringBuilder();
            GL.GetShaderInfoLog(vertShader, 1024, out vertLogLength, outVertLog);
            if (vertLogLength > 1) Logger.Log(LogLevel.ERROR, outVertLog.ToString());

            GL.ShaderSource((int)fragShader, fsSource);
            GL.CompileShader(fragShader);

            int fragLogLength = 0;
            StringBuilder outFragLog = new StringBuilder();
            GL.GetShaderInfoLog(vertShader, 1024, out fragLogLength, outFragLog);
            if (vertLogLength > 1) Logger.Log(LogLevel.ERROR, outFragLog.ToString());

            _program = (uint)GL.CreateProgram();
            GL.AttachShader(_program, vertShader);
            GL.AttachShader(_program, fragShader);
            GL.LinkProgram(_program);

            int programLogLength = 0;
            StringBuilder programLog = new StringBuilder();
            GL.GetProgramInfoLog(_program, 1024, out programLogLength, programLog);
            if (programLogLength > 1) Logger.Log(LogLevel.ERROR, programLog.ToString());

            GL.DeleteShader(vertShader);
            GL.DeleteShader(fragShader);
        }

        public void Bind()
        {
            GL.UseProgram(_program);
        }

        public void Unbind()
        {
            GL.UseProgram(0);
        }

        public int GetVariableLocation(string name)
        {
            return GL.GetUniformLocation(_program, name);
        }

        public uint Program
        {
            get
            {
                return _program;
            }
        }
    }
}
