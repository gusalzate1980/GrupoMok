using DataBase;
using Dto.Classes;
using Dto.Classes.Geography;
using GenericDataBase;
using Microsoft.Extensions.Configuration;

namespace BusinessRules.Classes
{
    public class StoreBr : GenericBr<Object>
    {
        private IDataBase _iDataBase { set; get; }
        private IConfiguration _configuration { set; get; }

        private StoreDb _storeDb { set; get; }
        public object DataReturned { set;get; }

        public StoreBr(IConfiguration configuration) 
        { 
            this._configuration = configuration;
            this._iDataBase= new SqlServer();
        }

        public void GetStoresSellTheMostByMonth(int month)
        {
            this._storeDb = new StoreDb(this._iDataBase, this._configuration);

            var pivot = this._storeDb.GetStoresSellTheMostByMonth(month);
            this.BuildRulesResponse("","Data could not be retrieve", 1, this._storeDb.ExecutionOk, this._storeDb.BusinessRuleError, this._storeDb.Exception);
            this.DataReturned = pivot;
        }

        public void GetNearestStoresToUserLocation(PointDto point)
        {
            this._storeDb = new StoreDb(this._iDataBase, this._configuration);

            var stores = this._storeDb.GetNearestStoresToUserLocation(point);
            this.BuildRulesResponse("", "Data could not be retrieve", 1, this._storeDb.ExecutionOk, this._storeDb.BusinessRuleError, this._storeDb.Exception);
            this.DataReturned = stores;
        }
    }
}