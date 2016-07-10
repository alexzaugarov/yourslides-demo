using System;
using System.Collections.Generic;
using System.Linq;
using Yourslides.Data.Infrastructure;
using Yourslides.Data.Repositories;
using Yourslides.Model;
using Yourslides.Model.SelectionOptions;

namespace Yourslides.Service {
    public interface IPresentationService : IService {
        Presentation Create(string title, string ownerId);
        void Delete(Presentation presentation);
        bool Delete(long id, string currentUserId);
        Presentation Get(long id, string currentUserId, Permission permission = Permission.Low);
        IEnumerable<Presentation> Get(PresentationSelectionOptions options, string currentUserId = null);
        bool UpdateAfterConvertation(Presentation presentation);
        bool UpdatePresentationStatus(Presentation presentation);
        IEnumerable<Presentation> GetRelatedPresentations(Presentation presentation);
        IDictionary<PresentationVisibility, int> GetVisibilityStats(string ownerId); 
        IDictionary<PresentationStatus, int> GetStatusStats(string ownerId);
        bool Update(Presentation presentation);
        Presentation GetPresentationForWatch(long id, string visitorId);
    }
    public class PresentationService : ServiceBase, IPresentationService {
        private readonly IPresentationRepository _presentationRepository;
        private readonly IPresentationWatchRepository _presentationWatchRepository;

        public PresentationService(IUnitOfWork unitOfWork, IPresentationRepository presentationRepository, IPresentationWatchRepository presentationWatchRepository)
            : base(unitOfWork) {
            _presentationRepository = presentationRepository;
            _presentationWatchRepository = presentationWatchRepository;
        }

        public Presentation Create(string title, string ownerId) {
            var presentation = new Presentation {
                Title = title,
                OwnerId = ownerId,
                CreatedDateTime = DateTime.UtcNow,
                Visibility = PresentationVisibility.All,
                LogoSlideIndex = 1,
                CommentEnable = true,
                ScreenBackgroundColor = "#FFFFFF",
                Status = PresentationStatus.Queue
            };
            _presentationRepository.Add(presentation);
            return presentation;
        }

        public void Delete(Presentation presentation) {
            _presentationRepository.Delete(presentation);
        }

        public bool Delete(long id, string currentUserId) {
            var presentation = Get(id, currentUserId, Permission.High);
            if (presentation != null) {
                Delete(presentation);
                return true;
            }
            return false;
        }

        public Presentation Get(long id, string currentsUserId, Permission permission = Permission.Low) {
            var result = _presentationRepository.GetById(id);
            if (result != null &&
                !String.Equals(currentsUserId, result.OwnerId) &&
                (result.Visibility == PresentationVisibility.Hide ||
                result.Status != PresentationStatus.Ready ||
                permission == Permission.High)) {
                return null;
            }
            return result;
        }

        public IEnumerable<Presentation> Get(PresentationSelectionOptions options, string currentUserId = null) {
            if (!String.IsNullOrEmpty(currentUserId) && !String.IsNullOrEmpty(options.OwnerId) && String.Equals(currentUserId, options.OwnerId)) {
                return _presentationRepository.GetMany(options);
            }
            options.Visibility = PresentationVisibility.All;
            options.Status = PresentationStatus.Ready;
            return _presentationRepository.GetMany(options);
        }

        public IEnumerable<Presentation> GetRelatedPresentations(Presentation presentation) {
            if (presentation == null) {
                return new List<Presentation>();
            }
            return _presentationRepository.GetMany(new PresentationSelectionOptions {
                Count = 6,
                SortColumn = SortColumn.WatchCount,
                SortType = SortType.Descending,
                OwnerId = presentation.OwnerId,
                Visibility = PresentationVisibility.All,
                Status = PresentationStatus.Ready
            }).Where(r => r.Id != presentation.Id).Take(5);
        }

        public IDictionary<PresentationVisibility, int> GetVisibilityStats(string ownerId) {
            return _presentationRepository.GroupByVisibility(p => p.OwnerId == ownerId);
        }

        public IDictionary<PresentationStatus, int> GetStatusStats(string ownerId) {
            return _presentationRepository.GroupByStatus(p => p.OwnerId == ownerId);
        }

        public bool Update(Presentation presentation) {
            var p = Get(presentation.Id, presentation.OwnerId, Permission.High);
            if (p != null) {
                p.Title = presentation.Title;
                p.Description = presentation.Description;
                p.CommentEnable = presentation.CommentEnable;
                p.LogoSlideIndex = presentation.LogoSlideIndex;
                p.ScreenBackgroundColor = presentation.ScreenBackgroundColor;
                p.Visibility = presentation.Visibility;
                return true;
            }
            return false;
        }

        public Presentation GetPresentationForWatch(long id, string visitorId) {
            var presentation = Get(id, visitorId);
            if (presentation != null) {
                var isNewWatch = _presentationWatchRepository.TryAdd(new PresentationWatch{
                    PresentationId = presentation.Id, 
                    VisitorId = visitorId,
                    WatchDateTime = DateTime.UtcNow
                });
                if (isNewWatch) {
                    presentation.WatchCount += 1;
                }
            }
            return presentation;
        }

        public bool UpdateAfterConvertation(Presentation presentation) {
            var p = _presentationRepository.GetById(presentation.Id);
            if (p != null) {
                p.SlideCount = presentation.SlideCount;
                p.Status = presentation.Status;
                return true;
            }
            return false;
        }

        public bool UpdateAfterEdit(Presentation presentation) {
            var p = _presentationRepository.GetById(presentation.Id);
            if (p != null) {
                p.Title = presentation.Title;
                p.Description = presentation.Description;
                p.CommentEnable = presentation.CommentEnable;
                p.LogoSlideIndex = presentation.LogoSlideIndex;
                p.ScreenBackgroundColor = presentation.ScreenBackgroundColor;
                p.Visibility = presentation.Visibility;
                return true;
            }
            return false;
        }
        public bool UpdatePresentationStatus(Presentation presentation) {
            var p = _presentationRepository.GetById(presentation.Id);
            if (p != null) {
                p.Status = presentation.Status;
                return true;
            }
            return false;
        }
    }
}
