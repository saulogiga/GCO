CREATE TABLE [dbo].[RamoDesenvolvimento](
	[IdRamoDesenvolvimento] [int] IDENTITY(1,1) NOT NULL,
	[Caminho] [varchar](200) NULL,
	[Versao] [varchar](75) NULL,
	[Comentario] [varchar](500) NULL,
	[IdRamoProducao] [int] NULL,
	[IdRepositorio] [int] NOT NULL,
	[IdUnidadeDesenvolvimento] [int] NOT NULL,
	[IdModulo] [int] NOT NULL,
	[Branch] [bit] NOT NULL,
	[Fechado] [bit] NULL,
	[DataInclusao] [datetime] NOT NULL,
	[DataAlteracao] [datetime] NULL,
	[DataExclusao] [datetime] NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_RamoDesenvolvimento] PRIMARY KEY CLUSTERED 
(
	[IdRamoDesenvolvimento] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[RamoDesenvolvimento]  WITH CHECK ADD  CONSTRAINT [FK_RamoDesenvolvimento_Modulo] FOREIGN KEY([IdModulo])
REFERENCES [dbo].[Modulo] ([IdModulo])
GO

ALTER TABLE [dbo].[RamoDesenvolvimento] CHECK CONSTRAINT [FK_RamoDesenvolvimento_Modulo]
GO

ALTER TABLE [dbo].[RamoDesenvolvimento]  WITH CHECK ADD  CONSTRAINT [FK_RamoDesenvolvimento_RamoProducao] FOREIGN KEY([IdRamoProducao])
REFERENCES [dbo].[RamoProducao] ([IdRamoProducao])
GO

ALTER TABLE [dbo].[RamoDesenvolvimento] CHECK CONSTRAINT [FK_RamoDesenvolvimento_RamoProducao]
GO

ALTER TABLE [dbo].[RamoDesenvolvimento]  WITH CHECK ADD  CONSTRAINT [FK_RamoDesenvolvimento_Repositorio] FOREIGN KEY([IdRepositorio])
REFERENCES [dbo].[Repositorio] ([IdRepositorio])
GO

ALTER TABLE [dbo].[RamoDesenvolvimento] CHECK CONSTRAINT [FK_RamoDesenvolvimento_Repositorio]
GO

ALTER TABLE [dbo].[RamoDesenvolvimento]  WITH CHECK ADD  CONSTRAINT [FK_RamoDesenvolvimento_UnidadeDesenvolvimento] FOREIGN KEY([IdUnidadeDesenvolvimento])
REFERENCES [dbo].[UnidadeDesenvolvimento] ([IdUnidadeDesenvolvimento])
GO

ALTER TABLE [dbo].[RamoDesenvolvimento] CHECK CONSTRAINT [FK_RamoDesenvolvimento_UnidadeDesenvolvimento]
GO


