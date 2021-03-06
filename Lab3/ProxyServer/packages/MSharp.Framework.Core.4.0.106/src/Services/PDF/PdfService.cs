﻿namespace MSharp.Framework.Services
{
    using System;

    /// <summary>
    /// Provides PDF services.
    /// </summary>
    public static class PdfService
    {
        const string HTML2PDF_CONVERTER_CONFIG_KEY = "Html2Pdf.Converter.Type";
        const string DEFAULT_HTML2PDF_TYPE = "Geeks.Html2PDF.Winnovative.Html2PdfConverter, Geeks.Html2PDF.Winnovative";

        /// <summary>
        /// Creates an instance of Html 2 PDF converter service.
        /// </summary>
        public static IHtml2PdfConverter CreateHtml2PdfConverter()
        {
            var typeName = Config.Get(HTML2PDF_CONVERTER_CONFIG_KEY, DEFAULT_HTML2PDF_TYPE);

            if (typeName.IsEmpty())
            {
                throw new Exception("Could not find the Html2Pdf converter type. The AppSetting of '{0}' is not defined.".FormatWith(HTML2PDF_CONVERTER_CONFIG_KEY));
            }

            Type type;

            try
            {
                type = Type.GetType(typeName);
                if (type == null) throw new Exception("Could not load the type: " + typeName);
            }
            catch (Exception ex)
            {
                throw new Exception("Unable to find the specified type: " + typeName, ex);
            }

            return type.CreateInstance<IHtml2PdfConverter>();
        }
    }
}