using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RealEstateWeb.Domain.Entities;

namespace RealEstateWeb.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<ClientProfile> ClientProfiles => Set<ClientProfile>();
        public DbSet<OwnerProfile> OwnerProfiles => Set<OwnerProfile>();

        public DbSet<Property> Properties => Set<Property>();
        public DbSet<Listing> Listings => Set<Listing>();
        public DbSet<Feature> Features => Set<Feature>();
        public DbSet<PropertyFeature> PropertyFeatures => Set<PropertyFeature>();
        public DbSet<PropertyType> PropertyTypes => Set<PropertyType>();
        public DbSet<ListingType> ListingTypes => Set<ListingType>();
        public DbSet<ListingStatus> ListingStatuses => Set<ListingStatus>();
        public DbSet<ListingPriceHistory> ListingPriceHistories => Set<ListingPriceHistory>();
        public DbSet<Media> Medias => Set<Media>();
        public DbSet<MediaType> MediaTypes => Set<MediaType>();


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // ClientProfile: 1–1 + contraintes uniques
            builder.Entity<ClientProfile>(e =>
            {
                e.HasIndex(x => x.EmailAddress).IsUnique();
                e.HasIndex(x => x.PhoneNumber).IsUnique();
                e.HasIndex(x => x.ApplicationUserId).IsUnique();

                e.HasOne(x => x.ApplicationUser)
                    .WithOne(u => u.ClientProfile)
                    .HasForeignKey<ClientProfile>(x => x.ApplicationUserId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // OwnerProfile: 1–1 + unique
            builder.Entity<OwnerProfile>(e =>
            {
                e.HasIndex(x => x.ApplicationUserId).IsUnique();

                e.HasOne(x => x.ApplicationUser)
                    .WithOne(u => u.OwnerProfile)
                    .HasForeignKey<OwnerProfile>(x => x.ApplicationUserId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<Property>(entity =>
            {
                entity.HasIndex(p => p.City);
                entity.HasIndex(p => p.Region);

                entity.HasOne(p => p.PropertyType)
                    .WithMany(pt => pt.Properties)
                    .HasForeignKey(p => p.PropertyTypeId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<Listing>(entity =>
            {
                entity.HasOne(l => l.Property)
                    .WithMany(p => p.Listings)
                    .HasForeignKey(l => l.PropertyId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(l => l.ListingType)
                    .WithMany(lt => lt.Listings)
                    .HasForeignKey(l => l.ListingTypeId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(l => l.ListingStatus)
                    .WithMany(ls => ls.Listings)
                    .HasForeignKey(l => l.ListingStatusId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasIndex(l => l.Price);
                entity.HasIndex(l => l.CreatedDate);
            });

            builder.Entity<PropertyFeature>(entity =>
            {
                entity.HasKey(pf => new { pf.PropertyId, pf.FeatureId });

                entity.HasOne(pf => pf.Property)
                    .WithMany(p => p.PropertyFeatures)
                    .HasForeignKey(pf => pf.PropertyId);

                entity.HasOne(pf => pf.Feature)
                    .WithMany(f => f.PropertyFeatures)
                    .HasForeignKey(pf => pf.FeatureId);
            });

            builder.Entity<Media>(entity =>
            {
                entity.HasOne(m => m.Property)
                    .WithMany(p => p.Medias)
                    .HasForeignKey(m => m.PropertyId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(m => m.MediaType)
                    .WithMany(mt => mt.Medias)
                    .HasForeignKey(m => m.MediaTypeId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasIndex(m => m.PropertyId);
            });

            builder.Entity<ListingPriceHistory>(entity =>
            {
                entity.HasOne(h => h.Listing)
                    .WithMany(l => l.PriceHistories)
                    .HasForeignKey(h => h.ListingId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasIndex(h => h.ListingId);
                entity.HasIndex(h => h.StartDate);
            });



            builder.Entity<PropertyType>().HasData(
               new PropertyType { Id = 1, Description = "House" },
               new PropertyType { Id = 2, Description = "Appartement" }
            );

            builder.Entity<ListingType>().HasData(
                new ListingType { Id = 1, Description = "Rent" },
                new ListingType { Id = 2, Description = "Sale" }
            );

            builder.Entity<ListingStatus>().HasData(
                new ListingStatus { Id = 1, Description = "Actif" },
                new ListingStatus { Id = 2, Description = "Sold" },
                new ListingStatus { Id = 3, Description = "Rented" }
            );

            builder.Entity<MediaType>().HasData(
                new MediaType { Id = 1, Description = "jpg" },
                new MediaType { Id = 2, Description = "png" },
                new MediaType { Id = 3, Description = "mp4" }
            );


        }
    }
}
