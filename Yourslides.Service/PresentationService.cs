using System.Collections.Generic;
using Yourslides.Data.Infrastructure;
using Yourslides.Data.Repositories;
using Yourslides.Model;
using Yourslides.Utils.DateTimeUtils;

namespace Yourslides.Service {
    public interface IPresentationService : IService {
        Presentation Create(string title, string ownerId);
        void Delete(Presentation presentation);
        Presentation Get(long id);
        IEnumerable<Presentation> GetPublicPresentationsSubset(int count, int offset);
        void UpdateAfterConvertation(Presentation presentation);
    }
    public class PresentationService : ServiceBase, IPresentationService {
        private readonly IPresentationRepository _presentationRepository;
        public PresentationService(IUnitOfWork unitOfWork, IPresentationRepository presentationRepository)
            : base(unitOfWork) {
            _presentationRepository = presentationRepository;
        }

        public Presentation Create(string title, string ownerId) {
            var presentation = new Presentation {
                Title = title,
                OwnerId = ownerId,
                Created = UnixTime.Now,
                Visibility = PresentationVisibility.Processing,
                LogoSlideIndex = 1,
                CommentEnable = true,
                Color = "#FFFFFF",
            };
            _presentationRepository.Add(presentation);
            return presentation;
        }

        public void Delete(Presentation presentation) {
            _presentationRepository.Delete(presentation);
        }

        public Presentation Get(long id) {
            return _presentationRepository.GetPresentationWithComments(id);
        }

        public IEnumerable<Presentation> GetPublicPresentationsSubset(int count, int offset) {
            return _presentationRepository.GetSubset(p => p.Visibility == PresentationVisibility.All,count, offset, false);
        }

        public void UpdateAfterConvertation(Presentation presentation) {
            _presentationRepository.Update(presentation, p => p.Visibility, p => p.SlideCount);
        }
    }
}
