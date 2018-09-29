using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;
using System.Threading.Tasks;

namespace Application
{
    public class Camera
    {
        private Vector3 position;
        private Vector3 lookAt;

        public Vector3 Position { get => position; set => position = value; }
        public Vector3 LookAt { get => lookAt; set => lookAt = value; }

        public Matrix4x4 Matrix { get => Matrix4x4.CreateLookAt(position, lookAt, new Vector3(0, 1, 0)); }
    }
}
