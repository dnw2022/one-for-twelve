import 'package:firebase_auth/firebase_auth.dart';
import 'package:flutter/material.dart';
import 'package:flutter_localizations/flutter_localizations.dart';
import 'package:provider/provider.dart';
import 'package:firebase_core/firebase_core.dart';

import './firebase_options.dart';
import './app_localizations.dart';
import './settings_provider.dart';

import './screens/splash_screen.dart';
import './screens/auth_screen.dart';
import './screens/home_screen.dart';

Future<void> main() async {
  WidgetsFlutterBinding.ensureInitialized();
  runApp(const MyApp());
}

class MyApp extends StatefulWidget {
  const MyApp({super.key});

  @override
  State<MyApp> createState() => _MyAppState();
}

class _MyAppState extends State<MyApp> {
  Future<FirebaseApp> init() async {
    await Future.delayed(const Duration(seconds: 1));
    await SettingsProvider.loadPreferences();

    final app = await Firebase.initializeApp(
      options: DefaultFirebaseOptions.currentPlatform,
    );

    return app;
  }

  Widget _buildHomeScreen() {
    return StreamBuilder(
      stream: FirebaseAuth.instance.authStateChanges(),
      builder: (_, snapshot) {
        if (snapshot.connectionState == ConnectionState.waiting) {
          return const SplashScreen();
        }

        return snapshot.hasData ? const HomeScreen() : AuthScreen(null);
      },
    );
  }

  @override
  Widget build(BuildContext context) {
    return FutureBuilder(
        future: init(),
        builder: (context, snapshot) {
          if (snapshot.connectionState == ConnectionState.waiting) {
            return const Center(child: CircularProgressIndicator());
          }

          return MultiProvider(
            providers: [
              ChangeNotifierProvider<SettingsProvider>(
                create: (_) {
                  return SettingsProvider();
                },
              )
            ],
            child: Consumer<SettingsProvider>(
              builder:
                  (BuildContext ctx, SettingsProvider settings, Widget? child) {
                return MaterialApp(
                  debugShowCheckedModeBanner: false,
                  locale: settings.locale,
                  supportedLocales: SettingsProvider.getSupportedLocales(),
                  localizationsDelegates: const [
                    AppLocalizations.delegate,
                    GlobalMaterialLocalizations.delegate,
                    GlobalWidgetsLocalizations.delegate,
                    GlobalCupertinoLocalizations.delegate,
                  ],
                  onGenerateRoute: (routeSettings) {
                    if (routeSettings.name == "/home") {
                      return PageRouteBuilder(
                        pageBuilder: (_, a1, a2) => _buildHomeScreen(),
                      );
                    }

                    return null;
                  },
                  home: _buildHomeScreen(),
                );
              },
            ),
          );
        });
  }
}
