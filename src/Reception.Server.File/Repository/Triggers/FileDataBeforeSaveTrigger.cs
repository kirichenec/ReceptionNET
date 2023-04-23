using EntityFrameworkCore.Triggered;
using Reception.Server.File.Entities;

namespace Reception.Server.File.Repository.Triggers
{
    public class FileDataBeforeSaveTrigger : IBeforeSaveTrigger<FileData>
    {
        public void BeforeSave(ITriggerContext<FileData> context)
        {
            if (context.ChangeType == ChangeType.Added || context.ChangeType == ChangeType.Modified)
            {
                context.Entity.Version = Guid.NewGuid();
            }
        }
    }
}
