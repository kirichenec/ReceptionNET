using Reception.App.Model.PersonInfo;

namespace Reception.App.Model
{
    public static class Types
    {
        static Types()
        {
            Dictionary = new Dictionary<Type, int>
            {
                { typeof(object), 0 },
                { typeof(Person), 1 },
                { typeof(Visitor), 2 },
                { typeof(BossDecision), 3 },
            };
        }

        /// <summary>
        /// null = -1;
        /// <see cref="object"/> = 0;
        /// <see cref="Person"/> = 1;
        /// <see cref="Visitor"/> = 2;
        /// <see cref="BossDecision"/> = 3;
        /// </summary>
        public static Dictionary<Type, int> Dictionary { get; set; }

        public static Dictionary<string, int> ToTypeNamesDictionary(this Dictionary<Type, int> value)
        {
            return value.ToDictionary(pair => pair.Key.FullName, pair => pair.Value);
        }
    }
}