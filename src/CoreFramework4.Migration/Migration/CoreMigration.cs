namespace CoreFramework4.Migration.Migration
{
    [AppVersionContainer("jclitenet", "{D435073A-231B-4a6f-9FC6-FD708FD263E7}")]
    public static class CoreMigration
    {
        #region Update scripts

//        private static void Update_1001()
//        {
//            #region Add [Core Migration] table
//            string sql = @"
//                BEGIN TRANSACTION
//
//                IF NOT EXISTS (SELECT * FROM sysobjects WHERE type = 'U' AND name = 'dbo.CoreAppSetting')
//                BEGIN
//
//                    CREATE TABLE [dbo].[CoreAppSetting]
//                    (
//	                    [Guid] [uniqueidentifier] NOT NULL,
//	                    [Value] [nvarchar](max) NULL
//                    ) ON [PRIMARY]
//
//                    ALTER TABLE [dbo].[CoreAppSetting] add CONSTRAINT [PK_CoreAppSetting]
//                    PRIMARY KEY CLUSTERED 
//                    (
//	                    [Guid] ASC
//                    )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
//
//                END
//
//                COMMIT";
//            CoreFramework4.Database.DBM.Default.ExecuteNonQuery(sql);

//            #endregion
//        }

        #endregion
    }
}