using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excel2Sqlite.Models.Sql
{
    class SqlRepAdapter : IDisposable
    {
        private SqlRep SqlRep { get; }
        private SQLiteConnection Connection { get; }

        public SqlRepAdapter(SqlRep sqlRep, SQLiteConnection connection)
        {
            SqlRep = sqlRep;
            Connection = connection;
        }

        public void BuildDatabase()
        {

            // CREATE DB
            var createStatement = "Create Table Features";
            var headersWithType = SqlRep.Headers
                .Select(header => $"[{header.Value}]" + " " + header.DataType.GetSqlDataType())
                .Aggregate((current, next) => $"{current}, {next}");

            var createQuery = $"{createStatement} ({headersWithType})";

            ExecuteQuery(createQuery);

            // INSERT TO DB
            var headers = SqlRep.Headers
                .Select(header => $"[{header.Value}]")
                .ToArray();

            var atHeaders = headers
                .Select(header => "@" + header.Trim('[', ']').Replace(" ", ""))
                .ToArray();

            var cmdText = $"INSERT INTO Features({String.Join(", ", headers)}) Values ({String.Join(", ", atHeaders)})";

            Connection.Open();
            var transaction = Connection.BeginTransaction();
            var command = Connection.CreateCommand();
            command.CommandText = cmdText;


            for (int i = 0; i < SqlRep.Columns[0].Cells.Count; i++)
            {
                for (int n = 0; n < SqlRep.Headers.Count; n++)
                {
                    command.Parameters.AddWithValue(atHeaders[n], SqlRep.Columns[n].Cells[i].GetDynamicValue());
                }
                command.ExecuteNonQuery();
            }

            transaction.Commit();
            transaction.Dispose();
            Connection.Close();
        }

        public void ExecuteQuery(string query)
        {
            Connection.Open();
            using (var command = new SQLiteCommand(query, Connection))
            {
                command.ExecuteNonQuery();
            }

            Connection.Close();
        }


        public void Dispose()
        {
            Connection.Dispose();
        }
    }
}
