using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Yourslides.Data.Infrastructure;
using Yourslides.Model;
using Yourslides.Utils.DateTimeUtils;

namespace Yourslides.Data.Repositories {
    public interface IPresentationRepository : IRepository<Presentation> {
        Presentation GetPresentationWithComments(long id);
        IEnumerable<Presentation> GetSubset(int count, int offset, bool sortAscending);
        IEnumerable<Presentation> GetSubset(Expression<Func<Presentation, bool>> where, int count, int offset, bool sortAscending);
        
    }
    public class PresentationRepository : RepositoryBase<Presentation>, IPresentationRepository {
        public PresentationRepository(IDbFactory dbFactory)
            : base(dbFactory) {
        }

        public Presentation GetPresentationWithComments(long id) {
            return DbContext.Presentations.Where(p => p.Id == id).Include(p=>p.Owner).FirstOrDefault();
                /*.Select(p=>new {
                    Presentation = p,
                    Comments = p.Comments
                });*/
        }

        public IEnumerable<Presentation> GetSubset(int count, int offset, bool sortAscending) {
            return GetSubset(null, count, offset, sortAscending);
        }

        public IEnumerable<Presentation> GetSubset(Expression<Func<Presentation, bool>> @where, int count, int offset, bool sortAscending) {
            var result = @where != null ? DbContext.Presentations.Where(@where) : DbContext.Presentations;
            result = sortAscending ? result.OrderBy(p => p.Created) : result.OrderByDescending(p => p.Created);
            return result.Include(p => p.Owner).Skip(offset).Take(count).ToList();
        }
    }
}