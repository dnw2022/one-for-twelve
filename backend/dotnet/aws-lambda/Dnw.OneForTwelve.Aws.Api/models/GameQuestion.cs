public class GameQuestion : Question {
  
  public int Number { get; private set; }
  public int WordPosition { get; private set; }
  
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