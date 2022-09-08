import '../models/game.dart';
import '../models/question_selection_strategies.dart';

import '../services/google_cloud_functions.dart';

import '../extensions/enum_extensions.dart';

class GameFactory {
  static Future<Game> createRandom(QuestionSelectionStrategies strategy,
      String languageCode, bool useUnrevisedQuestions) async {
    final result = await GoogleCloudFunctions.callFunction(
        useUnrevisedQuestions
            ? 'getRandomGameWithUnrevisedQuestions'
            : 'getRandomGame',
        <String, dynamic>{
          'questionSelectionStrategy': strategy.asString(),
          'languageCode': languageCode
        });

    return Game.fromMap(result.data);
  }

  static Future<Game> createDemoNl() async {
    final result = await GoogleCloudFunctions.callFunction('getDemoGameNl1');
    return Game.fromMap(result.data);
  }

  static Future<Game> createDemoEn() async {
    final result = await GoogleCloudFunctions.callFunction('getDemoGameEn1');
    return Game.fromMap(result.data);
  }
}
