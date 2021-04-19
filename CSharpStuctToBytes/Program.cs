using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CSharpStuctToBytes
{
    class Program
    {
        static void Main(string[] args)
        {

            NQuaternion ccc;
            ccc.x = 1;
            ccc.y = 2;
            ccc.z = 3;
            ccc.w = 4;

            var bytes = StructToBytes(ccc);

            var obj = BytesToStruct(bytes, typeof(NQuaternion));

        }


        /// <summary>
        /// 结构体转化成byte[]
        /// </summary>
        /// <param name="structure"></param>
        /// <returns></returns>
        public static Byte[] StructToBytes(Object structure)
        {
            Int32 size = Marshal.SizeOf(structure);
            IntPtr buffer = Marshal.AllocHGlobal(size);
            try
            {
                Marshal.StructureToPtr(structure, buffer, false);
                Byte[] bytes = new Byte[size];
                Marshal.Copy(buffer, bytes, 0, size);

                return bytes;
            }
            finally
            {
                Marshal.FreeHGlobal(buffer);
            }
        }
        /// <summary>
        /// byte[]转化成结构体
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="strcutType"></param>
        /// <returns></returns>
        public static Object BytesToStruct(Byte[] bytes, Type strcutType)
        {
            Int32 size = Marshal.SizeOf(strcutType);
            IntPtr buffer = Marshal.AllocHGlobal(size);
            try
            {
                Marshal.Copy(bytes, 0, buffer, size);

                return Marshal.PtrToStructure(buffer, strcutType);
            }
            finally
            {
                Marshal.FreeHGlobal(buffer);
            }
        }

    }



    [StructLayout(LayoutKind.Sequential)]
    public struct NQuaternion
    {
        public int x;
        public int y;
        public int z;
        public int w;
        public override string ToString()
        {
            return string.Format("({0:N5},{1:N5},{2:N5},{3:N5})", w, x, y, z);
        }
    }

}
