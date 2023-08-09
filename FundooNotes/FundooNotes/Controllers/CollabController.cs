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
                    logger.LogInformation("Collaboration Added Successfully");
                    return Ok(new ResponseModel<CollabEntity> { Success = true, Message = "Collaboration Added Successfully", Data = result });
                }
                else
                {
                    logger.LogError("Collaboration NOT Added Successfully");
                    return BadRequest(new ResponseModel<CollabEntity> { Success = false, Message = "Collaboration NOT Added Successfully", Data = result });
                }
            }
            catch (Exception ex)
            {
                logger.LogCritical("Exception Occurred...");
                throw ex;
            }
        }

        [HttpGet]
        [Route("Get-All-Collaborations")]
        public IActionResult GetAllCollaborations()
        {
            try
            {
                int userId = Convert.ToInt32(this.User.FindFirst("UserId").Value);
                var result = icollabBusiness.GetAllCollabs(userId);
                if (result != null)
                {
                    logger.LogInformation("Collaborations Displayed");
                    return Ok(new ResponseModel<List<CollabEntity>> { Success = true, Message = "Displaying all the Collaborations.", Data = result });
                }
                else
                {
                    logger.LogError("Collaborations Could Not be Displayed");
                    return BadRequest(new ResponseModel<List<CollabEntity>> { Success = false, Message = "No Collaborations available to Display", Data = result });
                }
            }
            catch(Exception ex)
            {
                logger.LogCritical("Exception Occurred...");
                throw ex;
            }
        }

        [HttpGet]
        [Route("Get-All-Collaborations-By-Note")]
        public IActionResult GetAllCollaborationsByNote(int noteId)
        {
            try
            {
                int userId = Convert.ToInt32(this.User.FindFirst("UserId").Value);
                var result = icollabBusiness.GetAllCollabsByNote(userId, noteId);
                if (result != null)
                {
                    logger.LogInformation("Collaborations Displayed For a Note");
                    return Ok(new ResponseModel<List<CollabEntity>> { Success = true, Message = "Displaying all the Collaborations For this Note", Data = result });
                    }
                else
                {
                    logger.LogError("Collaborations Could Not be Displayed For a Note");
                    return BadRequest(new ResponseModel<List<CollabEntity>> { Success = false, Message = "No Collaborations available to Display For this Note", Data = result });
                    }
            }
            catch (Exception ex)
            {
                logger.LogCritical("Exception Occurred...");
                throw ex;
            }
        }

        [HttpGet]
        [Route("Get-Collaboration")]
        public IActionResult GetCollaboration(int collabId)
        {
            try
            {
                int userId = Convert.ToInt32(this.User.FindFirst("UserId").Value);
                var result = icollabBusiness.GetCollab(userId, collabId);
                if (result != null)
                {
                    logger.LogInformation("Collaboration Displayed");
                    return Ok(new ResponseModel<CollabEntity> { Success = true, Message = "Displaying the Collaboration.", Data = result });
                }
                else
                {
                    logger.LogError("Collaboration Could Not be Displayed");
                    return BadRequest(new ResponseModel<CollabEntity> { Success = false, Message = "No Collaboration available to Display", Data = result });
                }
            }
            catch(Exception ex)
            {
                logger.LogCritical("Exception Occurred...");
                throw ex;
            }
        }

        [HttpGet]
        [Route("Get-All-Notes-By-Collaboration")]
        public IActionResult GetAllNoteByCollaboration(int collabId)
        {
            try
            {
                int userId = Convert.ToInt32(this.User.FindFirst("UserId").Value);
                var result = icollabBusiness.GetAllNotesByCollab(userId, collabId);
                if (result != null)
                {
                    logger.LogInformation("Notes Displayed Based On Collaborations");
                    return Ok(new ResponseModel<List<NoteEntity>> { Success = true, Message = "Displaying the Notes By Collaboration.", Data = result });
                }
                else
                {
                    logger.LogError("Notes Could Not be Displayed Based On Collborations");
                    return BadRequest(new ResponseModel<List<NoteEntity>> { Success = false, Message = "No Notes available to Display By Collaboration", Data = result });
                }
            }
            catch (Exception ex)
            {
                logger.LogCritical("Exception Occurred...");
                throw ex;
            }
        }

        [HttpGet]
        [Route("Get-Note-By-Collaboration")]
        public IActionResult GetNoteByCollaboration(int noteId, int collabId)
        {
            try
            {
                int userId = Convert.ToInt32(this.User.FindFirst("UserId").Value);
                var result = icollabBusiness.GetNoteByCollab(userId, noteId, collabId);
                if (result != null)
                {
                    logger.LogInformation("Note Displayed Based On Collaboration");
                    return Ok(new ResponseModel<NoteEntity> { Success = true, Message = "Displaying the Note By Collaboration.", Data = result });
                }
                else
                {
                    logger.LogError("Note Could Not be Displayed Based On Collboration");
                    return BadRequest(new ResponseModel<NoteEntity> { Success = false, Message = "No Note available to Display", Data = result });
                }
            }
            catch (Exception ex)
            {
                logger.LogCritical("Exception Occurred...");
                throw ex;
            }
        }

        [HttpDelete]
        [Route("Remove-Collaboration")]
        public IActionResult RemoveCollaboration(int noteId, int collabId)
        {
            try
            {
                int userId = Convert.ToInt32(this.User.FindFirst("UserId").Value);
                var result = icollabBusiness.RemoveCollab(userId, noteId, collabId);
                if (result != null)
                {
                    logger.LogInformation("Collaboration Removed");
                    return Ok(new ResponseModel<CollabEntity> { Success = true, Message = "Collaboration Removed.", Data = result });
                }
                else
                {
                    logger.LogError("Collaboration Not Removed");
                    return BadRequest(new ResponseModel<CollabEntity> { Success = false, Message = "Collaboration NOT Removed", Data = result });
                }
            }
            catch (Exception ex)
            {
                logger.LogCritical("Exception Occurred...");
                throw ex;
            }
        }
    }
}
