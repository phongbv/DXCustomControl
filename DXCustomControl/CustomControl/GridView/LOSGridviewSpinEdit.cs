using DevExpress.Web.Mvc;
using System;
using DevExpress.Web;
using DevExpress.Web.Internal;
using System.Web.Mvc;

namespace ISTS.Mvc
{
    public class LOSSpinEditProperties : TextBoxProperties
    {
        public LOSSpinEditProperties()
        : base()
        {
            OnUpdateNumberType();
        }

        public LOSSpinEditProperties(IPropertiesOwner owner)
        : base(owner)
        {
            OnUpdateNumberType();
            this.DisplayFormatInEditMode = true;
        }

        protected override ASPxEditBase CreateEditInstance(EditorsFactory factory)
        {
            return new LOSASPxSpinEdit();
        }
        private NumberType _numberType = NumberType.Float;
        public NumberType NumberType { get { return _numberType; } set { _numberType = value; OnUpdateNumberType(); } }
        private void OnUpdateNumberType()
        {
            string funName = "";
            switch (_numberType)
            {
                case NumberType.Integer:
                    funName = "SpinEditInteger";
                    break;
                case NumberType.Float:
                    funName = "SpinEditFloat";
                    break;
                default:
                    break;
            }
            ClientSideEvents.KeyPress = funName;
        }
    }
   
    public class GridViewSpinEditColumn : MVCxGridViewColumn
    {
        public GridViewSpinEditColumn()
        {
            PropertiesEdit = new LOSMVCxSpinEditProperties();
        }

        public override void Assign(CollectionItem source)
        {
            GridViewSpinEditColumn cxGridViewColumn = source as GridViewSpinEditColumn;
            this.Context = cxGridViewColumn.Context;
            IsCustomDataItem = cxGridViewColumn.IsCustomDataItem;
            base.Assign(source);
        }

        protected override GridViewColumnCollection CreateColumnCollection()
        {
            return new LOSGridViewColumnCollection();
        }
        protected internal bool IsCustomDataItem { get; set; }
        public new LOSMVCxSpinEditProperties PropertiesEdit
        {
            get { return (LOSMVCxSpinEditProperties)base.PropertiesEdit; }
            set { base.PropertiesEdit = value; }
        }
        [Obsolete]
        public new MVCxGridViewColumnType ColumnType { get { return MVCxGridViewColumnType.TextBox; } set { } }

        public ViewContext Context { get; internal set; }

        protected override EditPropertiesBase CreateEditProperties()
        {
            return new LOSMVCxSpinEditProperties();
        }
        protected override GridDataColumnAdapter CreateColumnAdapter()
        {
            return new GridDataColumnAdapter(this);
        }
        public new void SetDataItemTemplateContent(Action<GridViewDataItemTemplateContainer> contentMethod)
        {
            IsCustomDataItem = true;
            this.DataItemTemplateContentMethod = contentMethod;
        }

        public new void SetDataItemTemplateContent(string content)
        {
            IsCustomDataItem = true;
            DataItemTemplateContent = content;
        }

    }
}