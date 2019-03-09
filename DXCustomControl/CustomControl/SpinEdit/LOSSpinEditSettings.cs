﻿using DevExpress.Web;
using DevExpress.Web.Mvc;

namespace LOS.CustomControl
{
    public class LOSSpinEditSettings : TextBoxSettings
    {
        public new LOSMVCxSpinEditProperties Properties => (LOSMVCxSpinEditProperties)base.Properties;
        protected override EditPropertiesBase CreateProperties() => new LOSMVCxSpinEditProperties();
    }

}