using authentication_authorization_crud_4_12_2025.Models;
using Microsoft.EntityFrameworkCore;

namespace authentication_authorization_crud_4_12_2025.Data
{
    public class DB : DbContext
    {
        public DB()
        {

        }

        public DB(DbContextOptions<DB> options) : base
            (options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Chocolate> chocolate_data { get; set; }
        public DbSet<UserDropDownData> dropdown_users { get; set; }
        public DbSet<Login_History> login_Histories { get; set; }
        public DbSet<ForgotPassword> forgot_Passwords { get; set; }

    }
}
