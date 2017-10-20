using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebCRU.DTO
{
    public class DResponse
    {
        public IEnumerable<object> DataList { get; set; }

        public int RowsCount { get; set; }
    }
}