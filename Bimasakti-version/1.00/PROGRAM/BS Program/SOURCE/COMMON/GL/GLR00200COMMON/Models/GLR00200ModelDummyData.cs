using BaseHeaderReportCOMMON;
using BaseHeaderReportCOMMON.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace GLR00200COMMON.Models
{
    public static class GenerateDataModelGLR00200
    {
        public static GLR00200ResultPrintDTO DefaultData()
        {
            GLR00200ResultPrintDTO loData = new GLR00200ResultPrintDTO();

            var loSummaryData = new GLR00200DTO
            {
                CGLACCOUNT_NO = "1001",
                CGLACCOUNT_NAME = "Account 1001",
                NTOTAL_DEBIT = "5000.00",
                NTOTAL_CREDIT = "3500.00",
                CFROM_PERIOD = "202301",
                CTO_PERIOD = "202312",
                CCURRENCY = "USD",
                CFROM_ACCOUNT_NO = "1000",
                CTO_ACCOUNT_NO = "2000",
                CFROM_CENTER_CODE = "C001",
                CTO_CENTER_CODE = "C002",
                CPRINT_METHOD_NAME = "Method 1",
                CINCLUDE_AUDIT_JOURNAL = "Yes"
            };

            var loDetailData = new List<GLR00201DTO>();

            int lnDetail;
            for (int i = 1; i <= 5; i++)
            {
                lnDetail = (i % 3) + 1;
                for (int s = 1; s <= lnDetail; s++)
                {
                    loDetailData.Add(new GLR00201DTO
                    {
                        CCOMPANY_ID = "COMP" + s,
                        CGLACCOUNT_NO = "100" + s,
                        CGLACCOUNT_NAME = "Account 100" + s,
                        CGLACCOUNT_TYPE = "Type " + s,
                        CACCOUNT = "Account " + s,
                        CFROM_PERIOD = "202301",
                        CTO_PERIOD = "202312",
                        CDBCR = "DB",
                        CBSIS = "SIS",
                        NBEGIN_BALANCE = 1000 * s,
                        NEND_BALANCE = 2000 * s,
                        CREC_ID = "REC" + i,
                        CREF_NO = "JV - Tes aja gitu " + i,
                        CREF_DATE = DateTime.Now.AddMonths(-i).ToString("yyyyMMdd"),
                        CREF_PRD = DateTime.Now.AddMonths(-i).ToString("yyyy-MM"),
                        CCENTER_CODE = "C00" + i,
                        NDEBIT_AMOUNT = 500 * i,
                        NLDEBIT_AMOUNT = 300 * i,
                        NBDEBIT_AMOUNT = 200 * i,
                        NCREDIT_AMOUNT = 400 * i,
                        NLCREDIT_AMOUNT = 250 * i,
                        NBCREDIT_AMOUNT = 150 * i,
                        CDETAIL_DESC = "Description " + i,
                        CDOCUMENT_NO = "DOC" + i,
                        CDOCUMENT_DATE = DateTime.Now.AddMonths(-i).ToString("yyyyMMdd"),
                        CSOURCE_MODULE = "Module " + i,
                        CCUSTOMER_SUPPLIER = "Supplier " + i,
                        CCURRENCY = "USD",
                        CFROM_ACCOUNT_NO = "1000",
                        CTO_ACCOUNT_NO = "2000",
                        CFROM_CENTER_CODE = "C001",
                        CTO_CENTER_CODE = "C002",
                        CPRINT_METHOD_NAME = "Method 1",
                        CINCLUDE_AUDIT_JOURNAL = "Yes"
                    });
                }
            }

            var loTempResultDetail = loDetailData.GroupBy(g =>
                new
                { 
                    g.CACCOUNT, g.CDBCR, g.CBSIS,  g.NBEGIN_BALANCE,
                    g.CGLACCOUNT_NO, g.CGLACCOUNT_NAME
                },
                g => new GLR00204DTO()
                {
                    CGLACCOUNT_NO = g.CGLACCOUNT_NO,
                    
                    CREF_DATE = DateTime.ParseExact(g.CREF_DATE, "yyyyMMdd", CultureInfo.InvariantCulture).ToString("dd-MMM-yyyy"),
                    CSOURCE_MODULE = g.CSOURCE_MODULE,
                    CREF_NO = g.CREF_NO,
                    CDETAIL_DESC = g.CDETAIL_DESC,
                    CCENTER_CODE = g.CCENTER_CODE,
                    CDOCUMENT_NO = g.CDOCUMENT_NO,
                    CCUSTOMER_SUPPLIER = g.CCUSTOMER_SUPPLIER,
                    NLDEBIT_AMOUNT = g.NLDEBIT_AMOUNT,
                    NCREDIT_AMOUNT = g.NCREDIT_AMOUNT,
                    NEND_BALANCE = g.NEND_BALANCE,
                    CDOCUMENT_DATE = !string.IsNullOrWhiteSpace(g.CDOCUMENT_DATE)
                                    ? DateTime.ParseExact(g.CDOCUMENT_DATE, "yyyyMMdd", CultureInfo.InvariantCulture).ToString("dd-MMM-yyyy")
                                    : "",

                    CREC_ID = g.CREC_ID,
                    CREF_PRD = g.CREF_PRD,
                    NDEBIT_AMOUNT = g.NDEBIT_AMOUNT,
                    NBDEBIT_AMOUNT = g.NBDEBIT_AMOUNT,
                    NLCREDIT_AMOUNT = g.NLCREDIT_AMOUNT,
                    NBCREDIT_AMOUNT = g.NBCREDIT_AMOUNT,
                    CCURRENCY = g.CCURRENCY,
                    CFROM_ACCOUNT_NO = g.CFROM_ACCOUNT_NO,
                    CTO_ACCOUNT_NO = g.CTO_ACCOUNT_NO,
                    CFROM_CENTER_CODE = g.CFROM_CENTER_CODE,
                    CTO_CENTER_CODE = g.CTO_CENTER_CODE,
                    CPRINT_METHOD_NAME = g.CPRINT_METHOD_NAME,
                    CINCLUDE_AUDIT_JOURNAL = g.CINCLUDE_AUDIT_JOURNAL
                }).Select(s => new GLR00202DTO
                {
                    CACCOUNT = s.Key.CACCOUNT,
                    CDBCR = s.Key.CDBCR,
                    CBSIS = s.Key.CBSIS,
                    CGLACCOUNT_NO = s.Key.CGLACCOUNT_NO,
                    CGLACCOUNT_NAME = s.Key.CGLACCOUNT_NAME,
                    NBEGIN_BALANCE = s.Key.NBEGIN_BALANCE,
                    NDEBIT_AMOUNT_SUM = s.Sum(item => item.NDEBIT_AMOUNT),
                    NCREDIT_AMOUNT_SUM = s.Sum(item => item.NCREDIT_AMOUNT),
                    PeriodDetailData = s.ToList()
                }).ToList();

            loData.SummaryData = loSummaryData;
            loData.DetailData = loTempResultDetail;

            return loData;
        }

        public static GLR00200ResultWithBaseHeaderPrintDTO DefaultDataWithHeader()
        {
            var loParam = new BaseHeaderDTO()
            {
                CCOMPANY_NAME = "PT Realta Chackradarma",
                CPRINT_CODE = "010",
                CPRINT_NAME = "GL ACCOUNT LEDGER",
                CUSER_ID = "FMC",
            };

            GLR00200ResultWithBaseHeaderPrintDTO loRtn = new GLR00200ResultWithBaseHeaderPrintDTO();

            loRtn.BaseHeaderData = loParam;
            loRtn.GLR = GenerateDataModelGLR00200.DefaultData();

            return loRtn;
        }
    }

}
