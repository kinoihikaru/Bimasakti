using System;

namespace APT00600COMMON
{
    public class APT00600DTO
    {
        public string CACTION { get; set; }
        public string CREC_ID { get; set; }
        public string CREF_NO { get; set; }
        public string CREF_DATE { get; set; }
        public DateTime? DREF_DATE { get; set; }
        public string CDUE_DATE { get; set; }
        public string CSUPPLIER_ID { get; set; }
        public string CSUPPLIER_ID_SEQ_NO { get; set; }
        public string CSUPPLIER_SEQ_NO { get; set; }
        public string CSUPPLIER_NAME { get; set; }
        public string CSUPPLIER_NAME_ID { get; set; }
        public string CDOC_NO { get; set; }
        public string CDOC_DATE { get; set; }
        public string CCURRENCY_CODE { get; set; }
        public string CTRANS_DESC { get; set; }
        public string CTRANS_STATUS { get; set; }
        public decimal NTRANS_AMOUNT { get; set; }
        public decimal NADJ_AMOUNT { get; set; }
        public string CTRANS_STATUS_NAME { get; set; }
        public string LACTIVE { get; set; }
        public string CCREATE_BY { get; set; }
        public DateTime? DCREATE_DATE { get; set; }
        public string CUPDATE_BY { get; set; }
        public DateTime? DUPDATE_DATE { get; set; }

        //Detail
        public string CCOMPANY_ID { get; set; }
        public string CPROPERTY_ID { get; set; }
        public string CPROPERTY_NAME { get; set; }
        public string CDEPT_CODE { get; set; }
        public string CDEPT_NAME { get; set; }
        public string CTRANS_CODE { get; set; }
        public string CTRANSACTION_NAME { get; set; }
        public string CREF_PRD { get; set; }
        public bool LONETIME { get; set; }
        public decimal NLBASE_RATE { get; set; }
        public decimal NLCURRENCY_RATE { get; set; }
        public decimal NBBASE_RATE { get; set; }
        public decimal NBCURRENCY_RATE { get; set; }
        public decimal NLTRANS_AMOUNT { get; set; }
        public decimal NBTRANS_AMOUNT { get; set; }
        public string CSOURCE_MODULE { get; set; }
        public decimal NTAX_PCT { get; set; }
        public bool LGLLINK { get; set; }
        public string CGL_REF_NO { get; set; }
    }

    public class APT00600ParamDTO
    {
        public string CPROPERTY_ID { get; set; }
        public string CDEPT_CODE { get; set; }
        public string CSUPPLIER_ID { get; set; }
        public string CFROM_PERIOD { get; set; }
        public string CTO_PERIOD { get; set; }
    }
}
