using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMaze.Helpers.Life
{
    [HtmlTargetElement("ReportSection")]
    public class ReportSectionTagHelper : TagHelper
    {
        public string BgColor { get; set; } = "dark";
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "h5";
            output.TagMode = TagMode.StartTagAndEndTag;
            output.Attributes.SetAttribute("class", $"bg-{BgColor} text-white text-center");

            var content = (await output.GetChildContentAsync()).GetContent();
            output.Content.SetHtmlContent(content);
        }
    }
}
