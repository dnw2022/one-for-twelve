import { Question, QuestionCategories } from "./models";

const csv = require("fast-csv");
const fs = require("fs");

export class GameCache {
  private static words: String[] | null = null;
  private static questionsByFirstLetterAnswer: Map<String, Question[]> | null =
    null;

  public static getWords = (): String[] => {
    return GameCache.words ?? [];
  };

  public static getQuestionsByFirstLetterAnswer = (
    firstLetterAnswer: String
  ): Question[] => {
    return (
      (
        GameCache.questionsByFirstLetterAnswer ??
        ({} as Map<String, Question[]>)
      ).get(firstLetterAnswer) ?? []
    );
  };

  public static init = async (
    wordFiles: String[],
    questionFiles: String[]
  ): Promise<void> => {
    console.log("GameCache.init => start");
    console.time();

    const promises = [];

    if (GameCache.words === null) {
      console.log("Caching words");
      promises.push(GameCache.cacheWords(wordFiles));
      console.log("Cached words");
    }

    if (GameCache.questionsByFirstLetterAnswer === null) {
      console.log("Caching questions");
      promises.push(GameCache.cacheQuestions(questionFiles));
      console.log("Cached questions");
    }

    await Promise.all(promises);

    console.timeEnd();
    console.log("GameCache.init => finished");
  };

  private static cacheWords = async (files: String[]): Promise<void> => {
    return new Promise(async (resolve, reject) => {
      const promises: Promise<String[]>[] = [];
      files.forEach((f) => promises.push(GameCache.fetchWords(f)));

      GameCache.words = ([] as String[]).concat(
        ...(await Promise.all(promises))
      );

      resolve();
    });
  };

  private static fetchWords = async (file: String): Promise<String[]> => {
    return new Promise((resolve, reject) => {
      const words: String[] = [];

      fs.createReadStream(file)
        .pipe(csv.parse({ delimiter: ";", headers: true, trim: true }))
        .on("data", (data: any) => {
          words.push(data.word);
        })
        .on("end", () => {
          resolve(words);
        });
    });
  };

  private static cacheQuestions = async (files: String[]): Promise<void> => {
    return new Promise(async (resolve, reject) => {
      const promises: Promise<Question[]>[] = [];
      files.forEach((f) => promises.push(GameCache.getQuestions(f)));

      const questions = ([] as Question[]).concat(
        ...(await Promise.all(promises))
      );
      GameCache.questionsByFirstLetterAnswer = GameCache.groupBy(
        questions,
        (q: Question) => q.firstLetterAnswer
      );

      resolve();
    });
  };

  private static getQuestions = async (file: String): Promise<Question[]> => {
    return new Promise((resolve, reject) => {
      const questions: Question[] = [];

      fs.createReadStream(file)
        .pipe(csv.parse({ delimiter: ";", headers: true, trim: true }))
        .transform((row: any, next: any) => {
          return next(
            null,
            Question.createText(
              row.number,
              GameCache.getCategory(String(row.category)),
              row.question,
              row.answer,
              parseInt(row.m) - 1
            )
          );
        })
        .on("data", (row: Question) => {
          questions.push(row);
        })
        .on("end", () => {
          resolve(questions);
        });
    });
  };

  private static groupBy = <T, K>(
    list: T[],
    getKey: (item: T) => K
  ): Map<K, T[]> => {
    const map = new Map<K, T[]>();
    list.forEach((item) => {
      const key = getKey(item);
      const collection = map.get(key);
      if (!collection) {
        map.set(key, [item]);
      } else {
        collection.push(item);
      }
    });

    return map;
  };

  private static getCategory = (category: String): QuestionCategories => {
    switch (category) {
      case "AARD":
        return QuestionCategories.Geography;
      case "BIJB":
        return QuestionCategories.Bible;
      case "BIO":
        return QuestionCategories.Biology;
      case "CRYP":
        return QuestionCategories.Cryptic;
      case "ECO":
        return QuestionCategories.Economy;
      case "GES":
        return QuestionCategories.History;
      case "KUN":
        return QuestionCategories.Art;
      case "LIT":
        return QuestionCategories.Literature;
      case "MUZ":
        return QuestionCategories.Music;
      case "POL":
        return QuestionCategories.Politics;
      case "SPO":
        return QuestionCategories.Sports;
      case "REK":
        return QuestionCategories.ScienceOrMaths;
      case "WET":
        return QuestionCategories.ScienceOrMaths;
      default:
        return QuestionCategories.Unknown;
    }
  };
}
