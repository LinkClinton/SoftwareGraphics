using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwaveGraphics
{
    public delegate void VertexShaderUnit(ref UnitProperty unitProperty, object vertex, params object[] inputData);

    public class VertexShader : Shader
    {

        public event VertexShaderUnit ProcessUnit;

        public VertexShader()
        {
            ProcessUnit += OnProcessUnit;
        }

        protected virtual void OnProcessUnit(ref UnitProperty unitProperty, object vertex, params object[] inputData)
        {
            //
        }
    }
}
