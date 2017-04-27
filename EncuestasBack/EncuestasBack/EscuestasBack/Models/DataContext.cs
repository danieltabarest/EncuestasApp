using System.Data.Entity;

namespace EncuestasBack.Models
{
    public class DataContext : DbContext
    {
        public DataContext() : base("DefaultConnection")
        {
        }

        public System.Data.Entity.DbSet<EncuestasBack.Models.Contact> Contacts { get; set; }
    }
}