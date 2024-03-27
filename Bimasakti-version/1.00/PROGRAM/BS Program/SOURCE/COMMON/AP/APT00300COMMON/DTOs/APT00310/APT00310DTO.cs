using System;
using System.Globalization;

namespace APT00300COMMON
{
    public class APT00310DTO
    {
        //Param
        public string CCOMPANY_ID { get; set; } 
        public string CPROPERTY_ID { get; set; } 
        public string CDEPT_CODE { get; set; }
        public string CTRANS_CODE { get; set; }
        public string CREF_NO { get; set; }
        public string CREC_ID { get; set; }
        public string CACTION { get; set; }

        //Result
        public string CPROPERTY_NAME { get; set; }
        public string CDEPT_NAME { get; set; }
        public string CREF_DATE { get; set; }
        public string CREF_PRD { get; set; }
        public string CSUPPLIER_ID { get; set; }
        public string CSUPPLIER_SEQ_NO { get; set; }
        public string CSUPPLIER_NAME { get; set; }
        public string CDOC_NO { get; set; }
        public string CDOC_DATE { get; set; }
        public string CPAY_TERM_CODE { get; set; } = "";
        public string CDUE_DATE { get; set; } = "";
        public string CTRANS_DESC { get; set; }
        public string CCURRENCY_CODE { get; set; }
        public string CCURRENCY_NAME { get; set; }
        public decimal NLBASE_RATE { get; set; }
        public decimal NLCURRENCY_RATE { get; set; }
        public decimal NBBASE_RATE { get; set; }
        public decimal NBCURRENCY_RATE { get; set; }
        public decimal NTAX_BASE_RATE { get; set; }
        public decimal NTAX_CURRENCY_RATE { get; set; }
        public decimal NAMOUNT { get; set; }
        public decimal NLAMOUNT { get; set; }
        public decimal NBAMOUNT { get; set; }
        public decimal NDISCOUNT { get; set; }
        public decimal NLDISCOUNT { get; set; }
        public decimal NBDISCOUNT { get; set; }
        public bool LSUMMARY_DISCOUNT { get; set; }
        public bool LONETIME { get; set; }
        public string CSUMMARY_DISC_TYPE { get; set; }
        public decimal NSUMMARY_DISC_PCT { get; set; }
        public decimal NSUMMARY_DISCOUNT { get; set; }
        public decimal NLSUMMARY_DISCOUNT { get; set; }
        public decimal NBSUMMARY_DISCOUNT { get; set; }
        public decimal NADD_ON { get; set; }
        public decimal NLADD_ON { get; set; }
        public decimal NBADD_ON { get; set; }
        public decimal NTAXABLE_AMOUNT { get; set; }
        public decimal NLTAXABLE_AMOUNT { get; set; }
        public decimal NBTAXABLE_AMOUNT { get; set; }
        public decimal NTAX { get; set; }
        public decimal NLTAX { get; set; }
        public decimal NBTAX { get; set; }
        public decimal NOTHER_TAX { get; set; }
        public decimal NLOTHER_TAX { get; set; }
        public decimal NBOTHER_TAX { get; set; }
        public decimal NADDITION { get; set; }
        public decimal NLADDITION { get; set; }
        public decimal NBADDITION { get; set; }
        public decimal NDEDUCTION { get; set; }
        public decimal NLDEDUCTION { get; set; }
        public decimal NBDEDUCTION { get; set; }
        public decimal NTRANS_AMOUNT { get; set; }
        public decimal NLTRANS_AMOUNT { get; set; }
        public decimal NBTRANS_AMOUNT { get; set; }
        public string CSOURCE_MODULE { get; set; }
        public bool LTAXABLE { get; set; }
        public string CTAX_ID { get; set; } = "";
        public string CTAX_NAME { get; set; } = "";
        public decimal NTAX_PCT { get; set; }
        public string CTRANS_STATUS { get; set; }
        public string CTRANS_STATUS_NAME { get; set; }
        public decimal NTOTAL_AMOUNT { get; set; }
        public bool LGLLINK { get; set; }
        public string CGL_REF_NO { get; set; }

        public string CCREATE_BY { get; set; }
        public DateTime? DCREATE_DATE { get; set; }
        public string CUPDATE_BY { get; set; }
        public DateTime? DUPDATE_DATE { get; set; }

        public decimal NAP_REMAINING { get; set; }
        public decimal NLAP_REMAINING { get; set; }
        public decimal NBAP_REMAINING { get; set; }
        public decimal NTAX_REMAINING { get; set; }
        public decimal NLTAX_REMAINING { get; set; }
        public decimal NBTAX_REMAINING { get; set; }
        public decimal NTOTAL_REMAINING { get; set; }
        public decimal NLTOTAL_REMAINING { get; set; }
        public decimal NBTOTAL_REMAINING { get; set; }
    }
}
