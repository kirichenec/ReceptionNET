using Reception.App.Model.Base;

namespace Reception.App.Model.Extensions
{
    public static class BaseModelExtension
    {
        public static bool IsNull(this BaseModel value)
        {
            return value == null;
        }

        public static bool IsNullOrEmpty(this BaseModel value)
        {
            return value.IsNull() || value.IsEmpty();
        }
    }
}
