class ApiResponse<T> {
  final bool success;
  final String message;
  final T? data;

  const ApiResponse({
    required this.success,
    required this.message,
    required this.data,
  });

  factory ApiResponse.fromJson(dynamic response) {
    return ApiResponse(
      data: response['data'] as T,
      message: response['message'] as String,
      success: response['success'] as bool,
    );
  }
}