using DataBase;
using Dto.Classes;
using GenericDataBase;
using Microsoft.Extensions.Configuration;

namespace BusinessRules.Classes
{
    public class ProductBr : GenericBr<Object>
    {
        private IDataBase _iDataBase { set; get; }
        private IConfiguration _configuration { set; get; }

        private ProductDb _productDb { set; get; }


        private string errors = "";
        public ProductBr(IConfiguration configuration) 
        { 
            this._configuration = configuration;
            this._iDataBase= new SqlServer();
        }

        public void InsertProduct(ProductDto product)
        {
            this._productDb = new ProductDb(this._iDataBase,this._configuration);

            var id = this._productDb.InsertProduct(product);

            this.BuildRulesResponse("Product inserted","Product could not be inserted", id,this._productDb.ExecutionOk,this._productDb.BusinessRuleError,this._productDb.Exception);
        }

        private bool ValidateFilter(int numRows,string logicalOperator)
        {
            bool result = true;
            
            if (numRows > 100)
            {
                errors = "You can not fetch more than 100 records";
                result = false;
            }

            if (logicalOperator != "AND" && logicalOperator != "OR")
            {
                result = false;
                if (errors == "")
                {
                    errors = "LogicalOperator value must be  AND or OR";
                }
                else
                {
                    errors += ", LogicalOperator value must be 0 for AND or 1 for OR";
                }
            }

            return result;
        }

        public void GetFilteredProducts(PaginationDto<GetFilteredProductsDto> filter)
        {
            this._productDb = new ProductDb(this._iDataBase, this._configuration);

            if(ValidateFilter(filter.NumRows,filter.LogicalOperator))
            {
                this.Data = this._productDb.GetFilteredProducts(filter);

                this.BuildRulesResponse("", "", 1, true, "", this._productDb.Exception);
            }
            else
            {
                this.BuildRulesResponse("", errors, 0, false, "", this._productDb.Exception);
            }

            
        }

        public void UpdateProduct(ProductDto product)
        {
            this._productDb = new ProductDb(this._iDataBase, this._configuration);

            var id = this._productDb.UpdateProduct(product);
            this.BuildRulesResponse("Product updated","Product could not be updated", id, this._productDb.ExecutionOk, this._productDb.BusinessRuleError, this._productDb.Exception);
            
        }

        public void RateProduct(RateProductDto rateProduct)
        {
            this._productDb = new ProductDb(this._iDataBase, this._configuration);

            var id = this._productDb.RateProduct(rateProduct);
            this.BuildRulesResponse("Product rated","Product could not be rated", id, this._productDb.ExecutionOk, this._productDb.BusinessRuleError, this._productDb.Exception);
        }
    }
}