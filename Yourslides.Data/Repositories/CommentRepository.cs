using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Yourslides.Data.Infrastructure;
using Yourslides.Model;
using Yourslides.Model.SelectionOptions;

namespace Yourslides.Data.Repositories {
    public interface ICommentRepository : IRepository<Comment> {
        IEnumerable<Comment> GetMany(CommentsSelectionOptions options);
    }
    public class CommentRepository : RepositoryBase<Comment>, ICommentRepository {
        public CommentRepository(IDbFactory dbFactory) : base(dbFactory) {
        }

        public override Comment GetById(long id) {
            return base.GetMany(c => c.Id == id).Include(c => c.Owner).FirstOrDefault();
        }

        public IEnumerable<Comment> GetMany(CommentsSelectionOptions options) {
            var result = base.GetMany(c => c.PresentationId == options.PresentationId && c.Presentation.CommentEnable);
            result = options.SortAscending ? result.OrderBy(c => c.CreatedDateTime) : result.OrderByDescending(c => c.CreatedDateTime);
            return result.Include(c => c.Owner).Skip(options.Offset).Take(options.Count).ToList();
        }
    }
}