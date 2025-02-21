﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using zbw.Auftragsverwaltung.Core.Articles.Entities;
using zbw.Auftragsverwaltung.Core.Common.Contracts;

namespace zbw.Auftragsverwaltung.Core.Articles.Contracts
{
    public interface IArticleRepository : IRepository<Article,Guid>
    {

    }
}
