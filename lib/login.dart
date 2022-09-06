import 'package:flutter/foundation.dart';

import 'package:firebase_auth/firebase_auth.dart';
import 'package:flutter/material.dart';
import 'package:google_sign_in/google_sign_in.dart';

import './firebase_options.dart';

class Login extends StatelessWidget {
  Login({super.key});
  final FirebaseAuth _auth = FirebaseAuth.instance;

  Future<UserCredential> signInWithUsernamePassword() async {
    return await _auth.signInWithEmailAndPassword(
      email: 'test@test.com',
      password: 'password',
    );
  }

  Future<UserCredential> signInWithGoogle() async {
    // Trigger the authentication flow
    final GoogleSignInAccount? googleUser = await GoogleSignIn(
            clientId: DefaultFirebaseOptions.currentPlatform.iosClientId)
        .signIn();

    // Obtain the auth details from the request
    final GoogleSignInAuthentication? googleAuth =
        await googleUser?.authentication;

    // Create a new credential
    final credential = GoogleAuthProvider.credential(
      accessToken: googleAuth?.accessToken,
      idToken: googleAuth?.idToken,
    );

    // Once signed in, return the UserCredential
    return await FirebaseAuth.instance.signInWithCredential(credential);
  }

  @override
  Widget build(BuildContext context) {
    return Column(
      mainAxisAlignment: MainAxisAlignment.center,
      children: [
        ElevatedButton(
          child: const Text('Login with email & pwd'),
          onPressed: () => signInWithUsernamePassword(),
        ),
        ElevatedButton(
          child: const Text('Login with Google'),
          onPressed: () => signInWithGoogle(),
        )
      ],
    );
  }
}
