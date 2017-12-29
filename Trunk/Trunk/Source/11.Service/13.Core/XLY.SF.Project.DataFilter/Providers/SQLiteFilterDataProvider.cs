﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SQLite;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using XLY.SF.Project.DataFilter.Asist;

namespace XLY.SF.Project.DataFilter.Providers
{
    /// <summary>
    /// 基于SQLite数据库的数据提供器。
    /// </summary>
    public class SQLiteFilterDataProvider : SQLFilterDataProvider
    {
        #region Constructors

        static SQLiteFilterDataProvider()
        {
            SQLiteFunction.RegisterFunction(typeof(RegexMatchFunction));
        }

        public SQLiteFilterDataProvider(String file, String tableName, String password = "")
            : base(new SQLiteConnectionStringBuilder()
            {
                DataSource = file ?? throw new ArgumentNullException("file"),
                Password = password
            }.ConnectionString, tableName)
        {
        }

        #endregion

        #region Properties

        #endregion

        #region Nested Type

        [SQLiteFunction(Name = "RegexMatch", FuncType = FunctionType.Scalar, Arguments = 2)]
        public class RegexMatchFunction : SQLiteFunction
        {
            public override Object Invoke(Object[] args)
            {
                return Regex.IsMatch(Convert.ToString(args[0]), Convert.ToString(args[1]), RegexOptions.Multiline);
            }
        }

        #endregion

        #region Methods

        #region Public

        /// <summary>
        /// 获取数据（目前暂时使用内容为SQL语句的常量表达式）。
        /// </summary>
        /// <param name="expression">表达式。</param>
        /// <param name="count">集合的大小。</param>
        /// <returns>数据。</returns>
        public override IEnumerable<T> Query<T>(Expression expression)
        {
            SQLiteConnection connection = new SQLiteConnection(ConnectionString);
            connection.Flags = SQLiteConnectionFlags.UseConnectionPool;
            connection.Open();

            SQLiteCommand command = connection.CreateCommand();
            command.CommandText = SQLExpressionConverter.GetSelectionSql(expression, TableName);
            DbDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            return new DbEnumerableDataReader<T>(reader);
        }

        /// <summary>
        /// 查询数量。
        /// </summary>
        /// <param name="expression">表达式。</param>
        /// <returns>集合的大小。</returns>
        public override Int32 GetCount(Expression expression)
        {
            lock (_Catch)
            {
                var connection = GetSQLiteConnection(ConnectionString);

                SQLiteCommand command = connection.CreateCommand();
                command.CommandText = SQLExpressionConverter.GetCountSql(expression, TableName);
                return (Int32)(Int64)command.ExecuteScalar();
            }
        }

        private static Dictionary<string, SQLiteConnection> _Catch = new Dictionary<string, SQLiteConnection>();

        private static SQLiteConnection GetSQLiteConnection(string connectionString)
        {
            SQLiteConnection connection = null;

            if (!_Catch.TryGetValue(connectionString, out connection))
            {
                connection = new SQLiteConnection(connectionString);
                connection.Flags = SQLiteConnectionFlags.UseConnectionPool;
                connection.Open();

                _Catch.Add(connectionString, connection);
            }

            return connection;
        }

        public static void ClearSQLiteConnectionCatch(string dbPath)
        {
            lock (_Catch)
            {
                SQLiteConnection connection = null;

                var conStr = _Catch.Keys.FirstOrDefault(s => s.Contains(dbPath));
                if (!String.IsNullOrEmpty(conStr) && _Catch.TryGetValue(conStr, out connection))
                {
                    connection?.Close();

                    _Catch.Remove(conStr);
                }
            }
        }

        #endregion

        #endregion
    }
}
