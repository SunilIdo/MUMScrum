using MUMScrum.Model.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MUMScrum.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() : base("name=ApplicationContext")
        {

        }
        public new DbEntityEntry Entry(object entity)
        {
            return base.Entry(entity);
        }

        public new DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class
        {
            return base.Entry<TEntity>(entity);
        }

        public override DbSet<TEntity> Set<TEntity>()
        {
            return base.Set<TEntity>();
        }

        public override DbSet Set(Type entityType)
        {
            return base.Set(entityType);
        }

        public Database DataBaseInfo
        {

            get
            {
                return base.Database;
            }
        }

        public IEnumerable<T> ExecuteProcedure<T>(string procedureName, params object[] parameters)
        {
            return Database.SqlQuery<T>(procedureName, parameters);
        }

        public Task<int> ExecuteSqlCommandAsync(string sqlQuery, params object[] parameters)
        {
            return Database.ExecuteSqlCommandAsync(sqlQuery, parameters);
        }

        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // This needs to go before the other rules!
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            //modelBuilder.Entity<ApplicationUser>().ToTable("ApplicationUser");
            //modelBuilder.Entity<ApplicationRole>().ToTable("ApplicationRole");
            //modelBuilder.Entity<ApplicationUserRole>().ToTable("ApplicationUserRole");
            //modelBuilder.Entity<ApplicationUserLogin>().ToTable("ApplicationUserLogin");
            //modelBuilder.Entity<ApplicationUserClaim>().ToTable("ApplicationUserClaim");
        }


        public override Task<int> SaveChangesAsync()
        {
            try
            {
                var __ = base.SaveChangesAsync();
                return __;
            }
            catch (DbEntityValidationException ex)
            {
                throw ex;
            }
        }

        public override int SaveChanges()
        {
            var __ = base.SaveChanges();
            return __;
        }
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public void Commit()
        {
            try
            {
                this.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<ReleaseBacklog> ReleaseBacklogs { get; set; }
        public virtual DbSet<Sprint> Sprints { get; set; }
        public virtual DbSet<UserStory> UserStorys { get; set; }
        public virtual DbSet<WorkLog> WorkLogs { get; set; }
        public virtual DbSet<LoginUser> LoginUsers { get; set; }
        

    }
}
