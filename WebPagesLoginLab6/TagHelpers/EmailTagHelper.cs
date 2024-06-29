using Microsoft.AspNetCore.Razor.TagHelpers;

namespace WebPagesLoginLab6.TagHelpers
{
    /// <summary>
    /// 這個 Tag Helper 將應用於所有 <email> 標籤
    /// </summary>
    [HtmlTargetElement("email")]
    public class EmailTagHelper : TagHelper
    {
        /// <summary>
        /// Tag Helper 屬性，可以在使用標籤時通過屬性設置
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Content { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "a"; // 將 <email> 標籤更改為 <a> 標籤
            output.Attributes.SetAttribute("href", $"mailto:{Address}");
            output.Content.SetContent(Content);
        }
    }
}
