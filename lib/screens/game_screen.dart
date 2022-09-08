import 'package:flutter/material.dart';
import 'package:provider/provider.dart';

import '../models/game.dart';
import '../widgets/size_config.dart';

class GameScreen extends StatefulWidget {
  const GameScreen({super.key});

  @override
  GameScreenState createState() {
    return GameScreenState();
  }
}

class GameScreenState extends State<GameScreen> {
  @override
  Widget build(BuildContext context) {
    final sizeConfig = SizeConfig(context);

    return Consumer<Game>(builder: (_, game, __) {
      final appBar = AppBar(
        automaticallyImplyLeading: false,
        leading: Container(),
        title: const Text('Game'),
        centerTitle: true,
        actions: [
          GestureDetector(
            onTap: () => Navigator.of(context).pushReplacementNamed('/home'),
            child: const Tooltip(
              message: 'Cancel game',
              child: Padding(
                padding: EdgeInsets.only(right: 8.0),
                child: Icon(
                  Icons.exit_to_app,
                  size: 30,
                  color: Colors.white,
                ),
              ),
            ),
          ),
        ],
      );

      return Scaffold(
        resizeToAvoidBottomInset: true,
        appBar: appBar,
        body: SingleChildScrollView(
          child: SafeArea(
            child: SizedBox(
              width: sizeConfig.safeBlockHorizontal * 100,
              height: (sizeConfig.safeBlockVertical * 100) -
                  appBar.preferredSize.height,
              child: Column(
                children: <Widget>[
                  Text(game.word!),
                ],
              ),
            ),
          ),
        ),
      );
    });
  }
}
