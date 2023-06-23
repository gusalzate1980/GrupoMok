using Dto.Classes;
using GenericDataBase;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace DataBase
{
    public class ProductDb:CommonSql
    {
        private IDataBase iDataBase;
        private IConfiguration _configuration;
        private string _connectionString;
       
        public ProductDb(IDataBase iDataBase, IConfiguration configuration) : base(configuration)
        {
            this.iDataBase = iDataBase;
            this._configuration = configuration;
            this.BusinessRuleError = "";
        }

        public int InsertProduct(ProductDto product)
        {
            var Id = 0;
            try
            {
                this.iDataBase.ConfigExecution(this.ConnectionString,"InsertProduct", CommandType.StoredProcedure);
                this.iDataBase.AddParameter(DbType.Int32, "IdBrand", 0, product.IdBrand);
                this.iDataBase.AddParameter(DbType.Int32, "IdCategory", 0, product.IdCategory);
                this.iDataBase.AddParameter(DbType.String, "Name", 50, product.Name);
                this.iDataBase.AddParameter(DbType.Int32, "Price", 0, product.Price);

                this.ExecutionOk = true;

                if (this.iDataBase.IsConnected())
                {
                    this.iDataBase.Query();
                    

                    if (!this.iDataBase.GetDataSet().Tables[0].Rows[0][0].ToString().Equals("0"))
                    {
                        Id = Int32.Parse(this.iDataBase.GetDataSet().Tables[0].Rows[0][0].ToString());
                        this.BusinessRule = true;
                    }
                    else
                    {
                        Id = 0;
                        this.BusinessRuleError = this.iDataBase.GetDataSet().Tables[0].Rows[0][1].ToString();
                        this.BusinessRule = false;
                    }
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

        public int UpdateProduct(ProductDto product)
        {
            var Id = 0;
            try
            {
                this.iDataBase.ConfigExecution(this.ConnectionString, "UpdateProduct", CommandType.StoredProcedure);
                this.iDataBase.AddParameter(DbType.Int32, "IdBrand", 0, product.IdBrand);
                this.iDataBase.AddParameter(DbType.Int32, "IdCategory", 0, product.IdCategory);
                this.iDataBase.AddParameter(DbType.String, "Name", 50, product.Name);
                this.iDataBase.AddParameter(DbType.Int32, "Price", 0, product.Price);
                this.iDataBase.AddParameter(DbType.Int32, "Id", 0, product.Id);

                this.ExecutionOk = true;

                if (this.iDataBase.IsConnected())
                {
                    this.iDataBase.Query();


                    if (!this.iDataBase.GetDataSet().Tables[0].Rows[0][0].ToString().Equals("0"))
                    {
                        Id = Int32.Parse(this.iDataBase.GetDataSet().Tables[0].Rows[0][0].ToString());
                        this.BusinessRule = true;
                    }
                    else
                    {
                        Id = 0;
                        this.BusinessRuleError = this.iDataBase.GetDataSet().Tables[0].Rows[0][1].ToString();
                        this.BusinessRule = false;
                    }
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

        public int RateProduct(RateProductDto rateProduct)
        {
            var Id = 0;
            try
            {
                this.iDataBase.ConfigExecution(this.ConnectionString, "RateProduct", CommandType.StoredProcedure);
                this.iDataBase.AddParameter(DbType.Int32, "IdProduct", 0, rateProduct.IdProduct);
                this.iDataBase.AddParameter(DbType.Int32, "IdCustomer", 0, rateProduct.IdCustomer);
                this.iDataBase.AddParameter(DbType.Int32, "Score", 0, rateProduct.Score);

                this.ExecutionOk = true;


                if (this.iDataBase.IsConnected())
                {
                    this.iDataBase.Query();

                    if (!this.iDataBase.GetDataSet().Tables[0].Rows[0][0].ToString().Equals("0"))
                    {
                        Id = Int32.Parse(this.iDataBase.GetDataSet().Tables[0].Rows[0][0].ToString());
                        this.BusinessRule = true;
                    }
                    else
                    {
                        Id = 0;
                        this.BusinessRuleError = this.iDataBase.GetDataSet().Tables[0].Rows[0][1].ToString();
                        this.BusinessRule = false;
                    }
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
    }
}