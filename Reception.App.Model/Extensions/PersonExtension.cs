﻿using Reception.App.Model.PersonInfo;

namespace Reception.App.Model.Extensions
{
    public static class PersonExtension
    {
        public static bool IsNullOrEmpty(this Person value)
        {
            return value.IsNull() || value.IsEmpty();
        }
    }
}