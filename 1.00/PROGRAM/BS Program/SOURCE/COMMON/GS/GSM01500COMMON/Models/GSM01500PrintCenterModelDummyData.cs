using BaseHeaderReportCOMMON.Models;
using BaseHeaderReportCOMMON;
using GSM01500COMMON.DTOs.GSM01500Print;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GSM01500COMMON.Models
{
    public static class GSM01500PrintCenterModelDummyData
    {
        public static GSM01500PrintCenterResultDTO DefaultDataCenter()
        {
            GSM01500PrintCenterResultDTO loData = new GSM01500PrintCenterResultDTO()
            {
                Title = "CENTER",
                Header = "JBMPC - Metro Park Residence",
                PrintDept = true,
                Column = new GSM01500PrintCenterColumnDTO()
            };

            int lnHeader = 5;
            int lnDetail;
            List<GSM01500PrintCenterDTO> loCollection = new List<GSM01500PrintCenterDTO>();
            for (int j = 1; j <= lnHeader; j++)
            {
                lnDetail = (j % 3) + 1;
                for (int k = 1; k <= lnDetail; k++)
                {
                    if (j % 3 == 0)
                    {
                        loCollection.Add(new GSM01500PrintCenterDTO()
                        {
                            CCENTER_CODE = $"Center Code {j}",
                            CCENTER_NAME = $"Center Name {j}",
                            LACTIVE = (j % 2 == 0)
                        });
                        break;
                    }
                    loCollection.Add(new GSM01500PrintCenterDTO()
                    {
                        CCENTER_CODE = $"Center Code {j}",
                        CCENTER_NAME = $"Center Name {j}",
                        CDEPT_CODE = $"Department Code {k}",
                        CDEPT_NAME = $"Department Name {k}",
                        LACTIVE = (j % 2 == 0)
                    });
                }
            }

            var loTempData = loCollection
                .GroupBy(item => new { item.CCENTER_CODE, item.CCENTER_NAME, item.LACTIVE})
                .Select(header => new GSM01500PrintCenterHeaderDTO
                {
                    CCENTER_CODE = header.Key.CCENTER_CODE,
                    CCENTER_NAME = header.Key.CCENTER_NAME,
                    LACTIVE = header.Key.LACTIVE,
                    DetailData = header
                        .Select(detail => new GSM01500PrintCenterDetailDTO
                        {
                            CDEPT_CODE = detail.CDEPT_CODE,
                            CDEPT_NAME = detail.CDEPT_NAME
                        })
                        .ToList()
                })
                .ToList();

            loData.Data = loTempData;

            return loData;
        }

        public static GSM01500PrintCenterResultWithBaseHeaderPrintDTO DefaultDataWithHeader()
        {/*
            var loParam = new BaseHeaderDTO()
            {
                CCOMPANY_NAME = "PT Realta Chackradarma",
                CPRINT_CODE = "001",
                CPRINT_NAME = "Center",
                CUSER_ID = "ERC",
            };*/

            GSM01500PrintCenterResultWithBaseHeaderPrintDTO loRtn = new GSM01500PrintCenterResultWithBaseHeaderPrintDTO();
            loRtn.BaseHeaderData = GenerateDataModelHeader.DefaultData().BaseHeaderData;
            loRtn.CenterData = GSM01500PrintCenterModelDummyData.DefaultDataCenter();

            return loRtn;
        }
    }
}
