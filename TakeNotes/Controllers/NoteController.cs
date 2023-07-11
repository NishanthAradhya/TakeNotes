using AutoMapper;
using BLL.Models;
using BLL.Services.NoteService;
using Microsoft.AspNetCore.Mvc;
using TakeNote.Contracts;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TakeNotes.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        private readonly INoteService _noteService;
        private Mapper _Mapper;

        public NoteController(INoteService noteService, ILogger<NoteController> logger)
        {
            _noteService = noteService;
            var _configProduct = new MapperConfiguration(cfg => { cfg.CreateMap<CreateNoteRequest, NoteBO>().ReverseMap(); cfg.CreateMap<NoteBO, NoteResponse>().ReverseMap(); });
            _Mapper = new Mapper(_configProduct);
        }

        /// <summary>
        /// Retrieves All the Notes in the Database
        /// </summary>
        /// <returns>Json Array of Notes will be returned</returns>
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(List<NoteResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
        public async Task<ActionResult<List<NoteResponse>>> Get()
        {
            var items =await _noteService.GetNotes();

            if (items.Count == 0)
            {
                return NoContent();  
            }
            var data = _Mapper.Map<List<NoteBO>, List<NoteResponse>>(items);
            return Ok(data);
        }

        /// <summary>
        /// Retrieves only one Note based on the Id provided as input
        /// </summary>
        /// <param name="id">id of type GUID used to retrieve the respective Note</param>
        /// <returns>Note based on the given Id</returns>
        /// 
        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(NoteResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
        public async Task<ActionResult<NoteResponse>> GetNoteById(Guid id)
        {
            var item =await _noteService.GetNoteById(id);
            if (item == null)
            {
                return NoContent();
            }
            var data = _Mapper.Map<NoteBO, NoteResponse>(item);
            return Ok(data);            
        }

        /// <summary>
        /// Used to Add a Note
        /// </summary>
        /// <param name="request">Request object to send as request body to insert</param>
        /// <returns></returns>
        [HttpPost]
        [Consumes("application/json"),Produces("application/json")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(NoContentResult), StatusCodes.Status204NoContent)]
        public async Task<ActionResult> Post(CreateNoteRequest request)
        {
            var item = _Mapper.Map<CreateNoteRequest, NoteBO>(request);
            var data = await _noteService.AddNote(item);
            var response = _Mapper.Map<NoteBO, NoteResponse>(data);

            return CreatedAtAction(
                actionName: nameof(GetNoteById),
                routeValues: new { id = item.Id },
                value: response);
        }

        /// <summary>
        /// Used to Update a Note based on the Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Consumes("application/json"), Produces("application/json")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateNote(Guid id, [FromBody] CreateNoteRequest value)
        {            
            var item = _Mapper.Map<CreateNoteRequest, NoteBO>(value);
            item.Id = id;
            var isUpdated = await _noteService.UpdateNote(item);
            if (!isUpdated)
            {
                throw new KeyNotFoundException();
            }
            return NoContent();            
        }

        /// <summary>
        /// Delete a Note based on the Id
        /// </summary>
        /// <param name="id">Id of type GUID used to delete the respective Note</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Consumes("application/json"), Produces("application/json")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteNote(Guid id)
        {
            var data = await _noteService.DeleteNote(id);
            if (!data)
            {
                throw new KeyNotFoundException("The given Id is not avaliable");
            }
            return NoContent();
        }
    }
}
