﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Exceptions;
using Newtonsoft.Json;
using RabbitMQ.Client.MessagePatterns;
using RabbitMQ.Client.Events;



namespace Epilogger.Common
{
    
    public class MQ
    {

        public class MSGProducer : MSGTransBase
        {
            public MSGProducer(string Exchange, string queueName)
            {
                base.init();
                QueueName = queueName;
                ExchangeName = Exchange;
                Model.QueueDeclare(QueueName, true, false, false, null);
            }

            public void SendMessage(object obj)
            {
                string json = JsonConvert.SerializeObject(obj);
                IBasicProperties basicProperties = Model.CreateBasicProperties();
                Model.BasicPublish(ExchangeName, QueueName, basicProperties, Encoding.UTF8.GetBytes(json));
            }
            
        }


        public class MSGConsumer<T> : MSGTransBase
            where T : new()
        {
            protected Subscription _Subscription;
            protected BasicDeliverEventArgs _basicDeliveryEventArg;

            public MSGConsumer(string Exchange, string queueName)
            {
                base.init();
                QueueName = queueName;
                ExchangeName = Exchange;
                _Subscription = new Subscription(Model, QueueName, false);
            }

            public Subscription SetupSubscription()
            {
                return new Subscription(Model, QueueName, false);
            }

            public T GetMSG()
            {
                try
                {
                    _basicDeliveryEventArg = _Subscription.Next();
                    T messageContent = JsonConvert.DeserializeObject<T>(Encoding.UTF8.GetString(_basicDeliveryEventArg.Body));
                    return messageContent;
                }
                catch (Exception)
                {   
                    throw;
                }
            }
            
            public void AckMSG()
            {
                _Subscription.Ack(_basicDeliveryEventArg);
            }

            public void test()
            {

            }

        }

        


        public class MSGTransBase : IDisposable
        {
            protected IModel Model;
            protected IConnection Connection;
            protected string ExchangeName;
            protected string QueueName;

            public void init()
            {
                var connectionFactory = new ConnectionFactory();
                connectionFactory.HostName = "epilogger.com";
                connectionFactory.UserName = "epilogger";
                connectionFactory.Password = "xea,87,21r";
                Connection = connectionFactory.CreateConnection();
                Model = Connection.CreateModel();
            }

            public void Dispose()
            {
                if (Connection != null)
                    Connection.Close();
                if (Model != null)
                    Model.Abort();
            }

        }



        public class Messages
        {
            public enum SystemMessageType
            {
                Delete
            }

            [Serializable]
            [JsonObject(MemberSerialization.OptOut)]
            public class TwitterSearchMSG
            {
                public int EventID { get; set; }
                public string SearchTerms { get; set; }
                public bool SearchFromLatestTweet { get; set; }
                public DateTime? SearchSince { get; set; }
                public DateTime? SearchUntil { get; set; }
            }

            [Serializable]
            [JsonObject(MemberSerialization.OptOut)]
            public class ImageMSG
            {
                public long EventID { get; set; }
                public long TweetID { get; set; }
                public string TwitterName { get; set; }
                public string ImageSource { get; set; }
                public Uri ImageURL { get; set; }
                public string DisplayURL { get; set; }
            }

            [Serializable]
            [JsonObject(MemberSerialization.OptOut)]
            public class LinkMSG
            {
                public long EventID { get; set; }
                public long TweetID { get; set; }
                public string LinkSource { get; set; }
                public Uri Link { get; set; }
            }

            [Serializable]
            [JsonObject(MemberSerialization.OptOut)]
            public class ErrorsMSG
            {
                public string Error { get; set; }
                public string Source { get; set; }
                public object ErrorMSG { get; set; }
            }

            [Serializable]
            [JsonObject(MemberSerialization.OptOut)]
            public class SystemProcessingMSG
            {
                public long EventID { get; set; }
                public SystemMessageType Task { get; set; }
                public object TaskObjectData { get; set; }
            }



        }

        

        //public class MSGBase
        //{
        //    //public string ToJson(this object obj)
        //    //{
        //    //    JavaScriptSerializer serializer = new JavaScriptSerializer();
        //    //    return serializer.Serialize(obj);
        //    //}
        //}

        

        //public class MSGConsumer : MSGBase
        //{
        //    protected bool isConsuming;

        //    // used to pass messages back to UI for processing
        //    public delegate void onReceiveMessage(byte[] message);
        //    public event onReceiveMessage onMessageReceived;

        //    public MSGConsumer(string Exchange, string queueName)
        //    {
        //        base.init();
        //        QueueName = queueName;
        //        ExchangeName = Exchange;
        //        Model.QueueDeclare(QueueName, true, false, false, null);
        //    }

        //    //internal delegate to run the queue consumer on a seperate thread
        //    private delegate void ConsumeDelegate();

        //    public void StartConsuming()
        //    {
        //        isConsuming = true;
        //        ConsumeDelegate c = new ConsumeDelegate(Consume);
        //        c.BeginInvoke(null, null);
        //    }

        //    public T Consume()
        //    {
        //        QueueingBasicConsumer consumer = new QueueingBasicConsumer(Model);
        //        String consumerTag = Model.BasicConsume(QueueName, false, consumer);
        //        while (isConsuming)
        //        {
        //            try
        //            {
        //                RabbitMQ.Client.Events.BasicDeliverEventArgs e = (RabbitMQ.Client.Events.BasicDeliverEventArgs)consumer.Queue.Dequeue();
        //                IBasicProperties props = e.BasicProperties;
                        
        //                //e.Body should contain a Json msg of type T
                        
        //                //byte[] body = e.Body;
                        
                        
        //                // ... process the message
                        
                        
                        
        //                onMessageReceived(body);
        //                Model.BasicAck(e.DeliveryTag, false);
        //            }
        //            catch (OperationInterruptedException ex)
        //            {
        //                // The consumer was removed, either through
        //                // channel or connection closure, or through the
        //                // action of IModel.BasicCancel().
        //                return default(T);
        //                break;
        //            }
        //        }
        //    }

        //}

        

    }
}