/****** Object:  Database [AgenciaAuto]    Script Date: 21/04/2021 07:21:27 ******/
DROP DATABASE IF EXISTS [AgenciaAuto]
Go
CREATE DATABASE [AgenciaAuto]
Go

USE [AgenciaAuto]
GO
/****** Object:  Table [dbo].[tb_Veiculos]    Script Date: 21/04/2021 07:10:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_Vagas] (
    [Id]          INT             IDENTITY (1, 1) NOT NULL,
    [Nome]        VARCHAR (50)    NOT NULL,
    [Descricao]   VARCHAR (MAX)   NULL,
    [Criador]     VARCHAR (50)    NOT NULL,
    [Link]        VARCHAR (MAX)   NULL,
    [Moeda]       INT             NOT NULL,
    [Pais]        VARCHAR (50)    NULL,
    [Valor]       DECIMAL (18, 2) NOT NULL,
    [Ativo]       BIT             NOT NULL,
    [Regiao]      VARCHAR (50)    DEFAULT ((0)) NOT NULL,
    [NomeCriador] VARCHAR (50)    NULL,
    [Previa]      VARCHAR (100)   NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);
CREATE TABLE [dbo].[Usuarios] (
    [Id]    INT          IDENTITY (1, 1) NOT NULL,
    [Email] VARCHAR (50) NULL,
    [Nome]  VARCHAR (50) NULL,
    [Senha] VARCHAR (50) NULL,
    [Tipo]  INT          DEFAULT ((1)) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
USE [master]
GO
ALTER DATABASE [AgenciaAuto] SET  READ_WRITE 
GO