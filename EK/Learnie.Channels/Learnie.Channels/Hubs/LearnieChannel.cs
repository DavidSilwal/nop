using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Polly;
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;

namespace Learnie.Channels.Hubs
{

    public class Client
    {
        public string Name { get; set; }
        public string Role { get; set; }
        public string Room { get; set; }
        public string StreamConnectionId { get; set; }
    }
    public class LearnieChannel : Hub
    {
        private readonly ILogger<LearnieChannel> _logger;

        private static readonly ConcurrentDictionary<string, Client> _clients = new ConcurrentDictionary<string, Client>();

        public LearnieChannel(ILogger<LearnieChannel> logger)
        {
            _logger = logger;
        }

        public override Task OnConnectedAsync()
        {
            var request = Context.GetHttpContext().Request;
            var client = new Client
            {
                Name = request.Query["Name"].ToString(),
                Role = request.Query["Role"].ToString(),
                Room = request.Query["Room"].ToString(),
            };
            _clients.TryAdd(Context.ConnectionId, client);

            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            _clients.TryRemove(Context.ConnectionId, out var _);
            return base.OnDisconnectedAsync(exception);
        }

        public void SetStreamId(string streamId)
        {
            if (_clients.TryGetValue(Context.ConnectionId, out Client oldClient))
            {
                var newClient = oldClient;
                newClient.StreamConnectionId = streamId;
                _clients.TryUpdate(Context.ConnectionId, newClient, oldClient);
            }
        }

        public Client GetClientByStreamId(string streamConnectionId)
        {
            var result = Policy
                 .Handle<NullReferenceException>()
                 .WaitAndRetry(new[]
                 {
                    TimeSpan.FromMilliseconds(500),
                    TimeSpan.FromMilliseconds(1000),
                    TimeSpan.FromMilliseconds(1500),
                    TimeSpan.FromMilliseconds(2000),
                    TimeSpan.FromSeconds(3),
                    TimeSpan.FromSeconds(4),
                    TimeSpan.FromSeconds(5),
                    TimeSpan.FromSeconds(6),
                 }, (exception, timeSpan) =>
                 {
                     // Add logic to be executed before each retry, such as logging    
                     _logger.LogError(exception.Message);
                 })
                 .Execute(() => FirstOrDefault(streamConnectionId));

            return result;
        }


        public static Client FirstOrDefault(string Id)
        {
            var result = _clients.FirstOrDefault(x =>x.Value.StreamConnectionId !=null &&  x.Value.StreamConnectionId == Id).Value;
            //var result = _clients.TryGetValue(Id, out Client client) ? client : default;
            if (result == default)
            {
                throw new NullReferenceException();
            }
            return result;
        }

        public string GetRoleByStreamId(string streamConnectionId)
        {
            return _clients.TryGetValue(streamConnectionId, out Client client) ? client.Role : default;
        }

        public ConcurrentDictionary<string, Client> All()
        {
            return _clients;
        }
    }
}
