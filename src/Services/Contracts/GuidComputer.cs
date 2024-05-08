using System.Security.Cryptography;
using System.Text;

namespace Contracts;

public static class GuidComputer
{
    public static Guid Calculate(string input)
    {
        byte[] hash = MD5.HashData(Encoding.UTF8.GetBytes(input));

        var guid = new Guid(hash);

        return guid;
    }
}