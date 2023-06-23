using Microsoft.Extensions.Configuration;

namespace GenericDataBase
{
    public class CommonSql
    {
        public bool ExecutionOk { set; get; }
        public Exception? Exception { set; get; }
        public string SqlError { set;get;}
        protected string ConnectionString { set; get; }
     
        protected IConfiguration Configuration { set; get; }

        protected bool BusinessRule { set; get; }
        public string BusinessRuleError { set; get; } 

        public CommonSql(IConfiguration configuration)
        {
            this.ConnectionString = configuration.GetSection("ConnectionString_"+ configuration.GetSection("Scope").Value).Value;
            this.Configuration = configuration;
        }
    }
}