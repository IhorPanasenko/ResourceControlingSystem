﻿using Microsoft.EntityFrameworkCore;
using ResourceControlingAPI.Models;

namespace ResourceControlingAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Renter> Renters { get; set; }
    }
}
