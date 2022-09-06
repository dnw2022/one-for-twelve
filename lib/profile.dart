import 'package:flutter/material.dart';
import 'package:firebase_auth/firebase_auth.dart';

class Profile extends StatelessWidget {
  Profile({super.key});
  final User _user = FirebaseAuth.instance.currentUser!;
  final FirebaseAuth _auth = FirebaseAuth.instance;

  @override
  Widget build(BuildContext context) {
    return Center(
      child: Column(
        mainAxisAlignment: MainAxisAlignment.center,
        children: [
          Text('UserId=${_user.uid.toString()}'),
          const SizedBox(
            height: 10,
          ),
          ElevatedButton(
            child: const Text('Logout'),
            onPressed: () {
              _auth.signOut();
            },
          )
        ],
      ),
    );
  }
}
