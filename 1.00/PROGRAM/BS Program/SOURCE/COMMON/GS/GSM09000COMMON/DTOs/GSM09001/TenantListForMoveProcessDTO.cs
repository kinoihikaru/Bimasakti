using System;
using System.Collections.Generic;
using System.Text;

namespace GSM09000COMMON.DTOs.GSM09001
{
    public class TenantListForMoveProcessDTO
    {
        public bool LSELECTED { get; set; } = false;
        public string CTENANT_ID { get; set; }
        public string CTENANT_NAME { get; set; }
    }
}
