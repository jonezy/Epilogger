﻿


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
		
        partial void OnURLStubChanging(string value);
        partial void OnURLStubChanged();
		
		private string _URLStub;
		public string URLStub { 
		    get{
		        return _URLStub;
		    } 
		    set{
		        this.OnURLStubChanging(value);
                this.SendPropertyChanging();
                this._URLStub = value;
                this.SendPropertyChanged("URLStub");
                this.OnURLStubChanged();
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
    /// A class which represents the APIApplication table in the Epilogger Database.
    /// This class is queryable through EpiloggerDB.APIApplication 
    /// </summary>

	public partial class APIApplication: INotifyPropertyChanging, INotifyPropertyChanged
	{
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
	    
	    public APIApplication(){
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
		
        partial void OnClientIDChanging(string value);
        partial void OnClientIDChanged();
		
		private string _ClientID;
		public string ClientID { 
		    get{
		        return _ClientID;
		    } 
		    set{
		        this.OnClientIDChanging(value);
                this.SendPropertyChanging();
                this._ClientID = value;
                this.SendPropertyChanged("ClientID");
                this.OnClientIDChanged();
		    }
		}
		
        partial void OnClientSecretChanging(string value);
        partial void OnClientSecretChanged();
		
		private string _ClientSecret;
		public string ClientSecret { 
		    get{
		        return _ClientSecret;
		    } 
		    set{
		        this.OnClientSecretChanging(value);
                this.SendPropertyChanging();
                this._ClientSecret = value;
                this.SendPropertyChanged("ClientSecret");
                this.OnClientSecretChanged();
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
		
        partial void OnWebsiteChanging(string value);
        partial void OnWebsiteChanged();
		
		private string _Website;
		public string Website { 
		    get{
		        return _Website;
		    } 
		    set{
		        this.OnWebsiteChanging(value);
                this.SendPropertyChanging();
                this._Website = value;
                this.SendPropertyChanged("Website");
                this.OnWebsiteChanged();
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
		
        partial void OnFromUserDisplayNameChanging(string value);
        partial void OnFromUserDisplayNameChanged();
		
		private string _FromUserDisplayName;
		public string FromUserDisplayName { 
		    get{
		        return _FromUserDisplayName;
		    } 
		    set{
		        this.OnFromUserDisplayNameChanging(value);
                this.SendPropertyChanging();
                this._FromUserDisplayName = value;
                this.SendPropertyChanged("FromUserDisplayName");
                this.OnFromUserDisplayNameChanged();
		    }
		}
		
        partial void OnLatitudeChanging(string value);
        partial void OnLatitudeChanged();
		
		private string _Latitude;
		public string Latitude { 
		    get{
		        return _Latitude;
		    } 
		    set{
		        this.OnLatitudeChanging(value);
                this.SendPropertyChanging();
                this._Latitude = value;
                this.SendPropertyChanged("Latitude");
                this.OnLatitudeChanged();
		    }
		}
		
        partial void OnLongitudeChanging(string value);
        partial void OnLongitudeChanged();
		
		private string _Longitude;
		public string Longitude { 
		    get{
		        return _Longitude;
		    } 
		    set{
		        this.OnLongitudeChanging(value);
                this.SendPropertyChanging();
                this._Longitude = value;
                this.SendPropertyChanged("Longitude");
                this.OnLongitudeChanged();
		    }
		}
		
        partial void OnInReplyToStatusIdChanging(long? value);
        partial void OnInReplyToStatusIdChanged();
		
		private long? _InReplyToStatusId;
		public long? InReplyToStatusId { 
		    get{
		        return _InReplyToStatusId;
		    } 
		    set{
		        this.OnInReplyToStatusIdChanging(value);
                this.SendPropertyChanging();
                this._InReplyToStatusId = value;
                this.SendPropertyChanged("InReplyToStatusId");
                this.OnInReplyToStatusIdChanged();
		    }
		}
		
        partial void OnToUserDisplayNameChanging(string value);
        partial void OnToUserDisplayNameChanged();
		
		private string _ToUserDisplayName;
		public string ToUserDisplayName { 
		    get{
		        return _ToUserDisplayName;
		    } 
		    set{
		        this.OnToUserDisplayNameChanging(value);
                this.SendPropertyChanging();
                this._ToUserDisplayName = value;
                this.SendPropertyChanged("ToUserDisplayName");
                this.OnToUserDisplayNameChanged();
		    }
		}
		
        partial void OnLatitudeStrChanging(string value);
        partial void OnLatitudeStrChanged();
		
		private string _LatitudeStr;
		public string LatitudeStr { 
		    get{
		        return _LatitudeStr;
		    } 
		    set{
		        this.OnLatitudeStrChanging(value);
                this.SendPropertyChanging();
                this._LatitudeStr = value;
                this.SendPropertyChanged("LatitudeStr");
                this.OnLatitudeStrChanged();
		    }
		}
		
        partial void OnLongitudeStrChanging(string value);
        partial void OnLongitudeStrChanged();
		
		private string _LongitudeStr;
		public string LongitudeStr { 
		    get{
		        return _LongitudeStr;
		    } 
		    set{
		        this.OnLongitudeStrChanging(value);
                this.SendPropertyChanging();
                this._LongitudeStr = value;
                this.SendPropertyChanged("LongitudeStr");
                this.OnLongitudeStrChanged();
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

        public IQueryable<CheckIn> CheckIns
        {
            get
            {
                  var db=new Epilogger.Data.EpiloggerDB();
                  return from items in db.CheckIns
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
    /// A class which represents the ScraperURLs table in the Epilogger Database.
    /// This class is queryable through EpiloggerDB.ScraperURL 
    /// </summary>

	public partial class ScraperURL: INotifyPropertyChanging, INotifyPropertyChanged
	{
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
	    
	    public ScraperURL(){
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
		
        partial void OnURLChanging(string value);
        partial void OnURLChanged();
		
		private string _URL;
		public string URL { 
		    get{
		        return _URL;
		    } 
		    set{
		        this.OnURLChanging(value);
                this.SendPropertyChanging();
                this._URL = value;
                this.SendPropertyChanged("URL");
                this.OnURLChanged();
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
		
        partial void OnFullURLChanging(string value);
        partial void OnFullURLChanged();
		
		private string _FullURL;
		public string FullURL { 
		    get{
		        return _FullURL;
		    } 
		    set{
		        this.OnFullURLChanging(value);
                this.SendPropertyChanging();
                this._FullURL = value;
                this.SendPropertyChanged("FullURL");
                this.OnFullURLChanged();
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
		
        partial void OnDateTimeChanging(DateTime? value);
        partial void OnDateTimeChanged();
		
		private DateTime? _DateTime;
		public DateTime? DateTime { 
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
    /// A class which represents the UserLoginTracking table in the Epilogger Database.
    /// This class is queryable through EpiloggerDB.UserLoginTracking 
    /// </summary>

	public partial class UserLoginTracking: INotifyPropertyChanging, INotifyPropertyChanged
	{
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
	    
	    public UserLoginTracking(){
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
		
        partial void OnLoginMethodChanging(string value);
        partial void OnLoginMethodChanged();
		
		private string _LoginMethod;
		public string LoginMethod { 
		    get{
		        return _LoginMethod;
		    } 
		    set{
		        this.OnLoginMethodChanging(value);
                this.SendPropertyChanging();
                this._LoginMethod = value;
                this.SendPropertyChanged("LoginMethod");
                this.OnLoginMethodChanged();
		    }
		}
		
        partial void OnDateTimeChanging(DateTime? value);
        partial void OnDateTimeChanged();
		
		private DateTime? _DateTime;
		public DateTime? DateTime { 
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
		

        #endregion

        #region Foreign Keys
        public IQueryable<User> Users
        {
            get
            {
                  var db=new Epilogger.Data.EpiloggerDB();
                  return from items in db.Users
                       where items.ID == _UserId
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
    /// A class which represents the UserAuthenticationProfile table in the Epilogger Database.
    /// This class is queryable through EpiloggerDB.UserAuthenticationProfile 
    /// </summary>

	public partial class UserAuthenticationProfile: INotifyPropertyChanging, INotifyPropertyChanged
	{
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
	    
	    public UserAuthenticationProfile(){
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
		
        partial void OnServiceChanging(string value);
        partial void OnServiceChanged();
		
		private string _Service;
		public string Service { 
		    get{
		        return _Service;
		    } 
		    set{
		        this.OnServiceChanging(value);
                this.SendPropertyChanging();
                this._Service = value;
                this.SendPropertyChanged("Service");
                this.OnServiceChanged();
		    }
		}
		
        partial void OnPlatformChanging(string value);
        partial void OnPlatformChanged();
		
		private string _Platform;
		public string Platform { 
		    get{
		        return _Platform;
		    } 
		    set{
		        this.OnPlatformChanging(value);
                this.SendPropertyChanging();
                this._Platform = value;
                this.SendPropertyChanged("Platform");
                this.OnPlatformChanged();
		    }
		}
		
        partial void OnServiceUsernameChanging(string value);
        partial void OnServiceUsernameChanged();
		
		private string _ServiceUsername;
		public string ServiceUsername { 
		    get{
		        return _ServiceUsername;
		    } 
		    set{
		        this.OnServiceUsernameChanging(value);
                this.SendPropertyChanging();
                this._ServiceUsername = value;
                this.SendPropertyChanged("ServiceUsername");
                this.OnServiceUsernameChanged();
		    }
		}
		
        partial void OnTokenChanging(string value);
        partial void OnTokenChanged();
		
		private string _Token;
		public string Token { 
		    get{
		        return _Token;
		    } 
		    set{
		        this.OnTokenChanging(value);
                this.SendPropertyChanging();
                this._Token = value;
                this.SendPropertyChanged("Token");
                this.OnTokenChanged();
		    }
		}
		
        partial void OnTokenSecretChanging(string value);
        partial void OnTokenSecretChanged();
		
		private string _TokenSecret;
		public string TokenSecret { 
		    get{
		        return _TokenSecret;
		    } 
		    set{
		        this.OnTokenSecretChanging(value);
                this.SendPropertyChanging();
                this._TokenSecret = value;
                this.SendPropertyChanged("TokenSecret");
                this.OnTokenSecretChanged();
		    }
		}
		

        #endregion

        #region Foreign Keys
        public IQueryable<User> Users
        {
            get
            {
                  var db=new Epilogger.Data.EpiloggerDB();
                  return from items in db.Users
                       where items.ID == _UserID
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
    /// A class which represents the MemoryBoxItems table in the Epilogger Database.
    /// This class is queryable through EpiloggerDB.MemoryBoxItem 
    /// </summary>

	public partial class MemoryBoxItem: INotifyPropertyChanging, INotifyPropertyChanged
	{
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
	    
	    public MemoryBoxItem(){
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
		
        partial void OnMemboxIdChanging(int value);
        partial void OnMemboxIdChanged();
		
		private int _MemboxId;
		public int MemboxId { 
		    get{
		        return _MemboxId;
		    } 
		    set{
		        this.OnMemboxIdChanging(value);
                this.SendPropertyChanging();
                this._MemboxId = value;
                this.SendPropertyChanged("MemboxId");
                this.OnMemboxIdChanged();
		    }
		}
		
        partial void OnEventIdChanging(int value);
        partial void OnEventIdChanged();
		
		private int _EventId;
		public int EventId { 
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
		
        partial void OnItemTypeChanging(string value);
        partial void OnItemTypeChanged();
		
		private string _ItemType;
		public string ItemType { 
		    get{
		        return _ItemType;
		    } 
		    set{
		        this.OnItemTypeChanging(value);
                this.SendPropertyChanging();
                this._ItemType = value;
                this.SendPropertyChanged("ItemType");
                this.OnItemTypeChanged();
		    }
		}
		
        partial void OnItemIdChanging(string value);
        partial void OnItemIdChanged();
		
		private string _ItemId;
		public string ItemId { 
		    get{
		        return _ItemId;
		    } 
		    set{
		        this.OnItemIdChanging(value);
                this.SendPropertyChanging();
                this._ItemId = value;
                this.SendPropertyChanged("ItemId");
                this.OnItemIdChanged();
		    }
		}
		
        partial void OnAddedDateTimeChanging(DateTime? value);
        partial void OnAddedDateTimeChanged();
		
		private DateTime? _AddedDateTime;
		public DateTime? AddedDateTime { 
		    get{
		        return _AddedDateTime;
		    } 
		    set{
		        this.OnAddedDateTimeChanging(value);
                this.SendPropertyChanging();
                this._AddedDateTime = value;
                this.SendPropertyChanged("AddedDateTime");
                this.OnAddedDateTimeChanged();
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
                       where items.ID == _EventId
                       select items;
            }
        }

        public IQueryable<MemoryBox> MemoryBoxes
        {
            get
            {
                  var db=new Epilogger.Data.EpiloggerDB();
                  return from items in db.MemoryBoxes
                       where items.ID == _MemboxId
                       select items;
            }
        }

        public IQueryable<User> Users
        {
            get
            {
                  var db=new Epilogger.Data.EpiloggerDB();
                  return from items in db.Users
                       where items.ID == _UserId
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
    /// A class which represents the PaypalTransactionLog table in the Epilogger Database.
    /// This class is queryable through EpiloggerDB.PaypalTransactionLog 
    /// </summary>

	public partial class PaypalTransactionLog: INotifyPropertyChanging, INotifyPropertyChanged
	{
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
	    
	    public PaypalTransactionLog(){
	        OnCreated();
	    }
	    
	    #region Properties
	    
        partial void OnIdChanging(int value);
        partial void OnIdChanged();
		
		private int _Id;
		public int Id { 
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
		
        partial void OnUniqueIdChanging(Guid value);
        partial void OnUniqueIdChanged();
		
		private Guid _UniqueId;
		public Guid UniqueId { 
		    get{
		        return _UniqueId;
		    } 
		    set{
		        this.OnUniqueIdChanging(value);
                this.SendPropertyChanging();
                this._UniqueId = value;
                this.SendPropertyChanged("UniqueId");
                this.OnUniqueIdChanged();
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
		
        partial void OnEventIdChanging(int value);
        partial void OnEventIdChanged();
		
		private int _EventId;
		public int EventId { 
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
		
        partial void OnPaypalTransactionIdChanging(string value);
        partial void OnPaypalTransactionIdChanged();
		
		private string _PaypalTransactionId;
		public string PaypalTransactionId { 
		    get{
		        return _PaypalTransactionId;
		    } 
		    set{
		        this.OnPaypalTransactionIdChanging(value);
                this.SendPropertyChanging();
                this._PaypalTransactionId = value;
                this.SendPropertyChanged("PaypalTransactionId");
                this.OnPaypalTransactionIdChanged();
		    }
		}
		
        partial void OnStartedDateTimeChanging(DateTime? value);
        partial void OnStartedDateTimeChanged();
		
		private DateTime? _StartedDateTime;
		public DateTime? StartedDateTime { 
		    get{
		        return _StartedDateTime;
		    } 
		    set{
		        this.OnStartedDateTimeChanging(value);
                this.SendPropertyChanging();
                this._StartedDateTime = value;
                this.SendPropertyChanged("StartedDateTime");
                this.OnStartedDateTimeChanged();
		    }
		}
		
        partial void OnCompletedDateTimeChanging(DateTime? value);
        partial void OnCompletedDateTimeChanged();
		
		private DateTime? _CompletedDateTime;
		public DateTime? CompletedDateTime { 
		    get{
		        return _CompletedDateTime;
		    } 
		    set{
		        this.OnCompletedDateTimeChanging(value);
                this.SendPropertyChanging();
                this._CompletedDateTime = value;
                this.SendPropertyChanged("CompletedDateTime");
                this.OnCompletedDateTimeChanged();
		    }
		}
		
        partial void OnPricePaidBeforeTaxChanging(int? value);
        partial void OnPricePaidBeforeTaxChanged();
		
		private int? _PricePaidBeforeTax;
		public int? PricePaidBeforeTax { 
		    get{
		        return _PricePaidBeforeTax;
		    } 
		    set{
		        this.OnPricePaidBeforeTaxChanging(value);
                this.SendPropertyChanging();
                this._PricePaidBeforeTax = value;
                this.SendPropertyChanged("PricePaidBeforeTax");
                this.OnPricePaidBeforeTaxChanged();
		    }
		}
		
        partial void OnIsPrivateEventChanging(bool value);
        partial void OnIsPrivateEventChanged();
		
		private bool _IsPrivateEvent;
		public bool IsPrivateEvent { 
		    get{
		        return _IsPrivateEvent;
		    } 
		    set{
		        this.OnIsPrivateEventChanging(value);
                this.SendPropertyChanging();
                this._IsPrivateEvent = value;
                this.SendPropertyChanged("IsPrivateEvent");
                this.OnIsPrivateEventChanged();
		    }
		}
		
        partial void Onmc_grossChanging(string value);
        partial void Onmc_grossChanged();
		
		private string _mc_gross;
		public string mc_gross { 
		    get{
		        return _mc_gross;
		    } 
		    set{
		        this.Onmc_grossChanging(value);
                this.SendPropertyChanging();
                this._mc_gross = value;
                this.SendPropertyChanged("mc_gross");
                this.Onmc_grossChanged();
		    }
		}
		
        partial void Onprotection_eligibilityChanging(string value);
        partial void Onprotection_eligibilityChanged();
		
		private string _protection_eligibility;
		public string protection_eligibility { 
		    get{
		        return _protection_eligibility;
		    } 
		    set{
		        this.Onprotection_eligibilityChanging(value);
                this.SendPropertyChanging();
                this._protection_eligibility = value;
                this.SendPropertyChanged("protection_eligibility");
                this.Onprotection_eligibilityChanged();
		    }
		}
		
        partial void Onaddress_statusChanging(string value);
        partial void Onaddress_statusChanged();
		
		private string _address_status;
		public string address_status { 
		    get{
		        return _address_status;
		    } 
		    set{
		        this.Onaddress_statusChanging(value);
                this.SendPropertyChanging();
                this._address_status = value;
                this.SendPropertyChanged("address_status");
                this.Onaddress_statusChanged();
		    }
		}
		
        partial void Onpayer_idChanging(string value);
        partial void Onpayer_idChanged();
		
		private string _payer_id;
		public string payer_id { 
		    get{
		        return _payer_id;
		    } 
		    set{
		        this.Onpayer_idChanging(value);
                this.SendPropertyChanging();
                this._payer_id = value;
                this.SendPropertyChanged("payer_id");
                this.Onpayer_idChanged();
		    }
		}
		
        partial void OntaxChanging(string value);
        partial void OntaxChanged();
		
		private string _tax;
		public string tax { 
		    get{
		        return _tax;
		    } 
		    set{
		        this.OntaxChanging(value);
                this.SendPropertyChanging();
                this._tax = value;
                this.SendPropertyChanged("tax");
                this.OntaxChanged();
		    }
		}
		
        partial void Onaddress_streetChanging(string value);
        partial void Onaddress_streetChanged();
		
		private string _address_street;
		public string address_street { 
		    get{
		        return _address_street;
		    } 
		    set{
		        this.Onaddress_streetChanging(value);
                this.SendPropertyChanging();
                this._address_street = value;
                this.SendPropertyChanged("address_street");
                this.Onaddress_streetChanged();
		    }
		}
		
        partial void Onpayment_dateChanging(string value);
        partial void Onpayment_dateChanged();
		
		private string _payment_date;
		public string payment_date { 
		    get{
		        return _payment_date;
		    } 
		    set{
		        this.Onpayment_dateChanging(value);
                this.SendPropertyChanging();
                this._payment_date = value;
                this.SendPropertyChanged("payment_date");
                this.Onpayment_dateChanged();
		    }
		}
		
        partial void Onpayment_statusChanging(string value);
        partial void Onpayment_statusChanged();
		
		private string _payment_status;
		public string payment_status { 
		    get{
		        return _payment_status;
		    } 
		    set{
		        this.Onpayment_statusChanging(value);
                this.SendPropertyChanging();
                this._payment_status = value;
                this.SendPropertyChanged("payment_status");
                this.Onpayment_statusChanged();
		    }
		}
		
        partial void OncharsetChanging(string value);
        partial void OncharsetChanged();
		
		private string _charset;
		public string charset { 
		    get{
		        return _charset;
		    } 
		    set{
		        this.OncharsetChanging(value);
                this.SendPropertyChanging();
                this._charset = value;
                this.SendPropertyChanged("charset");
                this.OncharsetChanged();
		    }
		}
		
        partial void Onaddress_zipChanging(string value);
        partial void Onaddress_zipChanged();
		
		private string _address_zip;
		public string address_zip { 
		    get{
		        return _address_zip;
		    } 
		    set{
		        this.Onaddress_zipChanging(value);
                this.SendPropertyChanging();
                this._address_zip = value;
                this.SendPropertyChanged("address_zip");
                this.Onaddress_zipChanged();
		    }
		}
		
        partial void Onfirst_nameChanging(string value);
        partial void Onfirst_nameChanged();
		
		private string _first_name;
		public string first_name { 
		    get{
		        return _first_name;
		    } 
		    set{
		        this.Onfirst_nameChanging(value);
                this.SendPropertyChanging();
                this._first_name = value;
                this.SendPropertyChanged("first_name");
                this.Onfirst_nameChanged();
		    }
		}
		
        partial void Onmc_feeChanging(string value);
        partial void Onmc_feeChanged();
		
		private string _mc_fee;
		public string mc_fee { 
		    get{
		        return _mc_fee;
		    } 
		    set{
		        this.Onmc_feeChanging(value);
                this.SendPropertyChanging();
                this._mc_fee = value;
                this.SendPropertyChanged("mc_fee");
                this.Onmc_feeChanged();
		    }
		}
		
        partial void Onaddress_country_codeChanging(string value);
        partial void Onaddress_country_codeChanged();
		
		private string _address_country_code;
		public string address_country_code { 
		    get{
		        return _address_country_code;
		    } 
		    set{
		        this.Onaddress_country_codeChanging(value);
                this.SendPropertyChanging();
                this._address_country_code = value;
                this.SendPropertyChanged("address_country_code");
                this.Onaddress_country_codeChanged();
		    }
		}
		
        partial void Onaddress_nameChanging(string value);
        partial void Onaddress_nameChanged();
		
		private string _address_name;
		public string address_name { 
		    get{
		        return _address_name;
		    } 
		    set{
		        this.Onaddress_nameChanging(value);
                this.SendPropertyChanging();
                this._address_name = value;
                this.SendPropertyChanged("address_name");
                this.Onaddress_nameChanged();
		    }
		}
		
        partial void Onnotify_versionChanging(string value);
        partial void Onnotify_versionChanged();
		
		private string _notify_version;
		public string notify_version { 
		    get{
		        return _notify_version;
		    } 
		    set{
		        this.Onnotify_versionChanging(value);
                this.SendPropertyChanging();
                this._notify_version = value;
                this.SendPropertyChanged("notify_version");
                this.Onnotify_versionChanged();
		    }
		}
		
        partial void OncustomChanging(string value);
        partial void OncustomChanged();
		
		private string _custom;
		public string custom { 
		    get{
		        return _custom;
		    } 
		    set{
		        this.OncustomChanging(value);
                this.SendPropertyChanging();
                this._custom = value;
                this.SendPropertyChanged("custom");
                this.OncustomChanged();
		    }
		}
		
        partial void Onpayer_statusChanging(string value);
        partial void Onpayer_statusChanged();
		
		private string _payer_status;
		public string payer_status { 
		    get{
		        return _payer_status;
		    } 
		    set{
		        this.Onpayer_statusChanging(value);
                this.SendPropertyChanging();
                this._payer_status = value;
                this.SendPropertyChanged("payer_status");
                this.Onpayer_statusChanged();
		    }
		}
		
        partial void OnbusinessChanging(string value);
        partial void OnbusinessChanged();
		
		private string _business;
		public string business { 
		    get{
		        return _business;
		    } 
		    set{
		        this.OnbusinessChanging(value);
                this.SendPropertyChanging();
                this._business = value;
                this.SendPropertyChanged("business");
                this.OnbusinessChanged();
		    }
		}
		
        partial void Onaddress_countryChanging(string value);
        partial void Onaddress_countryChanged();
		
		private string _address_country;
		public string address_country { 
		    get{
		        return _address_country;
		    } 
		    set{
		        this.Onaddress_countryChanging(value);
                this.SendPropertyChanging();
                this._address_country = value;
                this.SendPropertyChanged("address_country");
                this.Onaddress_countryChanged();
		    }
		}
		
        partial void Onaddress_cityChanging(string value);
        partial void Onaddress_cityChanged();
		
		private string _address_city;
		public string address_city { 
		    get{
		        return _address_city;
		    } 
		    set{
		        this.Onaddress_cityChanging(value);
                this.SendPropertyChanging();
                this._address_city = value;
                this.SendPropertyChanged("address_city");
                this.Onaddress_cityChanged();
		    }
		}
		
        partial void OnquantityChanging(string value);
        partial void OnquantityChanged();
		
		private string _quantity;
		public string quantity { 
		    get{
		        return _quantity;
		    } 
		    set{
		        this.OnquantityChanging(value);
                this.SendPropertyChanging();
                this._quantity = value;
                this.SendPropertyChanged("quantity");
                this.OnquantityChanged();
		    }
		}
		
        partial void Onpayer_emailChanging(string value);
        partial void Onpayer_emailChanged();
		
		private string _payer_email;
		public string payer_email { 
		    get{
		        return _payer_email;
		    } 
		    set{
		        this.Onpayer_emailChanging(value);
                this.SendPropertyChanging();
                this._payer_email = value;
                this.SendPropertyChanged("payer_email");
                this.Onpayer_emailChanged();
		    }
		}
		
        partial void Onverify_signChanging(string value);
        partial void Onverify_signChanged();
		
		private string _verify_sign;
		public string verify_sign { 
		    get{
		        return _verify_sign;
		    } 
		    set{
		        this.Onverify_signChanging(value);
                this.SendPropertyChanging();
                this._verify_sign = value;
                this.SendPropertyChanged("verify_sign");
                this.Onverify_signChanged();
		    }
		}
		
        partial void Ontxn_idChanging(string value);
        partial void Ontxn_idChanged();
		
		private string _txn_id;
		public string txn_id { 
		    get{
		        return _txn_id;
		    } 
		    set{
		        this.Ontxn_idChanging(value);
                this.SendPropertyChanging();
                this._txn_id = value;
                this.SendPropertyChanged("txn_id");
                this.Ontxn_idChanged();
		    }
		}
		
        partial void Onpayment_typeChanging(string value);
        partial void Onpayment_typeChanged();
		
		private string _payment_type;
		public string payment_type { 
		    get{
		        return _payment_type;
		    } 
		    set{
		        this.Onpayment_typeChanging(value);
                this.SendPropertyChanging();
                this._payment_type = value;
                this.SendPropertyChanged("payment_type");
                this.Onpayment_typeChanged();
		    }
		}
		
        partial void Onlast_nameChanging(string value);
        partial void Onlast_nameChanged();
		
		private string _last_name;
		public string last_name { 
		    get{
		        return _last_name;
		    } 
		    set{
		        this.Onlast_nameChanging(value);
                this.SendPropertyChanging();
                this._last_name = value;
                this.SendPropertyChanged("last_name");
                this.Onlast_nameChanged();
		    }
		}
		
        partial void Onaddress_stateChanging(string value);
        partial void Onaddress_stateChanged();
		
		private string _address_state;
		public string address_state { 
		    get{
		        return _address_state;
		    } 
		    set{
		        this.Onaddress_stateChanging(value);
                this.SendPropertyChanging();
                this._address_state = value;
                this.SendPropertyChanged("address_state");
                this.Onaddress_stateChanged();
		    }
		}
		
        partial void Onreceiver_emailChanging(string value);
        partial void Onreceiver_emailChanged();
		
		private string _receiver_email;
		public string receiver_email { 
		    get{
		        return _receiver_email;
		    } 
		    set{
		        this.Onreceiver_emailChanging(value);
                this.SendPropertyChanging();
                this._receiver_email = value;
                this.SendPropertyChanged("receiver_email");
                this.Onreceiver_emailChanged();
		    }
		}
		
        partial void Onpayment_feeChanging(string value);
        partial void Onpayment_feeChanged();
		
		private string _payment_fee;
		public string payment_fee { 
		    get{
		        return _payment_fee;
		    } 
		    set{
		        this.Onpayment_feeChanging(value);
                this.SendPropertyChanging();
                this._payment_fee = value;
                this.SendPropertyChanged("payment_fee");
                this.Onpayment_feeChanged();
		    }
		}
		
        partial void Onreceiver_idChanging(string value);
        partial void Onreceiver_idChanged();
		
		private string _receiver_id;
		public string receiver_id { 
		    get{
		        return _receiver_id;
		    } 
		    set{
		        this.Onreceiver_idChanging(value);
                this.SendPropertyChanging();
                this._receiver_id = value;
                this.SendPropertyChanged("receiver_id");
                this.Onreceiver_idChanged();
		    }
		}
		
        partial void Ontxn_typeChanging(string value);
        partial void Ontxn_typeChanged();
		
		private string _txn_type;
		public string txn_type { 
		    get{
		        return _txn_type;
		    } 
		    set{
		        this.Ontxn_typeChanging(value);
                this.SendPropertyChanging();
                this._txn_type = value;
                this.SendPropertyChanged("txn_type");
                this.Ontxn_typeChanged();
		    }
		}
		
        partial void Onitem_nameChanging(string value);
        partial void Onitem_nameChanged();
		
		private string _item_name;
		public string item_name { 
		    get{
		        return _item_name;
		    } 
		    set{
		        this.Onitem_nameChanging(value);
                this.SendPropertyChanging();
                this._item_name = value;
                this.SendPropertyChanged("item_name");
                this.Onitem_nameChanged();
		    }
		}
		
        partial void Onmc_currencyChanging(string value);
        partial void Onmc_currencyChanged();
		
		private string _mc_currency;
		public string mc_currency { 
		    get{
		        return _mc_currency;
		    } 
		    set{
		        this.Onmc_currencyChanging(value);
                this.SendPropertyChanging();
                this._mc_currency = value;
                this.SendPropertyChanged("mc_currency");
                this.Onmc_currencyChanged();
		    }
		}
		
        partial void Onitem_numberChanging(string value);
        partial void Onitem_numberChanged();
		
		private string _item_number;
		public string item_number { 
		    get{
		        return _item_number;
		    } 
		    set{
		        this.Onitem_numberChanging(value);
                this.SendPropertyChanging();
                this._item_number = value;
                this.SendPropertyChanged("item_number");
                this.Onitem_numberChanged();
		    }
		}
		
        partial void Onresidence_countryChanging(string value);
        partial void Onresidence_countryChanged();
		
		private string _residence_country;
		public string residence_country { 
		    get{
		        return _residence_country;
		    } 
		    set{
		        this.Onresidence_countryChanging(value);
                this.SendPropertyChanging();
                this._residence_country = value;
                this.SendPropertyChanged("residence_country");
                this.Onresidence_countryChanged();
		    }
		}
		
        partial void Ontest_ipnChanging(string value);
        partial void Ontest_ipnChanged();
		
		private string _test_ipn;
		public string test_ipn { 
		    get{
		        return _test_ipn;
		    } 
		    set{
		        this.Ontest_ipnChanging(value);
                this.SendPropertyChanging();
                this._test_ipn = value;
                this.SendPropertyChanged("test_ipn");
                this.Ontest_ipnChanged();
		    }
		}
		
        partial void Onhandling_amountChanging(string value);
        partial void Onhandling_amountChanged();
		
		private string _handling_amount;
		public string handling_amount { 
		    get{
		        return _handling_amount;
		    } 
		    set{
		        this.Onhandling_amountChanging(value);
                this.SendPropertyChanging();
                this._handling_amount = value;
                this.SendPropertyChanged("handling_amount");
                this.Onhandling_amountChanged();
		    }
		}
		
        partial void Ontransaction_subjectChanging(string value);
        partial void Ontransaction_subjectChanged();
		
		private string _transaction_subject;
		public string transaction_subject { 
		    get{
		        return _transaction_subject;
		    } 
		    set{
		        this.Ontransaction_subjectChanging(value);
                this.SendPropertyChanging();
                this._transaction_subject = value;
                this.SendPropertyChanged("transaction_subject");
                this.Ontransaction_subjectChanged();
		    }
		}
		
        partial void Onpayment_grossChanging(string value);
        partial void Onpayment_grossChanged();
		
		private string _payment_gross;
		public string payment_gross { 
		    get{
		        return _payment_gross;
		    } 
		    set{
		        this.Onpayment_grossChanging(value);
                this.SendPropertyChanging();
                this._payment_gross = value;
                this.SendPropertyChanged("payment_gross");
                this.Onpayment_grossChanged();
		    }
		}
		
        partial void OnshippingChanging(string value);
        partial void OnshippingChanged();
		
		private string _shipping;
		public string shipping { 
		    get{
		        return _shipping;
		    } 
		    set{
		        this.OnshippingChanging(value);
                this.SendPropertyChanging();
                this._shipping = value;
                this.SendPropertyChanged("shipping");
                this.OnshippingChanged();
		    }
		}
		
        partial void OnauthChanging(string value);
        partial void OnauthChanged();
		
		private string _auth;
		public string auth { 
		    get{
		        return _auth;
		    } 
		    set{
		        this.OnauthChanging(value);
                this.SendPropertyChanging();
                this._auth = value;
                this.SendPropertyChanged("auth");
                this.OnauthChanged();
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
    /// A class which represents the UserFollowsEvent table in the Epilogger Database.
    /// This class is queryable through EpiloggerDB.UserFollowsEvent 
    /// </summary>

	public partial class UserFollowsEvent: INotifyPropertyChanging, INotifyPropertyChanged
	{
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
	    
	    public UserFollowsEvent(){
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
		
        partial void OnTimestampChanging(DateTime value);
        partial void OnTimestampChanged();
		
		private DateTime _Timestamp;
		public DateTime Timestamp { 
		    get{
		        return _Timestamp;
		    } 
		    set{
		        this.OnTimestampChanging(value);
                this.SendPropertyChanging();
                this._Timestamp = value;
                this.SendPropertyChanged("Timestamp");
                this.OnTimestampChanged();
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

        public IQueryable<User> Users
        {
            get
            {
                  var db=new Epilogger.Data.EpiloggerDB();
                  return from items in db.Users
                       where items.ID == _UserID
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
    /// A class which represents the LiveModeCustomSettings table in the Epilogger Database.
    /// This class is queryable through EpiloggerDB.LiveModeCustomSetting 
    /// </summary>

	public partial class LiveModeCustomSetting: INotifyPropertyChanging, INotifyPropertyChanged
	{
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
	    
	    public LiveModeCustomSetting(){
	        OnCreated();
	    }
	    
	    #region Properties
	    
        partial void OnIdChanging(int value);
        partial void OnIdChanged();
		
		private int _Id;
		public int Id { 
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
		
        partial void OnEventIdChanging(int value);
        partial void OnEventIdChanged();
		
		private int _EventId;
		public int EventId { 
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
		
        partial void OnBackgroundChanging(string value);
        partial void OnBackgroundChanged();
		
		private string _Background;
		public string Background { 
		    get{
		        return _Background;
		    } 
		    set{
		        this.OnBackgroundChanging(value);
                this.SendPropertyChanging();
                this._Background = value;
                this.SendPropertyChanged("Background");
                this.OnBackgroundChanged();
		    }
		}
		
        partial void OnFooterTextColorChanging(string value);
        partial void OnFooterTextColorChanged();
		
		private string _FooterTextColor;
		public string FooterTextColor { 
		    get{
		        return _FooterTextColor;
		    } 
		    set{
		        this.OnFooterTextColorChanging(value);
                this.SendPropertyChanging();
                this._FooterTextColor = value;
                this.SendPropertyChanged("FooterTextColor");
                this.OnFooterTextColorChanged();
		    }
		}
		
        partial void OnLinkColourChanging(string value);
        partial void OnLinkColourChanged();
		
		private string _LinkColour;
		public string LinkColour { 
		    get{
		        return _LinkColour;
		    } 
		    set{
		        this.OnLinkColourChanging(value);
                this.SendPropertyChanging();
                this._LinkColour = value;
                this.SendPropertyChanged("LinkColour");
                this.OnLinkColourChanged();
		    }
		}
		
        partial void OnTwitterUserNameColourChanging(string value);
        partial void OnTwitterUserNameColourChanged();
		
		private string _TwitterUserNameColour;
		public string TwitterUserNameColour { 
		    get{
		        return _TwitterUserNameColour;
		    } 
		    set{
		        this.OnTwitterUserNameColourChanging(value);
                this.SendPropertyChanging();
                this._TwitterUserNameColour = value;
                this.SendPropertyChanged("TwitterUserNameColour");
                this.OnTwitterUserNameColourChanged();
		    }
		}
		
        partial void OnLogoChanging(string value);
        partial void OnLogoChanged();
		
		private string _Logo;
		public string Logo { 
		    get{
		        return _Logo;
		    } 
		    set{
		        this.OnLogoChanging(value);
                this.SendPropertyChanging();
                this._Logo = value;
                this.SendPropertyChanged("Logo");
                this.OnLogoChanged();
		    }
		}
		
        partial void OnSponsorLogosChanging(string value);
        partial void OnSponsorLogosChanged();
		
		private string _SponsorLogos;
		public string SponsorLogos { 
		    get{
		        return _SponsorLogos;
		    } 
		    set{
		        this.OnSponsorLogosChanging(value);
                this.SendPropertyChanging();
                this._SponsorLogos = value;
                this.SendPropertyChanged("SponsorLogos");
                this.OnSponsorLogosChanged();
		    }
		}
		
        partial void OnLightThemeChanging(bool? value);
        partial void OnLightThemeChanged();
		
		private bool? _LightTheme;
		public bool? LightTheme { 
		    get{
		        return _LightTheme;
		    } 
		    set{
		        this.OnLightThemeChanging(value);
                this.SendPropertyChanging();
                this._LightTheme = value;
                this.SendPropertyChanged("LightTheme");
                this.OnLightThemeChanged();
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
                       where items.ID == _EventId
                       select items;
            }
        }

        public IQueryable<SponsorImage> SponsorImages
        {
            get
            {
                  var db=new Epilogger.Data.EpiloggerDB();
                  return from items in db.SponsorImages
                       where items.LiveModeID == _Id
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
    /// A class which represents the StatusMessages table in the Epilogger Database.
    /// This class is queryable through EpiloggerDB.StatusMessage 
    /// </summary>

	public partial class StatusMessage: INotifyPropertyChanging, INotifyPropertyChanged
	{
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
	    
	    public StatusMessage(){
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
		
        partial void OnMSGDateTimeChanging(DateTime? value);
        partial void OnMSGDateTimeChanged();
		
		private DateTime? _MSGDateTime;
		public DateTime? MSGDateTime { 
		    get{
		        return _MSGDateTime;
		    } 
		    set{
		        this.OnMSGDateTimeChanging(value);
                this.SendPropertyChanging();
                this._MSGDateTime = value;
                this.SendPropertyChanged("MSGDateTime");
                this.OnMSGDateTimeChanged();
		    }
		}
		
        partial void OnMSGChanging(string value);
        partial void OnMSGChanged();
		
		private string _MSG;
		public string MSG { 
		    get{
		        return _MSG;
		    } 
		    set{
		        this.OnMSGChanging(value);
                this.SendPropertyChanging();
                this._MSG = value;
                this.SendPropertyChanged("MSG");
                this.OnMSGChanged();
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
    /// A class which represents the UserClickActions table in the Epilogger Database.
    /// This class is queryable through EpiloggerDB.UserClickAction 
    /// </summary>

	public partial class UserClickAction: INotifyPropertyChanging, INotifyPropertyChanged
	{
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
	    
	    public UserClickAction(){
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
		
        partial void OnActionDateTimeChanging(DateTime? value);
        partial void OnActionDateTimeChanged();
		
		private DateTime? _ActionDateTime;
		public DateTime? ActionDateTime { 
		    get{
		        return _ActionDateTime;
		    } 
		    set{
		        this.OnActionDateTimeChanging(value);
                this.SendPropertyChanging();
                this._ActionDateTime = value;
                this.SendPropertyChanged("ActionDateTime");
                this.OnActionDateTimeChanged();
		    }
		}
		
        partial void OnhrefChanging(string value);
        partial void OnhrefChanged();
		
		private string _href;
		public string href { 
		    get{
		        return _href;
		    } 
		    set{
		        this.OnhrefChanging(value);
                this.SendPropertyChanging();
                this._href = value;
                this.SendPropertyChanged("href");
                this.OnhrefChanged();
		    }
		}
		
        partial void OnHostChanging(string value);
        partial void OnHostChanged();
		
		private string _Host;
		public string Host { 
		    get{
		        return _Host;
		    } 
		    set{
		        this.OnHostChanging(value);
                this.SendPropertyChanging();
                this._Host = value;
                this.SendPropertyChanged("Host");
                this.OnHostChanged();
		    }
		}
		
        partial void OnPathNameChanging(string value);
        partial void OnPathNameChanged();
		
		private string _PathName;
		public string PathName { 
		    get{
		        return _PathName;
		    } 
		    set{
		        this.OnPathNameChanging(value);
                this.SendPropertyChanging();
                this._PathName = value;
                this.SendPropertyChanged("PathName");
                this.OnPathNameChanged();
		    }
		}
		
        partial void OnUserAgentChanging(string value);
        partial void OnUserAgentChanged();
		
		private string _UserAgent;
		public string UserAgent { 
		    get{
		        return _UserAgent;
		    } 
		    set{
		        this.OnUserAgentChanging(value);
                this.SendPropertyChanging();
                this._UserAgent = value;
                this.SendPropertyChanged("UserAgent");
                this.OnUserAgentChanged();
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
    /// A class which represents the CollectionQueueSettings table in the Epilogger Database.
    /// This class is queryable through EpiloggerDB.CollectionQueueSetting 
    /// </summary>

	public partial class CollectionQueueSetting: INotifyPropertyChanging, INotifyPropertyChanged
	{
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
	    
	    public CollectionQueueSetting(){
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
		
        partial void OnUpdateEventsModeTimeChanging(int value);
        partial void OnUpdateEventsModeTimeChanged();
		
		private int _UpdateEventsModeTime;
		public int UpdateEventsModeTime { 
		    get{
		        return _UpdateEventsModeTime;
		    } 
		    set{
		        this.OnUpdateEventsModeTimeChanging(value);
                this.SendPropertyChanging();
                this._UpdateEventsModeTime = value;
                this.SendPropertyChanged("UpdateEventsModeTime");
                this.OnUpdateEventsModeTimeChanged();
		    }
		}
		
        partial void OnBeforeEventPollingTimeChanging(int value);
        partial void OnBeforeEventPollingTimeChanged();
		
		private int _BeforeEventPollingTime;
		public int BeforeEventPollingTime { 
		    get{
		        return _BeforeEventPollingTime;
		    } 
		    set{
		        this.OnBeforeEventPollingTimeChanging(value);
                this.SendPropertyChanging();
                this._BeforeEventPollingTime = value;
                this.SendPropertyChanged("BeforeEventPollingTime");
                this.OnBeforeEventPollingTimeChanged();
		    }
		}
		
        partial void OnDuringEventPollingTimeChanging(int value);
        partial void OnDuringEventPollingTimeChanged();
		
		private int _DuringEventPollingTime;
		public int DuringEventPollingTime { 
		    get{
		        return _DuringEventPollingTime;
		    } 
		    set{
		        this.OnDuringEventPollingTimeChanging(value);
                this.SendPropertyChanging();
                this._DuringEventPollingTime = value;
                this.SendPropertyChanged("DuringEventPollingTime");
                this.OnDuringEventPollingTimeChanged();
		    }
		}
		
        partial void OnAfterEventPollingTimeChanging(int value);
        partial void OnAfterEventPollingTimeChanged();
		
		private int _AfterEventPollingTime;
		public int AfterEventPollingTime { 
		    get{
		        return _AfterEventPollingTime;
		    } 
		    set{
		        this.OnAfterEventPollingTimeChanging(value);
                this.SendPropertyChanging();
                this._AfterEventPollingTime = value;
                this.SendPropertyChanged("AfterEventPollingTime");
                this.OnAfterEventPollingTimeChanged();
		    }
		}
		
        partial void OnOnGoingEventsPollingTimeChanging(int value);
        partial void OnOnGoingEventsPollingTimeChanged();
		
		private int _OnGoingEventsPollingTime;
		public int OnGoingEventsPollingTime { 
		    get{
		        return _OnGoingEventsPollingTime;
		    } 
		    set{
		        this.OnOnGoingEventsPollingTimeChanging(value);
                this.SendPropertyChanging();
                this._OnGoingEventsPollingTime = value;
                this.SendPropertyChanged("OnGoingEventsPollingTime");
                this.OnOnGoingEventsPollingTimeChanged();
		    }
		}
		
        partial void OnFullEventPollingTimeChanging(int value);
        partial void OnFullEventPollingTimeChanged();
		
		private int _FullEventPollingTime;
		public int FullEventPollingTime { 
		    get{
		        return _FullEventPollingTime;
		    } 
		    set{
		        this.OnFullEventPollingTimeChanging(value);
                this.SendPropertyChanging();
                this._FullEventPollingTime = value;
                this.SendPropertyChanged("FullEventPollingTime");
                this.OnFullEventPollingTimeChanged();
		    }
		}
		
        partial void OnWorkerCollectionThreadsChanging(int value);
        partial void OnWorkerCollectionThreadsChanged();
		
		private int _WorkerCollectionThreads;
		public int WorkerCollectionThreads { 
		    get{
		        return _WorkerCollectionThreads;
		    } 
		    set{
		        this.OnWorkerCollectionThreadsChanging(value);
                this.SendPropertyChanging();
                this._WorkerCollectionThreads = value;
                this.SendPropertyChanged("WorkerCollectionThreads");
                this.OnWorkerCollectionThreadsChanged();
		    }
		}
		
        partial void OnWorkerImageThreadsChanging(int value);
        partial void OnWorkerImageThreadsChanged();
		
		private int _WorkerImageThreads;
		public int WorkerImageThreads { 
		    get{
		        return _WorkerImageThreads;
		    } 
		    set{
		        this.OnWorkerImageThreadsChanging(value);
                this.SendPropertyChanging();
                this._WorkerImageThreads = value;
                this.SendPropertyChanged("WorkerImageThreads");
                this.OnWorkerImageThreadsChanged();
		    }
		}
		
        partial void OnWorkerLinkThreadsChanging(int value);
        partial void OnWorkerLinkThreadsChanged();
		
		private int _WorkerLinkThreads;
		public int WorkerLinkThreads { 
		    get{
		        return _WorkerLinkThreads;
		    } 
		    set{
		        this.OnWorkerLinkThreadsChanging(value);
                this.SendPropertyChanging();
                this._WorkerLinkThreads = value;
                this.SendPropertyChanged("WorkerLinkThreads");
                this.OnWorkerLinkThreadsChanged();
		    }
		}
		
        partial void OnWorkeSystemThreadsChanging(int value);
        partial void OnWorkeSystemThreadsChanged();
		
		private int _WorkeSystemThreads;
		public int WorkeSystemThreads { 
		    get{
		        return _WorkeSystemThreads;
		    } 
		    set{
		        this.OnWorkeSystemThreadsChanging(value);
                this.SendPropertyChanging();
                this._WorkeSystemThreads = value;
                this.SendPropertyChanged("WorkeSystemThreads");
                this.OnWorkeSystemThreadsChanged();
		    }
		}
		
        partial void OnWorkerTweetsThreadsChanging(int value);
        partial void OnWorkerTweetsThreadsChanged();
		
		private int _WorkerTweetsThreads;
		public int WorkerTweetsThreads { 
		    get{
		        return _WorkerTweetsThreads;
		    } 
		    set{
		        this.OnWorkerTweetsThreadsChanging(value);
                this.SendPropertyChanging();
                this._WorkerTweetsThreads = value;
                this.SendPropertyChanged("WorkerTweetsThreads");
                this.OnWorkerTweetsThreadsChanged();
		    }
		}
		
        partial void OnCalculateTrendingEventsUpdateIntervalChanging(int value);
        partial void OnCalculateTrendingEventsUpdateIntervalChanged();
		
		private int _CalculateTrendingEventsUpdateInterval;
		public int CalculateTrendingEventsUpdateInterval { 
		    get{
		        return _CalculateTrendingEventsUpdateInterval;
		    } 
		    set{
		        this.OnCalculateTrendingEventsUpdateIntervalChanging(value);
                this.SendPropertyChanging();
                this._CalculateTrendingEventsUpdateInterval = value;
                this.SendPropertyChanged("CalculateTrendingEventsUpdateInterval");
                this.OnCalculateTrendingEventsUpdateIntervalChanged();
		    }
		}
		
        partial void OnMinCollectionRateChanging(int value);
        partial void OnMinCollectionRateChanged();
		
		private int _MinCollectionRate;
		public int MinCollectionRate { 
		    get{
		        return _MinCollectionRate;
		    } 
		    set{
		        this.OnMinCollectionRateChanging(value);
                this.SendPropertyChanging();
                this._MinCollectionRate = value;
                this.SendPropertyChanged("MinCollectionRate");
                this.OnMinCollectionRateChanged();
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
    /// A class which represents the CheckIns table in the Epilogger Database.
    /// This class is queryable through EpiloggerDB.CheckIn 
    /// </summary>

	public partial class CheckIn: INotifyPropertyChanging, INotifyPropertyChanged
	{
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
	    
	    public CheckIn(){
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
		
        partial void OnTweetIDChanging(long? value);
        partial void OnTweetIDChanged();
		
		private long? _TweetID;
		public long? TweetID { 
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
		
        partial void OnUserIDChanging(int? value);
        partial void OnUserIDChanged();
		
		private int? _UserID;
		public int? UserID { 
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
		
        partial void OnCheckInDateTimeChanging(DateTime value);
        partial void OnCheckInDateTimeChanged();
		
		private DateTime _CheckInDateTime;
		public DateTime CheckInDateTime { 
		    get{
		        return _CheckInDateTime;
		    } 
		    set{
		        this.OnCheckInDateTimeChanging(value);
                this.SendPropertyChanging();
                this._CheckInDateTime = value;
                this.SendPropertyChanged("CheckInDateTime");
                this.OnCheckInDateTimeChanged();
		    }
		}
		
        partial void OnFourSquareCheckInURLChanging(string value);
        partial void OnFourSquareCheckInURLChanged();
		
		private string _FourSquareCheckInURL;
		public string FourSquareCheckInURL { 
		    get{
		        return _FourSquareCheckInURL;
		    } 
		    set{
		        this.OnFourSquareCheckInURLChanging(value);
                this.SendPropertyChanging();
                this._FourSquareCheckInURL = value;
                this.SendPropertyChanged("FourSquareCheckInURL");
                this.OnFourSquareCheckInURLChanged();
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
    /// A class which represents the Roles table in the Epilogger Database.
    /// This class is queryable through EpiloggerDB.Role 
    /// </summary>

	public partial class Role: INotifyPropertyChanging, INotifyPropertyChanged
	{
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
	    
	    public Role(){
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
		
        partial void OnRoleXChanging(string value);
        partial void OnRoleXChanged();
		
		private string _RoleX;
		public string RoleX { 
		    get{
		        return _RoleX;
		    } 
		    set{
		        this.OnRoleXChanging(value);
                this.SendPropertyChanging();
                this._RoleX = value;
                this.SendPropertyChanged("RoleX");
                this.OnRoleXChanged();
		    }
		}
		

        #endregion

        #region Foreign Keys
        public IQueryable<UserInRole> UserInRoles
        {
            get
            {
                  var db=new Epilogger.Data.EpiloggerDB();
                  return from items in db.UserInRoles
                       where items.RoleID == _ID
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
    /// A class which represents the userClickTracking table in the Epilogger Database.
    /// This class is queryable through EpiloggerDB.userClickTracking 
    /// </summary>

	public partial class userClickTracking: INotifyPropertyChanging, INotifyPropertyChanged
	{
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
	    
	    public userClickTracking(){
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
		
        partial void OnClickDateTimeChanging(DateTime? value);
        partial void OnClickDateTimeChanged();
		
		private DateTime? _ClickDateTime;
		public DateTime? ClickDateTime { 
		    get{
		        return _ClickDateTime;
		    } 
		    set{
		        this.OnClickDateTimeChanging(value);
                this.SendPropertyChanging();
                this._ClickDateTime = value;
                this.SendPropertyChanged("ClickDateTime");
                this.OnClickDateTimeChanged();
		    }
		}
		
        partial void OnxChanging(short? value);
        partial void OnxChanged();
		
		private short? _x;
		public short? x { 
		    get{
		        return _x;
		    } 
		    set{
		        this.OnxChanging(value);
                this.SendPropertyChanging();
                this._x = value;
                this.SendPropertyChanged("x");
                this.OnxChanged();
		    }
		}
		
        partial void OnyChanging(short? value);
        partial void OnyChanged();
		
		private short? _y;
		public short? y { 
		    get{
		        return _y;
		    } 
		    set{
		        this.OnyChanging(value);
                this.SendPropertyChanging();
                this._y = value;
                this.SendPropertyChanged("y");
                this.OnyChanged();
		    }
		}
		
        partial void OnlocationChanging(string value);
        partial void OnlocationChanged();
		
		private string _location;
		public string location { 
		    get{
		        return _location;
		    } 
		    set{
		        this.OnlocationChanging(value);
                this.SendPropertyChanging();
                this._location = value;
                this.SendPropertyChanged("location");
                this.OnlocationChanged();
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
		
        partial void OnFoursquareVenueIDChanging(string value);
        partial void OnFoursquareVenueIDChanged();
		
		private string _FoursquareVenueID;
		public string FoursquareVenueID { 
		    get{
		        return _FoursquareVenueID;
		    } 
		    set{
		        this.OnFoursquareVenueIDChanging(value);
                this.SendPropertyChanging();
                this._FoursquareVenueID = value;
                this.SendPropertyChanged("FoursquareVenueID");
                this.OnFoursquareVenueIDChanged();
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
                       where items.VenueID == _ID
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
    /// A class which represents the UserInRoles table in the Epilogger Database.
    /// This class is queryable through EpiloggerDB.UserInRole 
    /// </summary>

	public partial class UserInRole: INotifyPropertyChanging, INotifyPropertyChanged
	{
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
	    
	    public UserInRole(){
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
		
        partial void OnRoleIDChanging(int value);
        partial void OnRoleIDChanged();
		
		private int _RoleID;
		public int RoleID { 
		    get{
		        return _RoleID;
		    } 
		    set{
		        this.OnRoleIDChanging(value);
                this.SendPropertyChanging();
                this._RoleID = value;
                this.SendPropertyChanged("RoleID");
                this.OnRoleIDChanged();
		    }
		}
		

        #endregion

        #region Foreign Keys
        public IQueryable<User> Users
        {
            get
            {
                  var db=new Epilogger.Data.EpiloggerDB();
                  return from items in db.Users
                       where items.ID == _UserID
                       select items;
            }
        }

        public IQueryable<Role> Roles
        {
            get
            {
                  var db=new Epilogger.Data.EpiloggerDB();
                  return from items in db.Roles
                       where items.ID == _RoleID
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
		
        partial void OnEventSlugChanging(string value);
        partial void OnEventSlugChanged();
		
		private string _EventSlug;
		public string EventSlug { 
		    get{
		        return _EventSlug;
		    } 
		    set{
		        this.OnEventSlugChanging(value);
                this.SendPropertyChanging();
                this._EventSlug = value;
                this.SendPropertyChanged("EventSlug");
                this.OnEventSlugChanged();
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
		
        partial void OnCreatedDateTimeChanging(DateTime? value);
        partial void OnCreatedDateTimeChanged();
		
		private DateTime? _CreatedDateTime;
		public DateTime? CreatedDateTime { 
		    get{
		        return _CreatedDateTime;
		    } 
		    set{
		        this.OnCreatedDateTimeChanging(value);
                this.SendPropertyChanging();
                this._CreatedDateTime = value;
                this.SendPropertyChanged("CreatedDateTime");
                this.OnCreatedDateTimeChanged();
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
		
        partial void OnStartDateTimeChanging(DateTime value);
        partial void OnStartDateTimeChanged();
		
		private DateTime _StartDateTime;
		public DateTime StartDateTime { 
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
		
        partial void OnCollectionStartDateTimeChanging(DateTime value);
        partial void OnCollectionStartDateTimeChanged();
		
		private DateTime _CollectionStartDateTime;
		public DateTime CollectionStartDateTime { 
		    get{
		        return _CollectionStartDateTime;
		    } 
		    set{
		        this.OnCollectionStartDateTimeChanging(value);
                this.SendPropertyChanging();
                this._CollectionStartDateTime = value;
                this.SendPropertyChanged("CollectionStartDateTime");
                this.OnCollectionStartDateTimeChanged();
		    }
		}
		
        partial void OnCollectionEndDateTimeChanging(DateTime? value);
        partial void OnCollectionEndDateTimeChanged();
		
		private DateTime? _CollectionEndDateTime;
		public DateTime? CollectionEndDateTime { 
		    get{
		        return _CollectionEndDateTime;
		    } 
		    set{
		        this.OnCollectionEndDateTimeChanging(value);
                this.SendPropertyChanging();
                this._CollectionEndDateTime = value;
                this.SendPropertyChanged("CollectionEndDateTime");
                this.OnCollectionEndDateTimeChanged();
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
		
        partial void OnVenueIDChanging(int? value);
        partial void OnVenueIDChanged();
		
		private int? _VenueID;
		public int? VenueID { 
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
		
        partial void OnIsPrivateChanging(bool? value);
        partial void OnIsPrivateChanged();
		
		private bool? _IsPrivate;
		public bool? IsPrivate { 
		    get{
		        return _IsPrivate;
		    } 
		    set{
		        this.OnIsPrivateChanging(value);
                this.SendPropertyChanging();
                this._IsPrivate = value;
                this.SendPropertyChanged("IsPrivate");
                this.OnIsPrivateChanged();
		    }
		}
		
        partial void OnIsAdultChanging(bool? value);
        partial void OnIsAdultChanged();
		
		private bool? _IsAdult;
		public bool? IsAdult { 
		    get{
		        return _IsAdult;
		    } 
		    set{
		        this.OnIsAdultChanging(value);
                this.SendPropertyChanging();
                this._IsAdult = value;
                this.SendPropertyChanged("IsAdult");
                this.OnIsAdultChanged();
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
		
        partial void OnEventStatusChanging(string value);
        partial void OnEventStatusChanged();
		
		private string _EventStatus;
		public string EventStatus { 
		    get{
		        return _EventStatus;
		    } 
		    set{
		        this.OnEventStatusChanging(value);
                this.SendPropertyChanging();
                this._EventStatus = value;
                this.SendPropertyChanged("EventStatus");
                this.OnEventStatusChanged();
		    }
		}
		
        partial void OnFacebookPageURLChanging(string value);
        partial void OnFacebookPageURLChanged();
		
		private string _FacebookPageURL;
		public string FacebookPageURL { 
		    get{
		        return _FacebookPageURL;
		    } 
		    set{
		        this.OnFacebookPageURLChanging(value);
                this.SendPropertyChanging();
                this._FacebookPageURL = value;
                this.SendPropertyChanged("FacebookPageURL");
                this.OnFacebookPageURLChanged();
		    }
		}
		
        partial void OnTwitterAccountChanging(string value);
        partial void OnTwitterAccountChanged();
		
		private string _TwitterAccount;
		public string TwitterAccount { 
		    get{
		        return _TwitterAccount;
		    } 
		    set{
		        this.OnTwitterAccountChanging(value);
                this.SendPropertyChanging();
                this._TwitterAccount = value;
                this.SendPropertyChanged("TwitterAccount");
                this.OnTwitterAccountChanged();
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
		
        partial void OnIsFeaturedChanging(bool? value);
        partial void OnIsFeaturedChanged();
		
		private bool? _IsFeatured;
		public bool? IsFeatured { 
		    get{
		        return _IsFeatured;
		    } 
		    set{
		        this.OnIsFeaturedChanging(value);
                this.SendPropertyChanging();
                this._IsFeatured = value;
                this.SendPropertyChanged("IsFeatured");
                this.OnIsFeaturedChanged();
		    }
		}
		
        partial void OnFeaturedStartDateTimeChanging(DateTime? value);
        partial void OnFeaturedStartDateTimeChanged();
		
		private DateTime? _FeaturedStartDateTime;
		public DateTime? FeaturedStartDateTime { 
		    get{
		        return _FeaturedStartDateTime;
		    } 
		    set{
		        this.OnFeaturedStartDateTimeChanging(value);
                this.SendPropertyChanging();
                this._FeaturedStartDateTime = value;
                this.SendPropertyChanged("FeaturedStartDateTime");
                this.OnFeaturedStartDateTimeChanged();
		    }
		}
		
        partial void OnFeaturedEndDateTimeChanging(DateTime? value);
        partial void OnFeaturedEndDateTimeChanged();
		
		private DateTime? _FeaturedEndDateTime;
		public DateTime? FeaturedEndDateTime { 
		    get{
		        return _FeaturedEndDateTime;
		    } 
		    set{
		        this.OnFeaturedEndDateTimeChanging(value);
                this.SendPropertyChanging();
                this._FeaturedEndDateTime = value;
                this.SendPropertyChanged("FeaturedEndDateTime");
                this.OnFeaturedEndDateTimeChanged();
		    }
		}
		
        partial void OnEventBrightUrlChanging(string value);
        partial void OnEventBrightUrlChanged();
		
		private string _EventBrightUrl;
		public string EventBrightUrl { 
		    get{
		        return _EventBrightUrl;
		    } 
		    set{
		        this.OnEventBrightUrlChanging(value);
                this.SendPropertyChanging();
                this._EventBrightUrl = value;
                this.SendPropertyChanged("EventBrightUrl");
                this.OnEventBrightUrlChanged();
		    }
		}
		
        partial void OnEventBriteEIDChanging(string value);
        partial void OnEventBriteEIDChanged();
		
		private string _EventBriteEID;
		public string EventBriteEID { 
		    get{
		        return _EventBriteEID;
		    } 
		    set{
		        this.OnEventBriteEIDChanging(value);
                this.SendPropertyChanging();
                this._EventBriteEID = value;
                this.SendPropertyChanged("EventBriteEID");
                this.OnEventBriteEIDChanged();
		    }
		}
		
        partial void OnLastCollectionDateTimeChanging(DateTime? value);
        partial void OnLastCollectionDateTimeChanged();
		
		private DateTime? _LastCollectionDateTime;
		public DateTime? LastCollectionDateTime { 
		    get{
		        return _LastCollectionDateTime;
		    } 
		    set{
		        this.OnLastCollectionDateTimeChanging(value);
                this.SendPropertyChanging();
                this._LastCollectionDateTime = value;
                this.SendPropertyChanged("LastCollectionDateTime");
                this.OnLastCollectionDateTimeChanged();
		    }
		}
		
        partial void OnNormalCollectionCountChanging(int? value);
        partial void OnNormalCollectionCountChanged();
		
		private int? _NormalCollectionCount;
		public int? NormalCollectionCount { 
		    get{
		        return _NormalCollectionCount;
		    } 
		    set{
		        this.OnNormalCollectionCountChanging(value);
                this.SendPropertyChanging();
                this._NormalCollectionCount = value;
                this.SendPropertyChanged("NormalCollectionCount");
                this.OnNormalCollectionCountChanged();
		    }
		}
		
        partial void OnIsPaidChanging(bool? value);
        partial void OnIsPaidChanged();
		
		private bool? _IsPaid;
		public bool? IsPaid { 
		    get{
		        return _IsPaid;
		    } 
		    set{
		        this.OnIsPaidChanging(value);
                this.SendPropertyChanging();
                this._IsPaid = value;
                this.SendPropertyChanged("IsPaid");
                this.OnIsPaidChanged();
		    }
		}
		
        partial void OnDatePaidChanging(DateTime? value);
        partial void OnDatePaidChanged();
		
		private DateTime? _DatePaid;
		public DateTime? DatePaid { 
		    get{
		        return _DatePaid;
		    } 
		    set{
		        this.OnDatePaidChanging(value);
                this.SendPropertyChanging();
                this._DatePaid = value;
                this.SendPropertyChanged("DatePaid");
                this.OnDatePaidChanged();
		    }
		}
		

        #endregion

        #region Foreign Keys
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

        public IQueryable<CheckIn> CheckIns
        {
            get
            {
                  var db=new Epilogger.Data.EpiloggerDB();
                  return from items in db.CheckIns
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

        public IQueryable<LiveModeCustomSetting> LiveModeCustomSettings
        {
            get
            {
                  var db=new Epilogger.Data.EpiloggerDB();
                  return from items in db.LiveModeCustomSettings
                       where items.EventId == _ID
                       select items;
            }
        }

        public IQueryable<MemoryBox> MemoryBoxes
        {
            get
            {
                  var db=new Epilogger.Data.EpiloggerDB();
                  return from items in db.MemoryBoxes
                       where items.EventId == _ID
                       select items;
            }
        }

        public IQueryable<MemoryBoxItem> MemoryBoxItems
        {
            get
            {
                  var db=new Epilogger.Data.EpiloggerDB();
                  return from items in db.MemoryBoxItems
                       where items.EventId == _ID
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

        public IQueryable<UserFollowsEvent> UserFollowsEvents
        {
            get
            {
                  var db=new Epilogger.Data.EpiloggerDB();
                  return from items in db.UserFollowsEvents
                       where items.EventID == _ID
                       select items;
            }
        }

        public IQueryable<UserRatesEvent> UserRatesEvents
        {
            get
            {
                  var db=new Epilogger.Data.EpiloggerDB();
                  return from items in db.UserRatesEvents
                       where items.EventID == _ID
                       select items;
            }
        }

        public IQueryable<WidgetCustomSetting> WidgetCustomSettings
        {
            get
            {
                  var db=new Epilogger.Data.EpiloggerDB();
                  return from items in db.WidgetCustomSettings
                       where items.EventId == _ID
                       select items;
            }
        }

        public IQueryable<User> Users
        {
            get
            {
                  var db=new Epilogger.Data.EpiloggerDB();
                  return from items in db.Users
                       where items.ID == _UserID
                       select items;
            }
        }

        public IQueryable<Venue> Venues
        {
            get
            {
                  var db=new Epilogger.Data.EpiloggerDB();
                  return from items in db.Venues
                       where items.ID == _VenueID
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
    /// A class which represents the SponsorImages table in the Epilogger Database.
    /// This class is queryable through EpiloggerDB.SponsorImage 
    /// </summary>

	public partial class SponsorImage: INotifyPropertyChanging, INotifyPropertyChanged
	{
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
	    
	    public SponsorImage(){
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
		
        partial void OnSponsorURLChanging(string value);
        partial void OnSponsorURLChanged();
		
		private string _SponsorURL;
		public string SponsorURL { 
		    get{
		        return _SponsorURL;
		    } 
		    set{
		        this.OnSponsorURLChanging(value);
                this.SendPropertyChanging();
                this._SponsorURL = value;
                this.SendPropertyChanged("SponsorURL");
                this.OnSponsorURLChanged();
		    }
		}
		
        partial void OnLiveModeIDChanging(int value);
        partial void OnLiveModeIDChanged();
		
		private int _LiveModeID;
		public int LiveModeID { 
		    get{
		        return _LiveModeID;
		    } 
		    set{
		        this.OnLiveModeIDChanging(value);
                this.SendPropertyChanging();
                this._LiveModeID = value;
                this.SendPropertyChanged("LiveModeID");
                this.OnLiveModeIDChanged();
		    }
		}
		

        #endregion

        #region Foreign Keys
        public IQueryable<LiveModeCustomSetting> LiveModeCustomSettings
        {
            get
            {
                  var db=new Epilogger.Data.EpiloggerDB();
                  return from items in db.LiveModeCustomSettings
                       where items.Id == _LiveModeID
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
		
        partial void OnDateTimeChanging(DateTime? value);
        partial void OnDateTimeChanged();
		
		private DateTime? _DateTime;
		public DateTime? DateTime { 
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

        public IQueryable<Image> Images
        {
            get
            {
                  var db=new Epilogger.Data.EpiloggerDB();
                  return from items in db.Images
                       where items.ID == _ImageID
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
		
        partial void OnImageFingerPrintChanging(string value);
        partial void OnImageFingerPrintChanged();
		
		private string _ImageFingerPrint;
		public string ImageFingerPrint { 
		    get{
		        return _ImageFingerPrint;
		    } 
		    set{
		        this.OnImageFingerPrintChanging(value);
                this.SendPropertyChanging();
                this._ImageFingerPrint = value;
                this.SendPropertyChanged("ImageFingerPrint");
                this.OnImageFingerPrintChanged();
		    }
		}
		
        partial void OnMediaTypeChanging(short value);
        partial void OnMediaTypeChanged();
		
		private short _MediaType;
		public short MediaType { 
		    get{
		        return _MediaType;
		    } 
		    set{
		        this.OnMediaTypeChanging(value);
                this.SendPropertyChanging();
                this._MediaType = value;
                this.SendPropertyChanged("MediaType");
                this.OnMediaTypeChanged();
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

        public IQueryable<ImageMetaDatum> ImageMetaData
        {
            get
            {
                  var db=new Epilogger.Data.EpiloggerDB();
                  return from items in db.ImageMetaData
                       where items.ImageID == _ID
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
    /// A class which represents the UserTwitterActions table in the Epilogger Database.
    /// This class is queryable through EpiloggerDB.UserTwitterAction 
    /// </summary>

	public partial class UserTwitterAction: INotifyPropertyChanging, INotifyPropertyChanged
	{
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
	    
	    public UserTwitterAction(){
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
		
        partial void OnTweetIdChanging(long? value);
        partial void OnTweetIdChanged();
		
		private long? _TweetId;
		public long? TweetId { 
		    get{
		        return _TweetId;
		    } 
		    set{
		        this.OnTweetIdChanging(value);
                this.SendPropertyChanging();
                this._TweetId = value;
                this.SendPropertyChanged("TweetId");
                this.OnTweetIdChanged();
		    }
		}
		
        partial void OnTwitterActionChanging(string value);
        partial void OnTwitterActionChanged();
		
		private string _TwitterAction;
		public string TwitterAction { 
		    get{
		        return _TwitterAction;
		    } 
		    set{
		        this.OnTwitterActionChanging(value);
                this.SendPropertyChanging();
                this._TwitterAction = value;
                this.SendPropertyChanged("TwitterAction");
                this.OnTwitterActionChanged();
		    }
		}
		
        partial void OnDateTimeChanging(DateTime? value);
        partial void OnDateTimeChanged();
		
		private DateTime? _DateTime;
		public DateTime? DateTime { 
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
    /// A class which represents the WidgetCustomSettings table in the Epilogger Database.
    /// This class is queryable through EpiloggerDB.WidgetCustomSetting 
    /// </summary>

	public partial class WidgetCustomSetting: INotifyPropertyChanging, INotifyPropertyChanged
	{
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
	    
	    public WidgetCustomSetting(){
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
		
        partial void OnEventIdChanging(int value);
        partial void OnEventIdChanged();
		
		private int _EventId;
		public int EventId { 
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
		
        partial void OnLogoChanging(string value);
        partial void OnLogoChanged();
		
		private string _Logo;
		public string Logo { 
		    get{
		        return _Logo;
		    } 
		    set{
		        this.OnLogoChanging(value);
                this.SendPropertyChanging();
                this._Logo = value;
                this.SendPropertyChanged("Logo");
                this.OnLogoChanged();
		    }
		}
		
        partial void OnHeaderBGColorChanging(string value);
        partial void OnHeaderBGColorChanged();
		
		private string _HeaderBGColor;
		public string HeaderBGColor { 
		    get{
		        return _HeaderBGColor;
		    } 
		    set{
		        this.OnHeaderBGColorChanging(value);
                this.SendPropertyChanging();
                this._HeaderBGColor = value;
                this.SendPropertyChanged("HeaderBGColor");
                this.OnHeaderBGColorChanged();
		    }
		}
		
        partial void OnHeaderTextColorChanging(string value);
        partial void OnHeaderTextColorChanged();
		
		private string _HeaderTextColor;
		public string HeaderTextColor { 
		    get{
		        return _HeaderTextColor;
		    } 
		    set{
		        this.OnHeaderTextColorChanging(value);
                this.SendPropertyChanging();
                this._HeaderTextColor = value;
                this.SendPropertyChanged("HeaderTextColor");
                this.OnHeaderTextColorChanged();
		    }
		}
		
        partial void OnHeaderLinkColorChanging(string value);
        partial void OnHeaderLinkColorChanged();
		
		private string _HeaderLinkColor;
		public string HeaderLinkColor { 
		    get{
		        return _HeaderLinkColor;
		    } 
		    set{
		        this.OnHeaderLinkColorChanging(value);
                this.SendPropertyChanging();
                this._HeaderLinkColor = value;
                this.SendPropertyChanged("HeaderLinkColor");
                this.OnHeaderLinkColorChanged();
		    }
		}
		
        partial void OnContentBGColorChanging(string value);
        partial void OnContentBGColorChanged();
		
		private string _ContentBGColor;
		public string ContentBGColor { 
		    get{
		        return _ContentBGColor;
		    } 
		    set{
		        this.OnContentBGColorChanging(value);
                this.SendPropertyChanging();
                this._ContentBGColor = value;
                this.SendPropertyChanged("ContentBGColor");
                this.OnContentBGColorChanged();
		    }
		}
		
        partial void OnContentTextColorChanging(string value);
        partial void OnContentTextColorChanged();
		
		private string _ContentTextColor;
		public string ContentTextColor { 
		    get{
		        return _ContentTextColor;
		    } 
		    set{
		        this.OnContentTextColorChanging(value);
                this.SendPropertyChanging();
                this._ContentTextColor = value;
                this.SendPropertyChanged("ContentTextColor");
                this.OnContentTextColorChanged();
		    }
		}
		
        partial void OnContentLinkColorChanging(string value);
        partial void OnContentLinkColorChanged();
		
		private string _ContentLinkColor;
		public string ContentLinkColor { 
		    get{
		        return _ContentLinkColor;
		    } 
		    set{
		        this.OnContentLinkColorChanging(value);
                this.SendPropertyChanging();
                this._ContentLinkColor = value;
                this.SendPropertyChanged("ContentLinkColor");
                this.OnContentLinkColorChanged();
		    }
		}
		
        partial void OnSpriteColorChanging(string value);
        partial void OnSpriteColorChanged();
		
		private string _SpriteColor;
		public string SpriteColor { 
		    get{
		        return _SpriteColor;
		    } 
		    set{
		        this.OnSpriteColorChanging(value);
                this.SendPropertyChanging();
                this._SpriteColor = value;
                this.SendPropertyChanged("SpriteColor");
                this.OnSpriteColorChanged();
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
                       where items.ID == _EventId
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
    /// A class which represents the ActiveVisitorsQueueOLD table in the Epilogger Database.
    /// This class is queryable through EpiloggerDB.ActiveVisitorsQueueOLD 
    /// </summary>

	public partial class ActiveVisitorsQueueOLD: INotifyPropertyChanging, INotifyPropertyChanged
	{
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
	    
	    public ActiveVisitorsQueueOLD(){
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
		
        partial void OnSessionIDChanging(string value);
        partial void OnSessionIDChanged();
		
		private string _SessionID;
		public string SessionID { 
		    get{
		        return _SessionID;
		    } 
		    set{
		        this.OnSessionIDChanging(value);
                this.SendPropertyChanging();
                this._SessionID = value;
                this.SendPropertyChanged("SessionID");
                this.OnSessionIDChanged();
		    }
		}
		
        partial void OnAccountChanging(string value);
        partial void OnAccountChanged();
		
		private string _Account;
		public string Account { 
		    get{
		        return _Account;
		    } 
		    set{
		        this.OnAccountChanging(value);
                this.SendPropertyChanging();
                this._Account = value;
                this.SendPropertyChanged("Account");
                this.OnAccountChanged();
		    }
		}
		
        partial void OnMyIDChanging(int value);
        partial void OnMyIDChanged();
		
		private int _MyID;
		public int MyID { 
		    get{
		        return _MyID;
		    } 
		    set{
		        this.OnMyIDChanging(value);
                this.SendPropertyChanging();
                this._MyID = value;
                this.SendPropertyChanged("MyID");
                this.OnMyIDChanged();
		    }
		}
		
        partial void OnCampaignIDChanging(int value);
        partial void OnCampaignIDChanged();
		
		private int _CampaignID;
		public int CampaignID { 
		    get{
		        return _CampaignID;
		    } 
		    set{
		        this.OnCampaignIDChanging(value);
                this.SendPropertyChanging();
                this._CampaignID = value;
                this.SendPropertyChanged("CampaignID");
                this.OnCampaignIDChanged();
		    }
		}
		
        partial void OnCurrentDateTimeChanging(DateTime value);
        partial void OnCurrentDateTimeChanged();
		
		private DateTime _CurrentDateTime;
		public DateTime CurrentDateTime { 
		    get{
		        return _CurrentDateTime;
		    } 
		    set{
		        this.OnCurrentDateTimeChanging(value);
                this.SendPropertyChanging();
                this._CurrentDateTime = value;
                this.SendPropertyChanged("CurrentDateTime");
                this.OnCurrentDateTimeChanged();
		    }
		}
		
        partial void OnDocumentTitleChanging(string value);
        partial void OnDocumentTitleChanged();
		
		private string _DocumentTitle;
		public string DocumentTitle { 
		    get{
		        return _DocumentTitle;
		    } 
		    set{
		        this.OnDocumentTitleChanging(value);
                this.SendPropertyChanging();
                this._DocumentTitle = value;
                this.SendPropertyChanged("DocumentTitle");
                this.OnDocumentTitleChanged();
		    }
		}
		
        partial void OnRefererChanging(string value);
        partial void OnRefererChanged();
		
		private string _Referer;
		public string Referer { 
		    get{
		        return _Referer;
		    } 
		    set{
		        this.OnRefererChanging(value);
                this.SendPropertyChanging();
                this._Referer = value;
                this.SendPropertyChanged("Referer");
                this.OnRefererChanged();
		    }
		}
		
        partial void OnHostNameChanging(string value);
        partial void OnHostNameChanged();
		
		private string _HostName;
		public string HostName { 
		    get{
		        return _HostName;
		    } 
		    set{
		        this.OnHostNameChanging(value);
                this.SendPropertyChanging();
                this._HostName = value;
                this.SendPropertyChanged("HostName");
                this.OnHostNameChanged();
		    }
		}
		
        partial void OnRelativePathOfPageChanging(string value);
        partial void OnRelativePathOfPageChanged();
		
		private string _RelativePathOfPage;
		public string RelativePathOfPage { 
		    get{
		        return _RelativePathOfPage;
		    } 
		    set{
		        this.OnRelativePathOfPageChanging(value);
                this.SendPropertyChanging();
                this._RelativePathOfPage = value;
                this.SendPropertyChanged("RelativePathOfPage");
                this.OnRelativePathOfPageChanged();
		    }
		}
		
        partial void OnTestChanging(string value);
        partial void OnTestChanged();
		
		private string _Test;
		public string Test { 
		    get{
		        return _Test;
		    } 
		    set{
		        this.OnTestChanging(value);
                this.SendPropertyChanging();
                this._Test = value;
                this.SendPropertyChanged("Test");
                this.OnTestChanged();
		    }
		}
		
        partial void OnUserAgentChanging(string value);
        partial void OnUserAgentChanged();
		
		private string _UserAgent;
		public string UserAgent { 
		    get{
		        return _UserAgent;
		    } 
		    set{
		        this.OnUserAgentChanging(value);
                this.SendPropertyChanging();
                this._UserAgent = value;
                this.SendPropertyChanged("UserAgent");
                this.OnUserAgentChanged();
		    }
		}
		
        partial void OnUserIDChanging(string value);
        partial void OnUserIDChanged();
		
		private string _UserID;
		public string UserID { 
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
		
        partial void OnTitleChanging(string value);
        partial void OnTitleChanged();
		
		private string _Title;
		public string Title { 
		    get{
		        return _Title;
		    } 
		    set{
		        this.OnTitleChanging(value);
                this.SendPropertyChanging();
                this._Title = value;
                this.SendPropertyChanged("Title");
                this.OnTitleChanged();
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
		
        partial void OnDateTimeChanging(DateTime? value);
        partial void OnDateTimeChanged();
		
		private DateTime? _DateTime;
		public DateTime? DateTime { 
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
		
        partial void OnThumbnailChanging(string value);
        partial void OnThumbnailChanged();
		
		private string _Thumbnail;
		public string Thumbnail { 
		    get{
		        return _Thumbnail;
		    } 
		    set{
		        this.OnThumbnailChanging(value);
                this.SendPropertyChanging();
                this._Thumbnail = value;
                this.SendPropertyChanged("Thumbnail");
                this.OnThumbnailChanged();
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
    /// A class which represents the AggregateVisitHistoryOLD table in the Epilogger Database.
    /// This class is queryable through EpiloggerDB.AggregateVisitHistoryOLD 
    /// </summary>

	public partial class AggregateVisitHistoryOLD: INotifyPropertyChanging, INotifyPropertyChanged
	{
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
	    
	    public AggregateVisitHistoryOLD(){
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
		
        partial void OnSessionIDChanging(string value);
        partial void OnSessionIDChanged();
		
		private string _SessionID;
		public string SessionID { 
		    get{
		        return _SessionID;
		    } 
		    set{
		        this.OnSessionIDChanging(value);
                this.SendPropertyChanging();
                this._SessionID = value;
                this.SendPropertyChanged("SessionID");
                this.OnSessionIDChanged();
		    }
		}
		
        partial void OnmyIDChanging(int value);
        partial void OnmyIDChanged();
		
		private int _myID;
		public int myID { 
		    get{
		        return _myID;
		    } 
		    set{
		        this.OnmyIDChanging(value);
                this.SendPropertyChanging();
                this._myID = value;
                this.SendPropertyChanged("myID");
                this.OnmyIDChanged();
		    }
		}
		
        partial void OnCampaignIDChanging(int value);
        partial void OnCampaignIDChanged();
		
		private int _CampaignID;
		public int CampaignID { 
		    get{
		        return _CampaignID;
		    } 
		    set{
		        this.OnCampaignIDChanging(value);
                this.SendPropertyChanging();
                this._CampaignID = value;
                this.SendPropertyChanged("CampaignID");
                this.OnCampaignIDChanged();
		    }
		}
		
        partial void OnEnterTimeChanging(DateTime value);
        partial void OnEnterTimeChanged();
		
		private DateTime _EnterTime;
		public DateTime EnterTime { 
		    get{
		        return _EnterTime;
		    } 
		    set{
		        this.OnEnterTimeChanging(value);
                this.SendPropertyChanging();
                this._EnterTime = value;
                this.SendPropertyChanged("EnterTime");
                this.OnEnterTimeChanged();
		    }
		}
		
        partial void OnExitTimeChanging(DateTime value);
        partial void OnExitTimeChanged();
		
		private DateTime _ExitTime;
		public DateTime ExitTime { 
		    get{
		        return _ExitTime;
		    } 
		    set{
		        this.OnExitTimeChanging(value);
                this.SendPropertyChanging();
                this._ExitTime = value;
                this.SendPropertyChanged("ExitTime");
                this.OnExitTimeChanged();
		    }
		}
		
        partial void OnNumberOfPagesViewedChanging(int value);
        partial void OnNumberOfPagesViewedChanged();
		
		private int _NumberOfPagesViewed;
		public int NumberOfPagesViewed { 
		    get{
		        return _NumberOfPagesViewed;
		    } 
		    set{
		        this.OnNumberOfPagesViewedChanging(value);
                this.SendPropertyChanging();
                this._NumberOfPagesViewed = value;
                this.SendPropertyChanged("NumberOfPagesViewed");
                this.OnNumberOfPagesViewedChanged();
		    }
		}
		
        partial void OnTotalTimeOnSiteChanging(string value);
        partial void OnTotalTimeOnSiteChanged();
		
		private string _TotalTimeOnSite;
		public string TotalTimeOnSite { 
		    get{
		        return _TotalTimeOnSite;
		    } 
		    set{
		        this.OnTotalTimeOnSiteChanging(value);
                this.SendPropertyChanging();
                this._TotalTimeOnSite = value;
                this.SendPropertyChanged("TotalTimeOnSite");
                this.OnTotalTimeOnSiteChanged();
		    }
		}
		
        partial void OnVisitTrailChanging(string value);
        partial void OnVisitTrailChanged();
		
		private string _VisitTrail;
		public string VisitTrail { 
		    get{
		        return _VisitTrail;
		    } 
		    set{
		        this.OnVisitTrailChanging(value);
                this.SendPropertyChanging();
                this._VisitTrail = value;
                this.SendPropertyChanged("VisitTrail");
                this.OnVisitTrailChanged();
		    }
		}
		
        partial void OnUserIDChanging(string value);
        partial void OnUserIDChanged();
		
		private string _UserID;
		public string UserID { 
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
    /// A class which represents the VisitHistoryOLD table in the Epilogger Database.
    /// This class is queryable through EpiloggerDB.VisitHistoryOLD 
    /// </summary>

	public partial class VisitHistoryOLD: INotifyPropertyChanging, INotifyPropertyChanged
	{
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
	    
	    public VisitHistoryOLD(){
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
		
        partial void OnSessionIDChanging(string value);
        partial void OnSessionIDChanged();
		
		private string _SessionID;
		public string SessionID { 
		    get{
		        return _SessionID;
		    } 
		    set{
		        this.OnSessionIDChanging(value);
                this.SendPropertyChanging();
                this._SessionID = value;
                this.SendPropertyChanged("SessionID");
                this.OnSessionIDChanged();
		    }
		}
		
        partial void OnAccountChanging(string value);
        partial void OnAccountChanged();
		
		private string _Account;
		public string Account { 
		    get{
		        return _Account;
		    } 
		    set{
		        this.OnAccountChanging(value);
                this.SendPropertyChanging();
                this._Account = value;
                this.SendPropertyChanged("Account");
                this.OnAccountChanged();
		    }
		}
		
        partial void OnMyIDChanging(string value);
        partial void OnMyIDChanged();
		
		private string _MyID;
		public string MyID { 
		    get{
		        return _MyID;
		    } 
		    set{
		        this.OnMyIDChanging(value);
                this.SendPropertyChanging();
                this._MyID = value;
                this.SendPropertyChanged("MyID");
                this.OnMyIDChanged();
		    }
		}
		
        partial void OnCampaignIDChanging(string value);
        partial void OnCampaignIDChanged();
		
		private string _CampaignID;
		public string CampaignID { 
		    get{
		        return _CampaignID;
		    } 
		    set{
		        this.OnCampaignIDChanging(value);
                this.SendPropertyChanging();
                this._CampaignID = value;
                this.SendPropertyChanged("CampaignID");
                this.OnCampaignIDChanged();
		    }
		}
		
        partial void OnCurrentDateTimeChanging(DateTime value);
        partial void OnCurrentDateTimeChanged();
		
		private DateTime _CurrentDateTime;
		public DateTime CurrentDateTime { 
		    get{
		        return _CurrentDateTime;
		    } 
		    set{
		        this.OnCurrentDateTimeChanging(value);
                this.SendPropertyChanging();
                this._CurrentDateTime = value;
                this.SendPropertyChanged("CurrentDateTime");
                this.OnCurrentDateTimeChanged();
		    }
		}
		
        partial void OnDocumentTitleChanging(string value);
        partial void OnDocumentTitleChanged();
		
		private string _DocumentTitle;
		public string DocumentTitle { 
		    get{
		        return _DocumentTitle;
		    } 
		    set{
		        this.OnDocumentTitleChanging(value);
                this.SendPropertyChanging();
                this._DocumentTitle = value;
                this.SendPropertyChanged("DocumentTitle");
                this.OnDocumentTitleChanged();
		    }
		}
		
        partial void OnRefererChanging(string value);
        partial void OnRefererChanged();
		
		private string _Referer;
		public string Referer { 
		    get{
		        return _Referer;
		    } 
		    set{
		        this.OnRefererChanging(value);
                this.SendPropertyChanging();
                this._Referer = value;
                this.SendPropertyChanged("Referer");
                this.OnRefererChanged();
		    }
		}
		
        partial void OnHostNameChanging(string value);
        partial void OnHostNameChanged();
		
		private string _HostName;
		public string HostName { 
		    get{
		        return _HostName;
		    } 
		    set{
		        this.OnHostNameChanging(value);
                this.SendPropertyChanging();
                this._HostName = value;
                this.SendPropertyChanged("HostName");
                this.OnHostNameChanged();
		    }
		}
		
        partial void OnRelativePathOfPageChanging(string value);
        partial void OnRelativePathOfPageChanged();
		
		private string _RelativePathOfPage;
		public string RelativePathOfPage { 
		    get{
		        return _RelativePathOfPage;
		    } 
		    set{
		        this.OnRelativePathOfPageChanging(value);
                this.SendPropertyChanging();
                this._RelativePathOfPage = value;
                this.SendPropertyChanged("RelativePathOfPage");
                this.OnRelativePathOfPageChanged();
		    }
		}
		
        partial void OnTestChanging(string value);
        partial void OnTestChanged();
		
		private string _Test;
		public string Test { 
		    get{
		        return _Test;
		    } 
		    set{
		        this.OnTestChanging(value);
                this.SendPropertyChanging();
                this._Test = value;
                this.SendPropertyChanged("Test");
                this.OnTestChanged();
		    }
		}
		
        partial void OnUserAgentChanging(string value);
        partial void OnUserAgentChanged();
		
		private string _UserAgent;
		public string UserAgent { 
		    get{
		        return _UserAgent;
		    } 
		    set{
		        this.OnUserAgentChanging(value);
                this.SendPropertyChanging();
                this._UserAgent = value;
                this.SendPropertyChanged("UserAgent");
                this.OnUserAgentChanged();
		    }
		}
		
        partial void OnUserIDChanging(string value);
        partial void OnUserIDChanged();
		
		private string _UserID;
		public string UserID { 
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
    /// A class which represents the UserRatesEvent table in the Epilogger Database.
    /// This class is queryable through EpiloggerDB.UserRatesEvent 
    /// </summary>

	public partial class UserRatesEvent: INotifyPropertyChanging, INotifyPropertyChanged
	{
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
	    
	    public UserRatesEvent(){
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
		
        partial void OnUserRatingChanging(int? value);
        partial void OnUserRatingChanged();
		
		private int? _UserRating;
		public int? UserRating { 
		    get{
		        return _UserRating;
		    } 
		    set{
		        this.OnUserRatingChanging(value);
                this.SendPropertyChanging();
                this._UserRating = value;
                this.SendPropertyChanged("UserRating");
                this.OnUserRatingChanged();
		    }
		}
		
        partial void OnRatingDateTimeChanging(DateTime value);
        partial void OnRatingDateTimeChanged();
		
		private DateTime _RatingDateTime;
		public DateTime RatingDateTime { 
		    get{
		        return _RatingDateTime;
		    } 
		    set{
		        this.OnRatingDateTimeChanging(value);
                this.SendPropertyChanging();
                this._RatingDateTime = value;
                this.SendPropertyChanged("RatingDateTime");
                this.OnRatingDateTimeChanged();
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

        public IQueryable<User> Users
        {
            get
            {
                  var db=new Epilogger.Data.EpiloggerDB();
                  return from items in db.Users
                       where items.ID == _UserID
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
    /// A class which represents the MemoryBoxes table in the Epilogger Database.
    /// This class is queryable through EpiloggerDB.MemoryBox 
    /// </summary>

	public partial class MemoryBox: INotifyPropertyChanging, INotifyPropertyChanged
	{
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
	    
	    public MemoryBox(){
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
		
        partial void OnEventIdChanging(int value);
        partial void OnEventIdChanged();
		
		private int _EventId;
		public int EventId { 
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
		
        partial void OnCreatedDateTimeChanging(DateTime value);
        partial void OnCreatedDateTimeChanged();
		
		private DateTime _CreatedDateTime;
		public DateTime CreatedDateTime { 
		    get{
		        return _CreatedDateTime;
		    } 
		    set{
		        this.OnCreatedDateTimeChanging(value);
                this.SendPropertyChanging();
                this._CreatedDateTime = value;
                this.SendPropertyChanged("CreatedDateTime");
                this.OnCreatedDateTimeChanged();
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
        public IQueryable<Event> Events
        {
            get
            {
                  var db=new Epilogger.Data.EpiloggerDB();
                  return from items in db.Events
                       where items.ID == _EventId
                       select items;
            }
        }

        public IQueryable<MemoryBoxItem> MemoryBoxItems
        {
            get
            {
                  var db=new Epilogger.Data.EpiloggerDB();
                  return from items in db.MemoryBoxItems
                       where items.MemboxId == _ID
                       select items;
            }
        }

        public IQueryable<User> Users
        {
            get
            {
                  var db=new Epilogger.Data.EpiloggerDB();
                  return from items in db.Users
                       where items.ID == _UserId
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
    /// A class which represents the User table in the Epilogger Database.
    /// This class is queryable through EpiloggerDB.User 
    /// </summary>

	public partial class User: INotifyPropertyChanging, INotifyPropertyChanged
	{
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
	    
	    public User(){
	        OnCreated();
	    }
	    
	    #region Properties
	    
        partial void OnIDChanging(Guid value);
        partial void OnIDChanged();
		
		private Guid _ID;
		public Guid ID { 
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
		
        partial void OnRoleIDChanging(int value);
        partial void OnRoleIDChanged();
		
		private int _RoleID;
		public int RoleID { 
		    get{
		        return _RoleID;
		    } 
		    set{
		        this.OnRoleIDChanging(value);
                this.SendPropertyChanging();
                this._RoleID = value;
                this.SendPropertyChanged("RoleID");
                this.OnRoleIDChanged();
		    }
		}
		
        partial void OnUsernameChanging(string value);
        partial void OnUsernameChanged();
		
		private string _Username;
		public string Username { 
		    get{
		        return _Username;
		    } 
		    set{
		        this.OnUsernameChanging(value);
                this.SendPropertyChanging();
                this._Username = value;
                this.SendPropertyChanged("Username");
                this.OnUsernameChanged();
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
		
        partial void OnFirstNameChanging(string value);
        partial void OnFirstNameChanged();
		
		private string _FirstName;
		public string FirstName { 
		    get{
		        return _FirstName;
		    } 
		    set{
		        this.OnFirstNameChanging(value);
                this.SendPropertyChanging();
                this._FirstName = value;
                this.SendPropertyChanged("FirstName");
                this.OnFirstNameChanged();
		    }
		}
		
        partial void OnLastNameChanging(string value);
        partial void OnLastNameChanged();
		
		private string _LastName;
		public string LastName { 
		    get{
		        return _LastName;
		    } 
		    set{
		        this.OnLastNameChanging(value);
                this.SendPropertyChanging();
                this._LastName = value;
                this.SendPropertyChanged("LastName");
                this.OnLastNameChanged();
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
		
        partial void OnCreatedDateChanging(DateTime value);
        partial void OnCreatedDateChanged();
		
		private DateTime _CreatedDate;
		public DateTime CreatedDate { 
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
		
        partial void OnDateOfBirthChanging(DateTime? value);
        partial void OnDateOfBirthChanged();
		
		private DateTime? _DateOfBirth;
		public DateTime? DateOfBirth { 
		    get{
		        return _DateOfBirth;
		    } 
		    set{
		        this.OnDateOfBirthChanging(value);
                this.SendPropertyChanging();
                this._DateOfBirth = value;
                this.SendPropertyChanged("DateOfBirth");
                this.OnDateOfBirthChanged();
		    }
		}
		
        partial void OnForgotPasswordHashChanging(Guid? value);
        partial void OnForgotPasswordHashChanged();
		
		private Guid? _ForgotPasswordHash;
		public Guid? ForgotPasswordHash { 
		    get{
		        return _ForgotPasswordHash;
		    } 
		    set{
		        this.OnForgotPasswordHashChanging(value);
                this.SendPropertyChanging();
                this._ForgotPasswordHash = value;
                this.SendPropertyChanged("ForgotPasswordHash");
                this.OnForgotPasswordHashChanged();
		    }
		}
		
        partial void OnProfilePictureChanging(string value);
        partial void OnProfilePictureChanged();
		
		private string _ProfilePicture;
		public string ProfilePicture { 
		    get{
		        return _ProfilePicture;
		    } 
		    set{
		        this.OnProfilePictureChanging(value);
                this.SendPropertyChanging();
                this._ProfilePicture = value;
                this.SendPropertyChanged("ProfilePicture");
                this.OnProfilePictureChanged();
		    }
		}
		
        partial void OnTimeZoneOffSetChanging(int value);
        partial void OnTimeZoneOffSetChanged();
		
		private int _TimeZoneOffSet;
		public int TimeZoneOffSet { 
		    get{
		        return _TimeZoneOffSet;
		    } 
		    set{
		        this.OnTimeZoneOffSetChanging(value);
                this.SendPropertyChanging();
                this._TimeZoneOffSet = value;
                this.SendPropertyChanged("TimeZoneOffSet");
                this.OnTimeZoneOffSetChanged();
		    }
		}
		
        partial void OnEmailVerifiedChanging(bool? value);
        partial void OnEmailVerifiedChanged();
		
		private bool? _EmailVerified;
		public bool? EmailVerified { 
		    get{
		        return _EmailVerified;
		    } 
		    set{
		        this.OnEmailVerifiedChanging(value);
                this.SendPropertyChanging();
                this._EmailVerified = value;
                this.SendPropertyChanged("EmailVerified");
                this.OnEmailVerifiedChanged();
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
		
        partial void OnProfilePictureLargeChanging(string value);
        partial void OnProfilePictureLargeChanged();
		
		private string _ProfilePictureLarge;
		public string ProfilePictureLarge { 
		    get{
		        return _ProfilePictureLarge;
		    } 
		    set{
		        this.OnProfilePictureLargeChanging(value);
                this.SendPropertyChanging();
                this._ProfilePictureLarge = value;
                this.SendPropertyChanged("ProfilePictureLarge");
                this.OnProfilePictureLargeChanged();
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
                       where items.UserID == _ID
                       select items;
            }
        }

        public IQueryable<MemoryBox> MemoryBoxes
        {
            get
            {
                  var db=new Epilogger.Data.EpiloggerDB();
                  return from items in db.MemoryBoxes
                       where items.UserId == _ID
                       select items;
            }
        }

        public IQueryable<MemoryBoxItem> MemoryBoxItems
        {
            get
            {
                  var db=new Epilogger.Data.EpiloggerDB();
                  return from items in db.MemoryBoxItems
                       where items.UserId == _ID
                       select items;
            }
        }

        public IQueryable<UserAuthenticationProfile> UserAuthenticationProfiles
        {
            get
            {
                  var db=new Epilogger.Data.EpiloggerDB();
                  return from items in db.UserAuthenticationProfiles
                       where items.UserID == _ID
                       select items;
            }
        }

        public IQueryable<UserFollowsEvent> UserFollowsEvents
        {
            get
            {
                  var db=new Epilogger.Data.EpiloggerDB();
                  return from items in db.UserFollowsEvents
                       where items.UserID == _ID
                       select items;
            }
        }

        public IQueryable<UserFollowsUser> UserFollowsUsers
        {
            get
            {
                  var db=new Epilogger.Data.EpiloggerDB();
                  return from items in db.UserFollowsUsers
                       where items.UserFollowedID == _ID
                       select items;
            }
        }

        public IQueryable<UserFollowsUser> UserFollowsUsers6
        {
            get
            {
                  var db=new Epilogger.Data.EpiloggerDB();
                  return from items in db.UserFollowsUsers
                       where items.UserFollowingID == _ID
                       select items;
            }
        }

        public IQueryable<UserLoginTracking> UserLoginTrackings
        {
            get
            {
                  var db=new Epilogger.Data.EpiloggerDB();
                  return from items in db.UserLoginTrackings
                       where items.UserId == _ID
                       select items;
            }
        }

        public IQueryable<UserRatesEvent> UserRatesEvents
        {
            get
            {
                  var db=new Epilogger.Data.EpiloggerDB();
                  return from items in db.UserRatesEvents
                       where items.UserID == _ID
                       select items;
            }
        }

        public IQueryable<UserInRole> UserInRoles
        {
            get
            {
                  var db=new Epilogger.Data.EpiloggerDB();
                  return from items in db.UserInRoles
                       where items.UserID == _ID
                       select items;
            }
        }

        public IQueryable<UserRole> UserRoles
        {
            get
            {
                  var db=new Epilogger.Data.EpiloggerDB();
                  return from items in db.UserRoles
                       where items.ID == _RoleID
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
    /// A class which represents the UserRole table in the Epilogger Database.
    /// This class is queryable through EpiloggerDB.UserRole 
    /// </summary>

	public partial class UserRole: INotifyPropertyChanging, INotifyPropertyChanged
	{
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
	    
	    public UserRole(){
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
		

        #endregion

        #region Foreign Keys
        public IQueryable<User> Users
        {
            get
            {
                  var db=new Epilogger.Data.EpiloggerDB();
                  return from items in db.Users
                       where items.RoleID == _ID
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
    /// A class which represents the UserFollowsUser table in the Epilogger Database.
    /// This class is queryable through EpiloggerDB.UserFollowsUser 
    /// </summary>

	public partial class UserFollowsUser: INotifyPropertyChanging, INotifyPropertyChanged
	{
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
	    
	    public UserFollowsUser(){
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
		
        partial void OnUserFollowingIDChanging(Guid value);
        partial void OnUserFollowingIDChanged();
		
		private Guid _UserFollowingID;
		public Guid UserFollowingID { 
		    get{
		        return _UserFollowingID;
		    } 
		    set{
		        this.OnUserFollowingIDChanging(value);
                this.SendPropertyChanging();
                this._UserFollowingID = value;
                this.SendPropertyChanged("UserFollowingID");
                this.OnUserFollowingIDChanged();
		    }
		}
		
        partial void OnUserFollowedIDChanging(Guid value);
        partial void OnUserFollowedIDChanged();
		
		private Guid _UserFollowedID;
		public Guid UserFollowedID { 
		    get{
		        return _UserFollowedID;
		    } 
		    set{
		        this.OnUserFollowedIDChanging(value);
                this.SendPropertyChanging();
                this._UserFollowedID = value;
                this.SendPropertyChanged("UserFollowedID");
                this.OnUserFollowedIDChanged();
		    }
		}
		

        #endregion

        #region Foreign Keys
        public IQueryable<User> Users
        {
            get
            {
                  var db=new Epilogger.Data.EpiloggerDB();
                  return from items in db.Users
                       where items.ID == _UserFollowedID
                       select items;
            }
        }

        public IQueryable<User> Users1
        {
            get
            {
                  var db=new Epilogger.Data.EpiloggerDB();
                  return from items in db.Users
                       where items.ID == _UserFollowingID
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