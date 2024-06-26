﻿// <auto-generated />
using System;
using AmeliyatDefteri.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AmeliyatDefteri.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.8");

            modelBuilder.Entity("AmeliyatDefteri.Entity.Ameliyat", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Ameliyatlar");
                });

            modelBuilder.Entity("AmeliyatDefteri.Entity.AmeliyatGun", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Gun")
                        .HasColumnType("INTEGER");

                    b.Property<string>("GunTurkce")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("AmeliyatGunleri");
                });

            modelBuilder.Entity("AmeliyatDefteri.Entity.Anestezi", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Anesteziler");
                });

            modelBuilder.Entity("AmeliyatDefteri.Entity.Doktor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Doktorlar");
                });

            modelBuilder.Entity("AmeliyatDefteri.Entity.Duyuru", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Baslangic")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Bitis")
                        .HasColumnType("TEXT");

                    b.Property<string>("Icerik")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Duyurular");
                });

            modelBuilder.Entity("AmeliyatDefteri.Entity.Gecmis", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Ameliyat")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("AmeliyatGünü")
                        .HasColumnType("TEXT");

                    b.Property<string>("Anestezi")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Detay")
                        .HasColumnType("TEXT");

                    b.Property<string>("Doktor")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("IslemTarihi")
                        .HasColumnType("TEXT");

                    b.Property<string>("Kullanici")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Olay")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Telefon")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Gecmisler");
                });

            modelBuilder.Entity("AmeliyatDefteri.Entity.Zaman", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("AmeliyatGünü")
                        .HasColumnType("TEXT");

                    b.Property<int>("AmeliyatId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("AnesteziId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Detay")
                        .HasColumnType("TEXT");

                    b.Property<int>("DoktorId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Telefon")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("AmeliyatId");

                    b.HasIndex("AnesteziId");

                    b.HasIndex("DoktorId");

                    b.ToTable("Zamanlar");
                });

            modelBuilder.Entity("AmeliyatDefteri.Entity.Zaman", b =>
                {
                    b.HasOne("AmeliyatDefteri.Entity.Ameliyat", "Ameliyat")
                        .WithMany()
                        .HasForeignKey("AmeliyatId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AmeliyatDefteri.Entity.Anestezi", "Anestezi")
                        .WithMany()
                        .HasForeignKey("AnesteziId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AmeliyatDefteri.Entity.Doktor", "Doktor")
                        .WithMany()
                        .HasForeignKey("DoktorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Ameliyat");

                    b.Navigation("Anestezi");

                    b.Navigation("Doktor");
                });
#pragma warning restore 612, 618
        }
    }
}
