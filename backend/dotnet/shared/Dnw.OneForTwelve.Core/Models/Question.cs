namespace Dnw.OneForTwelve.Core.Models;

public class Question {
    public int Id { get; protected init; }
    public QuestionCategories Category { get; protected init; }
    public string Text { get; protected init; } = "";
    public string Answer { get; protected init;} = "";
    public QuestionLevels Level { get; protected init; }
    public string? ImageUrl { get; protected init; }
    public bool BlurImage { get; protected init; }
    public RemoteVideo? Video { get; protected init; }

    public string FirstLetterAnswer => Answer.ToArray().First().ToString().ToUpper();

    protected Question() {}

    public static Question CreateText(int id, QuestionCategories category, string text, string answer, QuestionLevels level) {
        return new Question {
            Id = id,
            Category = category,
            Text = text,
            Answer = answer,
            Level = level
        };
    }

    public static Question CreateImage(int id, QuestionCategories category, string text, string answer, QuestionLevels level, string imageUrl, bool blurImage = false) {
        return new Question {
            Id = id,
            Category = category,
            Text = text,
            Answer = answer,
            Level = level,
            ImageUrl = imageUrl,
            BlurImage = blurImage
        };
    }

    public static Question CreateVideo(int id, QuestionCategories category, string text, string answer, QuestionLevels level, RemoteVideo video) {
        return new Question {
            Id = id,
            Category = category,
            Text = text,
            Answer = answer,
            Level = level,
            Video = video
        };
    }
}