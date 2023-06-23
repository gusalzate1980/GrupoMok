using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericDataBase
{
    public interface IDataBase
    {
        void AddParameter(DbType type, string Name, int Size, object Value);
        void AddParameter(SqlDbType type, string Name, int Size, object Value);
        void ConfigExecution(string connectionString,string sql, System.Data.CommandType Type);
        void Query();
        void UpdateOrDelete();
        DataSet GetDataSet();
        void NonQuery();
        void Dispose();

        bool IsConnected();
    }
}
