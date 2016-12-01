using AspCoreMongo.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspCoreMongo.Context
{
    public interface IPostRepository
    {
        Task<IEnumerable<Post>> GetAllPosts();
        Task<Post> GetPost(string id);
        void AddPost(Post item);
        Task<bool> RemovePost(string id);
        void UpdatePost(string id, string body);

        // it should be cautiously used (only in relation with development tests)
        void RemoveAllPosts();
    }
}
