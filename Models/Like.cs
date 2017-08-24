using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;


namespace beltretake.Models
{
    public class Like
    {
        public int LikeId {get; set;}
        public int IdeaId {get; set;}
        public Idea Idea {get; set;}

        public int UserId {get; set;}
        public User User {get; set;}
        public DateTime CreatedAt {get; set;}
        public DateTime UpdatedAt {get; set;}

        public Like(){}
    }
}