using RepositoryLayer.Entity;
using System.Collections.Generic;

namespace BusinessLayer.Interface
{
    public interface ICollabBusiness
    {
        public CollabEntity AddCollab(int userId, int noteId, string collabEmail);
        public List<CollabEntity> GetAllCollabs(int userId);
        public List<NoteEntity> GetAllNotesByCollab(int userId, int collabId);
        public CollabEntity GetCollab(int userId, int collabId);
        public NoteEntity GetNoteByCollab(int userId, int noteId, int collabId);
        public CollabEntity RemoveCollab(int userId, int noteId, int collabId);
    }
}