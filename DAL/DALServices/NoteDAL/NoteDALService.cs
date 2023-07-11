using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DALServices.NoteDAL
{
    public class NoteDALService : INoteDALService
    {
        public static Dictionary<Guid, Note> _noteDic = new Dictionary<Guid, Note>();
        /// <summary>
        /// To fetch Notes from the Dictionary
        /// </summary>
        /// <returns>List of Note</returns>
        public async Task<List<Note>> GetNotes()
        {
            //await should be used once the Actual DB is use
            return _noteDic.Values.ToList();

        }
        /// <summary>
        /// To fetch Note from the Dictionary based on the Id recieved from the Business Logic Layer
        /// </summary>
        /// <param name="Id">Id of Note to be fetched</param>
        /// <returns>Note which matches the param Id</returns>
        public async Task<Note> GetNoteById(Guid Id)
        {
            //await should be used once the Actual DB is use
            return _noteDic[Id];
        }

        /// <summary>
        /// Add note from the data sent from BLL
        /// </summary>
        /// <param name="note">data of a Note to be added</param>
        /// <returns>Note Entity which is added</returns>
        public async Task<Note> AddNote(Note note)
        {
            note.CreatedAt = note.ModifiedAt=DateTime.UtcNow;
            //await should be used once the Actual DB is use
            _noteDic.Add(note.Id, note);
            return note;
        }

        /// <summary>
        /// Update Note based on the Data sent from BLL
        /// </summary>
        /// <param name="note">Data to be updated</param>
        /// <returns>bool based on the result of action</returns>
        public async Task<bool> UpdateNote(Note note)
        {
            if (_noteDic.ContainsKey(note.Id))
            {
                //await should be used once the Actual DB is use

                // Updating Modified time
                note.ModifiedAt = DateTime.UtcNow;

                //Created time is assigned so that it shouldnt get lost
                note.CreatedAt = _noteDic[note.Id].CreatedAt;
                _noteDic[note.Id] = note;
                return true;
            }
            else
                return false;
            
        }

        /// <summary>
        /// Delect Note based on the Id recieved from BLL
        /// </summary>
        /// <param name="Id">Id of the Note to be deleted</param>
        /// <returns>return bool based on the result</returns>
        public async Task<bool> DeleteNote(Guid Id)
        {
            //await should be used once the Actual DB is use
            return _noteDic.Remove(Id);            
        }



    }
}
