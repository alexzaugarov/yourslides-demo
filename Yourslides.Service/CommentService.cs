using System;
using System.Collections.Generic;
using Yourslides.Data.Infrastructure;
using Yourslides.Data.Repositories;
using Yourslides.Model;
using Yourslides.Model.SelectionOptions;

namespace Yourslides.Service {
    public interface ICommentService : IService {
        Comment Add(Comment comment);
        IEnumerable<Comment> Get(CommentsSelectionOptions options);
        Comment Update(Comment comment);
        bool Delete(Comment comment);
    }
    public class CommentService : ServiceBase, ICommentService {
        private readonly ICommentRepository _commentRepository;
        private readonly IPresentationRepository _presentationRepository;
        public CommentService(IUnitOfWork unitOfWork, ICommentRepository commentRepository, IPresentationRepository presentationRepository)
            : base(unitOfWork) {
            _commentRepository = commentRepository;
            _presentationRepository = presentationRepository;
        }

        public Comment Add(Comment comment) {
            var presentation = _presentationRepository.GetById(comment.PresentationId);
            if (presentation != null && presentation.CommentEnable) {
                var newcomment = new Comment {
                    PresentationId = comment.PresentationId,
                    Text = comment.Text,
                    CreatedDateTime = DateTime.UtcNow,
                    OwnerId = comment.OwnerId
                };
                _commentRepository.Add(newcomment);
                return newcomment;
            }
            return null;
        }

        public IEnumerable<Comment> Get(CommentsSelectionOptions options) {
            return _commentRepository.GetMany(options);
        }

        public Comment Update(Comment comment) {
            var entity = _commentRepository.GetById(comment.Id);
            if (entity != null && String.Equals(entity.OwnerId, comment.OwnerId)) {
                entity.Text = comment.Text;
            }
            return entity;
        }

        public bool Delete(Comment comment) {
            var entity = _commentRepository.GetById(comment.Id);
            if (entity != null && String.Equals(entity.OwnerId,comment.OwnerId)) {
                _commentRepository.Delete(entity);
                return true;
            }
            return false;
        }
    }
}