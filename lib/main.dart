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
  await Firebase.initializeApp(
    options: DefaultFirebaseOptions.currentPlatform,
  );

  final window = WidgetsBinding.instance.window;

  await SettingsProvider.loadPreferences(window.locale.languageCode);

  final settingsProvider = SettingsProvider();
  settingsProvider.setPlatformBrightness(window.platformBrightness);

  window.onPlatformBrightnessChanged = () {
    settingsProvider.setPlatformBrightness(window.platformBrightness);
  };

  window.onLocaleChanged = () {
    settingsProvider.setLocale(window.locale, saveInPrefs: false);
  };

  runApp(MyApp(settingsProvider));
}

class MyApp extends StatefulWidget {
  final SettingsProvider _settingsProvider;

  const MyApp(this._settingsProvider, {super.key});

  @override
  State<MyApp> createState() => _MyAppState();
}

class _MyAppState extends State<MyApp> {
  SettingsProvider get _settingsProvider => widget._settingsProvider;

  @override
  void initState() {
    super.initState();
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
    return MultiProvider(
      providers: [
        ChangeNotifierProvider<SettingsProvider>(
          create: (_) {
            return _settingsProvider;
          },
        )
      ],
      child: Consumer<SettingsProvider>(
        builder: (BuildContext ctx, SettingsProvider settings, Widget? child) {
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
            theme: ThemeData(
              brightness: Brightness.light,
              primarySwatch: Colors.blue,
            ),
            darkTheme: ThemeData(
              scaffoldBackgroundColor: Colors.black,
              backgroundColor: Colors.black,
              primaryColor: Colors.blue,
              colorScheme: ColorScheme.fromSwatch().copyWith(
                primary: Colors.black,
                secondary: Colors.blue,
                brightness: Brightness.dark,
              ),
              appBarTheme: const AppBarTheme(
                  backgroundColor: Colors.blue, foregroundColor: Colors.white),
            ),
            themeMode: settings.themeMode,
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
  }
}
