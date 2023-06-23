using Dto.Classes;
using Dto.Classes.Geography;
using GenericDataBase;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase
{
    public class StoreDb: CommonSql
    {
        private IDataBase iDataBase;
        private IConfiguration _configuration;
        private string _connectionString;

        public StoreDb(IDataBase iDataBase, IConfiguration configuration) : base(configuration)
        {
            this.iDataBase = iDataBase;
            this._configuration = configuration;
            this.BusinessRuleError = "";
        }


        private List<string> BuildPivotValues(DataRowCollection rows,string column)
        {
            List<string> values = new List<string>();
            var i=0;
            foreach(DataRow row in rows)
            {
                values.Add(row[column].ToString());
            }

            return values;
        }

        private PivotDto BuildGetStoresSellTheMostByMonth(DataSet dataSet)
        {
            PivotDto pivot = new PivotDto();
            pivot.Columns = new List<ColumnDto>();
            foreach (var column in dataSet.Tables[0].Columns)
            {
                pivot.Columns.Add(new ColumnDto()
                {
                    Name = column.ToString(),
                    Values = BuildPivotValues(dataSet.Tables[0].Rows,column.ToString())
                });
            }

            return pivot;
        }

        private List<GetNearestStoresToUserLocationDto> BuildGetNearestStoresToUserLocationDto(DataSet dataSet)
        {
List<GetNearestStoresToUserLocationDto> list = new List<GetNearestStoresToUserLocationDto>();

            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                list.Add(new GetNearestStoresToUserLocationDto()
                {
                    Address = row["Address"].ToString(),
                    Id = Int32.Parse(row["Id"].ToString()),
                    Distance = Decimal.Parse(row["Distance"].ToString()),
                    Name = row["Name"].ToString(),
                    Phone= row["Phone"].ToString()
                });
            }

            return list;
        }

        public PivotDto GetStoresSellTheMostByMonth(int month)
        {
            var Id = 0;
            try
            {
                this.iDataBase.ConfigExecution(this.ConnectionString, "GetStoresSellTheMostByMonth", CommandType.StoredProcedure);
                this.iDataBase.AddParameter(DbType.Int32, "Month", 0, month);

                this.ExecutionOk = true;
                this.BusinessRule = true;

                if (this.iDataBase.IsConnected())
                {
                    this.iDataBase.Query();
                    return BuildGetStoresSellTheMostByMonth(this.iDataBase.GetDataSet());
                }
                else
                {
                    this.BusinessRuleError = "There is not data base connection";
                    Id = 0;
                }
            }
            catch (Exception e)
            {
                this.ExecutionOk = false;
                this.Exception = e;
                Id = 0;
                this.BusinessRule = false;
            }

            return new PivotDto();
        }

        public List<GetNearestStoresToUserLocationDto> GetNearestStoresToUserLocation(PointDto point)
        {
            var Id = 0;
            try
            {
                this.iDataBase.ConfigExecution(this.ConnectionString, "GetNearestStoresToUserLocation", CommandType.StoredProcedure);
                this.iDataBase.AddParameter(DbType.String, "X", 20, point.X);
                this.iDataBase.AddParameter(DbType.String, "Y", 20, point.Y);

                this.ExecutionOk = true;
                this.BusinessRule = true;

                if (this.iDataBase.IsConnected())
                {
                    this.iDataBase.Query();
                    return BuildGetNearestStoresToUserLocationDto(this.iDataBase.GetDataSet());
                }
                else
                {
                    this.BusinessRuleError = "There is not data base connection";
                    Id = 0;
                    this.BusinessRule = false;
                }
            }
            catch (Exception e)
            {
                this.ExecutionOk = false;
                this.Exception = e;
                this.BusinessRule = false;
                Id = 0;
            }

            return new List<GetNearestStoresToUserLocationDto>();
        }
    }
}