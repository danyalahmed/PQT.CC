using Microsoft.EntityFrameworkCore;
using PQT.CC.Models;
using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace PQT.CC.Data
{
    public class DbInitializer
    {
        public static async Task Initialize(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();
            // Look for any users.
            if (await context.Applicants.AnyAsync())
            {
                return; // DB has been seeded
            }

            SeedCards(context);
            SeedUsers(context);
            //SeedSomeRandomResults(context);
        }

        private static async void SeedCards(ApplicationDbContext context)
        {
            Random rnd = new Random();
            var Cards = new List<Card>
            {
                new Card { Name = "Barclaycard", APR = 34.9},
                new Card { Name = "Vanquis", APR = 35.4}
            };

            await context.AddRangeAsync(PreparePromotions());
            await context.SaveChangesAsync();

            var promos = await context.Promotions.ToListAsync();
            foreach (var card in Cards)
            {
                card.Promotion = promos[rnd.Next(0, promos.Count - 1)];
            }

            await context.AddRangeAsync(Cards);
            await context.SaveChangesAsync();
        }

        private static async void SeedUsers(ApplicationDbContext context)
        {
            await context.AddRangeAsync(PrepareApplicants());
            await context.SaveChangesAsync();
        }

        private static List<Promotion> PreparePromotions()
        {
            var PromoMessagesFile = Path.Combine("data", "PromoMessages.csv");
            var PromoMessages = new List<Promotion>();

            using (var stReader = File.OpenText(PromoMessagesFile))
            {
                while (!stReader.EndOfStream)
                {
                    var line = stReader.ReadLine();
                    var data = line
                        .Split(new[] { ',' });
                    if (Int32.TryParse(data[0], out int d))
                    {
                        var promo = new Promotion
                        {
                            //ID = Int32.Parse(data[0]),
                            Message = data[1],
                            Start = DateTime.Parse(data[2]),
                            End = DateTime.Parse(data[3]),
                            IsActive = Boolean.Parse(data[4])
                        };
                        PromoMessages.Add(promo);
                    }
                }
            }
            return PromoMessages;
        }

        private static List<Applicant> PrepareApplicants()
        {
            var applicantFile = Path.Combine("data", "Applicants.csv");
            var Applicants = new List<Applicant>();

            using (var stReader = File.OpenText(applicantFile))
            {
                while (!stReader.EndOfStream)
                {
                    var line = stReader.ReadLine();
                    var data = line
                        .Split(new[] { ',' });
                    if (Int32.TryParse(data[0], out int d))
                    {
                        var applicant = new Applicant
                        {
                            //ID = Int32.Parse(data[0]),
                            FirstName = data[1],
                            LastName = data[2],
                            DOB = DateTime.Parse(data[3]),
                            AnnualIncome = Double.Parse(data[4])
                        };
                        Applicants.Add(applicant);
                    }
                }
            }
            return Applicants;
        }
    }
}
