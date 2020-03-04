using Reception.App.Model.PersonInfo;

namespace Reception.App.Model.Extensions
{
    public static class VisitorExtension
    {
        public static bool IsNullOrEmpty(this Visitor value)
        {
            return value.IsNull() || value.IsEmpty();
        }
    }
}