using System;
using System.Collections.Generic;
using System.Text;

namespace LMM04000COMMON.DTOs.LMM04010
{
    public class UploadContractorExcelDTO
    {
        public string ContractorId { get; set; } = "";
        public string ContractorName { get; set; } = "";
        public string Address { get; set; } = "";
        public string City { get; set; } = "";
        public string Province { get; set; } = "";
        public string Country { get; set; } = "";
        public string ZipCode { get; set; } = "";
        public string Email { get; set; } = "";
        public string PhoneNo1 { get; set; } = "";
        public string PhoneNo2 { get; set; } = "";
        public string ContractorGroup { get; set; } = "";
        public string ContractorCategory { get; set; } = "";
        public string JournalGroup { get; set; } = "";
        public string PaymentTerm { get; set; } = "";
        public string Currency { get; set; } = "";
        public string LineOfBusiness { get; set; } = "";
    }
}
