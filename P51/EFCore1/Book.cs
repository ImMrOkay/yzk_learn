using System;

namespace EFCore1
{
    /// <summary>
    /// 1.建立实体类
    /// </summary>
    public class Book
    {
        public long Id { get; set; }

        public string Title { get; set; }

        public DateTime PubTime { get; set; }

        public double Price { get; set; }

        public string AuthorName { get; set; }
    }
}
