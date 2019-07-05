using Freitas.Octopus;
using System.Linq;

namespace System.Collections.Generic
{
    public static class OctopusCollectionGenericExtensions
    {
        #region Public Methods

        public static IList<T> OctopusOrderBy<T>(this IList<T> items)
            where T : IOctopusItem
        {
            return OctopusOrderBy((IEnumerable<T>)items).ToList();
        }

        public static ICollection<T> OctopusOrderBy<T>(this ICollection<T> items)
            where T : IOctopusItem
        {
            return (ICollection<T>)OctopusOrderBy((IEnumerable<T>)items);
        }

        public static IEnumerable<T> OctopusOrderBy<T>(this IEnumerable<T> items)
            where T : IOctopusItem
        {
            return items.OrderBy(f => f.Position).AsEnumerable();
        }

        public static IList<T> InitializeOctopusOrdination<T>(this IList<T> items)
            where T : IOctopusItem
        {
            return (IList<T>)InternalInitializeOctopusOrdination(items);
        }

        public static ICollection<T> InitializeOctopusOrdination<T>(this ICollection<T> items)
            where T : IOctopusItem
        {
            return (ICollection<T>)InternalInitializeOctopusOrdination(items);
        }

        public static IEnumerable<T> InitializeOctopusOrdination<T>(this IEnumerable<T> items)
            where T : IOctopusItem
        {
            return InternalInitializeOctopusOrdination(items);
        }

        public static IList<T> MoveUp<T>(this IList<T> items, T item)
            where T : IOctopusItem
        {
            var index = items.IndexOf(item);
            if (items.Count <= 1 || index <= 0)
                return items;

            string previousPosition = string.Empty;
            string nextPosition = items[index - 1].Position;

            if (index != 1)
                previousPosition = items[index - 2].Position;

            items[index].Position = OctopusPositionGenerator.Instance.GeneratePositionValue(previousPosition, nextPosition);

            return items.OctopusOrderBy();
        }

        public static IList<T> MoveDown<T>(this IList<T> items, T item)
            where T : IOctopusItem
        {
            var index = items.IndexOf(item);
            if (items.Count <= 1 || index < 0 || index == items.Count - 1)
                return items;

            string previousPosition = items[index + 1].Position;
            string nextPosition = string.Empty;

            if (index < items.Count - 2)
                nextPosition = items[index + 2].Position;
            
            items[index].Position = OctopusPositionGenerator.Instance.GeneratePositionValue(previousPosition, nextPosition);

            return items.OctopusOrderBy();
        }

        #endregion

        #region Private Methods

        private static IEnumerable<T> InternalInitializeOctopusOrdination<T>(this IEnumerable<T> items)
            where T : IOctopusItem
        {
            string prev = string.Empty,
                   next = string.Empty;
            for (int i = 0; i < items.Count(); i++)
            {
                if (i > 0)
                {
                    prev = items.ElementAt(i - 1).Position;
                    if (items.Count() > (i + 1))
                        next = items.ElementAt(i).Position;
                }

                if (string.IsNullOrEmpty(items.ElementAt(i).Position))
                    items.ElementAt(i).Position = OctopusPositionGenerator.Instance.GeneratePositionValue(prev, next);
            }

            return items;
        }

        #endregion
    }
}
