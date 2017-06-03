using MUMScrum.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MUMScrum.Infrastructure.Core
{
    public class UnitOfWork:IUnitOfWork
    {
        private readonly IDatabaseFactory databaseFactory;
        private ApplicationDbContext dataContext;

        public UnitOfWork(IDatabaseFactory databaseFactory)
        {
            this.databaseFactory = databaseFactory;
        }

        protected ApplicationDbContext DataContext
        {
            get { return dataContext ?? (dataContext = databaseFactory.Get()); }
        }

        public void Commit()
        {
            using (var dbContextTransaction = DataContext.DataBaseInfo.BeginTransaction())
            {
                try
                {
                    DataContext.SaveChanges();
                    dbContextTransaction.Commit();
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException ex)
                {
                    dbContextTransaction.Rollback();
                }
            }
        }

        public async System.Threading.Tasks.Task<bool> CommitAsync()
        {
            try
            {
                await this.DataContext.SaveChangesAsync();
                return true;
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

        public bool TransactionCommit()
        {
            using (var dbContextTransaction = DataContext.DataBaseInfo.BeginTransaction())
            {
                try
                {
                    DataContext.SaveChanges();
                    dbContextTransaction.Commit();
                    return true;
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException ex)
                {
                    dbContextTransaction.Rollback();
                    var context = ((IObjectContextAdapter)dataContext).ObjectContext;
                    foreach (var change in dataContext.ChangeTracker.Entries())
                    {
                        if (change.State == EntityState.Modified)
                        {
                            context.Refresh(RefreshMode.StoreWins, change.Entity);
                        }
                        if (change.State == EntityState.Added)
                        {
                            context.Detach(change.Entity);
                        }
                        if (change.State == EntityState.Deleted)
                        {
                            context.Refresh(RefreshMode.StoreWins, change.Entity);
                        }
                    }
                    return false;
                }
                catch (Exception ex)
                {
                    dbContextTransaction.Rollback();
                    var context = ((IObjectContextAdapter)dataContext).ObjectContext;
                    foreach (var change in dataContext.ChangeTracker.Entries())
                    {
                        if (change.State == EntityState.Modified)
                        {
                            context.Refresh(RefreshMode.StoreWins, change.Entity);
                        }
                        if (change.State == EntityState.Added)
                        {
                            context.Detach(change.Entity);
                        }
                        if (change.State == EntityState.Deleted)
                        {
                            context.Refresh(RefreshMode.StoreWins, change.Entity);
                        }
                    }
                    throw ex;
                }
            }
        }
    }
}
