using EventApp.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventApp.Context
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
        
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Attendee> Attendees { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Events> Events { get; set; }
        public DbSet<EventAttendee> EventAttendees { get; set; }
        public DbSet<EventCategory> EventCategories { get; set; }
        public DbSet<EventOrganizer> EventOrganizers { get; set; }
        public DbSet<Organizer> Organizers { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<AttendeeCategory> AttendeeCategories { get; set; }
    }
}
