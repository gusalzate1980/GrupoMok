using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessRules.Classes
{
    public class GenericBr<T> where T:class
    {
        public bool BusinessRuleOk { get; set; }
        public bool ServerOk { get; set; }
        public string UserMessage { get; set; }
        public string DeveloperMessage { get; set; }
        public int Id { set;get;}
        public T Data { get; set; }

        protected void BuildRulesResponse(string messageOk, string messageFail, int id,bool executionOk, string businessRuleError,Exception? exceptionError)
        {
            if (id > 0)
            {
                this.UserMessage = messageOk;
                this.BusinessRuleOk = true;
                this.ServerOk = true;
                this.Id = id;
            }
            else
            {
                if (executionOk)
                {
                    this.BusinessRuleOk = false;
                    this.ServerOk = true;
                    this.UserMessage = businessRuleError;
                }
                else
                {
                    this.BusinessRuleOk = true;
                    this.ServerOk = false;
                    this.DeveloperMessage = exceptionError != null ? exceptionError.Message : "";
                    this.UserMessage = messageFail;
                }
            }
        }
    }
}
