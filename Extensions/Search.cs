using Framework.Exceptions;
using Framework.Models;
using System;
using System.Collections.Generic;

namespace Framework.Extensions
{
    public static class Search
    {

        public static bool CheckFilter(this bool operand, KeyValuePair<string,string> filter)
        {
            try
            {
                return operand == bool.Parse(filter.Value);
            }
            catch (System.Exception)
            {
                throw new CustomException($"Failed to parse filter, Filter: {filter.Key} Value: {filter.Value}");
            }
        }

        public static bool CheckFilter(this int operand, KeyValuePair<string, string> filter)
        {
            try
            {
                return operand == int.Parse(filter.Value);
            }
            catch (System.Exception)
            {
                throw new CustomException($"Failed to parse filter, Filter: {filter.Key} Value: {filter.Value}");
            }
        }

        public static bool CheckFilter(this int? operand, KeyValuePair<string, string> filter)
        {
            try
            {
                if (operand == null)
                    return false;
                return operand == int.Parse(filter.Value);
            }
            catch (System.Exception)
            {
                throw new CustomException($"Failed to parse filter, Filter: {filter.Key} Value: {filter.Value}");
            }
        }

        public static bool CheckFilter(this decimal operand, KeyValuePair<string, string> filter)
        {
            try
            {
                return operand == decimal.Parse(filter.Value);
            }
            catch (System.Exception)
            {
                throw new CustomException($"Failed to parse filter, Filter: {filter.Key} Value: {filter.Value}");
            }
        }

        public static bool CheckFilter(this decimal? operand, KeyValuePair<string, string> filter)
        {
            try
            {
                if (operand == null)
                    return false;
                return operand == decimal.Parse(filter.Value);
            }
            catch (System.Exception)
            {
                throw new CustomException($"Failed to parse filter, Filter: {filter.Key} Value: {filter.Value}");
            }
        }

        public static bool CheckFilter(this short operand, KeyValuePair<string, string> filter)
        {
            try
            {
                return operand == short.Parse(filter.Value);
            }
            catch (System.Exception)
            {
                throw new CustomException($"Failed to parse filter, Filter: {filter.Key} Value: {filter.Value}");
            }
        }

        public static bool CheckFilter(this short? operand, KeyValuePair<string, string> filter)
        {
            try
            {
                if (operand == null)
                    return false;
                return operand == short.Parse(filter.Value);
            }
            catch (System.Exception)
            {
                throw new CustomException($"Failed to parse filter, Filter: {filter.Key} Value: {filter.Value}");
            }
        }

        public static bool CheckFilter(this string operand, KeyValuePair<string, string> filter)
        {
            return operand.Contains(filter.Value);
        }

    }
}
