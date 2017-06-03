using MUMScrum.Model.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MUMScrum.Model.BaseEntity
{
    public class BaseEntity<TKey>
    {
        [Key]
        public TKey Id { get; set; }
        public int? CreatedById { get; set; }
        public int? ModifiedById { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }

        [ForeignKey("CreatedById")]
        public virtual Employee CreatedBy { get; set; }

        [ForeignKey("ModifiedById")]
        public virtual Employee ModifiedBy { get; set; }
    }
}
