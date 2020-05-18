using System;
using BestHTTP.SocketIO;
using BestHTTP.SocketIO.Transports;
using Newtonsoft.Json;
using UnityEngine;

namespace Silicom.Name
{
    public class SilicomRemoteServerHandler : SilicomServerHandler
    {

        private SocketManager _socketManager;
        private Socket _socket;
        private SocketOptions _socketOptions;

        // TODO : load this from config file ?
        private string _serverAddress = "http://localhost:64210";

        private bool _connected;

        private void Start()
        {
            CreateSocket();
            SetListeners();
        }

        private void Update()
        {
            if (!_connected) return;
            
            
        }


        private void CreateSocket()
        {
            _socketOptions = new SocketOptions
            {
                ReconnectionAttempts = 10,
                AutoConnect = true,
                ReconnectionDelay = TimeSpan.FromMilliseconds(1000),
                ConnectWith = TransportTypes.WebSocket
            };

            _socketManager = new SocketManager(new Uri($"{_serverAddress}/socket.io/"), _socketOptions);

            _socket = _socketManager.Socket;
        }

        private void SetListeners()
        {
            _socket.Once("CONNECTED", OnConnected);
            _socket.On("SILICOM_DATA", OnSilicomData);
            _socket.On("error", OnError);
        }

        private void OnDestroy()
        {
            CloseConnection();
        }

        private void CloseConnection()
        {
            _connected = false;
            _socket?.Disconnect();
            _socketManager?.Close();
        }


        #region Socket event handlers

        private void OnConnected(Socket socket, Packet packet, object[] args)
        {
            _connected = true;
            // TODO
            Debug.Log($"Connected to server {_serverAddress}");
        }
        
        private static void OnSilicomData(Socket s, Packet packet, params object[] args)
        {
            SilicomData data = JsonConvert.DeserializeObject<SilicomData>(args[0].ToString());

            // TODO
            SilicomClient.Instance.QueueData(data);
        }
        
        private static void OnError(Socket s, Packet packet, params object[] args)
        {
            if (!(args[0] is Error error)) return;
            Debug.Log($"Server error : {error}");
        }
        

        #endregion

        public override void SendData(SilicomData data)
        {
            _socket.Emit("SILICOM_DATA", JsonConvert.SerializeObject(data));
        }

    }
}