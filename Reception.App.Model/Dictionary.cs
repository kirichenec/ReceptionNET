using Reception.App.Model.PersonInfo;
using System;
using System.Collections.Generic;

namespace Reception.App.Model
{
    public static class Collection
    {
        static Collection()
        {
            Dictionary = new Dictionary<Type, int>
            {
                { typeof(Person), 1 },
                { typeof(Visitor), 2 }
            };
        }

        /// <summary>
        /// <see cref="Person"/> - 1;
        /// <see cref="Visitor"/> - 2
        /// </summary>
        public static Dictionary<Type, int> Dictionary { get; set; }
    }
}