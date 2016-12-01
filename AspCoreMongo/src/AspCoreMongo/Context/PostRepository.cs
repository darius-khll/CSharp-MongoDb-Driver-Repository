using AspCoreMongo.Model;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspCoreMongo.Context
{
    public class PostRepository : IPostRepository
    {
        private readonly MongoContext _context = null;

        public PostRepository(IOptions<Settings> settings)
        {
            _context = new MongoContext(settings);
        }

        public async Task<IEnumerable<Post>> GetAllPosts()
        {
            var documents = await _context.Posts.Find(_ => true)
                                .ToListAsync();
            return documents;
        }

        public async Task<Post> GetPost(string id)
        {
            var filter = Builders<Post>.Filter.Eq("Id", id);
            var document = await _context.Posts.Find(filter).FirstOrDefaultAsync();
            return document;
        }

        public async void AddPost(Post item)
        {
            await _context.Posts.InsertOneAsync(item);
        }

        public async Task<bool> RemovePost(string id)
        {
            var result = await _context.Posts.DeleteOneAsync(
                          Builders<Post>.Filter.Eq("Id", id));

            return result.DeletedCount > 0;
        }

        public async void UpdatePost(string id, string content)
        {
            var filter = Builders<Post>.Filter.Eq(s => s.Id, id);
            var update = Builders<Post>.Update
                                .Set(s => s.Content, content)
                                .CurrentDate(s => s.UpdatedOn);
            var result = await _context.Posts.UpdateOneAsync(filter, update);
        }

        public void RemoveAllPosts()
        {
            _context.Posts.DeleteManyAsync(new BsonDocument());
        }
    }
}
