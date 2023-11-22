using System;
using System.Collections.Generic;
using System.Text;

namespace GSM09300COMMON.DTOs.GSM09310
{
    public class SupplierListForMoveProcessDTO
    {
        public bool LSELECTED { get; set; } = false;
        public string CSUPPLIER_ID { get; set; }
        public string CSUPPLIER_NAME { get; set; }
    }
}
