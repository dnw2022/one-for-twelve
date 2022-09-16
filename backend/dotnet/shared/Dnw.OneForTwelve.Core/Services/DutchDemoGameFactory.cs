
// ReSharper disable StringLiteralTypo

using Dnw.OneForTwelve.Core.Models;

namespace Dnw.OneForTwelve.Core.Services;

public class DutchDemoGameFactory : IDemoGameFactory {
  public Languages Language => Languages.Dutch;

  public Game GetGame() {
    return new Game("buschauffeur", new List<GameQuestion> { 
        new GameQuestion(
          8,
          1,
          Question.CreateImage(
            1,
            QuestionCategories.Sports,
            "Wie is deze basketballer die begin 2020 samen met zijn dochter om het leven kwam bij een helikoper ongeluk?",
            "Bryant, Kobe",
            QuestionLevels.Normal,
            "assets/bryant.png"
          )
        ),
        new GameQuestion(
          1,
          2,
          Question.CreateVideo(
            2,
            QuestionCategories.Music,
            "Welke britse groep zong o.a. dit nummer 'Kingston Town'?",
            "UB40",
            QuestionLevels.Normal,
            new RemoteVideo("154445067", 35, 55, RemoteVideoSources.Vimeo)
          )
        ),
        new GameQuestion(
          3,
          3,
          Question.CreateImage(
            3,
            QuestionCategories.Biology,
            "Wat is de naam van deze Japanse rauwe vis lekkernij?",
            "Sashimi",
            QuestionLevels.Normal,
            "assets/sashimi.png"
          )
        ),
        new GameQuestion(
          9,
          4,
          Question.CreateImage(
            4,
            QuestionCategories.Art,
            "Wie is deze filmster die ook bekend werd met zijn Nespresso reclames?",
            "Clooney, George",
            QuestionLevels.Normal,
            "assets/clooney.png",
            true
          )
        ),
        new GameQuestion(
          7,
          5,
          Question.CreateImage(
            5,
            QuestionCategories.Art,
            "Wie is de acteur die samen met Tom Cruise in de film Rainman speelde?",
            "Hoffman, Dustin",
            QuestionLevels.Normal,
            "assets/hoffman.png"
          )
        ),
        new GameQuestion(
          6,
          6,
          Question.CreateVideo(
            6,
            QuestionCategories.Art,
            "O'G3NE zingt hier het nummer Clown in De Beste Zangers 2016. De zussen heten Lisa, Shelley en ... wie is de derde?",
            "Amy",
            QuestionLevels.Normal,
            new RemoteVideo("167597368", 50, 70, RemoteVideoSources.Vimeo)
          )
        ),
        new GameQuestion(
          2,
          7,
          Question.CreateText(
            7,
            QuestionCategories.Cryptic,
            "Cryptisch: Doven die gaan stappen ...",
            "Uitgaan",
            QuestionLevels.Normal
          )
        ),
        new GameQuestion(
          11,
          8,
          Question.CreateImage(
            8,
            QuestionCategories.Geography,
            "Wat is de naam van deze bekendse Japanse berg?",
            "Fuji",
            QuestionLevels.Normal,
            "assets/fuji.png"
          )
        ),
        new GameQuestion(
          12,
          9,
          Question.CreateImage(
            9,
            QuestionCategories.Biology,
            "Hoe wordt het proces genoemd waarbij in de bladgroenkorrels van planten CO2 wordt omgezet in zuurstof?",
            "Fotosynthese",
            QuestionLevels.Normal,
            "assets/fotosynthese.png"
          )
        ),
        new GameQuestion(
          4,
          10,
          Question.CreateVideo(
            10,
            QuestionCategories.Art,
            "Welke groep had een grote hit met het nummer 'more than words'?",
            "Extreme",
            QuestionLevels.Normal,
            new RemoteVideo("500134973", 60, 90, RemoteVideoSources.Vimeo)
          )
        ),
        new GameQuestion(
          10,
          11,
          Question.CreateImage(
            11,
            QuestionCategories.Music,
            "Dit is het nationale instrument van Hawai. Wat is de naam?",
            "Ukulele",
            QuestionLevels.Normal,
            "assets/ukulele.png"
          )
        ),
        new GameQuestion(
          5,
          12,
          Question.CreateImage(
            12,
            QuestionCategories.Biology,
            "Wat is de naam van de grootste bloem ter wereld?",
            "Rafflesia",
            QuestionLevels.Normal,
            "assets/rafflesia.png"
          )
        ),
      }
    );
  }
}