


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
        public Query<aspnet_WebEvent_Event> aspnet_WebEvent_Events { get; set; }
        public Query<aspnet_Application> aspnet_Applications { get; set; }
        public Query<BlogPost> BlogPosts { get; set; }
        public Query<aspnet_User> aspnet_Users { get; set; }
        public Query<Event> Events { get; set; }
        public Query<aspnet_SchemaVersion> aspnet_SchemaVersions { get; set; }
        public Query<Picture> Pictures { get; set; }
        public Query<BetaSignup> BetaSignups { get; set; }
        public Query<aspnet_Membership> aspnet_Memberships { get; set; }
        public Query<Tweet> Tweets { get; set; }
        public Query<URL> URLS { get; set; }
        public Query<aspnet_Profile> aspnet_Profiles { get; set; }
        public Query<aspnet_Role> aspnet_Roles { get; set; }
        public Query<aspnet_UsersInRole> aspnet_UsersInRoles { get; set; }
        public Query<ImageMetaDatum> ImageMetaData { get; set; }
        public Query<Image> Images { get; set; }
        public Query<aspnet_Path> aspnet_Paths { get; set; }
        public Query<aspnet_PersonalizationAllUser> aspnet_PersonalizationAllUsers { get; set; }
        public Query<aspnet_PersonalizationPerUser> aspnet_PersonalizationPerUsers { get; set; }
        public Query<Venue> Venues { get; set; }

			

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
            aspnet_WebEvent_Events = new Query<aspnet_WebEvent_Event>(provider);
            aspnet_Applications = new Query<aspnet_Application>(provider);
            BlogPosts = new Query<BlogPost>(provider);
            aspnet_Users = new Query<aspnet_User>(provider);
            Events = new Query<Event>(provider);
            aspnet_SchemaVersions = new Query<aspnet_SchemaVersion>(provider);
            Pictures = new Query<Picture>(provider);
            BetaSignups = new Query<BetaSignup>(provider);
            aspnet_Memberships = new Query<aspnet_Membership>(provider);
            Tweets = new Query<Tweet>(provider);
            URLS = new Query<URL>(provider);
            aspnet_Profiles = new Query<aspnet_Profile>(provider);
            aspnet_Roles = new Query<aspnet_Role>(provider);
            aspnet_UsersInRoles = new Query<aspnet_UsersInRole>(provider);
            ImageMetaData = new Query<ImageMetaDatum>(provider);
            Images = new Query<Image>(provider);
            aspnet_Paths = new Query<aspnet_Path>(provider);
            aspnet_PersonalizationAllUsers = new Query<aspnet_PersonalizationAllUser>(provider);
            aspnet_PersonalizationPerUsers = new Query<aspnet_PersonalizationPerUser>(provider);
            Venues = new Query<Venue>(provider);
            #endregion


            #region ' Schemas '
        	if(DataProvider.Schema.Tables.Count == 0)
			{
            	DataProvider.Schema.Tables.Add(new EventCategoriesTable(DataProvider));
            	DataProvider.Schema.Tables.Add(new aspnet_WebEvent_EventsTable(DataProvider));
            	DataProvider.Schema.Tables.Add(new aspnet_ApplicationsTable(DataProvider));
            	DataProvider.Schema.Tables.Add(new BlogPostsTable(DataProvider));
            	DataProvider.Schema.Tables.Add(new aspnet_UsersTable(DataProvider));
            	DataProvider.Schema.Tables.Add(new EventsTable(DataProvider));
            	DataProvider.Schema.Tables.Add(new aspnet_SchemaVersionsTable(DataProvider));
            	DataProvider.Schema.Tables.Add(new PicturesTable(DataProvider));
            	DataProvider.Schema.Tables.Add(new BetaSignupsTable(DataProvider));
            	DataProvider.Schema.Tables.Add(new aspnet_MembershipTable(DataProvider));
            	DataProvider.Schema.Tables.Add(new TweetsTable(DataProvider));
            	DataProvider.Schema.Tables.Add(new URLsTable(DataProvider));
            	DataProvider.Schema.Tables.Add(new aspnet_ProfileTable(DataProvider));
            	DataProvider.Schema.Tables.Add(new aspnet_RolesTable(DataProvider));
            	DataProvider.Schema.Tables.Add(new aspnet_UsersInRolesTable(DataProvider));
            	DataProvider.Schema.Tables.Add(new ImageMetaDataTable(DataProvider));
            	DataProvider.Schema.Tables.Add(new ImagesTable(DataProvider));
            	DataProvider.Schema.Tables.Add(new aspnet_PathsTable(DataProvider));
            	DataProvider.Schema.Tables.Add(new aspnet_PersonalizationAllUsersTable(DataProvider));
            	DataProvider.Schema.Tables.Add(new aspnet_PersonalizationPerUserTable(DataProvider));
            	DataProvider.Schema.Tables.Add(new VenuesTable(DataProvider));
            }
            #endregion
        }
    }
}