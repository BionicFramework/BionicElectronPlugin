using System;
using System.Diagnostics;
using System.IO;
using McMaster.Extensions.CommandLineUtils;

namespace BionicElectronPlugin.Commands {
  [Command(Name = "init", Description = "Initialize Electron project structure")]
  public class InitCommand : CommandBase {
    protected override int OnExecute(CommandLineApplication app) => Init();

    private static int Init() {
      Console.WriteLine("☕  Initializing Electron...");

      // TODO: Check if already installed
      Process.Start(
        DotNetExe.FullPathOrDefault(),
        "new -i BionicElectronTemplate"
      )?.WaitForExit();

      // TODO: Check if platforms/electron already exists
      Process.Start(
        DotNetExe.FullPathOrDefault(),
        "new bionic.electron -o platforms"
      )?.WaitForExit();

      var cd = Directory.GetCurrentDirectory();
      try {
        Directory.SetCurrentDirectory($"{cd}/platforms/electron");
        Process.Start(
          new ProcessStartInfo("npm", $"install") {
            CreateNoWindow = true,
            UseShellExecute = false,
            RedirectStandardOutput = false
          }
        )?.WaitForExit();
      }
      catch (Exception) {
        Console.WriteLine($"☠  Something went wrong during Electron install. Please check platforms/electron");
        return 1;
      }
      finally
      {
        Directory.SetCurrentDirectory(cd);
      }

      Console.WriteLine("🚀  Electron is ready to go! - try: bionic platform electron serve");
      return 1;
    }
  }
}