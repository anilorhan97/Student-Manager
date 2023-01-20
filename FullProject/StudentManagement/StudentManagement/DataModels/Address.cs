﻿using System;

namespace StudentManagement.DataModels
{
    public class Address
    {
        public Guid Id { get; set; }
        public string PhysicalAddress { get; set; }
        public string PostalAddress { get; set; }

        public Guid StudentId { get; set; } //İlişkili olacağı için Student'den yakalayacağız.
    }
}