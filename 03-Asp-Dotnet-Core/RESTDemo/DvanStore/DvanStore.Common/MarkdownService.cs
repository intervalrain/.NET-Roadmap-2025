namespace DvanStore.Common;

/// <summary>
/// Service for loading markdown documentation files
/// </summary>
public static class MarkdownService
{
    private static readonly Dictionary<string, string> _markdownCache = [];
    private static readonly string _docsPath = Path.Combine(AppContext.BaseDirectory, "Docs");

    /// <summary>
    /// Load markdown content from file
    /// </summary>
    /// <param name="filename">Markdown filename (without .md extension)</param>
    /// <returns>Markdown content</returns>
    public static string LoadMarkdown(string filename)
    {
        if (_markdownCache.TryGetValue(filename, out var cachedContent))
        {
            return cachedContent;
        }

        var filePath = Path.Combine(_docsPath, $"{filename}.md");
        
        if (!File.Exists(filePath))
        {
            return $"Documentation file '{filename}.md' not found.";
        }

        try
        {
            var content = File.ReadAllText(filePath);
            _markdownCache[filename] = content;
            return content;
        }
        catch (Exception ex)
        {
            return $"Error loading documentation: {ex.Message}";
        }
    }

    /// <summary>
    /// Clear the markdown cache
    /// </summary>
    public static void ClearCache()
    {
        _markdownCache.Clear();
    }

    /// <summary>
    /// Load markdown content with fallback text
    /// </summary>
    /// <param name="filename">Markdown filename (without .md extension)</param>
    /// <param name="fallback">Fallback text if file not found</param>
    /// <returns>Markdown content or fallback text</returns>
    public static string LoadMarkdownOrFallback(string filename, string fallback)
    {
        var content = LoadMarkdown(filename);
        return content.StartsWith("Documentation file") || content.StartsWith("Error loading") 
            ? fallback 
            : content;
    }
}