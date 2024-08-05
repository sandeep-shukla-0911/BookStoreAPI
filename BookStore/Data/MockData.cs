using BookStore.Constants;
using BookStore.Models;

namespace BookStore.Data
{
    public static class MockData
    {
        public static readonly List<Books> books =
        [
            new() {
                Author = "Sandeep Shukla",
                Title = "ASP.NET Core 5 and Angular",
                Description = "Lorem ipsum, ASP.NET Core 5 and Angular",
                Category = "Programming",
                Id = 1,
                Language = "English",
                Price = 500
            },
            new()
            {
                Author = "Sandeep Shukla",
                Title = "React JS",
                Description = "Lorem ipsum, React JS",
                Category = "Programming",
                Id = 2,
                Language = "English",
                Price = 500
            },
            new()
            {
                Author = "Sandeep Shukla",
                Title = "Mobile Programming",
                Description = "Lorem ipsum, Mobile Programming",
                Category = "Programming",
                Id = 3,
                Language = "English",
                Price = 500
            },
            new()
            {
                Author = "Dummy Author",
                Title = "Dummy Novel",
                Description = "Lorem ipsum, Mobile Programming",
                Category = "Fiction",
                Id = 4,
                Language = "English",
                Price = 300
            },
            new()
            {
                Author = "Dummy Author",
                Title = "Dummy Novel Again",
                Description = "Lorem ipsum, Mobile Programming",
                Category = "Fiction",
                Id = 5,
                Language = "English",
                Price = 600
            },
        ];

        public static readonly List<Users> users =
        [
            new()
            {
                Id = 1,
                UserName = "admin",
                Password = "admin",
                Role = Global.UserRoleAdmin,
                FullName = "Sandeep Admin"
            },
             new()
            {
                Id = 2,
                UserName = "user",
                Password = "user",
                Role = Global.UserRoleUser,
                FullName = "Sandeep User"
            }
        ];

        public static readonly List<Orders> orders =
        [
            new()
            {
                Id = 1,
                OrderDate = DateTime.UtcNow,
                UserId = 1,
                TotalAmount = 1500,
                AddressId = 1
            },
            new()
            {
                Id = 2,
                OrderDate = DateTime.UtcNow,
                UserId = 1,
                TotalAmount = 500,
                AddressId = 1
            },
            new()
            {
                Id = 3,
                OrderDate = DateTime.UtcNow,
                UserId = 2,
                TotalAmount = 500,
                AddressId = 2
            }
        ];

        public static readonly List<OrderLine> orderLines = 
        [
            new()
            {
                Id = 1,
                OrderId = 1,
                BookId = 1,
                Quantity = 2,
                Price = 1000
            },
            new()
            {
                Id = 2,
                OrderId = 1,
                BookId = 2,
                Quantity = 1,
                Price = 500
            },
            new()
            {
                Id = 3,
                OrderId = 2,
                BookId = 3,
                Quantity = 1,
                Price = 500
            },
            new()
            {
                Id = 4,
                OrderId = 3,
                BookId = 3,
                Quantity = 1,
                Price = 500
            }
        ];

        public static readonly List<Address> addresses =
        [
            new()
            {
                Id = 1,
                UserId = 1,
                AddressLine1 = "38 Main",
                AddressLine2 = "2nd Cross",
                City = "Bangalore",
                Region = "Karnataka",
                Country = "India",
                Email = "sandeep.shukla@euromonitor.com",
                Mobile = "9820908679",
                ZipCode = "560068",
                Phone = "080-23456789",
            },
            new()
            {
                Id = 2,
                UserId = 2,
                AddressLine1 = "38 Main",
                AddressLine2 = "2nd Cross",
                City = "Bangalore",
                Region = "Karnataka",
                Country = "India",
                Email = "sandeep.shukla@euromonitor.com",
                Mobile = "9820908679",
                ZipCode = "560068",
                Phone = "080-23456789",
            }
        ];
    }
}
