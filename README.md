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
