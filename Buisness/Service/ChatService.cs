using System;
using System.Collections.Generic;
using Database.Context;
using Database.Model;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Buisness.Service
{
    public class ChatService(FTMContext context)
    {
        private readonly FTMContext _context = context;

        public async Task<Result> StartChat(string playerId, string agentId, string clientId)
        {
            if (!await _context.Player.AnyAsync(x => x.PlayerId == playerId))
                return new Result(false, "Player does not exist");

            if (!await _context.AgentInfo.AnyAsync(x => x.AgentId == agentId))
                return new Result(false, "Agent does not exist");

            if (!await _context.UserInfo.AnyAsync(x => x.UserInfoId == clientId))
                return new Result(false, "Client does not exist");

            var existingChat = await _context.Chat.FirstOrDefaultAsync(x =>
                x.PlayerId == playerId &&
                x.AgentId == agentId &&
                x.ClientId == clientId);

            if (existingChat != null)
                return new Result(true, "Chat already exists", existingChat);

            var chat = new Chat
            {
                PlayerId = playerId,
                AgentId = agentId,
                ClientId = clientId
            };

            await _context.Chat.AddAsync(chat);
            return await Result.DBcommit(_context, "Chat started", "Failed to start chat", chat);
        }


        public async Task<Result> SendMessage(string chatId, string senderId, string senderType, string messageText)
        {
            if (!await _context.Chat.AnyAsync(x => x.ChatId == chatId))
                return new Result(false, "Chat does not exist");

            if (string.IsNullOrWhiteSpace(messageText))
                return new Result(false, "Message cannot be empty");

            if (senderType != "Agent" && senderType != "Client")
                return new Result(false, "Invalid sender type");

            var message = new ChatMessage
            {
                ChatId = chatId,
                SenderId = senderId,
                SenderType = senderType,
                MessageText = messageText
            };

            await _context.ChatMessage.AddAsync(message);


            var chat = await _context.Chat.FindAsync(chatId);
            chat!.LastMessageDate = DateTime.UtcNow;
            return await Result.DBcommit(_context, "Message sent", "Failed to send message", message);
        }

        public async Task<Result> GetChatMessages(string chatId)
        {
            try
            {
                var messages = await _context.ChatMessage
                    .Where(x => x.ChatId == chatId)
                    .OrderBy(x => x.SentDate)
                    .ToListAsync();

                return new Result(true, "Success", messages);
            }
            catch (Exception ex)
            {
                return new Result(false, ex.Message);
            }
        }

        public async Task<Result> GetUserChats(string userId, string userType)
        {
            try
            {
                List<Chat> chats;

                if (userType == "Agent")
                {
                    chats = await _context.Chat
                        .Where(x => x.AgentId == userId)
                        .OrderByDescending(x => x.LastMessageDate)
                        .ToListAsync();
                }
                else if (userType == "Client")
                {
                    chats = await _context.Chat
                        .Where(x => x.ClientId == userId)
                        .OrderByDescending(x => x.LastMessageDate)
                        .ToListAsync();
                }
                else
                {
                    return new Result(false, "Invalid user type");
                }

                return new Result(true, "Success", chats);
            }
            catch (Exception ex)
            {
                return new Result(false, ex.Message);
            }
        }

        public async Task<Result> GetChatById(string chatId)
        {
            try
            {
                var chat = await _context.Chat
                    .Include(x => x.Player)
                    .Include(x => x.Agent)
                    .Include(x => x.Client)
                    .FirstOrDefaultAsync(x => x.ChatId == chatId);

                if (chat == null)
                    return new Result(false, "Chat not found");

                return new Result(true, "Success", chat);
            }
            catch (Exception ex)
            {
                return new Result(false, ex.Message);
            }
        }

        public async Task<Result> MarkAsRead(string chatId, string userId)
        {
            try
            {
                var messages = await _context.ChatMessage
                    .Where(x => x.ChatId == chatId &&
                                x.SenderId != userId &&
                                x.IsRead == false)
                    .ToListAsync();

                foreach (var message in messages)
                {
                    message.IsRead = true;
                }

                return await Result.DBcommit(_context, "Messages marked as read", "Failed");
            }
            catch (Exception ex)
            {
                return new Result(false, ex.Message);
            }
        }

        public async Task<Result> GetUnreadCount(string chatId, string userId)
        {
            try
            {
                var count = await _context.ChatMessage
                    .Where(x => x.ChatId == chatId &&
                                x.SenderId != userId &&
                                x.IsRead == false)
                    .CountAsync();

                return new Result(true, "Success", new { UnreadCount = count });
            }
            catch (Exception ex)
            {
                return new Result(false, ex.Message);
            }
        }

        public async Task<Result> DeleteChat(string chatId)
        {
            if (!await _context.Chat.AnyAsync(x => x.ChatId == chatId))
                return new Result(false, "Chat does not exist");

            var messages = await _context.ChatMessage
                .Where(x => x.ChatId == chatId)
                .ToListAsync();

            _context.ChatMessage.RemoveRange(messages);

            var chat = await _context.Chat.FindAsync(chatId);
            if (chat == null)
                return new Result(false, "Chat does not exist");

            _context.Chat.Remove(chat);
            return await Result.DBcommit(_context, "Chat deleted", "Failed to delete");
        }

        public async Task<Result> DeleteMessage(string messageId)
        {
            if (!await _context.ChatMessage.AnyAsync(x => x.MessageId == messageId))
                return new Result(false, "Message does not exist");

            var message = await _context.ChatMessage.FindAsync(messageId);

            if (message == null)
                return new Result(false, "Message does not exist");

            _context.ChatMessage.Remove(message);
            return await Result.DBcommit(_context, "Message deleted", "Failed to delete");
        }

        public async Task<Result> GetChatsByPlayer(string playerId)
        {
            try
            {
                var chats = await _context.Chat
                    .Where(x => x.PlayerId == playerId)
                    .OrderByDescending(x => x.LastMessageDate)
                    .ToListAsync();

                return new Result(true, "Success", chats);
            }
            catch (Exception ex)
            {
                return new Result(false, ex.Message);
            }
        }
    }
}