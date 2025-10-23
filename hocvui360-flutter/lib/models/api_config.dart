mixin ApiConfig {
  final String _apiVersion = '1';
  final String _httpScheme = 'http';
  final String _apiHost = '192.168.2.9';
  final int _apiPort = 3000;
  final String _apiContextPath = 'api';

  Uri get apiUri => Uri(scheme: _httpScheme, host: _apiHost, port: _apiPort, path: _apiContextPath);

  Uri buildUri(String endPoint, {Map<String, dynamic>? queryParameters}) {
    return apiUri.replace(
        path: '${apiUri.path}$endPoint',
        queryParameters: queryParameters
    );
  }

  String get apiBaseUri => '$_httpScheme://$_apiHost:$_apiPort/$_apiContextPath';
}
