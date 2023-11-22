using System;
using System.Collections.Generic;
using System.Text;

namespace APM00200COMMON.DTOs.APM00200
{
    public class SaveExpenditureToExcelDTO
    {
        public int No { get; set; } = 0;
        public string Expenditure_Id { get; set; } = "";
        public string Expenditure_Name { get; set; } = "";
        public string Expenditure_Description { get; set; } = "";
        public string Journal_Group_Id { get; set; } = "";
        public string Category_Id { get; set; } = "";
        public string Unit { get; set; } = "";
        public string Taxable { get; set; } = "";
        public string Tax_ID { get; set; } = "";
        public string Valid { get; set; } = "";
        public string Notes { get; set; } = "";
    }
}
