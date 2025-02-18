using System;
using System.IO;
using System.Security.Cryptography;

namespace Hash {

  public class Program {

    private static void Main(string[] args) {
      string filename = null;
      string hash = null;
      bool? compute = null;

      for (int i = 0; i < args.Length; i++)
        switch (args[i]) {
          case "-c":
            compute = true;
            break;

          case "-v":
            compute = false;
            break;

          case "-f":
            if (i + 1 < args.Length)
              filename = args[++i];
            else
              Console.WriteLine("Argument -f missing filename");
            break;

          case "-h":
            if (i + 1 < args.Length)
              hash = args[++i];
            else
              Console.WriteLine("Argument -h missing hash");
            break;

          default:
            Console.WriteLine("Unexpected argument: " + args[i]);
            break;
        }

      if (compute == true) {
        // validate arguments
        if (filename != null && File.Exists(filename))
          // compute hash
          Console.WriteLine(ComputeHash(filename));
        else
          Console.WriteLine("Compute hash, missing filename or unknown file");

      } else if (compute == false) {
        // validate arguments
        if (filename != null && File.Exists(filename)) {
          if (hash != null) {
            // verify hash
            if (ComputeHash(filename) == hash)
              Console.WriteLine("File verified");
            else
              Console.WriteLine("File failed verification!");

          } else
            Console.WriteLine("Verify hash, missing hash");

        } else
          Console.WriteLine("Verify hash, missing filename or unknown file");

      } else // compute == null
        Console.WriteLine("Missing -c or -v (i.e. compute or verify hash)");
    }

    private static string ComputeHash(string filename) {
      MD5 hashCalculator = new MD5CryptoServiceProvider();
      byte[] bytes = hashCalculator.ComputeHash(File.ReadAllBytes(filename));
      return Convert.ToBase64String(bytes);
    }
  }
}
