namespace Reception.Extension.Dictionaries
{
    public static class TypeDictionary
    {
        public static int TryGetValue<T>(this Dictionary<T, int> value, T type) where T : Type
        {
            if (type == null)
            {
                return -1;
            }

            value.TryGetValue(type, out int typeId);

            return typeId;
        }
    }
}
