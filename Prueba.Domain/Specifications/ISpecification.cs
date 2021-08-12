using System;
using System.Linq.Expressions;

namespace Prueba.Domain.Specifications
{
    public interface ISpecification<T>
    {
        Expression<Func<T, bool>> SpecExpression { get; }
        bool IsSatisfiedBy(T obj);
    }
}
