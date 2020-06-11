using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CoreApp31.CustomTagHelper
{
    [HtmlTargetElement("table", Attributes = "generate-rows, source-model")]
    public class TableTagHelper : TagHelper
    {
        //2.
        [HtmlAttributeName("generate-rows")]
        public int RepeatCount { get; set; }
        //3.
        [HtmlAttributeName("source-model")]
        public ModelExpression DataModel { get; set; }

        //4.
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            //4a
            IEnumerable model = DataModel.Model as IEnumerable;
            //4b
            if (model == null)
            {
                return;
            }
            else
            {
                //4c
                StringBuilder sb = new StringBuilder();
                //4d
                foreach (var m in model)
                {
                    PropertyInfo[] properties = m.GetType().GetProperties();


                    //4e
                    string html = "";
                    for (int i = 0; i < properties.Length; i++)
                    {
                        html += "" + m.GetType().GetProperty(properties[i].Name).GetValue(m, null) + "";
                    }
                    html += "";

                    sb.Append(html);
                }

                //4f
                output.Content.SetHtmlContent(sb.ToString());
            }
        }
    }
}
