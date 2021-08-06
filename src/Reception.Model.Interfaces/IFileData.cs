﻿using System;

namespace Reception.Model.Interface
{
    public interface IFileData : IUnique, ICommentable
    {
        public enum FileType
        {
            Unknown,
            Document,
            Photo
        }

        byte[] Data { get; set; }

        string Extension { get; set; }

        string FullName { get; }

        string Name { get; set; }

        FileType Type { get; set; }

        Guid Version { get; set; }
    }

    public static class IFileDataExtensions
    {
        public static string GetFullName(this IFileData value)
        {
            return string.Join('.', value.Name, value.Extension);
        }
    }
}