import { Question, QuestionCategories } from "./models";

const csv = require("fast-csv");
const fs = require("fs");

export class GameCache {
  private static words: String[] | null = null;
  private static questionsByFirstLetterAnswer: Map<String, Question[]> | null =
    null;

  static get Words(): String[] {
    return GameCache.words ?? [];
  }
  static get QuestionsByFirstLetterAnswer(): Map<String, Question[]> {
    return (
      GameCache.questionsByFirstLetterAnswer ?? ({} as Map<String, Question[]>)
    );
  }

  public static Init = async (
    wordFiles: String[],
    questionFiles: String[]
  ): Promise<void> => {
    const promises = [];

    if (GameCache.words === null) {
      promises.push(GameCache.cacheWords(wordFiles));
    }

    if (GameCache.questionsByFirstLetterAnswer === null) {
      promises.push(GameCache.cacheQuestions(questionFiles));
    }

    await Promise.all(promises);
  };

  private static cacheWords = async (files: String[]): Promise<void> => {
    return new Promise(async (resolve, reject) => {
      const promises: Promise<String[]>[] = [];
      files.forEach((f) => promises.push(GameCache.getWords(f)));

      const words = ([] as String[]).concat(...(await Promise.all(promises)));
      GameCache.words = words;

      resolve();
    });
  };

  private static getWords = async (file: String): Promise<String[]> => {
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

  private static groupBy<T, K>(list: T[], getKey: (item: T) => K): Map<K, T[]> {
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
  }

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
