using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;
using System.Threading.Tasks;

namespace SoftwaveGraphics
{
    public delegate object PixelShaderUnit(ref UnitProperty unitProperty, params object[] inputData); 

    public class PixelShader : Shader
    {
        public event PixelShaderUnit ProcessUnit;

        internal void StartProcessUnit(ref UnitProperty unitProperty, params object[] inputData)
        {
            ProcessUnit.Invoke(ref unitProperty, inputData);
        }

        public PixelShader()
        {
            ProcessUnit += OnProcessUnit;
        }

        private object OnProcessUnit(ref UnitProperty unitProperty, params object[] inputData)
        {
            return Vector4.Zero;
        }
    }
}
