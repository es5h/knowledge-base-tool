using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using html_to_json;
using HtmlAgilityPack;

// directoryPath from args
var directoryPath = args.Length > 0 ? args[0] : "";
var jsonDirectory = Path.Combine(directoryPath, "json");

if (!Directory.Exists(jsonDirectory))
{
    Directory.CreateDirectory(jsonDirectory);
}

var files = Directory.GetFiles(directoryPath, "*.html");

foreach (var file in files)
{
    var doc = new HtmlDocument();
    doc.Load(file, Encoding.UTF8);

    var article = new Article
    {
        Url = doc.DocumentNode
            .SelectSingleNode("//meta[@property='og:url']")
            ?.GetAttributeValue("content", "Not found"),

        Title = doc.DocumentNode
            .SelectSingleNode("//h1[@class='article-title']")
            ?.InnerText.Trim() ?? "Not found",

        Content = doc.DocumentNode
            .SelectSingleNode("//div[@class='article-body']")
            ?.InnerText.Trim() ?? "Not found"
    };

    // JSON 형식으로 변환
    var json = JsonSerializer.Serialize(
        article with { Content = article.Content.DeEntitize().NormalizeWhitespace().NormalizeNewlines() },
        new JsonSerializerOptions
        {
            WriteIndented = true,
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
        });

    // 동일한 이름의 .txt 파일로 저장
    var outputFileName = Path.GetFileNameWithoutExtension(file) + ".txt";
    var outputPath = Path.Combine(jsonDirectory, outputFileName);
    File.WriteAllText(outputPath, json, Encoding.UTF8);
}


Console.WriteLine("Data saved to 'json' directory.");

internal record Article()
{
    public string? Url { get; init; }
    public string? Title { get; init; }
    public string? Content { get; init; }
}