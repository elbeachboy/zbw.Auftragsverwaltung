﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace zbw.Auftragsverwaltung.Api.Common.Models
{
    public class BaseMessage
    {
        public virtual string Status { get; protected set; }
        public string Message { get; set; }
    }
}
