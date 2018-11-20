﻿namespace Data.Models
{
    using Core;
    using System.Data.Entity;

    public class DataContext : DbContext
    {
        public DataContext()
            : base("name=DefaultConnection")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Configurations.Add(new NodeConfig());
            modelBuilder.Configurations.Add(new SubjectConfig());
            modelBuilder.Configurations.Add(new ContractorConfig());
            //modelBuilder.Configurations.Add(new LinkConfig());
        }

        public virtual DbSet<Node> Nodes { get; set; }
        public virtual DbSet<Subject> Subjects { get; set; }
        public virtual DbSet<Contractor> Contractors { get; set; }
    }

}