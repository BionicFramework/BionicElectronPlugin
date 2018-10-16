using System;
using System.Diagnostics;
using System.IO;
using BionicCLI;
using BionicPlugin;
using McMaster.Extensions.CommandLineUtils;
using static BionicCore.DirectoryUtils;

namespace BionicElectronPlugin.Commands {
  [Command(Name = "serve", Description = "Serve Electron project")]
  public class ServeCommand : CommandBase {
    protected override int OnExecute(CommandLineApplication app) => Serve();

    private static int Serve() {
      Logger.Preparing("Serving Electron...");

      var cd = Directory.GetCurrentDirectory();
      try {
        Directory.SetCurrentDirectory(ToOSPath($"{cd}/platforms/electron"));
        Process.Start(
          new ProcessStartInfo("npm", $"start") {
            CreateNoWindow = true,
            UseShellExecute = false,
            RedirectStandardOutput = false
          }
        )?.WaitForExit();
      }
      catch (Exception) {
        Logger.Error("Something went wrong during electron serve. Please check platforms/electron");
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