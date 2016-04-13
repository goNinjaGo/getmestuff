using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace getmestuff.Models
{
    /// <summary>
    /// Data transfer object for <see cref="OrderNote"/>
    /// </summary>
    public class OrderNoteDto
    {
        public OrderNoteDto() { }

        public OrderNoteDto(OrderNote note)
        {
            OrderNoteId = note.OrderNoteId;
            OrderId = note.OrderId;
            NoteText = note.NoteText;
            CreatedDate = note.CreatedDate;
        }

        [Key]
        public int OrderNoteId { get; set; }

        public int OrderId { get; set; }

        public string NoteText { get; set; }

        public DateTime CreatedDate { get; set; }

        public OrderNote ToEntity()
        {
            return new OrderNote
            {
                OrderNoteId = OrderNoteId,
                OrderId = OrderId,
                NoteText = NoteText,
                CreatedDate = CreatedDate
            };
        }
    }
}