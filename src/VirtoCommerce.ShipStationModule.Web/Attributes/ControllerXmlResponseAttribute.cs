using System;
using System.Xml;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace VirtoCommerce.ShipStationModule.Web.Attributes;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
public class ControllerXmlResponseAttribute : Attribute, IResultFilter
{
    public void OnResultExecuting(ResultExecutingContext context)
    {
        if (context.Result is ObjectResult objectResult)
        {
            objectResult.Formatters.Clear();
            objectResult.Formatters.Add(new XmlSerializerOutputFormatter(new XmlWriterSettings()));
        }
    }

    public void OnResultExecuted(ResultExecutedContext context)
    {
        // There is nothing to do here
    }
}
