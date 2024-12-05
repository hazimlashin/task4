namespace task_4
{
    public class Account
    {
        public string Name { get; set; }
        public double Balance { get; set; }

        public Account(string Name = "Unnamed Account", double Balance = 0.0)
        {
            this.Name = Name;
            this.Balance = Balance;
        }

        public virtual bool Deposit(double amount)
        {
            if (amount > 0)
            {
                Balance += amount;
                return true;
            }
            return false;
        }

        public virtual bool Withdraw(double amount)
        {
            if (Balance - amount >= 0)
            {
                Balance -= amount;
                return true;
            }
            return false;
        }
        public override string ToString()
        {
            return $"Name : {Name} , Balance : {Balance}";
        }
        public static Account operator +(Account a, Account b)
        {
            Account account = new Account(a.Name+" "+ b.Name+"    "+a.Balance +b.Balance );
        }
    }
    class SavingsAccount : Account
    {
        public SavingsAccount(string Name = "Unnamed Account", double Balance = 0.0 ,double rate =0.0 ) : base(Name,Balance) 
        {
            Rate = rate;
        }

        public double Rate { get; set; }
      
        public override string ToString()
        {
            return $"{base.ToString} ,Rate : {Rate}";
        }

    }
    class CheckingAccount : Account
    {
        public CheckingAccount(string Name = "Unnamed Account", double Balance = 0.0 ,double fee=1.5) : base(Name,Balance) 
        {
            Fee = fee;
        }

        public double Fee { get; set; }
        public override bool Withdraw(double amount)
        {
            if (Balance - (amount+Fee) >= 0)
            {
                bool suc = base.Withdraw(amount);
                if (suc)
                {
                    Balance -= Fee;
                    return true;
                }
            }
            return false;
        }
        public override string ToString()
        {
            return $"{base.ToString()}";
        }


    }
    public class TrustAccount : SavingsAccount
    {
        private int limitcount = 0;
        private const int maximumcount = 3;
        private const int money = 5000;
        private const int bouns = 50;
        DateTime dateTime = DateTime.Now;

        public TrustAccount(string Name = " ", double Balance = default, double Rate = default) : base(Name, Balance, Rate)
        {

        }
        public override bool Deposit(double amount)
        {
            if (amount >= money)
            {
                Balance += bouns; //Add Bouns
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"Bouns of{bouns:C} is Added to Accont for Deposit {amount:C}");
            }
            return base.Deposit(amount);
        }
        public override bool Withdraw(double amount)
        {
            if (limitcount >= maximumcount)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("You have exceeded the number of withdrawals allowed per year.");
                return false;
            }
            else if (amount <= Balance * 0.20)
            {
                limitcount++;
                return base.Withdraw(amount);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("The withdrawn amount exceeded 20 % ");
                Console.ForegroundColor = ConsoleColor.Gray;
                return false;
            }
        }
        public override string ToString()
        {
            return $"{base.ToString()} , Withdraw Limit : {maximumcount - limitcount}";
        }
    }

    public static class AccountUtil 
    {
        // Utility helper functions for Account class

        public static void Display(List<Account> accounts)
        {
            Console.WriteLine("\n=== Accounts ==========================================");
            foreach (var acc in accounts)
            {
                Console.WriteLine(acc);
            }
        }

        public static void Deposit(List<Account> accounts, double amount)
        {
            Console.WriteLine("\n=== Depositing to Accounts =================================");
            foreach (var acc in accounts)
            {
                if (acc.Deposit(amount))
                    Console.WriteLine($"Deposited {amount} to {acc}");
                else
                    Console.WriteLine($"Failed Deposit of {amount} to {acc}");
            }
        }

        public static void Withdraw(List<Account> accounts, double amount)
        {
            Console.WriteLine("\n=== Withdrawing from Accounts ==============================");
            foreach (var acc in accounts)
            {
                if (acc.Withdraw(amount))
                    Console.WriteLine($"Withdrew {amount} from {acc}");
                else
                    Console.WriteLine($"Failed Withdrawal of {amount} from {acc}");
            }
        }
    }
   
    public class Program
    {
        static void Main()
        {
            // Accounts
            var accounts = new List<Account>();
            accounts.Add(new Account());
            accounts.Add(new Account("Larry"));
            accounts.Add(new Account("Moe", 2000));
            accounts.Add(new Account("Curly", 5000));

            AccountUtil.Display(accounts);
            AccountUtil.Deposit(accounts, 1000);
            AccountUtil.Withdraw(accounts, 2000);

            // Savings
            var savAccounts = new List<SavingsAccount>();
            accounts.Add(new SavingsAccount());
            accounts.Add(new SavingsAccount("Superman"));
            accounts.Add(new SavingsAccount("Batman", 2000));
            accounts.Add(new SavingsAccount("Wonderwoman", 5000, 5.0));

            AccountUtil.Display(accounts);
            AccountUtil.Deposit(accounts, 1000);
            AccountUtil.Withdraw(accounts, 2000);

            // Checking
            var checAccounts = new List<CheckingAccount>();
            accounts.Add(new CheckingAccount());
            accounts.Add(new CheckingAccount("Larry2"));
            accounts.Add(new CheckingAccount("Moe2", 2000));
            accounts.Add(new CheckingAccount("Curly2", 5000));

            AccountUtil.Display(accounts);
            AccountUtil.Deposit(accounts, 1000);
            AccountUtil.Withdraw(accounts, 2000);
            AccountUtil.Withdraw(accounts, 2000);

            // Trust
            var trustAccounts = new List<TrustAccount>();
            accounts.Add(new TrustAccount());
            accounts.Add(new TrustAccount("Superman2"));
            accounts.Add(new TrustAccount("Batman2", 2000));
            accounts.Add(new TrustAccount("Wonderwoman2", 5000, 5.0));

            AccountUtil.Display(accounts);
            AccountUtil.Deposit(accounts, 1000);
            AccountUtil.Deposit(accounts, 6000);
            AccountUtil.Withdraw(accounts, 2000);
            AccountUtil.Withdraw(accounts, 3000);
            AccountUtil.Withdraw(accounts, 500);

            Console.WriteLine();
        }
    }
}
