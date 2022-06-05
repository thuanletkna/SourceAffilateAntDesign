using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.DataSource;

namespace AffilateSource.Shared.Kendohelpers
{
    public static class KendoApplyFilter
    {
        public static string GetSorts<T>(DataSourceRequest request)
        {
            if (request.Filters == null)
                return " (SELECT NULL) ";

            List<string> sort = new List<string>();
            var Tname = typeof(T).Name;
            if (request.Sorts.Any())
            {
                foreach (SortDescriptor sortDescriptor in request.Sorts)
                {
                    if (sortDescriptor.SortDirection == Telerik.DataSource.ListSortDirection.Ascending)
                        sort.Add(sortDescriptor.Member + " ASC");
                    else
                        sort.Add(sortDescriptor.Member + " DESC");
                }
            }

            string sortString = string.Join(",", sort.Select(s => s));
            return string.IsNullOrEmpty(sortString) ? " (SELECT NULL) " : sortString;
        }
        public static string ApplyFilter(IFilterDescriptor filter)
        {
            return ApplyFilter(filter, "");
        }

        public static string ApplyFilter(IFilterDescriptor filter, string id)
        {
            var filters = string.Empty;
            if (filter is CompositeFilterDescriptor)
            {
                filters += "(";
                var compositeFilterDescriptor = (CompositeFilterDescriptor)filter;
                foreach (IFilterDescriptor childFilter in compositeFilterDescriptor.FilterDescriptors)
                {
                    filters += ApplyFilter(childFilter, id);
                    filters += string.Format(" {0} ", compositeFilterDescriptor.LogicalOperator.ToString());
                }
            }
            else
            {
                string filterDescriptor = string.Empty;
                var descriptor = (FilterDescriptor)filter;
                var filterMember = descriptor.Member;
                var filterValue = descriptor.Value.ToString().Replace("'", "''");

                DateTime temp;

                switch (descriptor.Operator)
                {
                    case FilterOperator.IsEqualTo:
                        if (filterMember.Contains('.'))
                            filterDescriptor += string.Format("{0} = N'{1}'", id + filterMember, filterValue);
                        else
                            filterDescriptor += string.Format("{0} = N'{1}'", id + "[" + filterMember + "]", filterValue);
                        break;
                    case FilterOperator.IsNotEqualTo:
                        if (filterMember.Contains('.'))
                            filterDescriptor += string.Format("{0} <> N'{1}'", id + filterMember, filterValue);
                        else
                            filterDescriptor += string.Format("{0} <> N'{1}'", id + "[" + filterMember + "]", filterValue);
                        break;
                    case FilterOperator.StartsWith:
                        if (filterMember.Contains('.'))
                            filterDescriptor += string.Format("{0} COLLATE Latin1_General_CI_AI LIKE N'{1}%'", id + filterMember, filterValue);
                        else
                            filterDescriptor += string.Format("{0} COLLATE Latin1_General_CI_AI LIKE N'{1}%'", id + "[" + filterMember + "]", filterValue);

                        break;
                    case FilterOperator.Contains:
                        if (filterMember.Contains('.'))
                            filterDescriptor += string.Format("{0} COLLATE Latin1_General_CI_AI LIKE N'%{1}%'", id + filterMember, filterValue);
                        else
                            filterDescriptor += string.Format("{0} COLLATE Latin1_General_CI_AI LIKE N'%{1}%'", id + "[" + filterMember + "]", filterValue);

                        break;
                    case FilterOperator.EndsWith:
                        if (filterMember.Contains('.'))
                            filterDescriptor += string.Format("{0} COLLATE Latin1_General_CI_AI LIKE N'%{1}'", id + filterMember, filterValue);
                        else
                            filterDescriptor += string.Format("{0} COLLATE Latin1_General_CI_AI LIKE N'%{1}'", id + "[" + filterMember + "]", filterValue);
                        break;
                    case FilterOperator.IsLessThanOrEqualTo:
                        if (DateTime.TryParse(filterValue.ToString(), out temp))
                        {
                            filterDescriptor += string.Format("{0} <='{1}'", id + "[" + filterMember + "]", filterValue);
                        }
                        else
                        {
                            filterDescriptor += string.Format("{0} <={1}", id + "[" + filterMember + "]", filterValue);
                        }
                        break;
                    case FilterOperator.IsLessThan:
                        if (DateTime.TryParse(filterValue.ToString(), out temp))
                        {
                            filterDescriptor += string.Format("{0}<'{1}'", id + "[" + filterMember + "]", filterValue);
                        }
                        else
                        {
                            filterDescriptor += string.Format("{0}<{1}", id + "[" + filterMember + "]", filterValue);
                        }


                        break;
                    case FilterOperator.IsGreaterThanOrEqualTo:
                        if (DateTime.TryParse(filterValue.ToString(), out temp))
                        {
                            filterDescriptor += string.Format("{0}>='{1}'", id + "[" + filterMember + "]", filterValue);
                        }
                        else
                        {
                            filterDescriptor += string.Format("{0}>={1}", id + "[" + filterMember + "]", filterValue);
                        }

                        break;
                    case FilterOperator.IsGreaterThan:
                        if (DateTime.TryParse(filterValue.ToString(), out temp))
                        {
                            filterDescriptor += string.Format("{0}>'{1}'", id + "[" + filterMember + "]", filterValue);
                        }
                        else
                        {
                            filterDescriptor += string.Format("{0}>{1}", id + "[" + filterMember + "]", filterValue);
                        }

                        break;
                }

                filters = filterDescriptor;
            }

            filters = filters.EndsWith("And ") == true ? string.Format("{0})", filters.Substring(0, filters.Length - 4)) : filters;
            filters = filters.EndsWith("Or ") == true ? string.Format("{0})", filters.Substring(0, filters.Length - 4)) : filters;

            return filters;
        }

        public static void ChangeMemberName(IEnumerable<IFilterDescriptor> filters, string memberToSearchFor, string newMemberName)
        {
            foreach (var filter in filters)
            {
                if (filter is CompositeFilterDescriptor)
                {
                    ChangeMemberName(((CompositeFilterDescriptor)filter).FilterDescriptors, memberToSearchFor, newMemberName);
                }
                else if (((FilterDescriptor)filter).Member == memberToSearchFor)
                {
                    ((FilterDescriptor)filter).Member = newMemberName;
                }
            }
        }
    }
}
