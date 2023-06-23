using System.Security;
using System.Security.Cryptography;
using System.Text;

namespace DataProtectionOnServer
{
    /*
     * Uygulamanızda gizlemeniz gereken bir çok bilgi olabilir. Bu bilgilere SADECE UGULAMANIZ erişebilmeli. 
     *   Bu verileri sakladığınız (.json gibi) dosyaları encrypt etmelisiniz!
     */
    public class DataProtector
    {
        private string path;
        private byte[] entropy;

        public DataProtector(string path)
        {
            this.path = path;
            entropy = new byte[16];
            entropy = RandomNumberGenerator.GetBytes(16);
            this.path += "EncryptedData.halk";
        }

        public int EncryptData(string secretData)
        {
            var encoded = Encoding.UTF8.GetBytes(secretData);
            FileStream fileStream = new FileStream(path, FileMode.OpenOrCreate);
            int length = encryptDataToFile(encoded, entropy, DataProtectionScope.CurrentUser, fileStream);
            fileStream.Close();
            return length;
        }

        private int encryptDataToFile(byte[] encoded, byte[] entropy, DataProtectionScope currentUser, FileStream fileStream)
        {
            byte[] encrypted = ProtectedData.Protect(encoded, entropy, currentUser);

            int outputLength = 0;
            if (fileStream.CanWrite && encrypted != null)
            {
                fileStream.Write(encrypted, 0, encrypted.Length);
                outputLength = encrypted.Length;
            }

            return outputLength;
        }

        public string DecryptData(int length)
        {
            FileStream fileStream = new FileStream(path, FileMode.Open);
            byte[] decrypt = decryptDataFromFile(fileStream, entropy, DataProtectionScope.CurrentUser, length);
            fileStream.Close();
            return Encoding.UTF8.GetString(decrypt);
        }

        private byte[] decryptDataFromFile(FileStream fileStream, byte[] entropy, DataProtectionScope currentUser, int length)
        {
            byte[] inputBuffer = new byte[length];
            byte[] outputBuffer;

            if (fileStream.CanRead)
            {
                fileStream.Read(inputBuffer, 0, inputBuffer.Length);
                outputBuffer = ProtectedData.Unprotect(inputBuffer, entropy, currentUser);
            }
            else
            {
                throw new SecurityException("Stream problemi oluştu");
            }

            return outputBuffer;
        }
    }
}
