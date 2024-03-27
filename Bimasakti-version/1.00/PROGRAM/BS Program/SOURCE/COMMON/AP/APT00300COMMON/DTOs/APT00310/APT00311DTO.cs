using System;
using System.Globalization;

namespace APT00300COMMON
{
    public class APT00311DTO
    {
        //Param
        public string CCOMPANY_ID { get; set; } 
        public string CPROPERTY_ID { get; set; } 
        public string CDEPT_CODE { get; set; }
        public string CTRANS_CODE { get; set; }
        public string CREF_NO { get; set; }
        public string CREC_ID { get; set; }
        public string CPARENT_ID { get; set; }
        public string CACTION { get; set; }

        //Result
        public string CSEQ_NO { get; set; }
        public string CPROD_DEPT_CODE { get; set; }
        public string CPROD_DEPT_NAME { get; set; }
        public string CPROD_TYPE { get; set; }
        public string CPROD_TYPE_NAME { get; set; }
        public string CPRODUCT_ID { get; set; }
        public string CPRODUCT_NAME { get; set; }
        public string CSUP_PRODUCT_ID { get; set; }
        public string CSUP_PRODUCT_NAME { get; set; }
        public string CALLOC_ID { get; set; }
        public string CALLOC_NAME { get; set; }
        public string CDETAIL_DESC { get; set; } = "";
        public string CFROM_DEPT_CODE { get; set; } = "";
        public string CFROM_TRANS_CODE { get; set; }
        public string CFROM_REF_NO { get; set; }
        public string CFROM_SEQ_NO { get; set; }
        public int IBILL_UNIT { get; set; }
        public string CBILL_UNIT { get; set; }
        public decimal NSUPP_CONV_FACTOR { get; set; }
        public decimal NTRANS_QTY { get; set; }
        public decimal NAPPV_QTY { get; set; }
        public decimal NUNIT_PRICE { get; set; }
        public decimal NAMOUNT { get; set; }
        public string CDISC_TYPE { get; set; }
        public decimal NDISC_PCT { get; set; }
        public decimal NDISC_AMOUNT { get; set; }
        public decimal NTAXABLE_AMOUNT { get; set; }
        public string CTAX_ID { get; set; }
        public string CTAX_NAME { get; set; } = "";
        public decimal NTAX_PCT { get; set; }
        public decimal NTAX_AMOUNT { get; set; }
        public string COTHER_TAX_ID { get; set; }
        public string COTHER_TAX_NAME { get; set; }
        public decimal NOTHER_TAX_PCT { get; set; }
        public decimal NOTHER_TAX_AMOUNT { get; set; }
        public decimal NTOTAL_AMOUNT { get; set; }
        public decimal NDIST_DISCOUNT { get; set; }
        public decimal NDIST_ADD_ON { get; set; }
        public decimal NLINE_AMOUNT { get; set; }
        public decimal NTRANS_AMOUNT { get; set; }
        public decimal NLTRANS_AMOUNT { get; set; }
        public decimal NBTRANS_AMOUNT { get; set; }
        public string CUNIT1 { get; set; }
        public string CUNIT2 { get; set; }
        public string CUNIT3 { get; set; }

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
