using BusinessLayer.Interface;
using BusinessLayer.Services;
using CommonLayer.Models;
using CommonLayer.RequestModels;
using CommonLayer.ResponseModels;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Entity;
using RepositoryLayer.Migrations;
using System;
using System.Collections.Generic;

namespace FundooNotes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoteController : Controller
    {
        private readonly INoteBusiness inoteBusiness;

        public NoteController(INoteBusiness inoteBusiness)
        {
            this.inoteBusiness = inoteBusiness;
        }

        [HttpPost]
        //LOCALHOST CONTROLLERNAME/METHODNAME/METHODROUTE => METHOD URL
        [Route("Take-A-Note")]
        public IActionResult TakeANote(NotesModel notesModel)
        {
            int userId = Convert.ToInt32(this.User.FindFirst("UserId").Value);
            var result = inoteBusiness.TakeANote(notesModel, userId);
            if (result != null)
            {
                return Ok(new ResponseModel<NoteEntity> { Success = true, Message = "Note Added Successfully", Data = result });
            }
            else
            {
                return BadRequest(new ResponseModel<NoteEntity> { Success = false, Message = "Note NOT Added Successfully", Data = result });
            }
        }

        [HttpGet]
        [Route("Get-All-Notes")]
        public IActionResult GetAllNote()
        {
            int userId = Convert.ToInt32(this.User.FindFirst("UserId").Value);
            var result = inoteBusiness.GetAllNotes(userId);
            if (result != null)
            {
                return Ok(new ResponseModel<List<NoteEntity>> { Success = true, Message = "Displaying the Notes.", Data = result });
            }
            else
            {
                return BadRequest(new ResponseModel<List<NoteEntity>> { Success = false, Message = "No Notes available to Display", Data = result });
            }
        }

        [HttpGet]
        [Route("Get-Note")]
        public IActionResult GetNote(int noteId)
        {
            int userId = Convert.ToInt32(this.User.FindFirst("UserId").Value);
            var result = inoteBusiness.GetNote(userId, noteId);
            if (result != null)
            {
                return Ok(new ResponseModel<NoteEntity> { Success = true, Message = "Displaying the Notes.", Data = result });
            }
            else
            {
                return BadRequest(new ResponseModel<NoteEntity> { Success = false, Message = "No Notes available to Display", Data = result });
            }
        }

        [HttpPost]
        [Route("Update-Note")]
        public IActionResult UpdateNote(int noteId, NotesModel notesModel)
        {
            int userId = Convert.ToInt32(this.User.FindFirst("UserId").Value);
            var result = inoteBusiness.UpdateNote(userId, noteId, notesModel);
            if (result != null)
            {
                return Ok(new ResponseModel<NoteEntity> { Success = true, Message = "Note Updated Successful", Data = result });
            }
            else
            {
                return BadRequest(new ResponseModel<NoteEntity> { Success = false, Message = "Note Update Unsuccessful", Data = result });
            }
        }

        [HttpDelete]
        [Route("Delete-Note")]
        public IActionResult DeleteNote(int noteId)
        {
            int userId = Convert.ToInt32(this.User.FindFirst("UserId").Value);
            var result = inoteBusiness.DeleteNote(userId, noteId);
            if (result != null)
            {
                return Ok(new ResponseModel<NoteEntity> { Success = true, Message = "Note Deleted Successful", Data = result });
            }
            else
            {
                return BadRequest(new ResponseModel<NoteEntity> { Success = false, Message = "Note Delete Unsuccessful", Data = result });
            }
        }

        [HttpPut]
        [Route("Trash-Note")]
        public IActionResult TrashNote(int noteId)
        {
            var result = inoteBusiness.TrashNote(noteId);
            if (result)
            {
                return Ok(new ResponseModel<bool> { Success = true, Message = "Note Trashed Successfully", Data = result });
            }
            else
            {
                return BadRequest(new ResponseModel<bool> { Success = false, Message = "Note Recovered From Trash", Data = result });
            }
        }

        [HttpPut]
        [Route("Pin-Note")]
        public IActionResult ChangePinNote(int noteId)
        {
            var result = inoteBusiness.ChangePinNote(noteId);
            if (result)
            {
                return Ok(new ResponseModel<bool> { Success = true, Message = "Note Pinned", Data = result });
            }
            else
            {
                return BadRequest(new ResponseModel<bool> { Success = false, Message = "Note UnPinned", Data = result });
            }
        }

        [HttpPut]
        [Route("Archive-Note")]
        public IActionResult ArchiveNote(int noteId)
        {
            var result = inoteBusiness.ArchiveNote(noteId);
            if (result)
            {
                return Ok(new ResponseModel<bool> { Success = true, Message = "Note Archived", Data = result });
            }
            else
            {
                return BadRequest(new ResponseModel<bool> { Success = false, Message = "Note UnArchived", Data = result });
            }
        }
    }
}
