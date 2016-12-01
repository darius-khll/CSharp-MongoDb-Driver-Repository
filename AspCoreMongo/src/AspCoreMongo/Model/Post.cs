using MongoDB.Bson.Serialization.Attributes;
using System;

namespace AspCoreMongo.Model
{
    public class Post
    {
        [BsonId]
        public string Id { get; set; }

        public string Content { get; set; }

        public DateTimeOffset CreatedOn { get; set; } = DateTimeOffset.UtcNow;

        public DateTimeOffset UpdatedOn { get; set; } = DateTimeOffset.UtcNow;

        public int UserId { get; set; } = 0;
    }
}
