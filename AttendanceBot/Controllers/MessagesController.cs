using AttendanceBot.Commands;
using AttendanceBot.Helpers;
using Microsoft.Bot.Connector;
using System;
using System.Threading.Tasks;
using System.Web.Http;
using System.Linq;

namespace AttendanceBot
{
    //[BotAuthentication]
    public class MessagesController : ApiController
    {        
        public async Task<Message> Post([FromBody]Message message)
        {
            if (message.Type == "Message")
            {
                if (message.Text.EqualsIgnoreCase("$help"))
                {
                    var commandReplyMessage = "## Available commands: " + 2.Lines() +
                        string.Join(Environment.NewLine, CommandProcessor.ListAvailableCommands().Select(c => "- " + c));

                    return message.CreateReplyMessage(commandReplyMessage);
                }

                var result = CommandProcessor.Process(message);
                Message reply = null;  
                result.IfSome(
                        replyMessage => reply = message.CreateReplyMessage(replyMessage)                        
                    );

                return reply;
            }
            else
            {
                return HandleSystemMessage(message);
            }
        }

        private Message HandleSystemMessage(Message message)
        {
            if (message.Type == "Ping")
            {
                Message reply = message.CreateReplyMessage();
                reply.Type = "Ping";
                return reply;
            }
            else if (message.Type == "DeleteUserData")
            {
                // Implement user deletion here
                // If we handle user deletion, return a real message
            }
            else if (message.Type == "BotAddedToConversation")
            {
                return message.CreateReplyMessage("Hi everyone, I'm Attendance Bot, who will help you keep track of attendance for different events, type $help to see list of available commands");
            }
            else if (message.Type == "BotRemovedFromConversation")
            {
                return message.CreateReplyMessage("Bye...I'm leaving");
            }
            else if (message.Type == "UserAddedToConversation")
            {                
                return message.CreateReplyMessage($"Welcome {message.From.Name}");
            }
            else if (message.Type == "UserRemovedFromConversation")
            {
            }
            else if (message.Type == "EndOfConversation")
            {
            }

            return null;
        }
    }
}