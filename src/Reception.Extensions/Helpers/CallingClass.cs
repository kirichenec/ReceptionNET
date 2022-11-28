using System.Diagnostics;

namespace Reception.Extension.Helpers
{
    /// <summary>
    /// Class for getting calling class info
    /// <br><see href="https://stackoverflow.com/a/48570616/7473834">Original SO answer</see></br>
    /// </summary>
    public static class CallingClass
    {
        public static string GetName(bool needsFull = false)
        {
            string className;
            Type declaringType;
            int skipFrames = 2;
            do
            {
                var method = new StackFrame(skipFrames, false).GetMethod();
                declaringType = method.DeclaringType;
                if (declaringType == null)
                {
                    return method.Name;
                }
                skipFrames++;
                className = needsFull ? declaringType.FullName : declaringType.Name;
            }
            while (declaringType.Module.Name.Equals("mscorlib.dll", StringComparison.OrdinalIgnoreCase));

            return className;
        }
    }
}
