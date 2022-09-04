// See https://aka.ms/new-console-template for more information


using ConsoleTables;
using DBManager.Models;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

//using System.Configuration;


//string connStr = @"postgres://postgres:postgrespw@localhost:49157";
namespace DBManager
{

    public class Program
    {

        private static DbContextOptions<DBManagerContext> _contextOptions;
        private static UserRepository _userRepository;
        private static UserAccountRepository _userAccountRepository;
        private static PaymentRepository _paymentRepository;
        static void Main(string[] args)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

            var connStr = ConfigurationManager.ConnectionStrings["postgreStr"].ToString();

            //string connStr = @"Server = 127.0.0.1;Port=49157; Database = postgres; User Id = postgres; Password = postgrespw;";
            Console.WriteLine("Connstr: " + connStr);



             _contextOptions = new DbContextOptionsBuilder<DBManagerContext>().Options;

            _userRepository = new UserRepository(new DBManagerContext(_contextOptions));
            _userAccountRepository = new UserAccountRepository(new DBManagerContext(_contextOptions));
            _paymentRepository = new PaymentRepository(new DBManagerContext(_contextOptions));

            //User user = userRepository.GetUser(10);
            //Console.WriteLine($"User info. Fio: {user.LastName}, {user.Name}, {user.MiddleName}, {user.Birthdate.ToString("YY-MM-dd")}");
            int menuItem = 0;
            while (menuItem != 20)
            {
                menuItem = ShowMenu();
                switch(menuItem)
                {
                    case 1:
                        var allUsers = _userRepository.GetAllUsers();
                        ConsoleTable.From<User>(allUsers).Write();
                        break;


                        //Get User by Id
                    case 2:
                        Console.WriteLine("Enter userID");
                        var useridReadKey = Console.ReadLine();
                        int userId = 0;
                        if (useridReadKey != null)
                        {
                            int.TryParse(useridReadKey, out userId);
                        }
                        var user = _userRepository.GetUser(userId);

                        if (user != null) {
                            Console.WriteLine($"User is {user.LastName} {user.MiddleName} {user.Name}");
                            }
                        else
                        {
                            Console.WriteLine("User doen't exist");
                        }
                        break;

                    case 3:
                        //Добавление пользователя
                        var newUser = _userRepository.AddNewUser(AddNewUser(1));
                        Console.WriteLine($"User {newUser} is added");
                        break;


                    case 4:
                        RemoveuUser();
                        break;

                    case 5:
                        ShowUserAccounts();
                        break;

                    case 6:
                        ShowUserAccountsForUser();
                        break;

                    case 7:
                        AddNewUserAccount();
                        break;
                    case 8:
                        RemoveUserAccount();
                        break;
                    case 9:
                        ShowAllPayments();
                        break;
                    case 10:
                        GetPaymentById();
                        break;

                    case 11:
                        AddNewPayment();
                        break;
                    case 12:
                        RemovePayment();
                        break;

                }

            }







            Console.WriteLine("Hello, World!");

        }

        private static void ShowAllPayments()
        {
            var payments = _paymentRepository.GetAllPayments();
            ConsoleTable.From<Payment>(payments).Write();
        }

        private static void ShowUserAccountsForUser()
        {
            Console.WriteLine("Enter userID");
            var useridReadKey = Console.ReadLine();
            int userId = 0;
            if (useridReadKey != null)
            {
                int.TryParse(useridReadKey, out userId);
            }
            var userAccount = _userAccountRepository.GetUserAccounts(userId);

            if (userAccount != null)
            {
                //Console.WriteLine($"User is {user.LastName} {user.MiddleName} {user.Name}");
                ConsoleTable.From<UserAccount>(userAccount).Write();

            }
            else
            {
                Console.WriteLine("User's account not found");
            }

        }


        public static void GetPaymentById()
        {
            Console.WriteLine("Enter paymentID");
            var paymentIdReadKey = Console.ReadLine();
            int paymentId = 0;
            if (paymentIdReadKey != null)
            {
                int.TryParse(paymentIdReadKey, out paymentId);
            }
            var payment = _paymentRepository.GetPaymentById(paymentId);

            if (payment != null)
            {
                Console.WriteLine($"Payment is {payment.AccountId} {payment.PaymentDate} {payment.PaymentSum} {payment.PaymentType}");
            }
            else
            {
                Console.WriteLine("Payment doen't exist");
            }
        }

        private static void ShowUserAccounts()
        {
            var allUserAccounts = _userAccountRepository.GetAllUserAccounts();
            ConsoleTable.From<UserAccount>(allUserAccounts).Write();
        }

        public static int ShowMenu()
        {
            Console.WriteLine(@"Choose menu item 

1 - Show Users
2 - Show User by id
3 - Add User
4 = Remove User
5 = Show User Accounts
6 = Show User Account 
7 = Add UserAccount
8 - Remove UserAccount
9 - Show Payments
10 - Show Payment by id
11 - Add payment
12 - Remove Payment
20 - Exit");
            var menuitem = Console.ReadLine();
            int selectedmenu = 0;
            if (menuitem != null) 
            {
                int.TryParse(menuitem, out selectedmenu);
                if (selectedmenu > 0 && selectedmenu <= 12) return selectedmenu;
            }
            if (selectedmenu == 0) ShowMenu();

            return selectedmenu;

        }

        static User AddNewUser(int lastId)
        {

            var newUser = new User { LastName = $"testLastName{lastId}" , Name = $"testName{lastId}", MiddleName = $"testMiddle{lastId}", Birthdate = new DateTime(2000,  3, 5) ,IsActive = true };
            return newUser;
        }


        static void AddNewUserAccount()
        {
            Console.WriteLine("Adding a new user account");
            UserAccountRepository uar = new UserAccountRepository(new DBManagerContext(_contextOptions));

            Console.WriteLine("Enter userID");
            var useridReadKey = Console.ReadLine();
            int userId = 0;
            if (useridReadKey != null)
            {
                int.TryParse(useridReadKey, out userId);
            }

            var user =  _userRepository.GetUser(userId);
            if (user != null)
            {

                UserAccount userAccount = new UserAccount
                {
                    UserId = userId,
                    Currency = "USD",
                    DateCreation = "2022-05-21 13:54:53"
                };
                var newUserAccountCreated = uar.AddNewUserAccount(userAccount);
                Console.WriteLine($"UserAccount is created {newUserAccountCreated}");
            }
            else
            {
                Console.WriteLine($"User is {userId} not found, operation cancelled");

            }
        }



        static void AddNewPayment()
        {
            Console.WriteLine("Adding a new payment");

            Console.WriteLine("Enter userAccountID");
            var userAccountIdReadKey = Console.ReadLine();
            int userAccountId = 0;
            if (userAccountIdReadKey != null)
            {
                int.TryParse(userAccountIdReadKey, out userAccountId);
            }

            var userAccount = _userAccountRepository.GetUserAccount(userAccountId);
            if (userAccount != null)
            {

                Payment payment = new Payment
                {
                    AccountId = userAccountId,
                    PaymentDate = DateTime.Now,
                    PaymentSum = Convert.ToDouble(new Random().Next(1, 100)),
                    PaymentType = "in"
                };
                var newPaymentCreated =  _paymentRepository.AddNewPayment(payment);
                Console.WriteLine($"new Payment  is created {newPaymentCreated}");
            }
            else
            {
                Console.WriteLine($"userAccount is {userAccount} not found, operation cancelled");

            }
        }

        static void RemoveUserAccount()
        {
            Console.WriteLine("Enter useraccountID to remove");
            UserAccountRepository uar = new UserAccountRepository(new DBManagerContext(_contextOptions));

            var userAccountIdReadKey = Console.ReadLine();
            int userAccountId = 0;
            if (userAccountIdReadKey  != null)
            {
                int.TryParse(userAccountIdReadKey, out userAccountId);
            }

            var userAccount = _userAccountRepository.RemoveUserAccount(userAccountId);
            if (userAccount == true)
            {
                Console.WriteLine($"UserAccount {userAccountId} is removed ");
            }
            else
            {
                Console.WriteLine($"User is {userAccountId} not found, operation cancelled");

            }
        }




        static void RemovePayment() 
        {
            Console.WriteLine("Enter useraccountID to remove");
            UserAccountRepository uar = new UserAccountRepository(new DBManagerContext(_contextOptions));

            var paymentReadKey = Console.ReadLine();
            int paymentReadKeyId = 0;
            if (paymentReadKey != null)
            {
                int.TryParse(paymentReadKey, out paymentReadKeyId);
            }

            var payment =  _paymentRepository.RemovePayment(paymentReadKeyId);
            if (payment == true)
            {
                Console.WriteLine($"UserAccount {paymentReadKeyId } is removed ");
            }
            else
            {
                Console.WriteLine($"Payment is {paymentReadKeyId} not found, operation cancelled");

            }
        }


        static void RemoveuUser()
        {
            Console.WriteLine("Enter userID to remove");
            var useridReadKey = Console.ReadLine();
            int userId = 0;
            if (useridReadKey != null)
            {
                int.TryParse(useridReadKey, out userId);
            }
            var user = _userRepository.RemoveUser(userId);

            if (user == true)
            {
                Console.WriteLine($"User {userId} is successfully removed");
            }
            else
            {
                Console.WriteLine("Error. User doen't exist");
            }
        }


    }



    
}