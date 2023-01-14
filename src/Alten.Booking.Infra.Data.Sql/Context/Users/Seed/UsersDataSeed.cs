using Alten.Booking.Infra.Data.Sql.Context.Users.Models;

namespace Alten.Booking.Infra.Data.Sql.Context.Users.Seed;

public static class UsersDataSeed
{
    public static UserModel[] Seed => new[]
    {
        new UserModel(1, "Roger", "Teles", "066.114.819-07",
            DateTime.Today.AddYears(-33), "rogers@email.com")
        {
            Active = true
        }
    };
}