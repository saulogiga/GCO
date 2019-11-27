CREATE TABLE [dbo].[ScriptBanco](
	[IdScript] [bigint] IDENTITY(1,1) NOT NULL,
	[Nome] [varchar](100) NOT NULL,
	[Comentario] [varchar](500) NULL,
	[Script] [ntext] NOT NULL,
	[Executado] [bit] NOT NULL DEFAULT ((0)),
	[IdUnidadeDesenvolvimento] [int] NOT NULL,
	[IdTipoScript] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	[Ordem] [int] NOT NULL,
	[DataInclusao] [datetime] NOT NULL,
	[DataAlteracao] [datetime] NULL,
	[DataExclusao] [datetime] NULL,
	[IsDeleted] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdScript] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

ALTER TABLE [dbo].[ScriptBanco]  WITH CHECK ADD  CONSTRAINT [FK_Script__TipoScriptBanco] FOREIGN KEY([IdTipoScript])
REFERENCES [dbo].[TipoScriptBanco] ([IdTipoScript])
GO

ALTER TABLE [dbo].[ScriptBanco] CHECK CONSTRAINT [FK_Script__TipoScriptBanco]
GO

ALTER TABLE [dbo].[ScriptBanco]  WITH CHECK ADD  CONSTRAINT [FK_Script__UnidadeDesenvolvimento] FOREIGN KEY([IdUnidadeDesenvolvimento])
REFERENCES [dbo].[UnidadeDesenvolvimento] ([IdUnidadeDesenvolvimento])
GO

ALTER TABLE [dbo].[ScriptBanco] CHECK CONSTRAINT [FK_Script__UnidadeDesenvolvimento]
GO

ALTER TABLE [dbo].[ScriptBanco]  WITH CHECK ADD  CONSTRAINT [FK_Script__UserProfile] FOREIGN KEY([UserId])
REFERENCES [dbo].[UserProfile] ([UserId])
GO

ALTER TABLE [dbo].[ScriptBanco] CHECK CONSTRAINT [FK_Script__UserProfile]
GO
