using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace ISTS.Mvc.Utils
{
    public class ModelExtensionsHelper
    {
        public static ModelMetadata GetMetadataForColumn(Type modelType, string columnName)
        {
            var modelMetadata = GetModelMetadataForModel(modelType);
            if (modelMetadata == null)
            {
                return null;
            }
            return GetMetadataForColumn(modelMetadata, columnName);
        }

        public static ModelMetadata GetMetadataForColumn(ModelMetadata modelMetadata, string columnName)
        {
            var propertyMetadata = modelMetadata.Properties.ToList().FirstOrDefault(e => e.PropertyName == columnName);
            if (propertyMetadata == null)
            {
                throw new KeyNotFoundException($"Cannot found property {columnName} in {modelMetadata.ModelType.FullName}");
            }
            return propertyMetadata;
        }

        public static ModelMetadata GetModelMetadataForModel(Type modeType)
        {
            return ModelMetadataProviders.Current.GetMetadataForType(null, modeType);
        }
    }
}