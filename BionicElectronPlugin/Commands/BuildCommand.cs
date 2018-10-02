using System;
using System.IO;
using System.Linq;
using McMaster.Extensions.CommandLineUtils;

namespace BionicElectronPlugin.Commands {
  [Command(Name = "build", Description = "Build Electron project")]
  public class BuildCommand : CommandBase {
    protected override int OnExecute(CommandLineApplication app) => Build();

    private static int Build() {
      Console.WriteLine("☕  Building Electron...");

      var cd = Directory.GetCurrentDirectory();

      var wwwroot = $"{cd}/wwwroot";
      var electron = $"{cd}/platforms/electron";
      var release = $"{cd}/bin/Release/netstandard2.0/dist";
      var debug = $"{cd}/bin/Debug/netstandard2.0/dist";

      if (!CopyDir(wwwroot, electron)) {
        Console.WriteLine("☠  Please make sure your are in a Blazor Client or Standalone directory");
        return 1;
      }

      var result = CopyDir(release, electron) || CopyDir(debug, electron);
      if (!result) {
        Console.WriteLine(
          "☠  Unable to find compiled project or electron has not yet been initialized.\nPlease build your project first. e.g.: dotnet build");
        return 1;
      }

      Console.WriteLine(
        "🚀  Electron successfully built. Try: bionic platform electron serve");
      
      return 0;
    }

    private static bool CopyDir(string sourceDirName, string destDirName, bool copySubDirs = true) {
      var dir = new DirectoryInfo(sourceDirName);

      if (!dir.Exists) return false;

      var dirs = dir.GetDirectories();
      if (!Directory.Exists(destDirName)) Directory.CreateDirectory(destDirName);

      var files = dir.GetFiles();
      foreach (var file in files) {
        var destPath = Path.Combine(destDirName, file.Name);
        file.CopyTo(destPath, true);
      }

      if (!copySubDirs) return true;
      
      return !(
        from subdir in dirs
        let destPath = Path.Combine(destDirName, subdir.Name)
        where !CopyDir(subdir.FullName, destPath, copySubDirs)
        select subdir).Any();
    }
  }
}