USE [master]
GO
/****** Object:  Database [Ludothek]    Script Date: 27.05.2018 20:56:25 ******/
CREATE DATABASE [Ludothek]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Ludothek', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\Ludothek.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Ludothek_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\Ludothek_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [Ludothek] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Ludothek].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Ludothek] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Ludothek] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Ludothek] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Ludothek] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Ludothek] SET ARITHABORT OFF 
GO
ALTER DATABASE [Ludothek] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Ludothek] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Ludothek] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Ludothek] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Ludothek] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Ludothek] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Ludothek] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Ludothek] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Ludothek] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Ludothek] SET  ENABLE_BROKER 
GO
ALTER DATABASE [Ludothek] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Ludothek] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Ludothek] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Ludothek] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Ludothek] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Ludothek] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Ludothek] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Ludothek] SET RECOVERY FULL 
GO
ALTER DATABASE [Ludothek] SET  MULTI_USER 
GO
ALTER DATABASE [Ludothek] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Ludothek] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Ludothek] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Ludothek] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Ludothek] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Ludothek] SET QUERY_STORE = OFF
GO
USE [Ludothek]
GO
ALTER DATABASE SCOPED CONFIGURATION SET IDENTITY_CACHE = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET LEGACY_CARDINALITY_ESTIMATION = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET PARAMETER_SNIFFING = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET QUERY_OPTIMIZER_HOTFIXES = PRIMARY;
GO
USE [Ludothek]
GO
/****** Object:  DatabaseRole [Verband]    Script Date: 27.05.2018 20:56:27 ******/
CREATE ROLE [Verband]
GO
/****** Object:  DatabaseRole [Mitarbeiter]    Script Date: 27.05.2018 20:56:27 ******/
CREATE ROLE [Mitarbeiter]
GO
/****** Object:  DatabaseRole [Kunde]    Script Date: 27.05.2018 20:56:27 ******/
CREATE ROLE [Kunde]
GO
/****** Object:  DatabaseRole [Filialleiter]    Script Date: 27.05.2018 20:56:27 ******/
CREATE ROLE [Filialleiter]
GO
/****** Object:  DatabaseRole [Administrator]    Script Date: 27.05.2018 20:56:27 ******/
CREATE ROLE [Administrator]
GO
/****** Object:  Table [dbo].[Ausleihe]    Script Date: 27.05.2018 20:56:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Ausleihe](
	[ID_Ausleihe] [int] NOT NULL,
	[FK_Person] [int] NOT NULL,
	[FK_Spiel] [int] NOT NULL,
	[VonDatum] [date] NOT NULL,
	[BisDatum] [date] NOT NULL,
	[Bezahlt] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID_Ausleihe] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Benutzer]    Script Date: 27.05.2018 20:56:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Benutzer](
	[FK_Person] [int] NOT NULL,
	[password] [varchar](50) NOT NULL,
	[mail] [varchar](50) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Bestellung]    Script Date: 27.05.2018 20:56:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Bestellung](
	[ID_Bestellung] [int] NOT NULL,
	[FK_Verlag] [int] NOT NULL,
	[FK_Spiel] [int] NOT NULL,
	[Datum] [date] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID_Bestellung] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Filialleiter]    Script Date: 27.05.2018 20:56:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Filialleiter](
	[ID_Filialleiter] [int] NOT NULL,
	[FK_Mitarbeiter] [int] NOT NULL,
	[FK_Stellvertretung] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID_Filialleiter] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Kategorie]    Script Date: 27.05.2018 20:56:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Kategorie](
	[ID_Kategorie] [int] NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Altersfreigabe] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID_Kategorie] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Ludothek]    Script Date: 27.05.2018 20:56:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Ludothek](
	[ID_Ludothek] [int] NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Strasse] [varchar](50) NOT NULL,
	[PLZ] [int] NOT NULL,
	[Ort] [varchar](50) NOT NULL,
	[FK_Verband] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID_Ludothek] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Mitarbeiter]    Script Date: 27.05.2018 20:56:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Mitarbeiter](
	[FK_Person] [int] NOT NULL,
	[FK_Ludothek] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[FK_Person] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Mitgliedschaft]    Script Date: 27.05.2018 20:56:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Mitgliedschaft](
	[ID_Mitgliedschaft] [int] NOT NULL,
	[Status] [varchar](50) NOT NULL,
	[Rechnungsstatus] [varchar](50) NOT NULL,
	[Erstellungsdatum] [date] NOT NULL,
	[Auslaufdatum] [date] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID_Mitgliedschaft] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Person]    Script Date: 27.05.2018 20:56:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Person](
	[ID_Person] [int] NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Geschlecht] [varchar](10) NOT NULL,
	[Geburtsdatum] [date] NOT NULL,
	[Einstiegsdatum] [date] NOT NULL,
	[FK_Mitgliedschaft] [int] NULL,
	[Strasse] [varchar](50) NULL,
	[Postleitzahl] [int] NULL,
	[Ort] [varchar](50) NULL,
	[Land] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID_Person] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Session]    Script Date: 27.05.2018 20:56:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Session](
	[sessionID] [varchar](50) NOT NULL,
	[FK_Person] [int] NOT NULL,
	[activeSession] [bit] NOT NULL,
	[lastActivity] [datetime] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Spiel]    Script Date: 27.05.2018 20:56:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Spiel](
	[ID_Spiel] [int] NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Verlag] [varchar](50) NOT NULL,
	[Lagerbestand] [int] NOT NULL,
	[FK_Tarifkategorie] [int] NOT NULL,
	[FK_Kategorie] [int] NOT NULL,
	[Image_Link] [text] NULL,
	[Description] [text] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID_Spiel] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TarifKategorie]    Script Date: 27.05.2018 20:56:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TarifKategorie](
	[ID_TarifKategorie] [int] NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Normalpreis] [float] NOT NULL,
	[Mitgliedschaftsauflage] [float] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID_TarifKategorie] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Verband]    Script Date: 27.05.2018 20:56:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Verband](
	[ID_Verband] [int] NOT NULL,
	[Name] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID_Verband] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Verbandsvorstand]    Script Date: 27.05.2018 20:56:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Verbandsvorstand](
	[ID_Verbandsvorstand] [int] NOT NULL,
	[FK_Verband] [int] NOT NULL,
	[FK_Vorsitzender] [int] NOT NULL,
	[Name] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID_Verbandsvorstand] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Verlag]    Script Date: 27.05.2018 20:56:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Verlag](
	[ID_Verlag] [int] NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Telefon] [varchar](50) NOT NULL,
	[Strasse] [varchar](50) NOT NULL,
	[PLZ] [int] NOT NULL,
	[Ort] [varchar](50) NOT NULL,
	[Land] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID_Verlag] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Verlängerung]    Script Date: 27.05.2018 20:56:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Verlängerung](
	[ID_Verlängerung] [int] NOT NULL,
	[FK_Ausleihe] [int] NOT NULL,
	[BisDatum] [date] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID_Verlängerung] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Vorstandsmitglied]    Script Date: 27.05.2018 20:56:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Vorstandsmitglied](
	[FK_Verbandsvorstand] [int] NOT NULL,
	[FK_Filialleiter] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Index [ClusteredIndex-Ausleihe]    Script Date: 27.05.2018 20:56:36 ******/
CREATE NONCLUSTERED INDEX [ClusteredIndex-Ausleihe] ON [dbo].[Ausleihe]
(
	[ID_Ausleihe] ASC,
	[FK_Person] ASC,
	[FK_Spiel] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [NonClusteredIndex-Ausleihe]    Script Date: 27.05.2018 20:56:36 ******/
CREATE NONCLUSTERED INDEX [NonClusteredIndex-Ausleihe] ON [dbo].[Ausleihe]
(
	[VonDatum] ASC,
	[BisDatum] ASC,
	[FK_Spiel] ASC,
	[ID_Ausleihe] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [NonClusteredIndex-Bestellung]    Script Date: 27.05.2018 20:56:36 ******/
CREATE NONCLUSTERED INDEX [NonClusteredIndex-Bestellung] ON [dbo].[Bestellung]
(
	[FK_Verlag] ASC,
	[FK_Spiel] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [ClusteredIndex-Ludothek]    Script Date: 27.05.2018 20:56:36 ******/
CREATE NONCLUSTERED INDEX [ClusteredIndex-Ludothek] ON [dbo].[Ludothek]
(
	[ID_Ludothek] ASC,
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [ClusteredIndex-Mitarbeiter]    Script Date: 27.05.2018 20:56:36 ******/
CREATE NONCLUSTERED INDEX [ClusteredIndex-Mitarbeiter] ON [dbo].[Mitarbeiter]
(
	[FK_Person] ASC,
	[FK_Ludothek] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [ClusteredIndex-Mitgliedschaft]    Script Date: 27.05.2018 20:56:36 ******/
CREATE NONCLUSTERED INDEX [ClusteredIndex-Mitgliedschaft] ON [dbo].[Mitgliedschaft]
(
	[Erstellungsdatum] ASC,
	[Auslaufdatum] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [ClusteredIndex-Person]    Script Date: 27.05.2018 20:56:36 ******/
CREATE NONCLUSTERED INDEX [ClusteredIndex-Person] ON [dbo].[Person]
(
	[ID_Person] ASC,
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [ClusteredIndex-Spiel]    Script Date: 27.05.2018 20:56:36 ******/
CREATE NONCLUSTERED INDEX [ClusteredIndex-Spiel] ON [dbo].[Spiel]
(
	[ID_Spiel] ASC,
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [ClusteredIndex-Verlag]    Script Date: 27.05.2018 20:56:36 ******/
CREATE NONCLUSTERED INDEX [ClusteredIndex-Verlag] ON [dbo].[Verlag]
(
	[ID_Verlag] ASC,
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [ClusteredIndex-Verlaengerung]    Script Date: 27.05.2018 20:56:36 ******/
CREATE NONCLUSTERED INDEX [ClusteredIndex-Verlaengerung] ON [dbo].[Verlängerung]
(
	[FK_Ausleihe] ASC,
	[BisDatum] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Ausleihe]  WITH CHECK ADD  CONSTRAINT [FK_Ausleihe_Person] FOREIGN KEY([FK_Person])
REFERENCES [dbo].[Person] ([ID_Person])
GO
ALTER TABLE [dbo].[Ausleihe] CHECK CONSTRAINT [FK_Ausleihe_Person]
GO
ALTER TABLE [dbo].[Ausleihe]  WITH CHECK ADD  CONSTRAINT [FK_Ausleihe_Spiel] FOREIGN KEY([FK_Spiel])
REFERENCES [dbo].[Spiel] ([ID_Spiel])
GO
ALTER TABLE [dbo].[Ausleihe] CHECK CONSTRAINT [FK_Ausleihe_Spiel]
GO
ALTER TABLE [dbo].[Benutzer]  WITH CHECK ADD  CONSTRAINT [FK_User_Person] FOREIGN KEY([FK_Person])
REFERENCES [dbo].[Person] ([ID_Person])
GO
ALTER TABLE [dbo].[Benutzer] CHECK CONSTRAINT [FK_User_Person]
GO
ALTER TABLE [dbo].[Bestellung]  WITH CHECK ADD  CONSTRAINT [FK_Bestellung_Spiel] FOREIGN KEY([FK_Spiel])
REFERENCES [dbo].[Spiel] ([ID_Spiel])
GO
ALTER TABLE [dbo].[Bestellung] CHECK CONSTRAINT [FK_Bestellung_Spiel]
GO
ALTER TABLE [dbo].[Bestellung]  WITH CHECK ADD  CONSTRAINT [FK_Bestellung_Verlag] FOREIGN KEY([FK_Verlag])
REFERENCES [dbo].[Verlag] ([ID_Verlag])
GO
ALTER TABLE [dbo].[Bestellung] CHECK CONSTRAINT [FK_Bestellung_Verlag]
GO
ALTER TABLE [dbo].[Filialleiter]  WITH CHECK ADD  CONSTRAINT [FK_Filialleiter_Mitarbeiter] FOREIGN KEY([FK_Stellvertretung])
REFERENCES [dbo].[Mitarbeiter] ([FK_Person])
GO
ALTER TABLE [dbo].[Filialleiter] CHECK CONSTRAINT [FK_Filialleiter_Mitarbeiter]
GO
ALTER TABLE [dbo].[Ludothek]  WITH CHECK ADD  CONSTRAINT [FK_Ludothek_Verband] FOREIGN KEY([FK_Verband])
REFERENCES [dbo].[Verband] ([ID_Verband])
GO
ALTER TABLE [dbo].[Ludothek] CHECK CONSTRAINT [FK_Ludothek_Verband]
GO
ALTER TABLE [dbo].[Mitarbeiter]  WITH CHECK ADD  CONSTRAINT [FK_Mitarbeiter_Ludothek] FOREIGN KEY([FK_Ludothek])
REFERENCES [dbo].[Ludothek] ([ID_Ludothek])
GO
ALTER TABLE [dbo].[Mitarbeiter] CHECK CONSTRAINT [FK_Mitarbeiter_Ludothek]
GO
ALTER TABLE [dbo].[Mitarbeiter]  WITH CHECK ADD  CONSTRAINT [FK_Mitarbeiter_Person] FOREIGN KEY([FK_Person])
REFERENCES [dbo].[Person] ([ID_Person])
GO
ALTER TABLE [dbo].[Mitarbeiter] CHECK CONSTRAINT [FK_Mitarbeiter_Person]
GO
ALTER TABLE [dbo].[Person]  WITH CHECK ADD  CONSTRAINT [FK_Person_Mitgliedschaft] FOREIGN KEY([FK_Mitgliedschaft])
REFERENCES [dbo].[Mitgliedschaft] ([ID_Mitgliedschaft])
GO
ALTER TABLE [dbo].[Person] CHECK CONSTRAINT [FK_Person_Mitgliedschaft]
GO
ALTER TABLE [dbo].[Session]  WITH CHECK ADD  CONSTRAINT [FK_Session_Person] FOREIGN KEY([FK_Person])
REFERENCES [dbo].[Person] ([ID_Person])
GO
ALTER TABLE [dbo].[Session] CHECK CONSTRAINT [FK_Session_Person]
GO
ALTER TABLE [dbo].[Spiel]  WITH CHECK ADD  CONSTRAINT [FK_Spiel_Kategorie] FOREIGN KEY([FK_Kategorie])
REFERENCES [dbo].[Kategorie] ([ID_Kategorie])
GO
ALTER TABLE [dbo].[Spiel] CHECK CONSTRAINT [FK_Spiel_Kategorie]
GO
ALTER TABLE [dbo].[Spiel]  WITH CHECK ADD  CONSTRAINT [FK_Spiel_TarifKategorie] FOREIGN KEY([FK_Tarifkategorie])
REFERENCES [dbo].[TarifKategorie] ([ID_TarifKategorie])
GO
ALTER TABLE [dbo].[Spiel] CHECK CONSTRAINT [FK_Spiel_TarifKategorie]
GO
ALTER TABLE [dbo].[Verbandsvorstand]  WITH CHECK ADD  CONSTRAINT [FK_Verbandsvorstand_Filialleiter] FOREIGN KEY([FK_Vorsitzender])
REFERENCES [dbo].[Filialleiter] ([ID_Filialleiter])
GO
ALTER TABLE [dbo].[Verbandsvorstand] CHECK CONSTRAINT [FK_Verbandsvorstand_Filialleiter]
GO
ALTER TABLE [dbo].[Verbandsvorstand]  WITH CHECK ADD  CONSTRAINT [FK_Verbandsvorstand_Verband] FOREIGN KEY([FK_Verband])
REFERENCES [dbo].[Verband] ([ID_Verband])
GO
ALTER TABLE [dbo].[Verbandsvorstand] CHECK CONSTRAINT [FK_Verbandsvorstand_Verband]
GO
ALTER TABLE [dbo].[Verlängerung]  WITH CHECK ADD  CONSTRAINT [FK_Verlängerung_Ausleihe] FOREIGN KEY([FK_Ausleihe])
REFERENCES [dbo].[Ausleihe] ([ID_Ausleihe])
GO
ALTER TABLE [dbo].[Verlängerung] CHECK CONSTRAINT [FK_Verlängerung_Ausleihe]
GO
ALTER TABLE [dbo].[Vorstandsmitglied]  WITH CHECK ADD  CONSTRAINT [FK_Vorstandsmitglied_Filialleiter] FOREIGN KEY([FK_Filialleiter])
REFERENCES [dbo].[Filialleiter] ([ID_Filialleiter])
GO
ALTER TABLE [dbo].[Vorstandsmitglied] CHECK CONSTRAINT [FK_Vorstandsmitglied_Filialleiter]
GO
ALTER TABLE [dbo].[Vorstandsmitglied]  WITH CHECK ADD  CONSTRAINT [FK_Vorstandsmitglied_Verbandsvorstand] FOREIGN KEY([FK_Verbandsvorstand])
REFERENCES [dbo].[Verbandsvorstand] ([ID_Verbandsvorstand])
GO
ALTER TABLE [dbo].[Vorstandsmitglied] CHECK CONSTRAINT [FK_Vorstandsmitglied_Verbandsvorstand]
GO
ALTER TABLE [dbo].[Person]  WITH CHECK ADD CHECK  (([Geschlecht]='Weiblich' OR [Geschlecht]='Männlich'))
GO
CREATE PROC Login_Proc
@mail varchar(50),
@password varchar(50)
AS
BEGIN
SELECT * FROM Benutzer WHERE mail=@mail AND password=@password
END
GO
CREATE PROC Update_Session
@lastActivity datetime,
@activeSession bit,
@sessionID varchar(50)
AS
BEGIN
UPDATE Session SET lastActivity=@lastActivity, activeSession=@activeSession WHERE sessionID=@SessionID
END
GO
CREATE PROC Insert_Session
@sessionID varchar(50),
@FkPerson int,
@activeSession bit,
@lastActivity datetime
AS
BEGIN
INSERT INTO Session (SessionID, FK_Person, activeSession, lastActivity) VALUES (@sessionID, @FkPerson, @activeSession, @lastActivity)
END
GO
CREATE PROC Select_Session
@sessionID varchar(50)
AS BEGIN
SELECT* FROM Session WHERE sessionID=@sessionID
END
GO
USE [master]
GO
ALTER DATABASE [Ludothek] SET  READ_WRITE 
