using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rabbit
{
    public class RabbitContext : DbContext
    {
        public RabbitContext(DbContextOptions<RabbitContext> options) : base(options) { }

        public DbSet<Animal> Animals { get; set; }
    }
}
