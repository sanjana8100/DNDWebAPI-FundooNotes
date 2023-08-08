using BusinessLayer.Interface;
using CommonLayer.ResponseModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;

namespace FundooNotes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CollabController : ControllerBase
    {
        private readonly ICollabBusiness icollabBusiness;
        private readonly ILogger<CollabController> logger;

        public CollabController(ICollabBusiness icollabBusiness, ILogger<CollabController> logger)
        {
            this.icollabBusiness = icollabBusiness;
            this.logger = logger;
        }

        [HttpPost]
        [Route("Add-Collaboration")]
        public IActionResult AddCollaboration(int noteId, string collabEmail)
        {
            try
            {
                int userId = Convert.ToInt32(this.User.FindFirst("UserId").Value);
                var result = icollabBusiness.AddCollab(userId, noteId, collabEmail);
                if (result != null)
                {
                    logger.LogInformation("Collaboration Added");
                    return Ok(new ResponseModel<CollabEntity> { Success = true, Message = "Collaboration Added Successfully", Data = result });
                }
                else
                {
                    return BadRequest(new ResponseModel<CollabEntity> { Success = false, Message = "Collaboration NOT Added Successfully", Data = result });
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                throw ex;
            }
        }

        [HttpGet]
        [Route("Get-All-Collaborations")]
        public IActionResult GetAllCollaborations()
        {
            int userId = Convert.ToInt32(this.User.FindFirst("UserId").Value);
            var result = icollabBusiness.GetAllCollabs(userId);
            if (result != null)
            {
                return Ok(new ResponseModel<List<CollabEntity>> { Success = true, Message = "Displaying all the Collaborations.", Data = result });
            }
            else
            {
                return BadRequest(new ResponseModel<List<CollabEntity>> { Success = false, Message = "No Collaborations available to Display", Data = result });
            }
        }

        [HttpGet]
        [Route("Get-Collaboration")]
        public IActionResult GetCollaboration(int collabId)
        {
            int userId = Convert.ToInt32(this.User.FindFirst("UserId").Value);
            var result = icollabBusiness.GetCollab(userId, collabId);
            if (result != null)
            {
                return Ok(new ResponseModel<CollabEntity> { Success = true, Message = "Displaying the Collaboration.", Data = result });
            }
            else
            {
                return BadRequest(new ResponseModel<CollabEntity> { Success = false, Message = "No Collaboration available to Display", Data = result });
            }
        }

        [HttpGet]
        [Route("Get-All-Notes-By-Collaboration")]
        public IActionResult GetAllNoteByCollaboration(int collabId)
        {
            int userId = Convert.ToInt32(this.User.FindFirst("UserId").Value);
            var result = icollabBusiness.GetAllNotesByCollab(userId, collabId);
            if (result != null)
            {
                return Ok(new ResponseModel<List<NoteEntity>> { Success = true, Message = "Displaying the Notes By Collaboration.", Data = result });
            }
            else
            {
                return BadRequest(new ResponseModel<List<NoteEntity>> { Success = false, Message = "No Notes available to Display By Collaboration", Data = result });
            }
        }

        [HttpGet]
        [Route("Get-Note-By-Collaboration")]
        public IActionResult GetNoteByCollaboration(int noteId, int collabId)
        {
            int userId = Convert.ToInt32(this.User.FindFirst("UserId").Value);
            var result = icollabBusiness.GetNoteByCollab(userId, noteId, collabId);
            if (result != null)
            {
                return Ok(new ResponseModel<NoteEntity> { Success = true, Message = "Displaying the Note By Collaboration.", Data = result });
            }
            else
            {
                return BadRequest(new ResponseModel<NoteEntity> { Success = false, Message = "No Note available to Display", Data = result });
            }
        }

        [HttpDelete]
        [Route("Remove-Collaboration")]
        public IActionResult RemoveCollaboration(int noteId, int collabId)
        {
            int userId = Convert.ToInt32(this.User.FindFirst("UserId").Value);
            var result = icollabBusiness.RemoveCollab(userId, noteId, collabId);
            if (result != null)
            {
                return Ok(new ResponseModel<CollabEntity> { Success = true, Message = "Collaboration Removed.", Data = result });
            }
            else
            {
                return BadRequest(new ResponseModel<CollabEntity> { Success = false, Message = "Collaboration NOT Removed", Data = result });
            }
        }
    }
}
