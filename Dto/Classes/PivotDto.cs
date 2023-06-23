using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.Classes
{
    public class PivotDto
    {
        public List<ColumnDto> Columns { get; set; }
    }

    public class ColumnDto
    {
        public string Name { get; set; }
        public List<string> Values { get; set; }
    }
}
