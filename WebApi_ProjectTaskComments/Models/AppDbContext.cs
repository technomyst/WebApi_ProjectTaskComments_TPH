using Microsoft.EntityFrameworkCore;
using WebApi_ProjectTaskComments.Models.Enums;

namespace WebApi_ProjectTaskComments.Models
{
    public class AppDbContext:DbContext
    {
        public DbSet<Project> Projects { get; set; } = null!;
        public DbSet<ProjectTask> ProjectTasks { get; set; } = null!;
        public DbSet<Comment> Comments { get; set; } = null!;
        public DbSet<ProjectComment> ProjectComments { get; set; } = null!;
        public DbSet<ProjectTaskComment> ProjectTaskComments { get; set; } = null!;
        public AppDbContext(DbContextOptions<AppDbContext> opts) : base(opts) 
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            modelBuilder.Entity<Comment>()
            .HasDiscriminator<CommentSourceTypeEnum>(e => e.SourceType)
            .HasValue<Comment>(CommentSourceTypeEnum.Undefined)
            .HasValue<ProjectComment>(CommentSourceTypeEnum.Project)
            .HasValue<ProjectTaskComment>(CommentSourceTypeEnum.ProjectTask);

            //modelBuilder.Entity<Comment>()
            //   .Property(b => b.SourceId)
            //   .HasColumnName("SourceId");

            modelBuilder.Entity<ProjectComment>()
            .Property(b => b.ProjectId)
            .HasColumnName("SourceId"); 

            modelBuilder.Entity<ProjectTaskComment>()
                .Property(b => b.ProjectTaskId)
                .HasColumnName("SourceId");


            modelBuilder.Entity<Project>().HasQueryFilter(p => !p.IsDeleted);
            modelBuilder.Entity<ProjectTask>().HasQueryFilter(pt => !pt.IsDeleted);
            modelBuilder.Entity<Comment>().HasQueryFilter(c => !c.IsDeleted);

        }


        
    }
}
