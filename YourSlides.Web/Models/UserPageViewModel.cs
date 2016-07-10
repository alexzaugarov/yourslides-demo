using System.Collections.Generic;
using Yourslides.Model.Account;
using Yourslides.Model.Api;
using Yourslides.Model.SelectionOptions;

namespace YourSlides.Web.Models {
    public class UserPageViewModel : MainPageModel {
        public User Owner { get; set; }
    }
}