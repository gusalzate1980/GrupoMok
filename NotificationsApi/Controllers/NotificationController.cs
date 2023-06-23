using ApiNotification.Controllers;
using Dto;
using Dto.Classes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestClient;
using Util;

namespace NotificationsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : MokController
    {
        public NotificationController(IConfiguration configuration) : base(configuration)
        {
            this._configuration = configuration;
        }

        [HttpPost("SendNotification")]
        public ActionResult<WebServiceResponse<WildCard>> SendPurchaseNotification(WebServiceRequest<Message> request)
        {
            if (this.ValidarPeticion(JsonConvert.SerializeObject(request.Data),
                                    request.Token,
                                    request.Timestamp,
                                    Int32.Parse(_configuration.GetSection("SegundosValidesPeticion").Value)))
            {
                try
                {
                    Email email = new Email();
                    email.Subject = request.Data.Subject;
                    email.Body = request.Data.Body;
                    email.ToList  =  request.Data.ToList;
                    email.NameToShow  = _configuration.GetSection("EmailNameToShow").Value;
                    email.From = _configuration.GetSection("EmailLogin").Value;
                    email.Smtp= _configuration.GetSection("EmailSmtp").Value;
                    email.Password = _configuration.GetSection("EmailPassword").Value;
                    email.Port = Int32.Parse(_configuration.GetSection("EmailPort").Value);

                    EmailSender sender  = new EmailSender(email);

                    if (sender.Send())
                    {
                        return new WebServiceResponse<WildCard>()
                        {
                            BusinessRulesOk = true,
                            Data = new WildCard()
                            {
                                Value = sender.Result
                            },
                            ServerOk= true
                        };
                    }
                    else
                    {
                        return new WebServiceResponse<WildCard>()
                        {
                            BusinessRulesOk = false,
                            Data = new WildCard()
                            {
                                Value = sender.Result
                            },
                            ServerOk = false
                        };
                    }
                }
                catch (Exception e)
                {
                    return new WebServiceResponse<WildCard>()
                    {
                        Data = new WildCard()
                        {
                            Value = "Email could not be sent"
                        },
                        BusinessRulesOk = false,
                        ServerOk = false,
                        UserMessage = "Email could not be sent",
                        DeveloperMessage = new string[]
                       {
                            new string(e.Message)
                       }
                    };
                }

            }
            else
            {
                return NotFound();
            }
        }
    }
}
