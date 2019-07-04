using Freitas.Octopus;
using Freitas.Octopus.Ordination;
using System.Linq;

namespace System.Collections.Generic
{
    public static class OctopusCollectionGenericExtensions
    {
        #region Public Methods

        public static IList<IOctopusItem> OrderByOctopus(this IList<IOctopusItem> items)
        {
            return (IList<IOctopusItem>)OrderByOctopus(items.AsEnumerable());
        }

        public static ICollection<IOctopusItem> OrderByOctopus(this ICollection<IOctopusItem> items)
        {
            return (ICollection<IOctopusItem>)OrderByOctopus(items.AsEnumerable());
        }

        public static IEnumerable<IOctopusItem> OrderByOctopus(this IEnumerable<IOctopusItem> items)
        {
            return items.OrderBy(f => f.Position);
        }

        public static IList<IOctopusItem> InitializeOctopusOrdination(this IList<IOctopusItem> items)
        {
            return (IList<IOctopusItem>)InternalInitializeOctopusOrdination(items);
        }

        public static ICollection<IOctopusItem> InitializeOctopusOrdination(this ICollection<IOctopusItem> items)
        {
            return (ICollection<IOctopusItem>)InternalInitializeOctopusOrdination(items);
        }

        public static IEnumerable<IOctopusItem> InitializeOctopusOrdination(this IEnumerable<IOctopusItem> items)
        {
            return InternalInitializeOctopusOrdination(items);
        }

        #endregion

        #region Private Methods

        private static IEnumerable<IOctopusItem> InternalInitializeOctopusOrdination(this IEnumerable<IOctopusItem> items)
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
                    items.ElementAt(i).Position = TextOrdination.Instance.GenerateOrdination(prev, next);
            }

            return items;
        }

        #endregion
    }
}
