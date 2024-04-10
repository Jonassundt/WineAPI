using WineReviewsApplication.Data;
using WineReviewsApplication.Models;

namespace WineReviewsApplication
{
    public class Seed
    {
        private readonly DataContext dataContext;
        public Seed(DataContext context)
        {
            this.dataContext = context;
        }
        public void SeedDataContext()
        {
            if (!dataContext.WineVineyards.Any())
            {
                var wineVineyards = new List<WineVineyard>()
                {
                    new WineVineyard()
                    {
                        Wine = new Wine()
                        {
                            Name = "Freemark Abbey Napa",
                            Vintage = new DateTime(1903,1,1),
                            WineCategories = new List<WineCategory>()
                            {
                                new WineCategory { WineType = new WineType() { Type = "Red wine"}}
                            },
                            Reviews = new List<Review>()
                            {
                                new Review { Title="Freemark Abbey Napa",Text = "Freemark Abbey Napa is the best wine because it tastes good", Rating = 8,
                                Reviewer = new Reviewer(){ FirstName = "Jonas", LastName = "Sundt" } },
                                new Review { Title="Freemark Abbey Napa", Text = "Freemark Abbey Napa is the best wine because it smells good", Rating = 5,
                                Reviewer = new Reviewer(){ FirstName = "Runnar", LastName = "Sundt" } },
                                new Review { Title="Freemark Abbey Napa",Text = "red wine red wine red wine.... amazing", Rating = 7,
                                Reviewer = new Reviewer(){ FirstName = "Connor", LastName = "McGregor" } },
                            }
                        },
                        Vineyard = new Vineyard()
                        {
                            Name = "Italiana Winehouse",
                            Estate = "Brockswine",
                            Country = new Country()
                            {
                                Name = "Italy"
                            }
                        }
                    },
                    new WineVineyard()
                    {
                        Wine = new Wine()
                        {
                            Name = "Esmeralda",
                            Vintage = new DateTime(1922,1,1),
                            WineCategories = new List<WineCategory>()
                            {
                                new WineCategory { WineType = new WineType() { Type = "White"}}
                            },
                            Reviews = new List<Review>()
                            {
                                new Review { Title= "Esmeralda", Text = "Esmeralda is actually really good", Rating = 5,
                                Reviewer = new Reviewer(){ FirstName = "Jonas", LastName = "Sundt" } },
                                new Review { Title= "Esmeralda",Text = "I like Esmeralda a lot because its taste is soft...", Rating = 5,
                                Reviewer = new Reviewer(){ FirstName = "Jonas", LastName = "Sundt" } },
                                new Review { Title= "Esmeralda", Text = "Esmeralda, it shouldve been made in Norway...", Rating = 1,
                                Reviewer = new Reviewer(){ FirstName = "Ronaldo", LastName = "McDonald" } },
                            }
                        },
                        Vineyard = new Vineyard()
                        {
                            Name = "Cataluna vineyard",
                            Estate = "Catalunjado",
                            Country = new Country()
                            {
                                Name = "Spain"
                            }
                        }
                    },
                                    new WineVineyard()
                    {
                        Wine = new Wine()
                        {
                            Name = "Jean David Rose",
                            Vintage = new DateTime(1953,1,1),
                            WineCategories = new List<WineCategory>()
                            {
                                new WineCategory { WineType = new WineType() { Type = "Rose"}}
                            },
                            Reviews = new List<Review>()
                            {
                                new Review { Title= "Jean David Rose", Text = "Jean David Rose tastes french", Rating = 5,
                                Reviewer = new Reviewer(){ FirstName = "Jonas", LastName = "Sundt" } },
                                new Review { Title= "Jean David Rose",Text = "I like Jean David Rose a lot because its taste is hard...", Rating = 5,
                                Reviewer = new Reviewer(){ FirstName = "Jonas", LastName = "Sundt" } },
                                new Review { Title= "Jean David Rose", Text = "Jean David Rose, it shouldve been made in Norway...", Rating = 1,
                                Reviewer = new Reviewer(){ FirstName = "Kari", LastName = "Nordmann" } },
                            }
                        },
                        Vineyard = new Vineyard()
                        {
                            Name = "French Riviera Vineyard",
                            Estate = "Rivieroui",
                            Country = new Country()
                            {
                                Name = "France"
                            }
                        }
                    }
                };
                dataContext.WineVineyards.AddRange(wineVineyards);
                dataContext.SaveChanges();
            }
        }
    }
}