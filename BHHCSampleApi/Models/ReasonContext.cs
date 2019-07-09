using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace BHHCSampleApi.Models
{
    public class ReasonContext : DbContext
    {
        public const string REASON1 = "I can identify with BHHC values of all inclusiveness";
        public const string REASON2 = "I can relate to BHHC mission to achieve its vision by means of its internal as well as external resources";
        public const string REASON3 = "I am one with BHHC vision of being the best in its class of business";
        public ReasonContext(DbContextOptions<ReasonContext> options)
            : base(options)
        {
        }

        public DbSet<ReasonItem> ReasonItems { get; set; }
    }
}
