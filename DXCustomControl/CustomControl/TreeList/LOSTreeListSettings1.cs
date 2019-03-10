//using DevExpress.Web;
//using DevExpress.Web.Internal;
//using DevExpress.Web.Mvc;
//using ISTS.Core.Validation.Attributes;
//using ISTS.Mvc;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Reflection;
//using System.Text;
//using System.Threading.Tasks;
//using System.Web.Mvc;

//namespace LOS.WebBase
//{
//    public class LOSTreeListSettings1 : TreeListSettings
//    {
//        public LOSTreeListSettings1()
//        {

//        }
//        public LOSTreeListSettings1(ModelMetadata modelMetadata, ViewContext viewContext)
//        {
//            Columns.viewContext = viewContext;
//            Columns.modelMetadata = modelMetadata;
//        }
//        public new LOSTreeListColumnCollection1 Columns => (LOSTreeListColumnCollection1)base.Columns;
//        protected override MVCxTreeListColumnCollection CreateColumnCollection()
//        {
//            return new LOSTreeListColumnCollection1();
//        }
//        public object DataSource { get; set; }
//    }

//    public class LOSTreeListColumnCollection1 : MVCxTreeListColumnCollection
//    {
//        internal bool IsInit = false;
//        internal ModelMetadata modelMetadata;
//        internal ViewContext viewContext;
//        private MVCxTreeListColumnCollection collection;
//        public LOSTreeListColumnCollection1()
//        {

//        }
//        public LOSTreeListColumnCollection1(ModelMetadata modelMetadata, ViewContext viewContext)
//        {
//            this.modelMetadata = modelMetadata;
//            this.viewContext = viewContext;
//        }
//        public void Add(Action<MVCxTreeListColumn> method);
//        public MVCxTreeListColumn Add();
//        public MVCxTreeListColumn Add(string fieldName);
//        public MVCxTreeListColumn Add(string fieldName, MVCxTreeListColumnType columnType);
//        public MVCxTreeListColumn Add(string fieldName, string caption);
//        public MVCxTreeListColumn Add(string fieldName, string caption, MVCxTreeListColumnType columnType);
//        private bool CheckingBeforeAdd()
//        {
//            return modelMetadata == null && !IsInit ? false : true;
//        }
//        public void AddSpinColumn(string fieldName, string caption, Action<TreeListSpinEditColumn1> columnSettings)
//        {
//            bool isValid = CheckingBeforeAdd();
//            // Null when init data to get datasource
//            if (!isValid)
//            {
//                throw new Exception("Cannot model metadata from datasource");
//            }
//            else if (modelMetadata == null)
//            {
//                return;
//            }
//            var column = new TreeListSpinEditColumn1();
//            columnSettings.Invoke(column);
//            column.FieldName = string.IsNullOrEmpty(column.FieldName) ? fieldName : column.FieldName;
//            column.Caption = string.IsNullOrEmpty(column.Caption) ? caption : column.Caption;
//            AddColumn(column);
//        }
//        public void AddSpinColumn(string fieldName, Action<TreeListSpinEditColumn1> columnSettings)
//        {
//            AddSpinColumn(fieldName, fieldName, columnSettings);
//        }
//        public void AddSpinColumn(string fieldName)
//        {
//            AddSpinColumn(fieldName, fieldName, (settings) => { });
//        }
//        public void AddSpinColumn(string fieldName, string caption)
//        {
//            AddSpinColumn(fieldName, caption, (settings) => { });
//        }
//        public void AddSpinColumn(Action<TreeListSpinEditColumn1> columnSettings)
//        {
//            AddSpinColumn(null, null, columnSettings);
//        }
//        private void AddColumn(TreeListSpinEditColumn1 column)
//        {
//            column.Context = viewContext;
//            var modelType = modelMetadata.ModelType;
//            PropertyInfo propertyInfo = modelType.GetProperty(column.FieldName);
//            if (propertyInfo != null)
//            {
//                var numberPrecisionAttribute = propertyInfo.GetCustomAttributes(typeof(NumberPrecisionAttribute), true).FirstOrDefault() as NumberPrecisionAttribute;
//                if (numberPrecisionAttribute != null)
//                {
//                    column.PropertiesEdit.MaxTotalNumber = numberPrecisionAttribute.MaxDigits;
//                    column.PropertiesEdit.MaxNumberAfterDecimalPoint = numberPrecisionAttribute.MaxDigitAfterPoint;
//                }

//            }
//            column.PropertiesEdit.Prepare();
//            Add(column);
//        }
//    }

//    public class TreeListSpinEditColumn1 : MVCxTreeListColumn
//    {
//        public TreeListSpinEditColumn1()
//        {
//            //PropertiesEdit = new LOSMVCxSpinEditProperties();
//        }

//        public override void Assign(CollectionItem source)
//        {
//            TreeListSpinEditColumn1 cxGridViewColumn = source as TreeListSpinEditColumn1;
//            this.Context = cxGridViewColumn.Context;
//            IsCustomDataItem = cxGridViewColumn.IsCustomDataItem;
//            base.Assign(source);
//        }

//        public new LOSMVCxSpinEditProperties PropertiesEdit
//        {
//            get { return (LOSMVCxSpinEditProperties)base.PropertiesEdit; }
//        }
//        protected internal bool IsCustomDataItem { get; set; }

//        [Obsolete]
//        public new MVCxGridViewColumnType ColumnType { get { return MVCxGridViewColumnType.TextBox; } set { } }

//        public ViewContext Context { get; internal set; }

//        protected override EditPropertiesBase CreateEditProperties()
//        {
//            return new LOSMVCxSpinEditProperties();
//        }



//    }
//}
