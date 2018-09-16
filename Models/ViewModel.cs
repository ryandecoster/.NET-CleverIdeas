using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CleverIdeas.Models
{
    public class ViewModel
    {
        public User Users {get; set;}
        public RegisterUser RegisterUser {get; set;}
        public LoginUser LoginUser {get; set;}
        public Idea Ideas {get; set;}
        public Like Likes {get; set;}
        public List<Idea> allIdeas { get; set; }
        public List<Like> allLikes { get; set; }
    }
}