using EntityFrameworkCore.Triggered;
using Reception.Server.File.Entities;

namespace Reception.Server.File.Repository.Triggers
{
    public class FileDataBeforeSaveTrigger : IBeforeSaveTrigger<FileData>
    {
        public Task BeforeSave(ITriggerContext<FileData> context, CancellationToken cancellationToken)
        {
            if (context.ChangeType == ChangeType.Added || context.ChangeType == ChangeType.Modified)
            {
                context.Entity.Version = Guid.NewGuid();
            }

            return Task.CompletedTask;
        }
    }
}