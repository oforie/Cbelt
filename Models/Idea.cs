using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace beltretake.Models
{
    public class Idea
    {
        public int IdeaId {get; set;}
        public int UserId {get; set;}
        public User User {get; set;}

        public string Content {get; set;}

        public List<Like> Likes {get; set;}

        public DateTime CreatedAt {get; set;}
        public DateTime UpdatedAt {get; set;}

        public Idea()
        {
            Likes = new List<Like>();
        }
    }
}