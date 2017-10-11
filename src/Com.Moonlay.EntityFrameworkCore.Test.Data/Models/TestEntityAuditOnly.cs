using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTest.Models
{
    public class TestEntityAuditOnly : BaseEntity<Guid>, IAuditEntity
    {
        public DateTime _CreatedUtc { get; set; }
        public string _CreatedBy { get; set; }
        public string _CreatedAgent { get; set; }
        public DateTime _LastModifiedUtc { get; set; }
        public string _LastModifiedBy { get; set; }
        public string _LastModifiedAgent { get; set; }
    }
}
