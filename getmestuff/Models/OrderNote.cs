using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace getmestuff.Models
{
    /// <summary>
    /// OrderNote Entity
    /// </summary>
    public class OrderNote
    {
        public int OrderNoteId { get; set; }

        [ForeignKey("Order")]
        public int OrderId { get; set; }

        public string NoteText { get; set; }

        public DateTime CreatedDate { get; set; }

        public virtual Order Order { get; set; }
    }
}