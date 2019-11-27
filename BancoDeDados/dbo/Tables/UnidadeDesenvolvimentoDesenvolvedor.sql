CREATE TABLE [dbo].[UnidadeDesenvolvimentoDesenvolvedor](
	[IdUnidadeDesenvolvimento] [int] NOT NULL,
	[UserId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdUnidadeDesenvolvimento] ASC,
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[UnidadeDesenvolvimentoDesenvolvedor]  WITH CHECK ADD  CONSTRAINT [FK_UnidadeDesenvolvimentoDesenvolvedor__UnidadeDesenvolvimento] FOREIGN KEY([IdUnidadeDesenvolvimento])
REFERENCES [dbo].[UnidadeDesenvolvimento] ([IdUnidadeDesenvolvimento])
GO

ALTER TABLE [dbo].[UnidadeDesenvolvimentoDesenvolvedor] CHECK CONSTRAINT [FK_UnidadeDesenvolvimentoDesenvolvedor__UnidadeDesenvolvimento]
GO

ALTER TABLE [dbo].[UnidadeDesenvolvimentoDesenvolvedor]  WITH CHECK ADD  CONSTRAINT [FK_UnidadeDesenvolvimentoDesenvolvedor__UserProfile] FOREIGN KEY([UserId])
REFERENCES [dbo].[UserProfile] ([UserId])
GO

ALTER TABLE [dbo].[UnidadeDesenvolvimentoDesenvolvedor] CHECK CONSTRAINT [FK_UnidadeDesenvolvimentoDesenvolvedor__UserProfile]
GO