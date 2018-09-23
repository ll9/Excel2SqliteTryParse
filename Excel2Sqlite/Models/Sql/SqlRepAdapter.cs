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
            CreateTable();
            InsertRows();
        }

        private void InsertRows()
        {
            var headers = SqlRep.Headers
                .Select(header => header.Value)
                .ToArray();

            var headersParameter = headers
                .Select(header => "@" + header)
                .ToArray();

            var cmdText = $"INSERT INTO Features({string.Join(", ", headers)}) Values ({string.Join(", ", headersParameter)})";

            Connection.Open();
            var transaction = Connection.BeginTransaction();
            var command = Connection.CreateCommand();
            command.CommandText = cmdText;


            for (int cellIndex = 0; cellIndex < SqlRep.Columns[0].Cells.Count; cellIndex++)
            {
                for (int columnIndex = 0; columnIndex < SqlRep.Headers.Count; columnIndex++)
                {
                    command.Parameters.AddWithValue(headersParameter[columnIndex], SqlRep.Columns[columnIndex].Cells[cellIndex].GetDynamicValue());
                }
                command.ExecuteNonQuery();
            }

            transaction.Commit();
            transaction.Dispose();
            Connection.Close();
        }

        private void CreateTable()
        {
            var createStatement = "Create Table Features";
            var headersWithType = SqlRep.Headers
                .Select(header => header.Value + " " + header.DataType.GetSqlDataType())
                .Aggregate((current, next) => $"{current}, {next}");

            var createQuery = $"{createStatement} ({headersWithType})";

            ExecuteQuery(createQuery);
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
