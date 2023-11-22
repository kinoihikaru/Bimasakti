using System;

namespace GSM02200COMMON
{
    public class GSM02201DTO
    {
        public int SeqNo { get; set; }
        public string GeographyCode { get; set; }
        public string GeographyName { get; set; }
        public string ParentCode { get; set; }
        public string CNAME { get; set; }
        public int Active { get; set; }
        public string Notes { get; set; }
        public string Valid { get; set; }

    }

    public class GSM02201ReqDTO
    {
        public int SeqNo { get; set; }
        public string GeographyCode { get; set; }
        public string GeographyName { get; set; }
        public string ParentCode { get; set; }
        public int Active { get; set; }

    }
}
