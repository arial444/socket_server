using System;
using System.Text;
using SocketServer.MessageHandler.Commands;

namespace SocketServer.MessageHandler
{
    internal class HandleMessage
    {
        public string? response { get; set; }

        public HandleMessage(string message) {

            CommandsList commandsList = new CommandsList();
            bool? isExist = false;

            if (message != " ")
            {
                foreach (Command cmd in commandsList.commands)
                {
                    if (message == cmd.command) {
                        response = cmd.response;
                        isExist = true;
                    }

                    if (message == cmd.command + " -help") {
                        response = cmd.help;
                        isExist = true;
                    }
                }

                if (isExist == false)
                    response = "Sorry the command you entered is not exist, try using '-help'.";
            }
            else {
                response = "Please Enter Request.";
            }
        }
    }
}