using BLL.Models;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.NoteService
{
    public interface INoteService
    {
        Task<List<NoteBO>> GetNotes();
        Task<NoteBO> AddNote(NoteBO note);        
        Task<NoteBO> GetNoteById(Guid Id);
        Task<bool> UpdateNote(NoteBO note);
        Task<bool> DeleteNote(Guid Id);
    }
}
