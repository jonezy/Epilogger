using System;
using System.Collections.Generic;
using System.IO;
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
                var connectionFactory = new ConnectionFactory
                                            {
                                                HostName = "epilogger.com",
                                                Port = 5672,
                                                UserName = "epilogger",
                                                Password = "xea,87,21r"
                                            };
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

            public enum MediaType
            {
                Tweet,
                Photo
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
            public class TwitterImageMSG
            {
                public int EventID { get; set; }
                public long TweetID { get; set; }
                public string TwitterName { get; set; }
                public string ImageSource { get; set; }
                public Uri ImageURL { get; set; }
                public string DisplayURL { get; set; }
            }

            [Serializable]
            [JsonObject(MemberSerialization.OptOut)]
            public class FacebookImageMSG
            {
                public int EventId { get; set; }
                public string AlbumId { get; set; }
                public Uri ImageUrl { get; set; }
                public Guid UserId { get; set; }

                public FacebookImageMSG()
                {
                }

                public FacebookImageMSG(int eventId, string albumId, Uri imageUrl, Guid userid)
                {
                    EventId = eventId;
                    AlbumId = albumId;
                    ImageUrl = imageUrl;
                    UserId = userid;
                }
            }

            [Serializable]
            [JsonObject(MemberSerialization.OptOut)]
            public class FlickrImageMSG
            {
                public int EventId { get; set; }
                public Uri ImageUrl { get; set; }

                public FlickrImageMSG()
                {
                }

                public FlickrImageMSG(int eventId, Uri imageUrl)
                {
                    EventId = eventId;
                    ImageUrl = imageUrl;
                }
            }

            [Serializable]
            [JsonObject(MemberSerialization.OptOut)]
            public class FileImageMSG
            {
                public int EventID { get; set; }
                public long? UserID { get; set; }
                public byte[] Content { get; set; }
                public string FileName { get; set; }

                public FileImageMSG()
                {
                }

                public FileImageMSG(int eventId, long? userId, byte[] content, string fileName)
                {
                    EventID = eventId;
                    UserID = userId;
                    Content = content;
                    FileName = fileName;
                }
            }

            [Serializable]
            [JsonObject(MemberSerialization.OptOut)]
            public class LinkMSG
            {
                public int EventID { get; set; }
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

            [Serializable]
            [JsonObject(MemberSerialization.OptOut)]
            public class TrendingUpdateMSG
            {
                public long EventID { get; set; }
                public MediaType MediaType { get; set; }
                public int Incrementvalue { get; set; }
                public DateTime UpdateDateTime { get; set; }
            }

            [Serializable]
            [JsonObject(MemberSerialization.OptOut)]
            public class TweetSenderMsg
            {
                public string TweetText { get; set; }
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
