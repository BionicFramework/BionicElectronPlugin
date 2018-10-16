using System.IO;
using BionicCLI;
using BionicPlugin;
using McMaster.Extensions.CommandLineUtils;
using static BionicCore.DirectoryUtils;

namespace BionicElectronPlugin.Commands {
  [Command(Name = "build", Description = "Build Electron project")]
  public class BuildCommand : CommandBase {
    protected override int OnExecute(CommandLineApplication app) => Build();

    private static int Build() {
      Logger.Preparing("Building Electron...");

      var cd = Directory.GetCurrentDirectory();

      var wwwroot = ToOSPath($"{cd}/wwwroot");
      var electron = ToOSPath($"{cd}/platforms/electron");
      var release = ToOSPath($"{cd}/bin/Release/netstandard2.0/dist");
      var debug = ToOSPath($"{cd}/bin/Debug/netstandard2.0/dist");

      if (!CopyDir(wwwroot, electron)) {
        Logger.Error("Please make sure your are in a Blazor Client or Standalone directory");
        return 1;
      }

      var result = CopyDir(release, electron) || CopyDir(debug, electron);
      if (!result) {
        Logger.Error("Unable to find compiled project or electron has not yet been initialized.\nPlease build your project first. e.g.: dotnet build");
        return 1;
      }

      Logger.Success("Electron successfully built. Try: bionic platform electron serve");
      
      return 0;
    }
  }
}