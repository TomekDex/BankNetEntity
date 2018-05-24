using BankNetEntityDB;
using BankNetEntityDB.DBModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BankNetEntity.Models
{
    public class Account
    {
        private static ContextBankNetEntityDB DB = new ContextBankNetEntityDB();
        private static Regex exNr = new Regex(@"\d", RegexOptions.Compiled);
        private static Regex exName = new Regex(@"\w+", RegexOptions.Compiled);

        public static Account account = new Account();

        public User User { get; set; }
        public double Balance
        {
            get
            {
                if (User == null)
                    return 0;
                try
                {
                    return User.TransfersTo.Sum(a => a.Value) - User.TransfersFrom.Sum(a => a.Value);
                }
                catch (Exception ex)
                {
                    return 0;
#if DEBUG
                    throw ex;
#endif
                }
            }
        }

        public int Id
        {
            get
            {
                if (User == null)
                    return 0;
                try
                {
                    return User.Id;
                }
                catch (Exception ex)
                {
                    return 0;
#if DEBUG
                    throw ex;
#endif
                }
            }
        }

        public enum ReturnCode
        {
            Succes,
            Fail,
            NoUser,
            WrongPass
        }

        public enum PaswordValideCode
        {
            Good = 0,
            NoSmall = 1,
            NoBig = 2,
            NoNr = 4,
            TooSmall = 8
        }

        public ReturnCode Payment(string value, string title)
        {
            return TryCatchWithReturnCode(() =>
            {
                if (string.IsNullOrWhiteSpace(value) || string.IsNullOrWhiteSpace(title))
                    return ReturnCode.Fail;
                double result;
                if (!ValueValideAndParse(value, out result))
                    return ReturnCode.Fail;

                User userFrom = GetSystemUser();

                DB.Transfers.Add(new Transfers { Title = title, UserFrom = userFrom, UserTo = User, Value = result, Date = DateTime.Now });
                DB.SaveChanges();

                return ReturnCode.Succes;
            });
        }

        private User GetSystemUser()
        {
            if (!UserExist("#system#"))
            {
                DB.User.Add(new User { Login = "#system#", FirstName = "#system#", LastName = "#system#", Pass = "#system#" });
                DB.SaveChanges();
            }
            return DB.User.SingleOrDefault(a => a.Login == "#system#");
        }

        private ReturnCode TryCatchWithReturnCode(Func<ReturnCode> p)
        {
            try
            {
                return p();
            }
            catch (Exception ex)
            {
#if DEBUG
                throw ex;
#else
                return ReturnCode.Fail;
#endif
            }
        }

        public ReturnCode Transfer(string loginTarget, string value, string title)
        {
            return TryCatchWithReturnCode(() =>
            {
                if (string.IsNullOrWhiteSpace(loginTarget) || string.IsNullOrWhiteSpace(value) || string.IsNullOrWhiteSpace(title))
                    return ReturnCode.Fail;
                double result;
                if (!ValueValideAndParse(value, out result))
                    return ReturnCode.Fail;
                User userTarget = DB.User.SingleOrDefault(a => a.Login == loginTarget.Trim());
                if (userTarget == null)
                    return ReturnCode.NoUser;

                if (Balance < result)
                    return ReturnCode.Fail;

                DB.Transfers.Add(new Transfers { Title = title, UserFrom = User, UserTo = userTarget, Value = result, Date = DateTime.Now });
                DB.SaveChanges();

                return ReturnCode.Succes;
            });
        }

        public static bool ValueValideAndParse(string value, out double result)
        {
            if (!double.TryParse(value, out result))
                return false;
            return result > 0.009999;
        }

        public ReturnCode LogIn(string login, string pass)
        {
            return TryCatchWithReturnCode(() =>
            {
                var aa = DB.User.ToList();
                User = DB.User.SingleOrDefault(a => a.Login == login.Trim());
                if (User == null)
                    return ReturnCode.NoUser;
                bool goodPass;

                using (MD5 md5Hash = MD5.Create())
                {
                    string hash = GetMd5Hash(md5Hash, pass);

                    goodPass = VerifyMd5Hash(md5Hash, User.Pass, hash);
                }

                if (!goodPass)
                    return ReturnCode.WrongPass;
                return ReturnCode.Succes;
            });
        }

        public ReturnCode Registry(string login, string pass, string firstName, string lastName)
        {
            return TryCatchWithReturnCode(() =>
            {
                var aaaa = DB.User.ToList();
                if (UserExist(login))
                    return ReturnCode.Fail;
                if (PaswordValide(ref pass) != PaswordValideCode.Good)
                    return ReturnCode.WrongPass;
                if (NormalizeAndCheckName(ref firstName) == ReturnCode.Fail)
                    return ReturnCode.Fail;
                if (NormalizeAndCheckName(ref lastName) == ReturnCode.Fail)
                    return ReturnCode.Fail;

                string hash;
                using (MD5 md5Hash = MD5.Create())
                {
                    hash = GetMd5Hash(md5Hash, pass);
                }

                DB.User.Add(new User { Login = login, FirstName = firstName, LastName = lastName, Pass = hash });
                DB.SaveChanges();
                if (LogIn(login, pass) != ReturnCode.Succes)
                {
                    DB.User.Remove(DB.User.Single(a => a.Login == login));
                    DB.SaveChanges();
                    return ReturnCode.Fail;
                }
                Payment("500", "Na Start");
                return ReturnCode.Succes;
            });
        }

        public static bool UserExist(string login)
        {
            return DB.User.Any(a => a.Login == login.Trim());
        }

        public static PaswordValideCode PaswordValide(ref string pass)
        {
            PaswordValideCode code = PaswordValideCode.Good;
            pass = pass?.Trim();

            if (pass == pass.ToLower())
                code |= PaswordValideCode.NoBig;
            if (pass == pass.ToUpper())
                code |= PaswordValideCode.NoSmall;
            if (!exNr.IsMatch(pass ?? ""))
                code |= PaswordValideCode.NoNr;
            if ((pass?.Length ?? 0) < 8)
                code |= PaswordValideCode.TooSmall;
            return code;
        }

        public static List<Clients> GetClients()
        {
            return DB.User.Select(a => new Clients { FirstName = a.FirstName, LastName = a.LastName, Login = a.Login }).ToList();
        }

        public static ReturnCode NormalizeAndCheckName(ref string firstName)
        {
            if (string.IsNullOrWhiteSpace(firstName))
                return ReturnCode.Fail;
            try
            {

                MatchCollection mcName = exName.Matches(firstName);
                firstName = "";
                foreach (Match mName in mcName)
                {
                    char[] nameChars = mName.Value.ToArray();
                    firstName += nameChars[0].ToString().ToUpper() + new string(nameChars, 1, mName.Value.Length - 1).ToLower() + " ";
                }
                firstName = firstName.Trim();

            }
            catch (Exception)
            {
                return ReturnCode.Fail;

            }
            return ReturnCode.Succes;
        }

        static string GetMd5Hash(MD5 md5Hash, string input)
        {
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
                sBuilder.Append(data[i].ToString("x2"));
            return sBuilder.ToString();
        }

        static bool VerifyMd5Hash(MD5 md5Hash, string input, string hash)
        {
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;

            return 0 == comparer.Compare(input, hash);
        }
    }
}
