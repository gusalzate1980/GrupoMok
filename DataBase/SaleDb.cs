using Dto.Classes;
using GenericDataBase;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace DataBase
{
    public class SaleDb:CommonSql
    {
        private IDataBase iDataBase;
        private IConfiguration _configuration;
        private string _connectionString;
       
        public SaleDb(IDataBase iDataBase, IConfiguration configuration) : base(configuration)
        {
            this.iDataBase = iDataBase;
            this._configuration = configuration;
            this.BusinessRuleError = "";
        }

        public int InsertSale(SaleDto sale)
        {
            var Id = 0;
            try
            {
                this.iDataBase.ConfigExecution(this.ConnectionString, "InsertSale", CommandType.StoredProcedure);
                this.iDataBase.AddParameter(DbType.Int32, "IdStore", 0, sale.IdStore);
                this.iDataBase.AddParameter(DbType.Int32, "IdCustomer", 0, sale.IdCustomer);

                this.ExecutionOk = true;

                if (this.iDataBase.IsConnected())
                {
                    this.iDataBase.Query();
                    
                    Id = Int32.Parse(this.iDataBase.GetDataSet().Tables[0].Rows[0][0].ToString());
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
            }

            return Id;
        }

        public bool InsertItemSale(ItemSaleDto item)
        {
            var Id = 0;
            try
            {
                this.iDataBase.ConfigExecution(this.ConnectionString, "InsertItemSale", CommandType.StoredProcedure);
                this.iDataBase.AddParameter(DbType.Int32, "IdSale", 0, item.IdSale);
                this.iDataBase.AddParameter(DbType.Int32, "IdProduct", 0, item.IdProduct);
                this.iDataBase.AddParameter(DbType.Int32, "Quantity", 0, item.Quantity);

                this.ExecutionOk = true;

                if (this.iDataBase.IsConnected())
                {
                    this.iDataBase.NonQuery();
                    return true;
                }
                else
                {
                    this.BusinessRuleError = "There is not data base connection";
                    return false;
                }
            }
            catch (Exception e)
            {
                this.ExecutionOk = false;
                this.Exception = e;
                return false;
            }
        }

        public void DeleteSale(int idSale)
        {
            var Id = 0;
            try
            {
                this.iDataBase.ConfigExecution(this.ConnectionString, "DeleteSale", CommandType.StoredProcedure);
                this.iDataBase.AddParameter(DbType.Int32, "IdSale", 0, idSale);
                
                this.ExecutionOk = true;

                if (this.iDataBase.IsConnected())
                {
                    this.iDataBase.NonQuery();
                }
                else
                {
                    this.BusinessRuleError = "There is not data base connection";
                }
            }
            catch (Exception e)
            {
                this.ExecutionOk = false;
                this.Exception = e;
            }
        }

        public void UpdateSalePrice(int idSale)
        {
            var Id = 0;
            try
            {
                this.iDataBase.ConfigExecution(this.ConnectionString, "UpdateSalePrice", CommandType.StoredProcedure);
                this.iDataBase.AddParameter(DbType.Int32, "IdSale", 0, idSale);

                this.ExecutionOk = true;

                if (this.iDataBase.IsConnected())
                {
                    this.iDataBase.NonQuery();
                }
                else
                {
                    this.BusinessRuleError = "There is not data base connection";
                }
            }
            catch (Exception e)
            {
                this.ExecutionOk = false;
                this.Exception = e;
            }
        }

        private List<PurchasedItem> BuildPurchaseItems(DataTable table)
        {
            List<PurchasedItem> list = new List<PurchasedItem>();

            foreach (DataRow row in table.Rows) 
            {
                list.Add(new PurchasedItem()
                {
                    Name = row["Name"].ToString(),
                    Price = Int32.Parse(row["Price"].ToString()),
                    Quantity = Int32.Parse(row["Quantity"].ToString())
                });
            }

            return list;
        }

        private PurchaseNotificationDto BuildPurchaseNotificationDto()
        {
            DataTableCollection tables= this.iDataBase.GetDataSet().Tables;
            PurchaseNotificationDto dto = new PurchaseNotificationDto()
            {
                Customer=   tables[0].Rows[0]["Customer"].ToString(),
                Email   =   tables[0].Rows[0]["Email"].ToString(),
                Store   =   tables[0].Rows[0]["Store"].ToString(),
                Subject =   "Your order is on the way",
                PurchasedItems = BuildPurchaseItems(tables[1])
            };

            return dto;
        }

        public PurchaseNotificationDto GetDataToNotifyPurchase(int idSale)
        {
            var Id = 0;
            try
            {
                this.iDataBase.ConfigExecution(this.ConnectionString, "GetDataToNotifyPurchase", CommandType.StoredProcedure);
                this.iDataBase.AddParameter(DbType.Int32, "IdSale", 0, idSale);

                this.ExecutionOk = true;

                if (this.iDataBase.IsConnected())
                {
                    this.iDataBase.Query();

                    return BuildPurchaseNotificationDto();
                }
                else
                {
                    this.BusinessRuleError = "There is not data base connection";
                    Id = 0;
                    return new PurchaseNotificationDto();
                }
            }
            catch (Exception e)
            {
                this.ExecutionOk = false;
                this.Exception = e;
                Id = 0;
                return new PurchaseNotificationDto();
            }
        }

    }
}