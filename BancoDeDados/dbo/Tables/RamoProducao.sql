CREATE TABLE [dbo].[RamoProducao](
	[IdRamoProducao] [int] NOT NULL,
	[Caminho] [varchar](200) NULL,
	[Versao] [varchar](75) NOT NULL,
	[Solicitacao] [varchar](50) NULL,
	[Comentario] [varchar](500) NULL,
	[DataPublicacao] [datetime] NOT NULL,
	[IdRepositorio] [int] NOT NULL,
	[IdProjeto] [int] NOT NULL,
	[IdModulo] [int] NOT NULL,
	[DataInclusao] [datetime] NOT NULL,
	[DataAlteracao] [datetime] NULL,
	[DataExclusao] [datetime] NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_RamoProducao] PRIMARY KEY CLUSTERED 
(
	[IdRamoProducao] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[RamoProducao]  WITH CHECK ADD  CONSTRAINT [FK_RamoProducao_Modulo] FOREIGN KEY([IdModulo])
REFERENCES [dbo].[Modulo] ([IdModulo])
GO

ALTER TABLE [dbo].[RamoProducao] CHECK CONSTRAINT [FK_RamoProducao_Modulo]
GO

ALTER TABLE [dbo].[RamoProducao]  WITH CHECK ADD  CONSTRAINT [FK_RamoProducao_Projeto] FOREIGN KEY([IdProjeto])
REFERENCES [dbo].[Projeto] ([IdProjeto])
GO

ALTER TABLE [dbo].[RamoProducao] CHECK CONSTRAINT [FK_RamoProducao_Projeto]
GO


