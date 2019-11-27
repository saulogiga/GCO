CREATE TABLE [dbo].[PublicacaoRelease](
	[IdPublicacaoRelease] [int] IDENTITY(1,1) NOT NULL,
	[IdUnidadeDesenvolvimento] [int] NOT NULL,
	[IdPublicacaoStatus] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	[DataInicioPublicacao] [datetime] NULL,
	[DataFimPublicacao] [datetime] NULL,
	[CaminhoOrigem] [varchar](500) NULL,
	[PastaOrigem] [varchar](500) NULL,
	[CaminhoDestino] [varchar](500) NULL,
	[ArqNaoSelecionados] [varchar](500) NULL,
	[DirNaoSelecionados] [varchar](500) NULL,
	[Release] [varchar](500) NULL,
	[Detalhes] [varchar](500) NULL,
	[DataInclusao] [datetime] NOT NULL,
	[DataAlteracao] [datetime] NULL,
	[DataExclusao] [datetime] NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_PublicacaoRelease] PRIMARY KEY CLUSTERED 
(
	[IdPublicacaoRelease] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[PublicacaoRelease]  WITH CHECK ADD  CONSTRAINT [FK_PublicacaoRelease_PublicacaoStatus] FOREIGN KEY([IdPublicacaoStatus])
REFERENCES [dbo].[PublicacaoStatus] ([IdPublicacaoStatus])
GO

ALTER TABLE [dbo].[PublicacaoRelease] CHECK CONSTRAINT [FK_PublicacaoRelease_PublicacaoStatus]
GO

ALTER TABLE [dbo].[PublicacaoRelease]  WITH NOCHECK ADD  CONSTRAINT [FK_PublicacaoRelease_UnidadeDesenvolvimento] FOREIGN KEY([IdUnidadeDesenvolvimento])
REFERENCES [dbo].[UnidadeDesenvolvimento] ([IdUnidadeDesenvolvimento])
NOT FOR REPLICATION 
GO

ALTER TABLE [dbo].[PublicacaoRelease] CHECK CONSTRAINT [FK_PublicacaoRelease_UnidadeDesenvolvimento]
GO

ALTER TABLE [dbo].[PublicacaoRelease]  WITH NOCHECK ADD  CONSTRAINT [FK_PublicacaoRelease_UserProfile] FOREIGN KEY([UserId])
REFERENCES [dbo].[UserProfile] ([UserId])
NOT FOR REPLICATION 
GO

ALTER TABLE [dbo].[PublicacaoRelease] CHECK CONSTRAINT [FK_PublicacaoRelease_UserProfile]
GO