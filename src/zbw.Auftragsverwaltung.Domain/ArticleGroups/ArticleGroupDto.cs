﻿using System;

namespace zbw.Auftragsverwaltung.Domain.ArticleGroups
{
    public class ArticleGroupDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public Guid? ParentId { get; set; }

    }
}
