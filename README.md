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
