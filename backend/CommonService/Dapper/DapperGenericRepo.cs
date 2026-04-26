using CommonService.Other.AppConfig;
using Dapper;
using Microsoft.AspNetCore.Http;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using static CommonService.Other.UtilityManager;

namespace CommonService.Dapper
{
    public class DapperGenericRepo
    {
        protected ServiceResponse<int> AddUpdate(string aQuery, object aObj, SqlTransaction aSqlTransaction = null, int? aCommondTimeout = null, bool SCOPE = true)
        {
            SqlConnection lSqlConnection = aSqlTransaction != null ? aSqlTransaction.Connection : new SqlConnection(AppConfigFactory.Configs.connectionStrings.Default);
            ServiceResponse<int> serviceResponse = new ServiceResponse<int>();

            try
            {
                string forLastInsertedRecord = aQuery + (SCOPE ? " SELECT CAST(SCOPE_IDENTITY() as int)" : "");
                if (aSqlTransaction == null) lSqlConnection.Open();
                var querys = ConvertToParameterString(aQuery, new DynamicParameters(aObj));
                serviceResponse.Data = lSqlConnection.Query<int>(forLastInsertedRecord, aObj, transaction: aSqlTransaction, buffered: true, commandTimeout: aCommondTimeout).FirstOrDefault();
                serviceResponse.StatusCode = StatusCodes.Status200OK;
                return serviceResponse;
            }
            catch (Exception ex)
            {
                if (aSqlTransaction != null)
                    aSqlTransaction.Rollback();
                //serviceResponse.StatusCode = StatusCodes.Status400BadRequest;
                //return ResponseError<int>(serviceResponse, ex);
                lSqlConnection.Close();
                throw ex;
            }
            finally
            {
                if (aSqlTransaction == null) lSqlConnection.Close();
            }
        }

        protected ServiceResponse<int> Update(string aQuery, object aObj, SqlTransaction aTrans = null, int? aCommandTimeout = null)
        {
            SqlConnection lConn = aTrans == null ? new SqlConnection(AppConfigFactory.Configs.connectionStrings.Default) : aTrans.Connection;

            ServiceResponse<int> serviceResponse = new ServiceResponse<int>();
            try
            {
                if (aTrans == null) lConn.Open();
                var querys = ConvertToParameterString(aQuery, new DynamicParameters(aObj));
                serviceResponse.Data = lConn.Execute(aQuery, aObj, aTrans, aCommandTimeout);
                return serviceResponse;
            }
            catch (Exception ex)
            {
                lConn.Close();
                throw ex;
                //serviceResponse.StatusCode = StatusCodes.Status400BadRequest;
                //return ResponseError<int>(serviceResponse, ex);
            }
            finally
            {
                if (aTrans == null) lConn.Close();
            }
        }

        protected ServiceResponse<int> Execute(string aQuery, object aObj, SqlTransaction aTrans = null, int? aCommandTimeout = null)
        {
            SqlConnection lConn = aTrans == null ? new SqlConnection(AppConfigFactory.Configs.connectionStrings.Default) : aTrans.Connection;

            ServiceResponse<int> serviceResponse = new ServiceResponse<int>();
            try
            {
                if (aTrans == null) lConn.Open();
                var querys = ConvertToParameterString(aQuery, new DynamicParameters(aObj));
                serviceResponse.Data = lConn.Query<int>(aQuery, aObj, aTrans, true, aCommandTimeout).FirstOrDefault();// lConn.Execute(aQuery, aObj, aTrans, aCommandTimeout);
                serviceResponse.IsSuccess = true;
                serviceResponse.StatusCode = StatusCodes.Status200OK;
                if (serviceResponse.Data == StatusCodes.Status409Conflict || serviceResponse.Data == StatusCodes.Status208AlreadyReported || serviceResponse.Data == 408) //408 slug Url
                {
                    serviceResponse.IsSuccess = false;
                    serviceResponse.StatusCode = serviceResponse.Data;
                }
                return serviceResponse;
            }
            catch (Exception ex)
            {
                //serviceResponse.IsSuccess = false;
                //serviceResponse.StatusCode = StatusCodes.Status400BadRequest;
                //return ResponseError<int>(serviceResponse, ex);
                if (aTrans != null)
                    aTrans.Rollback();
                lConn.Close();
                throw ex;
            }
            finally
            {
                if (aTrans == null) lConn.Close();
            }
        }

        protected ServiceResponse<int> ExecuteReturnData(string aQuery, object aObj)
        {
            SqlConnection lConn = new SqlConnection(AppConfigFactory.Configs.connectionStrings.Default);
            ServiceResponse<int> serviceResponse = new ServiceResponse<int>();
            try
            {
                var querys = ConvertToParameterString(aQuery, new DynamicParameters(aObj));
                lConn.Open();
                serviceResponse.Data = lConn.Query<int>(aQuery, aObj).FirstOrDefault();
                serviceResponse.StatusCode = serviceResponse.Data;
                return serviceResponse;
            }
            catch (Exception ex)
            {
                lConn.Close();
                throw ex;
                //serviceResponse.StatusCode = StatusCodes.Status400BadRequest;
                //return ResponseError<int>(serviceResponse, ex);
            }
            finally
            {
                lConn.Close();
            }
        }

        public ServiceResponse<T> QueryFast<T>(string query, object perams)
        {
            SqlConnection lSqlConnection = new SqlConnection(AppConfigFactory.Configs.connectionStrings.Default);
            try
            {
                var querys = ConvertToParameterString(query, new DynamicParameters(perams));
                ServiceResponse<T> serviceResponse = new ServiceResponse<T>();
                lSqlConnection.Open();
                serviceResponse.Data = lSqlConnection.Query<T>(query, perams).FirstOrDefault();
                return serviceResponse;

            }
            catch (Exception ex)
            {
                lSqlConnection.Close();
                throw ex;
            }
            finally
            {
                lSqlConnection.Close();
            }
        }

        public ServiceResponse<T> Query<T>(string query, object perams)
        {
            SqlConnection lSqlConnection = new SqlConnection(AppConfigFactory.Configs.connectionStrings.Default);
            try
            {
                var querys = ConvertToParameterString(query, new DynamicParameters(perams));

                ServiceResponse<T> serviceResponse = new ServiceResponse<T>();
                lSqlConnection.Open();
                serviceResponse.Data = lSqlConnection.Query<T>(query, perams).FirstOrDefault();
                return serviceResponse;

            }
            catch (Exception ex)
            {
                lSqlConnection.Close();
                throw ex;
            }
            finally
            {
                lSqlConnection.Close();
            }
        }

        protected ServiceResponse<List<T>> QueryList<T>(string query, object perams)
        {
            using (SqlConnection lSqlConnection = new SqlConnection(AppConfigFactory.Configs.connectionStrings.Default))
            {
                ServiceResponse<List<T>> serviceResponse = new ServiceResponse<List<T>>();
                try
                {
                    var querys = ConvertToParameterString(query, new DynamicParameters(perams));
                    lSqlConnection.Open();
                    serviceResponse.Data = lSqlConnection.Query<T>(query, perams).ToList();
                    return serviceResponse;
                }
                catch (Exception ex)
                {
                    lSqlConnection.Close();
                    throw ex;
                }
                finally
                {
                    lSqlConnection.Close();
                }
            }
        }

        protected ServiceResponse<List<object>> QueryMultiple(string query, object perams)
        {
            using (SqlConnection lSqlConnection = new SqlConnection(AppConfigFactory.Configs.connectionStrings.Default))
            {
                ServiceResponse<List<object>> serviceResponsed = new ServiceResponse<List<object>>();
                try
                {
                    var querys = ConvertToParameterString(query, new DynamicParameters(perams));

                    serviceResponsed.Data = new List<object>();
                    lSqlConnection.Open();
                    var results = lSqlConnection.QueryMultiple(query, perams);
                    while (!results.IsConsumed)
                    {
                        serviceResponsed.Data.Add(results.Read());
                    }
                    return serviceResponsed;
                }
                catch (Exception ex)
                {
                    lSqlConnection.Close();
                    throw ex;
                }
                finally
                {
                    lSqlConnection.Close();
                }
            }
        }

        protected ServiceResponse<List<object>> QueryMultiple(string query, DynamicParameters perams)
        {
            using (SqlConnection lSqlConnection = new SqlConnection(AppConfigFactory.Configs.connectionStrings.Default))
            {
                ServiceResponse<List<object>> serviceResponsed = new ServiceResponse<List<object>>();
                try
                {
                    var querys = ConvertToParameterString(query, perams);

                    serviceResponsed.Data = new List<object>();
                    lSqlConnection.Open();
                    var results = lSqlConnection.QueryMultiple(query, perams);
                    while (!results.IsConsumed)
                    {
                        serviceResponsed.Data.Add(results.Read());
                    }
                    return serviceResponsed;
                }
                catch (Exception ex)
                {
                    lSqlConnection.Close();
                    throw ex;
                }
                finally
                {
                    lSqlConnection.Close();
                }
            }
        }

        private static string ConvertToParameterString(
        string queryOrSpName,
        DynamicParameters parameters,
        bool useNamedFormat = true)
        {
            if (parameters == null)
                return queryOrSpName;

            string result = queryOrSpName;

            foreach (var paramName in parameters.ParameterNames)
            {
                var value = parameters.Get<object>(paramName);
                string formattedValue = FormatSqlValue(value);

                if (useNamedFormat)
                {
                    // @Page  →  @Page=1
                    result = Regex.Replace(
                        result,
                        $@"@{paramName}\b",
                        $"@{paramName}={formattedValue}",
                        RegexOptions.IgnoreCase);
                }
                else
                {
                    // @Page  →  1
                    result = Regex.Replace(
                        result,
                        $@"@{paramName}\b",
                        formattedValue,
                        RegexOptions.IgnoreCase);
                }
            }
            return result;
        }

        private static string FormatSqlValue(object value)
        {
            if (value == null)
                return "NULL";

            if (value is string || value is char)
                return $"'{value.ToString().Replace("'", "''")}'";

            if (value is DateTime dt)
                return $"'{dt:yyyy-MM-dd HH:mm:ss.fff}'";

            if (value is bool b)
                return b ? "1" : "0";

            if (value is Enum)
                return Convert.ToInt32(value).ToString();

            if (value is Guid g)
                return $"'{g}'";

            return value.ToString();
        }



        public async Task<Dictionary<(Type Type, string Alias), IEnumerable<object>>>
        GetMultipleDatasetAsync(
            string sql,
            DynamicParameters parameters,
            params (Type Type, string Alias)[] datasets)
        {
            var result = new Dictionary<(Type, string), IEnumerable<object>>();

            using var connection = new SqlConnection(
                AppConfigFactory.Configs.connectionStrings.Default);

            await connection.OpenAsync();

            var query = ConvertToParameterString(sql, new DynamicParameters(parameters));

            using var multi = await connection.QueryMultipleAsync(query);

            foreach (var item in datasets)
            {
                var data = multi.Read(item.Type).Cast<object>().ToList();
                result[(item.Type, item.Alias)] = data;
            }

            return result;
        }

        public async Task<Dictionary<Type, IEnumerable<object>>> GetMultipleDatasetAsync(
      string sql,
      DynamicParameters parameters,
      params Type[] resultTypes)
        {
            var result = new Dictionary<Type, IEnumerable<object>>();

            using var connection = new SqlConnection(
                AppConfigFactory.Configs.connectionStrings.Default);

            await connection.OpenAsync();

            var query = ConvertToParameterString(sql, new DynamicParameters(parameters));

            using var multi = await connection.QueryMultipleAsync(query);

            foreach (var type in resultTypes)
            {
                var data = multi.Read(type).Cast<object>().ToList();
                result[type] = data;
            }

            return result;
        }

        public List<T> GetListingType<T>( Dictionary<Type, IEnumerable<object>> dictionary)
        {
            if (!dictionary.TryGetValue(typeof(T), out var data))
                return new List<T>();

            return data.Cast<T>().ToList();
        }

        public List<T> GetListingType<T>(
        Dictionary<(Type Type, string Alias), IEnumerable<object>> result,
        string alias)
        {
            return result
                .Where(x => x.Key.Type == typeof(T) && x.Key.Alias == alias)
                .SelectMany(x => x.Value)
                .Cast<T>().ToList();
        }
        protected SqlTransaction GetNewTransaction()
        {
            SqlConnection lSqlConnection = new SqlConnection(AppConfigFactory.Configs.connectionStrings.Default);
            lSqlConnection.Open();
            return lSqlConnection.BeginTransaction();
        }
    }

}
