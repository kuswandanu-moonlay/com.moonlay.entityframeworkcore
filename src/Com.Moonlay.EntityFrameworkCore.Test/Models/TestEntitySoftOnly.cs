using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
 
{
    public class TestEntitySoftOnly : BaseEntity<int>, ISoftEntity
    {
        [Key]
        public string Code { get; set; }

        [Key]
        public string Identity { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime DeletedUtc { get; set; }
        public string DeletedBy { get; set; }
        public string DeletedAgent { get; set; }
    }
}
