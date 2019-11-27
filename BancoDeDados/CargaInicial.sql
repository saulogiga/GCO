/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/


SET IDENTITY_INSERT [dbo].[UserProfile] ON 
INSERT [dbo].[UserProfile] ([UserId], [UserName]) VALUES (1, N'saulop')
SET IDENTITY_INSERT [dbo].[UserProfile] OFF

SET IDENTITY_INSERT [dbo].[webpages_Roles] ON 
INSERT [dbo].[webpages_Roles] ([RoleId], [RoleName]) VALUES (1, N'GCO')
INSERT [dbo].[webpages_Roles] ([RoleId], [RoleName]) VALUES (2, N'Desenvolvedor')
INSERT [dbo].[webpages_Roles] ([RoleId], [RoleName]) VALUES (3, N'AnalistaDeProjeto')
INSERT [dbo].[webpages_Roles] ([RoleId], [RoleName]) VALUES (4, N'AnalistaDeSistema')
SET IDENTITY_INSERT [dbo].[webpages_Roles] OFF

INSERT [dbo].[webpages_Membership] ([UserId], [CreateDate], [ConfirmationToken], [IsConfirmed], [LastPasswordFailureDate], [PasswordFailuresSinceLastSuccess], [Password], [PasswordChangedDate], [PasswordSalt], [PasswordVerificationToken], [PasswordVerificationTokenExpirationDate]) VALUES (1, CAST(0x0000A18D014297F9 AS DateTime), NULL, 1, CAST(0x0000A1A200289B56 AS DateTime), 0, N'AMgE86XFYauJTeTwQKNiZiK0umHVeA3d1LpqQBgqn2daDusU6Ia62MT1Gz23Hvx1Iw==', CAST(0x0000A18D014297F9 AS DateTime), N'', NULL, NULL)

INSERT [dbo].[webpages_UsersInRoles] ([UserId], [RoleId]) VALUES (1, 1)

--#### Colocar aqui o insert dos projetos caso seja necessário
--INSERT [dbo].[Cliente] ([IdCliente], [Nome], [DataInclusao], [DataAlteracao], [DataExclusao], [IsDeleted]) VALUES (1, N'Cliente', CAST(0x0000A19A00000000 AS DateTime), NULL, NULL, 0)
--INSERT [dbo].[Projeto] ([IdProjeto], [Nome], [IdCliente], [DataInclusao], [DataAlteracao], [DataExclusao], [IsDeleted]) VALUES (1, N'BIP', 1, CAST(0x0000A19A00B058AB AS DateTime), NULL, NULL, 0)
--INSERT [dbo].[Projeto] ([IdProjeto], [Nome], [IdCliente], [DataInclusao], [DataAlteracao], [DataExclusao], [IsDeleted]) VALUES (2, N'EFox', 1, CAST(0x0000A19A00B058AB AS DateTime), NULL, NULL, 0)

--#### Colocar aqui o insert dos servidores de banco caso seja necessario
--INSERT [dbo].[ServidorBanco] ([IdServidorBanco], [NomeServidor], [StringConexao], [DataInclusao], [DataAlteracao], [DataExclusao], [IsDeleted]) VALUES (1, N'NOTE-EVERTON', N'Data Source=NOTE-EVERTON;Initial Catalog=;user Id=sa; pwd=su790000', CAST(0x0000A1A900000000 AS DateTime), NULL, NULL, 0)

INSERT [dbo].[StatusUnidadeDesenvolvimento] ([IdStatusUnidadeDesenvolvimento], [Nome], [DataInclusao], [DataAlteracao], [DataExclusao], [IsDeleted]) VALUES (1, N'Pendente', CAST(0x0000A19A00000000 AS DateTime), NULL, NULL, 0)
INSERT [dbo].[StatusUnidadeDesenvolvimento] ([IdStatusUnidadeDesenvolvimento], [Nome], [DataInclusao], [DataAlteracao], [DataExclusao], [IsDeleted]) VALUES (2, N'Em Desenvolvimento', CAST(0x0000A19A00000000 AS DateTime), NULL, NULL, 0)
INSERT [dbo].[StatusUnidadeDesenvolvimento] ([IdStatusUnidadeDesenvolvimento], [Nome], [DataInclusao], [DataAlteracao], [DataExclusao], [IsDeleted]) VALUES (3, N'Homologação', CAST(0x0000A19A00000000 AS DateTime), NULL, NULL, 0)
INSERT [dbo].[StatusUnidadeDesenvolvimento] ([IdStatusUnidadeDesenvolvimento], [Nome], [DataInclusao], [DataAlteracao], [DataExclusao], [IsDeleted]) VALUES (4, N'Aguardando Homologação de Produtos', CAST(0x0000A19A00000000 AS DateTime), NULL, NULL, 0)
INSERT [dbo].[StatusUnidadeDesenvolvimento] ([IdStatusUnidadeDesenvolvimento], [Nome], [DataInclusao], [DataAlteracao], [DataExclusao], [IsDeleted]) VALUES (5, N'Aguardando Publicação em Produção', CAST(0x0000A19A00000000 AS DateTime), NULL, NULL, 0)
INSERT [dbo].[StatusUnidadeDesenvolvimento] ([IdStatusUnidadeDesenvolvimento], [Nome], [DataInclusao], [DataAlteracao], [DataExclusao], [IsDeleted]) VALUES (6, N'Finalizado', CAST(0x0000A19A00000000 AS DateTime), NULL, NULL, 0)

INSERT [dbo].[TipoScriptBanco] ([IdTipoScript], [Nome], [DataInclusao], [DataAlteracao], [DataExclusao], [IsDeleted]) VALUES (1, N'Estrutura de Banco', CAST(0x0000A1A1016CC549 AS DateTime), NULL, NULL, 0)
INSERT [dbo].[TipoScriptBanco] ([IdTipoScript], [Nome], [DataInclusao], [DataAlteracao], [DataExclusao], [IsDeleted]) VALUES (2, N'Dados', CAST(0x0000A1A1016CC54D AS DateTime), NULL, NULL, 0)
INSERT [dbo].[TipoScriptBanco] ([IdTipoScript], [Nome], [DataInclusao], [DataAlteracao], [DataExclusao], [IsDeleted]) VALUES (3, N'Carga Inicial', CAST(0x0000A1A1016CC54D AS DateTime), NULL, NULL, 0)

INSERT [dbo].[TipoServidor] ([IdTipoServidor], [Nome], [DataInclusao], [DataAlteracao], [DataExclusao], [IsDeleted]) VALUES (1, N'Homologação de Banco', CAST(0x0000A1A9014CBDFC AS DateTime), NULL, NULL, 0)
INSERT [dbo].[TipoServidor] ([IdTipoServidor], [Nome], [DataInclusao], [DataAlteracao], [DataExclusao], [IsDeleted]) VALUES (2, N'Homologação de Aplicação', CAST(0x0000A1A9014CBE00 AS DateTime), NULL, NULL, 0)
INSERT [dbo].[TipoServidor] ([IdTipoServidor], [Nome], [DataInclusao], [DataAlteracao], [DataExclusao], [IsDeleted]) VALUES (3, N'Produção de Banco', CAST(0x0000A1A9014CBE00 AS DateTime), NULL, NULL, 0)
INSERT [dbo].[TipoServidor] ([IdTipoServidor], [Nome], [DataInclusao], [DataAlteracao], [DataExclusao], [IsDeleted]) VALUES (4, N'Produção de Aplicação', CAST(0x0000A1A9014CBE00 AS DateTime), NULL, NULL, 0)

INSERT [dbo].[TipoUnidadeDesenvolvimento] ([IdTipoUnidadeDesenvolvimento], [Nome], [DataInclusao], [DataAlteracao], [DataExclusao], [IsDeleted]) VALUES (1, N'Desenvolvimento', CAST(0x0000A19A00000000 AS DateTime), NULL, NULL, 0)
INSERT [dbo].[TipoUnidadeDesenvolvimento] ([IdTipoUnidadeDesenvolvimento], [Nome], [DataInclusao], [DataAlteracao], [DataExclusao], [IsDeleted]) VALUES (2, N'Bug', CAST(0x0000A19A00000000 AS DateTime), NULL, NULL, 0)

SET IDENTITY_INSERT [dbo].[TipoWebConfig] ON
INSERT INTO [TipoWebConfig]([IdTipoWebConfig],[Nome],[DataInclusao],[IsDeleted]) VALUES (1,'Key',getdate(),0)
INSERT INTO [TipoWebConfig]([IdTipoWebConfig],[Nome],[DataInclusao],[IsDeleted]) VALUES (2,'Conection String',getdate(),0)
INSERT INTO [TipoWebConfig]([IdTipoWebConfig],[Nome],[DataInclusao],[IsDeleted]) VALUES (3,'Outro',getdate(),0)
SET IDENTITY_INSERT [dbo].[TipoWebConfig] OFF

SET IDENTITY_INSERT [dbo].[TipoArquivo] ON
INSERT INTO [TipoArquivo]([IdTipoArquivo],[Nome],[DataInclusao],[IsDeleted]) VALUES (1,'Imagens',getdate(),0)
INSERT INTO [TipoArquivo]([IdTipoArquivo],[Nome],[DataInclusao],[IsDeleted]) VALUES (2,'CSS',getdate(),0)
INSERT INTO [TipoArquivo]([IdTipoArquivo],[Nome],[DataInclusao],[IsDeleted]) VALUES (3,'JavaScript',getdate(),0)
INSERT INTO [TipoArquivo]([IdTipoArquivo],[Nome],[DataInclusao],[IsDeleted]) VALUES (4,'outro',getdate(),0)
SET IDENTITY_INSERT [dbo].[TipoArquivo] OFF

SET IDENTITY_INSERT [dbo].[TipoRepositorio] ON
INSERT INTO [TipoRepositorio]([IdTipoRepositorio],[Nome],[DataInclusao],[IsDeleted])VALUES (1, 'TFS', GETDATE(),0)
INSERT INTO [TipoRepositorio]([IdTipoRepositorio],[Nome],[DataInclusao],[IsDeleted])VALUES (2, 'SVN', GETDATE(),0)
SET IDENTITY_INSERT [dbo].[TipoRepositorio] OFF