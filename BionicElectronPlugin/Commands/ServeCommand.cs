using System;
using System.Diagnostics;
using System.IO;
using McMaster.Extensions.CommandLineUtils;

namespace BionicElectronPlugin.Commands {
  [Command(Name = "serve", Description = "Serve Electron project")]
  public class ServeCommand : CommandBase {
    protected override int OnExecute(CommandLineApplication app) => Serve();

    private static int Serve() {
      Console.WriteLine("☕  Serving Electron...");

      var cd = Directory.GetCurrentDirectory();
      try {
        Directory.SetCurrentDirectory($"{cd}/platforms/electron");
        Process.Start(
          new ProcessStartInfo("npm", $"start") {
            CreateNoWindow = true,
            UseShellExecute = false,
            RedirectStandardOutput = false
          }
        )?.WaitForExit();
      }
      catch (Exception) {
        Console.WriteLine($"☠  Something went wrong during electron serve. Please check platforms/electron");
        return 1;
      }
      finally
      {
        Directory.SetCurrentDirectory(cd);
      }

      return 0;
    }
  }
}