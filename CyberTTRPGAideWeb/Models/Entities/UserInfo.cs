﻿namespace CyberTTRPGAideWeb.Models.Entities
{
    public class UserInfo
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }

        public UserInfo()
        {
            Username = string.Empty;
            Email = string.Empty;
        }
    }
}
