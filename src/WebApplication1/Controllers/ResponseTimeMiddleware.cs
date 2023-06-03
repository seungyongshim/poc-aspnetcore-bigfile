﻿using System.Diagnostics;

public class ResponseTimeMiddleware
{
    // Name of the Response Header, Custom Headers starts with "X-"  
    private const string RESPONSE_HEADER_RESPONSE_TIME = "X-Response-Time-ms";
    // Handle to the next Middleware in the pipeline  
    private readonly RequestDelegate _next;
    public ResponseTimeMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    public Task InvokeAsync(HttpContext context)
    {
        // Start the Timer using Stopwatch  
        var watch = new Stopwatch();
        watch.Start();
        context.Response.OnStarting(() => {
            // Stop the timer information and calculate the time   
            watch.Stop();
            var responseTimeForCompleteRequest = watch.ElapsedMilliseconds;
            // Add the Response time information in the Response headers.   
            context.Response.Headers[RESPONSE_HEADER_RESPONSE_TIME] = responseTimeForCompleteRequest.ToString();
            return Task.CompletedTask;
        });
        // Call the next delegate/middleware in the pipeline   
        return _next(context);
    }
}
