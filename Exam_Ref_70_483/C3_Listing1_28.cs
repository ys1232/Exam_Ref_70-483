using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using System.Linq;
using System.Security;
using System.Runtime.InteropServices;

namespace Exam_Ref_70_483
{
    //class C3_Listing1
    //{

    //}

    //class C3_Listing2
    //{

    //}

    //class C3_Listing3
    //{

    //}

    class C3_Listing4_6
    {
        public static void Test_Parse_TryParse()
        {
            string value = "true";
            bool b = bool.Parse(value);
            Console.WriteLine(b);

            value = "1";
            int result;
            bool success = int.TryParse(value, out result);
            if (success)
                Console.WriteLine("{0} is valid integer", result);
            else
                Console.WriteLine("the input is not valid integer");

            CultureInfo english = new CultureInfo("En");
            value = "$19.95";
            decimal d = decimal.Parse(value, NumberStyles.Currency, english);
            Console.WriteLine(d.ToString(english));

            Console.ReadLine();
        }
    }

    class C3_Listing7_8
    {
        public static void Test_Convert()
        {
            int i = Convert.ToInt32(null);
            Console.WriteLine(i);

            double d = 23.15;
            i = Convert.ToInt32(d);
            Console.WriteLine(i);

            Console.ReadLine();
        }
    }

    class C3_Listing9_10
    {
        public static void Test_ZipCode()
        {
            string ZipCode = "1234 AB";
            if (ValidateZipCode(ZipCode))
                Console.WriteLine("{0} is valid ZipCode", ZipCode);
            else
                Console.WriteLine("{0} is invalid ZipCode", ZipCode);

            if (ValidateZipCodeRegExp(ZipCode))
                Console.WriteLine("{0} is valid ZipCode", ZipCode);
            else
                Console.WriteLine("{0} is invalid ZipCode", ZipCode);

            Console.ReadLine();
        }

        public static bool ValidateZipCode(string zipCode)  // don't use regEx
        {
            if (zipCode.Length < 6) return false;

            string numberPart = zipCode.Substring(0, 4);
            int number;
            if (!int.TryParse(numberPart, out number)) return false;

            string characterPart = zipCode.Substring(4);

            if (numberPart.StartsWith("0")) return false;
            if (characterPart.Trim().Length < 2) return false;
            if (characterPart.Length == 3 && characterPart.Trim().Length != 2)
                return false;

            return true;
        }

        public static bool ValidateZipCodeRegExp(string zipCode)    // ues regEx
        {
            Match match = Regex.Match(zipCode, @"^[1-9][0-9]{3}\s?[a-zA-Z]{2}$", RegexOptions.IgnoreCase);
            return match.Success;
        }
    }

    class C3_Listing11
    {
        public static void Test_Collapse_multiple_Space_with_RegEx()
        {
            RegexOptions options = RegexOptions.None;
            Regex regex = new Regex(@"[ ]{2,}", options); // 2 or more space

            string input = "1 2 3 4   5";
            string result = regex.Replace(input, " ");

            Console.WriteLine(result);
            Console.ReadLine();
        }
    }

    //class C3_Listing12
    //{

    //}

    class C3_Listing13
    {
        // JavaScriptSerializer is in  System.Web, not supported here
        //public static void Test_Deserializing_Json_string()
        //{
        //    string json = "";
        //    var serializer = new JavaScriptSerializer();
        //    var result = serializer.Deserialize<Dictionary<string, object> >(json);
        //}
    }

    //class C3_Listing14
    //{

    //}

    //class C3_Listing15
    //{

    //}

    //class C3_Listing16
    //{

    //}

    class C3_Listing17
    {
        //public static void Test_Symmetric_Encryption()
        //{
        //    string original = "My secret data!";
        //    using (SymmetricAlgorithm symmetricAlgorithm = new AesManaged())    // class AesManaged is not accessible...
        //    {
        //        // symmetric key and IV are all in the symmetricAlgorithm
        //        byte[] encrypted = Encrypt(symmetricAlgorithm, original);   // call the first custom method Encrypt, pass the plaintext, get the ciphertext
        //        string roundtrip = Decrypt(symmetricAlgorithm, encrypted);  // call the second custom method Decrypt, pass the ciphertext, convert to plaintext

        //        Console.WriteLine("Original : {0}", original);
        //        Console.WriteLine("Round Trip: {0}", roundtrip);
        //    }
        //}

        //static byte[] Encrypt(SymmetricAlgorithm aesAlg, string plainText)
        //{
        //    // aesAlg.Key, aesAlg.IV : get the initialization vector (IV) and secret key to use for the symmetric algorithm
        //    ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);  // CreateEncryptor: Creates a symmetric encryptor object using the specified key and initialization vector (IV)

        //    using (MemoryStream msEncrypt = new MemoryStream()) // MemoryStream Class: Creates a stream whose backing store is memory
        //    {
        //        using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
        //        // CryptoStream Class: Defines a stream that links data streams to cryptographic transformations
        //        // the arguments: target data stream, the transformation to use, and the mode of the stream
        //        {
        //            using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
        //            {
        //                swEncrypt.Write(plainText);
        //            }
        //            return msEncrypt.ToArray();
        //        }
        //    }
        //}

        //static string Decrypt(SymmetricAlgorithm aesAlg, byte[] cipherText)
        //{
        //    ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

        //    using (MemoryStream msDecrypt = new MemoryStream(cipherText))
        //    {
        //        using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Write))
        //        {
        //            using (StreamReader srDecrypt = new StreamReader(csDecrypt))
        //            {
        //                return srDecrypt.ReadToEnd();
        //            }
        //        }
        //    }
        //}

    }

    class C3_Listing18_19
    {
        public static void Test_asymmetric_algorithm()
        {
            //RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            //string publicKeyXML = rsa.ToXMLString(false);   // get the public key
            //string privateKeyML = rsa.ToXmlString(true);    // get the private key

            //UnicodeEncoding ByteConverter = new UnicodeEncoding();
            //byte[] dataToEncrypt = ByteConverter.GetBytes("My Secret Data!");   // convert your message to bytes
            //using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
            //{
            //    RSA.FromXmlString(publicKeyXML);    // use the public key
            //    encryptedData = RSA.Encrypt(dataToEncrypt, false);  // convert plaintext to encryptedData
            //}

            //// this is in the receiver side
            //byte[] decryptedData;
            //using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
            //{
            //    RSA.FromXmlString(privateKeyXML);   // use the private key
            //    decryptedData = RSA.Decrypt(encryptedData, false);  // convert encryptedData to plaintext
            //}

            //string decryptedString = ByteConverter.GetString(decryptedData);    // convert bytes to string
            //Console.WriteLine(decryptedString); // Displays: My Secret Data!

        }
    }

    class C3_Listing20
    {
        //public static void Test_asymmetric_key_container()
        //{
        //    string containerName = "SecretContainer";
        //    CspParameters csp = new CspParameters() { KeyContainerName = containerName};    // csp is the KeyContainer we created, with the specified KeyContainerName
        //    byte[] encryptedData;
        //    using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider(csp))    // the Asymmetric algorithem will use the key container to keep the key
        //    {
        //        encryptedData = RSA.Encrypt(dataToEncrypt, false);
        //    }
        //}
    }

    class C3_Listing21
    {
        class Set_Naive_approach<T>
        {
            private List<T> list = new List<T>();

            public void Insert(T item)
            {
                if (!Contains(item))
                    list.Add(item);
            }

            public bool Contains(T item)
            {
                foreach (T member in list)
                    if (member.Equals(item))
                        return true;
                return false;
            }

        }

        class Set_Using_Hashing<T>
        {
            private List<T>[] buckets = new List<T>[100];   // 2-D list, the first D is indexed by the bucket number, which is the hashing result based on the item

            public void Insert(T item)
            {
                int bucket = GetBucket(item.GetHashCode());
                if (Contains(item, bucket))
                    return;
                if (buckets[bucket] == null)
                    buckets[bucket] = new List<T>();
                buckets[bucket].Add(item);
            }

            public bool Contains(T item)
            {
                return Contains(item, GetBucket(item.GetHashCode()));
            }

            private int GetBucket(int hashcode)
            {
                unchecked
                {
                    return (int)((uint)hashcode % (uint)buckets.Length);    // mode the hashcode to fit into the buckets
                }
            }

            private bool Contains(T item, int bucket)
            {
                if (buckets[bucket] != null)
                    foreach (T member in buckets[bucket])   // loop the subgroup for itmes with the same hash value 
                        if (member.Equals(item))
                            return true;
                return false;
            }

        }
    }

    class C3_Listing23
    {
        public static void Test_SHA256Managed_Hash_method()
        {
            UnicodeEncoding byteConverter = new UnicodeEncoding();
            SHA256 sha256 = SHA256.Create();

            string data = "A paragraph of text";
            byte[] hashA = sha256.ComputeHash(byteConverter.GetBytes(data));

            data = "A paragraph of changed text";
            byte[] hashB = sha256.ComputeHash(byteConverter.GetBytes(data));

            data = "A paragraph of text";
            byte[] hashC = sha256.ComputeHash(byteConverter.GetBytes(data));
            //byteConverter.
            Console.WriteLine(hashA.SequenceEqual(hashB));
            Console.WriteLine(hashA.SequenceEqual(hashC));

            Console.WriteLine("hashA: {0}", byteConverter.GetString(hashA));
            Console.WriteLine("hashB: {0}", byteConverter.GetString(hashB));
            Console.WriteLine("hashC: {0}", byteConverter.GetString(hashC));

            Console.WriteLine("hashA: {0}", BitConverter.ToString(hashA));  // the hash value is 256 bits
            Console.WriteLine("hashA: {0}", BitConverter.ToString(hashB));
            Console.WriteLine("hashA: {0}", BitConverter.ToString(hashC));

            Console.ReadLine();
        }
    }

    class C3_Listing24
    {
        //public static void Test_Certificate_SignAndVerify()
        //{
        //    string textToSign = "Text paragraph";
        //    byte[] signature = Sign(textToSign, "cn = WouterDeKort");   // sign a signature for the given text

        //    Console.WriteLine(Verify(textToSign, signature));   // verify the text using the signature
        //}

        //static byte[] Sign(string text, string certSubject)
        //{
        //    X509Certificate2 cert = GetCertificate();
        //    var csp = (RSACryptoServiceProvider)cert.PrivateKey;
        //    byte[] hash = HashData(text);
        //    return csp.SignHash(hash, CryptoConfig.MapNameTo01D("SHA1"));
        //}

        //static bool Verify(string text, byte[] signature)
        //{
        //    X509Certificate2 cert = GetCertificate();
        //    var csp = (RSACryptoServiceProvider)cert.PublicKey.Key;
        //    byte[] hash = HashData(text);
        //    return csp.VerifyHash(hash, CryptoConfig.MapNameTo01D("SHA1"), signature);
        //}

        //private static byte[] HashData(string text)
        //{
        //    HashAlgorithm hashAlgorithm = new SHA1Managed();
        //    UnicodeEncoding encoding = new UnicodeEncoding();
        //    byte[] data = encoding.GetBytes(text);
        //    byte[] hash = hashAlgorithm.ComputeHash(data);
        //    return hash;
        //}

        //private static X509Certificate2 GetCertificate()
        //{
        //    X509Store my = new X509Store("testCertStore", StoreLocation.CurrentUser);   // you need to install a certificate first. then get the certificate in the given location
        //    my.Open(OpenFlags.ReadOnly);

        //    var certificate = my.Certificates[0];
        //    return certificate;
        //}
    }

    class C3_Listing25_26
    {
        // cannot add using System.Security.CodeAccessPermission.........

        //[FileIOPermission(SecurityAction.Demand, AllLocalFiles = FileIOPermissionAccess.Read)]
        //public void DeclarativeCAS()
        //{ }

        //public static void Test_code_access_permissions()
        //{
        //    FileIOPermission f = new FIleIOPermission(PermissionState.None);
        //    f.AllLocalFiles = FileIOPermissionAccess.Read;
        //    try
        //    {
        //        f.Demand();
        //    }
        //    catch (SecurityException s)
        //    {
        //        Console.WriteLine(s.Message);
        //    }
        //}

    }

    class C3_Listing27
    {
        // cannot add using System.Security.SecureString;

        //using (SecureString ss = new SecureString())  // use SecureString to save password
        //{
        //    Console.Write("Please enter password: ");
        //    while (true)
        //    {
        //        ConsoleKeyInfo cki = Console.ReadKey(true);   // read the character use pressed
        //        if (cki.Key == ConsoleKey.Enter) break;   // if user pressed enter, the password inputting is done

        //        ss.AppendChar(cki.KeyChar);   // append the char to SecurityString
        //        Console.Write("*");
        //    }
        //    ss.MakeReadOnly();    // when the password build is done, make the securitystring immutable
        //}
    }

    class C3_Listing28
    {
        // cannot add using System.Security.SecureString;
        //public static void ConvertToUnsecureString(SecureString securePassword)
        //{
        //    IntPtr unmanagedString = IntPtr.Zero;
        //    try
        //    {
        //        unmanagedString = Marshal.SecureStringToGlobalAllocUnicode(securePassword); // convert the SecureString to IntPtr
        //        Console.WriteLine(Marshal.PtrToStringUni(unmanagedString)); // convert the IntPtr to regular string
        //    }
        //    finally
        //    {
        //        Marshal.ZeroFreeGlobalAllocUnicode(unmanagedString);    // cleanup the IntPtr
        //    }
        //}
    }

}
