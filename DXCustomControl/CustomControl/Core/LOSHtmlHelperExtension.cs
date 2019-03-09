using System.Web.Mvc;

namespace LOS.CustomControl
{
    public static class LOSHtmlHelperExtension
    {
        //public static LOSExtensionsFactory DevExpress(this HtmlHelper helper)
        //{
        //    return null;
        //}
        public static LOSExtensionsFactory<TModelType> LOS<TModelType>(this HtmlHelper<TModelType> helper)
        {
            var instance = LOSExtensionsFactory<TModelType>.Intance;
            instance.HtmlHelper = helper;
            return instance;
        }
    }
}