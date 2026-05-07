using System.Text;

namespace CSaP.CourseProject.RSA;

class Program
{
    static void Main()
    {
        Console.InputEncoding = Encoding.UTF8;
        Console.OutputEncoding = Encoding.UTF8;

        string text = "Ніхто нам не збудує держави, коли ми її самі не збудуємо, і ніхто з нас не зробить нації, коли ми самі нацією не схочемо бути. В’ячеслав Липинський";
        RSA rsa = RSA.Create();
        Wrapper wrapper = new Wrapper(rsa);

//      ========================= Lab 3: Encrypting and Decrypting of the text =========================

        byte[] encryptedData = wrapper.Encrypt(text);
        string encryptedText = Encoding.UTF8.GetString(encryptedData);
        string decryptedText = wrapper.DecryptToString(encryptedData);

        Console.WriteLine("========================= Lab 3: Encrupting and Decrypting of the text =========================\n");

        Console.WriteLine($"Plain text - {text}");

        Console.WriteLine($"Encrypted text in string representation - {encryptedText}\n");
        Console.WriteLine($"Decrypted text in string representation - {decryptedText}\n\n");


//      =========================== Lab 4: Signing and Verifying of the text ===========================

        byte[] signature = wrapper.Sign(text);
        string signatureInStringFormat = string.Join(", ", signature.Select(b => $"0x{b:X2}"));

        Console.WriteLine("=========================== Lab 4: Signing and Verifying of the text ===========================\n");

        Console.WriteLine("Used the same plain text as before.\n");

        Console.WriteLine($"Signature: {signatureInStringFormat}\n");
        Console.WriteLine($"Whether signature represents text, provided before: {rsa.Verify(Encoding.UTF8.GetBytes(text), signature)}");
    }
}