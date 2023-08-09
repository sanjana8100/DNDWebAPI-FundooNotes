using BusinessLayer.Interface;
using BusinessLayer.Services;
using CommonLayer.RequestModels;
using CommonLayer.ResponseModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RepositoryLayer.Entity;
using System.Collections.Generic;
using System;
using RepositoryLayer.Migrations;

namespace FundooNotes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LabelController : ControllerBase
    {
        private readonly ILabelBusiness ilabelBusiness;
        private readonly ILogger<LabelController> logger;

        public LabelController(ILabelBusiness ilabelBusiness, ILogger<LabelController> logger)
        {
            this.ilabelBusiness = ilabelBusiness;
            this.logger = logger;
        }

        [HttpPost]
        [Route("Add-Label")]
        public IActionResult AddLabel(int noteId, string labelName)
        {
            try
            {
                int userId = Convert.ToInt32(this.User.FindFirst("UserId").Value);
                var result = ilabelBusiness.AddLabel(userId, noteId, labelName);
                if (result != null)
                {
                    logger.LogInformation("Label Added Successfully");
                    return Ok(new ResponseModel<LabelEntity> { Success = true, Message = "Label Added Successfully", Data = result });
                }
                else
                {
                    logger.LogError("Label Not Added Successfully");
                    return BadRequest(new ResponseModel<LabelEntity> { Success = false, Message = "Label NOT Added Successfully", Data = result });
                }
            }
            catch (Exception ex)
            {
                logger.LogCritical("Exception Occurred...");
                throw ex;
            }
        }

        [HttpGet]
        [Route("Get-All-Labels")]
        public IActionResult GetAllLabels()
        {
            try
            {
                int userId = Convert.ToInt32(this.User.FindFirst("UserId").Value);
                var result = ilabelBusiness.GetAllLabels(userId);
                if (result != null)
                {
                    logger.LogInformation("Labels Displayed");
                    return Ok(new ResponseModel<List<LabelEntity>> { Success = true, Message = "Displaying all the Labels.", Data = result });
                }
                else
                {
                    logger.LogError("Labels Could Not be Displayed");
                    return BadRequest(new ResponseModel<List<LabelEntity>> { Success = false, Message = "No Labels available to Display", Data = result });
                }
            }
            catch (Exception ex)
            {
                logger.LogCritical("Exception Occurred...");
                throw ex;
            }
        }

        [HttpGet]
        [Route("Get-All-Labels-By-Note")]
        public IActionResult GetAllLabelsByNote(int noteId)
        {
            try
            {
                int userId = Convert.ToInt32(this.User.FindFirst("UserId").Value);
                var result = ilabelBusiness.GetAllLabelsByNote(userId, noteId);
                if (result != null)
                {
                    logger.LogInformation("Labels Displayed For this Note");
                    return Ok(new ResponseModel<List<LabelEntity>> { Success = true, Message = "Displaying all the Labels For a Note", Data = result });
                    }
                else
                {
                    logger.LogError("Labels Could Not be Displayed For this Note");
                    return BadRequest(new ResponseModel<List<LabelEntity>> { Success = false, Message = "No Labels available to Display For a Note", Data = result });
                    }
            }
            catch (Exception ex)
            {
                logger.LogCritical("Exception Occurred...");
                throw ex;
            }
        }

        [HttpGet]
        [Route("Get-Label")]
        public IActionResult GetLabel(int labelId)
        {
            try
            {
                int userId = Convert.ToInt32(this.User.FindFirst("UserId").Value);
                var result = ilabelBusiness.GetLabel(userId, labelId);
                if (result != null)
                {
                    logger.LogInformation("Label Displayed By ID");
                    return Ok(new ResponseModel<LabelEntity> { Success = true, Message = "Displaying the Label.", Data = result });
                }
                else
                {
                    logger.LogError("Label Could Not be Displayed By ID");
                    return BadRequest(new ResponseModel<LabelEntity> { Success = false, Message = "No Label available to Display", Data = result });
                }
            }
            catch(Exception ex)
            {
                logger.LogCritical("Exception Occurred...");
                throw ex;
            }
        }

        [HttpGet]
        [Route("Get-All-Notes-By-Label")]
        public IActionResult GetAllNoteByLabels(int labelId)
        {
            try
            {
                int userId = Convert.ToInt32(this.User.FindFirst("UserId").Value);
                var result = ilabelBusiness.GetAllNotesByLabels(userId, labelId);
                if (result != null)
                {
                    logger.LogInformation("Notes Displayed Based On Labels");
                    return Ok(new ResponseModel<List<NoteEntity>> { Success = true, Message = "Displaying the Notes By Label.", Data = result });
                }
                else
                {
                    logger.LogError("Notes Could Not be Displayed Based On Labels");
                    return BadRequest(new ResponseModel<List<NoteEntity>> { Success = false, Message = "No Notes available to Display By Label", Data = result });
                }
            }
            catch (Exception ex)
            {
                logger.LogCritical("Exception Occurred...");
                throw ex;
            }
        }

        [HttpGet]
        [Route("Get-Note-By-Label")]
        public IActionResult GetNoteByLabels(int noteId, int labelId)
        {
            try
            {
                int userId = Convert.ToInt32(this.User.FindFirst("UserId").Value);
                var result = ilabelBusiness.GetNoteByLabels(userId, noteId, labelId);
                if (result != null)
                {
                    logger.LogInformation("Note Displayed Based On Label");
                    return Ok(new ResponseModel<NoteEntity> { Success = true, Message = "Displaying the Notes.", Data = result });
                }
                else
                {
                    logger.LogError("Note Could Not be Displayed Based On Label");
                    return BadRequest(new ResponseModel<NoteEntity> { Success = false, Message = "No Notes available to Display", Data = result });
                }
            }
            catch (Exception ex)
            {
                logger.LogCritical("Exception Occurred...");
                throw ex;
            }
        }

        [HttpDelete]
        [Route("Remove-Label")]
        public IActionResult RemoveLabel(int noteId, int labelId)
        {
            try
            {
                int userId = Convert.ToInt32(this.User.FindFirst("UserId").Value);
                var result = ilabelBusiness.RemoveLabel(userId, noteId, labelId);
                if (result != null)
                {
                    logger.LogInformation("Label Removed");
                    return Ok(new ResponseModel<LabelEntity> { Success = true, Message = "Label Removed.", Data = result });
                }
                else
                {
                    logger.LogError("Label Not Removed");
                    return BadRequest(new ResponseModel<LabelEntity> { Success = false, Message = "Label NOT Removed", Data = result });
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
