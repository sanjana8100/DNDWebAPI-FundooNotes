using CommonLayer.Models;
using CommonLayer.RequestModels;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using RepositoryLayer.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public bool TrashNote(int noteId)
        {
            try
            {
                NoteEntity noteEntity = new NoteEntity();
                noteEntity = fundooDBContext.Notes.Where(x => x.NoteId == noteId).FirstOrDefault();
                if (noteEntity.Trash)
                {
                    noteEntity.Trash = false;
                    return noteEntity.Trash;
                }
                noteEntity.Trash = true;
                return noteEntity.Trash;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool ChangePinNote(int noteId)
        {
            try
            {
                NoteEntity noteEntity = new NoteEntity();
                noteEntity = fundooDBContext.Notes.Where(x => x.NoteId == noteId).FirstOrDefault();
                if (noteEntity.PinNote)
                {
                    noteEntity.PinNote = false;
                    noteEntity.UnPinNote = true;
                    return noteEntity.PinNote;
                }
                noteEntity.PinNote = true;
                noteEntity.UnPinNote = false;
                return noteEntity.PinNote;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool ArchiveNote(int noteId)
        {
            try
            {
                NoteEntity noteEntity = new NoteEntity();
                noteEntity = fundooDBContext.Notes.Where(x => x.NoteId == noteId).FirstOrDefault();
                if (noteEntity.Archive)
                {
                    noteEntity.Archive = false;
                    return noteEntity.Archive;
                }
                noteEntity.Archive = true;
                return noteEntity.Archive;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
