using System.Collections.Generic;
using System.Web.Http;
using AutoMapper;
using Microsoft.AspNet.Identity;
using Yourslides.Model;
using Yourslides.Model.Api;
using Yourslides.Model.SelectionOptions;
using Yourslides.Service;

namespace YourSlides.Web.ApiControllers {
    public class CommentsController : ApiController {
        private readonly ICommentService _commentService;
        private readonly IMapper _mapper;

        public CommentsController(ICommentService commentService, IMapper mapper) {
            _commentService = commentService;
            _mapper = mapper;
        }

        // GET api/<controller>
        [HttpGet]
        [Route("api/comments/")]
        public IEnumerable<CommentApi> Get([FromUri] CommentsSelectionOptions options) {
            if (ModelState.IsValid) {
                return _mapper.Map<IEnumerable<CommentApi>>(_commentService.Get(options));
            }
            return null;
        }

        // POST api/<controller>
        [Authorize]
        [HttpPost]
        [Route("api/comments/")]
        public CommentApi Post([FromBody]CommentApi postComment) {
            if (ModelState.IsValid) {
                var commentEntity = _mapper.Map<Comment>(postComment);
                commentEntity.OwnerId = User.Identity.GetUserId();
                var result = _commentService.Add(commentEntity);
                if (result != null) {
                    _commentService.Save();
                    var resultApi = _mapper.Map<CommentApi>(result);
                    resultApi.OwnerName = User.Identity.Name;
                    return resultApi;
                }
            }
            return null;
        }

        // PUT api/<controller>/5
        [Authorize]
        [HttpPut]
        [Route("api/comments/")]
        public CommentApi Put(CommentApi comment) {
            if (ModelState.IsValid) {
                var commentEntity = _mapper.Map<Comment>(comment);
                commentEntity.OwnerId = User.Identity.GetUserId();
                var result = _commentService.Update(commentEntity);
                if (result != null) {
                    _commentService.Save();
                }
                return _mapper.Map<CommentApi>(result);
            }
            return null;
        }

        // DELETE api/<controller>/5
        [Authorize]
        [HttpDelete]
        [Route("api/comments/")]
        public bool Delete([FromBody]long id) {
            var commentEntity = new Comment {
                Id = id,
                OwnerId = User.Identity.GetUserId()
            };
            var result = _commentService.Delete(commentEntity);
            _commentService.Save();
            return result;
        }
    }
}