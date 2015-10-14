using System.Data.Entity;
using CoreFramework4.Model;
using System.Configuration;
using System.Data.Entity.Migrations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CoreFramework4.EF4_1.Model;

namespace CoreFramework4
{
    public class DbManager : DbContext
    {
        public DbManager(bool enableLazyLoading)
            : base(GetConnectionString())
        {
            EnableLazyLoading = enableLazyLoading;
        }

        public DbManager(string connectionString, bool enableLazyLoading) : base(connectionString)
        {
            EnableLazyLoading = enableLazyLoading;
            ConnectionString = connectionString;
        }

        public bool EnableLazyLoading 
        {
            get { return this.Configuration.LazyLoadingEnabled; }
            set { this.Configuration.LazyLoadingEnabled = value; }
        }

        public string ConnectionString { get; set; }

        public DbSet<Blog> Blog { get; set; }
        public DbSet<Comment> Comment { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Album> Album { get; set; }
        public DbSet<Photo> Photo { get; set; }
        public DbSet<Tutorial> Tutorial { get; set; }
        public DbSet<TutorialType> TutorialType { get; set; }
        public DbSet<TutorialCategory> TutorialCategory { get; set; }
        public DbSet<ChangeLog> ChangeLog { get; set; }
        public DbSet<SiteConfiguration> SiteConfiguration { get; set; }
        public DbSet<Game> Game { get; set; }

        public DbSet<Resume> Resume { get; set; }

        public DbSet<ResumeDetails> ResumeDetail { get; set; }
        private static string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["DefaultConnectionString"].ConnectionString;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tutorial>()
                        .HasKey(t => t.ID)
                        .Property(t => t.ID)
                        .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<Album>()
                        .HasKey(t => t.UID)
                        .Property(t => t.ID)
                        .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<Photo>()
                        .HasKey(t => t.UID)
                        .Property(t => t.ID)
                        .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<Comment>()
                        .HasKey(t => t.ID)
                        .Property(t => t.ID)
                        .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<Game>()
                        .HasKey(t => t.ID)
                        .Property(t => t.ID)
                        .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<Resume>()
                        .HasKey(t => t.ID);

            modelBuilder.Entity<ResumeDetails>()
                        .HasKey(t => t.ID);

            //modelBuilder.Entity<TutorialCategory>()
            //    .HasMany(c => c.Tutorials);

            //modelBuilder.Entity<Tutorial>().HasKey(t => t.UID).
            //            .HasRequired(t => t.TutorialCategory);

            //modelBuilder.Entity<TutorialType>()
            //            .HasMany(t => t.TutorialCategory);
        }
    }
}