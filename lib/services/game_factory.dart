import '../models/game.dart';

import '../services/google_cloud_functions.dart';

class GameFactory {
  static Future<Game> createDemoNl() async {
    final result = await GoogleCloudFunctions.callFunction('getDemoGameNl1');
    return Game.fromMap(result.data);
  }

  static Future<Game> createDemoEn() async {
    final result = await GoogleCloudFunctions.callFunction('getDemoGameEn1');
    return Game.fromMap(result.data);
  }
}
