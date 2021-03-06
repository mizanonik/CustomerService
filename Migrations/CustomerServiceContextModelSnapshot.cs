﻿// <auto-generated />
using System;
using CustomerServiceApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CustomerServiceApi.Migrations
{
    [DbContext(typeof(CustomerServiceContext))]
    partial class CustomerServiceContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CustomerServiceApi.Models.CustomerLedger", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("Amount");

                    b.Property<int>("BatchId");

                    b.Property<int>("CustomerMasterId");

                    b.Property<DateTime>("TransactionDate");

                    b.Property<int>("TransactionId");

                    b.Property<int>("TransactionTypeId");

                    b.HasKey("Id");

                    b.HasIndex("CustomerMasterId");

                    b.HasIndex("TransactionTypeId");

                    b.ToTable("CustomerLedgers");
                });

            modelBuilder.Entity("CustomerServiceApi.Models.CustomerMaster", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("Amount");

                    b.Property<int>("CustomerId");

                    b.Property<DateTime>("LastTransactionDate");

                    b.HasKey("Id");

                    b.ToTable("CustomerMasters");
                });

            modelBuilder.Entity("CustomerServiceApi.Models.TransactionType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("TypeName");

                    b.HasKey("Id");

                    b.ToTable("TransactionTypes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            TypeName = "Debit"
                        },
                        new
                        {
                            Id = 2,
                            TypeName = "Credit"
                        });
                });

            modelBuilder.Entity("CustomerServiceApi.Models.CustomerLedger", b =>
                {
                    b.HasOne("CustomerServiceApi.Models.CustomerMaster", "CustomerMaster")
                        .WithMany()
                        .HasForeignKey("CustomerMasterId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CustomerServiceApi.Models.TransactionType", "TransactionType")
                        .WithMany()
                        .HasForeignKey("TransactionTypeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
