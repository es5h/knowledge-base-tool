using HtmlAgilityPack;

namespace html_to_json;

using System.Text.RegularExpressions;

public static class StringExtensions
{
    public static string? DeEntitize(this string? content) => HtmlEntity.DeEntitize(content);

    public static string? NormalizeWhitespace(this string? content) => Regex.Replace(content ?? "", @"\s+", " ");

    public static string? NormalizeNewlines(this string? content) => Regex.Replace(content ?? "", @"\n+", "\n");
}