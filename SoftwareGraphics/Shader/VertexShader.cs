using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareGraphics
{
    public delegate void VertexShaderUnit(ref UnitProperty unitProperty, object vertex, params object[] inputData);

    public class VertexShader : Shader
    {
        public event VertexShaderUnit ProcessUnit;

        internal void StartProcessUnit(ref UnitProperty unitProperty, object vertex, params object[] inputData)
        {
            ProcessUnit.Invoke(ref unitProperty, vertex, inputData);
        }

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
