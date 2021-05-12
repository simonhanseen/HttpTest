﻿using System;
using System.Collections.Generic;
using System.Text;

namespace HttpTest.Data
{
    public class Book
    {
        public int id { get; set; }
        public string Title { get; set; }
        public string ISBN { get; set; }
        public string Description { get; set; }
        public DateTime published_at { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public object Image { get; set; }

    }
}
