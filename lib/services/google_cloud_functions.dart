import 'dart:io';

import 'package:cloud_functions/cloud_functions.dart';

import '../app_config.dart';

class GoogleCloudFunctions {
  static Future<HttpsCallableResult> callFunction(String name,
      [dynamic parameters]) async {
    final caller = FirebaseFunctions.instanceFor(
        region: AppConfig.instance.cloudFunctionsRegion);

    if (AppConfig.instance.useEmulator) {
      print('Using emulator. Platform ${Platform.isIOS}');
      caller.useFunctionsEmulator(
          Platform.isIOS ? '0.0.0.0' : '10.0.2.2', 5001);
    }

    final callable = caller.httpsCallable(name);

    return await callable.call(parameters);
  }
}
