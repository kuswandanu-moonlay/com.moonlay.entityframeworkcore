using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTest.Models
{
    public class TestEntitySoftOnly : BaseEntity<int>, ISoftEntity
    {
        public bool _IsDeleted { get; set; }
        public DateTime _DeletedUtc { get; set; }
        public string _DeletedBy { get; set; }
        public string _DeletedAgent { get; set; }
    }
}
