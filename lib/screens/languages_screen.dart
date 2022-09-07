import 'package:flutter/material.dart';
import 'package:one_for_twelve/settings_provider.dart';
import 'package:settings_ui/settings_ui.dart';

import '../app_localizations.dart';

class LanguagesScreen extends StatefulWidget {
  final String _currentLanguageCode;
  final Function(Locale locale) _onLanguageChosen;

  const LanguagesScreen(this._currentLanguageCode, this._onLanguageChosen,
      {super.key});

  @override
  LanguagesScreenState createState() => LanguagesScreenState();
}

class LanguagesScreenState extends State<LanguagesScreen> {
  @override
  Widget build(BuildContext context) {
    final text = AppLocalizations.of(context);
    final supportedlocales = SettingsProvider.getSupportedLocales();

    return Scaffold(
      appBar: AppBar(
        title: Text(text.translate('languages')),
        centerTitle: true,
      ),
      body: SettingsList(
        sections: [
          SettingsSection(
              tiles: supportedlocales.map((l) {
            return SettingsTile(
              title: Text(text.translate('language_${l.languageCode}')),
              trailing:
                  trailingWidget(widget._currentLanguageCode == l.languageCode),
              onPressed: (_) {
                widget._onLanguageChosen(l);
                Navigator.of(context).pop();
              },
            );
          }).toList()),
        ],
      ),
    );
  }

  Widget trailingWidget(bool isSelected) {
    return (isSelected)
        ? const Icon(Icons.check, color: Colors.blue)
        : const Icon(null);
  }
}
