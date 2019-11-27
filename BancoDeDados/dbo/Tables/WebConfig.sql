CREATE TABLE [dbo].[WebConfig](
	[IdWebConfig] [int] IDENTITY(1,1) NOT NULL,
	[Valor] [varchar](500) NOT NULL,
	[IdTipoWebConfig] [int] NOT NULL,
	[IdUnidadeDesenvolvimento] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	[idModulo] [int] NOT NULL,
	[Acao] [int] NULL,
	[DataInclusao] [datetime] NOT NULL,
	[DataAlteracao] [datetime] NULL,
	[DataExclusao] [datetime] NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK__WebConfi__487F20881AE4971C] PRIMARY KEY CLUSTERED 
(
	[IdWebConfig] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[WebConfig]  WITH CHECK ADD  CONSTRAINT [FK_WebConfig__TipoWebConfig] FOREIGN KEY([IdTipoWebConfig])
REFERENCES [dbo].[TipoWebConfig] ([IdTipoWebConfig])
GO

ALTER TABLE [dbo].[WebConfig] CHECK CONSTRAINT [FK_WebConfig__TipoWebConfig]
GO

ALTER TABLE [dbo].[WebConfig]  WITH CHECK ADD  CONSTRAINT [FK_WebConfig__UnidadeDesenvolvimento] FOREIGN KEY([IdUnidadeDesenvolvimento])
REFERENCES [dbo].[UnidadeDesenvolvimento] ([IdUnidadeDesenvolvimento])
GO

ALTER TABLE [dbo].[WebConfig] CHECK CONSTRAINT [FK_WebConfig__UnidadeDesenvolvimento]
GO

ALTER TABLE [dbo].[WebConfig]  WITH CHECK ADD  CONSTRAINT [FK_WebConfig_Modulo] FOREIGN KEY([idModulo])
REFERENCES [dbo].[Modulo] ([IdModulo])
GO

ALTER TABLE [dbo].[WebConfig] CHECK CONSTRAINT [FK_WebConfig_Modulo]
GO

ALTER TABLE [dbo].[WebConfig]  WITH CHECK ADD  CONSTRAINT [FK_WebConfig_UserProfile] FOREIGN KEY([UserId])
REFERENCES [dbo].[UserProfile] ([UserId])
GO

ALTER TABLE [dbo].[WebConfig] CHECK CONSTRAINT [FK_WebConfig_UserProfile]
GO


