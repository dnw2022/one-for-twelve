import 'package:flutter/material.dart';
import 'package:shared_preferences/shared_preferences.dart';

class SettingsProvider with ChangeNotifier {
  static SharedPreferences? _prefs;

  static Future<void> loadPreferences() async {
    _prefs = await SharedPreferences.getInstance();
  }

  static List<Locale> getSupportedLocales() {
    return [
      const Locale('nl'),
      const Locale('en'),
    ];
  }

  Locale get locale {
    final languageCode = _prefs?.getString('language_code') ??
        getSupportedLocales().first.languageCode;
    return Locale(languageCode);
  }

  Future<void> setLocale(Locale newLocale) async {
    if (locale.languageCode == newLocale.languageCode) return;
    if (!getSupportedLocales()
        .any((l) => l.languageCode == newLocale.languageCode)) {
      return;
    }

    await _prefs?.setString('language_code', newLocale.languageCode);

    notifyListeners();
  }

  bool get darkTheme => false;
}
