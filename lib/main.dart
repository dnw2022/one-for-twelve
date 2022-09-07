import 'package:firebase_auth/firebase_auth.dart';
import 'package:flutter/material.dart';
import 'package:firebase_core/firebase_core.dart';

import './firebase_options.dart';

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
  Future<FirebaseApp> initApp() async {
    await Future.delayed(const Duration(seconds: 1));

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
    return MaterialApp(
      title: 'Flutter Demo',
      theme: ThemeData(
        primarySwatch: Colors.blue,
      ),
      onGenerateRoute: (routeSettings) {
        if (routeSettings.name == "/home") {
          return PageRouteBuilder(
            pageBuilder: (_, a1, a2) => _buildHomeScreen(),
          );
        }

        return null;
      },
      home: FutureBuilder(
          future: initApp(),
          builder: (context, snapshot) {
            if (snapshot.hasError) {
              return Text(snapshot.error.toString());
            } else if (snapshot.hasData) {
              return _buildHomeScreen();
            } else {
              return const Center(child: SplashScreen());
            }
          }),
    );
  }
}
