﻿using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace GLT00500COMMON.DTOs
{
    public class GetSuccessProcessResultDTO : R_APIResultBaseDTO
    {
        public List<GetImportAdjustmentJournalResult> Data { get; set; }
    }
}
