import 'package:flutter/material.dart';
import 'package:provider/provider.dart';

import '../models/game_user.dart';
import '../models/game.dart';

import '../services/auth.dart';
import '../services/game_factory.dart';

import './settings_screen.dart';
import './game_screen.dart';
import './choose_game_screen.dart';

import '../app_localizations.dart';

class HomeScreen extends StatelessWidget {
  const HomeScreen({super.key});

  void _chooseGame(BuildContext context) async {
    Navigator.push(
      context,
      MaterialPageRoute<GameScreen>(
        builder: (_) {
          return ChooseGameScreen();
        },
      ),
    );
  }

  void _startGame(BuildContext context, GameUser user) async {
    try {
      final game = await GameFactory.create(user.gameSettings.languageCode,
          user.gameSettings.questionSelectionStrategy);

      Navigator.pushReplacement(
        context,
        MaterialPageRoute<GameScreen>(
          builder: (_) {
            return ChangeNotifierProvider<Game>.value(
              value: game,
              child: GameScreen(),
            );
          },
        ),
      );
    } on Exception catch (ex) {
      showDialog(
          context: context,
          builder: (BuildContext context) {
            return AlertDialog(
              title: const Text("Error"),
              content: Text(ex.toString()),
            );
          });
    }
  }

  @override
  Widget build(BuildContext context) {
    final text = AppLocalizations.of(context);

    return Scaffold(
      appBar: AppBar(
        title: Text(text.translate('homescreen.appbar.title')),
        centerTitle: true,
        titleSpacing: 8,
        leading: FutureBuilder<GameUser?>(
            future: Auth.instance.getCurrentUser(),
            builder: (context, snapshot) {
              if (snapshot.connectionState == ConnectionState.waiting) {
                return const Center(child: CircularProgressIndicator());
              }

              // todo: what if user is null?
              final user = snapshot.data!;

              return PopupMenuButton<String>(
                icon: const Icon(Icons.more_vert),
                onSelected: (String value) {
                  if (value == 'settings') {
                    Navigator.of(context).push(
                      MaterialPageRoute(builder: (_) {
                        return const SettingsScreen();
                      }),
                    );
                  } else if (value == 'administration') {
                    Navigator.of(context).push(
                      MaterialPageRoute(builder: (_) {
                        return const Text('administration_screen');
                        //return AdministrationScreen();
                      }),
                    );
                  }
                },
                itemBuilder: (BuildContext context) => [
                  PopupMenuItem<String>(
                    value: "settings",
                    child: Row(
                      children: [
                        Icon(Icons.settings,
                            color: Theme.of(context).iconTheme.color),
                        const SizedBox(width: 5),
                        Text(text.translate('settings')),
                      ],
                    ),
                  ),
                  if (user.isSuperUser)
                    PopupMenuItem<String>(
                      value: "administration",
                      child: Row(
                        children: [
                          Icon(Icons.build,
                              color: Theme.of(context).iconTheme.color),
                          const SizedBox(width: 5),
                          Text(text.translate('administration')),
                        ],
                      ),
                    )
                ],
              );
            }),
        actions: <Widget>[
          GestureDetector(
            onTap: () {},
            child: const Padding(
              padding: EdgeInsets.only(right: 4.0),
              child: Icon(Icons.help),
            ),
          ),
        ],
      ),
      body: SafeArea(
        child: Center(
          child: Padding(
            padding: const EdgeInsets.all(8.0),
            child: Column(
              mainAxisAlignment: MainAxisAlignment.spaceEvenly,
              children: <Widget>[
                ElevatedButton(
                  child: Text(text.translate('start_game_text')),
                  onPressed: () async {
                    final user = await Auth.instance.getCurrentUser();
                    if (user!.gameSettings.showGameSelectionScreen) {
                      _chooseGame(context);
                    } else {
                      _startGame(context, user);
                    }
                  },
                ),
                const Flexible(
                  child: Image(
                    image: AssetImage('assets/homer_desperate.png'),
                  ),
                ),
                Text(text.translate('homescreen.subtitle')),
              ],
            ),
          ),
        ),
      ),
    );
  }
}
