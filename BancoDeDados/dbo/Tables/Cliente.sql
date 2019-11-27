CREATE TABLE [dbo].[Cliente](
	[IdCliente] [int] NOT NULL,
	[Nome] [varchar](50) NOT NULL,
	[TeamProject] [varchar](100) NULL,
	[DataInclusao] [datetime] NOT NULL,
	[DataAlteracao] [datetime] NULL,
	[DataExclusao] [datetime] NULL,
	[IsDeleted] [bit] NOT NULL CONSTRAINT [DF__Cliente__IsDelet__47DBAE45]  DEFAULT ((0)),
 CONSTRAINT [PK__Cliente__D594664208F1B9F3] PRIMARY KEY CLUSTERED 
(
	[IdCliente] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

