namespace YourSlides.Web.Models {
    public class PageStatesLabel {
        public PageStatesLabel() {
            Loading = "Загрузка...";
            Empty = "Презентации отсутствуют";
            More = "Показать еще";
            Complete = "Все презентации загружены";
            IsEmptyShowed = true;
        }

        public string Loading { get; set; }
        public string Empty { get; set; }
        public string More { get; set; }
        public string Complete { get; set; }
        public bool IsEmptyShowed { get; set; }
    }
}