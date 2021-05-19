﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vibbraapi.Domain.Entities;

namespace vibbraapi.Domain.DTO
{
    public class ProjectDTO:Entity
    {
        public string Title { get; private set; }

        public string Description { get; private set; }        

        public ProjectDTO() { }

        public ProjectDTO(long id, string title, string description)
        {
            Id = id;
            Title = title;
            Description = description;
        }
    }
}
