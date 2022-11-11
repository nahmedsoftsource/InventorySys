using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Context
{
    public class DBConnectionFactory : SqlConnectionFactory
    {
        public DBConnectionFactory(Action<SqlConnection> connectionBuilder) : base(connectionBuilder) { }
    }
}
