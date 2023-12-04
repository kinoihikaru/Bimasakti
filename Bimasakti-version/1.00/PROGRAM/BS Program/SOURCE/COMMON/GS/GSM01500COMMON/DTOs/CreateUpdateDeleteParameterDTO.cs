using System;
using System.Collections.Generic;
using System.Text;

namespace GSM01500COMMON.DTOs
{
    public class CreateUpdateDeleteParameterDTO
    {
        public GSM01500DTO Data { get; set; }
        public string CCOMPANY_ID { get; set; } = "";
        public string CACTION { get; set; } = "";
        public string CUSER_ID { get; set; } = "";
    }
}
