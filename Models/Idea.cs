using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace CleverIdeas.Models
{
    public class Idea
    {
        [Key]
        public int IdeaId { get; set; }

        [Required(ErrorMessage = "Idea field must not be empty.")]
        [MinLength(5, ErrorMessage = "Ideas be at least 5 characters.")]
        public string Content { get; set; }
        public DateTime Created_At { get; set;}
        public DateTime Updated_At { get; set;}
        public int UserId { get; set; }
        public User Creator { get; set; }
        public List<Like> LikesList { get; set; }
        public Idea()
        {
            Created_At = DateTime.Now;
            Updated_At = DateTime.Now;
            LikesList = new List<Like>();
        }
    }
}