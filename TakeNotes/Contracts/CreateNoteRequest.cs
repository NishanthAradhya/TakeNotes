using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TakeNote.Contracts
{
    /// <summary>
    /// Request record to be accepted as Form body for AddNote Endpoint
    /// </summary>
    public record CreateNoteRequest
    {
        [MinLength(1, ErrorMessage = "Title must not be empty.")]
        public string Title { get; set; } = default!;

        public string Content { get; set; } = default!;
        
    }
}
