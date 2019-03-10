using DevExpress.Web;
using DevExpress.Web.ASPxTreeList;
using DevExpress.Web.Mvc;
using DevExpress.Web.Mvc.Internal;
using ISTS.Mvc;
using ISTS.Mvc.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ISTS.Mvc.TreeList
{

    public class LOSTreeListColumn : MVCxTreeListColumn
    {
        public LOSTreeListColumn()
        {
        }
        public LOSTreeListColumn(string fieldName) : base(fieldName)
        {
        }

        public LOSTreeListColumn(string fieldName, LOSTreeListColumnType columnType) : this(fieldName)
        {
            this.ColumnType = columnType;
        }

        public LOSTreeListColumn(string fieldName, string caption) : base(fieldName, caption)
        {
        }

        public LOSTreeListColumn(string fieldName, string caption, LOSTreeListColumnType columnType) : this(fieldName, caption)
        {
            this.ColumnType = columnType;
        }

        public LOSTreeListColumn(ModelMetadata metadata)
        {
            this.Metadata = metadata;
        }
        private LOSTreeListColumnType columnType;
        public override void Assign(CollectionItem source)
        {
            LOSTreeListColumn mvcxTreeListColumn = source as LOSTreeListColumn;
            if (mvcxTreeListColumn != null)
            {
                this.ColumnType = mvcxTreeListColumn.ColumnType;
                this.Metadata = mvcxTreeListColumn.Metadata;
            }
            if (mvcxTreeListColumn.PropertiesEdit is LOSMVCxSpinEditProperties)
            {
                (mvcxTreeListColumn.PropertiesEdit as LOSMVCxSpinEditProperties).Prepare();
            }
            base.Assign(source);

        }
        public new LOSTreeListColumnType ColumnType
        {
            get
            {
                return this.columnType;
            }
            set
            {
                if (this.columnType == value)
                {
                    return;
                }
                this.columnType = value;
                if (columnType == LOSTreeListColumnType.SpinEdit)
                {
                    base.ColumnType = MVCxTreeListColumnType.TextBox;
                }
                else
                {
                    base.ColumnType = (MVCxTreeListColumnType)columnType.GetHashCode();
                }
                base.ResetEditProperties();
                ExtensionsHelper.ConfigureEditPropertiesByMetadata(base.PropertiesEdit, this.Metadata);
            }
        }

        protected override EditPropertiesBase CreateEditProperties()
        {
            switch (this.ColumnType)
            {
                case LOSTreeListColumnType.BinaryImage:
                    return new MVCxColumnBinaryImageEditProperties(this);
                case LOSTreeListColumnType.ButtonEdit:
                    return new MVCxColumnButtonEditProperties(this);
                case LOSTreeListColumnType.CheckBox:
                    return new MVCxColumnCheckBoxProperties(this);
                case LOSTreeListColumnType.ColorEdit:
                    return new MVCxColumnColorEditProperties(this);
                case LOSTreeListColumnType.ComboBox:
                    return new MVCxColumnComboBoxProperties(this);
                case LOSTreeListColumnType.DateEdit:
                    return new MVCxColumnDateEditProperties(this);
                case LOSTreeListColumnType.DropDownEdit:
                    return new MVCxColumnDropDownEditProperties(this);
                case LOSTreeListColumnType.HyperLink:
                    return new MVCxColumnHyperLinkProperties(this);
                case LOSTreeListColumnType.Image:
                    return new MVCxColumnImageEditProperties(this);
                case LOSTreeListColumnType.Memo:
                    return new MVCxColumnMemoProperties(this);
                case LOSTreeListColumnType.ProgressBar:
                    return new MVCxColumnProgressBarProperties(this);
                case LOSTreeListColumnType.SpinEdit:
                    return new LOSMVCxSpinEditProperties();
                case LOSTreeListColumnType.TextBox:
                    return new MVCxColumnTextBoxProperties(this);
                case LOSTreeListColumnType.TimeEdit:
                    return new MVCxColumnTimeEditProperties(this);
                case LOSTreeListColumnType.TokenBox:
                    return new MVCxColumnTokenBoxProperties(this);
                default:
                    return new MVCxColumnTextBoxProperties(this);
            }
        }
    }

    #region OLD_VERSION
    //public class LOSTreeListColumn : TreeListEditDataColumn
    //{
    //    public LOSTreeListColumn()
    //    {
    //    }

    //    public LOSTreeListColumn(string fieldName) : base(fieldName)
    //    {
    //    }

    //    public LOSTreeListColumn(string fieldName, LOSTreeListColumnType columnType) : this(fieldName)
    //    {
    //        this.ColumnType = columnType;
    //    }

    //    public LOSTreeListColumn(string fieldName, string caption) : base(fieldName, caption)
    //    {
    //    }

    //    public LOSTreeListColumn(string fieldName, string caption, LOSTreeListColumnType columnType) : this(fieldName, caption)
    //    {
    //        this.ColumnType = columnType;
    //    }

    //    public LOSTreeListColumn(ModelMetadata metadata)
    //    {
    //        this.Metadata = metadata;
    //    }
    //    public override void Assign(CollectionItem source)
    //    {
    //        LOSTreeListColumn mvcxTreeListColumn = source as LOSTreeListColumn;
    //        if (mvcxTreeListColumn != null)
    //        {
    //            this.ColumnType = mvcxTreeListColumn.ColumnType;
    //            this.Metadata = mvcxTreeListColumn.Metadata;
    //        }
    //        if (mvcxTreeListColumn.PropertiesEdit is LOSMVCxSpinEditProperties)
    //        {
    //            (mvcxTreeListColumn.PropertiesEdit as LOSMVCxSpinEditProperties).Prepare();
    //        }
    //        base.Assign(source);

    //    }
    //    public override string FieldName
    //    {
    //        get
    //        {
    //            return base.FieldName;
    //        }
    //        set
    //        {
    //            if (this.FieldName == value)
    //            {
    //                return;
    //            }
    //            if (this.Metadata == null && !string.IsNullOrEmpty(value))
    //            {
    //                this.Metadata = ExtensionsHelper.GetMetadataForColumn(value);
    //            }
    //            base.FieldName = value;
    //        }
    //    }
    //    public LOSTreeListColumnType ColumnType
    //    {
    //        get
    //        {
    //            return this.columnType;
    //        }
    //        set
    //        {
    //            if (this.columnType == value)
    //            {
    //                return;
    //            }
    //            this.columnType = value;
    //            base.ResetEditProperties();
    //            ExtensionsHelper.ConfigureEditPropertiesByMetadata(base.PropertiesEdit, this.Metadata);
    //        }
    //    }
    //    public ModelMetadata Metadata
    //    {
    //        get
    //        {
    //            return this.metadata;
    //        }
    //        set
    //        {
    //            if (this.metadata == value)
    //            {
    //                return;
    //            }
    //            this.metadata = value;
    //            if (string.IsNullOrEmpty(base.Caption))
    //            {
    //                base.Caption = this.Metadata.DisplayName;
    //            }
    //            if (!base.ReadOnly)
    //            {
    //                base.ReadOnly = this.Metadata.IsReadOnly;
    //            }
    //            if (base.Visible)
    //            {
    //                base.Visible = this.Metadata.ShowForDisplay;
    //            }
    //            ExtensionsHelper.ConfigureEditPropertiesByMetadata(base.PropertiesEdit, this.Metadata);
    //        }
    //    }
    //    protected override EditPropertiesBase CreateEditProperties()
    //    {
    //        switch (this.ColumnType)
    //        {
    //            case LOSTreeListColumnType.BinaryImage:
    //                return new MVCxColumnBinaryImageEditProperties(this);
    //            case LOSTreeListColumnType.ButtonEdit:
    //                return new MVCxColumnButtonEditProperties(this);
    //            case LOSTreeListColumnType.CheckBox:
    //                return new MVCxColumnCheckBoxProperties(this);
    //            case LOSTreeListColumnType.ColorEdit:
    //                return new MVCxColumnColorEditProperties(this);
    //            case LOSTreeListColumnType.ComboBox:
    //                return new MVCxColumnComboBoxProperties(this);
    //            case LOSTreeListColumnType.DateEdit:
    //                return new MVCxColumnDateEditProperties(this);
    //            case LOSTreeListColumnType.DropDownEdit:
    //                return new MVCxColumnDropDownEditProperties(this);
    //            case LOSTreeListColumnType.HyperLink:
    //                return new MVCxColumnHyperLinkProperties(this);
    //            case LOSTreeListColumnType.Image:
    //                return new MVCxColumnImageEditProperties(this);
    //            case LOSTreeListColumnType.Memo:
    //                return new MVCxColumnMemoProperties(this);
    //            case LOSTreeListColumnType.ProgressBar:
    //                return new MVCxColumnProgressBarProperties(this);
    //            case LOSTreeListColumnType.SpinEdit:
    //                return new LOSMVCxSpinEditProperties();
    //            case LOSTreeListColumnType.TextBox:
    //                return new MVCxColumnTextBoxProperties(this);
    //            case LOSTreeListColumnType.TimeEdit:
    //                return new MVCxColumnTimeEditProperties(this);
    //            case LOSTreeListColumnType.TokenBox:
    //                return new MVCxColumnTokenBoxProperties(this);
    //            default:
    //                return new MVCxColumnTextBoxProperties(this);
    //        }
    //    }
    //    private ModelMetadata metadata;

    //    // Token: 0x04000A55 RID: 2645
    //    private LOSTreeListColumnType columnType;
    //}


    #endregion
}