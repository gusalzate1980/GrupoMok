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
                    this.BusinessRule = false;
                }
            }
            catch (Exception e)
            {
                this.ExecutionOk = false;
                this.Exception = e;
                Id = 0;
                this.BusinessRule = false;
            }

            return Id;
        }

        private PaginatedQuery<FilteredProductsDto> BuildFilteredProducts(DataTableCollection tables,int numRows)
        {
            return new PaginatedQuery<FilteredProductsDto>()
            {
                TotalPages = (int)Math.Ceiling((double)(Int32.Parse(tables[0].Rows[0][0].ToString())/numRows)),
                TotalRecords = Int32.Parse(tables[0].Rows[0][0].ToString()),
                Result = BuildFilteredItems(tables[1].Rows)
            };
        }

        private List<FilteredProductsDto> BuildFilteredItems(DataRowCollection rows)
        {
            List<FilteredProductsDto> products  = new List<FilteredProductsDto>();

            foreach (DataRow row in rows) 
            {
                products.Add(new FilteredProductsDto()
                {
                    Brand = row["Brand"].ToString(),
                    Category = row["Category"].ToString(),
                    Name = row["Name"].ToString(),
                    Price = Int32.Parse(row["Price"].ToString()),
                    Score = Decimal.Parse(row["Score"].ToString())
                });
            }

            return products;
        }

        public PaginatedQuery<FilteredProductsDto> GetFilteredProducts(PaginationDto<GetFilteredProductsDto> filter)
        {
            var Id = 0;
            try
            {
                this.iDataBase.ConfigExecution(this.ConnectionString, "GetFilteredProducts", CommandType.StoredProcedure);
                this.iDataBase.AddParameter(DbType.Int32, "Page", 0, filter.Page);
                this.iDataBase.AddParameter(DbType.Int32, "NumRows", 0, filter.NumRows);
                this.iDataBase.AddParameter(DbType.Int32, "IdBrand", 0, filter.Filters.IdBrand);
                this.iDataBase.AddParameter(DbType.Int32, "IdCategory", 0, filter.Filters.IdCategory);
                this.iDataBase.AddParameter(DbType.String, "Name", 50, filter.Filters.Name);
                this.iDataBase.AddParameter(DbType.Int32, "PriceMin",0 , filter.Filters.PriceMin);
                this.iDataBase.AddParameter(DbType.Int32, "PriceMax", 0, filter.Filters.PriceMax);
                this.iDataBase.AddParameter(DbType.String, "LogicalOperator", 3, filter.LogicalOperator);

                this.ExecutionOk = true;

                if (this.iDataBase.IsConnected())
                {
                    this.iDataBase.Query();

                    return BuildFilteredProducts(this.iDataBase.GetDataSet().Tables,filter.NumRows);
                    this.BusinessRule = true;
                    
                }
                else
                {
                    this.BusinessRule = false;
                    this.BusinessRuleError = "There is not data base connection";
                    Id = 0;
                }
            }
            catch (Exception e)
            {
                this.ExecutionOk = false;
                this.Exception = e;
                this.BusinessRule = false;
                Id = 0;
            }

            return new PaginatedQuery<FilteredProductsDto>();
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