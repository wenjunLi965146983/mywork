using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Security;
using System.Security.Cryptography;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace WorldGeneralLib.Login
{
    [Serializable]
    public class LoginInfo
    {
        public LoginInfo()
        {
            UserName = "null";
            Password = "null";
            UserLevel = 0;
        }

        public string UserName { get; set; }
        public string Password { get; set; }
        public int UserLevel { get; set; }
    }

    [Serializable]
    public class LoginDoc
    {
        public Dictionary<string, LoginInfo> dicLoginInfo;

        public LoginDoc()
        {
            dicLoginInfo = new Dictionary<string, LoginInfo>();
        }

        public static LoginDoc LoadLoginInfo()
        {
            LoginDoc loginDoc = new LoginDoc();

            BinaryFormatter fmt = new BinaryFormatter();
            FileStream fsReader = null;
            try
            {
                fsReader = File.OpenRead(Application.StartupPath + @"\Login\login.dat");
                loginDoc = (LoginDoc)fmt.Deserialize(fsReader);
                fsReader.Close();


            }
            catch (Exception)
            {
                if (fsReader != null)
                {
                    fsReader.Close();
                }
            }

            return loginDoc;
        }

        public bool SaveDoc()
        {
            if (!Directory.Exists(Application.StartupPath + @"\Login\"))
            {
                Directory.CreateDirectory(Application.StartupPath + @"\Login\");
            }

            FileStream fsWriter = new FileStream(Application.StartupPath + @"\Login\login.dat", FileMode.Create, FileAccess.Write, FileShare.Read);
            BinaryFormatter fmt = new BinaryFormatter();
            fmt.Serialize(fsWriter, this);
            fsWriter.Close();

            return true;
        }

    }

    public class LoginManage
    {
        public static LoginDoc loginDoc;
        public static string strCurrUserName = "";
        public static string strCurrPassword = "";
        public static int iCurrUserLevel = -1;
        public delegate void EventUserChanged();
        public static EventUserChanged eventUserChanged;

        public static void LoadLoginDoc()
        {
            loginDoc = LoginDoc.LoadLoginInfo();
        }
    }

    public static class SecurityMd5
    {
        public static string Encrypt(string password)
        {
            MD5 md5 = new MD5CryptoServiceProvider();

            //明文转成byte[]
            byte[] palindata = Encoding.Default.GetBytes(password);

            //加密
            byte[] encryptdata = md5.ComputeHash(palindata);

            //密文转成string
            return Convert.ToBase64String(encryptdata);
        }
    }
}
