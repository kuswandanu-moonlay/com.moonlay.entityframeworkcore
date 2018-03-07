using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Com.Moonlay.EntityFrameworkCore.Test.Models
{
    public class StandardEntity : Com.Moonlay.Models.StandardEntity
    {
        public string Code { get; set; }
        public DateTime Stamp { get; set; }
    }
}
