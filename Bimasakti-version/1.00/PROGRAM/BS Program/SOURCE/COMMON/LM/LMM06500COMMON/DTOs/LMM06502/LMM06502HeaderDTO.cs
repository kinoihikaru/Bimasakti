namespace LMM06500COMMON
{
    public class LMM06502HeaderDTO
    {
        // param
        public string CCOMPANY_ID { get; set; }
        public string CPROPERTY_ID { get; set; }
        public string CUSER_ID { get; set; }

        // result
        public string COLD_SUPERVISOR_ID { get; set; } = "";
        public string COLD_SUPERVISOR_NAME { get; set; } = "";
        public string COLD_DEPT_CODE { get; set; } = "";
        public string COLD_DEPT_NAME { get; set; } = "";
        public string CNEW_SUPERVISOR_ID { get; set; } = "";
        public string CNEW_SUPERVISOR_NAME { get; set; } = "";
        public string CNEW_DEPT_CODE { get; set; } = "";
        public string CNEW_DEPT_NAME { get; set; } = "";
    }


}
