import 'package:flutter/material.dart';

class SettingsProvider with ChangeNotifier {
  static List<Locale> getSupportedLocales() {
    return [const Locale('en'), const Locale('nl')];
  }

  Locale? _locale;
  Locale get locale => _locale ?? const Locale('nl');
  set locale(Locale value) {
    if (locale.languageCode != _locale?.languageCode) {
      _locale = value;
      // _preferences.setLocale(value);
      notifyListeners();
    }
  }

  void setLocale(Locale locale) {
    if (locale.languageCode != _locale?.languageCode) {
      _locale = locale;
      // _preferences.setLocale(locale);
      notifyListeners();
    }
  }

  bool get darkTheme => false;
}
