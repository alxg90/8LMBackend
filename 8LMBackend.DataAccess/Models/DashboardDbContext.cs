using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace _8LMBackend.DataAccess.Models
{
    public partial class DashboardDbContext : DbContext
    {
        public virtual DbSet<Campaign> Campaign { get; set; }
        public virtual DbSet<Campaigncategory> Campaigncategory { get; set; }
        public virtual DbSet<Campaignshare> Campaignshare { get; set; }
        public virtual DbSet<Campaignstatus> Campaignstatus { get; set; }
        public virtual DbSet<Campaigntag> Campaigntag { get; set; }
        public virtual DbSet<Company> Company { get; set; }
        public virtual DbSet<Contacttype> Contacttype { get; set; }
        public virtual DbSet<Controlgroup> Controlgroup { get; set; }
        public virtual DbSet<Controltype> Controltype { get; set; }
        public virtual DbSet<Pagecampaign> Pagecampaign { get; set; }
        public virtual DbSet<Pages> Pages { get; set; }
        public virtual DbSet<Pagestatistic> Pagestatistic { get; set; }
        public virtual DbSet<Pagestatus> Pagestatus { get; set; }
        public virtual DbSet<Pagetag> Pagetag { get; set; }
        public virtual DbSet<Pagetoolbox> Pagetoolbox { get; set; }
        public virtual DbSet<Pagetype> Pagetype { get; set; }
        public virtual DbSet<Promocode> Promocode { get; set; }
        public virtual DbSet<Rolefunction> Rolefunction { get; set; }
        public virtual DbSet<Securityfunction> Securityfunction { get; set; }
        public virtual DbSet<Securityrole> Securityrole { get; set; }
        public virtual DbSet<Tags> Tags { get; set; }
        public virtual DbSet<Usercompany> Usercompany { get; set; }
        public virtual DbSet<Usercontact> Usercontact { get; set; }
        public virtual DbSet<Userpromocode> Userpromocode { get; set; }
        public virtual DbSet<Userrole> Userrole { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<Userstatus> Userstatus { get; set; }
        public virtual DbSet<Usertoken> Usertoken { get; set; }
        public virtual DbSet<Usertokenstatus> Usertokenstatus { get; set; }
        public virtual DbSet<Usertype> Usertype { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            #warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
            optionsBuilder.UseMySql(@"server=localhost;userid=core;pwd=Obl1skc3p0;port=3306;database=dashboard_development;sslmode=none;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Campaign>(entity =>
            {
                entity.ToTable("campaign");

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

            modelBuilder.Entity<Campaigncategory>(entity =>
            {
                entity.ToTable("campaigncategory");

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

                entity.Property(e => e.IsActual).HasColumnType("bit(1)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.ParentCategoryId)
                    .HasColumnName("ParentCategoryID")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.Campaigncategory)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_CampaignCategory_CreatedBy");
            });

            modelBuilder.Entity<Campaignshare>(entity =>
            {
                entity.ToTable("campaignshare");

                entity.HasIndex(e => e.CreatedBy)
                    .HasName("FK_CampaignShare_CreatedBy");

                entity.HasIndex(e => e.UserId)
                    .HasName("FK_CampaignShare_UserID");

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
                    .WithMany(p => p.Campaignshare)
                    .HasForeignKey(d => d.CampaignId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_CampaignShare_CampaignID");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.CampaignshareCreatedByNavigation)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_CampaignShare_CreatedBy");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.CampaignshareUser)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_CampaignShare_UserID");
            });

            modelBuilder.Entity<Campaignstatus>(entity =>
            {
                entity.ToTable("campaignstatus");

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

            modelBuilder.Entity<Campaigntag>(entity =>
            {
                entity.ToTable("campaigntag");

                entity.HasIndex(e => e.TagId)
                    .HasName("FK_CampaignTag_TagID");

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
                    .WithMany(p => p.Campaigntag)
                    .HasForeignKey(d => d.CampaignId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_CampaignTag_CampaignID");

                entity.HasOne(d => d.Tag)
                    .WithMany(p => p.Campaigntag)
                    .HasForeignKey(d => d.TagId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_CampaignTag_TagID");
            });

            modelBuilder.Entity<Company>(entity =>
            {
                entity.ToTable("company");

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

            modelBuilder.Entity<Contacttype>(entity =>
            {
                entity.ToTable("contacttype");

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

            modelBuilder.Entity<Controlgroup>(entity =>
            {
                entity.ToTable("controlgroup");

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

            modelBuilder.Entity<Controltype>(entity =>
            {
                entity.ToTable("controltype");

                entity.HasIndex(e => e.GroupId)
                    .HasName("FK_ControlType_GroupID");

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
                    .WithMany(p => p.Controltype)
                    .HasForeignKey(d => d.GroupId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_ControlType_GroupID");
            });

            modelBuilder.Entity<Pagecampaign>(entity =>
            {
                entity.ToTable("pagecampaign");

                entity.HasIndex(e => e.PageId)
                    .HasName("FK_PageCampaign_PageID");

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
                    .WithMany(p => p.Pagecampaign)
                    .HasForeignKey(d => d.CampaignId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_PageCampaign_CampaignID");

                entity.HasOne(d => d.Page)
                    .WithMany(p => p.Pagecampaign)
                    .HasForeignKey(d => d.PageId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_PageCampaign_PageID");
            });

            modelBuilder.Entity<Pages>(entity =>
            {
                entity.ToTable("pages");

                entity.HasIndex(e => e.CreatedBy)
                    .HasName("FK_Pages_CreatedBy");

                entity.HasIndex(e => e.Name)
                    .HasName("Name")
                    .IsUnique();

                entity.HasIndex(e => e.StatusId)
                    .HasName("FK_Pages_StatusID");

                entity.HasIndex(e => e.TypeId)
                    .HasName("FK_Pages_TypeID");

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
                    .HasConstraintName("FK_Pages_StatusID");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.Pages)
                    .HasForeignKey(d => d.TypeId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Pages_TypeID");
            });

            modelBuilder.Entity<Pagestatistic>(entity =>
            {
                entity.ToTable("pagestatistic");

                entity.HasIndex(e => e.PageId)
                    .HasName("FK_PageStatistic_PageID");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ControlId)
                    .HasColumnName("ControlID")
                    .HasColumnType("char(16)");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.IsLoad).HasColumnType("bit(1)");

                entity.Property(e => e.PageId)
                    .HasColumnName("PageID")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.Page)
                    .WithMany(p => p.Pagestatistic)
                    .HasForeignKey(d => d.PageId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_PageStatistic_PageID");
            });

            modelBuilder.Entity<Pagestatus>(entity =>
            {
                entity.ToTable("pagestatus");

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

            modelBuilder.Entity<Pagetag>(entity =>
            {
                entity.ToTable("pagetag");

                entity.HasIndex(e => e.TagId)
                    .HasName("FK_PageTag_TagID");

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
                    .WithMany(p => p.Pagetag)
                    .HasForeignKey(d => d.PageId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_PageTag_PageID");

                entity.HasOne(d => d.Tag)
                    .WithMany(p => p.Pagetag)
                    .HasForeignKey(d => d.TagId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_PageTag_TagID");
            });

            modelBuilder.Entity<Pagetoolbox>(entity =>
            {
                entity.ToTable("pagetoolbox");

                entity.HasIndex(e => e.PageTypeId)
                    .HasName("FK_PageToolbox_PageTypeID");

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
                    .WithMany(p => p.Pagetoolbox)
                    .HasForeignKey(d => d.ControlTypeId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_PageToolbox_ControlTypeID");

                entity.HasOne(d => d.PageType)
                    .WithMany(p => p.Pagetoolbox)
                    .HasForeignKey(d => d.PageTypeId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_PageToolbox_PageTypeID");
            });

            modelBuilder.Entity<Pagetype>(entity =>
            {
                entity.ToTable("pagetype");

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

            modelBuilder.Entity<Promocode>(entity =>
            {
                entity.ToTable("promocode");

                entity.HasIndex(e => e.Code)
                    .HasName("Code")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.FromDate).HasColumnType("int(11)");

                entity.Property(e => e.ToDate).HasColumnType("int(11)");
            });

            modelBuilder.Entity<Rolefunction>(entity =>
            {
                entity.ToTable("rolefunction");

                entity.HasIndex(e => e.CreatedBy)
                    .HasName("FK_RoleFunction_CreatedBy");

                entity.HasIndex(e => e.FunctionId)
                    .HasName("FK_RoleFunction_SecurityFunctionID");

                entity.HasIndex(e => new { e.RoleId, e.FunctionId })
                    .HasName("RoleID")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CreatedBy).HasColumnType("int(11)");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.FunctionId)
                    .HasColumnName("FunctionID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.RoleId)
                    .HasColumnName("RoleID")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.Rolefunction)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_RoleFunction_CreatedBy");

                entity.HasOne(d => d.Function)
                    .WithMany(p => p.Rolefunction)
                    .HasForeignKey(d => d.FunctionId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_RoleFunction_SecurityFunctionID");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Rolefunction)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_RoleFunction_SecurityRoleID");
            });

            modelBuilder.Entity<Securityfunction>(entity =>
            {
                entity.ToTable("securityfunction");

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

            modelBuilder.Entity<Securityrole>(entity =>
            {
                entity.ToTable("securityrole");

                entity.HasIndex(e => e.CreatedBy)
                    .HasName("FK_SecurityRole_CreatedBy");

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
                    .WithMany(p => p.Securityrole)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_SecurityRole_CreatedBy");
            });

            modelBuilder.Entity<Tags>(entity =>
            {
                entity.ToTable("tags");

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

            modelBuilder.Entity<Usercompany>(entity =>
            {
                entity.ToTable("usercompany");

                entity.HasIndex(e => e.CompanyId)
                    .HasName("FK_UserCompany_CompanyID");

                entity.HasIndex(e => new { e.UserId, e.CompanyId })
                    .HasName("UserID")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CompanyId)
                    .HasColumnName("CompanyID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.UserId)
                    .HasColumnName("UserID")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.Usercompany)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_UserCompany_CompanyID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Usercompany)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_UserCompany_UserID");
            });

            modelBuilder.Entity<Usercontact>(entity =>
            {
                entity.ToTable("usercontact");

                entity.HasIndex(e => e.ContactTypeId)
                    .HasName("FK_UserContact_ContactTypeID");

                entity.HasIndex(e => new { e.UserId, e.ContactTypeId })
                    .HasName("UserID")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ContactTypeId)
                    .HasColumnName("ContactTypeID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.UserId)
                    .HasColumnName("UserID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Value)
                    .IsRequired()
                    .HasColumnType("varchar(255)");

                entity.HasOne(d => d.ContactType)
                    .WithMany(p => p.Usercontact)
                    .HasForeignKey(d => d.ContactTypeId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_UserContact_ContactTypeID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Usercontact)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_UserContact_UserID");
            });

            modelBuilder.Entity<Userpromocode>(entity =>
            {
                entity.ToTable("userpromocode");

                entity.HasIndex(e => new { e.UserId, e.Code })
                    .HasName("UserID")
                    .IsUnique();

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

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Userpromocode)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_UserPromoCode_UserID");
            });

            modelBuilder.Entity<Userrole>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId })
                    .HasName("UserID");

                entity.ToTable("userrole");

                entity.HasIndex(e => e.CreatedBy)
                    .HasName("FK_UserRole_CreatedBy");

                entity.HasIndex(e => e.RoleId)
                    .HasName("FK_UserRole_SecurityRoleID");

                entity.Property(e => e.UserId)
                    .HasColumnName("UserID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.RoleId)
                    .HasColumnName("RoleID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CreatedBy).HasColumnType("int(11)");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.UserroleCreatedByNavigation)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_UserRole_CreatedBy");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Userrole)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_UserRole_SecurityRoleID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserroleUser)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_UserRole_UserID");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.ToTable("users");

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

                entity.Property(e => e.CId)
                    .HasColumnName("c_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.IndustryId)
                    .HasColumnName("industry_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.LinenameId)
                    .HasColumnName("linename_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Login)
                    .IsRequired()
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.SalesId)
                    .HasColumnName("sales_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.SourceId)
                    .HasColumnName("source_id")
                    .HasColumnType("int(11)");

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

            modelBuilder.Entity<Userstatus>(entity =>
            {
                entity.ToTable("userstatus");

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

            modelBuilder.Entity<Usertoken>(entity =>
            {
                entity.ToTable("usertoken");

                entity.HasIndex(e => e.CreatedBy)
                    .HasName("FK_UserToken_CreatedBy");

                entity.HasIndex(e => e.StatusId)
                    .HasName("FK_UserToken_UserTokenStatusID");

                entity.HasIndex(e => new { e.UserId, e.Token, e.Seed })
                    .HasName("UserID")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ClearPassword)
                    .IsRequired()
                    .HasColumnType("varchar(255)");

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
                    .WithMany(p => p.UsertokenCreatedByNavigation)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_UserToken_CreatedBy");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Usertoken)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_UserToken_UserTokenStatusID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UsertokenUser)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_UserToken_UserID");
            });

            modelBuilder.Entity<Usertokenstatus>(entity =>
            {
                entity.ToTable("usertokenstatus");

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

            modelBuilder.Entity<Usertype>(entity =>
            {
                entity.ToTable("usertype");

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
        }
    }
}