using RepositoryLayer.Entity;
using System.Collections.Generic;

namespace RepositoryLayer.Interface
{
    public interface ILabelRepo
    {
        public LabelEntity AddLabel(int userId, int noteId, string labelName);
        public List<LabelEntity> GetAllLabels(int userId);
        public List<LabelEntity> GetAllLabelsByNote(int userId, int noteId);
        public LabelEntity GetLabel(int userId, int labelId);
        public List<NoteEntity> GetAllNotesByLabels(int userId, int labelId);
        public NoteEntity GetNoteByLabels(int userId, int noteId, int labelId);
        public LabelEntity RemoveLabel(int userId, int noteId, int labelId);
    }
}