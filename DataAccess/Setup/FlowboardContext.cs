using Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Data.Setup
{
    public class FlowboardContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Token> Tokens { get; set; }
        public DbSet<Workspace> Workspaces { get; set; }
        public DbSet<List> Lists { get; set; }
        public DbSet<Data.Entities.Task> Tasks { get; set; }
        public DbSet<Data.Entities.Files> Files { get; set; }

        public FlowboardContext(DbContextOptions<FlowboardContext> options) : base(options)
        {
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
        {
            string currentUser = "MODIFYTHIS";
            var timestamp = DateTime.UtcNow;


            foreach (var entry in ChangeTracker.Entries<AuditEntity>())
            {
                entry.Entity.CreatedBy = currentUser;
                entry.Entity.UpdatedAt = timestamp;

                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedAt = timestamp;
                }
                 
                if (entry.Entity is Data.Entities.Task task)
                {
                    var list = await this.Lists
                                .Include(l => l.Workspace)
                                .FirstOrDefaultAsync(l => l.Id == task.ListId, cancellationToken);


                    list!.UpdatedAt = timestamp;
                    list!.Workspace.UpdatedAt = timestamp;
                }
                else if (entry.Entity is Data.Entities.List list)
                {
                    var workspace = await this.Workspaces
                                .FirstOrDefaultAsync(w => w.Id == list.WorkspaceId, cancellationToken);


                    list!.UpdatedAt = timestamp;
                    workspace!.UpdatedAt = timestamp;
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }

    }
}
