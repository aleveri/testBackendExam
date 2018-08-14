using System;
using System.Collections.Generic;
using static Entities.Enums;

namespace Entities
{
    public class Catalog : BaseEntity
    {
        public string Name { get; set; }
        
        public string Code { get; set; }
        
        public CatalogType Type { get; set; }

        public Guid? ParentId { get; set; }

        public virtual Catalog Parent { get; set; }

        public virtual ICollection<Catalog> Childs { get; set; }
    }
}
