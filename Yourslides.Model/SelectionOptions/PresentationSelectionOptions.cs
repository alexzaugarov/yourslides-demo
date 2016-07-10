using System.Collections.Generic;
using System.Reflection;

namespace Yourslides.Model.SelectionOptions {
    public class PresentationSelectionOptions : SelectionOptionsBase {
        private static readonly Dictionary<SortColumn, PropertyInfo> MapColumnProperty = new Dictionary<SortColumn, PropertyInfo> {
            {SortColumn.Date, typeof(Presentation).GetProperty("CreatedDateTime")}, 
            {SortColumn.SlideCount, typeof(Presentation).GetProperty("SlideCount")},
            {SortColumn.WatchCount, typeof(Presentation).GetProperty("WatchCount")}
        };
        public PresentationSelectionOptions() {
            Count = 15;
            SortColumn = SortColumn.Date;
            SortType = SortType.Descending;
            Visibility = PresentationVisibility.None;
            Status = PresentationStatus.None;
        }

        public string SearchString { get; set; }
        public SortColumn SortColumn { get; set; }
        public SortType SortType { get; set; }
        public PropertyInfo SortProperty {
            get { return MapColumnProperty[SortColumn]; }
        }
        public string OwnerId { get; set; }
        public PresentationVisibility Visibility { get; set; }
        public PresentationStatus Status { get; set; }
    }
    public enum SortColumn {
        Date,
        WatchCount,
        SlideCount
    }
    public enum SortType {
        Ascending,
        Descending
    }
}