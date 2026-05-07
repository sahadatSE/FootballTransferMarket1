using Database.Context;

namespace Buisness
{
    public class Result
    {
        private bool v;

        public bool IsSuccess { get; set; }
        public string Message { get; set; } = null!;
        public object? Data { get; set; }
        private Result() { }
        public Result(bool success, string message, object? data)
        {
            IsSuccess = success;
            Message = message;
            Data = data;
        }

        public Result(bool v, string message)
        {
            this.v = v;
            Message = message;
        }

        public async static Task<Result> DBcommit(FTMContext context, string message, string? errorMessage = null, object? data = null)
        {
            try
            {
                await context.SaveChangesAsync();
                return new Result(true, message, data);

            }
            catch (Exception ex)
            {
                return new Result(false, errorMessage ?? ex.Message, null);
            }
        }
    }
}
