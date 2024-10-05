using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

public class CarDbContext : DbContext
{

        public CarDbContext(DbContextOptions<CarDbContext> options) : base(options) { }

        public DbSet<Car> Cars { get; set; }

}