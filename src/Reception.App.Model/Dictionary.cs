using Reception.App.Model.PersonInfo;

namespace Reception.App.Model
{
    public static class Types
    {
        public const int NULL_TYPE_ID = -1;
        public const int OBJECT_TYPE_ID = 0;
        public const int PERSON_TYPE_ID = 1;
        public const int VISITOR_TYPE_ID = 2;
        public const int BOSS_DECISION_TYPE_ID = 3;


        static Types()
        {
            Dictionary = new Dictionary<Type, int>
            {
                { typeof(object), OBJECT_TYPE_ID },
                { typeof(Person), PERSON_TYPE_ID },
                { typeof(Visitor), VISITOR_TYPE_ID },
                { typeof(BossDecision), BOSS_DECISION_TYPE_ID },
            };
        }


        /// <remarks>
        /// <list type="bullet">
        /// <item><see langword="null"/> = <see cref="NULL_TYPE_ID"/>;</item>
        /// <item><see cref="object"/> = <see cref="OBJECT_TYPE_ID"/>;</item>
        /// <item><see cref="Person"/> = <see cref="PERSON_TYPE_ID"/>;</item>
        /// <item><see cref="Visitor"/> = <see cref="VISITOR_TYPE_ID"/>;</item>
        /// <item><see cref="BossDecision"/> = <see cref="BOSS_DECISION_TYPE_ID"/>;</item>
        /// </list>
        /// </remarks>
        public static Dictionary<Type, int> Dictionary { get; set; }

        public static Dictionary<string, int> ToTypeNamesDictionary(this Dictionary<Type, int> value)
        {
            return value.ToDictionary(pair => pair.Key.FullName, pair => pair.Value);
        }
    }
}