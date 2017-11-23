using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace thanksgiving.Model
{
	public class DbBuilder : DbContext
	{
		// DB-RELATED: ADD THESE CONTRUCTORS!
		public DbBuilder() { }
		public DbBuilder(DbContextOptions<DbBuilder> options) : base(options) { }

		// DB-RELATED: CREATE A DB FOR EACH EXISTING MODEL(S)
		//public DbSet<Greetings> Friends { get; set; }
		//public DbSet<Greetings> Frenemies { get; set; }
		//public DbSet<Greetings> Enemies { get; set; }
		public DbSet<Greetings> Greetings { get; set; }



	}
}
