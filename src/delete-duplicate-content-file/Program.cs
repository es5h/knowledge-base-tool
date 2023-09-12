var directoryPath = args.Length > 0 ? args[0] : "";
var files = Directory.GetFiles(directoryPath, "*.txt"); // 확장자가 .txt인 파일만 가져옴.

// 숫자로 시작하는 파일을 그룹화.
var groupedFiles = files.GroupBy(file =>
{
    var fileName = Path.GetFileNameWithoutExtension(file);
    var parts = fileName.Split('-');
    return parts[0];
});

var deletedFilesCount = 0;

foreach (var group in groupedFiles)
{
    if (group.Count() > 1) // 동일한 숫자로 시작하는 파일이 둘 이상 있을 경우
    {
        var numericFileName = $"{group.Key}.txt"; // 숫자만 있는 파일 이름 (확장자 추가)
        var numericFilePath = Path.Combine(directoryPath, numericFileName); // 파일의 전체 경로

        if (!File.Exists(numericFilePath)) // 파일이 없으면 다음 반복으로 이동
        {
            continue;
        }

        // 숫자로만 된 파일의 내용을 읽음.
        var numericFileContent = File.ReadAllText(numericFilePath);

        // 그룹 내의 다른 파일과 내용을 비교
        if (group.Where(f => f != numericFilePath).Select(File.ReadAllText)
            .Any(otherFileContent => numericFileContent == otherFileContent))
        {
            File.Delete(numericFilePath); // 숫자로만 된 파일을 삭제
            deletedFilesCount++; // 삭제된 파일 카운터 증가
        }
    }
}

// 삭제된 파일과 남은 파일의 수를 표시
Console.WriteLine($"Deleted files: {deletedFilesCount}");
Console.WriteLine($"Remaining files: {files.Length - deletedFilesCount}");