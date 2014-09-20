using System;
using System.Linq.Expressions;
using System.ComponentModel;
using System.Reflection;

namespace HelloWord
{
    public static class PropertyExtensions
    {
        public static IDisposable SubscribeToPropertyChanged<TSource, TProp>(
            this TSource source,
            Expression<Func<TSource, TProp>> propertySelector,
            Action onChanged)
            where TSource : INotifyPropertyChanged
        {
            if (source == null) throw new ArgumentNullException("source");
            if (propertySelector == null) throw new ArgumentNullException("propertySelector");
            if (onChanged == null) throw new ArgumentNullException("onChanged");

            var subscribedPropertyName = GetPropertyName(propertySelector);

            PropertyChangedEventHandler handler = (s, e) =>
            {
                if (string.Equals(e.PropertyName, subscribedPropertyName, StringComparison.InvariantCulture))
                    onChanged();
            };

            source.PropertyChanged += handler;

            return Disposable.Create(() => source.PropertyChanged -= handler);
        }

        private static string GetPropertyName<TSource, TProp>(Expression<Func<TSource, TProp>> propertySelector)
        {
            var memberExpr = propertySelector.Body as MemberExpression;

            if (memberExpr == null) throw new ArgumentException("must be a member accessor", "propertySelector");

            var propertyInfo = memberExpr.Member as PropertyInfo;

            if (propertyInfo == null || propertyInfo.DeclaringType != typeof(TSource))
                throw new ArgumentException("must yield a single property on the given object", "propertySelector");

            return propertyInfo.Name;
        }
    }
}

