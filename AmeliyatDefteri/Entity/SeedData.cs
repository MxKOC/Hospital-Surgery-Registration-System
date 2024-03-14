using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace AmeliyatDefteri.Entity
{
    public static class SeedData
    {
        public static void TestVerileriniDoldur(IApplicationBuilder app)
        {
            string tarihStr = "26/03/2024";
            DateTime gun = DateTime.ParseExact(tarihStr, "dd/MM/yyyy", null);

            var context = app.ApplicationServices.CreateScope().ServiceProvider.GetService<DataContext>();

            if (context != null)
            {
                if (context.Database.GetPendingMigrations().Any())
                {
                    context.Database.Migrate();
                }
                var Pazartesi = DayOfWeek.Monday;
                var Salı = DayOfWeek.Tuesday;
                var Çarşamba = DayOfWeek.Wednesday;
                var Perşembe = DayOfWeek.Thursday;
                var Cuma = DayOfWeek.Friday;

                if (!context.AmeliyatGunleri.Any())
                {
                    context.AmeliyatGunleri.AddRange(
                        new AmeliyatGun { GunTurkce="Salı", Gun = Salı },
                        new AmeliyatGun { GunTurkce="Perşembe", Gun = Perşembe }
                        );

                    context.SaveChanges();
                }



                if (!context.Ameliyatlar.Any())
                {
                    context.Ameliyatlar.AddRange(
                        new Ameliyat { Name = "AAAA" },
                        new Ameliyat { Name = "BBBB" },
                        new Ameliyat { Name = "CCCC" },
                        new Ameliyat { Name = "DDDD" }
                        );

                    context.SaveChanges();
                }

                if (!context.Anesteziler.Any())
                {
                    context.Anesteziler.AddRange(
                        new Anestezi { Name = "Tam" },
                        new Anestezi { Name = "Roa" }
                        );

                    context.SaveChanges();
                }



                if (!context.Doktorlar.Any())
                {
                    context.Doktorlar.AddRange(
                        new Doktor { Name = "XXX" },
                        new Doktor { Name = "YYY" },
                        new Doktor { Name = "ZZZ" }

                    );
                    context.SaveChanges();
                }


                if (!context.Zamanlar.Any())
                {
                    context.Zamanlar.AddRange(
                        new Zaman
                        {
                            DoktorId = 1,
                            Name = "Muhammed Koç",
                            Telefon = "123456",
                            AmeliyatId = 1,
                            AnesteziId = 1,
                            Detay = "Detay Bilgisi Göster",
                            AmeliyatGünü = gun
                        },
                        new Zaman
                        {
                            DoktorId = 2,
                            Name = "Abdullah Koç",
                            Telefon = "123456",
                            AmeliyatId = 2,
                            AnesteziId = 1,
                            Detay = "Detay Bilgisi Göster",
                            AmeliyatGünü = gun
                        },
                        new Zaman
                        {
                            DoktorId = 3,
                            Name = "Sena Koç",
                            Telefon = "123456",
                            AmeliyatId = 3,
                            AnesteziId = 1,
                            Detay = "Detay Bilgisi Göster",
                            AmeliyatGünü = gun
                        },
                        new Zaman
                        {
                            DoktorId = 3,
                            Name = "Tuğba Koç",
                            Telefon = "123456",
                            AmeliyatId = 3,
                            AnesteziId = 1,
                            Detay = "Detay Bilgisi Göster",
                            AmeliyatGünü = gun
                        },
                        new Zaman
                        {
                            DoktorId = 3,
                            Name = "Refika Koç",
                            Telefon = "123456",
                            AmeliyatId = 2,
                            AnesteziId = 1,
                            Detay = "Detay Bilgisi Göster",
                            AmeliyatGünü = gun
                        },
                        new Zaman
                        {
                            DoktorId = 3,
                            Name = "Muhammed Koç",
                            Telefon = "123456",
                            AmeliyatId = 1,
                            AnesteziId = 1,
                            Detay = "Detay Bilgisi Göster",
                            AmeliyatGünü = gun
                        },
                        new Zaman
                        {
                            DoktorId = 3,
                            Name = "Yusuf Bütün",
                            Telefon = "123456",
                            AmeliyatId = 4,
                            AnesteziId = 1,
                            Detay = "Detay Bilgisi Göster",
                            AmeliyatGünü = gun
                        },
                        new Zaman
                        {
                            DoktorId = 2,
                            Name = "Emre Koç",
                            Telefon = "123456",
                            AmeliyatId = 1,
                            AnesteziId = 1,
                            Detay = "Detay Bilgisi Göster",
                            AmeliyatGünü = gun
                        },
                        new Zaman
                        {
                            DoktorId = 3,
                            Name = "Ömer Koç",
                            Telefon = "123456",
                            AmeliyatId = 2,
                            AnesteziId = 1,
                            Detay = "Detay Bilgisi Göster",
                            AmeliyatGünü = gun
                        },
                        new Zaman
                        {
                            DoktorId = 1,
                            Name = "Salih Koç",
                            Telefon = "123456",
                            AmeliyatId = 3,
                            AnesteziId = 1,
                            Detay = "Detay Bilgisi Göster",
                            AmeliyatGünü = gun
                        }

                    );
                    context.SaveChanges();
                }







            }
        }
    }
}