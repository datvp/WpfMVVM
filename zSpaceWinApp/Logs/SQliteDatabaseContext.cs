using System.Data.Common;
using System.Data.Entity;
using System.Data.SQLite;
using zSpaceWinApp.Model;

namespace zSpaceWinApp.Logs
{
    public class SQliteDatabaseContext: DbContext
    {
        public SQliteDatabaseContext(DbConnection connection)
            : base(connection, true)
        {
        }
        public SQliteDatabaseContext(string ConnectString) : base(new SQLiteConnection(ConnectString), true)
        {

        }
        public DbSet<Downloads> Downloads { get; set; }
        public DbSet<Errors> Errors { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
