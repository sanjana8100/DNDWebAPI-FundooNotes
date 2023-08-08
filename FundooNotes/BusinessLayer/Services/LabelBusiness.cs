using BusinessLayer.Interface;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class LabelBusiness : ILabelBusiness
    {
        private readonly ILabelRepo ilabelRepo;

        public LabelBusiness(ILabelRepo ilabelRepo)
        {
            this.ilabelRepo = ilabelRepo;
        }
        public List<LabelEntity> GetAllLabels(int userId)
        {
            return ilabelRepo.GetAllLabels(userId);
        }
        public LabelEntity GetLabel(int userId, int labelId)
        {
            return ilabelRepo.GetLabel(userId, labelId);
        }
        public LabelEntity AddLabel(int userId, int noteId, string labelName)
        {
            return ilabelRepo.AddLabel(userId, noteId, labelName);
        }
        public List<NoteEntity> GetAllNotesByLabels(int userId, int labelId)
        {
            return ilabelRepo.GetAllNotesByLabels(userId, labelId);
        }
        public NoteEntity GetNoteByLabels(int userId, int noteId, int labelId)
        {
            return ilabelRepo.GetNoteByLabels(userId, noteId, labelId);
        }
        public LabelEntity RemoveLabel(int userId, int noteId, int labelId)
        {
            return ilabelRepo.RemoveLabel(userId, noteId, labelId);
        }
    }
}
