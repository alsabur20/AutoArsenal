﻿namespace AutoArsenal_App.Models
{
    public class Manufacturer
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Contact { get; set; }
        public string StreetAddress { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public bool IsDeleted { get; set; }
    }
}
