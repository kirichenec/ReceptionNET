using System;

namespace Reception.Model.Interface
{
    public interface IFileVersion : IUnique, ICommentable
    {
        string Extension { get; set; }
        string Name { get; set; }
        Guid Version { get; set; }
    }
}