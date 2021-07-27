﻿using System;
using System.Collections.Generic;
using System.Text;

namespace zbw.Auftragsverwaltung.Core.Orders.Dto
{
    public class OrderDto
    {
        public Guid Id { get; set; }

        public int OrderNr { get; set; }

        public int CustomerNr { get; set; }

        public DateTime Date { get; set; }

        public string UserId { get; set; }
    }
}
