using System;
using System.IO;
using BionicCLI;
using BionicCore;
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
        return ProcessHelper.RunCmd("npm", "start");
      }
      catch (Exception) {
        Logger.Error("Something went wrong during electron serve. Please check platforms/electron");
        return 1;
      }
      finally {
        Directory.SetCurrentDirectory(cd);
      }
    }
  }
}