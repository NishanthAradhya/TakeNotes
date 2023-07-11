using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DALServices.NoteDAL
{
    public interface INoteDALService
    {
        Task<List<Note>> GetNotes();
        Task<Note> GetNoteById(Guid Id);
        Task<Note> AddNote(Note note);
        Task<bool> UpdateNote(Note note);
        Task<bool> DeleteNote(Guid Id);
    }
}
