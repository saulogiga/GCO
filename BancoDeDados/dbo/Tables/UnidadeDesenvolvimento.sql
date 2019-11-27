CREATE TABLE [dbo].[UnidadeDesenvolvimento](
	[IdUnidadeDesenvolvimento] [int] IDENTITY(1,1) NOT NULL,
	[NumeroTicket] [varchar](50) NOT NULL,
	[NumeroSolicitacao] [varchar](50) NULL,
	[DescricaoModulos] [nvarchar](100) NULL,
	[ExecutantoScript] [bit] NULL,
	[DataPublicacao] [datetime] NULL,
	[IdTipoUnidadeDesenvolvimento] [int] NOT NULL,
	[IdProjeto] [int] NOT NULL,
	[IdSolicitante] [int] NOT NULL,
	[IdStatusUnidadeDesenvolvimento] [int] NOT NULL,
	[DataInclusao] [datetime] NOT NULL,
	[DataAlteracao] [datetime] NULL,
	[DataExclusao] [datetime] NULL,
	[IsDeleted] [bit] NOT NULL,
	[CaminhoBuild] [varchar](500) NULL,
	[NomeBuild] [varchar](15) NULL,
	[CaminhoPublish] [varchar](500) NULL,
	[TeamProject] [varchar](50) NULL,
 CONSTRAINT [PK__UnidadeDesenvolv__20C1E124] PRIMARY KEY CLUSTERED 
(
	[IdUnidadeDesenvolvimento] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[UnidadeDesenvolvimento]  WITH CHECK ADD  CONSTRAINT [FK_UnidadeDesenvolvimento__Projeto] FOREIGN KEY([IdProjeto])
REFERENCES [dbo].[Projeto] ([IdProjeto])
GO

ALTER TABLE [dbo].[UnidadeDesenvolvimento] CHECK CONSTRAINT [FK_UnidadeDesenvolvimento__Projeto]
GO

ALTER TABLE [dbo].[UnidadeDesenvolvimento]  WITH CHECK ADD  CONSTRAINT [FK_UnidadeDesenvolvimento__StatusUnidadeDesenvolvimento] FOREIGN KEY([IdStatusUnidadeDesenvolvimento])
REFERENCES [dbo].[StatusUnidadeDesenvolvimento] ([IdStatusUnidadeDesenvolvimento])
GO

ALTER TABLE [dbo].[UnidadeDesenvolvimento] CHECK CONSTRAINT [FK_UnidadeDesenvolvimento__StatusUnidadeDesenvolvimento]
GO

ALTER TABLE [dbo].[UnidadeDesenvolvimento]  WITH CHECK ADD  CONSTRAINT [FK_UnidadeDesenvolvimento__TipoUnidadeDesenvolvimento] FOREIGN KEY([IdTipoUnidadeDesenvolvimento])
REFERENCES [dbo].[TipoUnidadeDesenvolvimento] ([IdTipoUnidadeDesenvolvimento])
GO

ALTER TABLE [dbo].[UnidadeDesenvolvimento] CHECK CONSTRAINT [FK_UnidadeDesenvolvimento__TipoUnidadeDesenvolvimento]
GO

ALTER TABLE [dbo].[UnidadeDesenvolvimento]  WITH CHECK ADD  CONSTRAINT [FK_UnidadeDesenvolvimento__UserProfile] FOREIGN KEY([IdSolicitante])
REFERENCES [dbo].[UserProfile] ([UserId])
GO

ALTER TABLE [dbo].[UnidadeDesenvolvimento] CHECK CONSTRAINT [FK_UnidadeDesenvolvimento__UserProfile]
GO