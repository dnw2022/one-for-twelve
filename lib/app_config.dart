import 'dart:convert';
import 'package:flutter/services.dart';

class AppConfig {
  static AppConfig? _instance;
  final dynamic _json;

  String get cloudFunctionsRegion => _json['cloudFunctionsRegion'];
  bool get useEmulator => _json['useEmulator'] ?? false;

  AppConfig._(this._json);

  static AppConfig get instance => _instance!;

  static Future<void> init() async {
    const env = String.fromEnvironment('FIREBASE_HOSTING_ENVIRONMEMT',
        defaultValue: 'dev');

    const jsonSettingsKey = 'assets/config/$env.json';
    final contents = await rootBundle.loadString(
      jsonSettingsKey,
    );

    final json = jsonDecode(contents);

    _instance = AppConfig._(json);
  }
}
