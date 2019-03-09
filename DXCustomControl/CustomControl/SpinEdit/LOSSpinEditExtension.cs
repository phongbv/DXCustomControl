using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using DevExpress.Web;
using DevExpress.Web.Mvc;
using ISTS.Core.Validation.Attributes;

namespace LOS.CustomControl
{
    public class LOSSpinEditExtension : TextBoxExtension
    {
        protected internal new MVCxTextBox Control => base.Control as MVCxTextBox;

        protected override EditPropertiesBase Properties => Control.Properties;

        protected internal new LOSSpinEditSettings Settings => (LOSSpinEditSettings)base.Settings;

        protected override ASPxWebControl CreateControl() => new MVCxTextBox();
        public LOSSpinEditExtension(TextBoxSettings settings) : base(settings)
        {
        }

        public LOSSpinEditExtension(TextBoxSettings settings, ViewContext viewContext) : base(settings, viewContext)
        {
        }

        public LOSSpinEditExtension(TextBoxSettings settings, ViewContext viewContext, ModelMetadata metadata) : base(settings, viewContext, metadata)
        {
        }

        protected override void AssignInitialProperties()
        {
            this.Settings.AutoCompleteType = AutoCompleteType.None;
            base.AssignInitialProperties();
        }

        protected override void ApplySettings(SettingsBase settings)
        {
            DoAnythingAfterApplySettings(settings as LOSSpinEditSettings);
            base.ApplySettings(settings);
            
        }


        internal void DoAnythingAfterApplySettings(LOSSpinEditSettings settings)
        {
            settings.AutoCompleteType = System.Web.UI.WebControls.AutoCompleteType.None;
            //Reading Custom attribute
            var modelType = Metadata.ContainerType;
            PropertyInfo propertyInfo = modelType.GetProperty(base.Metadata.PropertyName);
            if (propertyInfo != null)
            {
                var numberPrecisionAttribute = propertyInfo.GetCustomAttributes(typeof(NumberPrecisionAttribute), true).FirstOrDefault() as NumberPrecisionAttribute;
                if (numberPrecisionAttribute != null)
                {
                    settings.Properties.MaxTotalNumber = numberPrecisionAttribute.MaxDigits;
                    settings.Properties.MaxNumberAfterDecimalPoint = numberPrecisionAttribute.MaxDigitAfterPoint;
                }
                if (Metadata.Model != null)
                    settings.Properties.SimpleValue = decimal.Parse(base.Metadata.Model.ToString());
            }
            settings.Properties.Prepare();
        }

    }
}