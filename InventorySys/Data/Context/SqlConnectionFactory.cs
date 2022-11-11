using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Context
{
    public class SqlConnectionFactory
    {
        private readonly Action<SqlConnection> connectionBuilder;

        public SqlConnectionFactory(Action<SqlConnection> connectionBuilder)
        {
            this.connectionBuilder = connectionBuilder;
        }

        public SqlConnection GetOpenConnection()
        {
            var connection = new SqlConnection();
            connectionBuilder(connection);
            connection.Open();
            return connection;
        }
    }
}
