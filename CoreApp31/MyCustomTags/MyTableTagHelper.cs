using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CoreApp31.MyCustomTags
{
    /// <summary>
    /// Target it to the HTML Table Tag
    /// </summary>
    /// 
    [HtmlTargetElement("table", Attributes ="generate-tablerows,src-model")]
    public class MyTableTagHelper : TagHelper
    {
        [HtmlAttributeName("generate-tablerows")]
        public int RowCount { get; set; }

        /// <summary>
        /// ModelExpression, the type that will be used to accept the Model class
        /// that will be used to pass data to teh tag helper 
        /// </summary>
        /// 
        [HtmlAttributeName("src-model")]
        public ModelExpression DataModel { get; set; }

        /// <summary>
        /// Logic for generating custom rendering
        /// </summary>
        /// <param name="context"></param>
        /// <param name="output"></param>
        /// <returns></returns>
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            // read the model type. The model must be a collection
            IEnumerable model = DataModel.Model as IEnumerable;

            if (model == null)
            {
                return;
            }
            else 
            {
                // generating the table rendering in output
                // <table><tr><td>
                StringBuilder sb = new StringBuilder();
                // iterate over the model to generate HTML string
                foreach (var item in model)
                {
                    // read property from model
                    PropertyInfo[] properties = item.GetType().GetProperties();

                    string html = "<tr>";
                    // iterate over the properties and read their values
                    for (int i = 0; i < properties.Length; i++)
                    {
                        html += $"<td style='border:double'> {item.GetType().GetProperty(properties[i].Name).GetValue(item, null)} </td>";
                    }

                    html += "</tr>";
                    sb.Append(html);
                }

                // set the output
                output.Content.SetHtmlContent(sb.ToString());
            }
        }
    }
}
