CREATE TABLE [dbo].[Modulo](
	[IdModulo] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [varchar](50) NOT NULL,
	[DataInclusao] [datetime] NOT NULL CONSTRAINT [DF_Modulo_DataInclusao]  DEFAULT (getdate()),
	[DataAlteracao] [datetime] NULL,
	[DataExclusao] [datetime] NULL,
	[IsDeleted] [bit] NOT NULL CONSTRAINT [DF_Modulo_IsDeleted]  DEFAULT ((0)),
 CONSTRAINT [PK__Modulo__D9F1531580AF0ED0] PRIMARY KEY CLUSTERED 
(
	[IdModulo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO