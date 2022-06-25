using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ToDo_list.Models;

namespace ToDo_list.Data
{
    public class ToDo_listContext : DbContext
    {
        public ToDo_listContext (DbContextOptions<ToDo_listContext> options)
            : base(options)
        {
        }
        public DbSet<ToDoList> ToDoLists { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // modelBuilder.Entity<Leave>().HasOne(m => m.User).WithMany().OnDelete(DeleteBehavior.Restrict);
            //modelBuilder.Entity<Expense>().HasOne(m => m.AddedByUser).WithMany().OnDelete(DeleteBehavior.Restrict);
            //modelBuilder.Entity<ExpenseDetail>().HasOne(m => m.AddedByUser).WithMany().OnDelete(DeleteBehavior.Restrict);

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
            base.OnModelCreating(modelBuilder);


        }
    }
}
