﻿namespace EfCoreOne2Many
{
    public class Comment
    {
        public long Id { get; set; }
        public Article Article { get; set; }
        public string Message { get; set; }
    }
}
