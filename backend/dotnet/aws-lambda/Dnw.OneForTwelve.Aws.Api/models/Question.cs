public class Question {
    public int Id { get; protected set; }
    public QuestionCategories Category { get; protected set; }
    public string Text { get; protected set; } = "";
    public string Answer { get; protected set;} = "";
    public QuestionLevels Level { get; protected set; }
    public string? ImageUrl { get; protected set; } = null;
    public bool? BlurImage { get; protected set; } = null;
    public RemoteVideo? Video { get; protected set; } = null;

    public string FirstLetterAnswer { get => Answer.Split("").First(); }

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