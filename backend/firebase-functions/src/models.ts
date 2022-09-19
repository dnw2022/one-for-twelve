export class Game {
  public word: String;
  public numberOfQuestions: Number;
  public questions: GameQuestion[];

  constructor(word: String, questions: GameQuestion[]) {
    this.word = word;
    this.numberOfQuestions = questions.length;
    this.questions = questions;
  }
}

export class Question {
  public id: Number;
  public category: QuestionCategories;
  public answer: String;
  public text: String;
  public level: QuestionLevels;
  public imageUrl: String | null;
  public blurImage: boolean;
  public video: RemoteVideo | null;

  public firstLetterAnswer: String;

  protected constructor(
    id: Number,
    category: QuestionCategories,
    question: String,
    answer: String,
    level: QuestionLevels,
    imageUrl: String | null = null,
    blurImage: boolean = false,
    video: RemoteVideo | null = null
  ) {
    this.id = id;
    this.category = category;
    this.answer = answer;
    this.text = question;
    this.level = level;
    this.imageUrl = imageUrl;
    this.blurImage = blurImage;
    this.video = video;

    this.firstLetterAnswer = [...answer][0].toUpperCase();
  }

  static createText = (
    id: Number,
    category: QuestionCategories,
    question: String,
    answer: String,
    level: QuestionLevels
  ): Question => {
    return new Question(id, category, question, answer, level);
  };

  static createImage = (
    id: Number,
    category: QuestionCategories,
    question: String,
    answer: String,
    level: QuestionLevels,
    imageUrl: String,
    blurImage: boolean = false
  ): Question => {
    return new Question(
      id,
      category,
      question,
      answer,
      level,
      imageUrl,
      blurImage
    );
  };

  static createVideo = (
    id: Number,
    category: QuestionCategories,
    question: String,
    answer: String,
    level: QuestionLevels,
    video: RemoteVideo
  ): Question => {
    return new Question(
      id,
      category,
      question,
      answer,
      level,
      null,
      false,
      video
    );
  };
}

export class GameQuestion extends Question {
  public number: Number;
  public wordPosition: Number;

  constructor(number: Number, wordPosition: Number, question: Question) {
    super(
      question.id,
      question.category,
      question.text,
      question.answer,
      question.level,
      question.imageUrl,
      question.blurImage,
      question.video
    );

    this.number = number;
    this.wordPosition = wordPosition;
  }

  toJSON() {
    let src = this as any;
    let dest = {} as any;

    for (let key of Object.keys(src)) {
      if (src[key] !== null) {
        dest[key] = src[key];
      }
    }

    dest.category = QuestionCategories[this.category].toString();
    dest.level = QuestionLevels[this.level].toString();

    return dest;
  }
}

export class RemoteVideo {
  public videoId: String;
  public startAt: Number;
  public endAt: Number;
  public source: RemoteVideoSources;

  constructor(
    videoId: String,
    startAt: Number,
    endAt: Number,
    source: RemoteVideoSources
  ) {
    this.videoId = videoId;
    this.startAt = startAt;
    this.endAt = endAt;
    this.source = source;
  }
}

export enum RemoteVideoSources {
  Youtube,
  Vimeo,
}

export enum QuestionCategories {
  Unknown,
  Geography,
  Bible,
  Biology,
  Cryptic,
  Economy,
  History,
  Art,
  Literature,
  Music,
  Politics,
  Sports,
  ScienceOrMaths,
}

export enum QuestionSelectionStrategies {
  Demo,
  Random,
  RandomOnlyEasy,
}

export enum QuestionLevels {
  Easy,
  Normal,
  Hard,
}

export class QuestionSelector {
  public category: QuestionCategories;
  public firstLetterAnswer: String;
  public level: QuestionLevels;

  constructor(
    firstLetterAnswer: String,
    level: QuestionLevels,
    category: QuestionCategories
  ) {
    this.category = category;
    this.firstLetterAnswer = firstLetterAnswer;
    this.level = level;
  }
}

export class CacheStats {
  public wordCount: Number;
  public questionCount: Number;

  constructor(wordCount: Number, questionCount: Number) {
    this.wordCount = wordCount;
    this.questionCount = questionCount;
  }
}
