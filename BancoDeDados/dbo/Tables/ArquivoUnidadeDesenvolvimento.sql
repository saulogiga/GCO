CREATE TABLE [dbo].[ArquivoUnidadeDesenvolvimento](
	[IdArquivoUnidadeDesenvolvimento] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [varchar](100) NOT NULL,
	[Caminho] [varchar](500) NOT NULL,
	[IdTipoArquivo] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	[IdUnidadeDesenvolvimento] [int] NOT NULL,
	[DataInclusao] [datetime] NOT NULL,
	[DataAlteracao] [datetime] NULL,
	[DataExclusao] [datetime] NULL,
	[IsDeleted] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdArquivoUnidadeDesenvolvimento] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[ArquivoUnidadeDesenvolvimento]  WITH CHECK ADD  CONSTRAINT [FK_ArquivoUnidadeDesenvolvimento__TipoArquivo] FOREIGN KEY([IdTipoArquivo])
REFERENCES [dbo].[TipoArquivo] ([IdTipoArquivo])
GO

ALTER TABLE [dbo].[ArquivoUnidadeDesenvolvimento] CHECK CONSTRAINT [FK_ArquivoUnidadeDesenvolvimento__TipoArquivo]
GO

ALTER TABLE [dbo].[ArquivoUnidadeDesenvolvimento]  WITH CHECK ADD  CONSTRAINT [FK_ArquivoUnidadeDesenvolvimento__UnidadeDesenvolvimento] FOREIGN KEY([IdUnidadeDesenvolvimento])
REFERENCES [dbo].[UnidadeDesenvolvimento] ([IdUnidadeDesenvolvimento])
GO

ALTER TABLE [dbo].[ArquivoUnidadeDesenvolvimento] CHECK CONSTRAINT [FK_ArquivoUnidadeDesenvolvimento__UnidadeDesenvolvimento]
GO