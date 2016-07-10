using System;
using Yourslides.Model.Account;

namespace Yourslides.Model {
    public class Comment {
        public long Id { get; set; }
        public long PresentationId { get; set; }
        public string OwnerId { get; set; }
        public string Text { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public User Owner { get; set; }
        public Presentation Presentation { get; set; }
    }
}