using Database.Context;

namespace Buisness
{
    public class Class1
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public object? Data { get; set; }
        public Result()
        { }
        public Result (bool Success, string Message,object? Data =null )
        {
            this.Success = Success;
            this.Message = Message;
            this.Data = Data;

        }
        public Result DbCommit( FootballTransferMarketContext footballTransferMarketContext, string Message,string?Failed=null,object?Data = null)
        {
            try
            {
                footballTransferMarketContext.SaveChanges();
                return new Result(true, Message, Data);
            }
            catch(Exception ex)
            {
                return new Result(false, FailedMessage ?? ex.Message);
            }
}
    }
}