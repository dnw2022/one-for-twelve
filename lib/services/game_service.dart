import 'dart:convert';
import 'package:http/http.dart' as http;
import 'package:one_for_twelve/app_config.dart';

import '../models/game.dart';
import '../models/question_selection_strategies.dart';
import '../extensions/enum_extensions.dart';

class GameService {
  static Future<Game> create(
    String languageCode,
    QuestionSelectionStrategies strategy,
  ) async {
    final url =
        '${AppConfig.instance.backendBaseUrl}/games/${_getApiLanguage(languageCode)}/${strategy.asString()}';
    final resp = await http.get(Uri.parse(url));
    final result = json.decode(resp.body);

    return Game.fromMap(result);
  }

  static String _getApiLanguage(String languageCode) {
    return languageCode == 'nl' ? 'Dutch' : 'English';
  }
}
