﻿using System;
using System.Collections.Generic;
using System.Text;

namespace GSM02500COMMON.DTOs.GSM02530
{
    public class UploadUnitDTO
    {
        public int No { get; set; } = 0;
        public string CompanyId { get; set; } = "";
        public string PropertyId { get; set; } = "";
        public string BuildingId { get; set; } = "";
        public string FloorId { get; set; } = "";
        public string UnitId { get; set; } = "";
        public string UnitName { get; set; } = "";
        public string UnitType { get; set; } = "";
        public string UnitView { get; set; } = "";
        public decimal GrossSize { get; set; } = 0;
        public decimal NetSize { get; set; } = 0;
        public decimal CommonArea { get; set; } = 0;
        public string UnitCategory { get; set; } = "";
        public bool Active { get; set; } = false;
        public string NonActiveDate { get; set; } = "";
        public string Valid { get; set; } = "";
        public string Notes { get; set; } = "";
    }
}
