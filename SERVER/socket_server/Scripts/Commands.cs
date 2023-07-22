using System;
using System.Text;
using System.Collections.Generic;
using SocketServer.MessageHandler.Commands.Plugins;

namespace SocketServer.MessageHandler.Commands
{
    internal class CommandsList
    {
        public List<Command>? commands = new List<Command>();
        PluginsList plugins = new PluginsList();
        public CommandsList() {
            commands.Add(new Command(
                " -myIP",
                "my ip is:",
                "-myIP - Show you your own IP adress."));
            commands.Add(new Command(
                " -disconnect",
                plugins.Disconnect(""),
                "-disconnect - Disconnect from the server."));
            commands.Add(new Command(
                " -help",
                plugins.Help(commands),
                "-help - get all list of commands."));
        }
    }

    internal class Command 
    {
        public string? command { get; set; }
        public string? response { get; set; }
        public string? help { get; set; }

        public Command(string command, string response, string help)
        {
            this.command = command;
            this.response = response;
            this.help = help;
        }
    }
}