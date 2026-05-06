using RecordShop.Api.CustomExceptions;

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
            catch (LongStringException ex)
            {
                await HandleLongStringException(ex, context);
            }
            catch (EmptyStringException ex)
            {
                await HandleEmptyStringException(ex, context);
            }
            catch (InvalidPriceException ex)
            {
                await HandleInvalidPriceException(ex, context);
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

        private async Task HandleInvalidPriceException(Exception ex, HttpContext context)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            await context.Response.WriteAsJsonAsync(new { message = "Please enter a price greater than or equal to0 and lower than 2 million" });
        }

        private async Task HandleEmptyStringException(Exception ex, HttpContext context)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            await context.Response.WriteAsJsonAsync(new { message = "Please enter something" });
        }
        private async Task HandleLongStringException(Exception ex, HttpContext context)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            await context.Response.WriteAsJsonAsync(new { message = "Please don't enter more than 255 chars" });
        }
    }
}
