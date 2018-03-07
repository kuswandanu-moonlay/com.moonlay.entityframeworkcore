using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Com.Moonlay.EntityFrameworkCore.Test.Models
{
    public class TestEntity : BaseEntity<string>, IAuditEntity, ISoftEntity
    {

        [Key]
        public string Code { get; set; }

        //[Key]
        public string Identity { get; set; }

        public DateTime CreatedUtc { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedAgent { get; set; }
        public DateTime LastModifiedUtc { get; set; }
        public string LastModifiedBy { get; set; }
        public string LastModifiedAgent { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime DeletedUtc { get; set; }
        public string DeletedBy { get; set; }
        public string DeletedAgent { get; set; }
    }
}
