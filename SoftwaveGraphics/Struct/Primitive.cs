using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwaveGraphics
{
    class Primitive
    {
        //beacuse of C-Sharp, we can use array of vertex intead of vertex's index
        private UnitProperty[] vertics = null;

        public Primitive(UnitProperty[] Vertics)
        {
            vertics = Vertics;
        }

        public UnitProperty[] Vertics => vertics;
    }
}
