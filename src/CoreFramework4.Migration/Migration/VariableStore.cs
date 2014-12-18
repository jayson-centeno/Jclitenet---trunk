using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreFramework4.Database;

namespace CoreFramework4.Migration.Migration
{
    public static class VariableStore
    {
        const string TABLE_NAME = "MigrationSetting";

        static VariableStore()
        {
            #region Create if variables table doesn't exist
            using (var sqlex = DBM.Default.CreateCommand("IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[" + TABLE_NAME + "]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1) SELECT 1 ELSE SELECT 0"))
            {
                bool tableExists = (int)sqlex.CommandObject.ExecuteScalar() == 1;

                if (!tableExists)
                {
                    DBM.Default.ExecuteNonQuery(@"
                         CREATE TABLE [dbo].[" + TABLE_NAME + @"](
	                        [Key] [nvarchar](250) NOT NULL,
	                        [Value] [ntext] NOT NULL,
                         CONSTRAINT [PK_RHF_VARIABLES] PRIMARY KEY CLUSTERED ( [Key] ASC ) )"
                    );
                }
            }
            #endregion
        }

        public static string Get(string key)
        {
            using (var sqlex = DBM.Default.CreateCommand("SELECT Value FROM [" + TABLE_NAME + "] WHERE [Key]=@key"))
            {
                sqlex.AddWithValue("@key", key);

                var dr = sqlex.CommandObject.ExecuteReader();
                if (!dr.Read()) return null;
                return dr[0].ToString();
            }
        }

        public static void Set(string key, string value)
        {
            if (value != null)
            {
                //Update
                using (var sqlex = DBM.Default.CreateCommand("UPDATE [" + TABLE_NAME + "] SET [Value] = @value WHERE [Key] = @key"))
                {
                    sqlex.AddWithValue("@key", key);
                    sqlex.AddWithValue("@value", value);

                    if (sqlex.CommandObject.ExecuteNonQuery() == 0)
                    {
                        //Insert (if no rows affected by update)
                        using (var sqlex_ins = DBM.Default.CreateCommand("INSERT INTO [" + TABLE_NAME + "] ([Key],[Value]) VALUES (@key, @value)"))
                        {
                            sqlex_ins.AddWithValue("@key", key);
                            sqlex_ins.AddWithValue("@value", value);
                            sqlex_ins.CommandObject.ExecuteNonQuery();
                        }
                    }
                }
            }
            else
            {
                //Delete
                using (var sqlex = DBM.Default.CreateCommand("DELETE FROM [" + TABLE_NAME + "] WHERE [Key] = @key"))
                {
                    sqlex.AddWithValue("@key", key);
                    sqlex.CommandObject.ExecuteNonQuery();
                }
            }
        }
    }
}
