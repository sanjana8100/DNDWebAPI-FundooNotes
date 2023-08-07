using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json.Serialization;

namespace RepositoryLayer.Entity
{
    public class NoteEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int NoteId { get; set; }
        public string Title { get; set; }
        public string Note { get; set; }
        public DateTime RemindMe { get; set; }
        public string BackgroundColor { get; set; }
        public string AddImage { get; set; }
        public bool Archive { get; set; }
        public bool PinNote { get; set; }
        public bool UnPinNote { get; set; }
        public bool Trash { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        [ForeignKey("Users")]
        public int UserId { get; set; }

        [JsonIgnore]
        public virtual UserEntity Users { get; set; }
    }
}
