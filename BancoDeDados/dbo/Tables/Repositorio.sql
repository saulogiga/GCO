CREATE TABLE [dbo].[Repositorio](
	[IdRepositorio] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [varchar](70) NOT NULL,
	[Caminho] [varchar](200) NOT NULL,
	[IdTipoRepositorio] [int] NOT NULL,
	[DataInclusao] [datetime] NOT NULL CONSTRAINT [DF_Repositorio_DataInclusao]  DEFAULT (getdate()),
	[DataAlteracao] [datetime] NULL,
	[DataExclusao] [datetime] NULL,
	[IsDeleted] [bit] NOT NULL CONSTRAINT [DF_Repositorio_IsDeleted]  DEFAULT ((0)),
 CONSTRAINT [PK_Repositorio] PRIMARY KEY CLUSTERED 
(
	[IdRepositorio] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Repositorio]  WITH CHECK ADD  CONSTRAINT [FK_Repositorio_TipoRepositorio] FOREIGN KEY([IdTipoRepositorio])
REFERENCES [dbo].[TipoRepositorio] ([IdTipoRepositorio])
GO

ALTER TABLE [dbo].[Repositorio] CHECK CONSTRAINT [FK_Repositorio_TipoRepositorio]
GO

