using R_CommonFrontBackAPI.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMM01000SERVICE
{
    public class LMM01000PrintLogKeyDTO<T>
    {
        public T poParam { get; set; }
        public R_NetCoreLogKeyDTO poLogKey { get; set; }
    }
}
