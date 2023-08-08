using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.UserSecrets;
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
    public class LabelRepo : ILabelRepo
    {
        private readonly FundooDBContext fundooDBContext;
        private readonly IConfiguration configuration;

        public LabelRepo(FundooDBContext fundooDBContext, IConfiguration configuration)
        {
            this.fundooDBContext = fundooDBContext;
            this.configuration = configuration;
        }

        public LabelEntity AddLabel(int userId, int noteId, string labelName)
        {
            try
            {
                LabelEntity labelEntity = new LabelEntity();

                if (fundooDBContext.Notes.Any(x => x.NoteId == noteId && x.UserId == userId))
                {
                    labelEntity.UserId = userId;
                    labelEntity.NoteId = noteId;

                    labelEntity.LabelName = labelName;
                    labelEntity.CreatedAt = DateTime.Now;
                    labelEntity.UpdatedAt = DateTime.Now;

                    fundooDBContext.Labels.Add(labelEntity);
                    fundooDBContext.SaveChanges();

                    return labelEntity;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<LabelEntity> GetAllLabels(int userId)
        {
            try
            {
                List<LabelEntity> labels = new List<LabelEntity>();
                labels = fundooDBContext.Labels.Where(x => x.UserId == userId).ToList();
                if (labels != null)
                {
                    return labels;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public LabelEntity GetLabel(int userId, int labelId)
        {
            try
            {
                LabelEntity labelEntity = new LabelEntity();
                labelEntity = fundooDBContext.Labels.Where(x => x.UserId == userId && x.LabelId == labelId).FirstOrDefault();

                if (labelEntity != null)
                {
                    return labelEntity;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<NoteEntity> GetAllNotesByLabels(int userId, int labelId)
        {
            try
            {
                List<NoteEntity> labelNotes = fundooDBContext.Labels.Where(c => c.UserId == userId && c.LabelId == labelId).Join(fundooDBContext.Notes, x => x.NoteId, y => y.NoteId, (x, y) => y).ToList();
                if (labelNotes != null)
                {
                    return labelNotes;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public NoteEntity GetNoteByLabels(int userId, int noteId, int labelId)
        {
            try
            {
                NoteEntity labelNoteEntity = fundooDBContext.Labels.Where(c => c.LabelId == labelId && c.UserId == userId).Join(fundooDBContext.Notes, x => x.NoteId, y => y.NoteId, (x, y) => y).FirstOrDefault();
                if (labelNoteEntity != null)
                {
                    return labelNoteEntity;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public LabelEntity RemoveLabel(int userId, int noteId, int labelId)
        {
            try
            {
                LabelEntity labelEntity = new LabelEntity();
                labelEntity = fundooDBContext.Labels.Where(x => x.UserId == userId && x.NoteId == noteId && x.LabelId == labelId).FirstOrDefault();
                if (labelEntity != null)
                {
                    fundooDBContext.Labels.Remove(labelEntity);
                    fundooDBContext.SaveChanges();
                    return labelEntity;
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
