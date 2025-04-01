using Markdig;

namespace MoblieShop.Service
{
    public class MarkdownService : IMarkdownService
    {
        public string ConvertMarkdownToHtml(string markdown)
        {
            if (string.IsNullOrEmpty(markdown))
            {
                return string.Empty;
            }

            var pipeline = new MarkdownPipelineBuilder()
                .UseAdvancedExtensions()
                .Build();

            return Markdown.ToHtml(markdown, pipeline);
        }
    }
}
