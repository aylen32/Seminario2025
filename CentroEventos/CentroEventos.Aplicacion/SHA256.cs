using System;
using System.Text;
using System.Security.Cryptography;
 
public class Hash
{
    static string ComputeSHA256(string s)
    {
        string hash = String.Empty;
 
        // Inicializar un objeto hash SHA256
        using (SHA256 sha256 = SHA256.Create())
        {
            // Calcular el hash de la cadena dada
            byte[] hashValue = sha256.ComputeHash(Encoding.UTF8.GetBytes(s));
 
            // Convierte la matriz de bytes a formato de cadena
            foreach (byte b in hashValue) {
                hash += $"{b:X2}";
            }
        }
 
        return hash;
    }
 
 
}

