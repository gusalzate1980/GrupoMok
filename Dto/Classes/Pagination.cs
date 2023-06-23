using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.Classes
{
    public class PaginationDto<T> where T:class
    {
        public T Filters { get; set; }
        public int NumRows { get; set; }
        public int Page { get; set; }
        public string LogicalOperator { get; set; }
    }

    public class PaginatedQuery<T> where T:class
    {
        public List<T> Result { get; set; }
        public int TotalRecords { get; set; }
        public int TotalPages { get;set; }
    }
}
