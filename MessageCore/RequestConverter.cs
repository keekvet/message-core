using MessageCore;
using MessageCore.DTO;
using MessageCore.Models;
using Newtonsoft.Json;
using System;
using System.Net.Sockets;
using System.Text;

namespace MessageRequest
{
    public class RequestConverter
    {
        private static int requestTypeLenght;

        static RequestConverter()
        {
            requestTypeLenght = RequestType.Login.ToString("X").Length;
        }

        //=============CORE CONVERTIONS=======

        public static string CreateDatagram(RequestType type, string data)
        {
            return type.ToString("X") + data;
        }

        public static RequestType GetRequestType(string data)
        {
            string typeInHex = data.Substring(0, requestTypeLenght);
            return (RequestType)int.Parse(typeInHex, System.Globalization.NumberStyles.HexNumber);
        }

        public static string GetData(string data)
        {
            return data.Substring(requestTypeLenght, data.Length - requestTypeLenght);
        }

        //==============LOGIN==================

        public static string ComposeLogin(User user)
        {
            return CreateDatagram(RequestType.Login, JsonConvert.SerializeObject(user));
        }

        public static User DecomposeLogin(string data)
        {
            return JsonConvert.DeserializeObject<User>(data);
        }

        public static string ComposeLoginResponse(bool response)
        {
            return CreateDatagram(RequestType.LoginResponse, JsonConvert.SerializeObject(response));
        }

        public static bool DecomposeLoginResponse(string data)
        {
            return JsonConvert.DeserializeObject<bool>(data);
        }

        //==============REGISTRAION==================

        public static string ComposeRegistration(User user)
        {
            return CreateDatagram(RequestType.Registration, JsonConvert.SerializeObject(user));
        }

        public static User DecomposeRegistration(string data)
        {
            return JsonConvert.DeserializeObject<User>(data);
        }

        public static string ComposeRegistrationResponse(bool response)
        {
            return CreateDatagram(RequestType.Registration, JsonConvert.SerializeObject(response));
        }

        public static bool DecomposeRegistrationResponse(string data)
        {
            return JsonConvert.DeserializeObject<bool>(data);
        }

        //==============MESSAGE================

        public static string ComposeMessage(Message message)
        {
            return CreateDatagram(
                RequestType.SendMessage,
                JsonConvert.SerializeObject(message));
        }

        public static Message DecomposeMessage(string messageData)
        {
            return JsonConvert.DeserializeObject<Message>(messageData);
        }

        public static string ComposeMessageReceived(int id)
        {
            return CreateDatagram(RequestType.MessageReceived, JsonConvert.SerializeObject(id));
        }

        public static int DecomposeMessageReceived(string data)
        {
            return JsonConvert.DeserializeObject<int>(data);
        }

        public static string ComposeMessageSyncInfo(int id, int localId, DateTime newDateTime)
        {
            return CreateDatagram(
                RequestType.MessageSyncInfo, 
                JsonConvert.SerializeObject(new MessageSyncInfo(id, localId, newDateTime)));
        }

        public static MessageSyncInfo DecomposeMessageSyncInfo(string data)
        {
            return JsonConvert.DeserializeObject<MessageSyncInfo>(data);
        }

        public static string ComposeMessageSynchronised(int id)
        {
            return CreateDatagram(RequestType.MessageSynchronised, JsonConvert.SerializeObject(id));
        }

        public static int DecomposeMessageSynchronised(string data)
        {
            return JsonConvert.DeserializeObject<int>(data);
        }


        //==============GET MESSAGES==================

        public static string ComposeGetAllMessages()
        {
            return CreateDatagram(RequestType.GetAllStoredMessages, string.Empty);
        }

        //==============USER==================

        public static string ComposeUserExist( string userToFind)
        {
            return CreateDatagram(
                RequestType.CheckUserExist,
                JsonConvert.SerializeObject( userToFind ));
        }

        public static string DecomposeUserExist(string data)
        {
            return JsonConvert.DeserializeObject<string>(data);
        }

        public static string ComposeUserExistResponse(bool result)
        {
            return CreateDatagram(
                RequestType.CheckUserExistResponse,
                JsonConvert.SerializeObject(result));
        }


        public static bool DecomposeUserExistResponse(string data)
        {
            return JsonConvert.DeserializeObject<bool>(data);
        }
    }

    public static class Package
    {

        public static string Read(NetworkStream ns)
        {
            byte[] buffer = new byte[ConfigurarionVars.TCP_BUFFER_SIZE];

            ns.Read(buffer, 0, buffer.Length);
            return Encoding.UTF8.GetString(buffer, 0, buffer.Length);
        }

        public static void Write(NetworkStream ns, string data)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(data);

            Array.Resize(ref buffer, ConfigurarionVars.TCP_BUFFER_SIZE);
            ns.Write(buffer, 0, buffer.Length);
        }
    }

    public enum RequestType
    {
        Login,
        LoginResponse,
        Registration,
        RegistrationResponse,
        SendMessage,
        MessageReceived,
        MessageSyncInfo,
        MessageSynchronised,
        GetAllStoredMessages,
        CheckUserExist,
        CheckUserExistResponse,
    }
}
