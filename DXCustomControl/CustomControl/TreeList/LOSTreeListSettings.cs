using DevExpress.Web;
using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.Web.ASPxTreeList;
using DevExpress.Web.Data;
using DevExpress.Utils;
using ISTS.Core.Validation.Attributes;
using System.Reflection;
using System.Web.Mvc;
using ISTS.Mvc;
using System.Web.UI.WebControls;
using ISTS.Mvc.TreeList;

namespace ISTS.Mvc
{
    public class LOSTreeListSettings
    {
        internal DevExpress.Web.Mvc.TreeListSettings Settings { get; set; } = new DevExpress.Web.Mvc.TreeListSettings();
        public LOSTreeListSettings(DevExpress.Web.Internal.IWebControlObject owner) : this()
        {
        }
        public LOSTreeListSettings(ModelMetadata modelMetadata, ViewContext viewContext)
        {
            Columns = new LOSTreeListColumnCollection(Settings.Columns, modelMetadata, viewContext);
        }
        public LOSTreeListSettings(DevExpress.Web.Internal.IWebControlObject owner, ModelMetadata modelMetadata, ViewContext viewContext)
        {
            Columns = new LOSTreeListColumnCollection(Settings.Columns, modelMetadata, viewContext);
        }
        public LOSTreeListSettings()
        {
            Columns = new LOSTreeListColumnCollection(Settings.Columns);
        }
        public object DataSource { get; set; }
        public LOSTreeListColumnCollection Columns { get; }
        #region DevExpress
        public string Name { get => Settings.Name; set { Settings.Name = value; } }
        public Unit Width { get => Settings.Width; set { Settings.Width = value; } }
        public Unit Height { get => Settings.Height; set { Settings.Height = value; } }
        public MenuStyles StylesToolbar => Settings.StylesToolbar;
        public PagerStyles StylesPager => Settings.StylesPager;
        public EditorStyles StylesEditors => Settings.StylesEditors;
        public TreeListStyles Styles => Settings.Styles;
        public MVCxTreeListToolbarCollection Toolbars => Settings.Toolbars;
        public MVCxTreeListSettingsText SettingsText => Settings.SettingsText;
        public MVCxTreeListSettingsSelection SettingsSelection => Settings.SettingsSelection;
        public string SummaryText
        {
            get { return Settings.SummaryText; }
            set { Settings.SummaryText = value; }
        }
        public MVCxTreeListSettingsDataSecurity SettingsDataSecurity => Settings.SettingsDataSecurity;
        public MVCxTreeListSettingsPager SettingsPager => Settings.SettingsPager;
        public MVCxTreeListSettingsLoadingPanel SettingsLoadingPanel => Settings.SettingsLoadingPanel;
        public MVCxTreeListSettingsExport SettingsExport => Settings.SettingsExport;
        public MVCxTreeListSettingsEditing SettingsEditing => Settings.SettingsEditing;
        public MVCxTreeListSettingsCookies SettingsCookies => Settings.SettingsCookies;
        public TreeListSettingsResizing SettingsResizing => Settings.SettingsResizing;
        public MVCxTreeListSettingsBehavior SettingsBehavior => Settings.SettingsBehavior;
        public MVCxTreeListSettingsPopupEditForm SettingsPopupEditForm => Settings.SettingsPopupEditForm;
        public MVCxTreeListSummaryCollection Summary => Settings.Summary;
       
        public MVCxTreeListCommandColumn CommandColumn => Settings.CommandColumn;
        public TreeListToolbarItemClickEventHandler ToolbarItemClick
        {
            get { return Settings.ToolbarItemClick; }
            set { Settings.ToolbarItemClick = value; }
        }
        public TreeListVirtualNodeEventHandler VirtualModeNodeCreated
        {
            get { return Settings.VirtualModeNodeCreated; }
            set { Settings.VirtualModeNodeCreated = value; }
        }
        public TreeListCustomNodeSortEventHandler CustomNodeSort
        {
            get { return Settings.CustomNodeSort; }
            set { Settings.CustomNodeSort = value; }
        }
        public TreeListCustomSummaryEventHandler CustomSummaryCalculate
        {
            get { return Settings.CustomSummaryCalculate; }
            set { Settings.CustomSummaryCalculate = value; }
        }
        public TreeListNodeValidationEventHandler NodeValidating
        {
            get { return Settings.NodeValidating; }
            set { Settings.NodeValidating = value; }
        }
        public ASPxDataInitNewRowEventHandler InitNewNode
        {
            get { return Settings.InitNewNode; }
            set { Settings.InitNewNode = value; }
        }
        public TreeListHtmlRowEventHandler HtmlRowPrepared
        {
            get { return Settings.HtmlRowPrepared; }
            set { Settings.HtmlRowPrepared = value; }
        }
        public TreeListHtmlDataCellEventHandler HtmlDataCellPrepared
        {
            get { return Settings.HtmlDataCellPrepared; }
            set { Settings.HtmlDataCellPrepared = value; }
        }
        public TreeListHtmlCommandCellEventHandler HtmlCommandCellPrepared
        {
            get { return Settings.HtmlCommandCellPrepared; }
            set { Settings.HtmlCommandCellPrepared = value; }
        }
        public EventHandler DataBound
        {
            get { return Settings.DataBound; }
            set { Settings.DataBound = value; }
        }
        public EventHandler DataBinding
        {
            get { return Settings.DataBinding; }
            set { Settings.DataBinding = value; }
        }
        public TreeListCustomJSPropertiesEventHandler CustomJSProperties
        {
            get { return Settings.CustomJSProperties; }
            set { Settings.CustomJSProperties = value; }
        }
        public TreeListCommandColumnButtonEventHandler CommandColumnButtonInitialize
        {
            get { return Settings.CommandColumnButtonInitialize; }
            set { Settings.CommandColumnButtonInitialize = value; }
        }
        public ASPxClientLayoutHandler ClientLayout
        {
            get { return Settings.ClientLayout; }
            set { Settings.ClientLayout = value; }
        }
        public TreeListColumnEditorEventHandler CellEditorInitialize
        {
            get { return Settings.CellEditorInitialize; }
            set { Settings.CellEditorInitialize = value; }
        }
        public EventHandler BeforeGetCallbackResult
        {
            get { return Settings.BeforeGetCallbackResult; }
            set { Settings.BeforeGetCallbackResult = value; }
        }
        public MVCxTreeListNodeCollection Nodes => Settings.Nodes;
        public object RootValue
        {
            get { return Settings.RootValue; }
            set { Settings.RootValue = value; }
        }
        public MVCxTreeListSettingsCustomizationWindow SettingsCustomizationWindow => Settings.SettingsCustomizationWindow;
        public bool PreviewEncodeHtml
        {
            get { return Settings.PreviewEncodeHtml; }
            set { Settings.PreviewEncodeHtml = value; }
        }
        public DefaultBoolean RightToLeft
        {
            get { return Settings.RightToLeft; }
            set { Settings.RightToLeft = value; }
        }
        public object CallbackRouteValues
        {
            get { return Settings.CallbackRouteValues; }
            set { Settings.CallbackRouteValues = value; }
        }
        public object DeleteSelectedNodesRouteValues
        {
            get { return Settings.DeleteSelectedNodesRouteValues; }
            set { Settings.DeleteSelectedNodesRouteValues = value; }
        }
        public object CustomActionRouteValues
        {
            get { return Settings.CustomActionRouteValues; }
            set { Settings.CustomActionRouteValues = value; }
        }
        public object CustomDataActionRouteValues
        {
            get { return Settings.CustomDataActionRouteValues; }
            set { Settings.CustomDataActionRouteValues = value; }
        }
        public bool AccessibilityCompliant
        {
            get { return Settings.AccessibilityCompliant; }
            set { Settings.AccessibilityCompliant = value; }
        }
        public bool AutoGenerateColumns
        {
            get { return Settings.AutoGenerateColumns; }
            set { Settings.AutoGenerateColumns = value; }
        }
        public string Caption
        {
            get { return Settings.Caption; }
            set { Settings.Caption = value; }
        }
        public TreeListClientSideEvents ClientSideEvents => Settings.ClientSideEvents;
        public bool ClientVisible
        {
            get { return Settings.ClientVisible; }
            set { Settings.ClientVisible = value; }
        }
        public AppearanceStyle ControlStyle => Settings.ControlStyle;
        public bool AutoGenerateServiceColumns
        {
            get { return Settings.AutoGenerateServiceColumns; }
            set { Settings.AutoGenerateServiceColumns = value; }
        }
        public bool EnableCallbackAnimation
        {
            get { return Settings.EnableCallbackAnimation; }
            set { Settings.EnableCallbackAnimation = value; }
        }
        public string PreviewFieldName
        {
            get { return Settings.PreviewFieldName; }
            set { Settings.PreviewFieldName = value; }
        }
        public TreeListDataCacheMode DataCacheMode
        {
            get { return Settings.DataCacheMode; }
            set { Settings.DataCacheMode = value; }
        }
        public string KeyFieldName
        {
            get { return Settings.KeyFieldName; }
            set { Settings.KeyFieldName = value; }
        }
        public bool KeyboardSupport
        {
            get { return Settings.KeyboardSupport; }
            set { Settings.KeyboardSupport = value; }
        }
        public string ParentFieldName
        {
            get { return Settings.ParentFieldName; }
            set { Settings.ParentFieldName = value; }
        }
        public TreeListImages Images => Settings.Images;
        public AutoBoolean EnablePagingGestures
        {
            get { return Settings.EnablePagingGestures; }
            set { Settings.EnablePagingGestures = value; }
        }
        public bool EnablePagingCallbackAnimation
        {
            get { return Settings.EnablePagingCallbackAnimation; }
            set { Settings.EnablePagingCallbackAnimation = value; }
        }
        public EditorImages ImagesEditors => Settings.ImagesEditors;

        public void SetDataCellTemplateContent(Action<TreeListDataCellTemplateContainer> contentMethod)
        {
            Settings.SetDataCellTemplateContent(contentMethod);
        }
        public void SetDataCellTemplateContent(string content)
        {
            Settings.SetDataCellTemplateContent(content);
        }
        public void SetEditFormTemplateContent(string content)
        {
            Settings.SetEditFormTemplateContent(content);
        }
        public void SetEditFormTemplateContent(Action<TreeListEditFormTemplateContainer> contentMethod)
        {
            Settings.SetEditFormTemplateContent(contentMethod);
        }
        public void SetFooterCellTemplateContent(Action<TreeListFooterCellTemplateContainer> contentMethod)
        {
            Settings.SetFooterCellTemplateContent(contentMethod);
        }
        public void SetFooterCellTemplateContent(string content)
        {
            Settings.SetFooterCellTemplateContent(content);
        }
        public void SetGroupFooterCellTemplateContent(Action<TreeListFooterCellTemplateContainer> contentMethod)
        {
            Settings.SetGroupFooterCellTemplateContent(contentMethod);
        }
        public void SetGroupFooterCellTemplateContent(string content)
        {
            Settings.SetGroupFooterCellTemplateContent(content);
        }
        public void SetHeaderCaptionTemplateContent(Action<TreeListHeaderTemplateContainer> contentMethod)
        {
            Settings.SetHeaderCaptionTemplateContent(contentMethod);
        }
        public void SetHeaderCaptionTemplateContent(string content)
        {
            Settings.SetHeaderCaptionTemplateContent(content);
        }
        public void SetPreviewTemplateContent(Action<TreeListPreviewTemplateContainer> contentMethod)
        {
            Settings.SetPreviewTemplateContent(contentMethod);
        }
        public void SetPreviewTemplateContent(string content)
        {
            Settings.SetPreviewTemplateContent(content);
        } 
        #endregion
    }

    public class LOSTreeListColumnCollection : DevExpress.Web.ASPxTreeList.TreeListColumnCollection
    {
        internal bool IsInit = false;
        internal System.Web.Mvc.ModelMetadata modelMetadata;
        internal ViewContext viewContext;
        internal MVCxTreeListColumnCollection Collection;
        public LOSTreeListColumnCollection(MVCxTreeListColumnCollection columns) : base(null)
        {
            this.Collection = columns;
        }
        public LOSTreeListColumnCollection(MVCxTreeListColumnCollection columns, System.Web.Mvc.ModelMetadata modelMetadata, ViewContext viewContext) : this(columns)
        {
            this.modelMetadata = modelMetadata;
            this.viewContext = viewContext;
        }
        public void Add(Action<LOSTreeListColumn> method)
        {
            LOSTreeListColumn column = new LOSTreeListColumn(modelMetadata);
            method.Invoke(column);
            Collection.Add(column);
            base.Add(column);

        }
        public MVCxTreeListColumn Add()
        {
            return Collection.Add();
        }
        public MVCxTreeListColumn Add(string fieldName)
        {
            var column = Collection.Add(fieldName);
            base.Add(column);
            return column;
        }
        public MVCxTreeListColumn Add(string fieldName, string caption)
        {
            var column = Collection.Add(fieldName, caption);
            base.Add(column);
            return column;
        }
        private bool CheckingBeforeAdd()
        {
            return modelMetadata == null && !IsInit ? false : true;
        }
        public void AddSpinColumn(string fieldName, string caption, Action<TreeListSpinEditColumn> columnSettings)
        {
            bool isValid = CheckingBeforeAdd();
            // Null when init data to get datasource
            if (!isValid)
            {
                throw new Exception("Cannot model metadata from datasource");
            }
            else if (modelMetadata == null)
            {
                return;
            }
            var column = new TreeListSpinEditColumn();
            columnSettings.Invoke(column);
            column.FieldName = string.IsNullOrEmpty(column.FieldName) ? fieldName : column.FieldName;
            column.Caption = string.IsNullOrEmpty(column.Caption) ? caption : column.Caption;
            AddColumn(column);
        }
        public void AddSpinColumn(string fieldName, Action<TreeListSpinEditColumn> columnSettings)
        {
            AddSpinColumn(fieldName, fieldName, columnSettings);
        }
        public void AddSpinColumn(string fieldName)
        {
            AddSpinColumn(fieldName, fieldName, (settings) => { });
        }
        public void AddSpinColumn(string fieldName, string caption)
        {
            AddSpinColumn(fieldName, caption, (settings) => { });
        }
        public void AddSpinColumn(Action<TreeListSpinEditColumn> columnSettings)
        {
            AddSpinColumn(null, null, columnSettings);
        }
        private void AddColumn(TreeListSpinEditColumn column)
        {
            column.Context = viewContext;
            var modelType = modelMetadata.ModelType;
            PropertyInfo propertyInfo = modelType.GetProperty(column.FieldName);
            if (propertyInfo != null)
            {
                var numberPrecisionAttribute = propertyInfo.GetCustomAttributes(typeof(NumberPrecisionAttribute), true).FirstOrDefault() as NumberPrecisionAttribute;
                if (numberPrecisionAttribute != null)
                {
                    column.PropertiesEdit.MaxTotalNumber = numberPrecisionAttribute.MaxDigits;
                    column.PropertiesEdit.MaxNumberAfterDecimalPoint = numberPrecisionAttribute.MaxDigitAfterPoint;
                }

            }
            column.PropertiesEdit.Prepare();
            Collection.Add(column);
            Add(column);
        }
    }
    public class TreeListSpinEditColumn : TreeListTextColumn
    {
        public ViewContext Context { get; internal set; }
        public TreeListSpinEditColumn() : base()
        {

        }

        public new LOSMVCxSpinEditProperties PropertiesEdit
        {
            get { return (LOSMVCxSpinEditProperties)base.PropertiesEdit; }
        }

        public override void Assign(CollectionItem source)
        {
            TreeListSpinEditColumn cxGridViewColumn = source as TreeListSpinEditColumn;
            this.Context = cxGridViewColumn.Context;
            base.Assign(cxGridViewColumn);
        }
        protected override EditPropertiesBase CreateEditProperties()
        {
            return new LOSMVCxSpinEditProperties();
        }

    }

    public class LOSTreeListTextColumn : TreeListTextColumn
    {

    }
}