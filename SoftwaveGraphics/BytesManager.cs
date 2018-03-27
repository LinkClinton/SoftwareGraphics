using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace SoftwaveGraphics
{
    static public class BytesManager
    {
        public static T BytesToStruct<T>(byte[] bytes, int index = 0)
        {
            int sizeT = Marshal.SizeOf<T>();

            IntPtr dataPtr = Marshal.AllocHGlobal(sizeT);

            Marshal.Copy(bytes, index, dataPtr, sizeT);

            T result = Marshal.PtrToStructure<T>(dataPtr);

            Marshal.FreeHGlobal(dataPtr);

            return result;
        }

        public static byte[] StructToBytes<T>(T data)
        {
            int sizeT = Marshal.SizeOf<T>();

            IntPtr dataPtr = Marshal.AllocHGlobal(sizeT);

            byte[] result = new byte[sizeT];

            Marshal.StructureToPtr(data, dataPtr, false);

            Marshal.Copy(dataPtr, result, 0, sizeT);

            Marshal.FreeHGlobal(dataPtr);
            
            return result;
        }

        public static void StructToBytes<T>(T data, byte[] bytes, int index = 0)
        {
            int sizeT = Marshal.SizeOf<T>();

            IntPtr dataPtr = Marshal.AllocHGlobal(sizeT);

            Marshal.StructureToPtr(data, dataPtr, false);

            Marshal.Copy(dataPtr, bytes, index, sizeT);

            Marshal.FreeHGlobal(dataPtr);
        }
    }
}
