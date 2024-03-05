using System;

namespace APT00600COMMON
{
    public class APT00610DTO
    {
        public int INO { get; set; }
        public string CALLOC_ID { get; set; }
        public string CALLOC_NO { get; set; }
        public string CALLOC_DATE { get; set; }
        public string CTRANS_CODE { get; set; }
        public string CTRANS_NAME { get; set; }
        public string CTRANS_REF_NO { get; set; }
        public string CTRANS_STATUS { get; set; }
        public string CTRANS_STATUS_NAME { get; set; }
        public decimal NALLOC_AMOUNT { get; set; }
        public string CTO_CURRENCY_CODE { get; set; }
        public string CCREATE_BY { get; set; }
        public DateTime DCREATE_DATE { get; set; }
        public string CUPDATE_BY { get; set; }
        public DateTime DUPDATE_DATE { get; set; }
        public string CDEPT_CODE { get; set; }
        public string CDEPT_NAME { get; set; }
        public string CCHARGES_ID { get; set; }
        public string CCHARGES_NAME { get; set; }
        public string CCHARGES_DESC { get; set; }

        //Detail
        public string CREF_NO { get; set; }
        public string CPROPERTY_ID { get; set; }
        public string CACTION { get; set; }
        public string CREC_ID { get; set; }
        public string CFR_REC_ID { get; set; } // REC ID HEADER
        public string CFR_REF_NO { get; set; } // REF NO HEADER
        public string CFR_REF_DATE { get; set; } // REF Date HEADER
        public string CFR_SUPPLIER_ID { get; set; } // Supplier_ID HEADER
        public string CFR_SUPPLIER_SEQ_NO { get; set; } // Supplier SEQ No HEADER
        public string CFR_DEPT_CODE { get; set; } // DEPT CODE HEADER
        public string CTO_REC_ID { get; set; }
        public string CTO_DEPT_CODE { get; set; }
        public string CTO_DEPT_NAME { get; set; }
        public string CTO_TRANS_CODE { get; set; }
        public string CTO_TRANS_NAME { get; set; }
        public string CTO_REF_NO { get; set; }
        public string CTO_REF_DATE { get; set; }
        public decimal NTO_AP_AMOUNT { get; set; }
        public decimal NLTO_AP_AMOUNT { get; set; }
        public decimal NBTO_AP_AMOUNT { get; set; }
        public decimal NAP_REMAINING { get; set; }
        public decimal NLAP_REMAINING { get; set; }
        public decimal NBTARGET_REMAINING { get; set; }
        public decimal NLBASE_RATE { get; set; }
        public decimal NLCURRENCY_RATE { get; set; }
        public decimal NTAX_BASE_RATE { get; set; }
        public decimal NTAX_CURRENCY_RATE { get; set; }
        public decimal NLTO_TAX_AMOUNT { get; set; }
        public decimal NBTO_TAX_AMOUNT { get; set; }
        public decimal NBBASE_RATE { get; set; }
        public decimal NBCURRENCY_RATE { get; set; }
        public decimal NTAX_REMAINING { get; set; }
        public decimal NLTAX_REMAINING { get; set; }
        public decimal NBTAX_REMAINING { get; set; }
        public decimal NTOTAL_REMAINING { get; set; }
        public decimal NLTOTAL_REMAINING { get; set; }
        public decimal NBTOTAL_REMAINING { get; set; }
        public decimal NLALLOC_AMOUNT { get; set; }
        public decimal NBALLOC_AMOUNT { get; set; }
        public decimal NTO_TAX_AMOUNT { get; set; }
        public decimal NTO_DISC_AMOUNT { get; set; }
        public decimal NTO_LBASE_RATE { get; set; }
        public decimal NTO_LCURRENCY_RATE { get; set; }
        public decimal NTO_BBASE_RATE { get; set; }
        public decimal NTO_BCURRENCY_RATE { get; set; }
        public decimal NTO_TAX_BASE_RATE { get; set; }
        public decimal NTO_TAX_CURRENCY_RATE { get; set; }
        public string CCHARGES_DEPT_CODE { get; set; }
        public string CCHARGES_DEPT_NAME { get; set; }
    }
}
