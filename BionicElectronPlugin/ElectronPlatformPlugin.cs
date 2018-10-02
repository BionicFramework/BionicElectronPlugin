using System;
using BionicElectronPlugin.Commands;
using BionicPlugin;
using McMaster.Extensions.CommandLineUtils;

namespace BionicElectronPlugin {
  public class ElectronPlatformPlugin : ICommandPlugin {
    public string CommandName { get; } = "electron";

    public void Initialize(CommandLineApplication app) {
      
      app.Commands.Add(new CommandLineApplication<InitCommand>());
      app.Commands.Add(new CommandLineApplication<BuildCommand>());
      app.Commands.Add(new CommandLineApplication<ServeCommand>());

      app.OnExecute(() => app.ShowHelp());
    }

    public int Execute() {
      Console.WriteLine("Hello from Bionic Electron Platform Plugin");
      return 0;
    }

    public int OnExecute(CommandLineApplication app) => Execute();
  }
}