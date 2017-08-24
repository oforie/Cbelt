using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace beltretake.Models
{
    public class User
    {
        public int UserId {get; set;}
        public string Name {get; set;}
        public string Alias {get; set;}
        public string Email {get; set;}
        public string Password {get; set;}
        public List<Like> Likes {get; set;}

        public List<Idea> Ideas {get; set;}
      
        public DateTime CreatedAt {get; set;}
        public DateTime UpdatedAt {get; set;}

        public User()
        {
            Ideas = new List<Idea>();
            Likes = new List<Like>();
        }
        
    }
}