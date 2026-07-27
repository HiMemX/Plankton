using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using System.IO;

namespace Plankton.Rendering
{
    public class Shader
    {
        int Handle;
        int VertexShader;
        int FragmentShader;


        public Shader(string vertexPath, string fragmentPath)
        {
            string VertexShaderSource = Preprocess(System.IO.Path.GetDirectoryName(vertexPath), File.ReadAllText(vertexPath));

            string FragmentShaderSource = Preprocess(System.IO.Path.GetDirectoryName(vertexPath), File.ReadAllText(fragmentPath));

            VertexShader = GL.CreateShader(ShaderType.VertexShader);
            GL.ShaderSource(VertexShader, VertexShaderSource);

            FragmentShader = GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource(FragmentShader, FragmentShaderSource);

            // Compilation and error checking
            GL.CompileShader(VertexShader);

            GL.GetShader(VertexShader, ShaderParameter.CompileStatus, out int success);
            if (success == 0)
            {
                string infoLog = GL.GetShaderInfoLog(VertexShader);
                MessageBox.Show(infoLog, vertexPath);
            }

            GL.CompileShader(FragmentShader);

            GL.GetShader(FragmentShader, ShaderParameter.CompileStatus, out success);
            if (success == 0)
            {
                string infoLog = GL.GetShaderInfoLog(FragmentShader);
                MessageBox.Show(infoLog, fragmentPath);
            }

            // Attach
            Handle = GL.CreateProgram();

            GL.AttachShader(Handle, VertexShader);
            GL.AttachShader(Handle, FragmentShader);

            GL.LinkProgram(Handle);

            GL.GetProgram(Handle, GetProgramParameterName.LinkStatus, out success);
            if (success == 0)
            {
                string infoLog = GL.GetProgramInfoLog(Handle);
                MessageBox.Show(infoLog);
            }


            // Cleanup
            GL.DetachShader(Handle, VertexShader);
            GL.DetachShader(Handle, FragmentShader);
            GL.DeleteShader(FragmentShader);
            GL.DeleteShader(VertexShader);


        }


        public string Preprocess(string basepath, string code)
        {
            // Replace "#include "FILEPATH";
            // With whatever is inside FILEPATH
            string newcode = "";
            try
            {
                string[] lines = code.Split("\n");
                foreach (string line in lines)
                {
                    if (line.Length < 8 || line.Substring(0, 8) != "#include")
                    {
                        newcode += line + "\n";
                        continue;
                    }

                    string path = line.Substring(line.IndexOf('"') + 1, line.LastIndexOf('"') - line.IndexOf('"') - 1);
                    string text = Preprocess(basepath, File.ReadAllText(basepath + "/" + path));

                    newcode += text + "\n";
                }

            }
            catch (Exception e)
            {
                Debug.debugWindow.AddEntry("Shader.Preprocess", e.ToString());
            }
            return newcode;

        }

        public void Use()
        {
            GL.UseProgram(Handle);
        }

        public void SetMatrix4(string name, Matrix4 mat)
        {
            int location = GL.GetUniformLocation(Handle, name);
            GL.UniformMatrix4(location, true, ref mat);
        }
        public void SetMatrix4(string name, System.Numerics.Matrix4x4 mat)
        {

            Matrix4 opentkmat = ConverterTools.ToOpenTK(mat);
            opentkmat.Transpose();
            int location = GL.GetUniformLocation(Handle, name);
            GL.UniformMatrix4(location, true, ref opentkmat);
        }

        public void SetInt(string name, int i)
        {
            int location = GL.GetUniformLocation(Handle, name);
            GL.Uniform1(location, i);
        }

        public void SetFloat2(string name, float i1, float i2)
        {
            int location = GL.GetUniformLocation(Handle, name);
            GL.Uniform2(location, i1, i2);
        }

        public void SetInts(string name, int[] i)
        {

            int location = GL.GetUniformLocation(Handle, name);
            GL.Uniform1(location, i.Length, i);
        }

        public void SetFloat(string name, float f)
        {
            int location = GL.GetUniformLocation(Handle, name);
            GL.Uniform1(location, f);
        }
        public void SetVector3(string name, System.Numerics.Vector3 vec3)
        {
            int location = GL.GetUniformLocation(Handle, name);
            if (location == -1)
            {
                Debug.debugWindow.AddEntry("SetVector3", "location = -1");
                return;
            }
            GL.Uniform3(location, ConverterTools.ToOpenTK(vec3));
        }

        public void SetVector4(string name, Vector4 vec4)
        {
            int location = GL.GetUniformLocation(Handle, name);
            GL.Uniform4(location, vec4);
        }

        public void SetColor4(string name, Color4 color)
        {
            int location = GL.GetUniformLocation(Handle, name);
            GL.Uniform4(location, color);
        }



    }
}
