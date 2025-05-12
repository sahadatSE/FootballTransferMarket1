using Database.Context;
using Database.Model;

namespace Business.Services
{
    public class PaymentMethodService
    {
        FootballTransferMarket footballTransferMarket = new FootballTransferMarket();
        public Result Add(PaymentMethod paymentMethod)
        {
            if (footballTransferMarket.PaymentMethod.Any(x => x.PaymentMethodName == paymentMethod.PaymentMethodName))
                return new Result(false, "Payment Method already exist!");
            footballTransferMarket.PaymentMethod.Add(paymentMethod);
            return new Result().DBCommit(footballTransferMarket, "Save Successfully!", null, paymentMethod);
        }
        public Result Update(PaymentMethod paymentMethod)
        {
            if (!footballTransferMarket.PaymentMethod.Any(x => x.PaymentMethodId == paymentMethod.PaymentMethodId))
                return new Result(false, "Payment Method not exist!");
            footballTransferMarket.PaymentMethod.Update(paymentMethod);
            return new Result().DBCommit(footballTransferMarket, "Updated Successfully!", null, paymentMethod);
        }
        public Result List()
        {
            try
            {
                var paymentMethods = footballTransferMarket.PaymentMethod.ToList();
                return new Result(true, "Success", paymentMethods);
            }
            catch (Exception ex)
            {
                return new Result(false, ex.Message);
            }
        }
        public Result Single(int Id)
        {
            try
            {
                var paymentMethod = footballTransferMarket.PaymentMethod.Where(x => x.PaymentMethodId == Id).FirstOrDefault();
                return new Result(true, "Success", paymentMethod);
            }
            catch (Exception ex)
            {
                return new Result(false, ex.Message);
            }
        }
    }
}
