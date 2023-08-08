using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Context
{
    public class FundooDBContext : DbContext
    {
        public FundooDBContext(DbContextOptions dbContextOptions) : base(dbContextOptions) { }

        public DbSet<UserEntity> Users { get; set; }
        public DbSet<NoteEntity> Notes { get; set; }
        public DbSet<LabelEntity> Labels { get; set; }
        public DbSet<CollabEntity> Collaborations { get; set; }
    }
}
