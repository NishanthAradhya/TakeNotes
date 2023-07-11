using AutoMapper;
using BLL.Models;
using DAL.DALServices.NoteDAL;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.NoteService
{
    public class NoteService : INoteService
    {
        private Mapper _Mapper;
        private readonly INoteDALService _noteDALService;
        public NoteService(INoteDALService noteDALService) 
        {
            //Mapper configuration to Map DAL object and BAL objects
            var _configProduct = new MapperConfiguration(cfg => cfg.CreateMap<NoteBO, Note>().ReverseMap());
            _Mapper = new Mapper(_configProduct);
            _noteDALService = noteDALService;
        }

        /// <summary>
        /// Service to fetch all the created Notes
        /// </summary>
        /// <returns>List of NoteBO object</returns>
        public async Task<List<NoteBO>> GetNotes()
        {
            var items = await _noteDALService.GetNotes();
            var data = _Mapper.Map<List<Note>, List<NoteBO>>(items);
            return data;
        }

        /// <summary>
        /// Service to fetch the Note based on the param id
        /// </summary>
        /// <param name="id">id of expected Note</param>
        /// <returns> Single object of NoteBO object</returns>
        public async Task<NoteBO> GetNoteById(Guid id)
        {
            var note=await _noteDALService.GetNoteById(id);
            var data = _Mapper.Map<Note, NoteBO>(note);
            return data;

        }
        /// <summary>
        /// Add Note based on the data given from param note of type NoteBO
        /// </summary>
        /// <param name="note">note of type NoteBO</param>
        /// <returns>NoteBO object which was mapped to Note entity which got added</returns>
        public async Task<NoteBO> AddNote(NoteBO note)
        {
            note.Id = Guid.NewGuid();
            var data = _Mapper.Map<NoteBO, Note>(note);
            var response= await _noteDALService.AddNote(data);
            return _Mapper.Map<Note, NoteBO>(response);

        }
        /// <summary>
        /// Update Note based on the id in the param note
        /// </summary>
        /// <param name="note"></param>
        /// <returns> return bool based on the operation</returns>
        public async Task<bool> UpdateNote(NoteBO note)
        {
            var data = _Mapper.Map<NoteBO, Note>(note);
             return await _noteDALService.UpdateNote(data);
        }
        /// <summary>
        /// Delete Note basesd on the param id
        /// </summary>
        /// <param name="id">id of Note to be deleted</param>
        /// <returns>return bool based on the operation performed</returns>
        public async Task<bool> DeleteNote(Guid id)
        {
            return await _noteDALService.DeleteNote(id);
        }
    }
}
