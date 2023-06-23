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