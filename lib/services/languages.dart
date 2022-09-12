import 'package:one_for_twelve/models/question_selection_strategies.dart';

class Languages {
  static List<String> getSupportedLanguageCodes() {
    return [
      "en",
      "nl",
    ];
  }

  static List<QuestionSelectionStrategies>
      getAvailableQuestionSelectionStrategies(String languageCode) {
    if (languageCode == "en") return [QuestionSelectionStrategies.Demo];

    return QuestionSelectionStrategies.values;
  }
}
