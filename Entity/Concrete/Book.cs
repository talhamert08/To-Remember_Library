using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Concrete
{
    public class Book : Core.Entities.Entity
    {
        public string BookName { get; set; }
        public string Writer { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public DateTime PublicationDate { get; set; }

    }

    public class BookDto : Dto
    {
        public string BookName { get; set; }
        public string Writer { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public DateTime PublicationDate { get; set; }
    }
}
