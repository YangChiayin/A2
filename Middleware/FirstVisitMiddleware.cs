namespace Chiayin_Yang_Assignment2.Middleware
{
    public class FirstVisitMiddleware
    {
        private readonly RequestDelegate _next;

        public FirstVisitMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Cookies["FirstVisit"] == null)
            {
                // It's their first visit
                var firstVisitTime = DateTime.Now.ToString("MM/dd/yyyy h:mm:ss tt");
                context.Response.Cookies.Append("FirstVisit", firstVisitTime, new CookieOptions
                {
                    Expires = DateTimeOffset.UtcNow.AddDays(30)
                });
                context.Items["FirstVisit"] = firstVisitTime;
                context.Items["IsFirstVisit"] = true;  // Add this flag for first-time visitors
            }
            else
            {
                // Returning visitor
                context.Items["FirstVisit"] = context.Request.Cookies["FirstVisit"];
                context.Items["IsFirstVisit"] = false;  // They're not a first-time visitor
            }

            await _next(context);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class FirstVisitMiddlewareExtensions
    {
        public static IApplicationBuilder UseFirstVisitMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<FirstVisitMiddleware>();
        }
    }
}
