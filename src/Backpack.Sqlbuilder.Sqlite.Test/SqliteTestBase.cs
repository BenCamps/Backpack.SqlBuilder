using Backpack.SqlBuilder.Sqlite;
using Backpack.SqlBuilder.TestCommon;
using FluentAssertions;
using System.Data.Common;
using Xunit.Abstractions;



namespace BackPack.SqlBuilder.Sqlite
{
    public class SqliteTestBase : TestBase
    {

        protected DbProviderFactory DbProvider { get; set; }

        public SqliteTestBase(ITestOutputHelper output) : base(output, new SqliteDialect())
        {
#if SYSTEM_DATA_SQLITE
            DbProvider = System.Data.SQLite.SQLiteFactory.Instance;
#elif MICROSOFT_DATA_SQLITE
            DbProvider = Microsoft.Data.Sqlite.SqliteFactory.Instance;
#endif
        }

        protected void VerifyCommandSyntex(string commandText)
        {
            using (var conn = DbProvider.CreateConnection())
            {
                var command = conn.CreateCommand();
                Output.WriteLine("testing:\r\n" + commandText);
                command.CommandText = "EXPLAIN " + commandText;

#if MICROSOFT_DATA_SQLITE
                var connectionString = "Data Source =:memory:;";
#elif SYSTEM_DATA_SQLITE
                var connectionString = "Data Source =:memory:; Version = 3; New = True;";
#endif
                conn.ConnectionString = connectionString;
                conn.Open();

                command.Connection = conn;

                try
                {
                    command.ExecuteNonQuery();//calling execute should always throw but we check that it isn't a syntax exception
                }
                catch (DbException ex)
                {
                    ex.Message.Should().NotContainEquivalentOf("syntax");
                    //Assert.DoesNotContain("syntax ", ex.Message, StringComparison.InvariantCultureIgnoreCase);
                }
            }
        }
    }
}
