


using System;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using SubSonic.DataProviders;
using SubSonic.Extensions;
using SubSonic.Linq.Structure;
using SubSonic.Query;
using SubSonic.Schema;
using System.Data.Common;
using System.Collections.Generic;

namespace Epilogger.Data
{
    public partial class EpiloggerDB : IQuerySurface
    {

        public IDataProvider DataProvider;
        public DbQueryProvider provider;
        
        public bool TestMode
		{
            get
			{
                return DataProvider.ConnectionString.Equals("test", StringComparison.InvariantCultureIgnoreCase);
            }
        }

        public EpiloggerDB() 
        { 
            DataProvider = ProviderFactory.GetProvider("ChrisDev1ConnectionString");
            Init();
        }

        public EpiloggerDB(string connectionStringName)
        {
            DataProvider = ProviderFactory.GetProvider(connectionStringName);
            Init();
        }

		public EpiloggerDB(string connectionString, string providerName)
        {
            DataProvider = ProviderFactory.GetProvider(connectionString,providerName);
            Init();
        }

		public ITable FindByPrimaryKey(string pkName)
        {
            return DataProvider.Schema.Tables.SingleOrDefault(x => x.PrimaryKey.Name.Equals(pkName, StringComparison.InvariantCultureIgnoreCase));
        }

        public Query<T> GetQuery<T>()
        {
            return new Query<T>(provider);
        }
        
        public ITable FindTable(string tableName)
        {
            return DataProvider.FindTable(tableName);
        }
               
        public IDataProvider Provider
        {
            get { return DataProvider; }
            set {DataProvider=value;}
        }
        
        public DbQueryProvider QueryProvider
        {
            get { return provider; }
        }
        
        BatchQuery _batch = null;
        public void Queue<T>(IQueryable<T> qry)
        {
            if (_batch == null)
                _batch = new BatchQuery(Provider, QueryProvider);
            _batch.Queue(qry);
        }

        public void Queue(ISqlQuery qry)
        {
            if (_batch == null)
                _batch = new BatchQuery(Provider, QueryProvider);
            _batch.Queue(qry);
        }

        public void ExecuteTransaction(IList<DbCommand> commands)
		{
            if(!TestMode)
			{
                using(var connection = commands[0].Connection)
				{
                   if (connection.State == ConnectionState.Closed)
                        connection.Open();
                   
                   using (var trans = connection.BeginTransaction()) 
				   {
                        foreach (var cmd in commands) 
						{
                            cmd.Transaction = trans;
                            cmd.Connection = connection;
                            cmd.ExecuteNonQuery();
                        }
                        trans.Commit();
                    }
                    connection.Close();
                }
            }
        }

        public IDataReader ExecuteBatch()
        {
            if (_batch == null)
                throw new InvalidOperationException("There's nothing in the queue");
            if(!TestMode)
                return _batch.ExecuteReader();
            return null;
        }
			
        public Query<EventCategory> EventCategories { get; set; }
        public Query<User> Users { get; set; }
        public Query<Tweet> Tweets { get; set; }
        public Query<URL> URLS { get; set; }
        public Query<aspnet_User> aspnet_Users { get; set; }
        public Query<BetaSignup> BetaSignups { get; set; }
        public Query<UserFollowsEvent> UserFollowsEvents { get; set; }
        public Query<StatusMessage> StatusMessages { get; set; }
        public Query<UserAuthenticationProfile> UserAuthenticationProfiles { get; set; }
        public Query<UserClickAction> UserClickActions { get; set; }
        public Query<CheckIn> CheckIns { get; set; }
        public Query<Role> Roles { get; set; }
        public Query<userClickTracking> userClickTrackings { get; set; }
        public Query<Venue> Venues { get; set; }
        public Query<UserInRole> UserInRoles { get; set; }
        public Query<Event> Events { get; set; }
        public Query<ImageMetaDatum> ImageMetaData { get; set; }
        public Query<Image> Images { get; set; }
        public Query<ActiveVisitorsQueueOLD> ActiveVisitorsQueueOLDs { get; set; }
        public Query<BlogPost> BlogPosts { get; set; }
        public Query<AggregateVisitHistoryOLD> AggregateVisitHistoryOLDs { get; set; }
        public Query<VisitHistoryOLD> VisitHistoryOLDs { get; set; }
        public Query<UserRatesEvent> UserRatesEvents { get; set; }
        public Query<UserRole> UserRoles { get; set; }
        public Query<UserFollowsUser> UserFollowsUsers { get; set; }

			

        #region ' Aggregates and SubSonic Queries '
        public Select SelectColumns(params string[] columns)
        {
            return new Select(DataProvider, columns);
        }

        public Select Select
        {
            get { return new Select(this.Provider); }
        }

        public Insert Insert
		{
            get { return new Insert(this.Provider); }
        }

        public Update<T> Update<T>() where T:new()
		{
            return new Update<T>(this.Provider);
        }

        public SqlQuery Delete<T>(Expression<Func<T,bool>> column) where T:new()
        {
            LambdaExpression lamda = column;
            SqlQuery result = new Delete<T>(this.Provider);
            result = result.From<T>();
            result.Constraints=lamda.ParseConstraints().ToList();
            return result;
        }

        public SqlQuery Max<T>(Expression<Func<T,object>> column)
        {
            LambdaExpression lamda = column;
            string colName = lamda.ParseObjectValue();
            string objectName = typeof(T).Name;
            string tableName = DataProvider.FindTable(objectName).Name;
            return new Select(DataProvider, new Aggregate(colName, AggregateFunction.Max)).From(tableName);
        }

        public SqlQuery Min<T>(Expression<Func<T,object>> column)
        {
            LambdaExpression lamda = column;
            string colName = lamda.ParseObjectValue();
            string objectName = typeof(T).Name;
            string tableName = this.Provider.FindTable(objectName).Name;
            return new Select(this.Provider, new Aggregate(colName, AggregateFunction.Min)).From(tableName);
        }

        public SqlQuery Sum<T>(Expression<Func<T,object>> column)
        {
            LambdaExpression lamda = column;
            string colName = lamda.ParseObjectValue();
            string objectName = typeof(T).Name;
            string tableName = this.Provider.FindTable(objectName).Name;
            return new Select(this.Provider, new Aggregate(colName, AggregateFunction.Sum)).From(tableName);
        }

        public SqlQuery Avg<T>(Expression<Func<T,object>> column)
        {
            LambdaExpression lamda = column;
            string colName = lamda.ParseObjectValue();
            string objectName = typeof(T).Name;
            string tableName = this.Provider.FindTable(objectName).Name;
            return new Select(this.Provider, new Aggregate(colName, AggregateFunction.Avg)).From(tableName);
        }

        public SqlQuery Count<T>(Expression<Func<T,object>> column)
        {
            LambdaExpression lamda = column;
            string colName = lamda.ParseObjectValue();
            string objectName = typeof(T).Name;
            string tableName = this.Provider.FindTable(objectName).Name;
            return new Select(this.Provider, new Aggregate(colName, AggregateFunction.Count)).From(tableName);
        }

        public SqlQuery Variance<T>(Expression<Func<T,object>> column)
        {
            LambdaExpression lamda = column;
            string colName = lamda.ParseObjectValue();
            string objectName = typeof(T).Name;
            string tableName = this.Provider.FindTable(objectName).Name;
            return new Select(this.Provider, new Aggregate(colName, AggregateFunction.Var)).From(tableName);
        }

        public SqlQuery StandardDeviation<T>(Expression<Func<T,object>> column)
        {
            LambdaExpression lamda = column;
            string colName = lamda.ParseObjectValue();
            string objectName = typeof(T).Name;
            string tableName = this.Provider.FindTable(objectName).Name;
            return new Select(this.Provider, new Aggregate(colName, AggregateFunction.StDev)).From(tableName);
        }

        #endregion

        void Init()
        {
            provider = new DbQueryProvider(this.Provider);

            #region ' Query Defs '
            EventCategories = new Query<EventCategory>(provider);
            Users = new Query<User>(provider);
            Tweets = new Query<Tweet>(provider);
            URLS = new Query<URL>(provider);
            aspnet_Users = new Query<aspnet_User>(provider);
            BetaSignups = new Query<BetaSignup>(provider);
            UserFollowsEvents = new Query<UserFollowsEvent>(provider);
            StatusMessages = new Query<StatusMessage>(provider);
            UserAuthenticationProfiles = new Query<UserAuthenticationProfile>(provider);
            UserClickActions = new Query<UserClickAction>(provider);
            CheckIns = new Query<CheckIn>(provider);
            Roles = new Query<Role>(provider);
            userClickTrackings = new Query<userClickTracking>(provider);
            Venues = new Query<Venue>(provider);
            UserInRoles = new Query<UserInRole>(provider);
            Events = new Query<Event>(provider);
            ImageMetaData = new Query<ImageMetaDatum>(provider);
            Images = new Query<Image>(provider);
            ActiveVisitorsQueueOLDs = new Query<ActiveVisitorsQueueOLD>(provider);
            BlogPosts = new Query<BlogPost>(provider);
            AggregateVisitHistoryOLDs = new Query<AggregateVisitHistoryOLD>(provider);
            VisitHistoryOLDs = new Query<VisitHistoryOLD>(provider);
            UserRatesEvents = new Query<UserRatesEvent>(provider);
            UserRoles = new Query<UserRole>(provider);
            UserFollowsUsers = new Query<UserFollowsUser>(provider);
            #endregion


            #region ' Schemas '
        	if(DataProvider.Schema.Tables.Count == 0)
			{
            	DataProvider.Schema.Tables.Add(new EventCategoriesTable(DataProvider));
            	DataProvider.Schema.Tables.Add(new UserTable(DataProvider));
            	DataProvider.Schema.Tables.Add(new TweetsTable(DataProvider));
            	DataProvider.Schema.Tables.Add(new URLsTable(DataProvider));
            	DataProvider.Schema.Tables.Add(new aspnet_UsersTable(DataProvider));
            	DataProvider.Schema.Tables.Add(new BetaSignupsTable(DataProvider));
            	DataProvider.Schema.Tables.Add(new UserFollowsEventTable(DataProvider));
            	DataProvider.Schema.Tables.Add(new StatusMessagesTable(DataProvider));
            	DataProvider.Schema.Tables.Add(new UserAuthenticationProfileTable(DataProvider));
            	DataProvider.Schema.Tables.Add(new UserClickActionsTable(DataProvider));
            	DataProvider.Schema.Tables.Add(new CheckInsTable(DataProvider));
            	DataProvider.Schema.Tables.Add(new RolesTable(DataProvider));
            	DataProvider.Schema.Tables.Add(new userClickTrackingTable(DataProvider));
            	DataProvider.Schema.Tables.Add(new VenuesTable(DataProvider));
            	DataProvider.Schema.Tables.Add(new UserInRolesTable(DataProvider));
            	DataProvider.Schema.Tables.Add(new EventsTable(DataProvider));
            	DataProvider.Schema.Tables.Add(new ImageMetaDataTable(DataProvider));
            	DataProvider.Schema.Tables.Add(new ImagesTable(DataProvider));
            	DataProvider.Schema.Tables.Add(new ActiveVisitorsQueueOLDTable(DataProvider));
            	DataProvider.Schema.Tables.Add(new BlogPostsTable(DataProvider));
            	DataProvider.Schema.Tables.Add(new AggregateVisitHistoryOLDTable(DataProvider));
            	DataProvider.Schema.Tables.Add(new VisitHistoryOLDTable(DataProvider));
            	DataProvider.Schema.Tables.Add(new UserRatesEventTable(DataProvider));
            	DataProvider.Schema.Tables.Add(new UserRoleTable(DataProvider));
            	DataProvider.Schema.Tables.Add(new UserFollowsUserTable(DataProvider));
            }
            #endregion
        }
    }
}