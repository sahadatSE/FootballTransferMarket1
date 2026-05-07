using Database.Context;
using Database.Model;

namespace Buisness
{
    public class Result
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; } = null!;
        public object? Data { get; set; }

     
        public Result()
        { }

        
        public Result(bool success, string message, object? data = null)
        {
            IsSuccess = success;
            Message = message;
            Data = data;
        }

       
        public static async Task<Result> DBcommit(FTMContext context, string message, string? FailedMessage = null, object? data = null)
        {
            try
            {
                await context.SaveChangesAsync();
                return new Result(true, message, data);
            }
            catch (Exception ex)
            {
                return new Result(false, FailedMessage ?? ex.Message, null);
            }
        }
    }
}