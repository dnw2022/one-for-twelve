import 'package:flutter/material.dart';

import 'package:provider/provider.dart';

import '../models/game.dart';
import '../models/game_user.dart';
import '../models/question_selection_strategies.dart';
import '../models/game_type.dart';

import '../app_localizations.dart';
import '../settings_provider.dart';

import '../services/auth.dart';
import '../services/game_factory.dart';

import '../extensions/enum_extensions.dart';

import './game_screen.dart';

class ChooseGameScreen extends StatefulWidget {
  const ChooseGameScreen({super.key});

  @override
  ChooseGameScreenState createState() => ChooseGameScreenState();
}

class ChooseGameScreenState extends State<ChooseGameScreen> {
  GameUser? _user;
  String? _languageCode;
  QuestionSelectionStrategies? _questionSelectionStrategy;
  bool _showGameSelectionScreen = false;
  GameType _gameType = GameType.Demo;

  @override
  void initState() {
    super.initState();

    Auth.instance.getCurrentUser().then((user) {
      setState(() {
        _user = user;
        _languageCode = _user!.gameSettings.languageCode;
        _questionSelectionStrategy =
            _user!.gameSettings.questionSelectionStrategy;
      });
    });
  }

  Future<void> _updateUserGameSettings() async {
    await _user!.gameSettings.setLanguageCode(_languageCode!);
    await _user!.gameSettings
        .setQuestionSelectionStrategy(_questionSelectionStrategy!);
    await _user!.gameSettings
        .setShowGameSelectionScreen(_showGameSelectionScreen);
  }

  Future<Game> _createGame() async {
    if (_languageCode == 'en') {
      return GameFactory.createDemoEn();
    }

    return _gameType == GameType.Demo
        ? GameFactory.createDemoNl()
        : GameFactory.createRandom(
            _questionSelectionStrategy!, _languageCode!, false);
  }

  Future<void> _newGame(BuildContext context) async {
    final game = await _createGame();

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
  }

  List<GameType> _getGameTypes() {
    if (_languageCode == 'nl') {
      return [GameType.Demo, GameType.Random];
    } else {
      return [GameType.Demo];
    }
  }

  @override
  Widget build(BuildContext context) {
    final text = AppLocalizations.of(context);

    return Scaffold(
      appBar: AppBar(
          title: Text(text.translate('choosegamescreen_title')),
          centerTitle: true),
      body: SafeArea(
        child: Center(
          child: _user == null
              ? const CircularProgressIndicator()
              : Padding(
                  padding: const EdgeInsets.all(4.0),
                  child: Column(
                    mainAxisAlignment: MainAxisAlignment.start,
                    crossAxisAlignment: CrossAxisAlignment.center,
                    children: <Widget>[
                      ListTile(
                        leading:
                            Text(text.translate('show_this_screen_in_future')),
                        title: Row(
                          mainAxisAlignment: MainAxisAlignment.end,
                          children: [
                            Switch.adaptive(
                              value: _showGameSelectionScreen,
                              onChanged: (value) {
                                setState(() {
                                  _showGameSelectionScreen = value;
                                });
                              },
                            ),
                          ],
                        ),
                      ),
                      ListTile(
                        leading: Text(text.translate('language')),
                        title: Row(
                          mainAxisAlignment: MainAxisAlignment.end,
                          children: [
                            DropdownButton(
                                value: _languageCode,
                                items: SettingsProvider.getSupportedLocales()
                                    .map((locale) {
                                  return DropdownMenuItem(
                                      value: locale.languageCode,
                                      child: Text(text.translate(
                                          'language_${locale.languageCode}')));
                                }).toList(),
                                onChanged: (value) {
                                  setState(() {
                                    _languageCode = value;
                                  });
                                })
                          ],
                        ),
                      ),
                      ListTile(
                        leading: Text(text.translate('game_type')),
                        title: Row(
                          mainAxisAlignment: MainAxisAlignment.end,
                          children: [
                            DropdownButton(
                                value: _gameType,
                                items: _getGameTypes().map((gameType) {
                                  return DropdownMenuItem(
                                      value: gameType,
                                      child: Text(text.translate(
                                          'game_type_${gameType.asString().toLowerCase()}')));
                                }).toList(),
                                onChanged: (value) {
                                  setState(() {
                                    _gameType = value!;
                                  });
                                })
                          ],
                        ),
                      ),
                      if (_gameType == GameType.Random)
                        ListTile(
                          leading: Text(text.translate('game_level')),
                          title: Row(
                            mainAxisAlignment: MainAxisAlignment.end,
                            children: [
                              DropdownButton(
                                  value: _questionSelectionStrategy,
                                  items: QuestionSelectionStrategies.values
                                      .map((strategy) {
                                    return DropdownMenuItem(
                                        value: strategy,
                                        child: Text(text.translate(
                                            'game_level_${strategy.index}')));
                                  }).toList(),
                                  onChanged: (value) {
                                    setState(() {
                                      _questionSelectionStrategy = value;
                                    });
                                  })
                            ],
                          ),
                        ),
                      ListTile(
                        title: Row(
                          mainAxisAlignment: MainAxisAlignment.end,
                          children: [
                            ElevatedButton(
                              child: Text(text.translate('start_game_text')),
                              onPressed: () async {
                                await _updateUserGameSettings();
                                await _newGame(context);
                              },
                            )
                          ],
                        ),
                      ),
                    ],
                  ),
                ),
        ),
      ),
    );
  }
}
