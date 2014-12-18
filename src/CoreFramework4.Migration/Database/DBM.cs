using System;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace CoreFramework4.Database
{
    /// <summary>
    /// Database Manager - Simplifying access to database objects
    /// </summary>
    public class DBM
    {
        #region Default
        static DBM _Default;
        public static DBM Default
        {
            get
            {
                if (_Default == null) _Default = DBM.LoadFromWebConfigConnectionStrings("DefaultConnectionString");
                return _Default;
            }
        }
        #endregion

        #region Methods
        public DataTable GetDataTable(string SQL)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                SqlDataAdapter da = new SqlDataAdapter(SQL, conn);
                DataTable result = new DataTable();
                da.Fill(result);

                conn.Close();
                return result;
            }
        }

        public int ExecuteNonQuery(string SQL)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(SQL, conn);
                int result = cmd.ExecuteNonQuery();

                conn.Close();
                return result;
            }
        }

        public object ExecuteScalar(string SQL)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(SQL, conn);
                object result = cmd.ExecuteScalar();

                conn.Close();
                return result;
            }
        }
        #endregion

        public DBM(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public static DBM LoadFromWebConfigConnectionStrings(string key)
        {
            return new DBM(ConfigurationManager.ConnectionStrings[key].ConnectionString);
        }

        public SqlCommandEx CreateCommand(string SQL)
        {
            return new SqlCommandEx(SQL, ConnectionString);
        }

        public string ConnectionString { get; set; }

        public class SqlCommandEx : IDisposable
        {
            public SqlConnection Connection { get; set; }
            public SqlCommand CommandObject { get; set; }

            public SqlCommandEx(string SQL, string ConnectionString)
            {
                Connection = new SqlConnection(ConnectionString);
                Connection.Open();
                CommandObject = new SqlCommand(SQL, Connection);
            }

            public DataTable GetDataTable()
            {
                SqlDataAdapter da = new SqlDataAdapter(CommandObject);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }

            #region AddWithValue

            public void AddWithValue(string parameter, Guid value)
            {
                CommandObject.Parameters.AddWithValue(parameter, value.ToString());
            }

            public void AddWithValue(string parameter, object value)
            {
                if (value != null)
                {
                    CommandObject.Parameters.AddWithValue(parameter, value);
                }
                else
                    CommandObject.Parameters.AddWithValue(parameter, DBNull.Value);
            }

            #endregion

            public bool StoredProcedure
            {
                get
                {
                    return (this.CommandObject.CommandType == CommandType.StoredProcedure);
                }
                set
                {
                    if (value) this.CommandObject.CommandType = CommandType.StoredProcedure;
                    else this.CommandObject.CommandType = CommandType.Text;
                }
            }

            public void Dispose()
            {
                Connection.Close();
            }
        }
    }

    public static class DBMStringExtension
    {
        public static string SqlReplaceQuotes(this string str)
        {
            return str.Replace("'", "''");
        }
    }

}