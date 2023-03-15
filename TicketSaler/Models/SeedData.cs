using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Diagnostics.Metrics;
using System;

namespace TicketSaler.Models
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new AppDBContext(serviceProvider.GetRequiredService<DbContextOptions<AppDBContext>>()))
            {
                var users = new List<User>
                    {
                        new User()
                        {
                            Email="admin@mail.com",
                            Password="123",
                            FirstName="Denis",
                            LastName="Horunzhyi",
                            Date=DateTime.Now,
                            PhoneNumber="+77762154569",
                            AcsessLevel = "admin"

                        },
                        new User()
                        {
                            Email="manager@mail.com",
                            Password="123",
                            FirstName="Aizat",
                            LastName="Garifullin",
                            Date=DateTime.Now,
                            PhoneNumber="+77774005687",
                            AcsessLevel ="manager"


                        },
                        new User()
                        {
                            Email="manager2@mail.com",
                            Password="123",
                            FirstName="Arsen",
                            LastName="Esinaliev",
                            Date=DateTime.Now,
                            PhoneNumber="+77478004014",
                            AcsessLevel = "manager",


                        },
                        new User()
                        {
                            Email="user@mail.com",
                            Password="123",
                            FirstName="Karmalin",
                            LastName="Andrey",
                            Date=DateTime.Now,
                            PhoneNumber="+77765001055",
                            AcsessLevel = "user"

                        },
                        new User()
                        {
                            Email="User2@mail.com",
                            Password="123",
                            FirstName="Alan",
                            LastName="Isaev",
                            Date=DateTime.Now,
                            PhoneNumber="+77774896321",
                            AcsessLevel ="user",

                        }
                    };
                if (!context.Users.Any())
                {
                    context.Users.AddRange(users);
                }
                var Events = new List<Events>
                {
                    new Events()
                    {
                        TicketPrice = 11000,
                        EventAdress ="Zharokova Street",
                        Name = "Stand-Up",
                        AgeRating = "18+",
                        EventTime = DateTime.Now,
                        Description = "Чорт Приехал",
                        MaxCapacity = 500,
                        SoldPlace = 381
                    },
                    new Events()
                    {
                        TicketPrice = 2100,
                        EventAdress = "Pod'ezd",
                        Name = "Rap Festival",
                        AgeRating = "16+",
                        EventTime = DateTime.Now,
                        Description = "Он сказал:будет жарко (на улице -50)",
                        MaxCapacity = 50,
                        SoldPlace = 6
                    }
                };
                if (!context.Users.Any())
                {
                    context.Events.AddRange(Events);
                }
                var userEvents = new List<UsersEvent>()
                {
                    new UsersEvent()
                    {
                        User = users[3],
                        UserId = users[3].UserId,
                        Events = Events[0],
                        EventsId = Events[0].EventsId
                    },
                    new UsersEvent()
                    {
                        User = users[4],
                        UserId = users[4].UserId,
                        Events = Events[0],
                        EventsId = Events[0].EventsId
                    },
                      new UsersEvent()
                    {
                        User = users[3],
                        UserId = users[3].UserId,
                        Events = Events[1],
                        EventsId = Events[1].EventsId
                    },
                    new UsersEvent()
                    {
                        User = users[4],
                        UserId = users[4].UserId,
                        Events = Events[1],
                        EventsId = Events[1].EventsId
                    }
                };
                if (!context.UsersEvent.Any())
                {
                    context.UsersEvent.AddRange(userEvents);
                }

                context.SaveChanges();
            }
        }
    }
}