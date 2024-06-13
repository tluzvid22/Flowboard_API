using Data.Entities;
using Data.Enums;
using Microsoft.EntityFrameworkCore;

namespace Data.Setup
{
    public class FlowboardContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Friend> Friends { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<Token> Tokens { get; set; }
        public DbSet<Workspace> Workspaces { get; set; }
        public DbSet<Collaborator> Collaborator { get; set; }
        public DbSet<List> Lists { get; set; }
        public DbSet<Data.Entities.Task> Tasks { get; set; }
        public DbSet<Data.Entities.Files> Files { get; set; }

        public FlowboardContext(DbContextOptions<FlowboardContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Friend>()
                .HasOne(f => f.User1)
                .WithMany(u => u.FriendsUser1)
                .HasForeignKey(f => f.User1Id)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Friend>()
                .HasOne(f => f.User2)
                .WithMany(u => u.FriendsUser2)
                .HasForeignKey(f => f.User2Id)
                .OnDelete(DeleteBehavior.Restrict);
            
            modelBuilder.Entity<Request>()
                .HasOne(f => f.Sender)
                .WithMany(u => u.SenderRequestsUser1)
                .HasForeignKey(f => f.SenderId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Request>()
                .HasOne(f => f.Receiver)
                .WithMany(u => u.ReceiverRequestsUser2)
                .HasForeignKey(f => f.ReceiverId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Collaborator>()
                .HasOne(f => f.Workspace)
                .WithMany(u => u.Collaborator)
                .HasForeignKey(f => f.WorkspaceId)
                .OnDelete(DeleteBehavior.Restrict);
            
            modelBuilder.Entity<Collaborator>()
                .HasOne(f => f.User)
                .WithMany(u => u.Collaborations)
                .HasForeignKey(f => f.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Friend>().HasKey(f => new
            {
                f.User1Id,
                f.User2Id
            });
            modelBuilder.Entity<Request>().HasKey(f => new
            {
                f.SenderId,
                f.ReceiverId
            });
            modelBuilder.Entity<Collaborator>().HasKey(c => new
            {
                c.WorkspaceId,
                c.UserId
            });
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
        {
            string currentUser = "MODIFYTHIS";
            var timestamp = DateTime.UtcNow;


            foreach (var entry in ChangeTracker.Entries<AuditEntity>())
            {
                entry.Entity.UpdatedAt = timestamp;

                if (entry.State == EntityState.Added) entry.Entity.CreatedAt = timestamp;
                
                if (entry.Entity is Data.Entities.Task task)
                {
                    var list = await this.Lists
                                .Include(l => l.Workspace)
                                .FirstOrDefaultAsync(l => l.Id == task.ListId, cancellationToken);


                    if (list?.Workspace != null)
                    {
                        list.UpdatedAt = timestamp;
                        list.Workspace.UpdatedAt = timestamp;
                    }
                }
                else if (entry.Entity is Data.Entities.List list)
                {
                    var workspace = await this.Workspaces
                                .FirstOrDefaultAsync(w => w.Id == list.WorkspaceId, cancellationToken);

                    if (workspace != null && list != null)
                    {
                        list.UpdatedAt = timestamp;
                        workspace.UpdatedAt = timestamp;
                    }
                //update cascade when list/task is updated
                } else if (entry.Entity is Data.Entities.Request request)
                {
                    if (request.Status == Status.Accepted)
                    {
                        this.Add(new Friend() {
                            User1Id = request.SenderId,
                            User2Id = request.ReceiverId,
                            CreatedAt = timestamp,
                            UpdatedAt = timestamp,
                        });
                    }
                    if (request.Status != Status.Waiting) this.Remove(request);
                }
                //add/delete friend/request if request is accepted/declined
            }

            return await base.SaveChangesAsync(cancellationToken);
        }

    }
}
