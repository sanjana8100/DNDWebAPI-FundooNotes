using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;

namespace RepositoryLayer.Entity
{
    public class CollabEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CollabId { get; set; }
        public string CollabEmail { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        [ForeignKey("Users")]
        public int UserId { get; set; }

        [JsonIgnore]
        public virtual UserEntity Users { get; set; }

        [ForeignKey("Notes")]
        public int NoteId { get; set; }

        [JsonIgnore]
        public virtual NoteEntity Notes { get; set; }
    }
}
