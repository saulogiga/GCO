CREATE TABLE [dbo].[UnidadeDesenvolvimentoBanco](
	[IdUnidadeDesenvolvimento] [int] NOT NULL,
	[IdServidorBanco] [int] NOT NULL,
	[IdTipoServidor] [int] NOT NULL,
	[NomeBanco] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdUnidadeDesenvolvimento] ASC,
	[IdServidorBanco] ASC,
	[IdTipoServidor] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[UnidadeDesenvolvimentoBanco]  WITH CHECK ADD  CONSTRAINT [FK_UnidadeDesenvolvimentoBanco__ServidorBanco] FOREIGN KEY([IdServidorBanco])
REFERENCES [dbo].[ServidorBanco] ([IdServidorBanco])
GO

ALTER TABLE [dbo].[UnidadeDesenvolvimentoBanco] CHECK CONSTRAINT [FK_UnidadeDesenvolvimentoBanco__ServidorBanco]
GO

ALTER TABLE [dbo].[UnidadeDesenvolvimentoBanco]  WITH CHECK ADD  CONSTRAINT [FK_UnidadeDesenvolvimentoBanco__TipoServidor] FOREIGN KEY([IdTipoServidor])
REFERENCES [dbo].[TipoServidor] ([IdTipoServidor])
GO

ALTER TABLE [dbo].[UnidadeDesenvolvimentoBanco] CHECK CONSTRAINT [FK_UnidadeDesenvolvimentoBanco__TipoServidor]
GO

ALTER TABLE [dbo].[UnidadeDesenvolvimentoBanco]  WITH CHECK ADD  CONSTRAINT [FK_UnidadeDesenvolvimentoBanco__UnidadeDesenvolvimento] FOREIGN KEY([IdUnidadeDesenvolvimento])
REFERENCES [dbo].[UnidadeDesenvolvimento] ([IdUnidadeDesenvolvimento])
GO

ALTER TABLE [dbo].[UnidadeDesenvolvimentoBanco] CHECK CONSTRAINT [FK_UnidadeDesenvolvimentoBanco__UnidadeDesenvolvimento]
GO
