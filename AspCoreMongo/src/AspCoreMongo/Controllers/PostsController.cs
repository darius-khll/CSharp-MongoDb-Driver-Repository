using AspCoreMongo.Context;
using AspCoreMongo.Model;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace AspCoreMongo.Controllers
{
    [Route("api/[controller]")]
    public class PostsController : Controller
    {
        private readonly IPostRepository _PostRepository;

        public PostsController(IPostRepository PostRepository)
        {
            _PostRepository = PostRepository;
        }

        // GET: Posts/Posts
        [HttpGet]
        public Task<string> Get()
        {
            return GetPostInternal();
        }

        private async Task<string> GetPostInternal()
        {
            var Posts = await _PostRepository.GetAllPosts();
            return JsonConvert.SerializeObject(Posts);
        }

        // GET api/Posts/5
        [HttpGet("{id}")]
        public Task<string> Get(string id)
        {
            return GetPostByIdInternal(id);
        }

        private async Task<string> GetPostByIdInternal(string id)
        {
            var Post = await _PostRepository.GetPost(id) ?? new Post();
            return JsonConvert.SerializeObject(Post);
        }

        // POST api/Posts
        [HttpPost]
        public void Post([FromBody]string value)
        {
            _PostRepository.AddPost(new Post() { Content = value, CreatedOn = DateTimeOffset.UtcNow, UpdatedOn = DateTimeOffset.UtcNow });
        }

        // PUT api/Posts/5
        [HttpPut("{id}")]
        public void Put(string id, [FromBody]string value)
        {
            _PostRepository.UpdatePost(id, value);
        }

        // DELETE api/Posts/5
        public void Delete(string id)
        {
            _PostRepository.RemovePost(id);
        }
    }
}
