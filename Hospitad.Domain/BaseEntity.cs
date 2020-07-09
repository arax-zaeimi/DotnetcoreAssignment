using System;
using System.Collections.Generic;
using System.Text;

namespace Hospitad.Domain
{
    /// <summary>
    /// All entities inherit from this entity due to their Id and modification dates
    /// </summary>
    public class BaseEntity : IEntity
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
