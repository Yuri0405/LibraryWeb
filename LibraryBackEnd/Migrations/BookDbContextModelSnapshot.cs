﻿// <auto-generated />
using LibraryBackEnd.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace LibraryBackEnd.Migrations
{
    [DbContext(typeof(BookDbContext))]
    partial class BookDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.17")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("LibraryBackEnd.Model.Book", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Author")
                        .HasColumnType("text");

                    b.Property<string>("Content")
                        .HasColumnType("text");

                    b.Property<string>("CoverUrl")
                        .HasColumnType("text");

                    b.Property<string>("Genre")
                        .HasColumnType("text");

                    b.Property<string>("Title")
                        .HasColumnType("text");

                    b.HasKey("ID");

                    b.ToTable("Books");
                });

            modelBuilder.Entity("LibraryBackEnd.Model.Rating", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("BookID")
                        .HasColumnType("integer");

                    b.Property<int>("Score")
                        .HasColumnType("integer");

                    b.HasKey("ID");

                    b.HasIndex("BookID");

                    b.ToTable("Ratings");
                });

            modelBuilder.Entity("LibraryBackEnd.Model.Review", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("BookID")
                        .HasColumnType("integer");

                    b.Property<string>("Message")
                        .HasColumnType("text");

                    b.Property<string>("Rewiewer")
                        .HasColumnType("text");

                    b.HasKey("ID");

                    b.HasIndex("BookID");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("LibraryBackEnd.Model.Rating", b =>
                {
                    b.HasOne("LibraryBackEnd.Model.Book", "Book")
                        .WithMany("Ratings")
                        .HasForeignKey("BookID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Book");
                });

            modelBuilder.Entity("LibraryBackEnd.Model.Review", b =>
                {
                    b.HasOne("LibraryBackEnd.Model.Book", "Book")
                        .WithMany("Reviews")
                        .HasForeignKey("BookID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Book");
                });

            modelBuilder.Entity("LibraryBackEnd.Model.Book", b =>
                {
                    b.Navigation("Ratings");

                    b.Navigation("Reviews");
                });
#pragma warning restore 612, 618
        }
    }
}