using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using Dnw.OneForTwelve.Core.Models;

namespace Dnw.OneForTwelve.Core.Services;

public interface IFileService
{
    public List<string> GetWords();
    List<Question> GetQuestions();
}

public class FileService : IFileService
{
    public List<string> GetWords()
    {
        var words = new List<string>();
        using var reader = GetFile("words.csv");

        reader.ReadLine();
        while (reader.ReadLine() is { } line)
        {
            if (!line.Contains("IJ", StringComparison.OrdinalIgnoreCase))
            {
                words.Add(line);
            }
        }

        return words;
    }

    public List<Question> GetQuestions()
    {
        using var fileReader = GetFile("questions.csv");
        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            Delimiter = ";",
            BadDataFound = null,
            HasHeaderRecord = true,
            IncludePrivateMembers = true
        };

        using var csv = new CsvReader(fileReader, config);
        csv.Context.RegisterClassMap<QuestionMap>();
        var records = csv.GetRecords<Question>();
        return records.ToList();
    }

    private static StreamReader GetFile(string fileName)
    {
        return File.OpenText($"{AppDomain.CurrentDomain.BaseDirectory}/resources/{fileName}");
    }

    // ReSharper disable once ClassNeverInstantiated.Local
    private sealed class QuestionMap : ClassMap<Question>
    {
        public QuestionMap()
        {
            Map(q => q.Id).Name("number");
            Map(q => q.Category).Name("category").TypeConverter<QuestionCategoryConverter>();
            Map(q => q.Answer).Name("answer");
            Map(q => q.Text).Name("question");
            Map(q => q.Level).Name("m").TypeConverter<QuestionLevelConverter>();
        }
    }
    
    // ReSharper disable once ClassNeverInstantiated.Local
    private class QuestionCategoryConverter : DefaultTypeConverter
    {
        public override object ConvertFromString(string? text, IReaderRow row, MemberMapData memberMapData)
        {
            return text switch
            {
                // ReSharper disable once StringLiteralTypo
                "AARD" => QuestionCategories.Geography,
                // ReSharper disable once StringLiteralTypo
                "BIJB" => QuestionCategories.Bible,
                "BIO" => QuestionCategories.Biology,
                // ReSharper disable once StringLiteralTypo
                "CRYP" => QuestionCategories.Cryptic,
                "ECO" => QuestionCategories.Economy,
                "GES" => QuestionCategories.History,
                "KUN" => QuestionCategories.Art,
                "LIT" => QuestionCategories.Literature,
                "MUZ" => QuestionCategories.Music,
                "POL" => QuestionCategories.Politics,
                "REK" => QuestionCategories.ScienceOrMaths,
                "SPO" => QuestionCategories.Sports,
                "WET" => QuestionCategories.ScienceOrMaths,
                _ => QuestionCategories.Unknown
            };
        }
    }
    
    // ReSharper disable once ClassNeverInstantiated.Local
    private class QuestionLevelConverter : DefaultTypeConverter
    {
        public override object ConvertFromString(string? text, IReaderRow row, MemberMapData memberMapData)
        {
            return text switch
            {
                "1" => QuestionLevels.Easy,
                "3" => QuestionLevels.Hard,
                _ => QuestionLevels.Normal
            };
        }
    }
}