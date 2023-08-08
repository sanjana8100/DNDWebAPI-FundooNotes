using Microsoft.EntityFrameworkCore.Internal;
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
    public class CollabRepo : ICollabRepo
    {
        private readonly FundooDBContext fundooDBContext;
        private readonly IConfiguration configuration;

        public CollabRepo(FundooDBContext fundooDBContext, IConfiguration configuration)
        {
            this.fundooDBContext = fundooDBContext;
            this.configuration = configuration;
        }

        public CollabEntity AddCollab(int userId, int noteId, string collabEmail)
        {
            try
            {
                CollabEntity collabEntity = new CollabEntity();

                if (fundooDBContext.Notes.Any(x => x.NoteId == noteId && x.UserId == userId))
                {
                    if (fundooDBContext.Users.Any(x => x.Email == collabEmail))
                    {
                        collabEntity.UserId = userId;
                        collabEntity.NoteId = noteId;

                        collabEntity.CollabEmail = collabEmail;
                        collabEntity.CreatedAt = DateTime.Now;
                        collabEntity.UpdatedAt = DateTime.Now;

                        fundooDBContext.Collaborations.Add(collabEntity);
                        fundooDBContext.SaveChanges();

                        return collabEntity;
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CollabEntity> GetAllCollabs(int userId)
        {
            try
            {
                List<CollabEntity> collabs = new List<CollabEntity>();
                collabs = fundooDBContext.Collaborations.Where(x => x.UserId == userId).ToList();
                if (collabs != null)
                {
                    return collabs;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CollabEntity GetCollab(int userId, int collabId)
        {
            try
            {
                CollabEntity collabEntity = new CollabEntity();
                collabEntity = fundooDBContext.Collaborations.Where(x => x.UserId == userId && x.CollabId == collabId).FirstOrDefault();

                if (collabEntity != null)
                {
                    return collabEntity;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<NoteEntity> GetAllNotesByCollab(int userId, int collabId)
        {
            try
            {
                List<NoteEntity> collabNotes = fundooDBContext.Collaborations.Where(c => c.UserId == userId && c.CollabId == collabId).Join(fundooDBContext.Notes, x => x.NoteId, y => y.NoteId, (x, y) => y).ToList();
                if (collabNotes != null)
                {
                    return collabNotes;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public NoteEntity GetNoteByCollab(int userId, int noteId, int collabId)
        {
            try
            {
                NoteEntity collabNoteEntity = fundooDBContext.Collaborations.Where(c => c.CollabId == collabId && c.UserId == userId).Join(fundooDBContext.Notes, x => x.NoteId, y => y.NoteId, (x, y) => y).FirstOrDefault();
                if (collabNoteEntity != null)
                {
                    return collabNoteEntity;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CollabEntity RemoveCollab(int userId, int noteId, int collabId)
        {
            try
            {
                CollabEntity collabEntity = new CollabEntity();
                collabEntity = fundooDBContext.Collaborations.Where(x => x.UserId == userId && x.NoteId == noteId && x.CollabId == collabId).FirstOrDefault();
                if (collabEntity != null)
                {
                    fundooDBContext.Collaborations.Remove(collabEntity);
                    fundooDBContext.SaveChanges();
                    return collabEntity;
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
