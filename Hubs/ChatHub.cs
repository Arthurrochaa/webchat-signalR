using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using web_app_chat.Models;
using web_app_chat.Repositories;

namespace web_app_chat.Hubs
{
    public class ChatHub : Hub
    {
        private readonly static ConnectionsRepository _connections = new ConnectionsRepository();

        // Retorna lista de usuário no chat e usuário que acabou de logar

        public override Task OnConnectedAsync()
        {
            var user = JsonConvert.DeserializeObject<User>(Context.GetHttpContext().Request.Query["user"]);
            _connections.Add(Context.ConnectionId, user);
            Clients.All.SendAsync("chat", _connections.GetUsers(), user);
            return base.OnConnectedAsync();
        }

        // Método responsável por encaminhar as mensagens pelo hub
        public async Task SendMessage(ChatMessage chat)
        {
            //Ao usar o método Client(_connections.GetUserId(chat.destination)) eu estou enviando a mensagem apenas para o usuário destino, não realizando broadcast
            await Clients.Client(_connections.GetUserById(chat.destination)).SendAsync("Receive", chat.sender, chat.message);
        }
    }
}
