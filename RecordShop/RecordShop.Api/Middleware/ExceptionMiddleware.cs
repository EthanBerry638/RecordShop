namespace RecordShop.Api.Middleware
{
    public class ExceptionMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (ArgumentException ex)
            {
                await HandleArgumentException(ex, context);
            }
            catch (Exception ex)
            {
                await HandleGeneralException(ex, context);
            }
        }

        private async Task HandleGeneralException(Exception ex, HttpContext context)
        {
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await context.Response.WriteAsJsonAsync(new { message = "Server is down :(" });
        }

        private async Task HandleArgumentException(Exception ex, HttpContext context)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            await context.Response.WriteAsJsonAsync(new { message = "Please enter a number greater than 0" });
        }
    }
}
