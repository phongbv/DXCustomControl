using DevExpress.Web;
using DevExpress.Web.Mvc;

namespace ISTS.Mvc
{
    public class LOSMVCxSpinEditProperties : MVCxTextBoxProperties
    {
        private CurrencyProperty _currencySettting;
        public LOSMVCxSpinEditProperties()
        {
            _currencySettting = new CurrencyProperty(this);
            this.DisplayFormatInEditMode = true;
            this.Styles.Style.CssClass = "warning-control";
        }

        #region Property
        /// <summary>
        /// Thuộc tính này chỉ có giá trị khi IsInputForCurrency = true.
        /// </summary>
        public CurrencyProperty Currency => _currencySettting;

        public string Prefix { get; set; } = string.Empty;
        public string Subfix { get; set; } = string.Empty;
        internal int? MaxTotalNumber { get; set; }
        internal int? MaxNumberAfterDecimalPoint { get; set; }
        internal decimal? SimpleValue { get; set; }
        public NumberType NumberType { get; set; } = NumberType.Float;
        /// <summary>
        /// Default value: NumberFormatType.None
        /// </summary>
        public NumberFormatType NumberFormatType { get; set; } = NumberFormatType.None;
        public new LOSSpinEditClientSideEvents ClientSideEvents => (LOSSpinEditClientSideEvents)base.ClientSideEvents;


        #endregion
        #region Method
        internal void Prepare()
        {
            ClientSideEvents.KeyDown = "OnSpinEditKeyDown";
            switch (NumberFormatType)
            {
                case NumberFormatType.Number:
                    Subfix = Prefix = string.Empty;
                    _currencySettting = new CurrencyProperty(this);
                    break;
                case NumberFormatType.NumberWithCurrency:
                    _displayFormatString = _currencySettting.DisplayFormat;
                    Prefix = string.Empty;
                    Subfix = " " + _currencySettting.CcyAsc3;
                    break;
                case NumberFormatType.CustomFormat:
                    _currencySettting = new CurrencyProperty(this);
                    break;
                case NumberFormatType.Prefix:
                    Subfix = string.Empty;
                    _currencySettting = new CurrencyProperty(this);
                    break;
                case NumberFormatType.Subfix:
                    Prefix = string.Empty;
                    _currencySettting = new CurrencyProperty(this);
                    break;
                case NumberFormatType.PrefixAndSubFix:
                    _currencySettting = new CurrencyProperty(this);
                    InitFormatString(Prefix, Subfix);
                    break;
                case NumberFormatType.Percentage:
                    _currencySettting = new CurrencyProperty(this);
                    Prefix = string.Empty;
                    Subfix = " %";
                    break;
                default:
                    Prefix = Subfix = string.Empty;
                    _currencySettting = new CurrencyProperty(this);
                    break;
            }
            _displayFormatString = InitFormatString(Prefix, Subfix);
            //this.ClientSideEvents.Init = "function(s,e){ console.log(s.GetRawValue());s.MaxTotalDigit =" + (MaxTotalNumber.HasValue ? MaxTotalNumber.Value.ToString() : "null") + "; "
            //                                                + "s.MaxDigitAfterDecimalPoint = " + (MaxNumberAfterDecimalPoint.HasValue ? MaxNumberAfterDecimalPoint.Value.ToString() : "null") + "; "
            //                                                + $"s.ThousandPoint = '{Currency.ThousandPoint}'; "
            //                                                + $"s.DecimalPoint = '{Currency.DecimalPoint}'; "
            //                                                + $"s.Prefix = '{Prefix}'; "
            //                                                + $"s.Subfix = '{Subfix}'; "
            //                                                + "s.IsCustomControl = true; "
            //                                                + "s.NumberType = '" + (NumberType == NumberType.Float ? "f" : "i") + "'; "
            //                                                + "InitTextBox(s);s.SetRawValue('" + SimpleValue + "');s.SetValue('" + SimpleValue + "')}";
            this.ClientSideEvents.Init = $"function(s,e,){{ InitSpinEdit(s,e, {(MaxTotalNumber.HasValue ? MaxTotalNumber.Value.ToString() : "null")}," +
                $"{(MaxNumberAfterDecimalPoint.HasValue ? MaxNumberAfterDecimalPoint.Value.ToString() : "null")}," +
                $"'{Currency.ThousandPoint}','{Currency.DecimalPoint}','{Prefix}','{Subfix}')}}";


        }
        protected override EditClientSideEventsBase CreateClientSideEvents()
        {
            return new LOSSpinEditClientSideEvents();
        }
        private string InitFormatString(string prefix, string subfix)
        {
            return $"{prefix}##,###{subfix};-{prefix}##,### {subfix};0 {subfix}";
        }
        private string _displayFormatString = string.Empty;
        /// <summary>
        /// Please use FormatNumberString instead of.
        /// </summary>
        public override string DisplayFormatString
        {
            get { return _displayFormatString; }
            set { _displayFormatString = value; }
        }
        #endregion
    }

    public class CurrencyProperty
    {
        private readonly LOSMVCxSpinEditProperties _spinEditProperties;
        public CurrencyProperty(LOSMVCxSpinEditProperties spinEditProperties)
        {
            _spinEditProperties = spinEditProperties;
        }
        private string _ccyAsc3;
        public string CcyAsc3
        {
            get { return _ccyAsc3; }
            set
            {
                _ccyAsc3 = value;
            }
        }

        public string DecimalPoint { get; set; } = ".";
        public string ThousandPoint { get; set; } = ",";
        public string DisplayFormat
        {
            get
            {
                return $"##{ThousandPoint}### {CcyAsc3};-##{ThousandPoint}### {CcyAsc3};0 {CcyAsc3}";
            }
        }
    }

    public enum NumberType
    {
        Integer,
        Float
    }

    public enum NumberFormatType
    {
        None,
        Number,
        NumberWithCurrency,
        CustomFormat,
        Prefix,
        Subfix,
        PrefixAndSubFix,
        Percentage
    }

}