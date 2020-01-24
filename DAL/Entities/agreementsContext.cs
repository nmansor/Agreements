
using DAL.NoneEntity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace DAL.Entities
{

    public class AgreementsContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public AgreementsContext(DbContextOptions<AgreementsContext> options)
          : base(options)
        {

        }
        public virtual DbSet<AgreementTypes> AgreementTypes { get; set; }
        public virtual DbSet<CityLookUp> Cities { get; set; }
        public virtual DbSet<CourtLookUp> Courts { get; set; }
        public virtual DbSet<QualificationDocumentLookup> QualificationDocumentLookup { get; set; }
        public virtual DbSet<Agreement> Agreements { get; set; }
        public virtual DbSet<Notarizer> Notarizers { get; set; }
        public virtual DbSet<Document> Documents { get; set; }

        public virtual DbSet<QualificationDocument> QualificationDocuments { get; set; }
        public virtual DbSet<VehicleSalesContract> VehicleSalesContracts { get; set; }

        public virtual DbSet<NotarizerStatusLookup> NotarizerStatusLookup { get; set; }

     //     public virtual DbSet<DashboardData> DashboardData { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            if (!optionsBuilder.IsConfigured)
            {
                // #warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                //  optionsBuilder.UseSqlServer("Server=DESKTOP-S0EGQAJ;Database=agreements;Integrated Security=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Agreement>(entity =>
            {
                entity.HasKey(e => e.AgreementId);

                entity.HasIndex(e => e.DocumentId);

                entity.HasIndex(e => e.NotarizerId);

                entity.HasOne(d => d.Document)
                    .WithMany(p => p.Agreements)
                    .HasForeignKey(d => d.DocumentId);

                entity.HasOne(d => d.Notarizer)
                    .WithMany(p => p.Agreements)
                    .HasForeignKey(d => d.NotarizerId);
            });


            modelBuilder.Entity<Notarizer>(entity =>
            {
                entity.HasKey(n => n.NotarizerId);
                entity.HasMany(a => a.Agreements);
                entity.HasOne(d => d.City);
                entity.HasOne(d => d.Court);
            });

        }
    }
}

//public partial class AgreementsContext : IdentityDbContext<User, IdentityRole<int>, int>
//{
//    public AgreementsContext(DbContextOptions<AgreementsContext> options)
//        : base(options)
//    {
//    }
//    public virtual DbSet<AgreementTypes> AgreementTypes { get; set; }
//    public virtual DbSet<Agreement> Agreements { get; set; }



//    public virtual DbSet<User> Users { get; set; }
//    public virtual DbSet<Cities> Cities { get; set; }
//    public virtual DbSet<Courts> Courts { get; set; }
//    public virtual DbSet<Document> Documents { get; set; }
//    public virtual DbSet<NotarizerQualificationDocument> NotarizerQualificationDocuments { get; set; }
//    public virtual DbSet<Notarizer> Notarizers { get; set; }
//    public virtual DbSet<People> People { get; set; }
//    public virtual DbSet<QualificationDocumentLookup> QualificationDocumentLookup { get; set; }
//    public virtual DbSet<VehicleSalesContracts> VehicleSalesContracts { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//    {

//        //            if (!optionsBuilder.IsConfigured)
//        //            {
//        //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
//        //                optionsBuilder.UseSqlServer("Server=DESKTOP-S0EGQAJ;Database=agreements;Integrated Security=true;");
//        //            }
//    }

//    protected override void OnModelCreating(ModelBuilder modelBuilder)
//    {
//        base.OnModelCreating(modelBuilder);

//        //modelBuilder.Entity<AspNetRoleClaim>(entity =>
//        //{
//        //    entity.HasIndex(e => e.RoleId);

//        //    entity.Property(e => e.RoleId).IsRequired();

//        //    entity.HasOne(d => d.Role)
//        //        .WithMany(p => p.AspNetRoleClaims)
//        //        .HasForeignKey(d => d.RoleId);
//        //});

//        //modelBuilder.Entity<AspNetRole>(entity =>
//        //{
//        //    entity.HasIndex(e => e.NormalizedName)
//        //        .HasName("RoleNameIndex")
//        //        .IsUnique()
//        //        .HasFilter("([NormalizedName] IS NOT NULL)");

//        //    entity.Property(e => e.Name).HasMaxLength(256);

//        //    entity.Property(e => e.NormalizedName).HasMaxLength(256);
//        //});



//        //modelBuilder.Entity<AspNetUserClaim>(entity =>
//        //{
//        //    entity.HasIndex(e => e.UserId);

//        //    entity.Property(e => e.UserId).IsRequired();

//        //    entity.HasOne(d => d.User)
//        //        .WithMany(p => p.AspNetUserClaims)
//        //        .HasForeignKey(d => d.UserId);
//        //});

//        //modelBuilder.Entity<AspNetUserLogin>(entity =>
//        //{
//        //    entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

//        //    entity.HasIndex(e => e.UserId);

//        //    entity.Property(e => e.LoginProvider).HasMaxLength(128);

//        //    entity.Property(e => e.ProviderKey).HasMaxLength(128);

//        //    entity.Property(e => e.UserId).IsRequired();

//        //    entity.HasOne(d => d.User)
//        //        .WithMany(p => p.AspNetUserLogins)
//        //        .HasForeignKey(d => d.UserId);
//        //});

//        //modelBuilder.Entity<AspNetUserRole>(entity =>
//        //{
//        //    entity.HasKey(e => new { e.UserId, e.RoleId });

//        //    entity.HasIndex(e => e.RoleId);

//        //    entity.HasOne(d => d.Role)
//        //        .WithMany(p => p.AspNetUserRoles)
//        //        .HasForeignKey(d => d.RoleId);

//        //    entity.HasOne(d => d.User)
//        //        .WithMany(p => p.AspNetUserRoles)
//        //        .HasForeignKey(d => d.UserId);
//        //});


//        //modelBuilder.Entity<AspNetUserToken>(entity =>
//        //{
//        //    entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

//        //    entity.Property(e => e.LoginProvider).HasMaxLength(128);

//        //    entity.Property(e => e.Name).HasMaxLength(128);

//        //    entity.HasOne(d => d.User)
//        //        .WithMany(p => p.AspNetUserTokens)
//        //        .HasForeignKey(d => d.UserId);
//        //});

//        //modelBuilder.Entity<AspNetUser>(entity =>
//        //{
//        //    entity.HasIndex(e => e.NormalizedEmail)
//        //        .HasName("EmailIndex");

//        //    entity.HasIndex(e => e.NormalizedUserName)
//        //        .HasName("UserNameIndex")
//        //        .IsUnique()
//        //        .HasFilter("([NormalizedUserName] IS NOT NULL)");

//        //    entity.Property(e => e.Email).HasMaxLength(256);

//        //    entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

//        //    entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

//        //    entity.Property(e => e.UserName).HasMaxLength(256);
//        //});

//       //  OnModelCreatingPartial(modelBuilder);

//        //modelBuilder.HasAnnotation("ProductVersion", "2.2.3-servicing-35854");

//        //modelBuilder.Entity<Agreement>(entity =>
//        //{
//        //    entity.HasKey(e => e.AgreementId);

//        //    entity.HasIndex(e => e.DocumentId);

//        //    entity.HasIndex(e => e.NotarizerId);

//        //    entity.Property(e => e.AgreementTypeId).HasDefaultValueSql("(CONVERT([smallint],(0)))");

//        //    entity.Property(e => e.NotarizerId).HasDefaultValueSql("(CONVERT([smallint],(0)))");

//        //    entity.HasOne(d => d.Document)
//        //        .WithMany(p => p.Agreements)
//        //        .HasForeignKey(d => d.DocumentId);

//        //    entity.HasOne(d => d.Notarizer)
//        //        .WithMany(p => p.Agreements)
//        //        .HasForeignKey(d => d.NotarizerId);
//        //});


//        //modelBuilder.Entity<Document>(entity =>
//        //{
//        //    entity.HasKey(e => e.DocumentId);
//        //});

//        //modelBuilder.Entity<NotarizerQualificationDocument>(entity =>
//        //{
//        //    entity.HasIndex(e => e.NotarizerId);

//        //    entity.Property(e => e.DocumentName).HasColumnName("documentName");

//        //    entity.HasOne(d => d.Notarizer)
//        //        .WithMany(p => p.QulificationDocuments)
//        //        .HasForeignKey(d => d.NotarizerId);
//        //});

//        //modelBuilder.Entity<Notarizer>(entity =>
//        //{
//        //    entity.HasKey(e => e.NotarizerId);

//        //    entity.Property(e => e.Address).HasMaxLength(100);

//        //    entity.Property(e => e.FirstName).HasMaxLength(60);
//        //});


//        //modelBuilder.Entity<Notarizer>(entity =>
//        //{
//        //    entity.HasKey(n => n.NotarizerId)
//        //   .HasOne(d => d.City)
//        //  .HasOne(d => d.Court)
//        //   .WithMany(d => d.Agreements);
//        //});

//        //modelBuilder.Entity<VehicleSalesContracts>(entity =>
//        //{
//        //    entity.HasIndex(e => e.BuyerNameId);

//        //    entity.HasIndex(e => e.SellerNameId);

//        //    entity.Property(e => e.Vinbumber).HasColumnName("VINBumber");

//        //    entity.HasOne(d => d.BuyerName)
//        //        .WithMany(p => p.VehicleSalesContractsBuyerName)
//        //        .HasForeignKey(d => d.BuyerNameId);

//        //    entity.HasOne(d => d.SellerName)
//        //        .WithMany(p => p.VehicleSalesContractsSellerName)
//        //        .HasForeignKey(d => d.SellerNameId);
//        //});
//    }
//}
//}
