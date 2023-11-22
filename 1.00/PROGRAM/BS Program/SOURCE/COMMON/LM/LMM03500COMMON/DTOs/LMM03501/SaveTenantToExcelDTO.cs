using System;
using System.Collections.Generic;
using System.Text;

namespace LMM03500COMMON.DTOs.LMM03501
{
    public class SaveTenantToExcelDTO
    {
        public int No { get; set; } = 0;
        public string TenantId { get; set; } = "";
        public string TenantName { get; set; } = "";
        public string Address { get; set; } = "";
        public string City { get; set; } = "";
        public string Province { get; set; } = "";
        public string Country { get; set; } = "";
        public string ZipCode { get; set; } = "";
        public string Email { get; set; } = "";
        public string PhoneNo1 { get; set; } = "";
        public string PhoneNo2 { get; set; } = "";
        public string TenantGroup { get; set; } = "";
        public string TenantCategory { get; set; } = "";
        public string TenantType { get; set; } = "";
        public string JournalGroup { get; set; } = "";
        public string PaymentTerm { get; set; } = "";
        public string Currency { get; set; } = "";
        public string Salesman { get; set; } = "";
        public string LineOfBusiness { get; set; } = "";
        public string FamilyCard { get; set; } = "";
        public string Valid { get; set; } = "";
        public string Notes { get; set; } = "";
    }
}
