using System;
using System.Collections.Generic;

namespace lab_8_Mediator
{
    interface IChatMediator
    {
        void SendMessage(string message, User sender, string receiverName);
    }

    abstract class User

    {
        protected IChatMediator _chatMediator;
        public string Name { get; }

        protected User(IChatMediator chatMediator,string name)
        {
            _chatMediator = chatMediator;
            Name = name;
        }

        public abstract void ReceiveMessage(string message);
    }
    
    class ChatMediator : IChatMediator
    {
        private List<User> _users = new List<User>();

        public void AddUser(User user)
        {
            _users.Add(user);
        }
        
        public void SendMessage(string message, User sender, string receiverName)
        {
           User receiver = _users.Find(usr => usr.Name== receiverName);

           if (receiver != null)
           {
               receiver.ReceiveMessage($"Message from: {sender.Name}, message: {message}");
           }
           else
           {
               Console.WriteLine($"User *{receiverName}* is not found");
           }
        }
    }
    
    class ConctreateUser : User
    {
        public ConctreateUser(IChatMediator chatMediator, string name) : base(chatMediator, name)
        {
        }

        public override void ReceiveMessage(string message)
        {
            Console.WriteLine($"{Name} received message; {message}");
        }

        public void SendMessage(string message, string receiverName)
        {
            _chatMediator.SendMessage(message,this,receiverName);
        }
    }

    internal class Program
    {
        public static void Main(string[] args)
        {
            ChatMediator mediator = new ChatMediator();

            ConctreateUser user1 = new ConctreateUser(mediator, "Ivan");
            ConctreateUser user2 = new ConctreateUser(mediator, "Petro");

            mediator.AddUser(user1);
            mediator.AddUser(user2);

            user1.SendMessage("Hello, Boris","Boris");
            user1.SendMessage("Hello, Petro", "Petro");
            user2.SendMessage("Hi, Ivan", "Ivan");
        }
    }
}