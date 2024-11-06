using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Dto : IDto
    {
        [Key]
        public Guid Id { get; set; }
        public string AddUserId { get; set; } = string.Empty;
        public DateTime AddDate { get; set; }
        public string EditUserId { get; set; } = string.Empty;
        public DateTime EditDate { get; set; }
    }
    public interface IDto
    {
        Guid Id { get; set; }
        string AddUserId { get; set; }
        DateTime AddDate { get; set; }
        string EditUserId { get; set; }
        DateTime EditDate { get; set; }
    }
}
