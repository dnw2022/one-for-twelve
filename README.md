## Intro

Flutter version of the popular Dutch TV game show 'Twee voor 12'

## Initial creation

Create app:

```
flutter create one_for_twelve
```

## Using Firebase

In any directory run:

```
npm install -g firebase-tools (install firebase cli)
firebase login
dart pub global activate flutterfire_cli
```

Create project in the firebase console manually (one-for-12).

Then in project root run:

```
flutter pub add firebase_core
flutterfire configure (choose project one-for-twelve)
```

Now update the main.dart file so firebase is initialised when the app loads.

Test in all configurations: chrome, ios, android & macOS. In my case everything except macOS worked. For macOS I got a message:

Update the `platform :osx, '10.11'` line in your macOS/Podfile to version `10.12`

After doing this it worked on macOS as well.

## Add Email/Password authentication:

In the firebase console for the project enable Authentication and add Email/Password and Google authentication. Add a single User with email test@test.com and password password.

In the project root run:

```
flutter pub add firebase_auth
```

Add widgets for the login screen and profile page and update main.dart.

Test all configurations. On my laptop all except macOS worked. To get macOS to work I needed to do allow Outgoing Connection in the macos Runner configuration using xcode:

https://github.com/firebase/firebase-ios-sdk/issues/8939

## Add Google authentication:

https://firebase.google.com/docs/auth/flutter/federated-auth

In the firebase console add the Google authentication provider.

Add google_sign_in package to the project:

```
flutter pub add google_sign_in
```

Add a button to the login screen that allows logging in using Google.

For ios you need to add some settings to the Info.plist (see https://pub.dev/packages/google_sign_in). Make sure you follow the instructions carefully.
I also had build errors in ios and followed the instructions here (https://github.com/rmtmckenzie/flutter_qr_mobile_vision/issues/129) to fix it:

cd ios
pod cache clean --all
pod repo update
pod update
cd ..
flutter clean
flutter build ios
grep -r IUWebView ios/Pods

I also had to open the ios runner project in xcode and select the correct team under "Signing and Capabilities".

For android configure the sha1 signing key for the android app in the firebase console project. You can get the key with this command (default pwd is android):

```
keytool -list -v -alias androiddebugkey -keystore ~/.android/debug.keystore
```

For flutter web follow these instructions:

https://github.com/flutter/plugins/tree/master_archive/packages/google_sign_in/google_sign_in_web#web-integration

For macOS it is a bit more involved and you need a different package:

https://github.com/flutter/flutter/issues/46157
https://pub.dev/packages/flutterfire_ui/example

## Next steps

Added Splash Screen

## Troubleshooting

When enabling a firebase feature you must run 'flutterfire configure' again.

When using the firebase emulators for lambda functions the projectId must be specified in the .firebaserc file in the project root. The projectId becomes part of the url.

# Speeding up the xcode build for ios & macOS

If you use firebase, about 500k of c++ have to be compiled. To speed up the build you can use the precompiled code from git. See:

https://firebase.google.com/docs/firestore/quickstart
https://github.com/firebase/flutterfire/issues/9015
