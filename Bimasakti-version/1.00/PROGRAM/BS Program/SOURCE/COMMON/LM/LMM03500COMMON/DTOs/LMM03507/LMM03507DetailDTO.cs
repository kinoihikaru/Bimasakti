using System;
using System.Collections.Generic;
using System.Text;

namespace LMM03500COMMON.DTOs.LMM03507
{
    public class LMM03507DetailDTO
    {
        public string CTENANT_ID { get; set; } = "";
        public string CTENANT_NAME { get; set; } = "";
        public string CMEMBER_SEQ_NO { get; set; } = "";
        public string CMEMBER_NAME { get; set; } = "";
        public string CGENDER { get; set; } = "";
        public string CDATE_BIRTH { get; set; } = "";
        public int IAGE { get; set; } = 0;
        public string CRELATIONSHIP { get; set; } = "";
        public string CID_TYPE { get; set; } = "";
        public string CID_NO { get; set; } = "";
        public string CNATIONALITY { get; set; } = "";
        public string CEMAIL { get; set; } = "";
        public string CMOBILE_PHONE1 { get; set; } = "";
        public string CMOBILE_PHONE2 { get; set; } = "";
        public string CUPDATE_BY { get; set; } = "";
        public DateTime DUPDATE_DATE { get; set; }
        public string CCREATE_BY { get; set; } = "";
        public DateTime DCREATE_DATE { get; set; }

        public void Clear()
        {
            CTENANT_ID = "";
            CTENANT_NAME = "";
            CMEMBER_SEQ_NO = "";
            CMEMBER_NAME = "";
            CGENDER = "";
            CDATE_BIRTH = null;
            IAGE = 0;
            CRELATIONSHIP = "";
            CID_TYPE = "";
            CID_NO = "";
            CNATIONALITY = "";
            CEMAIL = "";
            CMOBILE_PHONE1 = "";
            CMOBILE_PHONE2 = "";
            CUPDATE_BY = "";
            DUPDATE_DATE = default(DateTime);
            CCREATE_BY = "";
            DCREATE_DATE = default(DateTime);
        }
    }
}
