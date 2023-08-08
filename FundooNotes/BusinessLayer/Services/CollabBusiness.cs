using BusinessLayer.Interface;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class CollabBusiness : ICollabBusiness
    {
        public readonly ICollabRepo icollabRepo;

        public CollabBusiness(ICollabRepo icollabRepo)
        {
            this.icollabRepo = icollabRepo;
        }

        public CollabEntity AddCollab(int userId, int noteId, string collabEmail)
        {
            return icollabRepo.AddCollab(userId, noteId, collabEmail);
        }
        public List<CollabEntity> GetAllCollabs(int userId)
        {
            return icollabRepo.GetAllCollabs(userId);
        }
        public List<NoteEntity> GetAllNotesByCollab(int userId, int collabId)
        {
            return icollabRepo.GetAllNotesByCollab(userId, collabId);
        }
        public CollabEntity GetCollab(int userId, int collabId)
        {
            return icollabRepo.GetCollab(userId, collabId);
        }
        public NoteEntity GetNoteByCollab(int userId, int noteId, int collabId)
        {
            return icollabRepo.GetNoteByCollab(userId, noteId, collabId);
        }
        public CollabEntity RemoveCollab(int userId, int noteId, int collabId)
        {
            return icollabRepo.RemoveCollab(userId, noteId, collabId);
        }
    }
}
