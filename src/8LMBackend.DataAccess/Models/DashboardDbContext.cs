using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace _8LMBackend.DataAccess.Models
{
    public partial class DevelopmentDbContext : DbContext
    {
        public virtual DbSet<distributors> distributors { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
            //optionsBuilder.UseMySql(@"server=core.mark8.media;userid=rails;pwd=oblisk1;port=3306;database=dashboard_development;sslmode=none;");
            //optionsBuilder.UseMySql(@"server=localhost;userid=root;pwd=oblisk1;port=3606;database=testdb;sslmode=none;");
            optionsBuilder.UseMySql(@"server=localhost;userid=root;pwd=oblisk1;port=3306;database=dashboard_development;sslmode=none;");
            //For debugging;
            //optionsBuilder.EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<distributors>(entity =>
            {
                entity.Property(e => e.id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.company).HasColumnType("varchar(255)");
                entity.Property(e => e.phone).HasColumnType("varchar(255)");
                entity.Property(e => e.mailing_state).HasColumnType("varchar(255)");
            });
        }
    }

    public partial class DashboardDbContext : DbContext
    {
        public virtual DbSet<AuthorizeNetcustomerProfile> AuthorizeNetcustomerProfile { get; set; }
        public virtual DbSet<AuthorizeNettransaction> AuthorizeNettransaction { get; set; }
        public virtual DbSet<Campaign> Campaign { get; set; }
        public virtual DbSet<CampaignCategory> CampaignCategory { get; set; }
        public virtual DbSet<CampaignEmail> CampaignEmail { get; set; }
        public virtual DbSet<CampaignEpage> CampaignEpage { get; set; }
        public virtual DbSet<CampaignProduct> CampaignProduct { get; set; }
        public virtual DbSet<CampaignShare> CampaignShare { get; set; }
        public virtual DbSet<CampaignTag> CampaignTag { get; set; }
        public virtual DbSet<ControlStat> ControlStat { get; set; }
        public virtual DbSet<Currency> Currency { get; set; }
        public virtual DbSet<Entity> Entity { get; set; }
        public virtual DbSet<EntityStatus> EntityStatus { get; set; }
        public virtual DbSet<EntityType> EntityType { get; set; }
        public virtual DbSet<Invoice> Invoice { get; set; }
        public virtual DbSet<Package> Package { get; set; }
        public virtual DbSet<PackageRatePlan> PackageRatePlan { get; set; }
        public virtual DbSet<PackageReferenceCode> PackageReferenceCode { get; set; }
        public virtual DbSet<PackageReferenceExtendCode> PackageReferenceExtendCode { get; set; }
        public virtual DbSet<PackageReferenceServiceCode> PackageReferenceServiceCode { get; set; }
        public virtual DbSet<PackageService> PackageService { get; set; }
        public virtual DbSet<PageCampaign> PageCampaign { get; set; }
        public virtual DbSet<PageStatistic> PageStatistic { get; set; }
        public virtual DbSet<PageTag> PageTag { get; set; }
        public virtual DbSet<Pages> Pages { get; set; }
        public virtual DbSet<PageControl> PageControl { get; set; }
        public virtual DbSet<PaymentSetting> PaymentSetting { get; set; }
        public virtual DbSet<PromoCode> PromoCode { get; set; }
        public virtual DbSet<PromoProduct> PromoProduct { get; set; }
        public virtual DbSet<PromoSupplier> PromoSupplier { get; set; }
        public virtual DbSet<RelayAuthorizeNetresponse> RelayAuthorizeNetresponse { get; set; }
        public virtual DbSet<RoleFunction> RoleFunction { get; set; }
        public virtual DbSet<SecurityFunction> SecurityFunction { get; set; }
        public virtual DbSet<SecurityRole> SecurityRole { get; set; }
        public virtual DbSet<Service> Service { get; set; }
        public virtual DbSet<ServiceFunction> ServiceFunction { get; set; }
        public virtual DbSet<Subscription> Subscription { get; set; }
        public virtual DbSet<SubscriptionExtraService> SubscriptionExtraService { get; set; }
        public virtual DbSet<Tags> Tags { get; set; }
        public virtual DbSet<UserRole> UserRole { get; set; }
        public virtual DbSet<UserToken> UserToken { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        public virtual DbSet<RoleService> RoleService { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
            //optionsBuilder.UseMySql(@"server=localhost;userid=core;pwd=Obl1skc3p0!;port=3306;database=dashboard_development;sslmode=none;");
            //optionsBuilder.UseMySql(@"server=localhost;userid=root;pwd=oblisk1;port=3606;database=testdb;sslmode=none;");
            optionsBuilder.UseMySql(@"server=localhost;userid=root;pwd=oblisk1;port=3306;database=railsapi_development;sslmode=none;");
            //For debugging;
            //optionsBuilder.EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AuthorizeNetcustomerProfile>(entity =>
            {
                entity.HasKey(e => e.CustomerProfileId)
                    .HasName("PK_AuthorizeNETCustomerProfile");

                entity.ToTable("AuthorizeNETCustomerProfile");

                entity.HasIndex(e => e.UserId)
                    .HasName("FKAuthorizeNETCustomerProfileUserID");

                entity.Property(e => e.CustomerProfileId)
                    .HasColumnName("CustomerProfileID")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.PaymentProfileId)
                    .HasColumnName("PaymentProfileID")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.UserId)
                    .HasColumnName("UserID")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AuthorizeNetcustomerProfile)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FKAuthorizeNETCustomerProfileUserID");
            });

            modelBuilder.Entity<AuthorizeNettransaction>(entity =>
            {
                entity.ToTable("AuthorizeNETTransaction");

                entity.HasIndex(e => e.CustomerProfileId)
                    .HasName("FKAuthorizeNETTransactionCustomerProfileID");

                entity.HasIndex(e => e.InvoiceId)
                    .HasName("FKAuthorizeNETTransactionInvoiceID");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.Amount).HasColumnType("int(11)");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.CustomerProfileId)
                    .HasColumnName("CustomerProfileID")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.InvoiceId)
                    .HasColumnName("InvoiceID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.MerchantName)
                    .IsRequired()
                    .HasColumnType("varchar(32)");

                entity.Property(e => e.MerchantTransactionKey)
                    .IsRequired()
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.PaymentProfileId)
                    .HasColumnName("PaymentProfileID")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.ResponseCode).HasColumnType("varchar(32)");

                entity.Property(e => e.ResponseResultCode).HasColumnType("varchar(32)");

                entity.Property(e => e.ResponseText).HasColumnType("varchar(32)");

                entity.Property(e => e.TransactionResponseAccountNumber)
                    .HasColumnName("transactionResponseAccountNumber")
                    .HasColumnType("varchar(32)");

                entity.Property(e => e.TransactionResponseAccountType)
                    .HasColumnName("transactionResponseAccountType")
                    .HasColumnType("varchar(32)");

                entity.Property(e => e.TransactionResponseAuthCode)
                    .HasColumnName("transactionResponseAuthCode")
                    .HasColumnType("int(11)");

                entity.Property(e => e.TransactionResponseAvsresultCode)
                    .HasColumnName("transactionResponseAVSResultCode")
                    .HasColumnType("varchar(32)");

                entity.Property(e => e.TransactionResponseCavvresultCode)
                    .HasColumnName("transactionResponseCAVVResultCode")
                    .HasColumnType("varchar(32)");

                entity.Property(e => e.TransactionResponseCvvresultCode)
                    .HasColumnName("transactionResponseCVVResultCode")
                    .HasColumnType("varchar(32)");

                entity.Property(e => e.TransactionResponseMessageCode)
                    .HasColumnName("transactionResponseMessageCode")
                    .HasColumnType("int(11)");

                entity.Property(e => e.TransactionResponseMessageDescription)
                    .HasColumnName("transactionResponseMessageDescription")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.TransactionResponseRefTransId)
                    .HasColumnName("transactionResponseRefTransID")
                    .HasColumnType("varchar(32)");

                entity.Property(e => e.TransactionResponseResponseCode)
                    .HasColumnName("transactionResponseResponseCode")
                    .HasColumnType("int(11)");

                entity.Property(e => e.TransactionResponseTestRequest)
                    .HasColumnName("transactionResponseTestRequest")
                    .HasColumnType("int(11)");

                entity.Property(e => e.TransactionResponseTransHash)
                    .HasColumnName("transactionResponseTransHash")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.TransactionResponseTransId)
                    .HasColumnName("transactionResponseTransID")
                    .HasColumnType("mediumtext");

                entity.Property(e => e.TransactionType)
                    .IsRequired()
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.HasOne(d => d.CustomerProfile)
                    .WithMany(p => p.AuthorizeNettransaction)
                    .HasForeignKey(d => d.CustomerProfileId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FKAuthorizeNETTransactionCustomerProfileID");

                entity.HasOne(d => d.Invoice)
                    .WithMany(p => p.AuthorizeNettransaction)
                    .HasForeignKey(d => d.InvoiceId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FKAuthorizeNETTransactionInvoiceID");
            });

            modelBuilder.Entity<Campaign>(entity =>
            {
                entity.HasIndex(e => e.CategoryId)
                    .HasName("FKCampaignCategoryID");

                entity.HasIndex(e => e.CreatedBy)
                    .HasName("FKCampaignCreatedBy");

                entity.HasIndex(e => e.StatusId)
                    .HasName("FKCampaignStatusID");

                entity.HasIndex(e => new { e.Name, e.CategoryId, e.CreatedBy })
                    .HasName("Name")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CategoryId)
                    .HasColumnName("CategoryID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CreatedBy).HasColumnType("int(11)");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Description).HasColumnType("varchar(4096)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.StatusId)
                    .HasColumnName("StatusID")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Campaign)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FKCampaignCategoryID");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.Campaign)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FKCampaignCreatedBy");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Campaign)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FKCampaignStatusID");
            });

            modelBuilder.Entity<CampaignCategory>(entity =>
            {
                entity.HasIndex(e => e.CreatedBy)
                    .HasName("FKCampaignCategoryCreatedBy");

                entity.HasIndex(e => new { e.ParentCategoryId, e.Name, e.CreatedBy })
                    .HasName("ParentCategoryID")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CreatedBy).HasColumnType("int(11)");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Description).HasColumnType("varchar(4096)");

                entity.Property(e => e.IsActual).HasColumnType("bit(1)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.ParentCategoryId)
                    .HasColumnName("ParentCategoryID")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.CampaignCategory)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FKCampaignCategoryCreatedBy");
            });

            modelBuilder.Entity<CampaignEmail>(entity =>
            {
                entity.HasKey(e => new { e.CampaignId, e.EmailId })
                    .HasName("PK_CampaignEmail");

                entity.Property(e => e.CampaignId)
                    .HasColumnName("CampaignID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.EmailId)
                    .HasColumnName("EmailID")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.Campaign)
                    .WithMany(p => p.CampaignEmail)
                    .HasForeignKey(d => d.CampaignId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FKCampaignEmailCampaignID");
            });

            modelBuilder.Entity<CampaignEpage>(entity =>
            {
                entity.HasKey(e => new { e.CampaignId, e.EpageId })
                    .HasName("PK_CampaignEPage");

                entity.ToTable("CampaignEPage");

                entity.Property(e => e.CampaignId)
                    .HasColumnName("CampaignID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.EpageId)
                    .HasColumnName("EPageID")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.Campaign)
                    .WithMany(p => p.CampaignEpage)
                    .HasForeignKey(d => d.CampaignId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FKCampaignEPageCampaignID");
            });

            modelBuilder.Entity<CampaignProduct>(entity =>
            {
                entity.HasKey(e => new { e.CampaignId, e.ProductId })
                    .HasName("PK_CampaignProduct");

                entity.Property(e => e.CampaignId)
                    .HasColumnName("CampaignID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ProductId)
                    .HasColumnName("ProductID")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.Campaign)
                    .WithMany(p => p.CampaignProduct)
                    .HasForeignKey(d => d.CampaignId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FKCampaignProductCampaignID");
            });

            modelBuilder.Entity<CampaignShare>(entity =>
            {
                entity.HasKey(e => new { e.CampaignId, e.UserId })
                    .HasName("PK_CampaignShare");

                entity.HasIndex(e => e.CreatedBy)
                    .HasName("FKCampaignShareCreatedBy");

                entity.HasIndex(e => e.UserId)
                    .HasName("FKCampaignShareUserID");

                entity.Property(e => e.CampaignId)
                    .HasColumnName("CampaignID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.UserId)
                    .HasColumnName("UserID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CreatedBy).HasColumnType("int(11)");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.HasOne(d => d.Campaign)
                    .WithMany(p => p.CampaignShare)
                    .HasForeignKey(d => d.CampaignId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FKCampaignShareCampaignID");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.CampaignShareCreatedByNavigation)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FKCampaignShareCreatedBy");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.CampaignShareUser)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FKCampaignShareUserID");
            });

            modelBuilder.Entity<CampaignTag>(entity =>
            {
                entity.HasKey(e => new { e.CampaignId, e.TagId })
                    .HasName("PK_CampaignTag");

                entity.HasIndex(e => e.TagId)
                    .HasName("FKCampaignTagTagID");

                entity.Property(e => e.CampaignId)
                    .HasColumnName("CampaignID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.TagId)
                    .HasColumnName("TagID")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.Campaign)
                    .WithMany(p => p.CampaignTag)
                    .HasForeignKey(d => d.CampaignId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FKCampaignTagCampaignID");

                entity.HasOne(d => d.Tag)
                    .WithMany(p => p.CampaignTag)
                    .HasForeignKey(d => d.TagId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FKCampaignTagTagID");
            });

            modelBuilder.Entity<ControlStat>(entity =>
            {
                entity.HasIndex(e => e.PageId)
                    .HasName("FKControlStatPageID");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("char(32)");

                entity.Property(e => e.IsActive).HasColumnType("bit(1)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.PageId)
                    .HasColumnName("PageID")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.Page)
                    .WithMany(p => p.ControlStat)
                    .HasForeignKey(d => d.PageId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FKControlStatPageID");
            });

            modelBuilder.Entity<Currency>(entity =>
            {
                entity.HasIndex(e => e.Code)
                    .HasName("Code")
                    .IsUnique();

                entity.HasIndex(e => e.Mark)
                    .HasName("Mark")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasColumnType("char(3)");

                entity.Property(e => e.Mark)
                    .IsRequired()
                    .HasColumnType("char(2)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(255)");
            });

            modelBuilder.Entity<Entity>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(255)");
            });

            modelBuilder.Entity<EntityStatus>(entity =>
            {
                entity.HasIndex(e => e.EntityId)
                    .HasName("FKEntityStatusEntityID");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.EntityId)
                    .HasColumnName("EntityID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(255)");

                entity.HasOne(d => d.Entity)
                    .WithMany(p => p.EntityStatus)
                    .HasForeignKey(d => d.EntityId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FKEntityStatusEntityID");
            });

            modelBuilder.Entity<EntityType>(entity =>
            {
                entity.HasIndex(e => e.EntityId)
                    .HasName("FKEntityTypeEntityID");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.EntityId)
                    .HasColumnName("EntityID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(255)");

                entity.HasOne(d => d.Entity)
                    .WithMany(p => p.EntityType)
                    .HasForeignKey(d => d.EntityId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FKEntityTypeEntityID");
            });

            modelBuilder.Entity<Invoice>(entity =>
            {
                entity.HasIndex(e => e.PackageRatePlanId)
                    .HasName("FKInvoicePackageRatePlanID");

                entity.HasIndex(e => e.StatusId)
                    .HasName("FKInvoiceStatusID");

                entity.HasIndex(e => e.UserId)
                    .HasName("FKInvoiceUserID");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Amount).HasColumnType("int(11)");

                entity.Property(e => e.AmountDue).HasColumnType("int(11)");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Discount).HasColumnType("int(11)");

                entity.Property(e => e.PackageRatePlanId)
                    .HasColumnName("PackageRatePlanID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ReferenceCode).HasColumnType("varchar(255)");

                entity.Property(e => e.StatusId)
                    .HasColumnName("StatusID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.UserId)
                    .HasColumnName("UserID")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.PackageRatePlan)
                    .WithMany(p => p.Invoice)
                    .HasForeignKey(d => d.PackageRatePlanId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FKInvoicePackageRatePlanID");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Invoice)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FKInvoiceStatusID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Invoice)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FKInvoiceUserID");
            });

            modelBuilder.Entity<Package>(entity =>
            {
                entity.HasIndex(e => e.CreatedBy)
                    .HasName("FKPackageCreatedBy");

                entity.HasIndex(e => e.StatusId)
                    .HasName("FKPackageStatusID");

                entity.HasIndex(e => new { e.UserTypeId, e.Name })
                    .HasName("UserTypeID")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CreatedBy).HasColumnType("int(11)");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnType("varchar(4096)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.PaletteId)
                    .HasColumnName("PaletteID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.StatusId)
                    .HasColumnName("StatusID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.UserTypeId)
                    .HasColumnName("UserTypeID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.VideoURL)
                    .IsRequired()
                    .HasColumnType("varchar(4096)");

                entity.Property(e => e.SortOrder)
                    .IsRequired()
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.Package)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FKPackageCreatedBy");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Package)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FKPackageStatusID");

                entity.HasOne(d => d.UserType)
                    .WithMany(p => p.Package)
                    .HasForeignKey(d => d.UserTypeId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FKPackageUserTypeID");
            });

            modelBuilder.Entity<PackageRatePlan>(entity =>
            {
                entity.HasIndex(e => e.CurrencyId)
                    .HasName("FKPackageCurrencyID");

                entity.HasIndex(e => new { e.PackageId, e.DurationInMonths })
                    .HasName("PackageID")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CurrencyId)
                    .HasColumnName("CurrencyID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.DurationInMonths).HasColumnType("int(11)");

                entity.Property(e => e.EmailLimitBroadcast).HasColumnType("int(11)");
                entity.Property(e => e.EmailLimitAddress).HasColumnType("int(11)");

                entity.Property(e => e.PackageId)
                    .HasColumnName("PackageID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Price).HasColumnType("int(11)");

                entity.HasOne(d => d.Currency)
                    .WithMany(p => p.PackageRatePlan)
                    .HasForeignKey(d => d.CurrencyId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FKPackageCurrencyID");

                entity.HasOne(d => d.Package)
                    .WithMany(p => p.PackageRatePlan)
                    .HasForeignKey(d => d.PackageId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FKPackagePackageID");
            });

            modelBuilder.Entity<PackageReferenceCode>(entity =>
            {
                entity.HasKey(e => new { e.PackageRatePlanId, e.ReferenceCode })
                    .HasName("PK_PackageReferenceCode");

                entity.Property(e => e.PackageRatePlanId)
                    .HasColumnName("PackageRatePlanID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ReferenceCode).HasColumnType("varchar(32)");

                entity.Property(e => e.IsFixed).HasColumnType("bit(1)");

                entity.Property(e => e.Value).HasColumnType("int(11)");

                entity.HasOne(d => d.PackageRatePlan)
                    .WithMany(p => p.PackageReferenceCode)
                    .HasForeignKey(d => d.PackageRatePlanId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FKPackageReferenceCodePackageRatePlanID");
            });

            modelBuilder.Entity<PackageReferenceExtendCode>(entity =>
            {
                entity.HasKey(e => new { e.PackageRatePlanId, e.ReferenceCode })
                    .HasName("PK_PackageReferenceExtendCode");

                entity.Property(e => e.PackageRatePlanId)
                    .HasColumnName("PackageRatePlanID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ReferenceCode).HasColumnType("varchar(32)");

                entity.Property(e => e.Months).HasColumnType("int(11)");

                entity.HasOne(d => d.PackageRatePlan)
                    .WithMany(p => p.PackageReferenceExtendCode)
                    .HasForeignKey(d => d.PackageRatePlanId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FKPackageReferenceExtendCodePackageRatePlanID");
            });

            modelBuilder.Entity<PackageReferenceServiceCode>(entity =>
            {
                entity.HasKey(e => new { e.PackageRatePlanId, e.ReferenceCode })
                    .HasName("PK_PackageReferenceServiceCode");

                entity.HasIndex(e => e.ServiceId)
                    .HasName("FKPackageReferenceServiceCodeServiceID");

                entity.Property(e => e.PackageRatePlanId)
                    .HasColumnName("PackageRatePlanID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ReferenceCode).HasColumnType("varchar(32)");

                entity.Property(e => e.ServiceId)
                    .HasColumnName("ServiceID")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.PackageRatePlan)
                    .WithMany(p => p.PackageReferenceServiceCode)
                    .HasForeignKey(d => d.PackageRatePlanId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FKPackageReferenceServiceCodePackageRatePlanID");

                entity.HasOne(d => d.Service)
                    .WithMany(p => p.PackageReferenceServiceCode)
                    .HasForeignKey(d => d.ServiceId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FKPackageReferenceServiceCodeServiceID");
            });

            modelBuilder.Entity<PackageService>(entity =>
            {
                entity.HasKey(e => new { e.PackageId, e.ServiceId })
                    .HasName("PK_PackageService");

                entity.HasIndex(e => e.ServiceId)
                    .HasName("FKPackageServiceServiceID");

                entity.Property(e => e.PackageId)
                    .HasColumnName("PackageID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ServiceId)
                    .HasColumnName("ServiceID")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.Package)
                    .WithMany(p => p.PackageService)
                    .HasForeignKey(d => d.PackageId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FKPackageServicePackageID");

                entity.HasOne(d => d.Service)
                    .WithMany(p => p.PackageService)
                    .HasForeignKey(d => d.ServiceId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FKPackageServiceServiceID");
            });

            modelBuilder.Entity<PageCampaign>(entity =>
            {
                entity.HasKey(e => new { e.CampaignId, e.PageId })
                    .HasName("PK_PageCampaign");

                entity.HasIndex(e => e.PageId)
                    .HasName("FKPageCampaignPageID");

                entity.Property(e => e.CampaignId)
                    .HasColumnName("CampaignID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.PageId)
                    .HasColumnName("PageID")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.Campaign)
                    .WithMany(p => p.PageCampaign)
                    .HasForeignKey(d => d.CampaignId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FKPageCampaignCampaignID");

                entity.HasOne(d => d.Page)
                    .WithMany(p => p.PageCampaign)
                    .HasForeignKey(d => d.PageId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FKPageCampaignPageID");
            });

            modelBuilder.Entity<PageStatistic>(entity =>
            {
                entity.HasIndex(e => e.PageId)
                    .HasName("FKPageStatisticPageID");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ControlId)
                    .HasColumnName("ControlID")
                    .HasColumnType("char(32)");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.IsLoad).HasColumnType("bit(1)");

                entity.Property(e => e.PageId)
                    .HasColumnName("PageID")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.Page)
                    .WithMany(p => p.PageStatistic)
                    .HasForeignKey(d => d.PageId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FKPageStatisticPageID");
            });

            modelBuilder.Entity<PageTag>(entity =>
            {
                entity.HasKey(e => new { e.PageId, e.TagId })
                    .HasName("PK_PageTag");

                entity.HasIndex(e => e.TagId)
                    .HasName("FKPageTagTagID");

                entity.Property(e => e.PageId)
                    .HasColumnName("PageID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.TagId)
                    .HasColumnName("TagID")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.Page)
                    .WithMany(p => p.PageTag)
                    .HasForeignKey(d => d.PageId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FKPageTagPageID");

                entity.HasOne(d => d.Tag)
                    .WithMany(p => p.PageTag)
                    .HasForeignKey(d => d.TagId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FKPageTagTagID");
            });

            modelBuilder.Entity<Pages>(entity =>
            {
                entity.HasIndex(e => e.CreatedBy)
                    .HasName("FKPagesCreatedBy");

                entity.HasIndex(e => e.Name)
                    .HasName("Name")
                    .IsUnique();

                entity.HasIndex(e => e.StatusId)
                    .HasName("FKPagesStatusID");

                entity.HasIndex(e => e.TypeId)
                    .HasName("FKPagesTypeID");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CreatedBy).HasColumnType("int(11)");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Description).HasColumnType("varchar(4096)");

                entity.Property(e => e.PreviewUrl).HasColumnName("PreviewUrl").HasColumnType("nvarchar(255)");

                entity.Property(e => e.Html)
                    .IsRequired()
                    .HasColumnName("HTML");

                entity.Property(e => e.Json)
                    .IsRequired()
                    .HasColumnName("JSON");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.IsPublic)
                    .IsRequired()
                    .HasColumnType("bit(1)");

                entity.Property(e => e.StatusId)
                    .HasColumnName("StatusID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.TypeId)
                    .HasColumnName("TypeID")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.Pages)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FKPagesCreatedBy");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Pages)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FKPagesStatusID");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.Pages)
                    .HasForeignKey(d => d.TypeId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FKPagesTypeID");
            });

            modelBuilder.Entity<PageControl>(entity =>
            {
                entity.HasIndex(e => e.TypeId)
                    .HasName("FKPagesTypeID");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("int(11)");
                
                entity.Property(e => e.PreviewUrl).HasColumnName("PreviewUrl").HasColumnType("nvarchar(255)");

                entity.Property(e => e.Json)
                    .IsRequired()
                    .HasColumnName("JSON");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.TypeId)
                    .HasColumnName("TypeID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ParentID)
                    .HasColumnName("ParentID")
                    .HasColumnType("int(11)");
            });

            modelBuilder.Entity<PaymentSetting>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.AuthorizeNetlogin)
                    .IsRequired()
                    .HasColumnName("AuthorizeNETLogin")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.AuthorizeNettransactionKey)
                    .IsRequired()
                    .HasColumnName("AuthorizeNETTransactionKey")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.SignatureKey)
                    .IsRequired()
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.WelcomePackageDays).HasColumnType("int(11)");

                entity.Property(e => e.WelcomePackagePrice).HasColumnType("int(11)");

                entity.Property(e => e.SupplierPDFPath)
                    .IsRequired()
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.defaultLandingPageTemplateID).HasColumnType("int(11)");
                entity.Property(e => e.defaultEmailTemplateID).HasColumnType("int(11)");
            });

            modelBuilder.Entity<PromoCode>(entity =>
            {
                entity.HasKey(e => new { e.Yyyy, e.Mm })
                    .HasName("PK_PromoCode");

                entity.Property(e => e.Yyyy)
                    .HasColumnName("yyyy")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Mm)
                    .HasColumnName("mm")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasColumnType("varchar(255)");
            });

            modelBuilder.Entity<PromoProduct>(entity =>
            {
                entity.HasIndex(e => e.SupplierId)
                    .HasName("FKPromoProductSupplierID");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.SupplierId)
                    .HasColumnName("SupplierID")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.Supplier)
                    .WithMany(p => p.PromoProduct)
                    .HasForeignKey(d => d.SupplierId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FKPromoProductSupplierID");
            });

            modelBuilder.Entity<PromoSupplier>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Address)
                    .HasColumnName("address")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.ArtworkEmail)
                    .HasColumnName("artworkEmail")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.CustomCode)
                    .HasColumnName("customCode")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.DiscountPolicy)
                    .HasColumnName("discountPolicy")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Fax)
                    .HasColumnName("fax")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.OrdersEmail)
                    .HasColumnName("ordersEmail")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.OrdersFax)
                    .HasColumnName("ordersFax")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Tollfree)
                    .HasColumnName("tollfree")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Web)
                    .HasColumnName("web")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.notes)
                    .HasColumnName("notes")
                    .HasColumnType("nvarchar(4096)");

                entity.Property(e => e.externalLink)
                    .HasColumnName("externalLink")
                    .HasColumnType("nvarchar(255)");

                entity.Property(e => e.DocumentPath)
                    .HasColumnName("DocumentPath")
                    .HasColumnType("nvarchar(255)");
            });

            modelBuilder.Entity<RelayAuthorizeNetresponse>(entity =>
            {
                entity.ToTable("RelayAuthorizeNETResponse");

                entity.HasIndex(e => e.InvoiceId)
                    .HasName("FKInvoiceInvoiceID");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.InvoiceId)
                    .HasColumnName("InvoiceID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.XAccountNumber)
                    .IsRequired()
                    .HasColumnName("x_account_number")
                    .HasColumnType("varchar(16)");

                entity.Property(e => e.XAddress)
                    .HasColumnName("x_address")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.XAmount)
                    .HasColumnName("x_amount")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.XAuthCode)
                    .HasColumnName("x_auth_code")
                    .HasColumnType("int(11)");

                entity.Property(e => e.XAvsCode)
                    .IsRequired()
                    .HasColumnName("x_avs_code")
                    .HasColumnType("char(1)");

                entity.Property(e => e.XCardType)
                    .IsRequired()
                    .HasColumnName("x_card_type")
                    .HasColumnType("varchar(32)");

                entity.Property(e => e.XCavvResponse)
                    .HasColumnName("x_cavv_response")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.XCity)
                    .HasColumnName("x_city")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.XCompany)
                    .HasColumnName("x_company")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.XCountry)
                    .HasColumnName("x_country")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.XCustId)
                    .HasColumnName("x_cust_id")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.XCvv2RespCode)
                    .HasColumnName("x_cvv2_resp_code")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.XDescription)
                    .HasColumnName("x_description")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.XDuty)
                    .HasColumnName("x_duty")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.XEmail)
                    .HasColumnName("x_email")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.XFax)
                    .HasColumnName("x_fax")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.XFirstName)
                    .HasColumnName("x_first_name")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.XFreight)
                    .HasColumnName("x_freight")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.XInvoiceNum)
                    .HasColumnName("x_invoice_num")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.XLastName)
                    .HasColumnName("x_last_name")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.XMd5Hash)
                    .HasColumnName("x_MD5_Hash")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.XMethod)
                    .IsRequired()
                    .HasColumnName("x_method")
                    .HasColumnType("char(2)");

                entity.Property(e => e.XPhone)
                    .HasColumnName("x_phone")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.XPoNum)
                    .HasColumnName("x_po_num")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.XResponseCode)
                    .HasColumnName("x_response_code")
                    .HasColumnType("int(11)");

                entity.Property(e => e.XResponseReasonCode)
                    .HasColumnName("x_response_reason_code")
                    .HasColumnType("int(11)");

                entity.Property(e => e.XResponseReasonText)
                    .IsRequired()
                    .HasColumnName("x_response_reason_text")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.XSha2Hash)
                    .HasColumnName("x_SHA2_Hash")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.XShipToAddress)
                    .HasColumnName("x_ship_to_address")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.XShipToCity)
                    .HasColumnName("x_ship_to_city")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.XShipToCompany)
                    .HasColumnName("x_ship_to_company")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.XShipToCountry)
                    .HasColumnName("x_ship_to_country")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.XShipToFirstName)
                    .HasColumnName("x_ship_to_first_name")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.XShipToLastName)
                    .HasColumnName("x_ship_to_last_name")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.XShipToState)
                    .HasColumnName("x_ship_to_state")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.XShipToZip)
                    .HasColumnName("x_ship_to_zip")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.XState)
                    .HasColumnName("x_state")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.XTax)
                    .HasColumnName("x_tax")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.XTaxExempt)
                    .HasColumnName("x_tax_exempt")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.XTestRequest)
                    .HasColumnName("x_test_request")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.XTransId)
                    .HasColumnName("x_trans_id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.XType)
                    .HasColumnName("x_type")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.XZip)
                    .HasColumnName("x_zip")
                    .HasColumnType("varchar(255)");

                entity.HasOne(d => d.Invoice)
                    .WithMany(p => p.RelayAuthorizeNetresponse)
                    .HasForeignKey(d => d.InvoiceId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FKInvoiceInvoiceID");
            });

            modelBuilder.Entity<RoleFunction>(entity =>
            {
                entity.HasKey(e => new { e.RoleId, e.FunctionId })
                    .HasName("PK_RoleFunction");

                entity.HasIndex(e => e.CreatedBy)
                    .HasName("FKRoleFunctionCreatedBy");

                entity.HasIndex(e => e.FunctionId)
                    .HasName("FKRoleFunctionFunctionID");

                entity.Property(e => e.RoleId)
                    .HasColumnName("RoleID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.FunctionId)
                    .HasColumnName("FunctionID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CreatedBy).HasColumnType("int(11)");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.RoleFunction)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FKRoleFunctionCreatedBy");

                entity.HasOne(d => d.Function)
                    .WithMany(p => p.RoleFunction)
                    .HasForeignKey(d => d.FunctionId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FKRoleFunctionFunctionID");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.RoleFunction)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FKRoleFunctionRoleID");
            });

            modelBuilder.Entity<RoleService>(entity =>
            {
                entity.HasKey(e => new { e.RoleId, e.ServiceId })
                    .HasName("PK_RoleService");

                entity.HasIndex(e => e.RoleId)
                    .HasName("FKRoleServiceRoleID");

                entity.HasIndex(e => e.ServiceId)
                    .HasName("FKRoleServiceServiceID");

                entity.Property(e => e.RoleId)
                    .HasColumnName("RoleID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ServiceId)
                    .HasColumnName("ServiceID")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.Service)
                    .WithMany(p => p.RoleService)
                    .HasForeignKey(d => d.ServiceId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FKRoleServiceServiceID");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.RoleService)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FKRoleServiceRoleID");
            });

            modelBuilder.Entity<SecurityFunction>(entity =>
            {
                entity.HasIndex(e => e.Name)
                    .HasName("Name")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Description).HasColumnType("varchar(4096)");

                entity.Property(e => e.IsActual).HasColumnType("bit(1)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(255)");
            });

            modelBuilder.Entity<SecurityRole>(entity =>
            {
                entity.HasIndex(e => e.CreatedBy)
                    .HasName("FKSecurityRoleCreatedBy");

                entity.HasIndex(e => new { e.Name, e.CreatedBy })
                    .HasName("Name")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CreatedBy).HasColumnType("int(11)");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Description).HasColumnType("varchar(4096)");

                entity.Property(e => e.IsActual).HasColumnType("bit(1)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(255)");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.SecurityRole)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FKSecurityRoleCreatedBy");
            });

            modelBuilder.Entity<Service>(entity =>
            {
                entity.HasIndex(e => e.Name)
                    .HasName("Name")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IsActual).HasColumnType("bit(1)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(255)");
            });

            modelBuilder.Entity<ServiceFunction>(entity =>
            {
                entity.HasKey(e => new { e.ServiceId, e.SecurityFunctionId })
                    .HasName("PK_ServiceFunction");

                entity.HasIndex(e => e.SecurityFunctionId)
                    .HasName("FKServiceFunctionSecurityFunctionID");

                entity.Property(e => e.ServiceId)
                    .HasColumnName("ServiceID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.SecurityFunctionId)
                    .HasColumnName("SecurityFunctionID")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.SecurityFunction)
                    .WithMany(p => p.ServiceFunction)
                    .HasForeignKey(d => d.SecurityFunctionId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FKServiceFunctionSecurityFunctionID");

                entity.HasOne(d => d.Service)
                    .WithMany(p => p.ServiceFunction)
                    .HasForeignKey(d => d.ServiceId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FKServiceFunctionServiceID");
            });

            modelBuilder.Entity<Subscription>(entity =>
            {
                entity.HasIndex(e => e.PackageRatePlanId)
                    .HasName("FKSubscriptionPackageRatePlanID");

                entity.HasIndex(e => e.StatusId)
                    .HasName("FKSubscriptionStatusID");

                entity.HasIndex(e => e.UserId)
                    .HasName("FKSubscriptionUserID");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.EffectiveDate).HasColumnType("datetime");

                entity.Property(e => e.ExpirationDate).HasColumnType("datetime");

                entity.Property(e => e.PackageRatePlanId)
                    .HasColumnName("PackageRatePlanID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.RelayAuthorizeNetresponse)
                    .HasColumnName("RelayAuthorizeNETResponse")
                    .HasColumnType("int(11)");

                entity.Property(e => e.StatusId)
                    .HasColumnName("StatusID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.UserId)
                    .HasColumnName("UserID")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.PackageRatePlan)
                    .WithMany(p => p.Subscription)
                    .HasForeignKey(d => d.PackageRatePlanId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FKSubscriptionPackageRatePlanID");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Subscription)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FKSubscriptionStatusID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Subscription)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FKSubscriptionUserID");
            });

            modelBuilder.Entity<SubscriptionExtraService>(entity =>
            {
                entity.HasKey(e => new { e.SubscriptionId, e.ServiceId })
                    .HasName("PK_SubscriptionExtraService");

                entity.HasIndex(e => e.ServiceId)
                    .HasName("FKSubscriptionExtraServiceServiceID");

                entity.Property(e => e.SubscriptionId)
                    .HasColumnName("SubscriptionID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ServiceId)
                    .HasColumnName("ServiceID")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.Service)
                    .WithMany(p => p.SubscriptionExtraService)
                    .HasForeignKey(d => d.ServiceId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FKSubscriptionExtraServiceServiceID");

                entity.HasOne(d => d.Subscription)
                    .WithMany(p => p.SubscriptionExtraService)
                    .HasForeignKey(d => d.SubscriptionId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FKSubscriptionExtraServiceSubscriptionID");
            });

            modelBuilder.Entity<Tags>(entity =>
            {
                entity.HasIndex(e => e.Tag)
                    .HasName("Tag")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Tag)
                    .IsRequired()
                    .HasColumnType("varchar(255)");
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId })
                    .HasName("PK_UserRole");

                entity.HasIndex(e => e.CreatedBy)
                    .HasName("FKUserRoleCreatedBy");

                entity.HasIndex(e => e.RoleId)
                    .HasName("FKUserRoleRoleID");

                entity.Property(e => e.UserId)
                    .HasColumnName("UserID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.RoleId)
                    .HasColumnName("RoleID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CreatedBy).HasColumnType("int(11)");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.UserRoleCreatedByNavigation)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FKUserRoleCreatedBy");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.UserRole)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FKUserRoleRoleID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserRoleUser)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FKUserRoleUserID");
            });

            modelBuilder.Entity<UserToken>(entity =>
            {
                entity.HasIndex(e => e.CreatedBy)
                    .HasName("FKUserTokenCreatedBy");

                entity.HasIndex(e => e.StatusId)
                    .HasName("FKUserTokenStatusID");

                entity.HasIndex(e => new { e.UserId, e.Token, e.Seed })
                    .HasName("UserID")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CreatedBy).HasColumnType("int(11)");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Seed)
                    .IsRequired()
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.StatusId)
                    .HasColumnName("StatusID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Token)
                    .IsRequired()
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.UserId)
                    .HasColumnName("UserID")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.UserTokenCreatedByNavigation)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FKUserTokenCreatedBy");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.UserToken)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FKUserTokenStatusID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserTokenUser)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FKUserTokenUserID");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasIndex(e => e.Login)
                    .HasName("Login")
                    .IsUnique();

                entity.HasIndex(e => e.StatusId)
                    .HasName("FKUsersStatusID");

                entity.HasIndex(e => e.TypeId)
                    .HasName("FKUsersTypeID");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ClearPassword)
                    .IsRequired()
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.CreatedBy).HasColumnType("int(11)");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Icon).HasColumnType("varchar(255)");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Login)
                    .IsRequired()
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.StatusId)
                    .HasColumnName("StatusID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.TypeId)
                    .HasColumnName("TypeID")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FKUsersStatusID");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.TypeId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FKUsersTypeID");
            });
        }
    }
}