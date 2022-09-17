using JetBrains.Annotations;

namespace Dnw.OneForTwelve.Core.Models;

public class GameQuestion : Question {
  
  public int Number { [UsedImplicitly] get; set; }
  [UsedImplicitly] public int WordPosition { get; private set; }

  public GameQuestion(int number, int wordPosition, Question question)
  {
    Number = number;
    WordPosition = wordPosition; 

    Id = question.Id;
    Category = question.Category;
    Text = question.Text;
    Answer = question.Answer;
    Level = question.Level;
    ImageUrl = question.ImageUrl;
    BlurImage = question.BlurImage;
    Video = question.Video;
  } 
  
}