﻿namespace LoginPage.Models
{
    public class PeopleDataBaseSettings : IPeopleDataBaseSettings
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
    public interface IPeopleDataBaseSettings
    {
        string? Name { get; set; }
        string? Email { get; set; }
        public string? Password { get; set; }
    }
}
