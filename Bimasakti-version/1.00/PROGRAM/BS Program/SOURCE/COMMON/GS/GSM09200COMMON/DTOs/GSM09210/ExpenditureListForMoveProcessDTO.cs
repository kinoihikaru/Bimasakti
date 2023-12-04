using System;
using System.Collections.Generic;
using System.Text;

namespace GSM09200COMMON.DTOs.GSM09210
{
    public class ExpenditureListForMoveProcessDTO
    {
        public bool LSELECTED { get; set; } = false;
        public string CEXPENDITURE_ID { get; set; }
        public string CEXPENDITURE_NAME { get; set; }
    }
}
