using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Mvc;
using DevExpress.Web.Internal;
using DevExpress.Web.Mvc.Internal;
using ISTS.Mvc.Utils;
using System.Web;
using System.Web.Mvc.Html;

namespace ISTS.Mvc
{
    public class LOSExtensionsFactory<ModelType>
    {
        protected internal HtmlHelper<ModelType> HtmlHelper { get; set; }
        internal static LOSExtensionsFactory<ModelType> Intance => new LOSExtensionsFactory<ModelType>();

        public LOSSpinEditExtension SpinEditFor<TValueType>(Expression<Func<ModelType, TValueType>> expression, Action<LOSSpinEditSettings> method)
        {
            HttpUtils.SetContextValue<HtmlHelper>("DXHtmlHelper", HtmlHelper);
            var settingInstance = new LOSSpinEditSettings
            {
                Name = ExtensionsHelper.GetFullHtmlFieldName(expression)
            };

            method?.Invoke(settingInstance);
            var modelMetadata = ModelMetadata.FromLambdaExpression(expression, HtmlHelper.ViewData);
            var spinEditComponent = new LOSSpinEditExtension(settingInstance, HtmlHelper.ViewContext, modelMetadata);
            //spinEditComponent.DoAnythingAfterApplySettings();
            return spinEditComponent;
        }

        public GridViewExtension GridView(Action<LOSGridSettings> method)
        {
            Action<LOSGridSettings> tmpMethod = (Action<LOSGridSettings>)method.Clone();
            var tmpInstance = new LOSGridSettings();
            tmpInstance.Columns.IsInit = true;
            method.Invoke(tmpInstance);
            if (tmpInstance.DataSource == null || !tmpInstance.DataSource.GetType().IsGenericType)
            {
                throw new InvalidOperationException("Cannot bind data source without generic type");
            }
            var modelType = tmpInstance.DataSource.GetType().GetGenericArguments()[0];
            var settingInstance = new LOSGridSettings(ModelExtensionsHelper.GetModelMetadataForModel(modelType), HtmlHelper.ViewContext);
            tmpMethod?.Invoke(settingInstance);
            var gridView = new GridViewExtension(settingInstance, HtmlHelper.ViewContext);
            gridView = gridView.Bind(settingInstance.DataSource);
            return gridView;
        }
        public TreeListExtension TreeList(Action<LOSTreeListSettings> method)
        {
            Action<LOSTreeListSettings> tmpMethod = (Action<LOSTreeListSettings>)method.Clone();
            var tmpInstance = new LOSTreeListSettings();
            tmpInstance.Columns.IsInit = true;
            method.Invoke(tmpInstance);
            if (tmpInstance.DataSource == null || !tmpInstance.DataSource.GetType().IsGenericType)
            {
                throw new InvalidOperationException("Cannot bind data source without generic type");
            }
            var modelType = tmpInstance.DataSource.GetType().GetGenericArguments()[0];
            var settingInstance = new LOSTreeListSettings(ModelExtensionsHelper.GetModelMetadataForModel(modelType), HtmlHelper.ViewContext);
            tmpMethod?.Invoke(settingInstance);
            var treeList = new TreeListExtension(settingInstance.Settings, HtmlHelper.ViewContext);
            treeList = treeList.Bind(settingInstance.DataSource);
            return treeList;
        }
        public DropDownListEditExtension DropDownEditFor<TValueType>(Expression<Func<ModelType, TValueType>> expression, Action<DropDownListEditSettings> settings)
        {
            HttpUtils.SetContextValue<HtmlHelper>("DXHtmlHelper", HtmlHelper);

            var settingsInstance = new DropDownListEditSettings();
            settings?.Invoke(settingsInstance);
            settingsInstance.HiddenPropertyName = ExtensionsHelper.GetFullHtmlFieldName(expression);
            if (string.IsNullOrEmpty(settingsInstance.Name))
            {
                throw new Exception("Name of control must be not empty.");
            }
            if (string.IsNullOrEmpty(settingsInstance.HiddenPropertyName))
            {
                throw new Exception("Name of hidden property must be not empty.");
            }
            HtmlHelper.ViewContext.Writer.Write(HtmlHelper.HiddenFor(expression).ToHtmlString());
            var dropDownEdit = new DropDownListEditExtension(settingsInstance);
            return dropDownEdit;
        }
        public DropDownListEditExtension DropDownEdit(Action<DropDownListEditSettings> settings)
        {
            HttpUtils.SetContextValue<HtmlHelper>("DXHtmlHelper", HtmlHelper);

            var settingsInstance = new DropDownListEditSettings();
            settings?.Invoke(settingsInstance);
            if (string.IsNullOrEmpty(settingsInstance.Name))
            {
                throw new Exception("Name of control must be not empty.");
            }
            if (string.IsNullOrEmpty(settingsInstance.HiddenPropertyName))
            {
                throw new Exception("Name of hidden property must be not empty.");
            }
            HtmlHelper.ViewContext.Writer.Write(HtmlHelper.Hidden(settingsInstance.HiddenPropertyName).ToHtmlString());
            var dropDownEdit = new DropDownListEditExtension(settingsInstance);
            return dropDownEdit;
        }
    }

    public class LOSExtensionUtils
    {
        public static ActionResult CallbackDropDownListEdit(Controller controller, IEnumerable<object> model)
        {
            var viewData = controller.ViewData;
            if (model != null)
            {
                viewData.Model = model;
            }
            viewData["controlName"] = HttpContext.Current.Request.Form["controlName"];
            viewData["KeyFieldName"] = HttpContext.Current.Request.Form["KeyFieldName"];
            viewData["ParentFieldName"] = HttpContext.Current.Request.Form["ParentFieldName"];
            viewData["TextField"] = HttpContext.Current.Request.Form["TextField"];
            PartialViewResult result = new PartialViewResult()
            {
                ViewEngineCollection = controller.ViewEngineCollection,
                ViewName = "~/Views/Shared/CustomControl/DropDownListEdit.cshtml",
                ViewData = viewData
            };
            return result;
        }

        public static string GenerateCallbackAgrs(Dictionary<string, object> values)
        {
            string result = "";
            foreach (var key in values.Keys)
            {
                result += $"e.customArgs[\"{ key }\"] = \"{values[key] }\";";
            }
            return result;
        }
    }
}