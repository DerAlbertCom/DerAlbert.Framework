using System;
using System.Collections;
using System.Linq;

namespace DerAlbert.Mappings
{
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class AutoMapperLinqProjection
    {
        readonly Type destinationType;
        readonly Type sourceType;

        public AutoMapperLinqProjection(Type destinationType, Type sourceType)
        {
            this.destinationType = destinationType;
            this.sourceType = sourceType;
        }

        private Type GetSourceType(object model)
        {
            if (sourceType != null)
                return sourceType;

            if (model is IEnumerable)
            {
                return model.GetType().GetGenericArguments()[0];
            }
            return model.GetType();
        }

        public object LinqProjection(IQueryable model)
        {
            // sourceModel.Project().To<DestinationModel>();

            var extentionsType = typeof (AutoMapper.QueryableExtensions.Extensions);

            // var projectExpression = Extension.Project<SourceType()
            var projectionMethod = extentionsType.GetGenericMethod("Project", new[] {GetSourceType(model)});
            var projectionExpression = projectionMethod.Invoke(null, new object[] {model});

            // var projection = projectionExpression.To<DestinationType()
            var toMethod = projectionExpression.GetType().GetGenericMethod("To", new[] {destinationType});
            var projection = toMethod.Invoke(projectionExpression, null);

            // return projection.ToList() // Execute Query and Convert to List
            var toListMethod = typeof (Enumerable).GetGenericMethod("ToList", new[] {destinationType});
            return toListMethod.Invoke(null, new[] {projection});
        }
    }
}