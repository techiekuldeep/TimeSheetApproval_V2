using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace TimeSheetApproval.Application.Parameters
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> ToFilterView<T>(
            this IQueryable<T> query, SearchFilters filter)
        {

            query = Filters(query, filter.FilterParams);
            filter.TotalCount = query.Count();

            if (filter.SortParams != null)
            {
                query = Sort(query, filter.SortParams);

                query = Limit(query, filter.PageSize, filter.PageNumber);
            }

            return query;
        }

        private static IQueryable<T> Filters<T>(
            IQueryable<T> queryable, Filters filter)
        {
            if ((filter != null) && (filter.LogicOperator != null))
            {
                var filters = GetAllFilters(filter);
                var values = filters.Select(f =>  f.FilterValue.ToString().ToLower()).ToArray();
                var where = Transform(filter, filters);
                queryable = queryable.Where(where, values);
            }
            return queryable;
        }

        private static IQueryable<T> Sort<T>(
            IQueryable<T> queryable, IEnumerable<Sorter> sort)
        {
            if (sort != null && sort.Any())
            {
                var ordering = string.Join(",",
                  sort.Select(s => $"{s.SortColumn} {s.SortOrder}"));
                return queryable.OrderBy(ordering);
            }
            return queryable;
        }

        private static IQueryable<T> Limit<T>(
          IQueryable<T> queryable, int pageSize, int pageNo)
        {

            pageNo = pageSize == -1 ? 1:(pageNo - 1) * pageSize;
            return pageSize == -1 ? queryable : queryable.Skip(pageNo).Take(pageSize);
        }

        private static readonly IDictionary<string, string>
        Operators = new Dictionary<string, string>
        {
        {"eq", "="},
        {"neq", "!="},
        {"lt", "<"},
        {"lte", "<="},
        {"gt", ">"},
        {"gte", ">="},
        {"startswith", "StartsWith"},
        {"endswith", "EndsWith"},
        {"contains", "Contains"},
        {"doesnotcontain", "Contains"},
        };

        public static IList<Filters> GetAllFilters(Filters filter)
        {
            var filters = new List<Filters>();
            GetFilters(filter, filters);
            return filters;
        }

        private static void GetFilters(Filters filter, IList<Filters> filters)
        {
            if (filter.FilterList != null && filter.FilterList.Any())
            {
                foreach (var item in filter.FilterList)
                {
                    GetFilters(item, filters);
                }
            }
            else
            {
                filters.Add(filter);
            }
        }

        public static string Transform(Filters filter, IList<Filters> filters)
        {
            if (filter.FilterList != null && filter.FilterList.Any())
            {
                return "(" + String.Join(" " + filter.LogicOperator + " ",
                    filter.FilterList.Select(f => Transform(f, filters)).ToArray()) + ")";
            }
            int index = filters.IndexOf(filter);
            var comparison = Operators[filter.Operator];
            if (filter.Operator == "doesnotcontain")
            {
                return String.Format("({0} != null && !Convert.ToString({0}).ToLower().{1}(@{2}))",
                    filter.FilterColumn, comparison, index);
            }
            if (comparison == "StartsWith" ||
                comparison == "EndsWith" ||
                comparison == "Contains")
            {
                return String.Format("({0} != null && Convert.ToString({0}).ToLower().{1}(@{2}))",
                filter.FilterColumn, comparison, index);
            }
            return String.Format("{0} {1} @{2}", filter.FilterColumn, comparison, index);
        }
    }

}
