import '../models/game.dart';
import '../models/question_selection_strategies.dart';

import '../services/google_cloud_functions.dart';

import '../extensions/enum_extensions.dart';

class GameFactory {
  static Future<Game> create(
    String languageCode,
    QuestionSelectionStrategies strategy,
  ) async {
    if (languageCode == "en") {
      return await _createDemoEn();
    }

    return strategy == QuestionSelectionStrategies.Demo
        ? await _createDemoNl()
        : await _createRandom(strategy, languageCode, false);
  }

  static Future<Game> _createRandom(
    QuestionSelectionStrategies strategy,
    String languageCode,
    bool useUnrevisedQuestions,
  ) async {
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

  static Future<Game> _createDemoNl() async {
    final result = await GoogleCloudFunctions.callFunction('getDemoGameNl1');
    return Game.fromMap(result.data);
  }

  static Future<Game> _createDemoEn() async {
    final result = await GoogleCloudFunctions.callFunction('getDemoGameEn1');
    return Game.fromMap(result.data);
  }
}
