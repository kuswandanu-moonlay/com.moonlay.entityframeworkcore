using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Com.Moonlay.EntityFrameworkCore.Test.Models
{
    public class StandardCompositeEntity : Com.Moonlay.Models.StandardEntity
    {
        [Key]
        public string Code { get; set; }

        [Key]
        public string Identity { get; set; }
    }
}
