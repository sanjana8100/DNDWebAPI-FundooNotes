﻿using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using CommonLayer.Models;
using CommonLayer.RequestModels;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using RepositoryLayer.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Security.Principal;
using System.Text;

namespace RepositoryLayer.Services
{
    public class NoteRepo : INoteRepo
    {
        private readonly FundooDBContext fundooDBContext;
        private readonly IConfiguration configuration;

        public NoteRepo(FundooDBContext fundooDBContext, IConfiguration configuration)
        {
            this.fundooDBContext = fundooDBContext;
            this.configuration = configuration;
        }

        public NoteEntity TakeANote(NotesModel notesModel, int userId)
        {
            try
            {
                NoteEntity noteEntity = new NoteEntity();

                noteEntity.UserId = userId;

                noteEntity.Title = notesModel.Title;
                noteEntity.Note = notesModel.Note;
                noteEntity.RemindMe = notesModel.RemindMe;
                noteEntity.BackgroundColor = notesModel.BackgroundColor;
                noteEntity.AddImage = notesModel.AddImage;
                noteEntity.Archive = notesModel.Archive;
                noteEntity.PinNote = notesModel.PinNote;
                noteEntity.UnPinNote = notesModel.UnPinNote;
                noteEntity.Trash = notesModel.Trash;
                noteEntity.CreatedAt = DateTime.Now;
                noteEntity.UpdatedAt = DateTime.Now;

                fundooDBContext.Notes.Add(noteEntity);
                fundooDBContext.SaveChanges();

                return noteEntity;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<NoteEntity> GetAllNotes(int userId)
        {
            try
            {
                List<NoteEntity> notes = new List<NoteEntity>();
                notes = fundooDBContext.Notes.Where(x => x.UserId == userId).ToList();
                if (notes != null)
                {
                    return notes;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public NoteEntity GetNote(int userId, int noteId)
        {
            try
            {
                NoteEntity noteEntity = new NoteEntity();
                noteEntity = fundooDBContext.Notes.Where(x => x.NoteId == noteId && x.UserId == userId).FirstOrDefault();

                if (noteEntity != null)
                {
                    return noteEntity;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public NoteEntity UpdateNote(int userId, int noteId, NotesModel notesModel)
        {
            try
            {
                NoteEntity noteEntity = new NoteEntity();
                noteEntity = fundooDBContext.Notes.Where(x => x.NoteId == noteId && x.UserId == userId).FirstOrDefault();

                if (noteEntity != null)
                {
                    noteEntity.Title = notesModel.Title;
                    noteEntity.Note = notesModel.Note;
                    noteEntity.RemindMe = notesModel.RemindMe;
                    noteEntity.BackgroundColor = notesModel.BackgroundColor;
                    noteEntity.AddImage = notesModel.AddImage;
                    noteEntity.Archive = notesModel.Archive;
                    noteEntity.PinNote = notesModel.PinNote;
                    noteEntity.UnPinNote = notesModel.UnPinNote;
                    noteEntity.Trash = notesModel.Trash;
                    noteEntity.UpdatedAt = DateTime.Now;

                    fundooDBContext.SaveChanges();
                    return noteEntity;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public NoteEntity DeleteNote(int userId, int noteId)
        {
            try
            {
                NoteEntity noteEntity = new NoteEntity();
                noteEntity = fundooDBContext.Notes.Where(x => x.NoteId == noteId && x.UserId == userId).FirstOrDefault();
                if (noteEntity != null)
                {
                    fundooDBContext.Notes.Remove(noteEntity);
                    fundooDBContext.SaveChanges();

                    return noteEntity;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool TrashNote(int userId, int noteId)
        {
            try
            {
                NoteEntity noteEntity = new NoteEntity();
                noteEntity = fundooDBContext.Notes.Where(x => x.NoteId == noteId && x.UserId == userId).FirstOrDefault();
                if (noteEntity.Trash)
                {
                    noteEntity.Trash = false;
                }
                else
                {
                    noteEntity.Trash = true;
                }

                fundooDBContext.SaveChanges();
                return noteEntity.Trash;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool ChangePinNote(int userId, int noteId)
        {
            try
            {
                NoteEntity noteEntity = new NoteEntity();
                noteEntity = fundooDBContext.Notes.Where(x => x.NoteId == noteId && x.UserId == userId).FirstOrDefault();
                if (noteEntity.PinNote)
                {
                    noteEntity.PinNote = false;
                    noteEntity.UnPinNote = true;
                }
                else
                {
                    noteEntity.PinNote = true;
                    noteEntity.UnPinNote = false;
                }

                fundooDBContext.SaveChanges();
                return noteEntity.PinNote;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool ArchiveNote(int userId, int noteId)
        {
            try
            {
                NoteEntity noteEntity = new NoteEntity();
                noteEntity = fundooDBContext.Notes.Where(x => x.NoteId == noteId && x.UserId == userId).FirstOrDefault();
                if (noteEntity.Archive)
                {
                    noteEntity.Archive = false;
                }
                else
                {
                    noteEntity.Archive = true;
                }

                fundooDBContext.SaveChanges();
                return noteEntity.Archive;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string ChangeBackgroundColor(int userId, int noteId, string backgroundColor)
        {
            try
            {
                NoteEntity noteEntity = new NoteEntity();
                noteEntity = fundooDBContext.Notes.Where(x => x.NoteId == noteId && x.UserId == userId).FirstOrDefault();
                if (noteEntity != null)
                {
                    noteEntity.BackgroundColor = backgroundColor;
                    fundooDBContext.SaveChanges();
                    return noteEntity.BackgroundColor;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DateTime NoteReminder(int userId, int noteId, DateTime reminder)
        {
            try
            {
                NoteEntity noteEntity = new NoteEntity();
                noteEntity = fundooDBContext.Notes.Where(x => x.NoteId == noteId && x.UserId == userId).FirstOrDefault();
                if (noteEntity != null)
                {
                    noteEntity.RemindMe = reminder;
                    fundooDBContext.SaveChanges();
                    return noteEntity.RemindMe;
                }
                return noteEntity.RemindMe;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /*public ImageUploadResult UploadImage(IFormFile imagePath)
        {
            try
            {
                Account account = new Account(configuration["Cloudinary:CloudName"], configuration["Cloudinary: ApiKey"], configuration["Cloudinry: ApiSecret"]);
                Cloudinary cloud = new Cloudinary(account);
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(imagePath.FileName, imagePath.OpenReadStream()),
                };
                var uploadImageRes = cloud.Upload(uploadParams);
                if (uploadImageRes != null)
                {
                    return uploadImageRes;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }*/

        public string UploadImage(int userId, int noteId, string filePath)
        {
            try
            {
                var filterUser = fundooDBContext.Notes.Where(e => e.UserId == userId);
                if (filterUser != null)
                {
                    var findNotes = filterUser.FirstOrDefault(e => e.NoteId == noteId);
                    if (findNotes != null)
                    {
                        Account account = new Account("dcnxgsczj", "911443645667983", "_VbI4OwPaGXZ8BMm8eTrwDnBqsU");
                        Cloudinary cloudinary = new Cloudinary(account);
                        ImageUploadParams uploadParams = new ImageUploadParams()
                        {
                            File = new FileDescription(filePath),
                            PublicId = findNotes.Title
                        };

                        ImageUploadResult uploadResult = cloudinary.Upload(uploadParams);

                        findNotes.UpdatedAt = DateTime.Now;
                        findNotes.AddImage = uploadResult.Url.ToString();
                        fundooDBContext.SaveChanges();
                        return "Upload Successfull";
                    }
                    return null;
                }
                else { return null; }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<NoteEntity> GetAllNotesInTable()
        {
            try
            {
                List<NoteEntity> notes = new List<NoteEntity>();
                notes = fundooDBContext.Notes.ToList();
                if (notes != null)
                {
                    return notes;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}  

