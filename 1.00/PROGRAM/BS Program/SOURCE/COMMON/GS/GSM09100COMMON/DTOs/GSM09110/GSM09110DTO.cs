using System;

namespace GSM09100COMMON
{
    public class GSM09110DTO
    {
        // Param
        public string CCOMPANY_ID { get; set; }
        public string CPROPERTY_ID { get; set; }
        public string CCATEGORY_ID { get; set; }
        public string CCATEGORY_NAME { get; set; }
        public string CLANGUAGE_ID { get; set; }

        //Result
        public bool LSELECTED { get; set; }
        public string CPRODUCT_ID { get; set; }
        public string CPRODUCT_NAME { get; set; }
        public string CALIAS_ID { get; set; }
        public string CALIAS_NAME { get; set; }
    }
}
