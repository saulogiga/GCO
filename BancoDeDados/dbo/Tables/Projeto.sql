CREATE TABLE [dbo].[Projeto](
	[IdProjeto] [int] NOT NULL,
	[Nome] [varchar](50) NOT NULL,
	[IdCliente] [int] NOT NULL,
	[DataInclusao] [datetime] NOT NULL,
	[DataAlteracao] [datetime] NULL,
	[DataExclusao] [datetime] NULL,
	[IsDeleted] [bit] NOT NULL DEFAULT ((0)),
	[idProjetoPai] [int] NULL,
	[Ambiente] [varchar](50) NULL,
	[Versao] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[IdProjeto] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Projeto]  WITH CHECK ADD  CONSTRAINT [FK_Projeto__Cliente] FOREIGN KEY([IdCliente])
REFERENCES [dbo].[Cliente] ([IdCliente])
GO

ALTER TABLE [dbo].[Projeto] CHECK CONSTRAINT [FK_Projeto__Cliente]
GO

ALTER TABLE [dbo].[Projeto]  WITH NOCHECK ADD  CONSTRAINT [FK_Projeto_Projeto] FOREIGN KEY([idProjetoPai])
REFERENCES [dbo].[Projeto] ([IdProjeto])
NOT FOR REPLICATION 
GO

ALTER TABLE [dbo].[Projeto] NOCHECK CONSTRAINT [FK_Projeto_Projeto]
GO