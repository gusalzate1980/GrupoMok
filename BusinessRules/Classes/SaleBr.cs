using DataBase;
using Dto;
using Dto.Classes;
using GenericDataBase;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestClient;
using Util;

namespace BusinessRules.Classes
{
    public class SaleBr : GenericBr<Object>
    {
        private IDataBase _iDataBase { set; get; }
        private IConfiguration _configuration { set; get; }

        private SaleDb _saleDb { set; get; }

        public SaleBr(IConfiguration configuration) 
        { 
            this._configuration = configuration;
            this._iDataBase= new SqlServer();
        }

        public async Task InsertSale(SaleDto dto)
        {
            this._saleDb = new SaleDb(this._iDataBase,this._configuration);

            var id = this._saleDb.InsertSale(dto);
            bool saleCompleted = true;
            if (id > 0)
            {
                foreach(var item in dto.Items)
                {
                    item.IdSale = id;
                    if(!this._saleDb.InsertItemSale(item))
                    {
                        this.BuildRulesResponse("", "The sale could not be done", 0, false, this._saleDb.BusinessRuleError, this._saleDb.Exception);
                        saleCompleted = false;
                        break;
                    }
                }

                if (!saleCompleted)
                {
                    this._saleDb.DeleteSale(id);
                }
                else
                {
                    this._saleDb.UpdateSalePrice(id);

                    //notificar
                    await NotifyPurchase(id);
                }
            }
            else
            {
                this.BuildRulesResponse("", "The sale could not be done", 0, false, this._saleDb.BusinessRuleError, this._saleDb.Exception);
            }

            
        }

        private string BuildItems(List<PurchasedItem> items)
        {
            string strItems = "<br/><br/><table border = '1'><tr><td>Product</td><td>Price</td><td>Quantity</td>";
            
            var sum=0;
            foreach(var item in items)
            {
                strItems  += "<tr><td>"+item.Name+"</td><td>"+item.Price+"</td><td>"+item.Quantity+"</td></tr>";
                sum += item.Price*item.Quantity;
            }
            strItems += "<tr><td colspan='3'>Total:"+sum;
            strItems += "</table><br/><br/>";

            return strItems;
        }

        private string BuildBody(PurchaseNotificationDto notification)
        {
            string html = string.Empty;

            html = _configuration.GetSection("PurchaseNotificacionTemplate").Value.Replace("[CUSTOMER]","<B>"+notification.Customer);
            html = html.Replace("[ITEMS]",BuildItems(notification.PurchasedItems));
            html = html.Replace("[STORE]",notification.Store);

            return html;
        }

        private Message BuildEmailToNotify(PurchaseNotificationDto notification)
        {
            return new Message()
            {
                Body = BuildBody(notification),
                Subject= notification.Subject,
                ToList = new List<string>()
                { 
                    new string(notification.Email) 
                }
            };
        }

        private async Task NotifyPurchase(int idSale)
        {
            PurchaseNotificationDto notification = this._saleDb.GetDataToNotifyPurchase(idSale);
            notification.Subject= "your order is on the way";
            RestClient<WildCard> client = new RestClient<WildCard>();
            
            var message = BuildEmailToNotify(notification);
            WebServiceRequest<Message> request = new WebServiceRequest<Message>()
            {
                Data = message,
                Timestamp = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds,
                Token = Encrypt.Sha256(JsonConvert.SerializeObject(message))
            };
            
            WebServiceResponse<WildCard> response = await client.PostRequest(JsonConvert.SerializeObject(request),_configuration.GetSection("UrlNotification").Value);
            
            if (response.ServerOk && response.BusinessRulesOk) 
            {
                this.BuildRulesResponse("Your sale is on the way, check your email", "", 1, true, "", this._saleDb.Exception);
            }
            else
            {
                this.BuildRulesResponse("Your sale is on the way, but we could not sent you an email", "", 1, true, "", this._saleDb.Exception);
            }
        }

        //public void UpdateProduct(ProductDto product)
        //{
        //    this._productDb = new ProductDb(this._iDataBase, this._configuration);

        //    var id = this._productDb.UpdateProduct(product);
        //    this.BuildRulesResponse("Product updated","Product could not be updated", id, this._productDb.ExecutionOk, this._productDb.BusinessRuleError, this._productDb.Exception);
            
        //}

        //public void RateProduct(RateProductDto rateProduct)
        //{
        //    this._productDb = new ProductDb(this._iDataBase, this._configuration);

        //    var id = this._productDb.RateProduct(rateProduct);
        //    this.BuildRulesResponse("Product rated","Product could not be rated", id, this._productDb.ExecutionOk, this._productDb.BusinessRuleError, this._productDb.Exception);
        //}
    }
}