


using System;
using SubSonic.Schema;
using System.Collections.Generic;
using SubSonic.DataProviders;
using System.Data;

namespace Epilogger.Data {
	
        /// <summary>
        /// Table: EventCategories
        /// Primary Key: ID
        /// </summary>

        public class EventCategoriesTable: DatabaseTable {
            
            public EventCategoriesTable(IDataProvider provider):base("EventCategories",provider){
                ClassName = "EventCategory";
                SchemaName = "dbo";
                

                Columns.Add(new DatabaseColumn("ID", this)
                {
	                IsPrimaryKey = true,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = true,
	                IsForeignKey = true,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("CategoryName", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 255
                });

                Columns.Add(new DatabaseColumn("URLStub", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 255
                });
                    
                
                
            }
            
            public IColumn ID{
                get{
                    return this.GetColumn("ID");
                }
            }
            				
   			public static string IDColumn{
			      get{
        			return "ID";
      			}
		    }
           
            public IColumn CategoryName{
                get{
                    return this.GetColumn("CategoryName");
                }
            }
            				
   			public static string CategoryNameColumn{
			      get{
        			return "CategoryName";
      			}
		    }
           
            public IColumn URLStub{
                get{
                    return this.GetColumn("URLStub");
                }
            }
            				
   			public static string URLStubColumn{
			      get{
        			return "URLStub";
      			}
		    }
           
                    
        }
        
        /// <summary>
        /// Table: User
        /// Primary Key: ID
        /// </summary>

        public class UserTable: DatabaseTable {
            
            public UserTable(IDataProvider provider):base("User",provider){
                ClassName = "User";
                SchemaName = "dbo";
                

                Columns.Add(new DatabaseColumn("ID", this)
                {
	                IsPrimaryKey = true,
	                DataType = DbType.Guid,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = true,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("RoleID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = true,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("Username", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 500
                });

                Columns.Add(new DatabaseColumn("Password", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 500
                });

                Columns.Add(new DatabaseColumn("FirstName", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 250
                });

                Columns.Add(new DatabaseColumn("LastName", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 250
                });

                Columns.Add(new DatabaseColumn("EmailAddress", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 500
                });

                Columns.Add(new DatabaseColumn("CreatedDate", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.DateTime,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("DateOfBirth", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.DateTime,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("ForgotPasswordHash", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Guid,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("ProfilePicture", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 250
                });

                Columns.Add(new DatabaseColumn("TimeZoneOffSet", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("IsActive", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Boolean,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });
                    
                
                
            }
            
            public IColumn ID{
                get{
                    return this.GetColumn("ID");
                }
            }
            				
   			public static string IDColumn{
			      get{
        			return "ID";
      			}
		    }
           
            public IColumn RoleID{
                get{
                    return this.GetColumn("RoleID");
                }
            }
            				
   			public static string RoleIDColumn{
			      get{
        			return "RoleID";
      			}
		    }
           
            public IColumn Username{
                get{
                    return this.GetColumn("Username");
                }
            }
            				
   			public static string UsernameColumn{
			      get{
        			return "Username";
      			}
		    }
           
            public IColumn Password{
                get{
                    return this.GetColumn("Password");
                }
            }
            				
   			public static string PasswordColumn{
			      get{
        			return "Password";
      			}
		    }
           
            public IColumn FirstName{
                get{
                    return this.GetColumn("FirstName");
                }
            }
            				
   			public static string FirstNameColumn{
			      get{
        			return "FirstName";
      			}
		    }
           
            public IColumn LastName{
                get{
                    return this.GetColumn("LastName");
                }
            }
            				
   			public static string LastNameColumn{
			      get{
        			return "LastName";
      			}
		    }
           
            public IColumn EmailAddress{
                get{
                    return this.GetColumn("EmailAddress");
                }
            }
            				
   			public static string EmailAddressColumn{
			      get{
        			return "EmailAddress";
      			}
		    }
           
            public IColumn CreatedDate{
                get{
                    return this.GetColumn("CreatedDate");
                }
            }
            				
   			public static string CreatedDateColumn{
			      get{
        			return "CreatedDate";
      			}
		    }
           
            public IColumn DateOfBirth{
                get{
                    return this.GetColumn("DateOfBirth");
                }
            }
            				
   			public static string DateOfBirthColumn{
			      get{
        			return "DateOfBirth";
      			}
		    }
           
            public IColumn ForgotPasswordHash{
                get{
                    return this.GetColumn("ForgotPasswordHash");
                }
            }
            				
   			public static string ForgotPasswordHashColumn{
			      get{
        			return "ForgotPasswordHash";
      			}
		    }
           
            public IColumn ProfilePicture{
                get{
                    return this.GetColumn("ProfilePicture");
                }
            }
            				
   			public static string ProfilePictureColumn{
			      get{
        			return "ProfilePicture";
      			}
		    }
           
            public IColumn TimeZoneOffSet{
                get{
                    return this.GetColumn("TimeZoneOffSet");
                }
            }
            				
   			public static string TimeZoneOffSetColumn{
			      get{
        			return "TimeZoneOffSet";
      			}
		    }
           
            public IColumn IsActive{
                get{
                    return this.GetColumn("IsActive");
                }
            }
            				
   			public static string IsActiveColumn{
			      get{
        			return "IsActive";
      			}
		    }
           
                    
        }
        
        /// <summary>
        /// Table: APIApplication
        /// Primary Key: ID
        /// </summary>

        public class APIApplicationTable: DatabaseTable {
            
            public APIApplicationTable(IDataProvider provider):base("APIApplication",provider){
                ClassName = "APIApplication";
                SchemaName = "dbo";
                

                Columns.Add(new DatabaseColumn("ID", this)
                {
	                IsPrimaryKey = true,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = true,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("UserID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Guid,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("ClientID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = -1
                });

                Columns.Add(new DatabaseColumn("ClientSecret", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = -1
                });

                Columns.Add(new DatabaseColumn("Name", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 100
                });

                Columns.Add(new DatabaseColumn("Description", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("Website", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 100
                });

                Columns.Add(new DatabaseColumn("IsActive", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Boolean,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });
                    
                
                
            }
            
            public IColumn ID{
                get{
                    return this.GetColumn("ID");
                }
            }
            				
   			public static string IDColumn{
			      get{
        			return "ID";
      			}
		    }
           
            public IColumn UserID{
                get{
                    return this.GetColumn("UserID");
                }
            }
            				
   			public static string UserIDColumn{
			      get{
        			return "UserID";
      			}
		    }
           
            public IColumn ClientID{
                get{
                    return this.GetColumn("ClientID");
                }
            }
            				
   			public static string ClientIDColumn{
			      get{
        			return "ClientID";
      			}
		    }
           
            public IColumn ClientSecret{
                get{
                    return this.GetColumn("ClientSecret");
                }
            }
            				
   			public static string ClientSecretColumn{
			      get{
        			return "ClientSecret";
      			}
		    }
           
            public IColumn Name{
                get{
                    return this.GetColumn("Name");
                }
            }
            				
   			public static string NameColumn{
			      get{
        			return "Name";
      			}
		    }
           
            public IColumn Description{
                get{
                    return this.GetColumn("Description");
                }
            }
            				
   			public static string DescriptionColumn{
			      get{
        			return "Description";
      			}
		    }
           
            public IColumn Website{
                get{
                    return this.GetColumn("Website");
                }
            }
            				
   			public static string WebsiteColumn{
			      get{
        			return "Website";
      			}
		    }
           
            public IColumn IsActive{
                get{
                    return this.GetColumn("IsActive");
                }
            }
            				
   			public static string IsActiveColumn{
			      get{
        			return "IsActive";
      			}
		    }
           
                    
        }
        
        /// <summary>
        /// Table: Tweets
        /// Primary Key: ID
        /// </summary>

        public class TweetsTable: DatabaseTable {
            
            public TweetsTable(IDataProvider provider):base("Tweets",provider){
                ClassName = "Tweet";
                SchemaName = "dbo";
                

                Columns.Add(new DatabaseColumn("ID", this)
                {
	                IsPrimaryKey = true,
	                DataType = DbType.Int64,
	                IsNullable = false,
	                AutoIncrement = true,
	                IsForeignKey = true,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("TwitterID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int64,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("EventID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = true,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("Text", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 1000
                });

                Columns.Add(new DatabaseColumn("TextAsHTML", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = -1
                });

                Columns.Add(new DatabaseColumn("Source", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 500
                });

                Columns.Add(new DatabaseColumn("CreatedDate", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.DateTime,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("ToUserID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int64,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("FromUserID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int64,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("FromUserScreenName", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 500
                });

                Columns.Add(new DatabaseColumn("ToUserScreenName", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 500
                });

                Columns.Add(new DatabaseColumn("IsoLanguageCode", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 100
                });

                Columns.Add(new DatabaseColumn("ProfileImageURL", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 500
                });

                Columns.Add(new DatabaseColumn("SinceID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int64,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("Location", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 500
                });

                Columns.Add(new DatabaseColumn("RawSource", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 1000
                });

                Columns.Add(new DatabaseColumn("DeleteVoteCount", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("Deleted", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Boolean,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });
                    
                
                
            }
            
            public IColumn ID{
                get{
                    return this.GetColumn("ID");
                }
            }
            				
   			public static string IDColumn{
			      get{
        			return "ID";
      			}
		    }
           
            public IColumn TwitterID{
                get{
                    return this.GetColumn("TwitterID");
                }
            }
            				
   			public static string TwitterIDColumn{
			      get{
        			return "TwitterID";
      			}
		    }
           
            public IColumn EventID{
                get{
                    return this.GetColumn("EventID");
                }
            }
            				
   			public static string EventIDColumn{
			      get{
        			return "EventID";
      			}
		    }
           
            public IColumn Text{
                get{
                    return this.GetColumn("Text");
                }
            }
            				
   			public static string TextColumn{
			      get{
        			return "Text";
      			}
		    }
           
            public IColumn TextAsHTML{
                get{
                    return this.GetColumn("TextAsHTML");
                }
            }
            				
   			public static string TextAsHTMLColumn{
			      get{
        			return "TextAsHTML";
      			}
		    }
           
            public IColumn Source{
                get{
                    return this.GetColumn("Source");
                }
            }
            				
   			public static string SourceColumn{
			      get{
        			return "Source";
      			}
		    }
           
            public IColumn CreatedDate{
                get{
                    return this.GetColumn("CreatedDate");
                }
            }
            				
   			public static string CreatedDateColumn{
			      get{
        			return "CreatedDate";
      			}
		    }
           
            public IColumn ToUserID{
                get{
                    return this.GetColumn("ToUserID");
                }
            }
            				
   			public static string ToUserIDColumn{
			      get{
        			return "ToUserID";
      			}
		    }
           
            public IColumn FromUserID{
                get{
                    return this.GetColumn("FromUserID");
                }
            }
            				
   			public static string FromUserIDColumn{
			      get{
        			return "FromUserID";
      			}
		    }
           
            public IColumn FromUserScreenName{
                get{
                    return this.GetColumn("FromUserScreenName");
                }
            }
            				
   			public static string FromUserScreenNameColumn{
			      get{
        			return "FromUserScreenName";
      			}
		    }
           
            public IColumn ToUserScreenName{
                get{
                    return this.GetColumn("ToUserScreenName");
                }
            }
            				
   			public static string ToUserScreenNameColumn{
			      get{
        			return "ToUserScreenName";
      			}
		    }
           
            public IColumn IsoLanguageCode{
                get{
                    return this.GetColumn("IsoLanguageCode");
                }
            }
            				
   			public static string IsoLanguageCodeColumn{
			      get{
        			return "IsoLanguageCode";
      			}
		    }
           
            public IColumn ProfileImageURL{
                get{
                    return this.GetColumn("ProfileImageURL");
                }
            }
            				
   			public static string ProfileImageURLColumn{
			      get{
        			return "ProfileImageURL";
      			}
		    }
           
            public IColumn SinceID{
                get{
                    return this.GetColumn("SinceID");
                }
            }
            				
   			public static string SinceIDColumn{
			      get{
        			return "SinceID";
      			}
		    }
           
            public IColumn Location{
                get{
                    return this.GetColumn("Location");
                }
            }
            				
   			public static string LocationColumn{
			      get{
        			return "Location";
      			}
		    }
           
            public IColumn RawSource{
                get{
                    return this.GetColumn("RawSource");
                }
            }
            				
   			public static string RawSourceColumn{
			      get{
        			return "RawSource";
      			}
		    }
           
            public IColumn DeleteVoteCount{
                get{
                    return this.GetColumn("DeleteVoteCount");
                }
            }
            				
   			public static string DeleteVoteCountColumn{
			      get{
        			return "DeleteVoteCount";
      			}
		    }
           
            public IColumn Deleted{
                get{
                    return this.GetColumn("Deleted");
                }
            }
            				
   			public static string DeletedColumn{
			      get{
        			return "Deleted";
      			}
		    }
           
                    
        }
        
        /// <summary>
        /// Table: ScraperURLs
        /// Primary Key: ID
        /// </summary>

        public class ScraperURLsTable: DatabaseTable {
            
            public ScraperURLsTable(IDataProvider provider):base("ScraperURLs",provider){
                ClassName = "ScraperURL";
                SchemaName = "dbo";
                

                Columns.Add(new DatabaseColumn("ID", this)
                {
	                IsPrimaryKey = true,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = true,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("URL", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 1000
                });
                    
                
                
            }
            
            public IColumn ID{
                get{
                    return this.GetColumn("ID");
                }
            }
            				
   			public static string IDColumn{
			      get{
        			return "ID";
      			}
		    }
           
            public IColumn URL{
                get{
                    return this.GetColumn("URL");
                }
            }
            				
   			public static string URLColumn{
			      get{
        			return "URL";
      			}
		    }
           
                    
        }
        
        /// <summary>
        /// Table: URLs
        /// Primary Key: ID
        /// </summary>

        public class URLsTable: DatabaseTable {
            
            public URLsTable(IDataProvider provider):base("URLs",provider){
                ClassName = "URL";
                SchemaName = "dbo";
                

                Columns.Add(new DatabaseColumn("ID", this)
                {
	                IsPrimaryKey = true,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = true,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("EventID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = true,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("TweetID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int64,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = true,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("ShortURL", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 1000
                });

                Columns.Add(new DatabaseColumn("FullURL", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 1000
                });

                Columns.Add(new DatabaseColumn("Type", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 500
                });

                Columns.Add(new DatabaseColumn("DateTime", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.DateTime,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("DeleteVoteCount", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("Deleted", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Boolean,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });
                    
                
                
            }
            
            public IColumn ID{
                get{
                    return this.GetColumn("ID");
                }
            }
            				
   			public static string IDColumn{
			      get{
        			return "ID";
      			}
		    }
           
            public IColumn EventID{
                get{
                    return this.GetColumn("EventID");
                }
            }
            				
   			public static string EventIDColumn{
			      get{
        			return "EventID";
      			}
		    }
           
            public IColumn TweetID{
                get{
                    return this.GetColumn("TweetID");
                }
            }
            				
   			public static string TweetIDColumn{
			      get{
        			return "TweetID";
      			}
		    }
           
            public IColumn ShortURL{
                get{
                    return this.GetColumn("ShortURL");
                }
            }
            				
   			public static string ShortURLColumn{
			      get{
        			return "ShortURL";
      			}
		    }
           
            public IColumn FullURL{
                get{
                    return this.GetColumn("FullURL");
                }
            }
            				
   			public static string FullURLColumn{
			      get{
        			return "FullURL";
      			}
		    }
           
            public IColumn Type{
                get{
                    return this.GetColumn("Type");
                }
            }
            				
   			public static string TypeColumn{
			      get{
        			return "Type";
      			}
		    }
           
            public IColumn DateTime{
                get{
                    return this.GetColumn("DateTime");
                }
            }
            				
   			public static string DateTimeColumn{
			      get{
        			return "DateTime";
      			}
		    }
           
            public IColumn DeleteVoteCount{
                get{
                    return this.GetColumn("DeleteVoteCount");
                }
            }
            				
   			public static string DeleteVoteCountColumn{
			      get{
        			return "DeleteVoteCount";
      			}
		    }
           
            public IColumn Deleted{
                get{
                    return this.GetColumn("Deleted");
                }
            }
            				
   			public static string DeletedColumn{
			      get{
        			return "Deleted";
      			}
		    }
           
                    
        }
        
        /// <summary>
        /// Table: aspnet_Users
        /// Primary Key: UserId
        /// </summary>

        public class aspnet_UsersTable: DatabaseTable {
            
            public aspnet_UsersTable(IDataProvider provider):base("aspnet_Users",provider){
                ClassName = "aspnet_User";
                SchemaName = "dbo";
                

                Columns.Add(new DatabaseColumn("UserId", this)
                {
	                IsPrimaryKey = true,
	                DataType = DbType.Guid,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("UserName", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 256
                });

                Columns.Add(new DatabaseColumn("LoweredUserName", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 256
                });

                Columns.Add(new DatabaseColumn("MobileAlias", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 16
                });

                Columns.Add(new DatabaseColumn("IsAnonymous", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Boolean,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("LastActivityDate", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.DateTime,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });
                    
                
                
            }
            
            public IColumn UserId{
                get{
                    return this.GetColumn("UserId");
                }
            }
            				
   			public static string UserIdColumn{
			      get{
        			return "UserId";
      			}
		    }
           
            public IColumn UserName{
                get{
                    return this.GetColumn("UserName");
                }
            }
            				
   			public static string UserNameColumn{
			      get{
        			return "UserName";
      			}
		    }
           
            public IColumn LoweredUserName{
                get{
                    return this.GetColumn("LoweredUserName");
                }
            }
            				
   			public static string LoweredUserNameColumn{
			      get{
        			return "LoweredUserName";
      			}
		    }
           
            public IColumn MobileAlias{
                get{
                    return this.GetColumn("MobileAlias");
                }
            }
            				
   			public static string MobileAliasColumn{
			      get{
        			return "MobileAlias";
      			}
		    }
           
            public IColumn IsAnonymous{
                get{
                    return this.GetColumn("IsAnonymous");
                }
            }
            				
   			public static string IsAnonymousColumn{
			      get{
        			return "IsAnonymous";
      			}
		    }
           
            public IColumn LastActivityDate{
                get{
                    return this.GetColumn("LastActivityDate");
                }
            }
            				
   			public static string LastActivityDateColumn{
			      get{
        			return "LastActivityDate";
      			}
		    }
           
                    
        }
        
        /// <summary>
        /// Table: UserAuthenticationProfile
        /// Primary Key: ID
        /// </summary>

        public class UserAuthenticationProfileTable: DatabaseTable {
            
            public UserAuthenticationProfileTable(IDataProvider provider):base("UserAuthenticationProfile",provider){
                ClassName = "UserAuthenticationProfile";
                SchemaName = "dbo";
                

                Columns.Add(new DatabaseColumn("ID", this)
                {
	                IsPrimaryKey = true,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = true,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("UserID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Guid,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = true,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("Service", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 100
                });

                Columns.Add(new DatabaseColumn("Platform", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 20
                });

                Columns.Add(new DatabaseColumn("ServiceUsername", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 250
                });

                Columns.Add(new DatabaseColumn("Token", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 500
                });

                Columns.Add(new DatabaseColumn("TokenSecret", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 500
                });
                    
                
                
            }
            
            public IColumn ID{
                get{
                    return this.GetColumn("ID");
                }
            }
            				
   			public static string IDColumn{
			      get{
        			return "ID";
      			}
		    }
           
            public IColumn UserID{
                get{
                    return this.GetColumn("UserID");
                }
            }
            				
   			public static string UserIDColumn{
			      get{
        			return "UserID";
      			}
		    }
           
            public IColumn Service{
                get{
                    return this.GetColumn("Service");
                }
            }
            				
   			public static string ServiceColumn{
			      get{
        			return "Service";
      			}
		    }
           
            public IColumn Platform{
                get{
                    return this.GetColumn("Platform");
                }
            }
            				
   			public static string PlatformColumn{
			      get{
        			return "Platform";
      			}
		    }
           
            public IColumn ServiceUsername{
                get{
                    return this.GetColumn("ServiceUsername");
                }
            }
            				
   			public static string ServiceUsernameColumn{
			      get{
        			return "ServiceUsername";
      			}
		    }
           
            public IColumn Token{
                get{
                    return this.GetColumn("Token");
                }
            }
            				
   			public static string TokenColumn{
			      get{
        			return "Token";
      			}
		    }
           
            public IColumn TokenSecret{
                get{
                    return this.GetColumn("TokenSecret");
                }
            }
            				
   			public static string TokenSecretColumn{
			      get{
        			return "TokenSecret";
      			}
		    }
           
                    
        }
        
        /// <summary>
        /// Table: BetaSignups
        /// Primary Key: ID
        /// </summary>

        public class BetaSignupsTable: DatabaseTable {
            
            public BetaSignupsTable(IDataProvider provider):base("BetaSignups",provider){
                ClassName = "BetaSignup";
                SchemaName = "dbo";
                

                Columns.Add(new DatabaseColumn("ID", this)
                {
	                IsPrimaryKey = true,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = true,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("EmailAddress", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 255
                });

                Columns.Add(new DatabaseColumn("IPAddress", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("DateTimeSubmitted", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.DateTime,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });
                    
                
                
            }
            
            public IColumn ID{
                get{
                    return this.GetColumn("ID");
                }
            }
            				
   			public static string IDColumn{
			      get{
        			return "ID";
      			}
		    }
           
            public IColumn EmailAddress{
                get{
                    return this.GetColumn("EmailAddress");
                }
            }
            				
   			public static string EmailAddressColumn{
			      get{
        			return "EmailAddress";
      			}
		    }
           
            public IColumn IPAddress{
                get{
                    return this.GetColumn("IPAddress");
                }
            }
            				
   			public static string IPAddressColumn{
			      get{
        			return "IPAddress";
      			}
		    }
           
            public IColumn DateTimeSubmitted{
                get{
                    return this.GetColumn("DateTimeSubmitted");
                }
            }
            				
   			public static string DateTimeSubmittedColumn{
			      get{
        			return "DateTimeSubmitted";
      			}
		    }
           
                    
        }
        
        /// <summary>
        /// Table: MemoryBoxItems
        /// Primary Key: ID
        /// </summary>

        public class MemoryBoxItemsTable: DatabaseTable {
            
            public MemoryBoxItemsTable(IDataProvider provider):base("MemoryBoxItems",provider){
                ClassName = "MemoryBoxItem";
                SchemaName = "dbo";
                

                Columns.Add(new DatabaseColumn("ID", this)
                {
	                IsPrimaryKey = true,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = true,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("MemboxId", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = true,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("EventId", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = true,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("UserId", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Guid,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = true,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("ItemType", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("ItemId", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 100
                });

                Columns.Add(new DatabaseColumn("AddedDateTime", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.DateTime,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });
                    
                
                
            }
            
            public IColumn ID{
                get{
                    return this.GetColumn("ID");
                }
            }
            				
   			public static string IDColumn{
			      get{
        			return "ID";
      			}
		    }
           
            public IColumn MemboxId{
                get{
                    return this.GetColumn("MemboxId");
                }
            }
            				
   			public static string MemboxIdColumn{
			      get{
        			return "MemboxId";
      			}
		    }
           
            public IColumn EventId{
                get{
                    return this.GetColumn("EventId");
                }
            }
            				
   			public static string EventIdColumn{
			      get{
        			return "EventId";
      			}
		    }
           
            public IColumn UserId{
                get{
                    return this.GetColumn("UserId");
                }
            }
            				
   			public static string UserIdColumn{
			      get{
        			return "UserId";
      			}
		    }
           
            public IColumn ItemType{
                get{
                    return this.GetColumn("ItemType");
                }
            }
            				
   			public static string ItemTypeColumn{
			      get{
        			return "ItemType";
      			}
		    }
           
            public IColumn ItemId{
                get{
                    return this.GetColumn("ItemId");
                }
            }
            				
   			public static string ItemIdColumn{
			      get{
        			return "ItemId";
      			}
		    }
           
            public IColumn AddedDateTime{
                get{
                    return this.GetColumn("AddedDateTime");
                }
            }
            				
   			public static string AddedDateTimeColumn{
			      get{
        			return "AddedDateTime";
      			}
		    }
           
                    
        }
        
        /// <summary>
        /// Table: UserFollowsEvent
        /// Primary Key: ID
        /// </summary>

        public class UserFollowsEventTable: DatabaseTable {
            
            public UserFollowsEventTable(IDataProvider provider):base("UserFollowsEvent",provider){
                ClassName = "UserFollowsEvent";
                SchemaName = "dbo";
                

                Columns.Add(new DatabaseColumn("ID", this)
                {
	                IsPrimaryKey = true,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = true,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("UserID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Guid,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = true,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("EventID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = true,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("Timestamp", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.DateTime,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });
                    
                
                
            }
            
            public IColumn ID{
                get{
                    return this.GetColumn("ID");
                }
            }
            				
   			public static string IDColumn{
			      get{
        			return "ID";
      			}
		    }
           
            public IColumn UserID{
                get{
                    return this.GetColumn("UserID");
                }
            }
            				
   			public static string UserIDColumn{
			      get{
        			return "UserID";
      			}
		    }
           
            public IColumn EventID{
                get{
                    return this.GetColumn("EventID");
                }
            }
            				
   			public static string EventIDColumn{
			      get{
        			return "EventID";
      			}
		    }
           
            public IColumn Timestamp{
                get{
                    return this.GetColumn("Timestamp");
                }
            }
            				
   			public static string TimestampColumn{
			      get{
        			return "Timestamp";
      			}
		    }
           
                    
        }
        
        /// <summary>
        /// Table: StatusMessages
        /// Primary Key: ID
        /// </summary>

        public class StatusMessagesTable: DatabaseTable {
            
            public StatusMessagesTable(IDataProvider provider):base("StatusMessages",provider){
                ClassName = "StatusMessage";
                SchemaName = "dbo";
                

                Columns.Add(new DatabaseColumn("ID", this)
                {
	                IsPrimaryKey = true,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = true,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("MSGDateTime", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.DateTime,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("MSG", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 500
                });
                    
                
                
            }
            
            public IColumn ID{
                get{
                    return this.GetColumn("ID");
                }
            }
            				
   			public static string IDColumn{
			      get{
        			return "ID";
      			}
		    }
           
            public IColumn MSGDateTime{
                get{
                    return this.GetColumn("MSGDateTime");
                }
            }
            				
   			public static string MSGDateTimeColumn{
			      get{
        			return "MSGDateTime";
      			}
		    }
           
            public IColumn MSG{
                get{
                    return this.GetColumn("MSG");
                }
            }
            				
   			public static string MSGColumn{
			      get{
        			return "MSG";
      			}
		    }
           
                    
        }
        
        /// <summary>
        /// Table: UserClickActions
        /// Primary Key: ID
        /// </summary>

        public class UserClickActionsTable: DatabaseTable {
            
            public UserClickActionsTable(IDataProvider provider):base("UserClickActions",provider){
                ClassName = "UserClickAction";
                SchemaName = "dbo";
                

                Columns.Add(new DatabaseColumn("ID", this)
                {
	                IsPrimaryKey = true,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = true,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("UserID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Guid,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("EventID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("ActionDateTime", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.DateTime,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("href", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = -1
                });

                Columns.Add(new DatabaseColumn("Host", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 255
                });

                Columns.Add(new DatabaseColumn("PathName", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = -1
                });

                Columns.Add(new DatabaseColumn("UserAgent", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 255
                });

                Columns.Add(new DatabaseColumn("IPAddress", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });
                    
                
                
            }
            
            public IColumn ID{
                get{
                    return this.GetColumn("ID");
                }
            }
            				
   			public static string IDColumn{
			      get{
        			return "ID";
      			}
		    }
           
            public IColumn UserID{
                get{
                    return this.GetColumn("UserID");
                }
            }
            				
   			public static string UserIDColumn{
			      get{
        			return "UserID";
      			}
		    }
           
            public IColumn EventID{
                get{
                    return this.GetColumn("EventID");
                }
            }
            				
   			public static string EventIDColumn{
			      get{
        			return "EventID";
      			}
		    }
           
            public IColumn ActionDateTime{
                get{
                    return this.GetColumn("ActionDateTime");
                }
            }
            				
   			public static string ActionDateTimeColumn{
			      get{
        			return "ActionDateTime";
      			}
		    }
           
            public IColumn href{
                get{
                    return this.GetColumn("href");
                }
            }
            				
   			public static string hrefColumn{
			      get{
        			return "href";
      			}
		    }
           
            public IColumn Host{
                get{
                    return this.GetColumn("Host");
                }
            }
            				
   			public static string HostColumn{
			      get{
        			return "Host";
      			}
		    }
           
            public IColumn PathName{
                get{
                    return this.GetColumn("PathName");
                }
            }
            				
   			public static string PathNameColumn{
			      get{
        			return "PathName";
      			}
		    }
           
            public IColumn UserAgent{
                get{
                    return this.GetColumn("UserAgent");
                }
            }
            				
   			public static string UserAgentColumn{
			      get{
        			return "UserAgent";
      			}
		    }
           
            public IColumn IPAddress{
                get{
                    return this.GetColumn("IPAddress");
                }
            }
            				
   			public static string IPAddressColumn{
			      get{
        			return "IPAddress";
      			}
		    }
           
                    
        }
        
        /// <summary>
        /// Table: CheckIns
        /// Primary Key: ID
        /// </summary>

        public class CheckInsTable: DatabaseTable {
            
            public CheckInsTable(IDataProvider provider):base("CheckIns",provider){
                ClassName = "CheckIn";
                SchemaName = "dbo";
                

                Columns.Add(new DatabaseColumn("ID", this)
                {
	                IsPrimaryKey = true,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = true,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("EventID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = true,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("TweetID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int64,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = true,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("UserID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("CheckInDateTime", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.DateTime,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("FourSquareCheckInURL", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 255
                });
                    
                
                
            }
            
            public IColumn ID{
                get{
                    return this.GetColumn("ID");
                }
            }
            				
   			public static string IDColumn{
			      get{
        			return "ID";
      			}
		    }
           
            public IColumn EventID{
                get{
                    return this.GetColumn("EventID");
                }
            }
            				
   			public static string EventIDColumn{
			      get{
        			return "EventID";
      			}
		    }
           
            public IColumn TweetID{
                get{
                    return this.GetColumn("TweetID");
                }
            }
            				
   			public static string TweetIDColumn{
			      get{
        			return "TweetID";
      			}
		    }
           
            public IColumn UserID{
                get{
                    return this.GetColumn("UserID");
                }
            }
            				
   			public static string UserIDColumn{
			      get{
        			return "UserID";
      			}
		    }
           
            public IColumn CheckInDateTime{
                get{
                    return this.GetColumn("CheckInDateTime");
                }
            }
            				
   			public static string CheckInDateTimeColumn{
			      get{
        			return "CheckInDateTime";
      			}
		    }
           
            public IColumn FourSquareCheckInURL{
                get{
                    return this.GetColumn("FourSquareCheckInURL");
                }
            }
            				
   			public static string FourSquareCheckInURLColumn{
			      get{
        			return "FourSquareCheckInURL";
      			}
		    }
           
                    
        }
        
        /// <summary>
        /// Table: Roles
        /// Primary Key: ID
        /// </summary>

        public class RolesTable: DatabaseTable {
            
            public RolesTable(IDataProvider provider):base("Roles",provider){
                ClassName = "Role";
                SchemaName = "dbo";
                

                Columns.Add(new DatabaseColumn("ID", this)
                {
	                IsPrimaryKey = true,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = true,
	                IsForeignKey = true,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("Role", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });
                    
                
                
            }
            
            public IColumn ID{
                get{
                    return this.GetColumn("ID");
                }
            }
            				
   			public static string IDColumn{
			      get{
        			return "ID";
      			}
		    }
           
            public IColumn Role{
                get{
                    return this.GetColumn("Role");
                }
            }
            				
   			public static string RoleColumn{
			      get{
        			return "Role";
      			}
		    }
           
                    
        }
        
        /// <summary>
        /// Table: userClickTracking
        /// Primary Key: ID
        /// </summary>

        public class userClickTrackingTable: DatabaseTable {
            
            public userClickTrackingTable(IDataProvider provider):base("userClickTracking",provider){
                ClassName = "userClickTracking";
                SchemaName = "dbo";
                

                Columns.Add(new DatabaseColumn("ID", this)
                {
	                IsPrimaryKey = true,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = true,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("UserID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Guid,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("ClickDateTime", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.DateTime,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("x", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int16,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("y", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int16,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("location", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 255
                });
                    
                
                
            }
            
            public IColumn ID{
                get{
                    return this.GetColumn("ID");
                }
            }
            				
   			public static string IDColumn{
			      get{
        			return "ID";
      			}
		    }
           
            public IColumn UserID{
                get{
                    return this.GetColumn("UserID");
                }
            }
            				
   			public static string UserIDColumn{
			      get{
        			return "UserID";
      			}
		    }
           
            public IColumn ClickDateTime{
                get{
                    return this.GetColumn("ClickDateTime");
                }
            }
            				
   			public static string ClickDateTimeColumn{
			      get{
        			return "ClickDateTime";
      			}
		    }
           
            public IColumn x{
                get{
                    return this.GetColumn("x");
                }
            }
            				
   			public static string xColumn{
			      get{
        			return "x";
      			}
		    }
           
            public IColumn y{
                get{
                    return this.GetColumn("y");
                }
            }
            				
   			public static string yColumn{
			      get{
        			return "y";
      			}
		    }
           
            public IColumn location{
                get{
                    return this.GetColumn("location");
                }
            }
            				
   			public static string locationColumn{
			      get{
        			return "location";
      			}
		    }
           
                    
        }
        
        /// <summary>
        /// Table: Venues
        /// Primary Key: ID
        /// </summary>

        public class VenuesTable: DatabaseTable {
            
            public VenuesTable(IDataProvider provider):base("Venues",provider){
                ClassName = "Venue";
                SchemaName = "dbo";
                

                Columns.Add(new DatabaseColumn("ID", this)
                {
	                IsPrimaryKey = true,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = true,
	                IsForeignKey = true,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("FoursquareVenueID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 500
                });

                Columns.Add(new DatabaseColumn("Address", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 1000
                });

                Columns.Add(new DatabaseColumn("City", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("CrossStreet", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 1000
                });

                Columns.Add(new DatabaseColumn("Geolat", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Double,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("Geolong", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Double,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("Name", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 500
                });

                Columns.Add(new DatabaseColumn("Phone", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("State", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("Zip", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 20
                });
                    
                
                
            }
            
            public IColumn ID{
                get{
                    return this.GetColumn("ID");
                }
            }
            				
   			public static string IDColumn{
			      get{
        			return "ID";
      			}
		    }
           
            public IColumn FoursquareVenueID{
                get{
                    return this.GetColumn("FoursquareVenueID");
                }
            }
            				
   			public static string FoursquareVenueIDColumn{
			      get{
        			return "FoursquareVenueID";
      			}
		    }
           
            public IColumn Address{
                get{
                    return this.GetColumn("Address");
                }
            }
            				
   			public static string AddressColumn{
			      get{
        			return "Address";
      			}
		    }
           
            public IColumn City{
                get{
                    return this.GetColumn("City");
                }
            }
            				
   			public static string CityColumn{
			      get{
        			return "City";
      			}
		    }
           
            public IColumn CrossStreet{
                get{
                    return this.GetColumn("CrossStreet");
                }
            }
            				
   			public static string CrossStreetColumn{
			      get{
        			return "CrossStreet";
      			}
		    }
           
            public IColumn Geolat{
                get{
                    return this.GetColumn("Geolat");
                }
            }
            				
   			public static string GeolatColumn{
			      get{
        			return "Geolat";
      			}
		    }
           
            public IColumn Geolong{
                get{
                    return this.GetColumn("Geolong");
                }
            }
            				
   			public static string GeolongColumn{
			      get{
        			return "Geolong";
      			}
		    }
           
            public IColumn Name{
                get{
                    return this.GetColumn("Name");
                }
            }
            				
   			public static string NameColumn{
			      get{
        			return "Name";
      			}
		    }
           
            public IColumn Phone{
                get{
                    return this.GetColumn("Phone");
                }
            }
            				
   			public static string PhoneColumn{
			      get{
        			return "Phone";
      			}
		    }
           
            public IColumn State{
                get{
                    return this.GetColumn("State");
                }
            }
            				
   			public static string StateColumn{
			      get{
        			return "State";
      			}
		    }
           
            public IColumn Zip{
                get{
                    return this.GetColumn("Zip");
                }
            }
            				
   			public static string ZipColumn{
			      get{
        			return "Zip";
      			}
		    }
           
                    
        }
        
        /// <summary>
        /// Table: UserInRoles
        /// Primary Key: ID
        /// </summary>

        public class UserInRolesTable: DatabaseTable {
            
            public UserInRolesTable(IDataProvider provider):base("UserInRoles",provider){
                ClassName = "UserInRole";
                SchemaName = "dbo";
                

                Columns.Add(new DatabaseColumn("ID", this)
                {
	                IsPrimaryKey = true,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = true,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("UserID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Guid,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = true,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("RoleID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = true,
	                MaxLength = 0
                });
                    
                
                
            }
            
            public IColumn ID{
                get{
                    return this.GetColumn("ID");
                }
            }
            				
   			public static string IDColumn{
			      get{
        			return "ID";
      			}
		    }
           
            public IColumn UserID{
                get{
                    return this.GetColumn("UserID");
                }
            }
            				
   			public static string UserIDColumn{
			      get{
        			return "UserID";
      			}
		    }
           
            public IColumn RoleID{
                get{
                    return this.GetColumn("RoleID");
                }
            }
            				
   			public static string RoleIDColumn{
			      get{
        			return "RoleID";
      			}
		    }
           
                    
        }
        
        /// <summary>
        /// Table: Events
        /// Primary Key: ID
        /// </summary>

        public class EventsTable: DatabaseTable {
            
            public EventsTable(IDataProvider provider):base("Events",provider){
                ClassName = "Event";
                SchemaName = "dbo";
                

                Columns.Add(new DatabaseColumn("ID", this)
                {
	                IsPrimaryKey = true,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = true,
	                IsForeignKey = true,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("EventSlug", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 255
                });

                Columns.Add(new DatabaseColumn("UserID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Guid,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = true,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("CreatedDateTime", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.DateTime,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("Name", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 100
                });

                Columns.Add(new DatabaseColumn("SubTitle", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 500
                });

                Columns.Add(new DatabaseColumn("Description", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.AnsiString,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 2147483647
                });

                Columns.Add(new DatabaseColumn("CategoryID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = true,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("WebsiteURL", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 500
                });

                Columns.Add(new DatabaseColumn("Cost", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 500
                });

                Columns.Add(new DatabaseColumn("StartDateTime", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.DateTime,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("EndDateTime", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.DateTime,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("CollectionStartDateTime", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.DateTime,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("CollectionEndDateTime", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.DateTime,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("TimeZoneOffset", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("VenueID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = true,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("SearchTerms", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 1000
                });

                Columns.Add(new DatabaseColumn("NumberOfTweets", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int64,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("IsPrivate", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Boolean,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("IsAdult", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Boolean,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("CollectionMode", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("EventStatus", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("FacebookPageURL", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 500
                });

                Columns.Add(new DatabaseColumn("TwitterAccount", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 100
                });

                Columns.Add(new DatabaseColumn("IsActive", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Boolean,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("IsFeatured", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Boolean,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("FeaturedStartDateTime", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.DateTime,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("FeaturedEndDateTime", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.DateTime,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });
                    
                
                
            }
            
            public IColumn ID{
                get{
                    return this.GetColumn("ID");
                }
            }
            				
   			public static string IDColumn{
			      get{
        			return "ID";
      			}
		    }
           
            public IColumn EventSlug{
                get{
                    return this.GetColumn("EventSlug");
                }
            }
            				
   			public static string EventSlugColumn{
			      get{
        			return "EventSlug";
      			}
		    }
           
            public IColumn UserID{
                get{
                    return this.GetColumn("UserID");
                }
            }
            				
   			public static string UserIDColumn{
			      get{
        			return "UserID";
      			}
		    }
           
            public IColumn CreatedDateTime{
                get{
                    return this.GetColumn("CreatedDateTime");
                }
            }
            				
   			public static string CreatedDateTimeColumn{
			      get{
        			return "CreatedDateTime";
      			}
		    }
           
            public IColumn Name{
                get{
                    return this.GetColumn("Name");
                }
            }
            				
   			public static string NameColumn{
			      get{
        			return "Name";
      			}
		    }
           
            public IColumn SubTitle{
                get{
                    return this.GetColumn("SubTitle");
                }
            }
            				
   			public static string SubTitleColumn{
			      get{
        			return "SubTitle";
      			}
		    }
           
            public IColumn Description{
                get{
                    return this.GetColumn("Description");
                }
            }
            				
   			public static string DescriptionColumn{
			      get{
        			return "Description";
      			}
		    }
           
            public IColumn CategoryID{
                get{
                    return this.GetColumn("CategoryID");
                }
            }
            				
   			public static string CategoryIDColumn{
			      get{
        			return "CategoryID";
      			}
		    }
           
            public IColumn WebsiteURL{
                get{
                    return this.GetColumn("WebsiteURL");
                }
            }
            				
   			public static string WebsiteURLColumn{
			      get{
        			return "WebsiteURL";
      			}
		    }
           
            public IColumn Cost{
                get{
                    return this.GetColumn("Cost");
                }
            }
            				
   			public static string CostColumn{
			      get{
        			return "Cost";
      			}
		    }
           
            public IColumn StartDateTime{
                get{
                    return this.GetColumn("StartDateTime");
                }
            }
            				
   			public static string StartDateTimeColumn{
			      get{
        			return "StartDateTime";
      			}
		    }
           
            public IColumn EndDateTime{
                get{
                    return this.GetColumn("EndDateTime");
                }
            }
            				
   			public static string EndDateTimeColumn{
			      get{
        			return "EndDateTime";
      			}
		    }
           
            public IColumn CollectionStartDateTime{
                get{
                    return this.GetColumn("CollectionStartDateTime");
                }
            }
            				
   			public static string CollectionStartDateTimeColumn{
			      get{
        			return "CollectionStartDateTime";
      			}
		    }
           
            public IColumn CollectionEndDateTime{
                get{
                    return this.GetColumn("CollectionEndDateTime");
                }
            }
            				
   			public static string CollectionEndDateTimeColumn{
			      get{
        			return "CollectionEndDateTime";
      			}
		    }
           
            public IColumn TimeZoneOffset{
                get{
                    return this.GetColumn("TimeZoneOffset");
                }
            }
            				
   			public static string TimeZoneOffsetColumn{
			      get{
        			return "TimeZoneOffset";
      			}
		    }
           
            public IColumn VenueID{
                get{
                    return this.GetColumn("VenueID");
                }
            }
            				
   			public static string VenueIDColumn{
			      get{
        			return "VenueID";
      			}
		    }
           
            public IColumn SearchTerms{
                get{
                    return this.GetColumn("SearchTerms");
                }
            }
            				
   			public static string SearchTermsColumn{
			      get{
        			return "SearchTerms";
      			}
		    }
           
            public IColumn NumberOfTweets{
                get{
                    return this.GetColumn("NumberOfTweets");
                }
            }
            				
   			public static string NumberOfTweetsColumn{
			      get{
        			return "NumberOfTweets";
      			}
		    }
           
            public IColumn IsPrivate{
                get{
                    return this.GetColumn("IsPrivate");
                }
            }
            				
   			public static string IsPrivateColumn{
			      get{
        			return "IsPrivate";
      			}
		    }
           
            public IColumn IsAdult{
                get{
                    return this.GetColumn("IsAdult");
                }
            }
            				
   			public static string IsAdultColumn{
			      get{
        			return "IsAdult";
      			}
		    }
           
            public IColumn CollectionMode{
                get{
                    return this.GetColumn("CollectionMode");
                }
            }
            				
   			public static string CollectionModeColumn{
			      get{
        			return "CollectionMode";
      			}
		    }
           
            public IColumn EventStatus{
                get{
                    return this.GetColumn("EventStatus");
                }
            }
            				
   			public static string EventStatusColumn{
			      get{
        			return "EventStatus";
      			}
		    }
           
            public IColumn FacebookPageURL{
                get{
                    return this.GetColumn("FacebookPageURL");
                }
            }
            				
   			public static string FacebookPageURLColumn{
			      get{
        			return "FacebookPageURL";
      			}
		    }
           
            public IColumn TwitterAccount{
                get{
                    return this.GetColumn("TwitterAccount");
                }
            }
            				
   			public static string TwitterAccountColumn{
			      get{
        			return "TwitterAccount";
      			}
		    }
           
            public IColumn IsActive{
                get{
                    return this.GetColumn("IsActive");
                }
            }
            				
   			public static string IsActiveColumn{
			      get{
        			return "IsActive";
      			}
		    }
           
            public IColumn IsFeatured{
                get{
                    return this.GetColumn("IsFeatured");
                }
            }
            				
   			public static string IsFeaturedColumn{
			      get{
        			return "IsFeatured";
      			}
		    }
           
            public IColumn FeaturedStartDateTime{
                get{
                    return this.GetColumn("FeaturedStartDateTime");
                }
            }
            				
   			public static string FeaturedStartDateTimeColumn{
			      get{
        			return "FeaturedStartDateTime";
      			}
		    }
           
            public IColumn FeaturedEndDateTime{
                get{
                    return this.GetColumn("FeaturedEndDateTime");
                }
            }
            				
   			public static string FeaturedEndDateTimeColumn{
			      get{
        			return "FeaturedEndDateTime";
      			}
		    }
           
                    
        }
        
        /// <summary>
        /// Table: ImageMetaData
        /// Primary Key: ID
        /// </summary>

        public class ImageMetaDataTable: DatabaseTable {
            
            public ImageMetaDataTable(IDataProvider provider):base("ImageMetaData",provider){
                ClassName = "ImageMetaDatum";
                SchemaName = "dbo";
                

                Columns.Add(new DatabaseColumn("ID", this)
                {
	                IsPrimaryKey = true,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = true,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("ImageID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = true,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("EventID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = true,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("UserID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Guid,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("ImageSource", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 100
                });

                Columns.Add(new DatabaseColumn("TwitterID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int64,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("TwitterName", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 100
                });

                Columns.Add(new DatabaseColumn("DateTime", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.DateTime,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });
                    
                
                
            }
            
            public IColumn ID{
                get{
                    return this.GetColumn("ID");
                }
            }
            				
   			public static string IDColumn{
			      get{
        			return "ID";
      			}
		    }
           
            public IColumn ImageID{
                get{
                    return this.GetColumn("ImageID");
                }
            }
            				
   			public static string ImageIDColumn{
			      get{
        			return "ImageID";
      			}
		    }
           
            public IColumn EventID{
                get{
                    return this.GetColumn("EventID");
                }
            }
            				
   			public static string EventIDColumn{
			      get{
        			return "EventID";
      			}
		    }
           
            public IColumn UserID{
                get{
                    return this.GetColumn("UserID");
                }
            }
            				
   			public static string UserIDColumn{
			      get{
        			return "UserID";
      			}
		    }
           
            public IColumn ImageSource{
                get{
                    return this.GetColumn("ImageSource");
                }
            }
            				
   			public static string ImageSourceColumn{
			      get{
        			return "ImageSource";
      			}
		    }
           
            public IColumn TwitterID{
                get{
                    return this.GetColumn("TwitterID");
                }
            }
            				
   			public static string TwitterIDColumn{
			      get{
        			return "TwitterID";
      			}
		    }
           
            public IColumn TwitterName{
                get{
                    return this.GetColumn("TwitterName");
                }
            }
            				
   			public static string TwitterNameColumn{
			      get{
        			return "TwitterName";
      			}
		    }
           
            public IColumn DateTime{
                get{
                    return this.GetColumn("DateTime");
                }
            }
            				
   			public static string DateTimeColumn{
			      get{
        			return "DateTime";
      			}
		    }
           
                    
        }
        
        /// <summary>
        /// Table: Images
        /// Primary Key: ID
        /// </summary>

        public class ImagesTable: DatabaseTable {
            
            public ImagesTable(IDataProvider provider):base("Images",provider){
                ClassName = "Image";
                SchemaName = "dbo";
                

                Columns.Add(new DatabaseColumn("ID", this)
                {
	                IsPrimaryKey = true,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = true,
	                IsForeignKey = true,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("EventID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = true,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("AzureContainerPrefix", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("Fullsize", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 1000
                });

                Columns.Add(new DatabaseColumn("Thumb", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 1000
                });

                Columns.Add(new DatabaseColumn("OriginalImageLink", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 1000
                });

                Columns.Add(new DatabaseColumn("DateTime", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.DateTime,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("DeleteVoteCount", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("Deleted", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Boolean,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("ImageFingerPrint", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 100
                });
                    
                
                
            }
            
            public IColumn ID{
                get{
                    return this.GetColumn("ID");
                }
            }
            				
   			public static string IDColumn{
			      get{
        			return "ID";
      			}
		    }
           
            public IColumn EventID{
                get{
                    return this.GetColumn("EventID");
                }
            }
            				
   			public static string EventIDColumn{
			      get{
        			return "EventID";
      			}
		    }
           
            public IColumn AzureContainerPrefix{
                get{
                    return this.GetColumn("AzureContainerPrefix");
                }
            }
            				
   			public static string AzureContainerPrefixColumn{
			      get{
        			return "AzureContainerPrefix";
      			}
		    }
           
            public IColumn Fullsize{
                get{
                    return this.GetColumn("Fullsize");
                }
            }
            				
   			public static string FullsizeColumn{
			      get{
        			return "Fullsize";
      			}
		    }
           
            public IColumn Thumb{
                get{
                    return this.GetColumn("Thumb");
                }
            }
            				
   			public static string ThumbColumn{
			      get{
        			return "Thumb";
      			}
		    }
           
            public IColumn OriginalImageLink{
                get{
                    return this.GetColumn("OriginalImageLink");
                }
            }
            				
   			public static string OriginalImageLinkColumn{
			      get{
        			return "OriginalImageLink";
      			}
		    }
           
            public IColumn DateTime{
                get{
                    return this.GetColumn("DateTime");
                }
            }
            				
   			public static string DateTimeColumn{
			      get{
        			return "DateTime";
      			}
		    }
           
            public IColumn DeleteVoteCount{
                get{
                    return this.GetColumn("DeleteVoteCount");
                }
            }
            				
   			public static string DeleteVoteCountColumn{
			      get{
        			return "DeleteVoteCount";
      			}
		    }
           
            public IColumn Deleted{
                get{
                    return this.GetColumn("Deleted");
                }
            }
            				
   			public static string DeletedColumn{
			      get{
        			return "Deleted";
      			}
		    }
           
            public IColumn ImageFingerPrint{
                get{
                    return this.GetColumn("ImageFingerPrint");
                }
            }
            				
   			public static string ImageFingerPrintColumn{
			      get{
        			return "ImageFingerPrint";
      			}
		    }
           
                    
        }
        
        /// <summary>
        /// Table: UserTwitterActions
        /// Primary Key: ID
        /// </summary>

        public class UserTwitterActionsTable: DatabaseTable {
            
            public UserTwitterActionsTable(IDataProvider provider):base("UserTwitterActions",provider){
                ClassName = "UserTwitterAction";
                SchemaName = "dbo";
                

                Columns.Add(new DatabaseColumn("ID", this)
                {
	                IsPrimaryKey = true,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = true,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("UserId", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Guid,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("TweetId", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int64,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("TwitterAction", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 255
                });

                Columns.Add(new DatabaseColumn("DateTime", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.DateTime,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });
                    
                
                
            }
            
            public IColumn ID{
                get{
                    return this.GetColumn("ID");
                }
            }
            				
   			public static string IDColumn{
			      get{
        			return "ID";
      			}
		    }
           
            public IColumn UserId{
                get{
                    return this.GetColumn("UserId");
                }
            }
            				
   			public static string UserIdColumn{
			      get{
        			return "UserId";
      			}
		    }
           
            public IColumn TweetId{
                get{
                    return this.GetColumn("TweetId");
                }
            }
            				
   			public static string TweetIdColumn{
			      get{
        			return "TweetId";
      			}
		    }
           
            public IColumn TwitterAction{
                get{
                    return this.GetColumn("TwitterAction");
                }
            }
            				
   			public static string TwitterActionColumn{
			      get{
        			return "TwitterAction";
      			}
		    }
           
            public IColumn DateTime{
                get{
                    return this.GetColumn("DateTime");
                }
            }
            				
   			public static string DateTimeColumn{
			      get{
        			return "DateTime";
      			}
		    }
           
                    
        }
        
        /// <summary>
        /// Table: WidgetCustomSettings
        /// Primary Key: Id
        /// </summary>

        public class WidgetCustomSettingsTable: DatabaseTable {
            
            public WidgetCustomSettingsTable(IDataProvider provider):base("WidgetCustomSettings",provider){
                ClassName = "WidgetCustomSetting";
                SchemaName = "dbo";
                

                Columns.Add(new DatabaseColumn("Id", this)
                {
	                IsPrimaryKey = true,
	                DataType = DbType.Guid,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("EventId", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = true,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("Logo", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 255
                });

                Columns.Add(new DatabaseColumn("HeaderBGColor", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("HeaderTextColor", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("HeaderLinkColor", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("ContentBGColor", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("ContentTextColor", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("ContentLinkColor", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("SpriteColor", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });
                    
                
                
            }
            
            public IColumn Id{
                get{
                    return this.GetColumn("Id");
                }
            }
            				
   			public static string IdColumn{
			      get{
        			return "Id";
      			}
		    }
           
            public IColumn EventId{
                get{
                    return this.GetColumn("EventId");
                }
            }
            				
   			public static string EventIdColumn{
			      get{
        			return "EventId";
      			}
		    }
           
            public IColumn Logo{
                get{
                    return this.GetColumn("Logo");
                }
            }
            				
   			public static string LogoColumn{
			      get{
        			return "Logo";
      			}
		    }
           
            public IColumn HeaderBGColor{
                get{
                    return this.GetColumn("HeaderBGColor");
                }
            }
            				
   			public static string HeaderBGColorColumn{
			      get{
        			return "HeaderBGColor";
      			}
		    }
           
            public IColumn HeaderTextColor{
                get{
                    return this.GetColumn("HeaderTextColor");
                }
            }
            				
   			public static string HeaderTextColorColumn{
			      get{
        			return "HeaderTextColor";
      			}
		    }
           
            public IColumn HeaderLinkColor{
                get{
                    return this.GetColumn("HeaderLinkColor");
                }
            }
            				
   			public static string HeaderLinkColorColumn{
			      get{
        			return "HeaderLinkColor";
      			}
		    }
           
            public IColumn ContentBGColor{
                get{
                    return this.GetColumn("ContentBGColor");
                }
            }
            				
   			public static string ContentBGColorColumn{
			      get{
        			return "ContentBGColor";
      			}
		    }
           
            public IColumn ContentTextColor{
                get{
                    return this.GetColumn("ContentTextColor");
                }
            }
            				
   			public static string ContentTextColorColumn{
			      get{
        			return "ContentTextColor";
      			}
		    }
           
            public IColumn ContentLinkColor{
                get{
                    return this.GetColumn("ContentLinkColor");
                }
            }
            				
   			public static string ContentLinkColorColumn{
			      get{
        			return "ContentLinkColor";
      			}
		    }
           
            public IColumn SpriteColor{
                get{
                    return this.GetColumn("SpriteColor");
                }
            }
            				
   			public static string SpriteColorColumn{
			      get{
        			return "SpriteColor";
      			}
		    }
           
                    
        }
        
        /// <summary>
        /// Table: ActiveVisitorsQueueOLD
        /// Primary Key: ID
        /// </summary>

        public class ActiveVisitorsQueueOLDTable: DatabaseTable {
            
            public ActiveVisitorsQueueOLDTable(IDataProvider provider):base("ActiveVisitorsQueueOLD",provider){
                ClassName = "ActiveVisitorsQueueOLD";
                SchemaName = "dbo";
                

                Columns.Add(new DatabaseColumn("ID", this)
                {
	                IsPrimaryKey = true,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("SessionID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 255
                });

                Columns.Add(new DatabaseColumn("Account", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 255
                });

                Columns.Add(new DatabaseColumn("MyID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("CampaignID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("CurrentDateTime", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.DateTime,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("DocumentTitle", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 255
                });

                Columns.Add(new DatabaseColumn("Referer", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 255
                });

                Columns.Add(new DatabaseColumn("HostName", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 255
                });

                Columns.Add(new DatabaseColumn("RelativePathOfPage", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 255
                });

                Columns.Add(new DatabaseColumn("Test", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 255
                });

                Columns.Add(new DatabaseColumn("UserAgent", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 255
                });

                Columns.Add(new DatabaseColumn("UserID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 255
                });
                    
                
                
            }
            
            public IColumn ID{
                get{
                    return this.GetColumn("ID");
                }
            }
            				
   			public static string IDColumn{
			      get{
        			return "ID";
      			}
		    }
           
            public IColumn SessionID{
                get{
                    return this.GetColumn("SessionID");
                }
            }
            				
   			public static string SessionIDColumn{
			      get{
        			return "SessionID";
      			}
		    }
           
            public IColumn Account{
                get{
                    return this.GetColumn("Account");
                }
            }
            				
   			public static string AccountColumn{
			      get{
        			return "Account";
      			}
		    }
           
            public IColumn MyID{
                get{
                    return this.GetColumn("MyID");
                }
            }
            				
   			public static string MyIDColumn{
			      get{
        			return "MyID";
      			}
		    }
           
            public IColumn CampaignID{
                get{
                    return this.GetColumn("CampaignID");
                }
            }
            				
   			public static string CampaignIDColumn{
			      get{
        			return "CampaignID";
      			}
		    }
           
            public IColumn CurrentDateTime{
                get{
                    return this.GetColumn("CurrentDateTime");
                }
            }
            				
   			public static string CurrentDateTimeColumn{
			      get{
        			return "CurrentDateTime";
      			}
		    }
           
            public IColumn DocumentTitle{
                get{
                    return this.GetColumn("DocumentTitle");
                }
            }
            				
   			public static string DocumentTitleColumn{
			      get{
        			return "DocumentTitle";
      			}
		    }
           
            public IColumn Referer{
                get{
                    return this.GetColumn("Referer");
                }
            }
            				
   			public static string RefererColumn{
			      get{
        			return "Referer";
      			}
		    }
           
            public IColumn HostName{
                get{
                    return this.GetColumn("HostName");
                }
            }
            				
   			public static string HostNameColumn{
			      get{
        			return "HostName";
      			}
		    }
           
            public IColumn RelativePathOfPage{
                get{
                    return this.GetColumn("RelativePathOfPage");
                }
            }
            				
   			public static string RelativePathOfPageColumn{
			      get{
        			return "RelativePathOfPage";
      			}
		    }
           
            public IColumn Test{
                get{
                    return this.GetColumn("Test");
                }
            }
            				
   			public static string TestColumn{
			      get{
        			return "Test";
      			}
		    }
           
            public IColumn UserAgent{
                get{
                    return this.GetColumn("UserAgent");
                }
            }
            				
   			public static string UserAgentColumn{
			      get{
        			return "UserAgent";
      			}
		    }
           
            public IColumn UserID{
                get{
                    return this.GetColumn("UserID");
                }
            }
            				
   			public static string UserIDColumn{
			      get{
        			return "UserID";
      			}
		    }
           
                    
        }
        
        /// <summary>
        /// Table: BlogPosts
        /// Primary Key: ID
        /// </summary>

        public class BlogPostsTable: DatabaseTable {
            
            public BlogPostsTable(IDataProvider provider):base("BlogPosts",provider){
                ClassName = "BlogPost";
                SchemaName = "dbo";
                

                Columns.Add(new DatabaseColumn("ID", this)
                {
	                IsPrimaryKey = true,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = true,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("UserID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Guid,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("EventID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("Title", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 500
                });

                Columns.Add(new DatabaseColumn("BlogURL", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 1000
                });

                Columns.Add(new DatabaseColumn("DateTime", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.DateTime,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("Description", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 1000
                });

                Columns.Add(new DatabaseColumn("Thumbnail", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 255
                });
                    
                
                
            }
            
            public IColumn ID{
                get{
                    return this.GetColumn("ID");
                }
            }
            				
   			public static string IDColumn{
			      get{
        			return "ID";
      			}
		    }
           
            public IColumn UserID{
                get{
                    return this.GetColumn("UserID");
                }
            }
            				
   			public static string UserIDColumn{
			      get{
        			return "UserID";
      			}
		    }
           
            public IColumn EventID{
                get{
                    return this.GetColumn("EventID");
                }
            }
            				
   			public static string EventIDColumn{
			      get{
        			return "EventID";
      			}
		    }
           
            public IColumn Title{
                get{
                    return this.GetColumn("Title");
                }
            }
            				
   			public static string TitleColumn{
			      get{
        			return "Title";
      			}
		    }
           
            public IColumn BlogURL{
                get{
                    return this.GetColumn("BlogURL");
                }
            }
            				
   			public static string BlogURLColumn{
			      get{
        			return "BlogURL";
      			}
		    }
           
            public IColumn DateTime{
                get{
                    return this.GetColumn("DateTime");
                }
            }
            				
   			public static string DateTimeColumn{
			      get{
        			return "DateTime";
      			}
		    }
           
            public IColumn Description{
                get{
                    return this.GetColumn("Description");
                }
            }
            				
   			public static string DescriptionColumn{
			      get{
        			return "Description";
      			}
		    }
           
            public IColumn Thumbnail{
                get{
                    return this.GetColumn("Thumbnail");
                }
            }
            				
   			public static string ThumbnailColumn{
			      get{
        			return "Thumbnail";
      			}
		    }
           
                    
        }
        
        /// <summary>
        /// Table: AggregateVisitHistoryOLD
        /// Primary Key: ID
        /// </summary>

        public class AggregateVisitHistoryOLDTable: DatabaseTable {
            
            public AggregateVisitHistoryOLDTable(IDataProvider provider):base("AggregateVisitHistoryOLD",provider){
                ClassName = "AggregateVisitHistoryOLD";
                SchemaName = "dbo";
                

                Columns.Add(new DatabaseColumn("ID", this)
                {
	                IsPrimaryKey = true,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("SessionID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 255
                });

                Columns.Add(new DatabaseColumn("myID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("CampaignID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("EnterTime", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.DateTime,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("ExitTime", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.DateTime,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("NumberOfPagesViewed", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("TotalTimeOnSite", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 20
                });

                Columns.Add(new DatabaseColumn("VisitTrail", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = -1
                });

                Columns.Add(new DatabaseColumn("UserID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 255
                });
                    
                
                
            }
            
            public IColumn ID{
                get{
                    return this.GetColumn("ID");
                }
            }
            				
   			public static string IDColumn{
			      get{
        			return "ID";
      			}
		    }
           
            public IColumn SessionID{
                get{
                    return this.GetColumn("SessionID");
                }
            }
            				
   			public static string SessionIDColumn{
			      get{
        			return "SessionID";
      			}
		    }
           
            public IColumn myID{
                get{
                    return this.GetColumn("myID");
                }
            }
            				
   			public static string myIDColumn{
			      get{
        			return "myID";
      			}
		    }
           
            public IColumn CampaignID{
                get{
                    return this.GetColumn("CampaignID");
                }
            }
            				
   			public static string CampaignIDColumn{
			      get{
        			return "CampaignID";
      			}
		    }
           
            public IColumn EnterTime{
                get{
                    return this.GetColumn("EnterTime");
                }
            }
            				
   			public static string EnterTimeColumn{
			      get{
        			return "EnterTime";
      			}
		    }
           
            public IColumn ExitTime{
                get{
                    return this.GetColumn("ExitTime");
                }
            }
            				
   			public static string ExitTimeColumn{
			      get{
        			return "ExitTime";
      			}
		    }
           
            public IColumn NumberOfPagesViewed{
                get{
                    return this.GetColumn("NumberOfPagesViewed");
                }
            }
            				
   			public static string NumberOfPagesViewedColumn{
			      get{
        			return "NumberOfPagesViewed";
      			}
		    }
           
            public IColumn TotalTimeOnSite{
                get{
                    return this.GetColumn("TotalTimeOnSite");
                }
            }
            				
   			public static string TotalTimeOnSiteColumn{
			      get{
        			return "TotalTimeOnSite";
      			}
		    }
           
            public IColumn VisitTrail{
                get{
                    return this.GetColumn("VisitTrail");
                }
            }
            				
   			public static string VisitTrailColumn{
			      get{
        			return "VisitTrail";
      			}
		    }
           
            public IColumn UserID{
                get{
                    return this.GetColumn("UserID");
                }
            }
            				
   			public static string UserIDColumn{
			      get{
        			return "UserID";
      			}
		    }
           
                    
        }
        
        /// <summary>
        /// Table: VisitHistoryOLD
        /// Primary Key: ID
        /// </summary>

        public class VisitHistoryOLDTable: DatabaseTable {
            
            public VisitHistoryOLDTable(IDataProvider provider):base("VisitHistoryOLD",provider){
                ClassName = "VisitHistoryOLD";
                SchemaName = "dbo";
                

                Columns.Add(new DatabaseColumn("ID", this)
                {
	                IsPrimaryKey = true,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("SessionID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 255
                });

                Columns.Add(new DatabaseColumn("Account", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 255
                });

                Columns.Add(new DatabaseColumn("MyID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 10
                });

                Columns.Add(new DatabaseColumn("CampaignID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 10
                });

                Columns.Add(new DatabaseColumn("CurrentDateTime", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.DateTime,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("DocumentTitle", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 255
                });

                Columns.Add(new DatabaseColumn("Referer", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 255
                });

                Columns.Add(new DatabaseColumn("HostName", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 255
                });

                Columns.Add(new DatabaseColumn("RelativePathOfPage", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 255
                });

                Columns.Add(new DatabaseColumn("Test", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 255
                });

                Columns.Add(new DatabaseColumn("UserAgent", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 255
                });

                Columns.Add(new DatabaseColumn("UserID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 255
                });
                    
                
                
            }
            
            public IColumn ID{
                get{
                    return this.GetColumn("ID");
                }
            }
            				
   			public static string IDColumn{
			      get{
        			return "ID";
      			}
		    }
           
            public IColumn SessionID{
                get{
                    return this.GetColumn("SessionID");
                }
            }
            				
   			public static string SessionIDColumn{
			      get{
        			return "SessionID";
      			}
		    }
           
            public IColumn Account{
                get{
                    return this.GetColumn("Account");
                }
            }
            				
   			public static string AccountColumn{
			      get{
        			return "Account";
      			}
		    }
           
            public IColumn MyID{
                get{
                    return this.GetColumn("MyID");
                }
            }
            				
   			public static string MyIDColumn{
			      get{
        			return "MyID";
      			}
		    }
           
            public IColumn CampaignID{
                get{
                    return this.GetColumn("CampaignID");
                }
            }
            				
   			public static string CampaignIDColumn{
			      get{
        			return "CampaignID";
      			}
		    }
           
            public IColumn CurrentDateTime{
                get{
                    return this.GetColumn("CurrentDateTime");
                }
            }
            				
   			public static string CurrentDateTimeColumn{
			      get{
        			return "CurrentDateTime";
      			}
		    }
           
            public IColumn DocumentTitle{
                get{
                    return this.GetColumn("DocumentTitle");
                }
            }
            				
   			public static string DocumentTitleColumn{
			      get{
        			return "DocumentTitle";
      			}
		    }
           
            public IColumn Referer{
                get{
                    return this.GetColumn("Referer");
                }
            }
            				
   			public static string RefererColumn{
			      get{
        			return "Referer";
      			}
		    }
           
            public IColumn HostName{
                get{
                    return this.GetColumn("HostName");
                }
            }
            				
   			public static string HostNameColumn{
			      get{
        			return "HostName";
      			}
		    }
           
            public IColumn RelativePathOfPage{
                get{
                    return this.GetColumn("RelativePathOfPage");
                }
            }
            				
   			public static string RelativePathOfPageColumn{
			      get{
        			return "RelativePathOfPage";
      			}
		    }
           
            public IColumn Test{
                get{
                    return this.GetColumn("Test");
                }
            }
            				
   			public static string TestColumn{
			      get{
        			return "Test";
      			}
		    }
           
            public IColumn UserAgent{
                get{
                    return this.GetColumn("UserAgent");
                }
            }
            				
   			public static string UserAgentColumn{
			      get{
        			return "UserAgent";
      			}
		    }
           
            public IColumn UserID{
                get{
                    return this.GetColumn("UserID");
                }
            }
            				
   			public static string UserIDColumn{
			      get{
        			return "UserID";
      			}
		    }
           
                    
        }
        
        /// <summary>
        /// Table: CollectionQueueSettings
        /// Primary Key: ID
        /// </summary>

        public class CollectionQueueSettingsTable: DatabaseTable {
            
            public CollectionQueueSettingsTable(IDataProvider provider):base("CollectionQueueSettings",provider){
                ClassName = "CollectionQueueSetting";
                SchemaName = "dbo";
                

                Columns.Add(new DatabaseColumn("ID", this)
                {
	                IsPrimaryKey = true,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = true,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("UpdateEventsModeTime", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("BeforeEventPollingTime", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("DuringEventPollingTime", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("AfterEventPollingTime", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("OnGoingEventsPollingTime", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("FullEventPollingTime", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("WorkerCollectionThreads", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("WorkerImageThreads", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("WorkerLinkThreads", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("WorkeSystemThreads", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("WorkerTweetsThreads", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("CalculateTrendingEventsUpdateInterval", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });
                    
                
                
            }
            
            public IColumn ID{
                get{
                    return this.GetColumn("ID");
                }
            }
            				
   			public static string IDColumn{
			      get{
        			return "ID";
      			}
		    }
           
            public IColumn UpdateEventsModeTime{
                get{
                    return this.GetColumn("UpdateEventsModeTime");
                }
            }
            				
   			public static string UpdateEventsModeTimeColumn{
			      get{
        			return "UpdateEventsModeTime";
      			}
		    }
           
            public IColumn BeforeEventPollingTime{
                get{
                    return this.GetColumn("BeforeEventPollingTime");
                }
            }
            				
   			public static string BeforeEventPollingTimeColumn{
			      get{
        			return "BeforeEventPollingTime";
      			}
		    }
           
            public IColumn DuringEventPollingTime{
                get{
                    return this.GetColumn("DuringEventPollingTime");
                }
            }
            				
   			public static string DuringEventPollingTimeColumn{
			      get{
        			return "DuringEventPollingTime";
      			}
		    }
           
            public IColumn AfterEventPollingTime{
                get{
                    return this.GetColumn("AfterEventPollingTime");
                }
            }
            				
   			public static string AfterEventPollingTimeColumn{
			      get{
        			return "AfterEventPollingTime";
      			}
		    }
           
            public IColumn OnGoingEventsPollingTime{
                get{
                    return this.GetColumn("OnGoingEventsPollingTime");
                }
            }
            				
   			public static string OnGoingEventsPollingTimeColumn{
			      get{
        			return "OnGoingEventsPollingTime";
      			}
		    }
           
            public IColumn FullEventPollingTime{
                get{
                    return this.GetColumn("FullEventPollingTime");
                }
            }
            				
   			public static string FullEventPollingTimeColumn{
			      get{
        			return "FullEventPollingTime";
      			}
		    }
           
            public IColumn WorkerCollectionThreads{
                get{
                    return this.GetColumn("WorkerCollectionThreads");
                }
            }
            				
   			public static string WorkerCollectionThreadsColumn{
			      get{
        			return "WorkerCollectionThreads";
      			}
		    }
           
            public IColumn WorkerImageThreads{
                get{
                    return this.GetColumn("WorkerImageThreads");
                }
            }
            				
   			public static string WorkerImageThreadsColumn{
			      get{
        			return "WorkerImageThreads";
      			}
		    }
           
            public IColumn WorkerLinkThreads{
                get{
                    return this.GetColumn("WorkerLinkThreads");
                }
            }
            				
   			public static string WorkerLinkThreadsColumn{
			      get{
        			return "WorkerLinkThreads";
      			}
		    }
           
            public IColumn WorkeSystemThreads{
                get{
                    return this.GetColumn("WorkeSystemThreads");
                }
            }
            				
   			public static string WorkeSystemThreadsColumn{
			      get{
        			return "WorkeSystemThreads";
      			}
		    }
           
            public IColumn WorkerTweetsThreads{
                get{
                    return this.GetColumn("WorkerTweetsThreads");
                }
            }
            				
   			public static string WorkerTweetsThreadsColumn{
			      get{
        			return "WorkerTweetsThreads";
      			}
		    }
           
            public IColumn CalculateTrendingEventsUpdateInterval{
                get{
                    return this.GetColumn("CalculateTrendingEventsUpdateInterval");
                }
            }
            				
   			public static string CalculateTrendingEventsUpdateIntervalColumn{
			      get{
        			return "CalculateTrendingEventsUpdateInterval";
      			}
		    }
           
                    
        }
        
        /// <summary>
        /// Table: UserRatesEvent
        /// Primary Key: ID
        /// </summary>

        public class UserRatesEventTable: DatabaseTable {
            
            public UserRatesEventTable(IDataProvider provider):base("UserRatesEvent",provider){
                ClassName = "UserRatesEvent";
                SchemaName = "dbo";
                

                Columns.Add(new DatabaseColumn("ID", this)
                {
	                IsPrimaryKey = true,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = true,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("UserID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Guid,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = true,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("EventID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = true,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("UserRating", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("RatingDateTime", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.DateTime,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });
                    
                
                
            }
            
            public IColumn ID{
                get{
                    return this.GetColumn("ID");
                }
            }
            				
   			public static string IDColumn{
			      get{
        			return "ID";
      			}
		    }
           
            public IColumn UserID{
                get{
                    return this.GetColumn("UserID");
                }
            }
            				
   			public static string UserIDColumn{
			      get{
        			return "UserID";
      			}
		    }
           
            public IColumn EventID{
                get{
                    return this.GetColumn("EventID");
                }
            }
            				
   			public static string EventIDColumn{
			      get{
        			return "EventID";
      			}
		    }
           
            public IColumn UserRating{
                get{
                    return this.GetColumn("UserRating");
                }
            }
            				
   			public static string UserRatingColumn{
			      get{
        			return "UserRating";
      			}
		    }
           
            public IColumn RatingDateTime{
                get{
                    return this.GetColumn("RatingDateTime");
                }
            }
            				
   			public static string RatingDateTimeColumn{
			      get{
        			return "RatingDateTime";
      			}
		    }
           
                    
        }
        
        /// <summary>
        /// Table: MemoryBoxes
        /// Primary Key: ID
        /// </summary>

        public class MemoryBoxesTable: DatabaseTable {
            
            public MemoryBoxesTable(IDataProvider provider):base("MemoryBoxes",provider){
                ClassName = "MemoryBox";
                SchemaName = "dbo";
                

                Columns.Add(new DatabaseColumn("ID", this)
                {
	                IsPrimaryKey = true,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = true,
	                IsForeignKey = true,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("UserId", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Guid,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = true,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("EventId", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = true,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("Name", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 500
                });

                Columns.Add(new DatabaseColumn("Type", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 25
                });

                Columns.Add(new DatabaseColumn("CreatedDateTime", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.DateTime,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("IsActive", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Boolean,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });
                    
                
                
            }
            
            public IColumn ID{
                get{
                    return this.GetColumn("ID");
                }
            }
            				
   			public static string IDColumn{
			      get{
        			return "ID";
      			}
		    }
           
            public IColumn UserId{
                get{
                    return this.GetColumn("UserId");
                }
            }
            				
   			public static string UserIdColumn{
			      get{
        			return "UserId";
      			}
		    }
           
            public IColumn EventId{
                get{
                    return this.GetColumn("EventId");
                }
            }
            				
   			public static string EventIdColumn{
			      get{
        			return "EventId";
      			}
		    }
           
            public IColumn Name{
                get{
                    return this.GetColumn("Name");
                }
            }
            				
   			public static string NameColumn{
			      get{
        			return "Name";
      			}
		    }
           
            public IColumn Type{
                get{
                    return this.GetColumn("Type");
                }
            }
            				
   			public static string TypeColumn{
			      get{
        			return "Type";
      			}
		    }
           
            public IColumn CreatedDateTime{
                get{
                    return this.GetColumn("CreatedDateTime");
                }
            }
            				
   			public static string CreatedDateTimeColumn{
			      get{
        			return "CreatedDateTime";
      			}
		    }
           
            public IColumn IsActive{
                get{
                    return this.GetColumn("IsActive");
                }
            }
            				
   			public static string IsActiveColumn{
			      get{
        			return "IsActive";
      			}
		    }
           
                    
        }
        
        /// <summary>
        /// Table: UserRole
        /// Primary Key: ID
        /// </summary>

        public class UserRoleTable: DatabaseTable {
            
            public UserRoleTable(IDataProvider provider):base("UserRole",provider){
                ClassName = "UserRole";
                SchemaName = "dbo";
                

                Columns.Add(new DatabaseColumn("ID", this)
                {
	                IsPrimaryKey = true,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = true,
	                IsForeignKey = true,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("Name", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 500
                });
                    
                
                
            }
            
            public IColumn ID{
                get{
                    return this.GetColumn("ID");
                }
            }
            				
   			public static string IDColumn{
			      get{
        			return "ID";
      			}
		    }
           
            public IColumn Name{
                get{
                    return this.GetColumn("Name");
                }
            }
            				
   			public static string NameColumn{
			      get{
        			return "Name";
      			}
		    }
           
                    
        }
        
        /// <summary>
        /// Table: UserFollowsUser
        /// Primary Key: ID
        /// </summary>

        public class UserFollowsUserTable: DatabaseTable {
            
            public UserFollowsUserTable(IDataProvider provider):base("UserFollowsUser",provider){
                ClassName = "UserFollowsUser";
                SchemaName = "dbo";
                

                Columns.Add(new DatabaseColumn("ID", this)
                {
	                IsPrimaryKey = true,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = true,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("UserFollowingID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Guid,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = true,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("UserFollowedID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Guid,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = true,
	                MaxLength = 0
                });
                    
                
                
            }
            
            public IColumn ID{
                get{
                    return this.GetColumn("ID");
                }
            }
            				
   			public static string IDColumn{
			      get{
        			return "ID";
      			}
		    }
           
            public IColumn UserFollowingID{
                get{
                    return this.GetColumn("UserFollowingID");
                }
            }
            				
   			public static string UserFollowingIDColumn{
			      get{
        			return "UserFollowingID";
      			}
		    }
           
            public IColumn UserFollowedID{
                get{
                    return this.GetColumn("UserFollowedID");
                }
            }
            				
   			public static string UserFollowedIDColumn{
			      get{
        			return "UserFollowedID";
      			}
		    }
           
                    
        }
        
}