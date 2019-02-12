using MaxLifts.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;


namespace MaxLifts.DAL
{
    public class MaxLiftsContext : DbContext
    {
        public MaxLiftsContext() : base("MaxLiftsContext")
        {
        }

        public DbSet<Set> Sets { get; set; }

        
    }
}


      