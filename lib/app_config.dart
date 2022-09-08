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
    const env = String.fromEnvironment('env', defaultValue: 'dev');

    final contents = await rootBundle.loadString(
      'assets/config/$env.json',
    );

    final json = jsonDecode(contents);

    _instance = AppConfig._(json);
  }
}
