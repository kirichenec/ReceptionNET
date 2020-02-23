using Reception.App.Models;

namespace Reception.App.Extensions
{
    public static class VisitorExtension
    {
        public static bool IsNullOrEmpty(this VisitorInfo value)
        {
            return value.IsNull() || value.IsEmpty();
        }
    }
}
