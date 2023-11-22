﻿using System;
using System.Collections.Generic;
using System.Text;

namespace GSM02500COMMON.DTOs.GSM02502
{
    public class UploadUnitTypeCategorySaveDTO
    {
        public int NO { get; set; } = 0;
        public string UnitTypeCategoryId { get; set; } = "";
        public string UnitTypeCategoryName { get; set; } = "";
        public string Description { get; set; } = "";
        public string PropertyType { get; set; } = "";
        public int InvitationInvoicePeriod { get; set; } = 0;
        public bool Active { get; set; } = false;
        public string NonActiveDate { get; set; } = "";
    }
}
