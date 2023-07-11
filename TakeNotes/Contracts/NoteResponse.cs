using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TakeNote.Contracts
{
    public record NoteResponse(
        Guid Id,
        string Title,
        string Content,
        DateTime CreatedAt,
        DateTime ModifiedAt 
        );
}

