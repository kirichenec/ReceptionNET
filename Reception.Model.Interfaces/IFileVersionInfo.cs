using System;

namespace Reception.Model.Interfaces
{
    public interface IFileVersionInfo : IUnique, ICommentable
    {
        string FileName { get; set; }
        string Name { get; set; }
        Guid Version { get; set; }
    }
}