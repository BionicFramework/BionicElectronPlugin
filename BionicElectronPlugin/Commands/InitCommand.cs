using System;
using System.Diagnostics;
using System.IO;
using BionicCLI;
using BionicCore;
using BionicPlugin;
using McMaster.Extensions.CommandLineUtils;
using static BionicCore.DirectoryUtils;

namespace BionicElectronPlugin.Commands {
  [Command(Name = "init", Description = "Initialize Electron project structure")]
  public class InitCommand : CommandBase {
    protected override int OnExecute(CommandLineApplication app) => Init();

    private static int Init() {
      Logger.Preparing("Initializing Electron...");

      // TODO: Check if already installed
      DotNetHelper.RunDotNet("new -i BionicElectronTemplate");

      // TODO: Check if platforms/electron already exists
      DotNetHelper.RunDotNet("new bionic.electron -o platforms");

      var cd = Directory.GetCurrentDirectory();
      int exitCode;
      try {
        Directory.SetCurrentDirectory(ToOSPath($"{cd}/platforms/electron"));
        exitCode = ProcessHelper.RunCmd("npm", "install");
      }
      catch (Exception) {
        Logger.Error("Something went wrong during Electron install. Please check platforms/electron");
        return 1;
      }
      finally
      {
        Directory.SetCurrentDirectory(cd);
      }

      if (exitCode == 0) Logger.Success("Electron is ready to go! - try: bionic platform electron serve");
      return exitCode;
    }
  }
}