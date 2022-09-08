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

  bool get isBrightnessBasedOnPhone =>
      _prefs?.getBool('isBrightnessBasedOnPhone') ?? true;
  Future<void> setIsBrightnessBasedOnPhone(bool value) async {
    await _prefs?.setBool('isBrightnessBasedOnPhone', value);
    notifyListeners();
  }

  static Brightness _platformBrightness = Brightness.light;
  void setPlatformBrightness(Brightness brightness) {
    _platformBrightness = brightness;

    if (isBrightnessBasedOnPhone) {
      notifyListeners();
    }
  }

  bool get useDarkMode => _prefs?.getBool('useDarkMode') ?? true;
  Future<void> setUseDarkMode(bool value) async {
    _prefs?.setBool('useDarkMode', value);
    notifyListeners();
  }

  ThemeMode get themeMode {
    if (isBrightnessBasedOnPhone) {
      return _platformBrightness == Brightness.dark
          ? ThemeMode.dark
          : ThemeMode.light;
    }

    return useDarkMode ? ThemeMode.dark : ThemeMode.light;
  }
}
