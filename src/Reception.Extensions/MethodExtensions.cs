using System.Linq;
using System.Reflection;

namespace Reception.Extension
{
    public static class MethodExtensions
    {
        public static string GetMethodParametersNames(this MethodInfo method)
        {
            return method?.GetParameters().Select(mParam => mParam.Name).ToJoinString(", ")
                ?? string.Empty;
        }
    }
}