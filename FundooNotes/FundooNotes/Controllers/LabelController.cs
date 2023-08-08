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
        private readonly ILogger<LabelController> _logger;

        public LabelController(ILabelBusiness ilabelBusiness, ILogger<LabelController> _logger)
        {
            this.ilabelBusiness = ilabelBusiness;
            this._logger = _logger;
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
                    _logger.LogInformation("Label Added");
                    return Ok(new ResponseModel<LabelEntity> { Success = true, Message = "Label Added Successfully", Data = result });
                }
                else
                {
                    return BadRequest(new ResponseModel<LabelEntity> { Success = false, Message = "Label NOT Added Successfully", Data = result });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw ex;
            }
        }

        [HttpGet]
        [Route("Get-All-Labels")]
        public IActionResult GetAllLabels()
        {
            int userId = Convert.ToInt32(this.User.FindFirst("UserId").Value);
            var result = ilabelBusiness.GetAllLabels(userId);
            if (result != null)
            {
                return Ok(new ResponseModel<List<LabelEntity>> { Success = true, Message = "Displaying all the Labels.", Data = result });
            }
            else
            {
                return BadRequest(new ResponseModel<List<LabelEntity>> { Success = false, Message = "No Labels available to Display", Data = result });
            }
        }

        [HttpGet]
        [Route("Get-Label")]
        public IActionResult GetLabel(int labelId)
        {
            int userId = Convert.ToInt32(this.User.FindFirst("UserId").Value);
            var result = ilabelBusiness.GetLabel(userId, labelId);
            if (result != null)
            {
                return Ok(new ResponseModel<LabelEntity> { Success = true, Message = "Displaying the Label.", Data = result });
            }
            else
            {
                return BadRequest(new ResponseModel<LabelEntity> { Success = false, Message = "No Label available to Display", Data = result });
            }
        }

        [HttpGet]
        [Route("Get-All-Notes-By-Label")]
        public IActionResult GetAllNoteByLabels(int labelId)
        {
            int userId = Convert.ToInt32(this.User.FindFirst("UserId").Value);
            var result = ilabelBusiness.GetAllNotesByLabels(userId, labelId);
            if (result != null)
            {
                return Ok(new ResponseModel<List<NoteEntity>> { Success = true, Message = "Displaying the Notes By Label.", Data = result });
            }
            else
            {
                return BadRequest(new ResponseModel<List<NoteEntity>> { Success = false, Message = "No Notes available to Display By Label", Data = result });
            }
        }

        [HttpGet]
        [Route("Get-Note-By-Label")]
        public IActionResult GetNoteByLabels(int noteId, int labelId)
        {
            int userId = Convert.ToInt32(this.User.FindFirst("UserId").Value);
            var result = ilabelBusiness.GetNoteByLabels(userId, noteId, labelId);
            if (result != null)
            {
                return Ok(new ResponseModel<NoteEntity> { Success = true, Message = "Displaying the Notes.", Data = result });
            }
            else
            {
                return BadRequest(new ResponseModel<NoteEntity> { Success = false, Message = "No Notes available to Display", Data = result });
            }
        }

        [HttpDelete]
        [Route("Remove-Label")]
        public IActionResult RemoveLabel(int noteId, int labelId)
        {
            int userId = Convert.ToInt32(this.User.FindFirst("UserId").Value);
            var result = ilabelBusiness.RemoveLabel(userId, noteId, labelId);
            if (result != null)
            {
                return Ok(new ResponseModel<LabelEntity> { Success = true, Message = "Label Removed.", Data = result });
            }
            else
            {
                return BadRequest(new ResponseModel<LabelEntity> { Success = false, Message = "Label NOT Removed", Data = result });
            }
        }
    }
}
