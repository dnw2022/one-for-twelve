import 'package:flutter/material.dart';
import 'package:shared_preferences/shared_preferences.dart';

class SettingsProvider with ChangeNotifier {
  static SharedPreferences? _prefs;
  static String _defaultLanguageCode = getSupportedLocales().first.languageCode;

  static Future<void> loadPreferences(String deviceLanguageCode) async {
    _prefs = await SharedPreferences.getInstance();

    if (_isSupportedLanguageCode(deviceLanguageCode)) {
      _defaultLanguageCode = deviceLanguageCode;
    }
  }

  static List<Locale> getSupportedLocales() {
    return [
      const Locale('en'),
      const Locale('nl'),
    ];
  }

  Locale get locale {
    final languageCode =
        _prefs?.getString('languageCode') ?? _defaultLanguageCode;
    return Locale(languageCode);
  }

  Future<void> setLocale(
    Locale newLocale, {
    bool saveInPrefs = true,
  }) async {
    if (locale.languageCode == newLocale.languageCode) return;

    if (!_isSupportedLanguageCode(newLocale.languageCode)) {
      return;
    }

    if (saveInPrefs) {
      await _prefs?.setString('languageCode', newLocale.languageCode);
    }

    notifyListeners();
  }

  static bool _isSupportedLanguageCode(String languageCode) {
    return getSupportedLocales().any((l) => l.languageCode == languageCode);
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
