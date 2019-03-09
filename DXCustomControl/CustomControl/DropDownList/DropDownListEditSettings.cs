using DevExpress.Web.Mvc;

namespace LOS.CustomControl
{
    public class DropDownListEditSettings : DropDownEditSettings
    {
        public string HiddenPropertyName { get; set; }
        public string HiddenPropertyId => string.IsNullOrEmpty(HiddenPropertyName) ? string.Empty : HiddenPropertyName.Replace(".", "_");
        public object GetDisplayTextRouteValues { get; set; }
        public object ClearFilterRouteValues { get; set; }
        public object ApplyFilterRouteValues { get; set; }
        public object CallbackRouteValues { get; set; }

        public object DataSource { get; set; }

        public string KeyFieldName { get; set; }
        public string ParentFieldName { get; set; }

        public string TextField { get; set; }
    }
}