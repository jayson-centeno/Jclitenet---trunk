// Type: NHibernate.Impl.SessionImpl
// Assembly: NHibernate, Version=3.1.0.4000, Culture=neutral, PublicKeyToken=aa95f207798dfdb4
// Assembly location: D:\Jayson\Projects\CoreFramework4\dll\NHibernate.dll

using Iesi.Collections;
using NHibernate;
using NHibernate.AdoNet;
using NHibernate.Collection;
using NHibernate.Engine;
using NHibernate.Engine.Query.Sql;
using NHibernate.Event;
using NHibernate.Hql;
using NHibernate.Loader.Custom;
using NHibernate.Persister.Entity;
using NHibernate.Stat;
using NHibernate.Type;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace NHibernate.Impl
{
    [Serializable]
    public sealed class SessionImpl : AbstractSessionImpl, IEventSource, ISessionImplementor, ISession, IDisposable, ISerializable, IDeserializationCallback
    {
        public ConnectionReleaseMode ConnectionReleaseMode { get; }
        public bool IsAutoCloseSessionEnabled { get; }
        public bool ShouldAutoClose { get; }
        public bool FlushBeforeCompletionEnabled { get; }

        #region IDeserializationCallback Members

        void IDeserializationCallback.OnDeserialization(object sender);

        #endregion

        #region IEventSource Members

        public IDbConnection Close();
        public override void AfterTransactionCompletion(bool success, ITransaction tx);
        public LockMode GetCurrentLockMode(object obj);
        public object Save(object obj);
        public object Save(string entityName, object obj);
        public void Save(object obj, object id);
        public void Delete(object obj);
        public void Delete(string entityName, object obj);
        public void Update(object obj);
        public void Update(string entityName, object obj);
        public void SaveOrUpdate(object obj);
        public void SaveOrUpdate(string entityName, object obj);
        public void Update(object obj, object id);
        public override void CloseSessionFromDistributedTransaction();
        public override IList List(string query, QueryParameters parameters);
        public override IList<T> List<T>(string query, QueryParameters parameters);
        public override void List(string query, QueryParameters queryParameters, IList results);
        public override IQueryTranslator[] GetQueries(string query, bool scalar);
        public override IEnumerable<T> Enumerable<T>(string query, QueryParameters queryParameters);
        public override IEnumerable Enumerable(string query, QueryParameters queryParameters);
        public int Delete(string query);
        public int Delete(string query, object value, IType type);
        public int Delete(string query, object[] values, IType[] types);
        public void Lock(object obj, LockMode lockMode);
        public void Lock(string entityName, object obj, LockMode lockMode);
        public IQuery CreateFilter(object collection, string queryString);
        public override object Instantiate(string clazz, object id);
        public object Instantiate(IEntityPersister persister, object id);
        public void ForceFlush(EntityEntry entityEntry);
        public void Merge(string entityName, object obj, IDictionary copiedAlready);
        public void Persist(string entityName, object obj, IDictionary createdAlready);
        public void PersistOnFlush(string entityName, object obj, IDictionary copiedAlready);
        public void Refresh(object obj, IDictionary refreshedAlready);

        [Obsolete("Use Merge(string, object, IDictionary) instead")]
        public void SaveOrUpdateCopy(string entityName, object obj, IDictionary copiedAlready);

        public void Delete(string entityName, object child, bool isCascadeDeleteEnabled, ISet transientEntities);
        public object Merge(string entityName, object obj);
        public object Merge(object obj);
        public void Persist(string entityName, object obj);
        public void Persist(object obj);
        public override string BestGuessEntityName(object entity);
        public override string GuessEntityName(object entity);
        public override object GetEntityUsingInterceptor(EntityKey key);
        public void Load(object obj, object id);
        public T Load<T>(object id);
        public T Load<T>(object id, LockMode lockMode);
        public object Load(string entityName, object id);
        public object Load(string entityName, object id, LockMode lockMode);
        public T Get<T>(object id);
        public T Get<T>(object id, LockMode lockMode);
        public string GetEntityName(object obj);
        public object Get(string entityName, object id);
        public override object ImmediateLoad(string entityName, object id);
        public override object InternalLoad(string entityName, object id, bool eager, bool isNullable);
        public void Refresh(object obj);
        public void Refresh(object obj, LockMode lockMode);
        public ITransaction BeginTransaction(IsolationLevel isolationLevel);
        public ITransaction BeginTransaction();
        public override void Flush();
        public bool IsDirty();
        public object GetIdentifier(object obj);
        public override object GetContextEntityIdentifier(object obj);
        public override void InitializeCollection(IPersistentCollection collection, bool writing);
        public IDbConnection Disconnect();
        public void Reconnect();
        public void Reconnect(IDbConnection conn);
        public void Dispose();
        public override IList ListFilter(object collection, string filter, QueryParameters queryParameters);
        public override IList<T> ListFilter<T>(object collection, string filter, QueryParameters queryParameters);
        public override IEnumerable EnumerableFilter(object collection, string filter, QueryParameters queryParameters);
        public override IEnumerable<T> EnumerableFilter<T>(object collection, string filter, QueryParameters queryParameters);
        public ICriteria CreateCriteria<T>() where T : class;
        public ICriteria CreateCriteria<T>(string alias) where T : class;
        public ICriteria CreateCriteria(string entityName, string alias);
        public ICriteria CreateCriteria(string entityName);
        public IQueryOver<T, T> QueryOver<T>() where T : class;
        public IQueryOver<T, T> QueryOver<T>(Expression<Func<T>> alias) where T : class;
        public override IList List(CriteriaImpl criteria);
        public override IList<T> List<T>(CriteriaImpl criteria);
        public override void List(CriteriaImpl criteria, IList results);
        public bool Contains(object obj);
        public void Evict(object obj);
        public override ISQLQuery CreateSQLQuery(string sql);
        public override IList List(NativeSQLQuerySpecification spec, QueryParameters queryParameters);
        public override IList<T> List<T>(NativeSQLQuerySpecification spec, QueryParameters queryParameters);
        public override void List(NativeSQLQuerySpecification spec, QueryParameters queryParameters, IList results);
        public override void ListCustomQuery(ICustomQuery customQuery, QueryParameters queryParameters, IList results);
        public override IList<T> ListCustomQuery<T>(ICustomQuery customQuery, QueryParameters queryParameters);
        public void Clear();
        public void Replicate(object obj, ReplicationMode replicationMode);
        public void Replicate(string entityName, object obj, ReplicationMode replicationMode);
        public void CancelQuery();

        [Obsolete("Use Merge(object) instead")]
        public object SaveOrUpdateCopy(object obj);

        [Obsolete("No direct replacement. Use Merge instead.")]
        public object SaveOrUpdateCopy(object obj, object id);

        public NHibernate.IFilter GetEnabledFilter(string filterName);
        public NHibernate.IFilter EnableFilter(string filterName);
        public void DisableFilter(string filterName);
        public override object GetFilterParameterValue(string filterParameterName);
        public override IType GetFilterParameterType(string filterParameterName);
        public IMultiQuery CreateMultiQuery();
        public IMultiCriteria CreateMultiCriteria();
        public override void AfterTransactionBegin(ITransaction tx);
        public override void BeforeTransactionCompletion(ITransaction tx);
        public ISession SetBatchSize(int batchSize);
        public ISessionImplementor GetSessionImplementation();
        public ISession GetSession(EntityMode entityMode);
        public void SetReadOnly(object entityOrProxy, bool readOnly);
        public bool IsReadOnly(object entityOrProxy);
        public override int ExecuteNativeUpdate(NativeSQLQuerySpecification nativeQuerySpecification, QueryParameters queryParameters);
        public override int ExecuteUpdate(string query, QueryParameters queryParameters);
        public override IEntityPersister GetEntityPersister(string entityName, object obj);
        public override FutureCriteriaBatch FutureCriteriaBatch { get; internal set; }
        public override FutureQueryBatch FutureQueryBatch { get; internal set; }
        public override IBatcher Batcher { get; }
        public override long Timestamp { get; }
        public override bool IsOpen { get; }
        public ActionQueue ActionQueue { get; }
        public override FlushMode FlushMode { get; set; }
        public override bool IsEventSource { get; }
        public override IPersistenceContext PersistenceContext { get; }
        public ITransaction Transaction { get; }
        public override bool TransactionInProgress { get; }
        public override IDbConnection Connection { get; }
        public override bool IsConnected { get; }
        public ISessionFactory SessionFactory { get; }
        public override IDictionary<string, NHibernate.IFilter> EnabledFilters { get; }
        public override ConnectionManager ConnectionManager { get; }
        public ISessionStatistics Statistics { get; }
        public override IInterceptor Interceptor { get; }
        public override EventListeners Listeners { get; }
        public override int DontFlushFromFind { get; }
        public override CacheMode CacheMode { get; set; }
        public override EntityMode EntityMode { get; }
        public EntityMode ActiveEntityMode { get; }
        public override string FetchProfile { get; set; }
        public bool DefaultReadOnly { get; set; }

        #endregion

        #region ISerializable Members

        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
        void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context);

        #endregion

        public override void List(IQueryExpression queryExpression, QueryParameters queryParameters, IList results);
        public void PersistOnFlush(string entityName, object obj);
        public void PersistOnFlush(object obj);
        public object Load(Type entityClass, object id, LockMode lockMode);
        public object Load(Type entityClass, object id);
        public object Get(Type entityClass, object id);
        public object Get(Type clazz, object id, LockMode lockMode);
        ~SessionImpl();
        public ICriteria CreateCriteria(Type persistentClass);
        public ICriteria CreateCriteria(Type persistentClass, string alias);
    }
}
