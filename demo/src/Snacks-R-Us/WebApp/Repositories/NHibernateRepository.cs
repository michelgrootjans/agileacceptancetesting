using System.Collections.Generic;
using NHibernate;

namespace Snacks_R_Us.WebApp.Repositories
{
    public class NHibernateRepository : IRepository
    {
        private ISession Session
        {
            get { return NHibernateHelper.GetCurrentSession(); }
        }

        public IEnumerable<T> FindAll<T>() where T : class
        {
            return Session.CreateCriteria(typeof(T)).List<T>();
        }

        //public IEnumerable<T> FindAll<T>(IQueryExpression<T> where) where T : class
        //{
        //    return FindAll(where.Expression);
        //}

        //public IEnumerable<T> FindAll<T>(Expression<Func<T, bool>> where) where T : class
        //{
        //    return QueryAll(where).ToList();
        //}

        //public T FindOne<T>(IQueryExpression<T> where)
        //{
        //    return FindOne(where.Expression);
        //}

        public Entity Get<Entity>(object id)
        {
            return Session.Get<Entity>(id);
        }

        public void Save<T>(T entity)
        {
            Session.SaveOrUpdate(entity);
        }

        //public IQueryable<T> QueryAll<T>(Expression<Func<T, bool>> where)
        //{
        //    return Query<T>().Where(where);
        //}

        //public IQueryable<T> Query<T>()
        //{
        //    var options = new QueryOptions();
        //    return new Query<T>(new NHibernateQueryProvider(Session, options), options);
        //}

        //public T FindOne<T>(Expression<Func<T, bool>> where)
        //{
        //    return Query<T>().Where(where).FirstOrDefault();
        //}
    }
}