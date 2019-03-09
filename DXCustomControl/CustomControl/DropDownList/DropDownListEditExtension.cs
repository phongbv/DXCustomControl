using System.Collections.Generic;
using System.Web.Mvc;
using DevExpress.Web.Internal;
using DevExpress.Web.Mvc;
using DevExpress.Web.Mvc.UI;
using DevExpress.Web;
using System.Web.UI.WebControls;

namespace LOS.CustomControl
{
    public class DropDownListEditExtension : DropDownEditExtension
    {
        private HtmlHelper HtmlHelper => HttpUtils.GetContextValue<HtmlHelper>("DXHtmlHelper", null);
        public DropDownListEditExtension(DropDownListEditSettings settings) : base(settings)
        {
        }

        public DropDownListEditExtension(DropDownListEditSettings settings, ViewContext viewContext) : base(settings, viewContext)
        {
        }

        public DropDownListEditExtension(DropDownListEditSettings settings, ViewContext viewContext, ModelMetadata metadata) : base(settings, viewContext, metadata)
        {
        }


        protected override void ApplySettings(SettingsBase settings)
        {
            var dropDownSetting = settings as DropDownListEditSettings;
            dropDownSetting.Properties.ClientSideEvents.KeyDown = "OnPressBack";
            dropDownSetting.Properties.ClientSideEvents.LostFocus = $"function(s,e){{OnLosFocusDropdown('{ dropDownSetting.HiddenPropertyId }','{dropDownSetting.Name}')}}";
            dropDownSetting.Properties.ClientSideEvents.KeyPress = "OnSearchOnTree";
            dropDownSetting.Properties.ClientSideEvents.GotFocus = "function(s,e){s.ShowDropDown()}";
            dropDownSetting.Properties.ClientSideEvents.Init = $"function(){{UpdateDropDownDisplay($('#{dropDownSetting.HiddenPropertyId}').val(),'{dropDownSetting.Name}')}}";
            dropDownSetting.CustomJSProperties = (s, e) =>
            {
                e.Properties["cpClearFilter"] = DevExpress.Web.Mvc.Internal.Utils.GetUrl(dropDownSetting.ClearFilterRouteValues);
                e.Properties["cpApplyFilter"] = DevExpress.Web.Mvc.Internal.Utils.GetUrl(dropDownSetting.ApplyFilterRouteValues);
                e.Properties["cpGetTextBoxDisplay"] = DevExpress.Web.Mvc.Internal.Utils.GetUrl(dropDownSetting.GetDisplayTextRouteValues);
                e.Properties["cpDDList"] = "TreeList" + dropDownSetting.Name;
            };
            dropDownSetting.SetDropDownWindowTemplateContent(c =>
            {
                var controlName = "TreeList" + dropDownSetting.Name;
                HtmlHelper.DevExpress().TreeList(
                tlSettings =>
                {
                    tlSettings.Name = controlName;
                    tlSettings.CallbackRouteValues = dropDownSetting.CallbackRouteValues;
                    tlSettings.ParentFieldName = dropDownSetting.ParentFieldName;
                    tlSettings.KeyFieldName = dropDownSetting.KeyFieldName;
                    tlSettings.Columns.Add(dropDownSetting.TextField);
                    tlSettings.ClientSideEvents.NodeClick = $"function(s,e){{OnDropDownSelected(s,e,'{ dropDownSetting.HiddenPropertyId }','{ dropDownSetting.Name }')}}";
                    tlSettings.Settings.ShowColumnHeaders = false;
                    tlSettings.SettingsBehavior.AutoExpandAllNodes = true;
                    tlSettings.Settings.VerticalScrollBarMode = ScrollBarMode.Visible;
                    tlSettings.Width = Unit.Pixel(690);
                    Dictionary<string, object> valuePairs = new Dictionary<string, object>();
                    valuePairs["controlName"] = controlName;
                    valuePairs["KeyFieldName"] = dropDownSetting.KeyFieldName;
                    valuePairs["ParentFieldName"] = dropDownSetting.ParentFieldName;
                    valuePairs["TextField"] = dropDownSetting.TextField;

                    var callbackArgs = LOSExtensionUtils.GenerateCallbackAgrs(valuePairs);

                    tlSettings.ClientSideEvents.BeginCallback = "function(s,e){" + callbackArgs + "}";
                    tlSettings.PreRender = (sender, e) =>
                    {
                        MVCxTreeList treeList = sender as DevExpress.Web.Mvc.MVCxTreeList;
                        treeList.ExpandAll();
                    };
                    tlSettings.BeforeGetCallbackResult = (sender, e) =>
                    {
                        MVCxTreeList treeList = sender as DevExpress.Web.Mvc.MVCxTreeList;
                        treeList.ExpandAll();
                    };
                }).Bind(dropDownSetting.DataSource).Render();
            });
            base.ApplySettings(settings);
        }
    }

}