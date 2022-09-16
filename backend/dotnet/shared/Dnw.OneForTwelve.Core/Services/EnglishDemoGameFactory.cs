// ReSharper disable StringLiteralTypo

using Dnw.OneForTwelve.Core.Models;

namespace Dnw.OneForTwelve.Core.Services;

public class EnglishDemoGameFactory : IDemoGameFactory {
    public Languages Language => Languages.English;

    public Game GetGame() {
    return new Game("supermarkets", new List<GameQuestion> { 
        new GameQuestion(
                8,
                1,
                Question.CreateVideo(
                    1,
                    QuestionCategories.Music,
                    "Which artist sings \"The Greatest\" here?",
                    "Sia",
                    QuestionLevels.Normal,
                    new RemoteVideo("181938772", 100, 125, RemoteVideoSources.Vimeo)
                )
            ),
            new GameQuestion(
                1,
                2,
                Question.CreateText(
                    2,
                    QuestionCategories.Geography,
                    "Of which South American country is Montevideo the capital?",
                    "Uruguay",
                    QuestionLevels.Normal
                )
            ),
            new GameQuestion(
                3,
                3,
                Question.CreateImage(
                    3,
                    QuestionCategories.Biology,
                    "What is the name of this freshwater fish that inhabits South American rivers and has extremely sharp teeth?",
                    "Piranha",
                    QuestionLevels.Normal,
                    "assets/piranha.png"
                )
              ),
              new GameQuestion(
                9,
                4,
                Question.CreateImage(
                    4,
                    QuestionCategories.Art,
                    "What is the french word for \"star\"?",
                    "Etoille",
                    QuestionLevels.Normal,
                    "assets/star.png"
                )
              ),
              new GameQuestion(
                7,
                5,
                Question.CreateVideo(
                    5,
                    QuestionCategories.Art,
                    "Which song is Katy Perry singing here?",
                    "Roar",
                    QuestionLevels.Normal,
                    new RemoteVideo("160883302", 75, 100, RemoteVideoSources.Vimeo)
                )
              ),
              new GameQuestion(
                6,
                6,
                Question.CreateImage(
                    6,
                    QuestionCategories.Art,
                    "Who is this wife of Harry in this picture?", 
                    "Markle, Meghan",
                    QuestionLevels.Normal,
                    "assets/markle.png",
                    true
                )
              ),
              new GameQuestion(
                2,
                7,
                Question.CreateVideo(
                    7,
                    QuestionCategories.Art,
                    "What is the name of the movie in which Lady Gaga sang this song? (4 words, first word)",
                    "A star is born",
                    QuestionLevels.Normal,
                    new RemoteVideo("308775088", 35, 55, RemoteVideoSources.Vimeo)
                )
              ),
              new GameQuestion(
                11,
                8,
                Question.CreateImage(
                    8,
                    QuestionCategories.Geography,
                    "What is the Indonesian name of this \"Hairy\" fruit?",
                    "Rambutan",
                    QuestionLevels.Normal,
                    "assets/rambutan.png"
                )
              ),
            new GameQuestion(
                12,
                9,
                Question.CreateText(
                    9,
                    QuestionCategories.Geography,
                    "What is the name of Zurich\"s international airport?",
                    "Kloten",
                    QuestionLevels.Normal
                )
            ),
            new GameQuestion(
                4,
                10,
                Question.CreateText(
                    10,
                    QuestionCategories.ScienceOrMaths,
                    "What do we call it when the moon moves in between the sun and earth, (partially) blocking sunlight? Solar ... ?",
                    "Eclips",
                    QuestionLevels.Normal
                )
            ),
            new GameQuestion(
                10,
                11,
                Question.CreateVideo(
                    11,
                    QuestionCategories.Art,
                    "Which movie (saga) is this?",
                    "Twilight",
                    QuestionLevels.Normal,
                    new RemoteVideo("134816480", 0, 25, RemoteVideoSources.Vimeo)
                )
              ),
              new GameQuestion(
                5,
                12,
                Question.CreateVideo(
                    12,
                    QuestionCategories.Music,
                    "Who sings \"Slow Down\" here? (first name)",
                    "Selena Gomez",
                    QuestionLevels.Normal,
                    new RemoteVideo("156668480", 30, 50, RemoteVideoSources.Vimeo)
                )
              )
      }
    );
  }
}