using System;
using System.Collections.Generic;
using System.Text;

namespace LMM03500COMMON.DTOs.LMM03504
{
    public class LMM03504DTO
    {
        public string CTENANT_ID { get; set; } = "";
        public string CTENANT_NAME { get; set; } = "";
        public bool LBILLING_OVERWRITE { get; set; } = false;
        public string CBILLING_TO { get; set; } = "";
        public string CBILLING_ADDRESS { get; set; } = "";
        public string CBILLING_PHONE1 { get; set; } = "";
        public string CBILLING_PHONE2 { get; set; } = "";
        public string CBILLING_EMAIL { get; set; } = "";
        public string CBILLING_ATTENTION_NAME { get; set; } = "";
        public string CBILLING_ATTENTION_POSITION { get; set; } = "";

        public void Clear()
        {
            CTENANT_ID = "";
            CTENANT_NAME = "";
            LBILLING_OVERWRITE = false;
            CBILLING_TO = "";
            CBILLING_ADDRESS = "";
            CBILLING_PHONE1 = "";
            CBILLING_PHONE2 = "";
            CBILLING_EMAIL = "";
            CBILLING_ATTENTION_NAME = "";
            CBILLING_ATTENTION_POSITION = "";
        }
    }
}
