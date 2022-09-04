using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBManager.Models
{
    internal class PaymentRepository : IPaymentRepository
    {
        public DBManagerContext _dbContext { get; set; }
        public Payment Payment { get; set; } 

        public PaymentRepository(DBManagerContext dbContext)
        {
            _dbContext = dbContext;
        }



        public int AddNewPayment(Payment payment)
        {
            _dbContext.Payments.Add(payment);
            try
            {
                _dbContext.SaveChanges();
                return _dbContext.Payments.Max(x => x.Id);
            }
            catch (Exception ex)
            {
                return -1;
            }
            
        }

        private Boolean PaymentExists(int id)
        {
            Boolean result = false;
            var payment = _dbContext.Payments.Where(p=>p.Id == id).FirstOrDefault();
            if(payment != null) return true;
            return false;
        }

        public Boolean RemovePayment(int id)
        {
            Boolean result = false;
            if(PaymentExists(id))
            {
                try
                {
                    _dbContext.Payments.Remove(GetPaymentById(id));
                    _dbContext.SaveChanges();
                    return true;
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }
            }
            else
            {
                Console.WriteLine($"Item {id} not found");
                return false;
            }
        }

        public List<Payment> GetAllPayments()
        {
           return _dbContext.Payments.ToList();
        }

        public Payment GetPaymentById(int id)
        {
            if(_dbContext.Payments.First(x=>x.Id == id).Id >0)
            {
                return _dbContext.Payments.First(x => x.Id == id);
            }

            return null;
        }

        public int UpdatePayment(Payment payment)
        {
            throw new NotImplementedException();
        }
    }
}
