using System;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
#pragma warning disable 1591


namespace StealthAPI
{
    public static class Marshaler
    {
        /// <summary>
        /// Перевести объект в массив байт
        /// </summary>
        /// <param name="obj">Объект для перевода</param>
        /// <returns>Массив байт</returns>
        public static byte[] MarshalToBytes(this object obj)
        {
            int size = Marshal.SizeOf(obj);
            byte[] target = new byte[size];
            GCHandle gcHandle = GCHandle.Alloc(target, GCHandleType.Pinned);
            IntPtr ptr = Marshal.UnsafeAddrOfPinnedArrayElement(target, 0);
            Marshal.StructureToPtr(obj, ptr, false);
            gcHandle.Free();
            return target;
        }

        /// <summary>
        /// Перевести массив байт в объект
        /// </summary>
        /// <param name="buffer">Массив байт</param>
        /// <returns>Результирующий объект</returns>
        public static T MarshalToObject<T>(this byte[] buffer)
        {
            return (T)MarshalToObject(buffer, typeof(T));
        }

        public static object MarshalToObject(this byte[] buffer, Type type)
        {
            object target = null;

            if (type.GetInterfaces().Any(intf => intf == typeof(IDeserialized)))
            {
                target = Activator.CreateInstance(type);
                using(MemoryStream str = new MemoryStream(buffer))
                using (BinaryReader br = new BinaryReader(str))
                {
                    (target as IDeserialized).Deserialize(br);
                }
            }
            else
            {
                int size = buffer.Length;
                GCHandle gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                IntPtr ptr = Marshal.UnsafeAddrOfPinnedArrayElement(buffer, 0);
                target = Marshal.PtrToStructure(ptr, type);
                gcHandle.Free();
            }
            return target;
        }

        public static T MarshalToObject<T>(this BinaryReader reader)
        {
            return (T)MarshalToObject(reader, typeof(T));
        }

        public static object MarshalToObject(this BinaryReader reader, Type type)
        {
            object target = null;

            if (type.GetInterfaces().Any(intf => intf == typeof(IDeserialized)))
            {
                target = Activator.CreateInstance(type);
                (target as IDeserialized).Deserialize(reader);                
            }
            else
            {
                var size = Marshal.SizeOf(type);
                var buffer = reader.ReadBytes(size);
                target = buffer.MarshalToObject(type);
            }
            return target;
        }

        public static IntPtr MarshalToPtr(this object obj)
        {
            var size = Marshal.SizeOf(obj);
            IntPtr ptr = Marshal.AllocHGlobal(size);
            Marshal.StructureToPtr(obj, ptr, false);
            return ptr;
        }
    }
}
