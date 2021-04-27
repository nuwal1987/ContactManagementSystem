using Microsoft.EntityFrameworkCore;

namespace CMS.Elements.Api.Dal.RA.Models
{
    public partial class ContactContext : DbContext
    {
        public ContactContext()
        {
        }

        public ContactContext(DbContextOptions<ContactContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ContactInformation> ContactInformation { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ContactInformation>(entity =>
            {
                entity.HasKey(e => e.ContactId);

                entity.ToTable("ContactInformation");

                entity.Property(e => e.Email)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
