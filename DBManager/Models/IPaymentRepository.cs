namespace DBManager.Models
{
    internal interface IPaymentRepository
    {
        DBManagerContext _dbContext { get; set; }
        Payment Payment { get; set; }
        
        
        Payment GetPaymentById (int id);
        List<Payment> GetAllPayments();

        int AddNewPayment(Payment payment);

        int UpdatePayment(Payment payment);
        //Boolean PaymentExists(int id);
        Boolean RemovePayment(int id);



    }
}
