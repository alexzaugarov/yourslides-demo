using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Web.Http;
using AutoMapper;
using Microsoft.AspNet.Identity;
using Yourslides.FileHandler.Service;
using Yourslides.Model;
using Yourslides.Model.Api;
using Yourslides.Model.SelectionOptions;
using Yourslides.Service;
using YourSlides.Web.Tools;

namespace YourSlides.Web.ApiControllers {
    public class PresentationsController : ApiController {
        private readonly IPresentationService _presentationService;
        private readonly IFileService _fileService;
        private readonly IMapper _mapper;

        public PresentationsController(IPresentationService presentationService, IMapper mapper, IFileService fileService) {
            _presentationService = presentationService;
            _mapper = mapper;
            _fileService = fileService;
        }
        [Route("api/presentations/")]
        public IEnumerable<PresentationApi> Get([FromUri]PresentationSelectionOptions options) {
            var presentations = _presentationService.Get(options, User.Identity.GetUserId());
            var response = _mapper.Map<IEnumerable<PresentationApi>>(presentations);
            return response;
        }

        // GET api/<controller>/5
        [Route("api/presentations/{id}")]
        public PresentationApi Get(string id) {
            var presentation = _presentationService.Get(Obfuscator.Deobfuscate(id), User.Identity.GetUserId());
            return _mapper.Map<PresentationApi>(presentation);
        }

        // POST api/<controller>
        public void Post([FromBody]string value) {
        }

        // PUT api/<controller>/5
        [Authorize]
        [HttpPut]
        [Route("api/presentations/")]
        public bool Put(PresentationApi presentation) {
            if (ModelState.IsValid) {
                var domainEntity = _mapper.Map<Presentation>(presentation);
                domainEntity.OwnerId = User.Identity.GetUserId();
                bool result = _presentationService.Update(domainEntity);
                _presentationService.Save();
                return result;
            }
            return false;
        }

        // DELETE api/<controller>/5
        [Authorize]
        [HttpDelete]
        [Route("api/presentations/")]
        public bool Delete([FromBody]string id) {
            try {
                var result = _presentationService.Delete(Obfuscator.Deobfuscate(id), User.Identity.GetUserId());
                if (result) {
                    _fileService.DeleteDirectory(id);
                }
                _presentationService.Save();
                return result;
            } catch (Exception e) {
                Debug.WriteLine(e.Message);
                return false;
            }
        }
    }
}