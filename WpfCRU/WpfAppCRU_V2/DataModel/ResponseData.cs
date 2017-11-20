using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAppCRU_V2.DataModel
{
    class ResponseData
    {
        public IEnumerable<object> DataList { get; set; }
        public int RowsCount { get; set; }
    }
}
