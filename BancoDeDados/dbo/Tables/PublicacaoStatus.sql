CREATE TABLE [dbo].[PublicacaoStatus](
	[IdPublicacaoStatus] [int] IDENTITY(1,1) NOT NULL,
	[Descricao] [varchar](500) NULL,
	[UserId] [int] NOT NULL,
	[DataInclusao] [datetime] NOT NULL,
	[DataAlteracao] [datetime] NULL,
	[DataExclusao] [datetime] NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_PublicacaoStatus] PRIMARY KEY CLUSTERED 
(
	[IdPublicacaoStatus] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO