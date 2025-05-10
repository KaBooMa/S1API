#if IL2CPPBEPINEX
using Il2CppInterop.Runtime.InteropTypes;
using Il2CppInterop.Runtime;
using System;

namespace UnityEngine
{
    internal static class InteropUtils
    {
        public static bool IsGeneratedAssemblyType(Type type)
            => IsInheritedFromIl2CppObjectBase(type) && !IsInjectedType(type);

        public static bool IsInheritedFromIl2CppObjectBase(Type type)
            => (type != null) && type.IsSubclassOf(typeof(Il2CppObjectBase));

        public static bool IsInjectedType(Type type)
        {
            IntPtr ptr = GetClassPointerForType(type);
            return ptr != IntPtr.Zero && RuntimeSpecificsStore.IsInjected(ptr);
        }

        public static IntPtr GetClassPointerForType(Type type)
        {
            if (type == typeof(void)) return Il2CppClassPointerStore<Il2CppSystem.Void>.NativeClassPtr;
            return (IntPtr)typeof(Il2CppClassPointerStore<>).MakeGenericType(type)
                  .GetField(nameof(Il2CppClassPointerStore<int>.NativeClassPtr)).GetValue(null);
        }
    }
}
#endif
