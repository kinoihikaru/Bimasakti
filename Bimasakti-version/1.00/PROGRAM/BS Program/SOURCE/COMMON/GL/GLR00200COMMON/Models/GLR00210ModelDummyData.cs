using BaseHeaderReportCOMMON;
using BaseHeaderReportCOMMON.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Xml.Linq;

namespace GLR00200COMMON.Models
{
    public static class GenerateDataModelGLR00210
    {
        public static GLR00210ResultPrintDTO DefaultData()
        {
            GLR00210ResultPrintDTO loData = new GLR00210ResultPrintDTO();

            var loHeaderData = new GLR00210DTO
            {
                CGLACCOUNT_NO = "1001",
                CGLACCOUNT_NAME = "Account 1001",
                NTOTAL_DEBIT = 500,
                NTOTAL_CREDIT = 500,
                CCENTER_CODE = "C001",
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
            var loHeader2Data = new GLR00210DTO
            {
                CGLACCOUNT_NO = "1001",
                CGLACCOUNT_NAME = "Account 1001",
                NTOTAL_DEBIT = 500,
                NTOTAL_CREDIT = 500,
                CCENTER_CODE = "C002",
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
            var loSummaryData = new List<GLR00210DTO>();
            loSummaryData.Add(loHeaderData);
            loSummaryData.Add(loHeader2Data);

            //Set string empty if Duplicate
            var duplicates = loSummaryData.GroupBy(x => x.CGLACCOUNT_NO)
                                .Where(group => group.Count() > 1)
                                .SelectMany(group => group.Skip(1));

            foreach (var duplicate in duplicates)
            {
                duplicate.CGLACCOUNT_NO = "";
                duplicate.CGLACCOUNT_NAME = "";
            }

            //Detail
            var loDetailData = new List<GLR00211DTO>();
            int lnDetail;
            for (int i = 1; i <= 5; i++)
            {
                lnDetail = (i % 3) + 1;
                for (int s = 1; s <= lnDetail; s++)
                {
                    loDetailData.Add(new GLR00211DTO
                    {
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

            // Grouping logic
            var groupedData = loDetailData
                .GroupBy(item =>
                    new { item.CGLACCOUNT_NO, item.CACCOUNT, item.CDBCR, item.CBSIS, item.NBEGIN_BALANCE, item.CGLACCOUNT_NAME })
                .Select(group => new GLR00212DTO
                {
                    CGLACCOUNT_NO = group.Key.CGLACCOUNT_NO,
                    CGLACCOUNT_NAME = group.Key.CGLACCOUNT_NAME,
                    CACCOUNT = group.Key.CACCOUNT,
                    CDBCR = group.Key.CDBCR,
                    CBSIS = group.Key.CBSIS,
                    AccountDetailData = group
                        .GroupBy(subItem => new { subItem.CCENTER_CODE, subItem.CCENTER_NAME, subItem.NBEGIN_BALANCE })
                        .Select(subGroup => new GLR00213DTO
                        {
                            CCENTER_CODE = subGroup.Key.CCENTER_CODE,
                            CCENTER_NAME = subGroup.Key.CCENTER_NAME,
                            NBEGIN_BALANCE = subGroup.Key.NBEGIN_BALANCE,
                            CGLACCOUNT_NO = subGroup.FirstOrDefault().CGLACCOUNT_NO,
                            CenterDetailData = subGroup.Select(subSubItem => new GLR00214DTO
                            {
                                NEND_BALANCE = subSubItem.NEND_BALANCE,
                                CREC_ID = subSubItem.CREC_ID,
                                CREF_NO = subSubItem.CREF_NO,
                                CREF_DATE = DateTime.ParseExact(subSubItem.CREF_DATE, "yyyyMMdd", CultureInfo.InvariantCulture).ToString("dd-MMM-yyyy"),
                                CREF_PRD = subSubItem.CREF_PRD,
                                NDEBIT_AMOUNT = subSubItem.NDEBIT_AMOUNT,
                                NCREDIT_AMOUNT = subSubItem.NCREDIT_AMOUNT,
                                CDETAIL_DESC = subSubItem.CDETAIL_DESC,
                                CDOCUMENT_DATE = DateTime.ParseExact(subSubItem.CDOCUMENT_DATE, "yyyyMMdd", CultureInfo.InvariantCulture).ToString("dd-MMM-yyyy"),
                                CSOURCE_MODULE = subSubItem.CSOURCE_MODULE,
                                CCUSTOMER_SUPPLIER = subSubItem.CCUSTOMER_SUPPLIER,
                                CDOCUMENT_NO = subSubItem.CDOCUMENT_NO,
                                CGLACCOUNT_NO = subSubItem.CGLACCOUNT_NO,
                                CCENTER_CODE = subSubItem.CCENTER_CODE,
                            }).ToList()
                        }).ToList()
                }).ToList();


            loData.HeaderData = loHeaderData;
            loData.SummaryData = loSummaryData;
            loData.DetailData = groupedData;

            return loData;
        }

        public static GLR00210ResultWithBaseHeaderPrintDTO DefaultDataWithHeader()
        {
            var loParam = new BaseHeaderDTO()
            {
                CCOMPANY_NAME = "PT Realta Chackradarma",
                CPRINT_CODE = "010",
                CPRINT_NAME = "GL ACCOUNT LEDGER",
                CUSER_ID = "FMC",
            };

            GLR00210ResultWithBaseHeaderPrintDTO loRtn = new GLR00210ResultWithBaseHeaderPrintDTO();

            loRtn.BaseHeaderData = loParam;
            loRtn.GLR = DefaultData();

            return loRtn;
        }
    }

}
