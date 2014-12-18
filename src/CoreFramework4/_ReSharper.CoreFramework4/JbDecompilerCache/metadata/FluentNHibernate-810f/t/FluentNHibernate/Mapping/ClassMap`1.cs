// Type: FluentNHibernate.Mapping.ClassMap`1
// Assembly: FluentNHibernate, Version=1.2.0.712, Culture=neutral, PublicKeyToken=8aa435e3cb308880
// Assembly location: D:\Jayson\Projects\CoreFramework4\dll\FluentNHibernate.dll

using FluentNHibernate;
using FluentNHibernate.MappingModel;
using FluentNHibernate.MappingModel.ClassBased;
using NHibernate.Persister.Entity;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq.Expressions;

namespace FluentNHibernate.Mapping
{
    public class ClassMap<T> : ClasslikeMapBase<T>, IMappingProvider
    {
        public ClassMap();
        protected ClassMap(AttributeStore<ClassMapping> attributes, MappingProviderStore providers);
        public CachePart Cache { get; }
        public HibernateMappingPart HibernateMapping { get; }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public ClassMap<T> Not { get; }

        public OptimisticLockBuilder<ClassMap<T>> OptimisticLock { get; }
        public PolymorphismBuilder<ClassMap<T>> Polymorphism { get; }
        public SchemaActionBuilder<ClassMap<T>> SchemaAction { get; }

        #region IMappingProvider Members

        ClassMapping IMappingProvider.GetClassMapping();
        HibernateMapping IMappingProvider.GetHibernateMapping();
        IEnumerable<Member> IMappingProvider.GetIgnoredProperties();

        #endregion

        public virtual IdentityPart Id(Expression<Func<T, object>> memberExpression);
        public virtual IdentityPart Id(Expression<Func<T, object>> memberExpression, string column);
        public IdentityPart Id();
        public IdentityPart Id<TId>();
        public IdentityPart Id<TId>(string column);
        public virtual NaturalIdPart<T> NaturalId();
        public virtual CompositeIdentityPart<T> CompositeId();
        public virtual CompositeIdentityPart<TId> CompositeId<TId>(Expression<Func<T, TId>> memberExpression);
        public VersionPart Version(Expression<Func<T, object>> memberExpression);
        protected virtual VersionPart Version(Member property);
        public virtual DiscriminatorPart DiscriminateSubClassesOnColumn<TDiscriminator>(string columnName, TDiscriminator baseClassDiscriminator);
        public virtual DiscriminatorPart DiscriminateSubClassesOnColumn<TDiscriminator>(string columnName);
        public virtual DiscriminatorPart DiscriminateSubClassesOnColumn(string columnName);
        public virtual void UseUnionSubclassForInheritanceMapping();

        [Obsolete("Inline definitions of subclasses are depreciated. Please create a derived class from SubclassMap in the same way you do with ClassMap.")]
        public virtual void JoinedSubClass<TSubclass>(string keyColumn, Action<JoinedSubClassPart<TSubclass>> action) where TSubclass : T;

        public void Schema(string schema);
        public void Table(string tableName);
        public void LazyLoad();
        public virtual void Join(string tableName, Action<JoinPart<T>> action);
        public virtual ImportPart ImportType<TImport>();
        public void ReadOnly();
        public void DynamicUpdate();
        public void DynamicInsert();
        public ClassMap<T> BatchSize(int size);
        public void CheckConstraint(string constraint);
        public void Persister<TPersister>() where TPersister : IEntityPersister;
        public void Persister(string type);
        public void Proxy<TProxy>();
        public void Proxy(Type type);
        public void Proxy(string type);
        public void SelectBeforeUpdate();
        public void Where(string where);
        public void Subselect(string subselectSql);
        public void EntityName(string entityName);
        public ClassMap<T> ApplyFilter(string name, string condition);
        public ClassMap<T> ApplyFilter(string name);
        public ClassMap<T> ApplyFilter<TFilter>(string condition) where TFilter : new(), FilterDefinition;
        public ClassMap<T> ApplyFilter<TFilter>() where TFilter : new(), FilterDefinition;
        public TuplizerPart Tuplizer(TuplizerMode mode, Type tuplizerType);
    }
}
