using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CommonLayer.RequestModels
{
    public class NotesModel
    {
        public string Title {  get; set; }
        public string Note {  get; set; }
        public DateTime RemindMe {  get; set; }
        public string BackgroundColor { get; set; }
        public string AddImage { get; set; }
        public bool Archive { get; set; }
        public bool PinNote { get; set; }
        public bool UnPinNote { get; set; }
        public bool Trash { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int UserId { get; set; }
    }
}
