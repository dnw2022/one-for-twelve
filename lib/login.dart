import 'package:firebase_auth/firebase_auth.dart';
import 'package:flutter/material.dart';

class Login extends StatelessWidget {
  Login({super.key});
  final FirebaseAuth _auth = FirebaseAuth.instance;

  Future<UserCredential> signInWithUsernamePassword() async {
    return await _auth.signInWithEmailAndPassword(
      email: 'test@test.com',
      password: 'password',
    );
  }

  @override
  Widget build(BuildContext context) {
    return Column(
      mainAxisAlignment: MainAxisAlignment.center,
      children: [
        ElevatedButton(
          child: const Text('Login with email & pwd'),
          onPressed: () => signInWithUsernamePassword(),
        )
      ],
    );
  }
}
