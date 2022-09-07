import 'package:firebase_auth/firebase_auth.dart';
import 'package:flutter/material.dart';
import 'package:one_for_twelve/screens/languages_screen.dart';
import 'package:provider/provider.dart';
import 'package:settings_ui/settings_ui.dart';

import '../services/auth.dart';
import '../widgets/size_config.dart';
import '../app_localizations.dart';
import '../settings_provider.dart';
import '../models/game_user.dart';

import './auth_screen.dart';

class SettingsScreen extends StatefulWidget {
  const SettingsScreen({super.key});

  @override
  SettingsScreenState createState() => SettingsScreenState();
}

class SettingsScreenState extends State<SettingsScreen> {
  final _auth = Auth.instance;

  Widget _getAccountLeading(GameUser? user) {
    if (user?.photoUrl == null) {
      return const Icon(Icons.account_circle);
    }

    return Padding(
      padding: const EdgeInsets.symmetric(vertical: 3.0),
      child: ClipOval(
        child: Image.network(user!.photoUrl,
            width: 25, height: 25, fit: BoxFit.cover),
      ),
    );
  }

  Future<GameUser?> _getUser() async {
    return await _auth.getCurrentUser();
  }

  @override
  Widget build(BuildContext context) {
    final sizeConfig = SizeConfig(context);
    final text = AppLocalizations.of(context);

    final appBar = AppBar(
      title: Text(text.translate('settings')),
      centerTitle: true,
    );

    final settingsProvider =
        Provider.of<SettingsProvider>(context, listen: false);

    return Scaffold(
      appBar: appBar,
      body: SingleChildScrollView(
        child: SafeArea(
          child: SizedBox(
            width: sizeConfig.safeBlockHorizontal * 100,
            height: (sizeConfig.safeBlockVertical * 100) -
                appBar.preferredSize.height,
            child: StreamBuilder<User?>(
              stream: _auth.onAuthStateChanged,
              builder: (context, snapshot) {
                return FutureBuilder<GameUser?>(
                  future: _getUser(),
                  builder: (context, snapshot) {
                    if (snapshot.connectionState == ConnectionState.waiting) {
                      return const Center(child: CircularProgressIndicator());
                    }

                    final user = snapshot.data;

                    return SettingsList(
                      sections: [
                        SettingsSection(
                          title: const Text('ACCOUNT'),
                          tiles: [
                            SettingsTile(
                              title: user?.providerName == null
                                  ? const Text('')
                                  : Text(user!.providerName),
                              value: user?.providerName == null
                                  ? Text(text.translate('not_logged_on'))
                                  : Text(user!.displayName),
                              leading: _getAccountLeading(user),
                              onPressed: (_) {
                                Navigator.of(context).push(MaterialPageRoute(
                                    builder: (BuildContext context) =>
                                        AuthScreen(user)));
                              },
                            ),
                          ],
                        ),
                        SettingsSection(
                          title: Text(text.translate('language').toUpperCase()),
                          tiles: [
                            SettingsTile(
                              title: Text(text.translate('language')),
                              value: Text(text.translate(
                                  'language_${settingsProvider.locale.languageCode}')),
                              leading: const Icon(Icons.language),
                              onPressed: (_) {
                                Navigator.of(context).push(MaterialPageRoute(
                                    builder: (BuildContext context) =>
                                        LanguagesScreen(
                                            settingsProvider
                                                .locale.languageCode,
                                            (l) => settingsProvider
                                                .setLocale(l))));
                              },
                            ),
                          ],
                        ),
                      ],
                    );
                  },
                );
              },
            ),
          ),
        ),
      ),
    );
  }
}
