using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreFramework4.Migration.Migration;
using CoreFramework4.Database;

namespace CoreFramework4.Migration
{
    [AppVersionContainer("jclitenet", "{4B543CC9-7A7B-4C9F-A43A-A514B9221248}")]
    public static class AppMigration
    {
        private static void Update_1001()
        {
            #region Add All System Tables
            string sql = @"
                BEGIN TRANSACTION

                IF NOT EXISTS (SELECT * FROM sysobjects WHERE type = 'U' AND name = 'User')
                BEGIN

                    CREATE TABLE [dbo].[User](
	                [ID] [int] IDENTITY(1,1) NOT NULL,
	                [UID] [uniqueidentifier] NOT NULL,
	                [User_PKID] [uniqueidentifier] NULL,
	                [FirstName] [nvarchar](50) NULL,
	                [LastName] [nvarchar](50) NULL,
	                [IsGuest] [bit] NULL,
	                [Email] [nvarchar](30) NULL,
	                [Password] [nvarchar](100) NULL,
                 CONSTRAINT [PK_UserInfo_1] PRIMARY KEY CLUSTERED 
                (
	                [UID] ASC
                )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
                ) ON [PRIMARY]
                
                END

                COMMIT";

            DBM.Default.ExecuteNonQuery(sql);

            sql = @"
                BEGIN TRANSACTION

                IF NOT EXISTS (SELECT * FROM sysobjects WHERE type = 'U' AND name = 'Album')
                BEGIN

                    CREATE TABLE [dbo].[Album](
	                    [ID] [int] IDENTITY(1,1) NOT NULL,
	                    [UID] [uniqueidentifier] NOT NULL,
	                    [Name] [nvarchar](300) NULL,
	                    [Comment] [nvarchar](500) NULL,
	                    [CreatedBy] [uniqueidentifier] NULL,
	                    [DateCreated] [datetime] NULL,
	                    [IsDeleted] [bit] NULL,
                     CONSTRAINT [PK_PhotoAlbum] PRIMARY KEY CLUSTERED 
                    (
	                    [UID] ASC
                    )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
                    ) ON [PRIMARY]
                
                END

                COMMIT";

            DBM.Default.ExecuteNonQuery(sql);

            sql = @"
                BEGIN TRANSACTION
                
                IF NOT EXISTS (SELECT * FROM sysobjects WHERE type = 'U' AND name = 'Photo')
                BEGIN

                    CREATE TABLE [dbo].[Photo](
	                    [ID] [int] IDENTITY(1,1) NOT NULL,
	                    [UID] [uniqueidentifier] NOT NULL,
	                    [Album_UID] [uniqueidentifier] NULL,
	                    [Name] [nvarchar](50) NULL,
	                    [FileName] [nvarchar](100) NULL,
	                    [CreatedBy] [uniqueidentifier] NULL,
	                    [DateCreated] [datetime] NULL,
	                    [IsDeleted] [bit] NULL,
                     CONSTRAINT [PK_AlbumPhotos] PRIMARY KEY CLUSTERED 
                    (
	                    [UID] ASC
                    )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
                    ) ON [PRIMARY]
                    
                    ALTER TABLE [dbo].[Photo]  WITH CHECK ADD  CONSTRAINT [FK_Photo_Album] FOREIGN KEY([Album_UID])
                    REFERENCES [dbo].[Album] ([UID])
                    ALTER TABLE [dbo].[Photo] CHECK CONSTRAINT [FK_Photo_Album]
                    
                END

                COMMIT";

            DBM.Default.ExecuteNonQuery(sql);

            sql = @"
                BEGIN TRANSACTION

                IF NOT EXISTS (SELECT * FROM sysobjects WHERE type = 'U' AND name = 'TutorialType')
                BEGIN

                    CREATE TABLE [dbo].[TutorialType](
	                    [ID] [int] IDENTITY(1,1) NOT NULL,
	                    [UID] [uniqueidentifier] NOT NULL,
	                    [Name] [nvarchar](50) NULL,
                     CONSTRAINT [PK_TutorialType] PRIMARY KEY CLUSTERED 
                    (
	                    [UID] ASC
                    )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
                    ) ON [PRIMARY]
                   
                    ALTER TABLE [dbo].[TutorialType] ADD  CONSTRAINT [DF_TutorialType_UID]  DEFAULT (newid()) FOR [UID] 
                END

                COMMIT";

            DBM.Default.ExecuteNonQuery(sql);

            sql = @"
                BEGIN TRANSACTION

                IF NOT EXISTS (SELECT * FROM sysobjects WHERE type = 'U' AND name = 'TutorialCategory')
                BEGIN

                    CREATE TABLE [dbo].[TutorialCategory](
	                    [ID] [int] IDENTITY(1,1) NOT NULL,
	                    [UID] [uniqueidentifier] NOT NULL,
	                    [Name] [nvarchar](50) NULL,
	                    [TutorialType_UID] [uniqueidentifier] NULL,
                     CONSTRAINT [PK_TutorialCategory] PRIMARY KEY CLUSTERED 
                    (
	                    [UID] ASC
                    )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
                    ) ON [PRIMARY]

                    ALTER TABLE [dbo].[TutorialCategory]  WITH CHECK ADD  CONSTRAINT [FK_TutorialCategory_TutorialType] FOREIGN KEY([TutorialType_UID])
                    REFERENCES [dbo].[TutorialType] ([UID])
                    ALTER TABLE [dbo].[TutorialCategory] CHECK CONSTRAINT [FK_TutorialCategory_TutorialType]

                END

                COMMIT";

            DBM.Default.ExecuteNonQuery(sql);

            sql = @"
                BEGIN TRANSACTION

                IF NOT EXISTS (SELECT * FROM sysobjects WHERE type = 'U' AND name = 'Tutorial')
                BEGIN

                    CREATE TABLE [dbo].[Tutorial](
	                    [ID] [int] IDENTITY(1,1) NOT NULL,
	                    [UID] [uniqueidentifier] NOT NULL,
	                    [TutorialCategory_UID] [uniqueidentifier] NULL,
	                    [Name] [nvarchar](50) NULL,
	                    [HtmlContent] [ntext] NULL,
	                    [CreatedBy] [uniqueidentifier] NULL,
	                    [DateCreated] [datetime] NULL,
	                    [IsDeleted] [bit] NULL,
                     CONSTRAINT [PK_Tutorial] PRIMARY KEY CLUSTERED 
                    (
	                    [UID] ASC
                    )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
                    ) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

                    ALTER TABLE [dbo].[Tutorial]  WITH CHECK ADD  CONSTRAINT [FK_Tutorial_TutorialCategory] FOREIGN KEY([TutorialCategory_UID])
                    REFERENCES [dbo].[TutorialCategory] ([UID])
                    ALTER TABLE [dbo].[Tutorial] CHECK CONSTRAINT [FK_Tutorial_TutorialCategory]

                END

                COMMIT";

            DBM.Default.ExecuteNonQuery(sql);

            #endregion
        }

        private static void Update_1002()
        {
            string sql = @"
                BEGIN TRANSACTION

                IF NOT EXISTS (SELECT * FROM sysobjects WHERE type = 'U' AND name = 'ChangeLog')
                BEGIN

                    CREATE TABLE [dbo].[ChangeLog](
	                [ID] [int] IDENTITY(1,1) NOT NULL,
	                [Description] [NVARCHAR](max) NULL,
	                [DateCreated] DateTime NULL,
                CONSTRAINT [PK_ChangeLog] PRIMARY KEY CLUSTERED 
                (
	                [ID] ASC
                )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
                ) ON [PRIMARY]
                
                END

                COMMIT";

            DBM.Default.ExecuteNonQuery(sql);
        }

        private static void Update_1003()
        {
            string sql = @"

                BEGIN TRANSACTION

                IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[Comment]') AND name = 'Website')
                BEGIN
	                ALTER TABLE Comment
	                ADD Website NVARCHAR(50) NULL
                END

                COMMIT";

            DBM.Default.ExecuteNonQuery(sql);
        }

        private static void Update_1004()
        {
            string sql = @"

                BEGIN TRANSACTION

                IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[Comment]') AND name = 'Tutorial_UID')
                BEGIN

                    ALTER TABLE Comment
                    ADD Tutorial_UID UNIQUEIDENTIFIER NULL

	                ALTER TABLE [dbo].[Comment]  WITH CHECK ADD  CONSTRAINT [FK_Comment_Tutorial] FOREIGN KEY([Tutorial_UID])
	                REFERENCES [dbo].[Tutorial] ([UID])

	                ALTER TABLE [dbo].[Comment] CHECK CONSTRAINT [FK_Comment_Tutorial]

                END

                COMMIT";

            DBM.Default.ExecuteNonQuery(sql);
        }

        private static void Update_1005() 
        {
            string sql = @"

                BEGIN TRANSACTION

                IF NOT EXISTS (SELECT * FROM sysobjects WHERE type = 'U' AND name = 'SiteConfiguration')
                BEGIN

                CREATE TABLE [dbo].[SiteConfiguration](
	                [ID] [int] IDENTITY(1,1) NOT NULL,
	                [Name] [nvarchar](100) NULL,
	                [ConfigValue] [nvarchar](max) NULL,
	                [IsDeleted] [bit] NULL,
                    [IsHtml] [bit] NULL,
                 CONSTRAINT [PK_SiteConfiguration] PRIMARY KEY CLUSTERED 
                (
	                [ID] ASC
                )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
                ) ON [PRIMARY]

                ALTER TABLE [dbo].[SiteConfiguration] ADD  CONSTRAINT [DF_SiteConfiguration_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]

                ALTER TABLE [dbo].[SiteConfiguration] ADD  CONSTRAINT [DF_SiteConfiguration_IsHhtml]  DEFAULT ((0)) FOR [IsHtml]

                END
                COMMIT";

            DBM.Default.ExecuteNonQuery(sql);
        }

        private static void Update_1006()
        {
            string sql = @"

                BEGIN TRANSACTION

                IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Game]') AND type in (N'U'))
                BEGIN  
                    DROP TABLE [dbo].[Game]
                END
                
                CREATE TABLE [dbo].[Game](
	                [ID] [int] IDENTITY(1,1) NOT NULL,
	                [Name] [nvarchar](300) NULL,
	                [HtmlContent] [ntext] NULL,
	                [CreatedBy] [uniqueidentifier] NULL,
	                [DateCreated] [datetime] NULL,
	                [IsDeleted] [bit] NULL,
                 CONSTRAINT [PK_Game] PRIMARY KEY CLUSTERED 
                (
	                [ID] ASC
                )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
                ) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

                COMMIT";

            DBM.Default.ExecuteNonQuery(sql);
        }

        private static void Update_1007()
        {
            string sql = @"

                BEGIN TRANSACTION

                INSERT INTO [jclitenet].[dbo].[Game]
                           ([Name]
                           ,[HtmlContent]
                           ,[DateCreated]
                           ,[IsDeleted])
                     VALUES
                           ('Picture Shuffle'
                           ,'&lt;p&gt; Here&#39;s my first javascript Picture Shuffle Game. This was one of the game I made when I was using VB6 language but now since I have my own website. I decided to create a web version of it. 
                &lt;/p&gt;

                &lt;p&gt; Feel free to download the source codes. It has some minor bugs but for the basic parts It&#39;s working just fine. &lt;/p&gt;
                &lt;p&gt; 
                &lt;a href=&quot;/Scripts/games/picShuffle/picShuffle.zip&quot;&gt;Download here&lt;/a&gt;
                &lt;/p&gt;'
                           , '10/29/13'
                           ,0)

                COMMIT";

            DBM.Default.ExecuteNonQuery(sql);
        }

        private static void Update_1008()
        {
            string sql = @"

                BEGIN TRANSACTION

                IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[Comment]') AND name = 'Game_ID')
                BEGIN

                    ALTER TABLE Comment
                    ADD Game_ID INT NULL

	                ALTER TABLE [dbo].[Comment]  WITH CHECK ADD  CONSTRAINT [FK_Comment_Game] FOREIGN KEY([Game_ID])
                    REFERENCES [dbo].[Game] ([ID])

	                ALTER TABLE [dbo].[Comment] CHECK CONSTRAINT [FK_Comment_Game]

                END

                COMMIT";

            DBM.Default.ExecuteNonQuery(sql);
        }

        private static void Update_1009()
        {
            string sql = @"

                BEGIN TRANSACTION

                IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[Comment]') AND name = 'IsValidWebSite')
                BEGIN

                    ALTER TABLE Comment
                    ADD IsValidWebSite BIT NULL

                    ALTER TABLE [dbo].[Comment] ADD  CONSTRAINT [DF_Comment_IsValidWebSite]  DEFAULT ((0)) FOR [IsValidWebSite]

                END

                COMMIT";

            DBM.Default.ExecuteNonQuery(sql);
        }

    }
}
