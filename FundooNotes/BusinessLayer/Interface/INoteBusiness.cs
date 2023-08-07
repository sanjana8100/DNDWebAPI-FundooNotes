using CommonLayer.RequestModels;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;

namespace BusinessLayer.Interface
{
    public interface INoteBusiness
    {
        public NoteEntity TakeANote(NotesModel notesModel, int userId);
        public List<NoteEntity> GetAllNotes(int userId);
        public NoteEntity GetNote(int userId, int noteId);
        public NoteEntity UpdateNote(int userId, int noteId, NotesModel notesModel);
        public NoteEntity DeleteNote(int userId, int noteId);
        public bool TrashNote(int userId, int noteId);
        public bool ChangePinNote(int userId, int noteId);
        public bool ArchiveNote(int userId, int noteId);
        public string ChangeBackgroundColor(int userId, int noteId, string backgroundColor);
        public DateTime NoteReminder(int userId, int noteId, DateTime reminder);
    }
}