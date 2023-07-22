using System;
using System.Text;
using System.Collections.Generic;

namespace SocketServer.MessageHandler.Commands.Plugins
{
    internal class PluginsList
    {
        public PluginsList() {}

        public string Help(List<Command> commands) {
            string response = string.Empty;

            response += "Commands List: \n";
            response += "-help - get all list of commands.\n";

            foreach (Command cmd in commands) {
                response += cmd.help + "\n";
            }

            return response;
        }

        public string Disconnect(string ip) {
            return ip + " is Disconnected.";
        }
    }
}