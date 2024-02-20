using System;

namespace APT00900COMMON
{
    public class APT00900DTO
    {
        public int INO { get; set; }
        public string CDEPT_CODE { get; set; }
        public string CREF_NO { get; set; }
        public string CREF_DATE { get; set; }
        public string CSUPPLIER_ID { get; set; }
        public string CDOC_NO { get; set; }
        public string CDOC_DATE { get; set; }
        public string CCURRENCY_CODE { get; set; }
        public string CPAY_TERM_CODE { get; set; }
        public string CTAX_ID { get; set; }
        public decimal NLBASE_RATE { get; set; }
        public decimal NLBASE_CURRENCY_RATE { get; set; }
        public decimal NBBASE_RATE { get; set; }
        public decimal NBBASE_CURRENCY_RATE { get; set; }
        public decimal NTAX_BASE_RATE { get; set; }
        public decimal NTAX_CURRENCY_RATE { get; set; }
        public string CTRANS_DESC { get; set; }
        public string CPROD_DEPT_CODE { get; set; }
        public string CPROD_TYPE { get; set; }
        public string CPRODUCT_ID { get; set; }
        public string CALLOC_ID { get; set; }
        public decimal NTRANS_QTY { get; set; }
        public decimal NUNIT_PRICE { get; set; }
        public string CDETAIL_DESC { get; set; }
        public string CREF_NO_NEW { get; set; }
    }

    public class APT00900DisplayDTO
    {
        public int SEQ_NO { get; set; }
        public string Department_Code { get; set; }
        public string Reference_No { get; set; }
        public string Reference_Date { get; set; }
        public string Supplier_Id { get; set; }
        public string Supplier_Reference_No { get; set; }
        public string Supplier_Reference_Date { get; set; }
        public string Currency_Code { get; set; }
        public string Term_Code { get; set; }
        public string Tax_Code { get; set; }
        public decimal Local_Currency_Base_Rate { get; set; }
        public decimal Local_Currency_Rate { get; set; }
        public decimal Base_Currency_Base_Rate { get; set; }
        public decimal Base_Currency_Rate { get; set; }
        public decimal Tax_Base_Rate { get; set; }
        public decimal Tax_Rate { get; set; }
        public string Header_Notes { get; set; }
        public string Product_Department { get; set; }
        public string Product_Type { get; set; }
        public string Product_Id { get; set; }
        public string Allocation_Id { get; set; }
        public decimal Quantity { get; set; }
        public decimal Unit_Price { get; set; }
        public string Detail_Notes { get; set; }
        public string Notes { get; set; }
        public string Valid { get; set; }
    }

    public class APT00900ValidateDTO
    {
        public int IERROR_COUNT { get; set; }
    }
}
