﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using System.ComponentModel.DataAnnotations.Schema;

namespace eShop.Data
{
	public class CatalogDBContext : DbContext
	{
		public CatalogDBContext(DbContextOptions options) : base(options)
		{
		}

		public DbSet<CatalogItem> CatalogItems { get; set; }

		public DbSet<CatalogBrand> CatalogBrands { get; set; }

		public DbSet<CatalogType> CatalogTypes { get; set; }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			ConfigureCatalogType(builder.Entity<CatalogType>());
			ConfigureCatalogBrand(builder.Entity<CatalogBrand>());
			ConfigureCatalogItem(builder.Entity<CatalogItem>());
			base.OnModelCreating(builder);
		}

		void ConfigureCatalogType(EntityTypeBuilder<CatalogType> builder)
		{
			builder.ToTable("CatalogType");
			builder.HasKey(ci => ci.Id);
			builder.Property(ci => ci.Id)
			   .IsRequired();

			builder.Property(cb => cb.Type)
				.IsRequired()
				.HasMaxLength(100);
		}

		void ConfigureCatalogBrand(EntityTypeBuilder<CatalogBrand> builder)
		{
			builder.ToTable("CatalogBrand");

			builder.HasKey(ci => ci.Id);

			builder.Property(ci => ci.Id)
			   .IsRequired();

			builder.Property(cb => cb.Brand)
				.IsRequired()
				.HasMaxLength(100);
		}

		void ConfigureCatalogItem(EntityTypeBuilder<CatalogItem> builder)
		{
			builder.ToTable("Catalog");

			builder.HasKey(ci => ci.Id);

			builder.Property(ci => ci.Id)
				.UseIdentityColumn()
				.IsRequired();

			builder.Property(ci => ci.Name)
				.IsRequired()
				.HasMaxLength(50);

			builder.Property(ci => ci.Price)
				.IsRequired();

			builder.Property(ci => ci.PictureFileName)
				.IsRequired();

			builder.Ignore(ci => ci.PictureUri);

			builder.HasOne(ci => ci.CatalogBrand)
				.WithMany()
				.HasForeignKey(ci => ci.CatalogBrandId);

			builder.HasOne<CatalogType>(ci => ci.CatalogType)
				.WithMany()
				.HasForeignKey(ci => ci.CatalogTypeId);
		}
	}
}
