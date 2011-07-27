


using System;
using System.ComponentModel;
using System.Linq;

namespace Epilogger.Data
{
    
    
    
    
    /// <summary>
    /// A class which represents the EventCategories table in the Epilogger Database.
    /// This class is queryable through EpiloggerDB.EventCategory 
    /// </summary>

	public partial class EventCategory: INotifyPropertyChanging, INotifyPropertyChanged
	{
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
	    
	    public EventCategory(){
	        OnCreated();
	    }
	    
	    #region Properties
	    
        partial void OnIDChanging(int value);
        partial void OnIDChanged();
		
		private int _ID;
		public int ID { 
		    get{
		        return _ID;
		    } 
		    set{
		        this.OnIDChanging(value);
                this.SendPropertyChanging();
                this._ID = value;
                this.SendPropertyChanged("ID");
                this.OnIDChanged();
		    }
		}
		
        partial void OnCategoryNameChanging(string value);
        partial void OnCategoryNameChanged();
		
		private string _CategoryName;
		public string CategoryName { 
		    get{
		        return _CategoryName;
		    } 
		    set{
		        this.OnCategoryNameChanging(value);
                this.SendPropertyChanging();
                this._CategoryName = value;
                this.SendPropertyChanged("CategoryName");
                this.OnCategoryNameChanged();
		    }
		}
		

        #endregion

        #region Foreign Keys
        public IQueryable<Event> Events
        {
            get
            {
                  var db=new Epilogger.Data.EpiloggerDB();
                  return from items in db.Events
                       where items.CategoryID == _ID
                       select items;
            }
        }

        #endregion


        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
        public event PropertyChangingEventHandler PropertyChanging;
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void SendPropertyChanging()
        {
            var handler = PropertyChanging;
            if (handler != null)
               handler(this, emptyChangingEventArgs);
        }

        protected virtual void SendPropertyChanged(String propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

	}
	
    
    
    /// <summary>
    /// A class which represents the aspnet_WebEvent_Events table in the Epilogger Database.
    /// This class is queryable through EpiloggerDB.aspnet_WebEvent_Event 
    /// </summary>

	public partial class aspnet_WebEvent_Event: INotifyPropertyChanging, INotifyPropertyChanged
	{
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
	    
	    public aspnet_WebEvent_Event(){
	        OnCreated();
	    }
	    
	    #region Properties
	    
        partial void OnEventIdChanging(string value);
        partial void OnEventIdChanged();
		
		private string _EventId;
		public string EventId { 
		    get{
		        return _EventId;
		    } 
		    set{
		        this.OnEventIdChanging(value);
                this.SendPropertyChanging();
                this._EventId = value;
                this.SendPropertyChanged("EventId");
                this.OnEventIdChanged();
		    }
		}
		
        partial void OnEventTimeUtcChanging(DateTime value);
        partial void OnEventTimeUtcChanged();
		
		private DateTime _EventTimeUtc;
		public DateTime EventTimeUtc { 
		    get{
		        return _EventTimeUtc;
		    } 
		    set{
		        this.OnEventTimeUtcChanging(value);
                this.SendPropertyChanging();
                this._EventTimeUtc = value;
                this.SendPropertyChanged("EventTimeUtc");
                this.OnEventTimeUtcChanged();
		    }
		}
		
        partial void OnEventTimeChanging(DateTime value);
        partial void OnEventTimeChanged();
		
		private DateTime _EventTime;
		public DateTime EventTime { 
		    get{
		        return _EventTime;
		    } 
		    set{
		        this.OnEventTimeChanging(value);
                this.SendPropertyChanging();
                this._EventTime = value;
                this.SendPropertyChanged("EventTime");
                this.OnEventTimeChanged();
		    }
		}
		
        partial void OnEventTypeChanging(string value);
        partial void OnEventTypeChanged();
		
		private string _EventType;
		public string EventType { 
		    get{
		        return _EventType;
		    } 
		    set{
		        this.OnEventTypeChanging(value);
                this.SendPropertyChanging();
                this._EventType = value;
                this.SendPropertyChanged("EventType");
                this.OnEventTypeChanged();
		    }
		}
		
        partial void OnEventSequenceChanging(decimal value);
        partial void OnEventSequenceChanged();
		
		private decimal _EventSequence;
		public decimal EventSequence { 
		    get{
		        return _EventSequence;
		    } 
		    set{
		        this.OnEventSequenceChanging(value);
                this.SendPropertyChanging();
                this._EventSequence = value;
                this.SendPropertyChanged("EventSequence");
                this.OnEventSequenceChanged();
		    }
		}
		
        partial void OnEventOccurrenceChanging(decimal value);
        partial void OnEventOccurrenceChanged();
		
		private decimal _EventOccurrence;
		public decimal EventOccurrence { 
		    get{
		        return _EventOccurrence;
		    } 
		    set{
		        this.OnEventOccurrenceChanging(value);
                this.SendPropertyChanging();
                this._EventOccurrence = value;
                this.SendPropertyChanged("EventOccurrence");
                this.OnEventOccurrenceChanged();
		    }
		}
		
        partial void OnEventCodeChanging(int value);
        partial void OnEventCodeChanged();
		
		private int _EventCode;
		public int EventCode { 
		    get{
		        return _EventCode;
		    } 
		    set{
		        this.OnEventCodeChanging(value);
                this.SendPropertyChanging();
                this._EventCode = value;
                this.SendPropertyChanged("EventCode");
                this.OnEventCodeChanged();
		    }
		}
		
        partial void OnEventDetailCodeChanging(int value);
        partial void OnEventDetailCodeChanged();
		
		private int _EventDetailCode;
		public int EventDetailCode { 
		    get{
		        return _EventDetailCode;
		    } 
		    set{
		        this.OnEventDetailCodeChanging(value);
                this.SendPropertyChanging();
                this._EventDetailCode = value;
                this.SendPropertyChanged("EventDetailCode");
                this.OnEventDetailCodeChanged();
		    }
		}
		
        partial void OnMessageChanging(string value);
        partial void OnMessageChanged();
		
		private string _Message;
		public string Message { 
		    get{
		        return _Message;
		    } 
		    set{
		        this.OnMessageChanging(value);
                this.SendPropertyChanging();
                this._Message = value;
                this.SendPropertyChanged("Message");
                this.OnMessageChanged();
		    }
		}
		
        partial void OnApplicationPathChanging(string value);
        partial void OnApplicationPathChanged();
		
		private string _ApplicationPath;
		public string ApplicationPath { 
		    get{
		        return _ApplicationPath;
		    } 
		    set{
		        this.OnApplicationPathChanging(value);
                this.SendPropertyChanging();
                this._ApplicationPath = value;
                this.SendPropertyChanged("ApplicationPath");
                this.OnApplicationPathChanged();
		    }
		}
		
        partial void OnApplicationVirtualPathChanging(string value);
        partial void OnApplicationVirtualPathChanged();
		
		private string _ApplicationVirtualPath;
		public string ApplicationVirtualPath { 
		    get{
		        return _ApplicationVirtualPath;
		    } 
		    set{
		        this.OnApplicationVirtualPathChanging(value);
                this.SendPropertyChanging();
                this._ApplicationVirtualPath = value;
                this.SendPropertyChanged("ApplicationVirtualPath");
                this.OnApplicationVirtualPathChanged();
		    }
		}
		
        partial void OnMachineNameChanging(string value);
        partial void OnMachineNameChanged();
		
		private string _MachineName;
		public string MachineName { 
		    get{
		        return _MachineName;
		    } 
		    set{
		        this.OnMachineNameChanging(value);
                this.SendPropertyChanging();
                this._MachineName = value;
                this.SendPropertyChanged("MachineName");
                this.OnMachineNameChanged();
		    }
		}
		
        partial void OnRequestUrlChanging(string value);
        partial void OnRequestUrlChanged();
		
		private string _RequestUrl;
		public string RequestUrl { 
		    get{
		        return _RequestUrl;
		    } 
		    set{
		        this.OnRequestUrlChanging(value);
                this.SendPropertyChanging();
                this._RequestUrl = value;
                this.SendPropertyChanged("RequestUrl");
                this.OnRequestUrlChanged();
		    }
		}
		
        partial void OnExceptionTypeChanging(string value);
        partial void OnExceptionTypeChanged();
		
		private string _ExceptionType;
		public string ExceptionType { 
		    get{
		        return _ExceptionType;
		    } 
		    set{
		        this.OnExceptionTypeChanging(value);
                this.SendPropertyChanging();
                this._ExceptionType = value;
                this.SendPropertyChanged("ExceptionType");
                this.OnExceptionTypeChanged();
		    }
		}
		
        partial void OnDetailsChanging(string value);
        partial void OnDetailsChanged();
		
		private string _Details;
		public string Details { 
		    get{
		        return _Details;
		    } 
		    set{
		        this.OnDetailsChanging(value);
                this.SendPropertyChanging();
                this._Details = value;
                this.SendPropertyChanged("Details");
                this.OnDetailsChanged();
		    }
		}
		

        #endregion

        #region Foreign Keys
        #endregion


        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
        public event PropertyChangingEventHandler PropertyChanging;
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void SendPropertyChanging()
        {
            var handler = PropertyChanging;
            if (handler != null)
               handler(this, emptyChangingEventArgs);
        }

        protected virtual void SendPropertyChanged(String propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

	}
	
    
    
    /// <summary>
    /// A class which represents the sysdiagrams table in the Epilogger Database.
    /// This class is queryable through EpiloggerDB.sysdiagram 
    /// </summary>

	public partial class sysdiagram: INotifyPropertyChanging, INotifyPropertyChanged
	{
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
	    
	    public sysdiagram(){
	        OnCreated();
	    }
	    
	    #region Properties
	    
        partial void OnnameChanging(string value);
        partial void OnnameChanged();
		
		private string _name;
		public string name { 
		    get{
		        return _name;
		    } 
		    set{
		        this.OnnameChanging(value);
                this.SendPropertyChanging();
                this._name = value;
                this.SendPropertyChanged("name");
                this.OnnameChanged();
		    }
		}
		
        partial void Onprincipal_idChanging(int value);
        partial void Onprincipal_idChanged();
		
		private int _principal_id;
		public int principal_id { 
		    get{
		        return _principal_id;
		    } 
		    set{
		        this.Onprincipal_idChanging(value);
                this.SendPropertyChanging();
                this._principal_id = value;
                this.SendPropertyChanged("principal_id");
                this.Onprincipal_idChanged();
		    }
		}
		
        partial void Ondiagram_idChanging(int value);
        partial void Ondiagram_idChanged();
		
		private int _diagram_id;
		public int diagram_id { 
		    get{
		        return _diagram_id;
		    } 
		    set{
		        this.Ondiagram_idChanging(value);
                this.SendPropertyChanging();
                this._diagram_id = value;
                this.SendPropertyChanged("diagram_id");
                this.Ondiagram_idChanged();
		    }
		}
		
        partial void OnversionChanging(int? value);
        partial void OnversionChanged();
		
		private int? _version;
		public int? version { 
		    get{
		        return _version;
		    } 
		    set{
		        this.OnversionChanging(value);
                this.SendPropertyChanging();
                this._version = value;
                this.SendPropertyChanged("version");
                this.OnversionChanged();
		    }
		}
		
        partial void OndefinitionChanging(byte[] value);
        partial void OndefinitionChanged();
		
		private byte[] _definition;
		public byte[] definition { 
		    get{
		        return _definition;
		    } 
		    set{
		        this.OndefinitionChanging(value);
                this.SendPropertyChanging();
                this._definition = value;
                this.SendPropertyChanged("definition");
                this.OndefinitionChanged();
		    }
		}
		

        #endregion

        #region Foreign Keys
        #endregion


        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
        public event PropertyChangingEventHandler PropertyChanging;
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void SendPropertyChanging()
        {
            var handler = PropertyChanging;
            if (handler != null)
               handler(this, emptyChangingEventArgs);
        }

        protected virtual void SendPropertyChanged(String propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

	}
	
    
    
    /// <summary>
    /// A class which represents the aspnet_Applications table in the Epilogger Database.
    /// This class is queryable through EpiloggerDB.aspnet_Application 
    /// </summary>

	public partial class aspnet_Application: INotifyPropertyChanging, INotifyPropertyChanged
	{
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
	    
	    public aspnet_Application(){
	        OnCreated();
	    }
	    
	    #region Properties
	    
        partial void OnApplicationNameChanging(string value);
        partial void OnApplicationNameChanged();
		
		private string _ApplicationName;
		public string ApplicationName { 
		    get{
		        return _ApplicationName;
		    } 
		    set{
		        this.OnApplicationNameChanging(value);
                this.SendPropertyChanging();
                this._ApplicationName = value;
                this.SendPropertyChanged("ApplicationName");
                this.OnApplicationNameChanged();
		    }
		}
		
        partial void OnLoweredApplicationNameChanging(string value);
        partial void OnLoweredApplicationNameChanged();
		
		private string _LoweredApplicationName;
		public string LoweredApplicationName { 
		    get{
		        return _LoweredApplicationName;
		    } 
		    set{
		        this.OnLoweredApplicationNameChanging(value);
                this.SendPropertyChanging();
                this._LoweredApplicationName = value;
                this.SendPropertyChanged("LoweredApplicationName");
                this.OnLoweredApplicationNameChanged();
		    }
		}
		
        partial void OnApplicationIdChanging(Guid value);
        partial void OnApplicationIdChanged();
		
		private Guid _ApplicationId;
		public Guid ApplicationId { 
		    get{
		        return _ApplicationId;
		    } 
		    set{
		        this.OnApplicationIdChanging(value);
                this.SendPropertyChanging();
                this._ApplicationId = value;
                this.SendPropertyChanged("ApplicationId");
                this.OnApplicationIdChanged();
		    }
		}
		
        partial void OnDescriptionChanging(string value);
        partial void OnDescriptionChanged();
		
		private string _Description;
		public string Description { 
		    get{
		        return _Description;
		    } 
		    set{
		        this.OnDescriptionChanging(value);
                this.SendPropertyChanging();
                this._Description = value;
                this.SendPropertyChanged("Description");
                this.OnDescriptionChanged();
		    }
		}
		

        #endregion

        #region Foreign Keys
        public IQueryable<aspnet_Membership> aspnet_Memberships
        {
            get
            {
                  var db=new Epilogger.Data.EpiloggerDB();
                  return from items in db.aspnet_Memberships
                       where items.ApplicationId == _ApplicationId
                       select items;
            }
        }

        public IQueryable<aspnet_Path> aspnet_Paths
        {
            get
            {
                  var db=new Epilogger.Data.EpiloggerDB();
                  return from items in db.aspnet_Paths
                       where items.ApplicationId == _ApplicationId
                       select items;
            }
        }

        public IQueryable<aspnet_Role> aspnet_Roles
        {
            get
            {
                  var db=new Epilogger.Data.EpiloggerDB();
                  return from items in db.aspnet_Roles
                       where items.ApplicationId == _ApplicationId
                       select items;
            }
        }

        public IQueryable<aspnet_User> aspnet_Users
        {
            get
            {
                  var db=new Epilogger.Data.EpiloggerDB();
                  return from items in db.aspnet_Users
                       where items.ApplicationId == _ApplicationId
                       select items;
            }
        }

        #endregion


        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
        public event PropertyChangingEventHandler PropertyChanging;
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void SendPropertyChanging()
        {
            var handler = PropertyChanging;
            if (handler != null)
               handler(this, emptyChangingEventArgs);
        }

        protected virtual void SendPropertyChanged(String propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

	}
	
    
    
    /// <summary>
    /// A class which represents the BlogPosts table in the Epilogger Database.
    /// This class is queryable through EpiloggerDB.BlogPost 
    /// </summary>

	public partial class BlogPost: INotifyPropertyChanging, INotifyPropertyChanged
	{
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
	    
	    public BlogPost(){
	        OnCreated();
	    }
	    
	    #region Properties
	    
        partial void OnIDChanging(int value);
        partial void OnIDChanged();
		
		private int _ID;
		public int ID { 
		    get{
		        return _ID;
		    } 
		    set{
		        this.OnIDChanging(value);
                this.SendPropertyChanging();
                this._ID = value;
                this.SendPropertyChanged("ID");
                this.OnIDChanged();
		    }
		}
		
        partial void OnUserIDChanging(Guid value);
        partial void OnUserIDChanged();
		
		private Guid _UserID;
		public Guid UserID { 
		    get{
		        return _UserID;
		    } 
		    set{
		        this.OnUserIDChanging(value);
                this.SendPropertyChanging();
                this._UserID = value;
                this.SendPropertyChanged("UserID");
                this.OnUserIDChanged();
		    }
		}
		
        partial void OnEventIDChanging(int value);
        partial void OnEventIDChanged();
		
		private int _EventID;
		public int EventID { 
		    get{
		        return _EventID;
		    } 
		    set{
		        this.OnEventIDChanging(value);
                this.SendPropertyChanging();
                this._EventID = value;
                this.SendPropertyChanged("EventID");
                this.OnEventIDChanged();
		    }
		}
		
        partial void OnBlogTitleChanging(string value);
        partial void OnBlogTitleChanged();
		
		private string _BlogTitle;
		public string BlogTitle { 
		    get{
		        return _BlogTitle;
		    } 
		    set{
		        this.OnBlogTitleChanging(value);
                this.SendPropertyChanging();
                this._BlogTitle = value;
                this.SendPropertyChanged("BlogTitle");
                this.OnBlogTitleChanged();
		    }
		}
		
        partial void OnBlogURLChanging(string value);
        partial void OnBlogURLChanged();
		
		private string _BlogURL;
		public string BlogURL { 
		    get{
		        return _BlogURL;
		    } 
		    set{
		        this.OnBlogURLChanging(value);
                this.SendPropertyChanging();
                this._BlogURL = value;
                this.SendPropertyChanged("BlogURL");
                this.OnBlogURLChanged();
		    }
		}
		

        #endregion

        #region Foreign Keys
        public IQueryable<aspnet_User> aspnet_Users
        {
            get
            {
                  var db=new Epilogger.Data.EpiloggerDB();
                  return from items in db.aspnet_Users
                       where items.UserId == _UserID
                       select items;
            }
        }

        public IQueryable<Event> Events
        {
            get
            {
                  var db=new Epilogger.Data.EpiloggerDB();
                  return from items in db.Events
                       where items.ID == _EventID
                       select items;
            }
        }

        #endregion


        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
        public event PropertyChangingEventHandler PropertyChanging;
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void SendPropertyChanging()
        {
            var handler = PropertyChanging;
            if (handler != null)
               handler(this, emptyChangingEventArgs);
        }

        protected virtual void SendPropertyChanged(String propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

	}
	
    
    
    /// <summary>
    /// A class which represents the aspnet_Users table in the Epilogger Database.
    /// This class is queryable through EpiloggerDB.aspnet_User 
    /// </summary>

	public partial class aspnet_User: INotifyPropertyChanging, INotifyPropertyChanged
	{
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
	    
	    public aspnet_User(){
	        OnCreated();
	    }
	    
	    #region Properties
	    
        partial void OnApplicationIdChanging(Guid value);
        partial void OnApplicationIdChanged();
		
		private Guid _ApplicationId;
		public Guid ApplicationId { 
		    get{
		        return _ApplicationId;
		    } 
		    set{
		        this.OnApplicationIdChanging(value);
                this.SendPropertyChanging();
                this._ApplicationId = value;
                this.SendPropertyChanged("ApplicationId");
                this.OnApplicationIdChanged();
		    }
		}
		
        partial void OnUserIdChanging(Guid value);
        partial void OnUserIdChanged();
		
		private Guid _UserId;
		public Guid UserId { 
		    get{
		        return _UserId;
		    } 
		    set{
		        this.OnUserIdChanging(value);
                this.SendPropertyChanging();
                this._UserId = value;
                this.SendPropertyChanged("UserId");
                this.OnUserIdChanged();
		    }
		}
		
        partial void OnUserNameChanging(string value);
        partial void OnUserNameChanged();
		
		private string _UserName;
		public string UserName { 
		    get{
		        return _UserName;
		    } 
		    set{
		        this.OnUserNameChanging(value);
                this.SendPropertyChanging();
                this._UserName = value;
                this.SendPropertyChanged("UserName");
                this.OnUserNameChanged();
		    }
		}
		
        partial void OnLoweredUserNameChanging(string value);
        partial void OnLoweredUserNameChanged();
		
		private string _LoweredUserName;
		public string LoweredUserName { 
		    get{
		        return _LoweredUserName;
		    } 
		    set{
		        this.OnLoweredUserNameChanging(value);
                this.SendPropertyChanging();
                this._LoweredUserName = value;
                this.SendPropertyChanged("LoweredUserName");
                this.OnLoweredUserNameChanged();
		    }
		}
		
        partial void OnMobileAliasChanging(string value);
        partial void OnMobileAliasChanged();
		
		private string _MobileAlias;
		public string MobileAlias { 
		    get{
		        return _MobileAlias;
		    } 
		    set{
		        this.OnMobileAliasChanging(value);
                this.SendPropertyChanging();
                this._MobileAlias = value;
                this.SendPropertyChanged("MobileAlias");
                this.OnMobileAliasChanged();
		    }
		}
		
        partial void OnIsAnonymousChanging(bool value);
        partial void OnIsAnonymousChanged();
		
		private bool _IsAnonymous;
		public bool IsAnonymous { 
		    get{
		        return _IsAnonymous;
		    } 
		    set{
		        this.OnIsAnonymousChanging(value);
                this.SendPropertyChanging();
                this._IsAnonymous = value;
                this.SendPropertyChanged("IsAnonymous");
                this.OnIsAnonymousChanged();
		    }
		}
		
        partial void OnLastActivityDateChanging(DateTime value);
        partial void OnLastActivityDateChanged();
		
		private DateTime _LastActivityDate;
		public DateTime LastActivityDate { 
		    get{
		        return _LastActivityDate;
		    } 
		    set{
		        this.OnLastActivityDateChanging(value);
                this.SendPropertyChanging();
                this._LastActivityDate = value;
                this.SendPropertyChanged("LastActivityDate");
                this.OnLastActivityDateChanged();
		    }
		}
		

        #endregion

        #region Foreign Keys
        public IQueryable<aspnet_Application> aspnet_Applications
        {
            get
            {
                  var db=new Epilogger.Data.EpiloggerDB();
                  return from items in db.aspnet_Applications
                       where items.ApplicationId == _ApplicationId
                       select items;
            }
        }

        public IQueryable<aspnet_Membership> aspnet_Memberships
        {
            get
            {
                  var db=new Epilogger.Data.EpiloggerDB();
                  return from items in db.aspnet_Memberships
                       where items.UserId == _UserId
                       select items;
            }
        }

        public IQueryable<aspnet_PersonalizationPerUser> aspnet_PersonalizationPerUsers
        {
            get
            {
                  var db=new Epilogger.Data.EpiloggerDB();
                  return from items in db.aspnet_PersonalizationPerUsers
                       where items.UserId == _UserId
                       select items;
            }
        }

        public IQueryable<aspnet_Profile> aspnet_Profiles
        {
            get
            {
                  var db=new Epilogger.Data.EpiloggerDB();
                  return from items in db.aspnet_Profiles
                       where items.UserId == _UserId
                       select items;
            }
        }

        public IQueryable<aspnet_UsersInRole> aspnet_UsersInRoles
        {
            get
            {
                  var db=new Epilogger.Data.EpiloggerDB();
                  return from items in db.aspnet_UsersInRoles
                       where items.UserId == _UserId
                       select items;
            }
        }

        public IQueryable<BlogPost> BlogPosts
        {
            get
            {
                  var db=new Epilogger.Data.EpiloggerDB();
                  return from items in db.BlogPosts
                       where items.UserID == _UserId
                       select items;
            }
        }

        public IQueryable<Event> Events
        {
            get
            {
                  var db=new Epilogger.Data.EpiloggerDB();
                  return from items in db.Events
                       where items.UserID == _UserId
                       select items;
            }
        }

        #endregion


        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
        public event PropertyChangingEventHandler PropertyChanging;
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void SendPropertyChanging()
        {
            var handler = PropertyChanging;
            if (handler != null)
               handler(this, emptyChangingEventArgs);
        }

        protected virtual void SendPropertyChanged(String propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

	}
	
    
    
    /// <summary>
    /// A class which represents the Events table in the Epilogger Database.
    /// This class is queryable through EpiloggerDB.Event 
    /// </summary>

	public partial class Event: INotifyPropertyChanging, INotifyPropertyChanged
	{
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
	    
	    public Event(){
	        OnCreated();
	    }
	    
	    #region Properties
	    
        partial void OnIDChanging(int value);
        partial void OnIDChanged();
		
		private int _ID;
		public int ID { 
		    get{
		        return _ID;
		    } 
		    set{
		        this.OnIDChanging(value);
                this.SendPropertyChanging();
                this._ID = value;
                this.SendPropertyChanged("ID");
                this.OnIDChanged();
		    }
		}
		
        partial void OnUserIDChanging(Guid? value);
        partial void OnUserIDChanged();
		
		private Guid? _UserID;
		public Guid? UserID { 
		    get{
		        return _UserID;
		    } 
		    set{
		        this.OnUserIDChanging(value);
                this.SendPropertyChanging();
                this._UserID = value;
                this.SendPropertyChanged("UserID");
                this.OnUserIDChanged();
		    }
		}
		
        partial void OnNameChanging(string value);
        partial void OnNameChanged();
		
		private string _Name;
		public string Name { 
		    get{
		        return _Name;
		    } 
		    set{
		        this.OnNameChanging(value);
                this.SendPropertyChanging();
                this._Name = value;
                this.SendPropertyChanged("Name");
                this.OnNameChanged();
		    }
		}
		
        partial void OnSubTitleChanging(string value);
        partial void OnSubTitleChanged();
		
		private string _SubTitle;
		public string SubTitle { 
		    get{
		        return _SubTitle;
		    } 
		    set{
		        this.OnSubTitleChanging(value);
                this.SendPropertyChanging();
                this._SubTitle = value;
                this.SendPropertyChanged("SubTitle");
                this.OnSubTitleChanged();
		    }
		}
		
        partial void OnDescriptionChanging(string value);
        partial void OnDescriptionChanged();
		
		private string _Description;
		public string Description { 
		    get{
		        return _Description;
		    } 
		    set{
		        this.OnDescriptionChanging(value);
                this.SendPropertyChanging();
                this._Description = value;
                this.SendPropertyChanged("Description");
                this.OnDescriptionChanged();
		    }
		}
		
        partial void OnCategoryIDChanging(int value);
        partial void OnCategoryIDChanged();
		
		private int _CategoryID;
		public int CategoryID { 
		    get{
		        return _CategoryID;
		    } 
		    set{
		        this.OnCategoryIDChanging(value);
                this.SendPropertyChanging();
                this._CategoryID = value;
                this.SendPropertyChanged("CategoryID");
                this.OnCategoryIDChanged();
		    }
		}
		
        partial void OnWebsiteURLChanging(string value);
        partial void OnWebsiteURLChanged();
		
		private string _WebsiteURL;
		public string WebsiteURL { 
		    get{
		        return _WebsiteURL;
		    } 
		    set{
		        this.OnWebsiteURLChanging(value);
                this.SendPropertyChanging();
                this._WebsiteURL = value;
                this.SendPropertyChanged("WebsiteURL");
                this.OnWebsiteURLChanged();
		    }
		}
		
        partial void OnCostChanging(string value);
        partial void OnCostChanged();
		
		private string _Cost;
		public string Cost { 
		    get{
		        return _Cost;
		    } 
		    set{
		        this.OnCostChanging(value);
                this.SendPropertyChanging();
                this._Cost = value;
                this.SendPropertyChanged("Cost");
                this.OnCostChanged();
		    }
		}
		
        partial void OnStartDateTimeChanging(DateTime? value);
        partial void OnStartDateTimeChanged();
		
		private DateTime? _StartDateTime;
		public DateTime? StartDateTime { 
		    get{
		        return _StartDateTime;
		    } 
		    set{
		        this.OnStartDateTimeChanging(value);
                this.SendPropertyChanging();
                this._StartDateTime = value;
                this.SendPropertyChanged("StartDateTime");
                this.OnStartDateTimeChanged();
		    }
		}
		
        partial void OnEndDateTimeChanging(DateTime? value);
        partial void OnEndDateTimeChanged();
		
		private DateTime? _EndDateTime;
		public DateTime? EndDateTime { 
		    get{
		        return _EndDateTime;
		    } 
		    set{
		        this.OnEndDateTimeChanging(value);
                this.SendPropertyChanging();
                this._EndDateTime = value;
                this.SendPropertyChanged("EndDateTime");
                this.OnEndDateTimeChanged();
		    }
		}
		
        partial void OnTimeZoneOffsetChanging(int? value);
        partial void OnTimeZoneOffsetChanged();
		
		private int? _TimeZoneOffset;
		public int? TimeZoneOffset { 
		    get{
		        return _TimeZoneOffset;
		    } 
		    set{
		        this.OnTimeZoneOffsetChanging(value);
                this.SendPropertyChanging();
                this._TimeZoneOffset = value;
                this.SendPropertyChanged("TimeZoneOffset");
                this.OnTimeZoneOffsetChanged();
		    }
		}
		
        partial void OnVenueIDChanging(long? value);
        partial void OnVenueIDChanged();
		
		private long? _VenueID;
		public long? VenueID { 
		    get{
		        return _VenueID;
		    } 
		    set{
		        this.OnVenueIDChanging(value);
                this.SendPropertyChanging();
                this._VenueID = value;
                this.SendPropertyChanged("VenueID");
                this.OnVenueIDChanged();
		    }
		}
		
        partial void OnSearchTermsChanging(string value);
        partial void OnSearchTermsChanged();
		
		private string _SearchTerms;
		public string SearchTerms { 
		    get{
		        return _SearchTerms;
		    } 
		    set{
		        this.OnSearchTermsChanging(value);
                this.SendPropertyChanging();
                this._SearchTerms = value;
                this.SendPropertyChanged("SearchTerms");
                this.OnSearchTermsChanged();
		    }
		}
		
        partial void OnNumberOfTweetsChanging(long value);
        partial void OnNumberOfTweetsChanged();
		
		private long _NumberOfTweets;
		public long NumberOfTweets { 
		    get{
		        return _NumberOfTweets;
		    } 
		    set{
		        this.OnNumberOfTweetsChanging(value);
                this.SendPropertyChanging();
                this._NumberOfTweets = value;
                this.SendPropertyChanged("NumberOfTweets");
                this.OnNumberOfTweetsChanged();
		    }
		}
		
        partial void OnCollectionModeChanging(int value);
        partial void OnCollectionModeChanged();
		
		private int _CollectionMode;
		public int CollectionMode { 
		    get{
		        return _CollectionMode;
		    } 
		    set{
		        this.OnCollectionModeChanging(value);
                this.SendPropertyChanging();
                this._CollectionMode = value;
                this.SendPropertyChanged("CollectionMode");
                this.OnCollectionModeChanged();
		    }
		}
		
        partial void OnIsActiveChanging(bool value);
        partial void OnIsActiveChanged();
		
		private bool _IsActive;
		public bool IsActive { 
		    get{
		        return _IsActive;
		    } 
		    set{
		        this.OnIsActiveChanging(value);
                this.SendPropertyChanging();
                this._IsActive = value;
                this.SendPropertyChanged("IsActive");
                this.OnIsActiveChanged();
		    }
		}
		

        #endregion

        #region Foreign Keys
        public IQueryable<aspnet_User> aspnet_Users
        {
            get
            {
                  var db=new Epilogger.Data.EpiloggerDB();
                  return from items in db.aspnet_Users
                       where items.UserId == _UserID
                       select items;
            }
        }

        public IQueryable<EventCategory> EventCategories
        {
            get
            {
                  var db=new Epilogger.Data.EpiloggerDB();
                  return from items in db.EventCategories
                       where items.ID == _CategoryID
                       select items;
            }
        }

        public IQueryable<BlogPost> BlogPosts
        {
            get
            {
                  var db=new Epilogger.Data.EpiloggerDB();
                  return from items in db.BlogPosts
                       where items.EventID == _ID
                       select items;
            }
        }

        public IQueryable<ImageMetaDatum> ImageMetaData
        {
            get
            {
                  var db=new Epilogger.Data.EpiloggerDB();
                  return from items in db.ImageMetaData
                       where items.EventID == _ID
                       select items;
            }
        }

        public IQueryable<Image> Images
        {
            get
            {
                  var db=new Epilogger.Data.EpiloggerDB();
                  return from items in db.Images
                       where items.EventID == _ID
                       select items;
            }
        }

        public IQueryable<Picture> Pictures
        {
            get
            {
                  var db=new Epilogger.Data.EpiloggerDB();
                  return from items in db.Pictures
                       where items.EventID == _ID
                       select items;
            }
        }

        public IQueryable<Tweet> Tweets
        {
            get
            {
                  var db=new Epilogger.Data.EpiloggerDB();
                  return from items in db.Tweets
                       where items.EventID == _ID
                       select items;
            }
        }

        public IQueryable<URL> URLS
        {
            get
            {
                  var db=new Epilogger.Data.EpiloggerDB();
                  return from items in db.URLS
                       where items.EventID == _ID
                       select items;
            }
        }

        public IQueryable<Venue> Venues
        {
            get
            {
                  var db=new Epilogger.Data.EpiloggerDB();
                  return from items in db.Venues
                       where items.VenueID == _VenueID
                       select items;
            }
        }

        #endregion


        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
        public event PropertyChangingEventHandler PropertyChanging;
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void SendPropertyChanging()
        {
            var handler = PropertyChanging;
            if (handler != null)
               handler(this, emptyChangingEventArgs);
        }

        protected virtual void SendPropertyChanged(String propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

	}
	
    
    
    /// <summary>
    /// A class which represents the aspnet_SchemaVersions table in the Epilogger Database.
    /// This class is queryable through EpiloggerDB.aspnet_SchemaVersion 
    /// </summary>

	public partial class aspnet_SchemaVersion: INotifyPropertyChanging, INotifyPropertyChanged
	{
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
	    
	    public aspnet_SchemaVersion(){
	        OnCreated();
	    }
	    
	    #region Properties
	    
        partial void OnFeatureChanging(string value);
        partial void OnFeatureChanged();
		
		private string _Feature;
		public string Feature { 
		    get{
		        return _Feature;
		    } 
		    set{
		        this.OnFeatureChanging(value);
                this.SendPropertyChanging();
                this._Feature = value;
                this.SendPropertyChanged("Feature");
                this.OnFeatureChanged();
		    }
		}
		
        partial void OnCompatibleSchemaVersionChanging(string value);
        partial void OnCompatibleSchemaVersionChanged();
		
		private string _CompatibleSchemaVersion;
		public string CompatibleSchemaVersion { 
		    get{
		        return _CompatibleSchemaVersion;
		    } 
		    set{
		        this.OnCompatibleSchemaVersionChanging(value);
                this.SendPropertyChanging();
                this._CompatibleSchemaVersion = value;
                this.SendPropertyChanged("CompatibleSchemaVersion");
                this.OnCompatibleSchemaVersionChanged();
		    }
		}
		
        partial void OnIsCurrentVersionChanging(bool value);
        partial void OnIsCurrentVersionChanged();
		
		private bool _IsCurrentVersion;
		public bool IsCurrentVersion { 
		    get{
		        return _IsCurrentVersion;
		    } 
		    set{
		        this.OnIsCurrentVersionChanging(value);
                this.SendPropertyChanging();
                this._IsCurrentVersion = value;
                this.SendPropertyChanged("IsCurrentVersion");
                this.OnIsCurrentVersionChanged();
		    }
		}
		

        #endregion

        #region Foreign Keys
        #endregion


        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
        public event PropertyChangingEventHandler PropertyChanging;
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void SendPropertyChanging()
        {
            var handler = PropertyChanging;
            if (handler != null)
               handler(this, emptyChangingEventArgs);
        }

        protected virtual void SendPropertyChanged(String propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

	}
	
    
    
    /// <summary>
    /// A class which represents the Pictures table in the Epilogger Database.
    /// This class is queryable through EpiloggerDB.Picture 
    /// </summary>

	public partial class Picture: INotifyPropertyChanging, INotifyPropertyChanged
	{
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
	    
	    public Picture(){
	        OnCreated();
	    }
	    
	    #region Properties
	    
        partial void OnIDChanging(int value);
        partial void OnIDChanged();
		
		private int _ID;
		public int ID { 
		    get{
		        return _ID;
		    } 
		    set{
		        this.OnIDChanging(value);
                this.SendPropertyChanging();
                this._ID = value;
                this.SendPropertyChanged("ID");
                this.OnIDChanged();
		    }
		}
		
        partial void OnEventIDChanging(int value);
        partial void OnEventIDChanged();
		
		private int _EventID;
		public int EventID { 
		    get{
		        return _EventID;
		    } 
		    set{
		        this.OnEventIDChanging(value);
                this.SendPropertyChanging();
                this._EventID = value;
                this.SendPropertyChanged("EventID");
                this.OnEventIDChanged();
		    }
		}
		
        partial void OnImageSourceChanging(string value);
        partial void OnImageSourceChanged();
		
		private string _ImageSource;
		public string ImageSource { 
		    get{
		        return _ImageSource;
		    } 
		    set{
		        this.OnImageSourceChanging(value);
                this.SendPropertyChanging();
                this._ImageSource = value;
                this.SendPropertyChanged("ImageSource");
                this.OnImageSourceChanged();
		    }
		}
		
        partial void OnTweetIDChanging(long value);
        partial void OnTweetIDChanged();
		
		private long _TweetID;
		public long TweetID { 
		    get{
		        return _TweetID;
		    } 
		    set{
		        this.OnTweetIDChanging(value);
                this.SendPropertyChanging();
                this._TweetID = value;
                this.SendPropertyChanged("TweetID");
                this.OnTweetIDChanged();
		    }
		}
		
        partial void OnImageURLChanging(string value);
        partial void OnImageURLChanged();
		
		private string _ImageURL;
		public string ImageURL { 
		    get{
		        return _ImageURL;
		    } 
		    set{
		        this.OnImageURLChanging(value);
                this.SendPropertyChanging();
                this._ImageURL = value;
                this.SendPropertyChanged("ImageURL");
                this.OnImageURLChanged();
		    }
		}
		

        #endregion

        #region Foreign Keys
        public IQueryable<Event> Events
        {
            get
            {
                  var db=new Epilogger.Data.EpiloggerDB();
                  return from items in db.Events
                       where items.ID == _EventID
                       select items;
            }
        }

        public IQueryable<Tweet> Tweets
        {
            get
            {
                  var db=new Epilogger.Data.EpiloggerDB();
                  return from items in db.Tweets
                       where items.ID == _TweetID
                       select items;
            }
        }

        #endregion


        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
        public event PropertyChangingEventHandler PropertyChanging;
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void SendPropertyChanging()
        {
            var handler = PropertyChanging;
            if (handler != null)
               handler(this, emptyChangingEventArgs);
        }

        protected virtual void SendPropertyChanged(String propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

	}
	
    
    
    /// <summary>
    /// A class which represents the BetaSignups table in the Epilogger Database.
    /// This class is queryable through EpiloggerDB.BetaSignup 
    /// </summary>

	public partial class BetaSignup: INotifyPropertyChanging, INotifyPropertyChanged
	{
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
	    
	    public BetaSignup(){
	        OnCreated();
	    }
	    
	    #region Properties
	    
        partial void OnIDChanging(int value);
        partial void OnIDChanged();
		
		private int _ID;
		public int ID { 
		    get{
		        return _ID;
		    } 
		    set{
		        this.OnIDChanging(value);
                this.SendPropertyChanging();
                this._ID = value;
                this.SendPropertyChanged("ID");
                this.OnIDChanged();
		    }
		}
		
        partial void OnEmailAddressChanging(string value);
        partial void OnEmailAddressChanged();
		
		private string _EmailAddress;
		public string EmailAddress { 
		    get{
		        return _EmailAddress;
		    } 
		    set{
		        this.OnEmailAddressChanging(value);
                this.SendPropertyChanging();
                this._EmailAddress = value;
                this.SendPropertyChanged("EmailAddress");
                this.OnEmailAddressChanged();
		    }
		}
		
        partial void OnIPAddressChanging(string value);
        partial void OnIPAddressChanged();
		
		private string _IPAddress;
		public string IPAddress { 
		    get{
		        return _IPAddress;
		    } 
		    set{
		        this.OnIPAddressChanging(value);
                this.SendPropertyChanging();
                this._IPAddress = value;
                this.SendPropertyChanged("IPAddress");
                this.OnIPAddressChanged();
		    }
		}
		
        partial void OnDateTimeSubmittedChanging(DateTime? value);
        partial void OnDateTimeSubmittedChanged();
		
		private DateTime? _DateTimeSubmitted;
		public DateTime? DateTimeSubmitted { 
		    get{
		        return _DateTimeSubmitted;
		    } 
		    set{
		        this.OnDateTimeSubmittedChanging(value);
                this.SendPropertyChanging();
                this._DateTimeSubmitted = value;
                this.SendPropertyChanged("DateTimeSubmitted");
                this.OnDateTimeSubmittedChanged();
		    }
		}
		

        #endregion

        #region Foreign Keys
        #endregion


        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
        public event PropertyChangingEventHandler PropertyChanging;
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void SendPropertyChanging()
        {
            var handler = PropertyChanging;
            if (handler != null)
               handler(this, emptyChangingEventArgs);
        }

        protected virtual void SendPropertyChanged(String propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

	}
	
    
    
    /// <summary>
    /// A class which represents the aspnet_Membership table in the Epilogger Database.
    /// This class is queryable through EpiloggerDB.aspnet_Membership 
    /// </summary>

	public partial class aspnet_Membership: INotifyPropertyChanging, INotifyPropertyChanged
	{
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
	    
	    public aspnet_Membership(){
	        OnCreated();
	    }
	    
	    #region Properties
	    
        partial void OnApplicationIdChanging(Guid value);
        partial void OnApplicationIdChanged();
		
		private Guid _ApplicationId;
		public Guid ApplicationId { 
		    get{
		        return _ApplicationId;
		    } 
		    set{
		        this.OnApplicationIdChanging(value);
                this.SendPropertyChanging();
                this._ApplicationId = value;
                this.SendPropertyChanged("ApplicationId");
                this.OnApplicationIdChanged();
		    }
		}
		
        partial void OnUserIdChanging(Guid value);
        partial void OnUserIdChanged();
		
		private Guid _UserId;
		public Guid UserId { 
		    get{
		        return _UserId;
		    } 
		    set{
		        this.OnUserIdChanging(value);
                this.SendPropertyChanging();
                this._UserId = value;
                this.SendPropertyChanged("UserId");
                this.OnUserIdChanged();
		    }
		}
		
        partial void OnPasswordChanging(string value);
        partial void OnPasswordChanged();
		
		private string _Password;
		public string Password { 
		    get{
		        return _Password;
		    } 
		    set{
		        this.OnPasswordChanging(value);
                this.SendPropertyChanging();
                this._Password = value;
                this.SendPropertyChanged("Password");
                this.OnPasswordChanged();
		    }
		}
		
        partial void OnPasswordFormatChanging(int value);
        partial void OnPasswordFormatChanged();
		
		private int _PasswordFormat;
		public int PasswordFormat { 
		    get{
		        return _PasswordFormat;
		    } 
		    set{
		        this.OnPasswordFormatChanging(value);
                this.SendPropertyChanging();
                this._PasswordFormat = value;
                this.SendPropertyChanged("PasswordFormat");
                this.OnPasswordFormatChanged();
		    }
		}
		
        partial void OnPasswordSaltChanging(string value);
        partial void OnPasswordSaltChanged();
		
		private string _PasswordSalt;
		public string PasswordSalt { 
		    get{
		        return _PasswordSalt;
		    } 
		    set{
		        this.OnPasswordSaltChanging(value);
                this.SendPropertyChanging();
                this._PasswordSalt = value;
                this.SendPropertyChanged("PasswordSalt");
                this.OnPasswordSaltChanged();
		    }
		}
		
        partial void OnMobilePINChanging(string value);
        partial void OnMobilePINChanged();
		
		private string _MobilePIN;
		public string MobilePIN { 
		    get{
		        return _MobilePIN;
		    } 
		    set{
		        this.OnMobilePINChanging(value);
                this.SendPropertyChanging();
                this._MobilePIN = value;
                this.SendPropertyChanged("MobilePIN");
                this.OnMobilePINChanged();
		    }
		}
		
        partial void OnEmailChanging(string value);
        partial void OnEmailChanged();
		
		private string _Email;
		public string Email { 
		    get{
		        return _Email;
		    } 
		    set{
		        this.OnEmailChanging(value);
                this.SendPropertyChanging();
                this._Email = value;
                this.SendPropertyChanged("Email");
                this.OnEmailChanged();
		    }
		}
		
        partial void OnLoweredEmailChanging(string value);
        partial void OnLoweredEmailChanged();
		
		private string _LoweredEmail;
		public string LoweredEmail { 
		    get{
		        return _LoweredEmail;
		    } 
		    set{
		        this.OnLoweredEmailChanging(value);
                this.SendPropertyChanging();
                this._LoweredEmail = value;
                this.SendPropertyChanged("LoweredEmail");
                this.OnLoweredEmailChanged();
		    }
		}
		
        partial void OnPasswordQuestionChanging(string value);
        partial void OnPasswordQuestionChanged();
		
		private string _PasswordQuestion;
		public string PasswordQuestion { 
		    get{
		        return _PasswordQuestion;
		    } 
		    set{
		        this.OnPasswordQuestionChanging(value);
                this.SendPropertyChanging();
                this._PasswordQuestion = value;
                this.SendPropertyChanged("PasswordQuestion");
                this.OnPasswordQuestionChanged();
		    }
		}
		
        partial void OnPasswordAnswerChanging(string value);
        partial void OnPasswordAnswerChanged();
		
		private string _PasswordAnswer;
		public string PasswordAnswer { 
		    get{
		        return _PasswordAnswer;
		    } 
		    set{
		        this.OnPasswordAnswerChanging(value);
                this.SendPropertyChanging();
                this._PasswordAnswer = value;
                this.SendPropertyChanged("PasswordAnswer");
                this.OnPasswordAnswerChanged();
		    }
		}
		
        partial void OnIsApprovedChanging(bool value);
        partial void OnIsApprovedChanged();
		
		private bool _IsApproved;
		public bool IsApproved { 
		    get{
		        return _IsApproved;
		    } 
		    set{
		        this.OnIsApprovedChanging(value);
                this.SendPropertyChanging();
                this._IsApproved = value;
                this.SendPropertyChanged("IsApproved");
                this.OnIsApprovedChanged();
		    }
		}
		
        partial void OnIsLockedOutChanging(bool value);
        partial void OnIsLockedOutChanged();
		
		private bool _IsLockedOut;
		public bool IsLockedOut { 
		    get{
		        return _IsLockedOut;
		    } 
		    set{
		        this.OnIsLockedOutChanging(value);
                this.SendPropertyChanging();
                this._IsLockedOut = value;
                this.SendPropertyChanged("IsLockedOut");
                this.OnIsLockedOutChanged();
		    }
		}
		
        partial void OnCreateDateChanging(DateTime value);
        partial void OnCreateDateChanged();
		
		private DateTime _CreateDate;
		public DateTime CreateDate { 
		    get{
		        return _CreateDate;
		    } 
		    set{
		        this.OnCreateDateChanging(value);
                this.SendPropertyChanging();
                this._CreateDate = value;
                this.SendPropertyChanged("CreateDate");
                this.OnCreateDateChanged();
		    }
		}
		
        partial void OnLastLoginDateChanging(DateTime value);
        partial void OnLastLoginDateChanged();
		
		private DateTime _LastLoginDate;
		public DateTime LastLoginDate { 
		    get{
		        return _LastLoginDate;
		    } 
		    set{
		        this.OnLastLoginDateChanging(value);
                this.SendPropertyChanging();
                this._LastLoginDate = value;
                this.SendPropertyChanged("LastLoginDate");
                this.OnLastLoginDateChanged();
		    }
		}
		
        partial void OnLastPasswordChangedDateChanging(DateTime value);
        partial void OnLastPasswordChangedDateChanged();
		
		private DateTime _LastPasswordChangedDate;
		public DateTime LastPasswordChangedDate { 
		    get{
		        return _LastPasswordChangedDate;
		    } 
		    set{
		        this.OnLastPasswordChangedDateChanging(value);
                this.SendPropertyChanging();
                this._LastPasswordChangedDate = value;
                this.SendPropertyChanged("LastPasswordChangedDate");
                this.OnLastPasswordChangedDateChanged();
		    }
		}
		
        partial void OnLastLockoutDateChanging(DateTime value);
        partial void OnLastLockoutDateChanged();
		
		private DateTime _LastLockoutDate;
		public DateTime LastLockoutDate { 
		    get{
		        return _LastLockoutDate;
		    } 
		    set{
		        this.OnLastLockoutDateChanging(value);
                this.SendPropertyChanging();
                this._LastLockoutDate = value;
                this.SendPropertyChanged("LastLockoutDate");
                this.OnLastLockoutDateChanged();
		    }
		}
		
        partial void OnFailedPasswordAttemptCountChanging(int value);
        partial void OnFailedPasswordAttemptCountChanged();
		
		private int _FailedPasswordAttemptCount;
		public int FailedPasswordAttemptCount { 
		    get{
		        return _FailedPasswordAttemptCount;
		    } 
		    set{
		        this.OnFailedPasswordAttemptCountChanging(value);
                this.SendPropertyChanging();
                this._FailedPasswordAttemptCount = value;
                this.SendPropertyChanged("FailedPasswordAttemptCount");
                this.OnFailedPasswordAttemptCountChanged();
		    }
		}
		
        partial void OnFailedPasswordAttemptWindowStartChanging(DateTime value);
        partial void OnFailedPasswordAttemptWindowStartChanged();
		
		private DateTime _FailedPasswordAttemptWindowStart;
		public DateTime FailedPasswordAttemptWindowStart { 
		    get{
		        return _FailedPasswordAttemptWindowStart;
		    } 
		    set{
		        this.OnFailedPasswordAttemptWindowStartChanging(value);
                this.SendPropertyChanging();
                this._FailedPasswordAttemptWindowStart = value;
                this.SendPropertyChanged("FailedPasswordAttemptWindowStart");
                this.OnFailedPasswordAttemptWindowStartChanged();
		    }
		}
		
        partial void OnFailedPasswordAnswerAttemptCountChanging(int value);
        partial void OnFailedPasswordAnswerAttemptCountChanged();
		
		private int _FailedPasswordAnswerAttemptCount;
		public int FailedPasswordAnswerAttemptCount { 
		    get{
		        return _FailedPasswordAnswerAttemptCount;
		    } 
		    set{
		        this.OnFailedPasswordAnswerAttemptCountChanging(value);
                this.SendPropertyChanging();
                this._FailedPasswordAnswerAttemptCount = value;
                this.SendPropertyChanged("FailedPasswordAnswerAttemptCount");
                this.OnFailedPasswordAnswerAttemptCountChanged();
		    }
		}
		
        partial void OnFailedPasswordAnswerAttemptWindowStartChanging(DateTime value);
        partial void OnFailedPasswordAnswerAttemptWindowStartChanged();
		
		private DateTime _FailedPasswordAnswerAttemptWindowStart;
		public DateTime FailedPasswordAnswerAttemptWindowStart { 
		    get{
		        return _FailedPasswordAnswerAttemptWindowStart;
		    } 
		    set{
		        this.OnFailedPasswordAnswerAttemptWindowStartChanging(value);
                this.SendPropertyChanging();
                this._FailedPasswordAnswerAttemptWindowStart = value;
                this.SendPropertyChanged("FailedPasswordAnswerAttemptWindowStart");
                this.OnFailedPasswordAnswerAttemptWindowStartChanged();
		    }
		}
		
        partial void OnCommentChanging(string value);
        partial void OnCommentChanged();
		
		private string _Comment;
		public string Comment { 
		    get{
		        return _Comment;
		    } 
		    set{
		        this.OnCommentChanging(value);
                this.SendPropertyChanging();
                this._Comment = value;
                this.SendPropertyChanged("Comment");
                this.OnCommentChanged();
		    }
		}
		

        #endregion

        #region Foreign Keys
        public IQueryable<aspnet_Application> aspnet_Applications
        {
            get
            {
                  var db=new Epilogger.Data.EpiloggerDB();
                  return from items in db.aspnet_Applications
                       where items.ApplicationId == _ApplicationId
                       select items;
            }
        }

        public IQueryable<aspnet_User> aspnet_Users
        {
            get
            {
                  var db=new Epilogger.Data.EpiloggerDB();
                  return from items in db.aspnet_Users
                       where items.UserId == _UserId
                       select items;
            }
        }

        #endregion


        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
        public event PropertyChangingEventHandler PropertyChanging;
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void SendPropertyChanging()
        {
            var handler = PropertyChanging;
            if (handler != null)
               handler(this, emptyChangingEventArgs);
        }

        protected virtual void SendPropertyChanged(String propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

	}
	
    
    
    /// <summary>
    /// A class which represents the Tweets table in the Epilogger Database.
    /// This class is queryable through EpiloggerDB.Tweet 
    /// </summary>

	public partial class Tweet: INotifyPropertyChanging, INotifyPropertyChanged
	{
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
	    
	    public Tweet(){
	        OnCreated();
	    }
	    
	    #region Properties
	    
        partial void OnIDChanging(long value);
        partial void OnIDChanged();
		
		private long _ID;
		public long ID { 
		    get{
		        return _ID;
		    } 
		    set{
		        this.OnIDChanging(value);
                this.SendPropertyChanging();
                this._ID = value;
                this.SendPropertyChanged("ID");
                this.OnIDChanged();
		    }
		}
		
        partial void OnTwitterIDChanging(long value);
        partial void OnTwitterIDChanged();
		
		private long _TwitterID;
		public long TwitterID { 
		    get{
		        return _TwitterID;
		    } 
		    set{
		        this.OnTwitterIDChanging(value);
                this.SendPropertyChanging();
                this._TwitterID = value;
                this.SendPropertyChanged("TwitterID");
                this.OnTwitterIDChanged();
		    }
		}
		
        partial void OnEventIDChanging(int? value);
        partial void OnEventIDChanged();
		
		private int? _EventID;
		public int? EventID { 
		    get{
		        return _EventID;
		    } 
		    set{
		        this.OnEventIDChanging(value);
                this.SendPropertyChanging();
                this._EventID = value;
                this.SendPropertyChanged("EventID");
                this.OnEventIDChanged();
		    }
		}
		
        partial void OnTextChanging(string value);
        partial void OnTextChanged();
		
		private string _Text;
		public string Text { 
		    get{
		        return _Text;
		    } 
		    set{
		        this.OnTextChanging(value);
                this.SendPropertyChanging();
                this._Text = value;
                this.SendPropertyChanged("Text");
                this.OnTextChanged();
		    }
		}
		
        partial void OnTextAsHTMLChanging(string value);
        partial void OnTextAsHTMLChanged();
		
		private string _TextAsHTML;
		public string TextAsHTML { 
		    get{
		        return _TextAsHTML;
		    } 
		    set{
		        this.OnTextAsHTMLChanging(value);
                this.SendPropertyChanging();
                this._TextAsHTML = value;
                this.SendPropertyChanged("TextAsHTML");
                this.OnTextAsHTMLChanged();
		    }
		}
		
        partial void OnSourceChanging(string value);
        partial void OnSourceChanged();
		
		private string _Source;
		public string Source { 
		    get{
		        return _Source;
		    } 
		    set{
		        this.OnSourceChanging(value);
                this.SendPropertyChanging();
                this._Source = value;
                this.SendPropertyChanged("Source");
                this.OnSourceChanged();
		    }
		}
		
        partial void OnCreatedDateChanging(DateTime? value);
        partial void OnCreatedDateChanged();
		
		private DateTime? _CreatedDate;
		public DateTime? CreatedDate { 
		    get{
		        return _CreatedDate;
		    } 
		    set{
		        this.OnCreatedDateChanging(value);
                this.SendPropertyChanging();
                this._CreatedDate = value;
                this.SendPropertyChanged("CreatedDate");
                this.OnCreatedDateChanged();
		    }
		}
		
        partial void OnToUserIDChanging(long? value);
        partial void OnToUserIDChanged();
		
		private long? _ToUserID;
		public long? ToUserID { 
		    get{
		        return _ToUserID;
		    } 
		    set{
		        this.OnToUserIDChanging(value);
                this.SendPropertyChanging();
                this._ToUserID = value;
                this.SendPropertyChanged("ToUserID");
                this.OnToUserIDChanged();
		    }
		}
		
        partial void OnFromUserIDChanging(long? value);
        partial void OnFromUserIDChanged();
		
		private long? _FromUserID;
		public long? FromUserID { 
		    get{
		        return _FromUserID;
		    } 
		    set{
		        this.OnFromUserIDChanging(value);
                this.SendPropertyChanging();
                this._FromUserID = value;
                this.SendPropertyChanged("FromUserID");
                this.OnFromUserIDChanged();
		    }
		}
		
        partial void OnFromUserScreenNameChanging(string value);
        partial void OnFromUserScreenNameChanged();
		
		private string _FromUserScreenName;
		public string FromUserScreenName { 
		    get{
		        return _FromUserScreenName;
		    } 
		    set{
		        this.OnFromUserScreenNameChanging(value);
                this.SendPropertyChanging();
                this._FromUserScreenName = value;
                this.SendPropertyChanged("FromUserScreenName");
                this.OnFromUserScreenNameChanged();
		    }
		}
		
        partial void OnToUserScreenNameChanging(string value);
        partial void OnToUserScreenNameChanged();
		
		private string _ToUserScreenName;
		public string ToUserScreenName { 
		    get{
		        return _ToUserScreenName;
		    } 
		    set{
		        this.OnToUserScreenNameChanging(value);
                this.SendPropertyChanging();
                this._ToUserScreenName = value;
                this.SendPropertyChanged("ToUserScreenName");
                this.OnToUserScreenNameChanged();
		    }
		}
		
        partial void OnIsoLanguageCodeChanging(string value);
        partial void OnIsoLanguageCodeChanged();
		
		private string _IsoLanguageCode;
		public string IsoLanguageCode { 
		    get{
		        return _IsoLanguageCode;
		    } 
		    set{
		        this.OnIsoLanguageCodeChanging(value);
                this.SendPropertyChanging();
                this._IsoLanguageCode = value;
                this.SendPropertyChanged("IsoLanguageCode");
                this.OnIsoLanguageCodeChanged();
		    }
		}
		
        partial void OnProfileImageURLChanging(string value);
        partial void OnProfileImageURLChanged();
		
		private string _ProfileImageURL;
		public string ProfileImageURL { 
		    get{
		        return _ProfileImageURL;
		    } 
		    set{
		        this.OnProfileImageURLChanging(value);
                this.SendPropertyChanging();
                this._ProfileImageURL = value;
                this.SendPropertyChanged("ProfileImageURL");
                this.OnProfileImageURLChanged();
		    }
		}
		
        partial void OnSinceIDChanging(long? value);
        partial void OnSinceIDChanged();
		
		private long? _SinceID;
		public long? SinceID { 
		    get{
		        return _SinceID;
		    } 
		    set{
		        this.OnSinceIDChanging(value);
                this.SendPropertyChanging();
                this._SinceID = value;
                this.SendPropertyChanged("SinceID");
                this.OnSinceIDChanged();
		    }
		}
		
        partial void OnLocationChanging(string value);
        partial void OnLocationChanged();
		
		private string _Location;
		public string Location { 
		    get{
		        return _Location;
		    } 
		    set{
		        this.OnLocationChanging(value);
                this.SendPropertyChanging();
                this._Location = value;
                this.SendPropertyChanged("Location");
                this.OnLocationChanged();
		    }
		}
		
        partial void OnRawSourceChanging(string value);
        partial void OnRawSourceChanged();
		
		private string _RawSource;
		public string RawSource { 
		    get{
		        return _RawSource;
		    } 
		    set{
		        this.OnRawSourceChanging(value);
                this.SendPropertyChanging();
                this._RawSource = value;
                this.SendPropertyChanged("RawSource");
                this.OnRawSourceChanged();
		    }
		}
		
        partial void OnDeleteVoteCountChanging(int? value);
        partial void OnDeleteVoteCountChanged();
		
		private int? _DeleteVoteCount;
		public int? DeleteVoteCount { 
		    get{
		        return _DeleteVoteCount;
		    } 
		    set{
		        this.OnDeleteVoteCountChanging(value);
                this.SendPropertyChanging();
                this._DeleteVoteCount = value;
                this.SendPropertyChanged("DeleteVoteCount");
                this.OnDeleteVoteCountChanged();
		    }
		}
		
        partial void OnDeletedChanging(bool? value);
        partial void OnDeletedChanged();
		
		private bool? _Deleted;
		public bool? Deleted { 
		    get{
		        return _Deleted;
		    } 
		    set{
		        this.OnDeletedChanging(value);
                this.SendPropertyChanging();
                this._Deleted = value;
                this.SendPropertyChanged("Deleted");
                this.OnDeletedChanged();
		    }
		}
		

        #endregion

        #region Foreign Keys
        public IQueryable<Event> Events
        {
            get
            {
                  var db=new Epilogger.Data.EpiloggerDB();
                  return from items in db.Events
                       where items.ID == _EventID
                       select items;
            }
        }

        public IQueryable<Picture> Pictures
        {
            get
            {
                  var db=new Epilogger.Data.EpiloggerDB();
                  return from items in db.Pictures
                       where items.TweetID == _ID
                       select items;
            }
        }

        public IQueryable<URL> URLS
        {
            get
            {
                  var db=new Epilogger.Data.EpiloggerDB();
                  return from items in db.URLS
                       where items.TweetID == _ID
                       select items;
            }
        }

        #endregion


        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
        public event PropertyChangingEventHandler PropertyChanging;
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void SendPropertyChanging()
        {
            var handler = PropertyChanging;
            if (handler != null)
               handler(this, emptyChangingEventArgs);
        }

        protected virtual void SendPropertyChanged(String propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

	}
	
    
    
    /// <summary>
    /// A class which represents the URLs table in the Epilogger Database.
    /// This class is queryable through EpiloggerDB.URL 
    /// </summary>

	public partial class URL: INotifyPropertyChanging, INotifyPropertyChanged
	{
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
	    
	    public URL(){
	        OnCreated();
	    }
	    
	    #region Properties
	    
        partial void OnIDChanging(int value);
        partial void OnIDChanged();
		
		private int _ID;
		public int ID { 
		    get{
		        return _ID;
		    } 
		    set{
		        this.OnIDChanging(value);
                this.SendPropertyChanging();
                this._ID = value;
                this.SendPropertyChanged("ID");
                this.OnIDChanged();
		    }
		}
		
        partial void OnEventIDChanging(int value);
        partial void OnEventIDChanged();
		
		private int _EventID;
		public int EventID { 
		    get{
		        return _EventID;
		    } 
		    set{
		        this.OnEventIDChanging(value);
                this.SendPropertyChanging();
                this._EventID = value;
                this.SendPropertyChanged("EventID");
                this.OnEventIDChanged();
		    }
		}
		
        partial void OnTweetIDChanging(long value);
        partial void OnTweetIDChanged();
		
		private long _TweetID;
		public long TweetID { 
		    get{
		        return _TweetID;
		    } 
		    set{
		        this.OnTweetIDChanging(value);
                this.SendPropertyChanging();
                this._TweetID = value;
                this.SendPropertyChanged("TweetID");
                this.OnTweetIDChanged();
		    }
		}
		
        partial void OnShortURLChanging(string value);
        partial void OnShortURLChanged();
		
		private string _ShortURL;
		public string ShortURL { 
		    get{
		        return _ShortURL;
		    } 
		    set{
		        this.OnShortURLChanging(value);
                this.SendPropertyChanging();
                this._ShortURL = value;
                this.SendPropertyChanged("ShortURL");
                this.OnShortURLChanged();
		    }
		}
		
        partial void OnURLXChanging(string value);
        partial void OnURLXChanged();
		
		private string _URLX;
		public string URLX { 
		    get{
		        return _URLX;
		    } 
		    set{
		        this.OnURLXChanging(value);
                this.SendPropertyChanging();
                this._URLX = value;
                this.SendPropertyChanged("URLX");
                this.OnURLXChanged();
		    }
		}
		
        partial void OnTypeChanging(string value);
        partial void OnTypeChanged();
		
		private string _Type;
		public string Type { 
		    get{
		        return _Type;
		    } 
		    set{
		        this.OnTypeChanging(value);
                this.SendPropertyChanging();
                this._Type = value;
                this.SendPropertyChanged("Type");
                this.OnTypeChanged();
		    }
		}
		
        partial void OnDeleteVoteCountChanging(int? value);
        partial void OnDeleteVoteCountChanged();
		
		private int? _DeleteVoteCount;
		public int? DeleteVoteCount { 
		    get{
		        return _DeleteVoteCount;
		    } 
		    set{
		        this.OnDeleteVoteCountChanging(value);
                this.SendPropertyChanging();
                this._DeleteVoteCount = value;
                this.SendPropertyChanged("DeleteVoteCount");
                this.OnDeleteVoteCountChanged();
		    }
		}
		
        partial void OnDeletedChanging(bool? value);
        partial void OnDeletedChanged();
		
		private bool? _Deleted;
		public bool? Deleted { 
		    get{
		        return _Deleted;
		    } 
		    set{
		        this.OnDeletedChanging(value);
                this.SendPropertyChanging();
                this._Deleted = value;
                this.SendPropertyChanged("Deleted");
                this.OnDeletedChanged();
		    }
		}
		

        #endregion

        #region Foreign Keys
        public IQueryable<Event> Events
        {
            get
            {
                  var db=new Epilogger.Data.EpiloggerDB();
                  return from items in db.Events
                       where items.ID == _EventID
                       select items;
            }
        }

        public IQueryable<Tweet> Tweets
        {
            get
            {
                  var db=new Epilogger.Data.EpiloggerDB();
                  return from items in db.Tweets
                       where items.ID == _TweetID
                       select items;
            }
        }

        #endregion


        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
        public event PropertyChangingEventHandler PropertyChanging;
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void SendPropertyChanging()
        {
            var handler = PropertyChanging;
            if (handler != null)
               handler(this, emptyChangingEventArgs);
        }

        protected virtual void SendPropertyChanged(String propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

	}
	
    
    
    /// <summary>
    /// A class which represents the aspnet_Profile table in the Epilogger Database.
    /// This class is queryable through EpiloggerDB.aspnet_Profile 
    /// </summary>

	public partial class aspnet_Profile: INotifyPropertyChanging, INotifyPropertyChanged
	{
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
	    
	    public aspnet_Profile(){
	        OnCreated();
	    }
	    
	    #region Properties
	    
        partial void OnUserIdChanging(Guid value);
        partial void OnUserIdChanged();
		
		private Guid _UserId;
		public Guid UserId { 
		    get{
		        return _UserId;
		    } 
		    set{
		        this.OnUserIdChanging(value);
                this.SendPropertyChanging();
                this._UserId = value;
                this.SendPropertyChanged("UserId");
                this.OnUserIdChanged();
		    }
		}
		
        partial void OnPropertyNamesChanging(string value);
        partial void OnPropertyNamesChanged();
		
		private string _PropertyNames;
		public string PropertyNames { 
		    get{
		        return _PropertyNames;
		    } 
		    set{
		        this.OnPropertyNamesChanging(value);
                this.SendPropertyChanging();
                this._PropertyNames = value;
                this.SendPropertyChanged("PropertyNames");
                this.OnPropertyNamesChanged();
		    }
		}
		
        partial void OnPropertyValuesStringChanging(string value);
        partial void OnPropertyValuesStringChanged();
		
		private string _PropertyValuesString;
		public string PropertyValuesString { 
		    get{
		        return _PropertyValuesString;
		    } 
		    set{
		        this.OnPropertyValuesStringChanging(value);
                this.SendPropertyChanging();
                this._PropertyValuesString = value;
                this.SendPropertyChanged("PropertyValuesString");
                this.OnPropertyValuesStringChanged();
		    }
		}
		
        partial void OnPropertyValuesBinaryChanging(byte[] value);
        partial void OnPropertyValuesBinaryChanged();
		
		private byte[] _PropertyValuesBinary;
		public byte[] PropertyValuesBinary { 
		    get{
		        return _PropertyValuesBinary;
		    } 
		    set{
		        this.OnPropertyValuesBinaryChanging(value);
                this.SendPropertyChanging();
                this._PropertyValuesBinary = value;
                this.SendPropertyChanged("PropertyValuesBinary");
                this.OnPropertyValuesBinaryChanged();
		    }
		}
		
        partial void OnLastUpdatedDateChanging(DateTime value);
        partial void OnLastUpdatedDateChanged();
		
		private DateTime _LastUpdatedDate;
		public DateTime LastUpdatedDate { 
		    get{
		        return _LastUpdatedDate;
		    } 
		    set{
		        this.OnLastUpdatedDateChanging(value);
                this.SendPropertyChanging();
                this._LastUpdatedDate = value;
                this.SendPropertyChanged("LastUpdatedDate");
                this.OnLastUpdatedDateChanged();
		    }
		}
		

        #endregion

        #region Foreign Keys
        public IQueryable<aspnet_User> aspnet_Users
        {
            get
            {
                  var db=new Epilogger.Data.EpiloggerDB();
                  return from items in db.aspnet_Users
                       where items.UserId == _UserId
                       select items;
            }
        }

        #endregion


        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
        public event PropertyChangingEventHandler PropertyChanging;
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void SendPropertyChanging()
        {
            var handler = PropertyChanging;
            if (handler != null)
               handler(this, emptyChangingEventArgs);
        }

        protected virtual void SendPropertyChanged(String propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

	}
	
    
    
    /// <summary>
    /// A class which represents the aspnet_Roles table in the Epilogger Database.
    /// This class is queryable through EpiloggerDB.aspnet_Role 
    /// </summary>

	public partial class aspnet_Role: INotifyPropertyChanging, INotifyPropertyChanged
	{
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
	    
	    public aspnet_Role(){
	        OnCreated();
	    }
	    
	    #region Properties
	    
        partial void OnApplicationIdChanging(Guid value);
        partial void OnApplicationIdChanged();
		
		private Guid _ApplicationId;
		public Guid ApplicationId { 
		    get{
		        return _ApplicationId;
		    } 
		    set{
		        this.OnApplicationIdChanging(value);
                this.SendPropertyChanging();
                this._ApplicationId = value;
                this.SendPropertyChanged("ApplicationId");
                this.OnApplicationIdChanged();
		    }
		}
		
        partial void OnRoleIdChanging(Guid value);
        partial void OnRoleIdChanged();
		
		private Guid _RoleId;
		public Guid RoleId { 
		    get{
		        return _RoleId;
		    } 
		    set{
		        this.OnRoleIdChanging(value);
                this.SendPropertyChanging();
                this._RoleId = value;
                this.SendPropertyChanged("RoleId");
                this.OnRoleIdChanged();
		    }
		}
		
        partial void OnRoleNameChanging(string value);
        partial void OnRoleNameChanged();
		
		private string _RoleName;
		public string RoleName { 
		    get{
		        return _RoleName;
		    } 
		    set{
		        this.OnRoleNameChanging(value);
                this.SendPropertyChanging();
                this._RoleName = value;
                this.SendPropertyChanged("RoleName");
                this.OnRoleNameChanged();
		    }
		}
		
        partial void OnLoweredRoleNameChanging(string value);
        partial void OnLoweredRoleNameChanged();
		
		private string _LoweredRoleName;
		public string LoweredRoleName { 
		    get{
		        return _LoweredRoleName;
		    } 
		    set{
		        this.OnLoweredRoleNameChanging(value);
                this.SendPropertyChanging();
                this._LoweredRoleName = value;
                this.SendPropertyChanged("LoweredRoleName");
                this.OnLoweredRoleNameChanged();
		    }
		}
		
        partial void OnDescriptionChanging(string value);
        partial void OnDescriptionChanged();
		
		private string _Description;
		public string Description { 
		    get{
		        return _Description;
		    } 
		    set{
		        this.OnDescriptionChanging(value);
                this.SendPropertyChanging();
                this._Description = value;
                this.SendPropertyChanged("Description");
                this.OnDescriptionChanged();
		    }
		}
		

        #endregion

        #region Foreign Keys
        public IQueryable<aspnet_Application> aspnet_Applications
        {
            get
            {
                  var db=new Epilogger.Data.EpiloggerDB();
                  return from items in db.aspnet_Applications
                       where items.ApplicationId == _ApplicationId
                       select items;
            }
        }

        public IQueryable<aspnet_UsersInRole> aspnet_UsersInRoles
        {
            get
            {
                  var db=new Epilogger.Data.EpiloggerDB();
                  return from items in db.aspnet_UsersInRoles
                       where items.RoleId == _RoleId
                       select items;
            }
        }

        #endregion


        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
        public event PropertyChangingEventHandler PropertyChanging;
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void SendPropertyChanging()
        {
            var handler = PropertyChanging;
            if (handler != null)
               handler(this, emptyChangingEventArgs);
        }

        protected virtual void SendPropertyChanged(String propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

	}
	
    
    
    /// <summary>
    /// A class which represents the aspnet_UsersInRoles table in the Epilogger Database.
    /// This class is queryable through EpiloggerDB.aspnet_UsersInRole 
    /// </summary>

	public partial class aspnet_UsersInRole: INotifyPropertyChanging, INotifyPropertyChanged
	{
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
	    
	    public aspnet_UsersInRole(){
	        OnCreated();
	    }
	    
	    #region Properties
	    
        partial void OnUserIdChanging(Guid value);
        partial void OnUserIdChanged();
		
		private Guid _UserId;
		public Guid UserId { 
		    get{
		        return _UserId;
		    } 
		    set{
		        this.OnUserIdChanging(value);
                this.SendPropertyChanging();
                this._UserId = value;
                this.SendPropertyChanged("UserId");
                this.OnUserIdChanged();
		    }
		}
		
        partial void OnRoleIdChanging(Guid value);
        partial void OnRoleIdChanged();
		
		private Guid _RoleId;
		public Guid RoleId { 
		    get{
		        return _RoleId;
		    } 
		    set{
		        this.OnRoleIdChanging(value);
                this.SendPropertyChanging();
                this._RoleId = value;
                this.SendPropertyChanged("RoleId");
                this.OnRoleIdChanged();
		    }
		}
		

        #endregion

        #region Foreign Keys
        public IQueryable<aspnet_Role> aspnet_Roles
        {
            get
            {
                  var db=new Epilogger.Data.EpiloggerDB();
                  return from items in db.aspnet_Roles
                       where items.RoleId == _RoleId
                       select items;
            }
        }

        public IQueryable<aspnet_User> aspnet_Users
        {
            get
            {
                  var db=new Epilogger.Data.EpiloggerDB();
                  return from items in db.aspnet_Users
                       where items.UserId == _UserId
                       select items;
            }
        }

        #endregion


        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
        public event PropertyChangingEventHandler PropertyChanging;
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void SendPropertyChanging()
        {
            var handler = PropertyChanging;
            if (handler != null)
               handler(this, emptyChangingEventArgs);
        }

        protected virtual void SendPropertyChanged(String propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

	}
	
    
    
    /// <summary>
    /// A class which represents the ImageMetaData table in the Epilogger Database.
    /// This class is queryable through EpiloggerDB.ImageMetaDatum 
    /// </summary>

	public partial class ImageMetaDatum: INotifyPropertyChanging, INotifyPropertyChanged
	{
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
	    
	    public ImageMetaDatum(){
	        OnCreated();
	    }
	    
	    #region Properties
	    
        partial void OnIDChanging(int value);
        partial void OnIDChanged();
		
		private int _ID;
		public int ID { 
		    get{
		        return _ID;
		    } 
		    set{
		        this.OnIDChanging(value);
                this.SendPropertyChanging();
                this._ID = value;
                this.SendPropertyChanged("ID");
                this.OnIDChanged();
		    }
		}
		
        partial void OnImageIDChanging(int value);
        partial void OnImageIDChanged();
		
		private int _ImageID;
		public int ImageID { 
		    get{
		        return _ImageID;
		    } 
		    set{
		        this.OnImageIDChanging(value);
                this.SendPropertyChanging();
                this._ImageID = value;
                this.SendPropertyChanged("ImageID");
                this.OnImageIDChanged();
		    }
		}
		
        partial void OnEventIDChanging(int value);
        partial void OnEventIDChanged();
		
		private int _EventID;
		public int EventID { 
		    get{
		        return _EventID;
		    } 
		    set{
		        this.OnEventIDChanging(value);
                this.SendPropertyChanging();
                this._EventID = value;
                this.SendPropertyChanged("EventID");
                this.OnEventIDChanged();
		    }
		}
		
        partial void OnUserIDChanging(Guid? value);
        partial void OnUserIDChanged();
		
		private Guid? _UserID;
		public Guid? UserID { 
		    get{
		        return _UserID;
		    } 
		    set{
		        this.OnUserIDChanging(value);
                this.SendPropertyChanging();
                this._UserID = value;
                this.SendPropertyChanged("UserID");
                this.OnUserIDChanged();
		    }
		}
		
        partial void OnImageSourceChanging(string value);
        partial void OnImageSourceChanged();
		
		private string _ImageSource;
		public string ImageSource { 
		    get{
		        return _ImageSource;
		    } 
		    set{
		        this.OnImageSourceChanging(value);
                this.SendPropertyChanging();
                this._ImageSource = value;
                this.SendPropertyChanged("ImageSource");
                this.OnImageSourceChanged();
		    }
		}
		
        partial void OnTwitterIDChanging(long? value);
        partial void OnTwitterIDChanged();
		
		private long? _TwitterID;
		public long? TwitterID { 
		    get{
		        return _TwitterID;
		    } 
		    set{
		        this.OnTwitterIDChanging(value);
                this.SendPropertyChanging();
                this._TwitterID = value;
                this.SendPropertyChanged("TwitterID");
                this.OnTwitterIDChanged();
		    }
		}
		
        partial void OnTwitterNameChanging(string value);
        partial void OnTwitterNameChanged();
		
		private string _TwitterName;
		public string TwitterName { 
		    get{
		        return _TwitterName;
		    } 
		    set{
		        this.OnTwitterNameChanging(value);
                this.SendPropertyChanging();
                this._TwitterName = value;
                this.SendPropertyChanged("TwitterName");
                this.OnTwitterNameChanged();
		    }
		}
		

        #endregion

        #region Foreign Keys
        public IQueryable<Event> Events
        {
            get
            {
                  var db=new Epilogger.Data.EpiloggerDB();
                  return from items in db.Events
                       where items.ID == _EventID
                       select items;
            }
        }

        #endregion


        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
        public event PropertyChangingEventHandler PropertyChanging;
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void SendPropertyChanging()
        {
            var handler = PropertyChanging;
            if (handler != null)
               handler(this, emptyChangingEventArgs);
        }

        protected virtual void SendPropertyChanged(String propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

	}
	
    
    
    /// <summary>
    /// A class which represents the Images table in the Epilogger Database.
    /// This class is queryable through EpiloggerDB.Image 
    /// </summary>

	public partial class Image: INotifyPropertyChanging, INotifyPropertyChanged
	{
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
	    
	    public Image(){
	        OnCreated();
	    }
	    
	    #region Properties
	    
        partial void OnIDChanging(int value);
        partial void OnIDChanged();
		
		private int _ID;
		public int ID { 
		    get{
		        return _ID;
		    } 
		    set{
		        this.OnIDChanging(value);
                this.SendPropertyChanging();
                this._ID = value;
                this.SendPropertyChanged("ID");
                this.OnIDChanged();
		    }
		}
		
        partial void OnEventIDChanging(int value);
        partial void OnEventIDChanged();
		
		private int _EventID;
		public int EventID { 
		    get{
		        return _EventID;
		    } 
		    set{
		        this.OnEventIDChanging(value);
                this.SendPropertyChanging();
                this._EventID = value;
                this.SendPropertyChanged("EventID");
                this.OnEventIDChanged();
		    }
		}
		
        partial void OnAzureContainerPrefixChanging(string value);
        partial void OnAzureContainerPrefixChanged();
		
		private string _AzureContainerPrefix;
		public string AzureContainerPrefix { 
		    get{
		        return _AzureContainerPrefix;
		    } 
		    set{
		        this.OnAzureContainerPrefixChanging(value);
                this.SendPropertyChanging();
                this._AzureContainerPrefix = value;
                this.SendPropertyChanged("AzureContainerPrefix");
                this.OnAzureContainerPrefixChanged();
		    }
		}
		
        partial void OnFullsizeChanging(string value);
        partial void OnFullsizeChanged();
		
		private string _Fullsize;
		public string Fullsize { 
		    get{
		        return _Fullsize;
		    } 
		    set{
		        this.OnFullsizeChanging(value);
                this.SendPropertyChanging();
                this._Fullsize = value;
                this.SendPropertyChanged("Fullsize");
                this.OnFullsizeChanged();
		    }
		}
		
        partial void OnThumbChanging(string value);
        partial void OnThumbChanged();
		
		private string _Thumb;
		public string Thumb { 
		    get{
		        return _Thumb;
		    } 
		    set{
		        this.OnThumbChanging(value);
                this.SendPropertyChanging();
                this._Thumb = value;
                this.SendPropertyChanged("Thumb");
                this.OnThumbChanged();
		    }
		}
		
        partial void OnOriginalImageLinkChanging(string value);
        partial void OnOriginalImageLinkChanged();
		
		private string _OriginalImageLink;
		public string OriginalImageLink { 
		    get{
		        return _OriginalImageLink;
		    } 
		    set{
		        this.OnOriginalImageLinkChanging(value);
                this.SendPropertyChanging();
                this._OriginalImageLink = value;
                this.SendPropertyChanged("OriginalImageLink");
                this.OnOriginalImageLinkChanged();
		    }
		}
		
        partial void OnDateTimeChanging(DateTime value);
        partial void OnDateTimeChanged();
		
		private DateTime _DateTime;
		public DateTime DateTime { 
		    get{
		        return _DateTime;
		    } 
		    set{
		        this.OnDateTimeChanging(value);
                this.SendPropertyChanging();
                this._DateTime = value;
                this.SendPropertyChanged("DateTime");
                this.OnDateTimeChanged();
		    }
		}
		
        partial void OnDeleteVoteCountChanging(int? value);
        partial void OnDeleteVoteCountChanged();
		
		private int? _DeleteVoteCount;
		public int? DeleteVoteCount { 
		    get{
		        return _DeleteVoteCount;
		    } 
		    set{
		        this.OnDeleteVoteCountChanging(value);
                this.SendPropertyChanging();
                this._DeleteVoteCount = value;
                this.SendPropertyChanged("DeleteVoteCount");
                this.OnDeleteVoteCountChanged();
		    }
		}
		
        partial void OnDeletedChanging(bool value);
        partial void OnDeletedChanged();
		
		private bool _Deleted;
		public bool Deleted { 
		    get{
		        return _Deleted;
		    } 
		    set{
		        this.OnDeletedChanging(value);
                this.SendPropertyChanging();
                this._Deleted = value;
                this.SendPropertyChanged("Deleted");
                this.OnDeletedChanged();
		    }
		}
		

        #endregion

        #region Foreign Keys
        public IQueryable<Event> Events
        {
            get
            {
                  var db=new Epilogger.Data.EpiloggerDB();
                  return from items in db.Events
                       where items.ID == _EventID
                       select items;
            }
        }

        #endregion


        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
        public event PropertyChangingEventHandler PropertyChanging;
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void SendPropertyChanging()
        {
            var handler = PropertyChanging;
            if (handler != null)
               handler(this, emptyChangingEventArgs);
        }

        protected virtual void SendPropertyChanged(String propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

	}
	
    
    
    /// <summary>
    /// A class which represents the aspnet_Paths table in the Epilogger Database.
    /// This class is queryable through EpiloggerDB.aspnet_Path 
    /// </summary>

	public partial class aspnet_Path: INotifyPropertyChanging, INotifyPropertyChanged
	{
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
	    
	    public aspnet_Path(){
	        OnCreated();
	    }
	    
	    #region Properties
	    
        partial void OnApplicationIdChanging(Guid value);
        partial void OnApplicationIdChanged();
		
		private Guid _ApplicationId;
		public Guid ApplicationId { 
		    get{
		        return _ApplicationId;
		    } 
		    set{
		        this.OnApplicationIdChanging(value);
                this.SendPropertyChanging();
                this._ApplicationId = value;
                this.SendPropertyChanged("ApplicationId");
                this.OnApplicationIdChanged();
		    }
		}
		
        partial void OnPathIdChanging(Guid value);
        partial void OnPathIdChanged();
		
		private Guid _PathId;
		public Guid PathId { 
		    get{
		        return _PathId;
		    } 
		    set{
		        this.OnPathIdChanging(value);
                this.SendPropertyChanging();
                this._PathId = value;
                this.SendPropertyChanged("PathId");
                this.OnPathIdChanged();
		    }
		}
		
        partial void OnPathChanging(string value);
        partial void OnPathChanged();
		
		private string _Path;
		public string Path { 
		    get{
		        return _Path;
		    } 
		    set{
		        this.OnPathChanging(value);
                this.SendPropertyChanging();
                this._Path = value;
                this.SendPropertyChanged("Path");
                this.OnPathChanged();
		    }
		}
		
        partial void OnLoweredPathChanging(string value);
        partial void OnLoweredPathChanged();
		
		private string _LoweredPath;
		public string LoweredPath { 
		    get{
		        return _LoweredPath;
		    } 
		    set{
		        this.OnLoweredPathChanging(value);
                this.SendPropertyChanging();
                this._LoweredPath = value;
                this.SendPropertyChanged("LoweredPath");
                this.OnLoweredPathChanged();
		    }
		}
		

        #endregion

        #region Foreign Keys
        public IQueryable<aspnet_Application> aspnet_Applications
        {
            get
            {
                  var db=new Epilogger.Data.EpiloggerDB();
                  return from items in db.aspnet_Applications
                       where items.ApplicationId == _ApplicationId
                       select items;
            }
        }

        public IQueryable<aspnet_PersonalizationAllUser> aspnet_PersonalizationAllUsers
        {
            get
            {
                  var db=new Epilogger.Data.EpiloggerDB();
                  return from items in db.aspnet_PersonalizationAllUsers
                       where items.PathId == _PathId
                       select items;
            }
        }

        public IQueryable<aspnet_PersonalizationPerUser> aspnet_PersonalizationPerUsers
        {
            get
            {
                  var db=new Epilogger.Data.EpiloggerDB();
                  return from items in db.aspnet_PersonalizationPerUsers
                       where items.PathId == _PathId
                       select items;
            }
        }

        #endregion


        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
        public event PropertyChangingEventHandler PropertyChanging;
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void SendPropertyChanging()
        {
            var handler = PropertyChanging;
            if (handler != null)
               handler(this, emptyChangingEventArgs);
        }

        protected virtual void SendPropertyChanged(String propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

	}
	
    
    
    /// <summary>
    /// A class which represents the aspnet_PersonalizationAllUsers table in the Epilogger Database.
    /// This class is queryable through EpiloggerDB.aspnet_PersonalizationAllUser 
    /// </summary>

	public partial class aspnet_PersonalizationAllUser: INotifyPropertyChanging, INotifyPropertyChanged
	{
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
	    
	    public aspnet_PersonalizationAllUser(){
	        OnCreated();
	    }
	    
	    #region Properties
	    
        partial void OnPathIdChanging(Guid value);
        partial void OnPathIdChanged();
		
		private Guid _PathId;
		public Guid PathId { 
		    get{
		        return _PathId;
		    } 
		    set{
		        this.OnPathIdChanging(value);
                this.SendPropertyChanging();
                this._PathId = value;
                this.SendPropertyChanged("PathId");
                this.OnPathIdChanged();
		    }
		}
		
        partial void OnPageSettingsChanging(byte[] value);
        partial void OnPageSettingsChanged();
		
		private byte[] _PageSettings;
		public byte[] PageSettings { 
		    get{
		        return _PageSettings;
		    } 
		    set{
		        this.OnPageSettingsChanging(value);
                this.SendPropertyChanging();
                this._PageSettings = value;
                this.SendPropertyChanged("PageSettings");
                this.OnPageSettingsChanged();
		    }
		}
		
        partial void OnLastUpdatedDateChanging(DateTime value);
        partial void OnLastUpdatedDateChanged();
		
		private DateTime _LastUpdatedDate;
		public DateTime LastUpdatedDate { 
		    get{
		        return _LastUpdatedDate;
		    } 
		    set{
		        this.OnLastUpdatedDateChanging(value);
                this.SendPropertyChanging();
                this._LastUpdatedDate = value;
                this.SendPropertyChanged("LastUpdatedDate");
                this.OnLastUpdatedDateChanged();
		    }
		}
		

        #endregion

        #region Foreign Keys
        public IQueryable<aspnet_Path> aspnet_Paths
        {
            get
            {
                  var db=new Epilogger.Data.EpiloggerDB();
                  return from items in db.aspnet_Paths
                       where items.PathId == _PathId
                       select items;
            }
        }

        #endregion


        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
        public event PropertyChangingEventHandler PropertyChanging;
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void SendPropertyChanging()
        {
            var handler = PropertyChanging;
            if (handler != null)
               handler(this, emptyChangingEventArgs);
        }

        protected virtual void SendPropertyChanged(String propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

	}
	
    
    
    /// <summary>
    /// A class which represents the aspnet_PersonalizationPerUser table in the Epilogger Database.
    /// This class is queryable through EpiloggerDB.aspnet_PersonalizationPerUser 
    /// </summary>

	public partial class aspnet_PersonalizationPerUser: INotifyPropertyChanging, INotifyPropertyChanged
	{
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
	    
	    public aspnet_PersonalizationPerUser(){
	        OnCreated();
	    }
	    
	    #region Properties
	    
        partial void OnIdChanging(Guid value);
        partial void OnIdChanged();
		
		private Guid _Id;
		public Guid Id { 
		    get{
		        return _Id;
		    } 
		    set{
		        this.OnIdChanging(value);
                this.SendPropertyChanging();
                this._Id = value;
                this.SendPropertyChanged("Id");
                this.OnIdChanged();
		    }
		}
		
        partial void OnPathIdChanging(Guid? value);
        partial void OnPathIdChanged();
		
		private Guid? _PathId;
		public Guid? PathId { 
		    get{
		        return _PathId;
		    } 
		    set{
		        this.OnPathIdChanging(value);
                this.SendPropertyChanging();
                this._PathId = value;
                this.SendPropertyChanged("PathId");
                this.OnPathIdChanged();
		    }
		}
		
        partial void OnUserIdChanging(Guid? value);
        partial void OnUserIdChanged();
		
		private Guid? _UserId;
		public Guid? UserId { 
		    get{
		        return _UserId;
		    } 
		    set{
		        this.OnUserIdChanging(value);
                this.SendPropertyChanging();
                this._UserId = value;
                this.SendPropertyChanged("UserId");
                this.OnUserIdChanged();
		    }
		}
		
        partial void OnPageSettingsChanging(byte[] value);
        partial void OnPageSettingsChanged();
		
		private byte[] _PageSettings;
		public byte[] PageSettings { 
		    get{
		        return _PageSettings;
		    } 
		    set{
		        this.OnPageSettingsChanging(value);
                this.SendPropertyChanging();
                this._PageSettings = value;
                this.SendPropertyChanged("PageSettings");
                this.OnPageSettingsChanged();
		    }
		}
		
        partial void OnLastUpdatedDateChanging(DateTime value);
        partial void OnLastUpdatedDateChanged();
		
		private DateTime _LastUpdatedDate;
		public DateTime LastUpdatedDate { 
		    get{
		        return _LastUpdatedDate;
		    } 
		    set{
		        this.OnLastUpdatedDateChanging(value);
                this.SendPropertyChanging();
                this._LastUpdatedDate = value;
                this.SendPropertyChanged("LastUpdatedDate");
                this.OnLastUpdatedDateChanged();
		    }
		}
		

        #endregion

        #region Foreign Keys
        public IQueryable<aspnet_Path> aspnet_Paths
        {
            get
            {
                  var db=new Epilogger.Data.EpiloggerDB();
                  return from items in db.aspnet_Paths
                       where items.PathId == _PathId
                       select items;
            }
        }

        public IQueryable<aspnet_User> aspnet_Users
        {
            get
            {
                  var db=new Epilogger.Data.EpiloggerDB();
                  return from items in db.aspnet_Users
                       where items.UserId == _UserId
                       select items;
            }
        }

        #endregion


        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
        public event PropertyChangingEventHandler PropertyChanging;
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void SendPropertyChanging()
        {
            var handler = PropertyChanging;
            if (handler != null)
               handler(this, emptyChangingEventArgs);
        }

        protected virtual void SendPropertyChanged(String propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

	}
	
    
    
    /// <summary>
    /// A class which represents the Venues table in the Epilogger Database.
    /// This class is queryable through EpiloggerDB.Venue 
    /// </summary>

	public partial class Venue: INotifyPropertyChanging, INotifyPropertyChanged
	{
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
	    
	    public Venue(){
	        OnCreated();
	    }
	    
	    #region Properties
	    
        partial void OnVenueIDChanging(long value);
        partial void OnVenueIDChanged();
		
		private long _VenueID;
		public long VenueID { 
		    get{
		        return _VenueID;
		    } 
		    set{
		        this.OnVenueIDChanging(value);
                this.SendPropertyChanging();
                this._VenueID = value;
                this.SendPropertyChanged("VenueID");
                this.OnVenueIDChanged();
		    }
		}
		
        partial void OnAddressChanging(string value);
        partial void OnAddressChanged();
		
		private string _Address;
		public string Address { 
		    get{
		        return _Address;
		    } 
		    set{
		        this.OnAddressChanging(value);
                this.SendPropertyChanging();
                this._Address = value;
                this.SendPropertyChanged("Address");
                this.OnAddressChanged();
		    }
		}
		
        partial void OnCityChanging(string value);
        partial void OnCityChanged();
		
		private string _City;
		public string City { 
		    get{
		        return _City;
		    } 
		    set{
		        this.OnCityChanging(value);
                this.SendPropertyChanging();
                this._City = value;
                this.SendPropertyChanged("City");
                this.OnCityChanged();
		    }
		}
		
        partial void OnCrossStreetChanging(string value);
        partial void OnCrossStreetChanged();
		
		private string _CrossStreet;
		public string CrossStreet { 
		    get{
		        return _CrossStreet;
		    } 
		    set{
		        this.OnCrossStreetChanging(value);
                this.SendPropertyChanging();
                this._CrossStreet = value;
                this.SendPropertyChanged("CrossStreet");
                this.OnCrossStreetChanged();
		    }
		}
		
        partial void OnGeolatChanging(double? value);
        partial void OnGeolatChanged();
		
		private double? _Geolat;
		public double? Geolat { 
		    get{
		        return _Geolat;
		    } 
		    set{
		        this.OnGeolatChanging(value);
                this.SendPropertyChanging();
                this._Geolat = value;
                this.SendPropertyChanged("Geolat");
                this.OnGeolatChanged();
		    }
		}
		
        partial void OnGeolongChanging(double? value);
        partial void OnGeolongChanged();
		
		private double? _Geolong;
		public double? Geolong { 
		    get{
		        return _Geolong;
		    } 
		    set{
		        this.OnGeolongChanging(value);
                this.SendPropertyChanging();
                this._Geolong = value;
                this.SendPropertyChanged("Geolong");
                this.OnGeolongChanged();
		    }
		}
		
        partial void OnNameChanging(string value);
        partial void OnNameChanged();
		
		private string _Name;
		public string Name { 
		    get{
		        return _Name;
		    } 
		    set{
		        this.OnNameChanging(value);
                this.SendPropertyChanging();
                this._Name = value;
                this.SendPropertyChanged("Name");
                this.OnNameChanged();
		    }
		}
		
        partial void OnPhoneChanging(string value);
        partial void OnPhoneChanged();
		
		private string _Phone;
		public string Phone { 
		    get{
		        return _Phone;
		    } 
		    set{
		        this.OnPhoneChanging(value);
                this.SendPropertyChanging();
                this._Phone = value;
                this.SendPropertyChanged("Phone");
                this.OnPhoneChanged();
		    }
		}
		
        partial void OnStateChanging(string value);
        partial void OnStateChanged();
		
		private string _State;
		public string State { 
		    get{
		        return _State;
		    } 
		    set{
		        this.OnStateChanging(value);
                this.SendPropertyChanging();
                this._State = value;
                this.SendPropertyChanged("State");
                this.OnStateChanged();
		    }
		}
		
        partial void OnZipChanging(string value);
        partial void OnZipChanged();
		
		private string _Zip;
		public string Zip { 
		    get{
		        return _Zip;
		    } 
		    set{
		        this.OnZipChanging(value);
                this.SendPropertyChanging();
                this._Zip = value;
                this.SendPropertyChanged("Zip");
                this.OnZipChanged();
		    }
		}
		

        #endregion

        #region Foreign Keys
        public IQueryable<Event> Events
        {
            get
            {
                  var db=new Epilogger.Data.EpiloggerDB();
                  return from items in db.Events
                       where items.VenueID == _VenueID
                       select items;
            }
        }

        #endregion


        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
        public event PropertyChangingEventHandler PropertyChanging;
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void SendPropertyChanging()
        {
            var handler = PropertyChanging;
            if (handler != null)
               handler(this, emptyChangingEventArgs);
        }

        protected virtual void SendPropertyChanged(String propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

	}
	
}