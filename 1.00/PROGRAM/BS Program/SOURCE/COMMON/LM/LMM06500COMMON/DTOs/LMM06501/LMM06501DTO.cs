using System;

namespace LMM06500COMMON
{
    public class LMM06501DTO
    {
        // result
        public int NO { get; set; }
        public string StaffId { get; set; }
        public string StaffName { get; set; }
        public string Active { get; set; }
        public string Department { get; set; }
        public string Position { get; set; }
        public string JoinDate { get; set; }
        public string Supervisor { get; set; }
        public string EmailAddress { get; set; }
        public string MobileNo1 { get; set; }
        public string MobileNo2 { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public string InActiveDate { get; set; }
        public string InactiveNote { get; set; }
        public string ErrorMessage { get; set; }
        public bool Var_Exists { get; set; }
        public bool Var_Overwrite { get; set; }

    }
    public class LMM06501ExcelDTO
    {
        // result
        public string StaffId { get; set; }
        public string StaffName { get; set; }
        public int Active { get; set; }
        public string Department { get; set; }
        public string Position { get; set; }
        public string JoinDate { get; set; }
        public string Supervisor { get; set; }
        public string EmailAddress { get; set; }
        public string MobileNo1 { get; set; }
        public string MobileNo2 { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public string InActiveDate { get; set; }
        public string InactiveNote { get; set; }
        public string ErrorMessage { get; set; }
    }

    public class LMM06501RequestDTO
    {
        // result
        public int NO { get; set; }
        public string StaffId { get; set; }
        public string StaffName { get; set; }
        public int Active { get; set; }
        public string Department { get; set; }
        public string Position { get; set; }
        public string JoinDate { get; set; }
        public string Supervisor { get; set; }
        public string EmailAddress { get; set; }
        public string MobileNo1 { get; set; }
        public string MobileNo2 { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public string InActiveDate { get; set; }
        public string InactiveNote { get; set; }

    }

    public class LMM06501ErrorValidateDTO
    {
        // result
        public int NO { get; set; }
        public string StaffId { get; set; }
        public string StaffName { get; set; }
        public int Active { get; set; }
        public string Department { get; set; }
        public string Position { get; set; }
        public string JoinDate { get; set; }
        public DateTime? JoinDateDisplay { get; set; }
        public string Supervisor { get; set; }
        public string EmailAddress { get; set; }
        public string MobileNo1 { get; set; }
        public string MobileNo2 { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public string InActiveDate { get; set; }
        public DateTime? InActiveDateDisplay { get; set; }
        public string InactiveNote { get; set; }
        public string ErrorMessage { get; set; }
        public string Valid { get; set; }
        public bool Var_Exists { get; set; }
        public bool Var_Overwrite { get; set; }
    }

    public class LMM06501ErrorValidateParamDTO
    {
        public string CCOMPANY_ID { get; set; }
        public string CUSER_ID { get; set; }
        public string CKEY_GUID { get; set; }

    }
}
