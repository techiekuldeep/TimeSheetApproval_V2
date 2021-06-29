using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using TimeSheetApproval.Application.Interfaces;
using TimeSheetApproval.Domain.Common;
using TimeSheetApproval.Domain.Entities;
using TimeSheetApproval.Persistence.Extensions;

namespace TimeSheetApproval.Persistence.Contexts
{
    public class ApplicationDbContext : DbContext
    {
        private readonly IAuthenticatedUserService _authenticatedUser;
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IAuthenticatedUserService authenticatedUser) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            _authenticatedUser = authenticatedUser;
        }
        public DbSet<People> People { get; set; }
        public DbSet<TimesheetsStatusTypes> TimesheetsStatusTypes { get; set; }
        public DbSet<Timesheet> Timesheet { get; set; }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedDateTime = DateTime.Now;
                        entry.Entity.CreatedBy = entry.Entity.UpdatedBy = _authenticatedUser.UserFullName;
                        entry.Entity.UpdatedDateTime = DateTime.Now;
                        break;
                    case EntityState.Modified:
                        entry.Entity.UpdatedDateTime = DateTime.Now;
                        entry.Entity.UpdatedBy = _authenticatedUser.UserFullName;
                        break;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var property in modelBuilder.Model.GetEntityTypes()
            .SelectMany(t => t.GetProperties())
            .Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?)))
            {
                property.SetColumnType("decimal(18,6)");
            }
            modelBuilder.Entity<People>().HasKey(p => p.PeopleId);
            modelBuilder.Entity<TimesheetsStatusTypes>().HasKey(p => p.TssTypeId);
            modelBuilder.Entity<Timesheet>().HasKey(p => p.TimesheetId);

            modelBuilder.Entity<Timesheet>().HasOne(a => a.People)
            .WithMany(c => c.Timesheets)
            .HasForeignKey(a => a.PeopleId).OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Timesheet>().HasOne(a => a.TimesheetsStatusTypes).WithOne(c => c.Timesheets)
             .HasPrincipalKey<TimesheetsStatusTypes>(x => x.TssTypeId)
             .HasForeignKey<Timesheet>(a => a.TssTypeId).OnDelete(DeleteBehavior.NoAction);

            base.OnModelCreating(modelBuilder);
            modelBuilder.FieldValidations();
            modelBuilder.SeedData();
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
