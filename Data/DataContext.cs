namespace Data.Models
{
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

            modelBuilder.Configurations.Add(new DictionaryDataEntityConfig());
            modelBuilder.Configurations.Add(new NodeConfig());
            modelBuilder.Configurations.Add(new ContractorConfig());
            modelBuilder.Configurations.Add(new LinkConfig());
            modelBuilder.Configurations.Add(new CountryConfig()); 
        }

        // Добавьте DbSet для каждого типа сущности, который требуется включить в модель. Дополнительные сведения 
        // о настройке и использовании модели Code First см. в статье http://go.microsoft.com/fwlink/?LinkId=390109.

        public virtual DbSet<DictionaryDataEntity> DictionaryDataEntities { get; set; }
        public virtual DbSet<Node> Nodes { get; set; }
        public virtual DbSet<Contractor> Contractors { get; set; }
        public virtual DbSet<Link> Links { get; set; }
        //public virtual DbSet<Country> Countries { get; set; }
    }

}