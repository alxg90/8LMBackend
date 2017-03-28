using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace _8LMBackend.DataAccess.Models
{
    public partial class DashboardDbContext : DbContext
    {
        public virtual DbSet<Campaign> Campaign { get; set; }
        public virtual DbSet<CampaignCategory> CampaignCategory { get; set; }
        public virtual DbSet<CampaignShare> CampaignShare { get; set; }
        public virtual DbSet<CampaignStatus> CampaignStatus { get; set; }
        public virtual DbSet<CampaignTag> CampaignTag { get; set; }
        public virtual DbSet<Company> Company { get; set; }
        public virtual DbSet<ContactType> ContactType { get; set; }
        public virtual DbSet<ControlGroup> ControlGroup { get; set; }
        public virtual DbSet<ControlStat> ControlStat { get; set; }
        public virtual DbSet<ControlType> ControlType { get; set; }
        public virtual DbSet<Currency> Currency { get; set; }
        public virtual DbSet<Package> Package { get; set; }
        public virtual DbSet<PackagePrice> PackagePrice { get; set; }
        public virtual DbSet<PackageReferenceCode> PackageReferenceCode { get; set; }
        public virtual DbSet<PackageReferenceExtendCode> PackageReferenceExtendCode { get; set; }
        public virtual DbSet<PackageReferenceServiceCode> PackageReferenceServiceCode { get; set; }
        public virtual DbSet<PackageService> PackageService { get; set; }
        public virtual DbSet<PageCampaign> PageCampaign { get; set; }
        public virtual DbSet<PageStatistic> PageStatistic { get; set; }
        public virtual DbSet<PageStatus> PageStatus { get; set; }
        public virtual DbSet<PageTag> PageTag { get; set; }
        public virtual DbSet<PageToolbox> PageToolbox { get; set; }
        public virtual DbSet<PageType> PageType { get; set; }
        public virtual DbSet<Pages> Pages { get; set; }
        public virtual DbSet<PromoCode> PromoCode { get; set; }
        public virtual DbSet<PromoProduct> PromoProduct { get; set; }
        public virtual DbSet<PromoSupplier> PromoSupplier { get; set; }
        public virtual DbSet<RoleFunction> RoleFunction { get; set; }
        public virtual DbSet<SecurityFunction> SecurityFunction { get; set; }
        public virtual DbSet<SecurityRole> SecurityRole { get; set; }
        public virtual DbSet<Service> Service { get; set; }
        public virtual DbSet<ServiceFunction> ServiceFunction { get; set; }
        public virtual DbSet<Tags> Tags { get; set; }
        public virtual DbSet<UserCompany> UserCompany { get; set; }
        public virtual DbSet<UserContact> UserContact { get; set; }
        public virtual DbSet<UserPromoCode> UserPromoCode { get; set; }
        public virtual DbSet<UserRole> UserRole { get; set; }
        public virtual DbSet<UserStatus> UserStatus { get; set; }
        public virtual DbSet<UserToken> UserToken { get; set; }
        public virtual DbSet<UserTokenStatus> UserTokenStatus { get; set; }
        public virtual DbSet<UserType> UserType { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            #warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
            optionsBuilder.UseMySql(@"server=localhost;userid=root;pwd=cbyrjgf3/4;port=3306;database=ELMDev;sslmode=none;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Campaign>(entity =>
            {
                entity.HasIndex(e => e.CategoryId)
                    .HasName("FK_Campaign_CategoryID");

                entity.HasIndex(e => e.CreatedBy)
                    .HasName("FK_Campaign_CreatedBy");

                entity.HasIndex(e => e.StatusId)
                    .HasName("FK_Campaign_StatusID");

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
                    .HasConstraintName("FK_Campaign_CategoryID");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.Campaign)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Campaign_CreatedBy");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Campaign)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Campaign_StatusID");
            });

            modelBuilder.Entity<CampaignCategory>(entity =>
            {
                entity.HasIndex(e => e.CreatedBy)
                    .HasName("FK_CC_CreatedBy");

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
                    .HasConstraintName("FK_CC_CreatedBy");
            });

            modelBuilder.Entity<CampaignShare>(entity =>
            {
                entity.HasKey(e => new { e.CampaignId, e.UserId })
                    .HasName("PK_CampaignShare");

                entity.HasIndex(e => e.CreatedBy)
                    .HasName("FK_CS_CreatedBy");

                entity.HasIndex(e => e.UserId)
                    .HasName("FK_CS_UserID");

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
                    .HasConstraintName("FK_CS_CampaignID");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.CampaignShareCreatedByNavigation)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_CS_CreatedBy");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.CampaignShareUser)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_CS_UserID");
            });

            modelBuilder.Entity<CampaignStatus>(entity =>
            {
                entity.HasIndex(e => e.Name)
                    .HasName("Name")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(255)");
            });

            modelBuilder.Entity<CampaignTag>(entity =>
            {
                entity.HasKey(e => new { e.CampaignId, e.TagId })
                    .HasName("PK_CampaignTag");

                entity.HasIndex(e => e.TagId)
                    .HasName("FK_CT_TagID");

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
                    .HasConstraintName("FK_CT_CampaignID");

                entity.HasOne(d => d.Tag)
                    .WithMany(p => p.CampaignTag)
                    .HasForeignKey(d => d.TagId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_CT_TagID");
            });

            modelBuilder.Entity<Company>(entity =>
            {
                entity.HasIndex(e => e.Name)
                    .HasName("Name")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(255)");
            });

            modelBuilder.Entity<ContactType>(entity =>
            {
                entity.HasIndex(e => e.Name)
                    .HasName("Name")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(255)");
            });

            modelBuilder.Entity<ControlGroup>(entity =>
            {
                entity.HasIndex(e => e.Name)
                    .HasName("Name")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(255)");
            });

            modelBuilder.Entity<ControlStat>(entity =>
            {
                entity.HasIndex(e => e.PageId)
                    .HasName("FK_ControlStat_PageID");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("char(16)");

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
                    .HasConstraintName("FK_ControlStat_PageID");
            });

            modelBuilder.Entity<ControlType>(entity =>
            {
                entity.HasIndex(e => e.GroupId)
                    .HasName("FK_CT_GroupID");

                entity.HasIndex(e => e.Name)
                    .HasName("Name")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.GroupId)
                    .HasColumnName("GroupID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IsActive).HasColumnType("bit(1)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(255)");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.ControlType)
                    .HasForeignKey(d => d.GroupId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_CT_GroupID");
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

            modelBuilder.Entity<Package>(entity =>
            {
                entity.HasIndex(e => e.CreatedBy)
                    .HasName("FK_Package_CreatedBy");

                entity.HasIndex(e => e.CurrencyId)
                    .HasName("FK_Package_CurrencyID");

                entity.HasIndex(e => e.Name)
                    .HasName("Name")
                    .IsUnique();

                entity.HasIndex(e => e.UserTypeId)
                    .HasName("FK_Package_UserTypeID");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CreatedBy).HasColumnType("int(11)");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.CurrencyId)
                    .HasColumnName("CurrencyID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnType("varchar(4096)");

                entity.Property(e => e.DurationInMonth).HasColumnType("int(11)");

                entity.Property(e => e.IsActual).HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.PaletteId)
                    .HasColumnName("PaletteID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Price).HasColumnType("int(11)");

                entity.Property(e => e.UserTypeId)
                    .HasColumnName("UserTypeID")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.Package)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Package_CreatedBy");

                entity.HasOne(d => d.Currency)
                    .WithMany(p => p.Package)
                    .HasForeignKey(d => d.CurrencyId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Package_CurrencyID");

                entity.HasOne(d => d.UserType)
                    .WithMany(p => p.Package)
                    .HasForeignKey(d => d.UserTypeId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Package_UserTypeID");
            });

            modelBuilder.Entity<PackagePrice>(entity =>
            {
                entity.HasKey(e => new { e.PackageId, e.CurrencyId, e.DurationInMonth })
                    .HasName("PK_PackagePrice");

                entity.HasIndex(e => e.CreatedBy)
                    .HasName("FK_PackagePrice_CreatedBy");

                entity.HasIndex(e => e.CurrencyId)
                    .HasName("FK_PackagePrice_CurrencyID");

                entity.Property(e => e.PackageId)
                    .HasColumnName("PackageID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CurrencyId)
                    .HasColumnName("CurrencyID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.DurationInMonth).HasColumnType("int(11)");

                entity.Property(e => e.CreatedBy).HasColumnType("int(11)");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.IsActual).HasColumnType("int(11)");

                entity.Property(e => e.Price).HasColumnType("int(11)");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.PackagePrice)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_PackagePrice_CreatedBy");

                entity.HasOne(d => d.Currency)
                    .WithMany(p => p.PackagePrice)
                    .HasForeignKey(d => d.CurrencyId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_PackagePrice_CurrencyID");
            });

            modelBuilder.Entity<PackageReferenceCode>(entity =>
            {
                entity.HasKey(e => new { e.PackageId, e.ReferenceCode })
                    .HasName("PK_PackageReferenceCode");

                entity.Property(e => e.PackageId)
                    .HasColumnName("PackageID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ReferenceCode).HasColumnType("varchar(32)");

                entity.Property(e => e.IsFixed).HasColumnType("bit(1)");

                entity.Property(e => e.Value).HasColumnType("int(11)");

                entity.HasOne(d => d.Package)
                    .WithMany(p => p.PackageReferenceCode)
                    .HasForeignKey(d => d.PackageId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_PackageReferenceCode_PackageID");
            });

            modelBuilder.Entity<PackageReferenceExtendCode>(entity =>
            {
                entity.HasKey(e => new { e.PackageId, e.ReferenceCode })
                    .HasName("PK_PackageReferenceExtendCode");

                entity.Property(e => e.PackageId)
                    .HasColumnName("PackageID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ReferenceCode).HasColumnType("varchar(32)");

                entity.Property(e => e.Months).HasColumnType("int(11)");

                entity.HasOne(d => d.Package)
                    .WithMany(p => p.PackageReferenceExtendCode)
                    .HasForeignKey(d => d.PackageId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_PackageReferenceExtendCode_PackageID");
            });

            modelBuilder.Entity<PackageReferenceServiceCode>(entity =>
            {
                entity.HasKey(e => new { e.PackageId, e.ReferenceCode })
                    .HasName("PK_PackageReferenceServiceCode");

                entity.HasIndex(e => e.ServiceId)
                    .HasName("FK_PackageReferenceServiceCode_ServiceID");

                entity.Property(e => e.PackageId)
                    .HasColumnName("PackageID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ReferenceCode).HasColumnType("varchar(32)");

                entity.Property(e => e.ServiceId)
                    .HasColumnName("ServiceID")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.Package)
                    .WithMany(p => p.PackageReferenceServiceCode)
                    .HasForeignKey(d => d.PackageId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_PackageReferenceServiceCode_PackageID");

                entity.HasOne(d => d.Service)
                    .WithMany(p => p.PackageReferenceServiceCode)
                    .HasForeignKey(d => d.ServiceId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_PackageReferenceServiceCode_ServiceID");
            });

            modelBuilder.Entity<PackageService>(entity =>
            {
                entity.HasKey(e => new { e.PackageId, e.ServiceId })
                    .HasName("PK_PackageService");

                entity.HasIndex(e => e.ServiceId)
                    .HasName("FK_PackageService_ServiceID");

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
                    .HasConstraintName("FK_PackageService_PackageID");

                entity.HasOne(d => d.Service)
                    .WithMany(p => p.PackageService)
                    .HasForeignKey(d => d.ServiceId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_PackageService_ServiceID");
            });

            modelBuilder.Entity<PageCampaign>(entity =>
            {
                entity.HasKey(e => new { e.CampaignId, e.PageId })
                    .HasName("PK_PageCampaign");

                entity.HasIndex(e => e.PageId)
                    .HasName("FK_PCampaign_PageID");

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
                    .HasConstraintName("FK_PCampaign_CampaignID");

                entity.HasOne(d => d.Page)
                    .WithMany(p => p.PageCampaign)
                    .HasForeignKey(d => d.PageId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_PCampaign_PageID");
            });

            modelBuilder.Entity<PageStatistic>(entity =>
            {
                entity.HasIndex(e => e.ControlId)
                    .HasName("FK_PageStatistic_ControlID");

                entity.HasIndex(e => e.PageId)
                    .HasName("FK_PageStatistic_PageID");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ControlId)
                    .HasColumnName("ControlID")
                    .HasColumnType("char(16)");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Ip)
                    .IsRequired()
                    .HasColumnName("IP")
                    .HasColumnType("varchar(15)");

                entity.Property(e => e.IsLoad).HasColumnType("bit(1)");

                entity.Property(e => e.PageId)
                    .HasColumnName("PageID")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.Control)
                    .WithMany(p => p.PageStatistic)
                    .HasForeignKey(d => d.ControlId)
                    .HasConstraintName("FK_PageStatistic_ControlID");

                entity.HasOne(d => d.Page)
                    .WithMany(p => p.PageStatistic)
                    .HasForeignKey(d => d.PageId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_PageStatistic_PageID");
            });

            modelBuilder.Entity<PageStatus>(entity =>
            {
                entity.HasIndex(e => e.Name)
                    .HasName("Name")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(255)");
            });

            modelBuilder.Entity<PageTag>(entity =>
            {
                entity.HasKey(e => new { e.PageId, e.TagId })
                    .HasName("PK_PageTag");

                entity.HasIndex(e => e.TagId)
                    .HasName("FK_PT_TagID");

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
                    .HasConstraintName("FK_PT_PageID");

                entity.HasOne(d => d.Tag)
                    .WithMany(p => p.PageTag)
                    .HasForeignKey(d => d.TagId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_PT_TagID");
            });

            modelBuilder.Entity<PageToolbox>(entity =>
            {
                entity.HasKey(e => new { e.ControlTypeId, e.PageTypeId })
                    .HasName("PK_PageToolbox");

                entity.HasIndex(e => e.PageTypeId)
                    .HasName("FK_PTbox_PTID");

                entity.Property(e => e.ControlTypeId)
                    .HasColumnName("ControlTypeID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.PageTypeId)
                    .HasColumnName("PageTypeID")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.ControlType)
                    .WithMany(p => p.PageToolbox)
                    .HasForeignKey(d => d.ControlTypeId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_PTbox_CTID");

                entity.HasOne(d => d.PageType)
                    .WithMany(p => p.PageToolbox)
                    .HasForeignKey(d => d.PageTypeId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_PTbox_PTID");
            });

            modelBuilder.Entity<PageType>(entity =>
            {
                entity.HasIndex(e => e.Name)
                    .HasName("Name")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(255)");
            });

            modelBuilder.Entity<Pages>(entity =>
            {
                entity.HasIndex(e => e.CreatedBy)
                    .HasName("FK_Pages_CreatedBy");

                entity.HasIndex(e => e.Name)
                    .HasName("Name")
                    .IsUnique();

                entity.HasIndex(e => e.StatusId)
                    .HasName("FK_Pages_SID");

                entity.HasIndex(e => e.TypeId)
                    .HasName("FK_Pages_TID");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CreatedBy).HasColumnType("int(11)");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Description).HasColumnType("varchar(4096)");

                entity.Property(e => e.Html)
                    .IsRequired()
                    .HasColumnName("HTML");

                entity.Property(e => e.Json)
                    .IsRequired()
                    .HasColumnName("JSON");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(255)");

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
                    .HasConstraintName("FK_Pages_CreatedBy");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Pages)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Pages_SID");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.Pages)
                    .HasForeignKey(d => d.TypeId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Pages_TID");
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
                entity.HasIndex(e => e.UserId)
                    .HasName("FK_PromoProduct_UserID");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.UserId)
                    .HasColumnName("UserID")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.PromoProduct)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_PromoProduct_UserID");
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
            });

            modelBuilder.Entity<RoleFunction>(entity =>
            {
                entity.HasKey(e => new { e.RoleId, e.FunctionId })
                    .HasName("PK_RoleFunction");

                entity.HasIndex(e => e.CreatedBy)
                    .HasName("FK_RF_CreatedBy");

                entity.HasIndex(e => e.FunctionId)
                    .HasName("FK_RF_SFID");

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
                    .HasConstraintName("FK_RF_CreatedBy");

                entity.HasOne(d => d.Function)
                    .WithMany(p => p.RoleFunction)
                    .HasForeignKey(d => d.FunctionId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_RF_SFID");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.RoleFunction)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_RF_SRID");
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
                    .HasName("FK_SR_CreatedBy");

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
                    .HasConstraintName("FK_SR_CreatedBy");
            });

            modelBuilder.Entity<Service>(entity =>
            {
                entity.HasIndex(e => e.Name)
                    .HasName("Name")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.IsActual).HasColumnType("bit(1)");
            });

            modelBuilder.Entity<ServiceFunction>(entity =>
            {
                entity.HasKey(e => new { e.ServiceId, e.SecurityFunctionId })
                    .HasName("PK_ServiceFunction");

                entity.HasIndex(e => e.SecurityFunctionId)
                    .HasName("FK_ServiceFunction_SFID");

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
                    .HasConstraintName("FK_ServiceFunction_SFID");
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

            modelBuilder.Entity<UserCompany>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.CompanyId })
                    .HasName("PK_UserCompany");

                entity.HasIndex(e => e.CompanyId)
                    .HasName("FK_UCompany_CID");

                entity.Property(e => e.UserId)
                    .HasColumnName("UserID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CompanyId)
                    .HasColumnName("CompanyID")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.UserCompany)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_UCompany_CID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserCompany)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_UCompany_UID");
            });

            modelBuilder.Entity<UserContact>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.ContactTypeId })
                    .HasName("PK_UserContact");

                entity.HasIndex(e => e.ContactTypeId)
                    .HasName("FK_UC_CTID");

                entity.Property(e => e.UserId)
                    .HasColumnName("UserID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ContactTypeId)
                    .HasColumnName("ContactTypeID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Value)
                    .IsRequired()
                    .HasColumnType("varchar(255)");

                entity.HasOne(d => d.ContactType)
                    .WithMany(p => p.UserContact)
                    .HasForeignKey(d => d.ContactTypeId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_UC_CTID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserContact)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_UC_UID");
            });

            modelBuilder.Entity<UserPromoCode>(entity =>
            {
                entity.HasIndex(e => e.CreatedBy)
                    .HasName("FK_UPC_CreatedBy");

                entity.HasIndex(e => e.UserId)
                    .HasName("FK_UPC_UserID");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.CreatedBy).HasColumnType("int(11)");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.IsActive).HasColumnType("bit(1)");

                entity.Property(e => e.UserId)
                    .HasColumnName("UserID")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.UserPromoCodeCreatedByNavigation)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_UPC_CreatedBy");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserPromoCodeUser)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_UPC_UserID");
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId })
                    .HasName("PK_UserRole");

                entity.HasIndex(e => e.CreatedBy)
                    .HasName("FK_UR_CreatedBy");

                entity.HasIndex(e => e.RoleId)
                    .HasName("FK_UR_SRID");

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
                    .HasConstraintName("FK_UR_CreatedBy");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.UserRole)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_UR_SRID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserRoleUser)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_UR_UserID");
            });

            modelBuilder.Entity<UserStatus>(entity =>
            {
                entity.HasIndex(e => e.Name)
                    .HasName("Name")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(255)");
            });

            modelBuilder.Entity<UserToken>(entity =>
            {
                entity.HasIndex(e => e.CreatedBy)
                    .HasName("FK_UT_CreatedBy");

                entity.HasIndex(e => e.StatusId)
                    .HasName("FK_UT_UTSID");

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
                    .HasConstraintName("FK_UT_CreatedBy");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.UserToken)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_UT_UTSID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserTokenUser)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_UT_UserID");
            });

            modelBuilder.Entity<UserTokenStatus>(entity =>
            {
                entity.HasIndex(e => e.Name)
                    .HasName("Name")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(255)");
            });

            modelBuilder.Entity<UserType>(entity =>
            {
                entity.HasIndex(e => e.Name)
                    .HasName("Name")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(255)");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasIndex(e => e.Login)
                    .HasName("Login")
                    .IsUnique();

                entity.HasIndex(e => e.StatusId)
                    .HasName("FK_Users_StatusID");

                entity.HasIndex(e => e.TypeId)
                    .HasName("FK_Users_TypeID");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CreatedBy).HasColumnType("int(11)");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

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
                    .HasConstraintName("FK_Users_StatusID");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.TypeId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Users_TypeID");
            });
        }
    }
}