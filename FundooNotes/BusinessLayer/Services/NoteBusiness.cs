using BusinessLayer.Interface;
using CommonLayer.RequestModels;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class NoteBusiness : INoteBusiness
    {
        private readonly INoteRepo inoteRepo;

        public NoteBusiness(INoteRepo inoteRepo)
        {
            this.inoteRepo = inoteRepo;
        }

        public NoteEntity TakeANote(NotesModel notesModel, int userId)
        {
            return inoteRepo.TakeANote(notesModel, userId);
        }

        public List<NoteEntity> GetAllNotes(int userId)
        {
            return inoteRepo.GetAllNotes(userId);
        }

        public NoteEntity GetNote(int userId, int noteId)
        {
            return inoteRepo.GetNote(userId, noteId);
        }

        public NoteEntity UpdateNote(int userId, int noteId, NotesModel notesModel)
        {
            return inoteRepo.UpdateNote(userId, noteId, notesModel);
        }

        public NoteEntity DeleteNote(int userId, int noteId)
        {
            return inoteRepo.DeleteNote(userId, noteId);
        }

        public bool TrashNote(int noteId)
        {
            return inoteRepo.TrashNote(noteId);
        }

        public bool ChangePinNote(int noteId)
        {
            return inoteRepo.ChangePinNote(noteId);
        }

        public bool ArchiveNote(int noteId)
        {
            return inoteRepo.ArchiveNote(noteId);
        }
    }
}
