﻿using Reception.App.Model.PersonInfo;
using System;
using System.Collections.Generic;
using System.Linq;

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
                { typeof(Visitor), 2 }
            };
        }

        /// <summary>
        /// null = -1;
        /// <see cref="object"/> = 0;
        /// <see cref="Person"/> = 1;
        /// <see cref="Visitor"/> = 2
        /// </summary>
        public static Dictionary<Type, int> Dictionary { get; set; }

        public static Dictionary<string, int> ToTypeNamesDictionary(this Dictionary<Type, int> value)
        {
            return value.ToDictionary(pair => pair.Key.FullName, pair => pair.Value);
        }
    }
}