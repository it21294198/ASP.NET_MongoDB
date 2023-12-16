using System;
using MongoDB.Bson.Serialization.Attributes;

namespace ASP.NET_MongoDB.Models
{
	public class User
	{
        public int Id { get; set; }

        public String Name { get; set; }

        public String Email { get; set; }

        public User(int id, string name, string email)
        {
            Id = id;
            Name = name;
            Email = email;
        }

        public User()
		{
		}
	}
}

