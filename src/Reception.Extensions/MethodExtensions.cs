using System.Linq;
using System.Reflection;

namespace Reception.Extension
{
    public static class MethodExtensions
    {
        public static string GetMethodParametersNames(this MethodInfo method)
        {
            if (method != null)
                return string.Join(", ", method.GetParameters().Select(mParam => mParam.Name));

            return string.Empty;
        }
    }
}