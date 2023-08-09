using BusinessLayer.Interface;
using BusinessLayer.Services;
using CommonLayer.Models;
using CommonLayer.RequestModels;
using CommonLayer.ResponseModels;
using GreenPipes.Caching;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RepositoryLayer.Entity;
using RepositoryLayer.Migrations;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FundooNotes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoteController : Controller
    {
        private readonly INoteBusiness inoteBusiness;
        private readonly ILogger<NoteController> logger;
        private readonly IDistributedCache cache;

        public NoteController(INoteBusiness inoteBusiness, ILogger<NoteController> logger, IDistributedCache cache)
        {
            this.inoteBusiness = inoteBusiness;
            this.logger = logger;
            this.cache = cache;
        }

        [HttpPost]
        //LOCALHOST CONTROLLERNAME/METHODNAME/METHODROUTE => METHOD URL
        [Route("Take-A-Note")]
        public IActionResult TakeANote(NotesModel notesModel)
        {
            try
            {
                int userId = Convert.ToInt32(this.User.FindFirst("UserId").Value);
                /*int userId = (int)HttpContext.Session.GetInt32("UserId");*/
                var result = inoteBusiness.TakeANote(notesModel, userId);
                if (result != null)
                {
                    logger.LogInformation("Note Added Successfully");
                    return Ok(new ResponseModel<NoteEntity> { Success = true, Message = "Note Added Successfully", Data = result });
                }
                else
                {
                    logger.LogError("Note Not Added Successfully");
                    return BadRequest(new ResponseModel<NoteEntity> { Success = false, Message = "Note NOT Added Successfully", Data = null });
                }
            }
            catch (Exception ex)
            {
                logger.LogCritical("Exception Occurred...");
                throw ex;
            }
        }

        [HttpGet]
        [Route("Get-All-Notes")]
        public IActionResult GetAllNote()
        {
            try
            {
                int userId = Convert.ToInt32(this.User.FindFirst("UserId").Value);
                var result = inoteBusiness.GetAllNotes(userId);
                if (result != null)
                {
                    logger.LogInformation("Notes Displayed");
                    return Ok(new ResponseModel<List<NoteEntity>> { Success = true, Message = "Displaying the Notes.", Data = result });
                }
                else
                {
                    logger.LogError("Notes Not Displayed");
                    return BadRequest(new ResponseModel<List<NoteEntity>> { Success = false, Message = "No Notes available to Display", Data = result });
                }
            }
            catch (Exception ex)
            {
                logger.LogCritical("Exception Occurred...");
                throw ex;
            }
        }

        [HttpGet]
        [Route("Get-Note")]
        public IActionResult GetNote(int noteId)
        {
            try
            {
                int userId = Convert.ToInt32(this.User.FindFirst("UserId").Value);
                var result = inoteBusiness.GetNote(userId, noteId);
                if (result != null)
                {
                    logger.LogInformation("Note Displayed Based on ID");
                    return Ok(new ResponseModel<NoteEntity> { Success = true, Message = "Displaying the Notes.", Data = result });
                }
                else
                {
                    logger.LogError("Note Could Not be Displayed Based on ID");
                    return BadRequest(new ResponseModel<NoteEntity> { Success = false, Message = "No Notes available to Display", Data = result });
                }
            }
            catch (Exception ex)
            {
                logger.LogCritical("Exception Occurred...");
                throw ex;
            }
        }

        [HttpPost]
        [Route("Update-Note")]
        public IActionResult UpdateNote(int noteId, NotesModel notesModel)
        {
            try
            {
                int userId = Convert.ToInt32(this.User.FindFirst("UserId").Value);
                var result = inoteBusiness.UpdateNote(userId, noteId, notesModel);
                if (result != null)
                {
                    logger.LogInformation("Note Update Successful");
                    return Ok(new ResponseModel<NoteEntity> { Success = true, Message = "Note Update Successful", Data = result });
                }
                else
                {
                    logger.LogError("Note Update Unsuccessful");
                    return BadRequest(new ResponseModel<NoteEntity> { Success = false, Message = "Note Update Unsuccessful", Data = result });
                }
            }
            catch (Exception ex)
            {
                logger.LogCritical("Exception Occurred...");
                throw ex;
            }
        }

        [HttpDelete]
        [Route("Delete-Note")]
        public IActionResult DeleteNote(int noteId)
        {
            try
            {
                int userId = Convert.ToInt32(this.User.FindFirst("UserId").Value);
                var result = inoteBusiness.DeleteNote(userId, noteId);
                if (result != null)
                {
                    logger.LogInformation("Note Deleted Successfully");
                    return Ok(new ResponseModel<NoteEntity> { Success = true, Message = "Note Deleted Successfully", Data = result });
                }
                else
                {
                    logger.LogError("Note Delete Unsuccessful");
                    return BadRequest(new ResponseModel<NoteEntity> { Success = false, Message = "Note Delete Unsuccessful", Data = result });
                }
            }
            catch (Exception ex)
            {
                logger.LogCritical("Exception Occurred...");
                throw ex;
            }
        }

        [HttpPut]
        [Route("Trash-Note")]
        public IActionResult TrashNote(int noteId)
        {
            try
            {
                int userId = Convert.ToInt32(this.User.FindFirst("UserId").Value);
                var result = inoteBusiness.TrashNote(userId, noteId);
                if (result)
                {
                    logger.LogInformation("Note Trashed Successfully");
                    return Ok(new ResponseModel<bool> { Success = true, Message = "Note Trashed Successfully", Data = result });
                }
                else
                {
                    logger.LogError("Note Rcovered From Trash");
                    return BadRequest(new ResponseModel<bool> { Success = false, Message = "Note Recovered From Trash", Data = result });
                }
            }
            catch (Exception ex)
            {
                logger.LogCritical("Exception Occurred...");
                throw ex;
            }
        }

        [HttpPut]
        [Route("Pin-Note")]
        public IActionResult ChangePinNote(int noteId)
        {
            try
            {
                int userId = Convert.ToInt32(this.User.FindFirst("UserId").Value);
                var result = inoteBusiness.ChangePinNote(userId, noteId);
                if (result)
                {
                    logger.LogInformation("Note Pinned");
                    return Ok(new ResponseModel<bool> { Success = true, Message = "Note Pinned", Data = result });
                }
                else
                {
                    logger.LogError("Note UnPinned");
                    return BadRequest(new ResponseModel<bool> { Success = false, Message = "Note UnPinned", Data = result });
                }
            }
            catch(Exception ex)
            {
                logger.LogCritical("Exception Occurred...");
                throw ex;
            }
        }

        [HttpPut]
        [Route("Archive-Note")]
        public IActionResult ArchiveNote(int noteId)
        {
            try
            {
                int userId = Convert.ToInt32(this.User.FindFirst("UserId").Value);
                var result = inoteBusiness.ArchiveNote(userId, noteId);
                if (result)
                {
                    logger.LogInformation("Note Archived");
                    return Ok(new ResponseModel<bool> { Success = true, Message = "Note Archived", Data = result });
                }
                else
                {
                    logger.LogError("Note UnArchived");
                    return BadRequest(new ResponseModel<bool> { Success = false, Message = "Note UnArchived", Data = result });
                }
            }
            catch(Exception ex)
            {
                logger.LogCritical("Exception Occurred...");
                throw ex;
            }
        }

        [HttpPut]
        [Route("Change-Background-Color")]
        public IActionResult ChangeBackgroundColor(int noteId, string backgroundColor)
        {
            try
            {
                int userId = Convert.ToInt32(this.User.FindFirst("UserId").Value);
                var result = inoteBusiness.ChangeBackgroundColor(userId, noteId, backgroundColor);
                if (result != null)
                {
                    logger.LogInformation("Background Color Changed");
                    return Ok(new ResponseModel<string> { Success = true, Message = "Background Color Changed", Data = result });
                }
                else
                {
                    logger.LogError("Background Color Not Changed");
                    return BadRequest(new ResponseModel<string> { Success = false, Message = "Background Color Not Changed", Data = result });
                }
            }
            catch (Exception ex)
            {
                logger.LogCritical("Exception Occurred...");
                throw ex;
            }
        }

        [HttpPut]
        [Route("Set-Reminder")]
        public IActionResult SetReminder(int noteId, DateTime reminder)
        {
            try
            {
                int userId = Convert.ToInt32(this.User.FindFirst("UserId").Value);
                var result = inoteBusiness.NoteReminder(userId, noteId, reminder);
                if (result == reminder)
                {
                    logger.LogInformation("Reminder Set");
                    return Ok(new ResponseModel<DateTime> { Success = true, Message = "Reminder Set", Data = result });
                }
                else
                {
                    logger.LogError("Reminder Not Set");
                    return BadRequest(new ResponseModel<DateTime> { Success = false, Message = "Reminder Not Set", Data = result });
                }
            }
            catch (Exception ex)
            {
                logger.LogCritical("Exception Occurred...");
                throw ex;
            }
        }

        [HttpPut]
        [Route("Add-Image")]
        public IActionResult AddImage(int noteId, string filePath)
        {
            try
            {
                int userId = Convert.ToInt32(this.User.FindFirst("UserId").Value);
                var result = inoteBusiness.UploadImage(userId, noteId, filePath);
                if (result != null)
                {
                    logger.LogInformation("Image Added Successfully");
                    return Ok(new ResponseModel<string> { Success = true, Message = "Image Added Successfully", Data = result });
                }
                else
                {
                    logger.LogError("Image Not Added Successfully");
                    return BadRequest(new ResponseModel<string> { Success = false, Message = "Image Not Added Successfully", Data = result });
                }
            }
            catch (Exception ex)
            {
                logger.LogCritical("Exception Occurred...");
                throw ex;
            }
        }

        [HttpGet]
        [Route("Get-All-Notes_Using_Redis")]
        public async Task<IActionResult> GetAllNotesUsingRedis()
        {
            try
            {
                var cacheKey = "NotesList";
                //string SerializedNoteList;
                List<NoteEntity> NoteList;

                byte[] RedisNoteList = await cache.GetAsync(cacheKey);//for getting the data from the cache

                if (RedisNoteList != null)//if data exists in distributed cache
                {
                    logger.LogDebug("Getting the list from Redis Cache");
                    var SerializedNoteList = Encoding.UTF8.GetString(RedisNoteList);
                    NoteList = JsonConvert.DeserializeObject<List<NoteEntity>>(SerializedNoteList);
                }
                else
                {
                    logger.LogDebug("Setting the list to cache which is requested for the first time");
                    NoteList = (List<NoteEntity>)inoteBusiness.GetAllNotesInTable();
                    var SerializedNoteList = JsonConvert.SerializeObject(NoteList);
                    var redisNoteList = Encoding.UTF8.GetBytes(SerializedNoteList);
                    var options = new DistributedCacheEntryOptions().SetAbsoluteExpiration(DateTime.Now.AddMinutes(10)).SetSlidingExpiration(TimeSpan.FromMinutes(10));
                    await cache.SetAsync(cacheKey, redisNoteList, options);
                }
                logger.LogInformation("Got the notes list successfully from Redis");
                return Ok(NoteList);
            }
            catch(Exception ex)
            {
                logger.LogCritical(ex, "Exception thrown...");
                return BadRequest(new{Success = false, Message = ex.Message});
            }
        }
    }
}
