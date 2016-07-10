using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Yourslides.Data.Infrastructure;
using Yourslides.Model;
using Yourslides.Model.SelectionOptions;

namespace Yourslides.Data.Repositories {
    public interface IPresentationRepository : IRepository<Presentation> {
        IEnumerable<Presentation> GetMany(PresentationSelectionOptions options);
        IDictionary<PresentationVisibility, int> GroupByVisibility(Expression<Func<Presentation, bool>> where = null);
        IDictionary<PresentationStatus, int> GroupByStatus(Expression<Func<Presentation, bool>> where);
    }
    public class PresentationRepository : RepositoryBase<Presentation>, IPresentationRepository {
        public PresentationRepository(IDbFactory dbFactory)
            : base(dbFactory) {
        }

        public override Presentation GetById(long id) {
            return DbContext.Presentations.Where(p => p.Id == id)
                .Include(p => p.Owner)
                .FirstOrDefault();
        }

        public IEnumerable<Presentation> GetMany(PresentationSelectionOptions options) {
            IQueryable<Presentation> result = DbContext.Presentations;
            if (!String.IsNullOrEmpty(options.SearchString)) {
                result = result.Where(p => p.Title.Contains(options.SearchString) || p.Description.Contains(options.SearchString));
            }
            if (!String.IsNullOrEmpty(options.OwnerId)) {
                result = result.Where(p => p.OwnerId.Equals(options.OwnerId));
            }
            if (options.Visibility != PresentationVisibility.None) {
                result = result.Where(p => p.Visibility == options.Visibility);
            }
            if (options.Status != PresentationStatus.None) {
                result = result.Where(p => p.Status == options.Status);
            }
            return result.OrderByField(options.SortProperty, options.SortType).Include(p => p.Owner).Skip(options.Offset).Take(options.Count).ToList();
        }

        public IDictionary<PresentationVisibility, int> GroupByVisibility(Expression<Func<Presentation, bool>> @where = null) {
            IQueryable<Presentation> result = @where == null ? DbContext.Presentations : DbContext.Presentations.Where(@where);
            return result.GroupBy(p => p.Visibility).Select(g => new { Visibility = g.Key, Count = g.Count() }).ToDictionary(r => r.Visibility, r => r.Count);
        }

        public IDictionary<PresentationStatus, int> GroupByStatus(Expression<Func<Presentation, bool>> @where) {
            IQueryable<Presentation> result = @where == null ? DbContext.Presentations : DbContext.Presentations.Where(@where);
            return result.GroupBy(p => p.Status).Select(g => new { Status = g.Key, Count = g.Count() }).ToDictionary(r => r.Status, r => r.Count);
        }
    }
}