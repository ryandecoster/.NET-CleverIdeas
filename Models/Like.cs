using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace CleverIdeas.Models
{
    public class Like
    {
        [Key]
        public int LikeId { get; set; }
        public int IdeaId { get; set; }
        public Idea Ideas { get; set; }
        public int UserId { get; set; }
        public User LikedUser { get; set; }
    }
}