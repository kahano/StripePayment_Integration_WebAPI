﻿namespace E_commercial_Web_RESTAPI.Extensions
{
    public class ApiResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public int StatusCode { get; set; }
    }
}
