using System.ComponentModel.DataAnnotations.Schema;

namespace DBManager
{
    [Table("newtable")]

    public class NewTable
    {
        public int id { get; set; }
        public string title { get; set; }
    }

    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }    
        public string MiddleName { get; set; }
        public DateTime Birthdate { get; set; }
        public bool IsActive { get; set; }
    }

    public class Payment
    {
        public int Id { get; set; }
        public Double PaymentSum { get; set; }
        public string PaymentType { get; set; } 
        public DateTime PaymentDate { get; set; }
        public UserAccount Account { get; set; }
        public int AccountId { get; set; }
    }

    public class UserAccount
    {
        public int Id { get; set; }
        public string DateCreation { get; set; }
        public string Currency { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }

    }

}
