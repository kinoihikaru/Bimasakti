using BaseHeaderReportCOMMON;
using BaseHeaderReportCOMMON.Models;
using GSM03000Common.DTOs;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace GSM03000Common.Models
{
    public static class GenerateDataModelGSM03000
    {
        public static GSM03000ResultPrintDTO DefaultData()
        {
            GSM03000ResultPrintDTO loData = new GSM03000ResultPrintDTO();

            List<GSM03000ResultDTO> loTempResult = new List<GSM03000ResultDTO>();
            int lnDetail;
            for (int i = 1; i <= 2; i++)
            {
                loTempResult.Add(new GSM03000ResultDTO
                {
                    CCOMPANY_ID = string.Format("Company_{0}", i),
                    CPROPERTY_ID = string.Format("Property_{0}", i),
                    CPROPERTY_NAME = string.Format("Property Name {0}", i),
                    CCHARGES_ID = string.Format("Charge_{0}", i),
                    CCHARGES_NAME = string.Format("Charge Name {0}", i),
                    LACTIVE = (i % 2 == 0),
                    CSTATUS_NAME = (i % 2 == 0) ? "Active Status" : "Inactive Status",
                    CGLACCOUNT_NO = string.Format("GL_Account_{0}", i),
                    CGL_ACCOUNT_NAME = string.Format("GL Account Name {0}", i),
                    CDESCRIPTION = string.Format("Description {0}", i)
                });
            }

            //set header data
            var loHeader = loTempResult.FirstOrDefault();

            loData.Title = "Other Charges";
            loData.Header = string.Format("{0} - {1}", loHeader.CPROPERTY_ID, loHeader.CPROPERTY_NAME);
            loData.Data = loTempResult;

            return loData;
        }

        public static GSM03000ResultWithBaseHeaderPrintDTO DefaultDataWithHeader()
        {
            var loParam = new BaseHeaderDTO()
            {
                CCOMPANY_NAME = "PT Realta Chackradarma",
                CPRINT_CODE = "010",
                CPRINT_NAME = "GL ACCOUNT LEDGER",
                CUSER_ID = "FMC",
            };

            GSM03000ResultWithBaseHeaderPrintDTO loRtn = new GSM03000ResultWithBaseHeaderPrintDTO();

            loRtn.BaseHeaderData = loParam;
            loRtn.OtherCharges = DefaultData();

            return loRtn;
        }
    }

}
