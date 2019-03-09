using DevExpress.Web.Mvc;
using ISTS.Core.Validation.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;

namespace LOS.CustomControl
{
    public class LOSGridSettings : GridViewSettings
    {

        public LOSGridSettings(ModelMetadata modelMetadata)
        {
            Columns.modelMetadata = modelMetadata;
        }

        public LOSGridSettings()
        {

        }

        public LOSGridSettings(ModelMetadata modelMetadata, ViewContext viewContext)
        {
            ctx = viewContext;
            Columns.viewContext = viewContext;
            Columns.modelMetadata = modelMetadata;
        }

        public new LOSGridViewColumnCollection Columns => (LOSGridViewColumnCollection)base.Columns;
        public IEnumerable<object> DataSource { get; set; }
        internal ViewContext ctx;

        protected override MVCxGridViewColumnCollection CreateColumnCollection()
        {
            return new LOSGridViewColumnCollection();
        }

    }
    public class LOSGridViewColumnCollection : MVCxGridViewColumnCollection
    {
        internal bool IsInit = false;
        internal ModelMetadata modelMetadata;
        internal ViewContext viewContext;

        public LOSGridViewColumnCollection()
        {

        }
        public LOSGridViewColumnCollection(ModelMetadata modelMetadata, ViewContext viewContext)
        {
            this.modelMetadata = modelMetadata;
            this.viewContext = viewContext;
        }

        private bool CheckingBeforeAdd()
        {
            return modelMetadata == null && !IsInit ? false : true;
        }
        public void AddSpinColumn(string fieldName, string caption, Action<GridViewSpinEditColumn> columnSettings)
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
            var column = new GridViewSpinEditColumn();
            columnSettings.Invoke(column);
            column.FieldName = string.IsNullOrEmpty(column.FieldName) ? fieldName : column.FieldName;
            column.Caption = string.IsNullOrEmpty(column.Caption) ? caption : column.Caption;
            AddColumn(column);
        }
        public void AddSpinColumn(string fieldName, Action<GridViewSpinEditColumn> columnSettings)
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
        public void AddSpinColumn(Action<GridViewSpinEditColumn> columnSettings)
        {
            AddSpinColumn(null, null, columnSettings);
        }
        private void AddColumn(GridViewSpinEditColumn column)
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
            if (!column.IsCustomDataItem)
            {
                column.SetDataItemTemplateContent(container =>
                {
                    var objectValue = (container.Grid.DataSource as IEnumerable<object>).ElementAt(container.VisibleIndex);
                    if (objectValue == null)
                    {
                        return;
                    }
                    GridViewSpinEditColumn spinColumn = container.Column as GridViewSpinEditColumn;
                    var rawValue = objectValue.GetType().GetProperty(spinColumn.FieldName).GetValue(objectValue);
                    if (rawValue != null)
                    {
                        var nfi = (System.Globalization.NumberFormatInfo)System.Globalization.CultureInfo.InvariantCulture.NumberFormat.Clone();
                        nfi.NumberGroupSeparator = column.PropertiesEdit.Currency.ThousandPoint;
                        nfi.NumberDecimalSeparator = column.PropertiesEdit.Currency.DecimalPoint;
                        spinColumn.Context.Writer.Write(column.PropertiesEdit.Prefix + double.Parse(rawValue.ToString()).ToString(nfi) + column.PropertiesEdit.Subfix);
                    }

                });
            }
            Add(column);
        }
    }
}