namespace LMM01000COMMON
{
    public class LMM01030DTO
    {
        // param
        public string CCOMPANY_ID { get; set; }
        public string CPROPERTY_ID { get; set; }
        public string CCHARGES_TYPE { get; set; }
        public string CCHARGES_ID { get; set; }
        public string CUSER_ID { get; set; }
        public string CACTION { get; set; }

        // result
        public string CCHARGES_NAME { get; set; }
        public string CCURENCY_CODE { get; set; }
        public decimal NBUY_STANDING_CHARGE { get; set; }
        public decimal NSTANDING_CHARGE { get; set; }
        public decimal NBUY_USAGE_CHARGE_RATE { get; set; }
        public decimal NUSAGE_CHARGE_RATE { get; set; }
        public decimal NMAINTENANCE_FEE { get; set; }
        public string CADMIN_FEE { get; set; }
        public string CADMIN_FEE_DESCR { get; set; }
        public decimal NADMIN_FEE_PCT { get; set; }
        public decimal NADMIN_FEE_AMT { get; set; }
        public bool LADMIN_FEE_TAX { get; set; }
        public bool LADMIN_FEE_SC { get; set; }
        public bool LADMIN_FEE_USAGE { get; set; }
        public bool LADMIN_FEE_MAINTENANCE { get; set; }
        public byte[] CLOGO { get; set; }
    }
}
