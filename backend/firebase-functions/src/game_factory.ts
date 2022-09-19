import {
  Game,
  QuestionSelector,
  QuestionSelectionStrategies,
  GameQuestion,
  Question,
} from "./models";
import { GameCache } from "./game_cache";
import { QuestionSelectionStrategyFactory } from "./question_selection";

export class GameFactory {
  public static getRandom = async (strategy: String): Promise<Game> => {
    const questionSelectionStrategy =
      GameFactory.getQuestionSelectionStrategy(strategy);

    do {
      const randomWord = GameFactory.getRandomWord();
      const questionSelectors = GameFactory.getQuestionSelectors(
        questionSelectionStrategy,
        randomWord
      );

      if (randomWord.length === questionSelectors.length) {
        let gameQuestions = GameFactory.getRandomQuestions(
          questionSelectors
        ).map(
          (question, index) => new GameQuestion(index + 1, index + 1, question)
        );
        GameFactory.shuffle(gameQuestions);
        gameQuestions = gameQuestions.map(
          (gameQuestion, index) =>
            new GameQuestion(index + 1, gameQuestion.wordPosition, gameQuestion)
        );
        return new Game(randomWord, gameQuestions);
      }
    } while (true);
  };

  public static getRandomQuestion = (selector: QuestionSelector) => {
    const questions = GameFactory.getPossibleQuestions(selector);
    const count = questions.length;
    const randomIndex = Math.floor(Math.random() * count);
    return questions[randomIndex];
  };

  public static getPossibleQuestions = (
    selector: QuestionSelector
  ): Question[] => {
    return (
      GameCache.QuestionsByFirstLetterAnswer.get(
        selector.firstLetterAnswer
      )?.filter(
        (q) => q.category === selector.category && q.level === selector.level
      ) ?? []
    );
  };

  private static getQuestionSelectionStrategy = (strategy: String) => {
    if (strategy === null || strategy === undefined)
      return QuestionSelectionStrategies.Random;

    switch (strategy.toUpperCase()) {
      case QuestionSelectionStrategies[
        QuestionSelectionStrategies.RandomOnlyEasy
      ].toUpperCase():
        return QuestionSelectionStrategies.RandomOnlyEasy;
      default:
        return QuestionSelectionStrategies.Random;
    }
  };

  private static getQuestionSelectors = (
    strategy: QuestionSelectionStrategies,
    word: String
  ) => {
    const selectionStrategy = QuestionSelectionStrategyFactory.create(strategy);

    let questionSelectors = [];
    let attempt = 1;

    do {
      questionSelectors = selectionStrategy.getSelectors(word);
      attempt++;
    } while (attempt <= 3 && questionSelectors.length < 12);

    if (attempt > 3) {
      console.log(`Could not create game for word: ${word}`);
    }

    return questionSelectors;
  };

  private static getRandomWord = (): String => {
    const count = GameCache.Words.length;
    const randomIndex = Math.floor(Math.random() * count);
    return GameCache.Words[randomIndex];
  };

  private static getRandomQuestions = (selectors: QuestionSelector[]) => {
    return selectors.map((selector) => {
      return GameFactory.getRandomQuestion(selector);
    });
  };

  private static shuffle(array: any) {
    for (let i = array.length - 1; i > 0; i--) {
      const j = Math.floor(Math.random() * (i + 1)); // random index from 0 to i

      // swap elements array[i] and array[j]
      // we use "destructuring assignment" syntax to achieve that
      // you'll find more details about that syntax in later chapters
      // same can be written as:
      // let t = array[i]; array[i] = array[j]; array[j] = t
      [array[i], array[j]] = [array[j], array[i]];
    }
  }
}
