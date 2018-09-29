using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SoftwaveGraphics;

namespace Application
{
    public class TestPixelShader : PixelShader
    {
        protected override object OnProcessUnit(ref UnitProperty unitProperty, params object[] inputData)
        {
            return unitProperty.Color;
        }
    }
}
