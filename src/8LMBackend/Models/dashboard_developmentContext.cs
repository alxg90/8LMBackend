using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace _8LMCore.Models
{
    public partial class dashboard_developmentContext : DbContext
    {
        public virtual DbSet<ArInternalMetadata> ArInternalMetadata { get; set; }
        public virtual DbSet<Campaign> Campaign { get; set; }
        public virtual DbSet<CampaignCategory> CampaignCategory { get; set; }
        public virtual DbSet<CampaignEmail> CampaignEmail { get; set; }
        public virtual DbSet<CampaignEpage> CampaignEpage { get; set; }
        public virtual DbSet<CampaignProduct> CampaignProduct { get; set; }
        public virtual DbSet<CampaignShare> CampaignShare { get; set; }
        public virtual DbSet<CampaignStatus> CampaignStatus { get; set; }
        public virtual DbSet<CampaignTag> CampaignTag { get; set; }
        public virtual DbSet<Contact> Contact { get; set; }
        public virtual DbSet<ContactType> ContactType { get; set; }
        public virtual DbSet<ControlGroup> ControlGroup { get; set; }
        public virtual DbSet<ControlType> ControlType { get; set; }
        public virtual DbSet<PageCampaign> PageCampaign { get; set; }
        public virtual DbSet<PageStatistic> PageStatistic { get; set; }
        public virtual DbSet<PageStatus> PageStatus { get; set; }
        public virtual DbSet<PageTag> PageTag { get; set; }
        public virtual DbSet<PageToolbox> PageToolbox { get; set; }
        public virtual DbSet<PageType> PageType { get; set; }
        public virtual DbSet<Pages> Pages { get; set; }
        public virtual DbSet<RoleFunction> RoleFunction { get; set; }
        public virtual DbSet<SchemaMigrations> SchemaMigrations { get; set; }
        public virtual DbSet<SecurityFunction> SecurityFunction { get; set; }
        public virtual DbSet<SecurityRole> SecurityRole { get; set; }
        public virtual DbSet<Tags> Tags { get; set; }
        public virtual DbSet<UserStatus> UserStatus { get; set; }
        public virtual DbSet<UserToken> UserToken { get; set; }
        public virtual DbSet<UserTokenStatus> UserTokenStatus { get; set; }
        public virtual DbSet<UserType> UserType { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<Users1> Users1 { get; set; }

        // Unable to generate entity type for table 'UserRole'. Please see the warning messages.

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            #warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
            optionsBuilder.UseMySql(@"server=localhost;userid=core;pwd=Obl1skc3p0!;port=3306;database=dashboard_development;sslmode=none;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ArInternalMetadata>(entity =>
            {
                entity.HasKey(e => e.Key)
                    .HasName("PK_ar_internal_metadata");

                entity.ToTable("ar_internal_metadata");

                entity.Property(e => e.Key)
                    .HasColumnName("key")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("datetime");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("datetime");

                entity.Property(e => e.Value)
                    .HasColumnName("value")
                    .HasColumnType("varchar(255)");
            });

            modelBuilder.Entity<Campaign>(entity =>
            {
                entity.HasIndex(e => e.CategoryId)
                    .HasName("FK_Campaign_Category");

                entity.HasIndex(e => e.CreatedBy)
                    .HasName("FK_Campaign_CreatedBy");

                entity.HasIndex(e => e.StatusId)
                    .HasName("FK_Campaign_Status");

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
                    .HasConstraintName("FK_Campaign_Category");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.Campaign)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Campaign_CreatedBy");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Campaign)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Campaign_Status");
            });

            modelBuilder.Entity<CampaignCategory>(entity =>
            {
                entity.HasIndex(e => e.CreatedBy)
                    .HasName("FK_CampaignCategory_CreatedBy");

                entity.HasIndex(e => new { e.ParentCategoryId, e.Name, e.CreatedBy })
                    .HasName("ParentCategoryID")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CreatedBy).HasColumnType("int(11)");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Description).HasColumnType("varchar(4096)");

                entity.Property(e => e.IsActual).HasColumnType("tinyint(1)");

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
                    .HasConstraintName("FK_CampaignCategory_CreatedBy");
            });

            modelBuilder.Entity<CampaignEmail>(entity =>
            {
                entity.HasIndex(e => new { e.CampaignId, e.EmailId })
                    .HasName("CampaignID")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("int(11)");

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
                    .HasConstraintName("FK_EmailCampaign_Campaign");
            });

            modelBuilder.Entity<CampaignEpage>(entity =>
            {
                entity.ToTable("CampaignEPage");

                entity.HasIndex(e => new { e.CampaignId, e.EpageId })
                    .HasName("CampaignID")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("int(11)");

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
                    .HasConstraintName("FK_EPageCampaign_Campaign");
            });

            modelBuilder.Entity<CampaignProduct>(entity =>
            {
                entity.HasIndex(e => new { e.CampaignId, e.ProductId })
                    .HasName("CampaignID")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("int(11)");

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
                    .HasConstraintName("FK_CampaignProduct_Campaign");
            });

            modelBuilder.Entity<CampaignShare>(entity =>
            {
                entity.HasIndex(e => e.CreatedBy)
                    .HasName("FK_CampaignShare_CreatedBy");

                entity.HasIndex(e => e.UserId)
                    .HasName("FK_CampaignShare_User");

                entity.HasIndex(e => new { e.CampaignId, e.UserId })
                    .HasName("CampaignID")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CampaignId)
                    .HasColumnName("CampaignID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CreatedBy).HasColumnType("int(11)");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.UserId)
                    .HasColumnName("UserID")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.Campaign)
                    .WithMany(p => p.CampaignShare)
                    .HasForeignKey(d => d.CampaignId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_CampaignShare_Campaign");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.CampaignShareCreatedByNavigation)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_CampaignShare_CreatedBy");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.CampaignShareUser)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_CampaignShare_User");
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
                entity.HasIndex(e => e.TagId)
                    .HasName("FK_CampaignTag_Tag");

                entity.HasIndex(e => new { e.CampaignId, e.TagId })
                    .HasName("CampaignID")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("int(11)");

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
                    .HasConstraintName("FK_CampaignTag_Campaign");

                entity.HasOne(d => d.Tag)
                    .WithMany(p => p.CampaignTag)
                    .HasForeignKey(d => d.TagId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_CampaignTag_Tag");
            });

            modelBuilder.Entity<Contact>(entity =>
            {
                entity.HasIndex(e => new { e.UserTypeId, e.ContactTypeId, e.Value })
                    .HasName("UserTypeID")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ContactTypeId)
                    .HasColumnName("ContactTypeID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.UserTypeId)
                    .HasColumnName("UserTypeID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Value)
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

            modelBuilder.Entity<ControlType>(entity =>
            {
                entity.HasIndex(e => e.GroupId)
                    .HasName("FK_ControlType_Group");

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
                    .HasConstraintName("FK_ControlType_Group");
            });

            modelBuilder.Entity<PageCampaign>(entity =>
            {
                entity.HasIndex(e => e.PageId)
                    .HasName("FK_PageCampaign_Page");

                entity.HasIndex(e => new { e.CampaignId, e.PageId })
                    .HasName("CampaignID")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("int(11)");

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
                    .HasConstraintName("FK_PageCampaign_Campaign");

                entity.HasOne(d => d.Page)
                    .WithMany(p => p.PageCampaign)
                    .HasForeignKey(d => d.PageId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_PageCampaign_Page");
            });

            modelBuilder.Entity<PageStatistic>(entity =>
            {
                entity.HasIndex(e => e.PageId)
                    .HasName("FK_PageStatistic_Page");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ControlId)
                    .HasColumnName("ControlID")
                    .HasColumnType("char(16)");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.IsLoad).HasColumnType("tinyint(1)");

                entity.Property(e => e.PageId)
                    .HasColumnName("PageID")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.Page)
                    .WithMany(p => p.PageStatistic)
                    .HasForeignKey(d => d.PageId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_PageStatistic_Page");
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
                entity.HasIndex(e => e.TagId)
                    .HasName("FK_PageTag_Tag");

                entity.HasIndex(e => new { e.PageId, e.TagId })
                    .HasName("PageID")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("int(11)");

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
                    .HasConstraintName("FK_PageTag_Page");

                entity.HasOne(d => d.Tag)
                    .WithMany(p => p.PageTag)
                    .HasForeignKey(d => d.TagId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_PageTag_Tag");
            });

            modelBuilder.Entity<PageToolbox>(entity =>
            {
                entity.HasIndex(e => e.PageTypeId)
                    .HasName("FK_PageToolbox_PageType");

                entity.HasIndex(e => new { e.ControlTypeId, e.PageTypeId })
                    .HasName("ControlTypeID")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("int(11)");

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
                    .HasConstraintName("FK_PageToolbox_ControlType");

                entity.HasOne(d => d.PageType)
                    .WithMany(p => p.PageToolbox)
                    .HasForeignKey(d => d.PageTypeId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_PageToolbox_PageType");
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
                    .HasName("FK_Pages_Status");

                entity.HasIndex(e => e.TypeId)
                    .HasName("FK_Pages_Type");

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
                    .HasConstraintName("FK_Pages_Status");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.Pages)
                    .HasForeignKey(d => d.TypeId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Pages_Type");
            });

            modelBuilder.Entity<RoleFunction>(entity =>
            {
                entity.HasKey(e => new { e.RoleId, e.FunctionId })
                    .HasName("RoleID");

                entity.HasIndex(e => e.CreatedBy)
                    .HasName("FK_RoleFunction_CreatedBy");

                entity.HasIndex(e => e.FunctionId)
                    .HasName("FK_RoleFunction_SecurityFunction");

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
                    .HasConstraintName("FK_RoleFunction_CreatedBy");

                entity.HasOne(d => d.Function)
                    .WithMany(p => p.RoleFunction)
                    .HasForeignKey(d => d.FunctionId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_RoleFunction_SecurityFunction");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.RoleFunction)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_RoleFunction_SecurityRole");
            });

            modelBuilder.Entity<SchemaMigrations>(entity =>
            {
                entity.HasKey(e => e.Version)
                    .HasName("PK_schema_migrations");

                entity.ToTable("schema_migrations");

                entity.Property(e => e.Version)
                    .HasColumnName("version")
                    .HasColumnType("varchar(255)");
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
                    .HasName("FK_SecurityRole_CreatedBy");

                entity.HasIndex(e => e.Name)
                    .HasName("Name")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CreatedBy).HasColumnType("int(11)");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Description).HasColumnType("varchar(4096)");

                entity.Property(e => e.IsActual).HasColumnType("tinyint(1)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(255)");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.SecurityRole)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_SecurityRole_CreatedBy");
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
                    .HasName("FK_UserToken_CreatedBy");

                entity.HasIndex(e => e.StatusId)
                    .HasName("FK_UserToken_UserTokenStatus");

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
                    .HasConstraintName("FK_UserToken_CreatedBy");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.UserToken)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_UserToken_UserTokenStatus");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserTokenUser)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_UserToken_Users");
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
                    .HasName("FK_Users_Status");

                entity.HasIndex(e => e.TypeId)
                    .HasName("FK_Users_Type");

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
                    .HasConstraintName("FK_Users_Status");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.TypeId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Users_Type");
            });

            modelBuilder.Entity<Users1>(entity =>
            {
                entity.ToTable("_users");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ClearPass)
                    .HasColumnName("clear_pass")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("datetime");

                entity.Property(e => e.HashedPassword)
                    .HasColumnName("hashed_password")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Role)
                    .HasColumnName("role")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Salt)
                    .HasColumnName("salt")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("datetime");
            });
        }
    }
}