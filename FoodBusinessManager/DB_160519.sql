USE [FBM]
GO
/****** Object:  Table [dbo].[Bitacora]    Script Date: 16/05/2019 1:04:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Bitacora](
	[id_Bitacora] [int] NOT NULL,
	[fecha_Hora] [datetime] NOT NULL,
	[id_usuario] [int] NOT NULL,
	[id_tipo_Suceso] [int] NOT NULL,
	[valorAnterior] [varchar](max) NULL,
	[valorNuevo] [varchar](max) NULL,
	[observaciones] [varchar](max) NULL,
	[DVH] [varchar](160) NULL,
 CONSTRAINT [PK_Bitacora] PRIMARY KEY CLUSTERED 
(
	[id_Bitacora] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Bitacora_errores]    Script Date: 16/05/2019 1:04:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Bitacora_errores](
	[id_bitacora_error] [int] NOT NULL,
	[stackTrace] [varchar](max) NULL,
	[exception] [varchar](max) NULL,
	[id_Bitacora] [int] NULL,
 CONSTRAINT [PK_Bitacora_errores] PRIMARY KEY CLUSTERED 
(
	[id_bitacora_error] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DVV]    Script Date: 16/05/2019 1:04:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DVV](
	[tabla] [varchar](100) NULL,
	[dvv] [varchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Etiqueta]    Script Date: 16/05/2019 1:04:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Etiqueta](
	[id_etiqueta] [int] NOT NULL,
	[etiqueta] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id_etiqueta] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Idioma_etiquetas]    Script Date: 16/05/2019 1:04:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Idioma_etiquetas](
	[id_idioma] [varchar](10) NOT NULL,
	[id_etiqueta] [int] NOT NULL,
	[traduccion] [varchar](max) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id_idioma] ASC,
	[id_etiqueta] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[idiomas]    Script Date: 16/05/2019 1:04:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[idiomas](
	[id_idioma] [varchar](10) NOT NULL,
	[nombre] [varchar](50) NOT NULL,
	[DVH] [varchar](160) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id_idioma] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Perfil]    Script Date: 16/05/2019 1:04:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Perfil](
	[id_perfil] [int] NOT NULL,
	[nombre] [varchar](50) NOT NULL,
	[es_permiso] [bit] NOT NULL,
	[url] [varchar](max) NOT NULL,
	[DVH] [varchar](160) NOT NULL,
	[es_substractiva] [bit] NOT NULL,
 CONSTRAINT [PK_Perfil] PRIMARY KEY CLUSTERED 
(
	[id_perfil] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Perfil_Permisos]    Script Date: 16/05/2019 1:04:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Perfil_Permisos](
	[id_Perfil] [int] NOT NULL,
	[id_Permiso] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tipo_Suceso]    Script Date: 16/05/2019 1:04:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tipo_Suceso](
	[id_tipo_suceso] [int] NOT NULL,
	[descripcion] [varchar](50) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuarios]    Script Date: 16/05/2019 1:04:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuarios](
	[nombre] [varchar](100) NOT NULL,
	[apellido] [varchar](100) NOT NULL,
	[username] [varchar](100) NOT NULL,
	[password] [varchar](50) NOT NULL,
	[DVH] [varchar](160) NOT NULL,
	[fecha_Creacion] [datetime] NOT NULL,
	[intentos] [int] NOT NULL,
	[bloqueado] [int] NOT NULL,
	[id_idioma] [varchar](10) NULL,
	[id_perfil] [int] NOT NULL,
	[id_usuario] [int] NOT NULL,
	[mail] [varchar](max) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id_usuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[Bitacora] ([id_Bitacora], [fecha_Hora], [id_usuario], [id_tipo_Suceso], [valorAnterior], [valorNuevo], [observaciones], [DVH]) VALUES (1, CAST(N'2019-04-13T14:17:50.333' AS DateTime), 1, 1, N'', N'', N'', N'')
INSERT [dbo].[Bitacora] ([id_Bitacora], [fecha_Hora], [id_usuario], [id_tipo_Suceso], [valorAnterior], [valorNuevo], [observaciones], [DVH]) VALUES (2, CAST(N'2019-04-23T20:01:04.797' AS DateTime), 7, 1, N'', N'', N'', N'')
INSERT [dbo].[idiomas] ([id_idioma], [nombre], [DVH]) VALUES (N'es-AR', N'Español Argentino', N'21957f8690cbeaae87b2ff9016ced1737efa9de5')
INSERT [dbo].[Perfil] ([id_perfil], [nombre], [es_permiso], [url], [DVH], [es_substractiva]) VALUES (1, N'Administrador', 0, N'url', N'DVH', 0)
INSERT [dbo].[Perfil] ([id_perfil], [nombre], [es_permiso], [url], [DVH], [es_substractiva]) VALUES (2, N'Backup & Restore', 1, N'url', N'DVH', 0)
INSERT [dbo].[Perfil] ([id_perfil], [nombre], [es_permiso], [url], [DVH], [es_substractiva]) VALUES (3, N'Bitacora', 1, N'url', N'DVH', 0)
INSERT [dbo].[Perfil] ([id_perfil], [nombre], [es_permiso], [url], [DVH], [es_substractiva]) VALUES (4, N'Idiomas', 1, N'url', N'DVH', 0)
INSERT [dbo].[Perfil] ([id_perfil], [nombre], [es_permiso], [url], [DVH], [es_substractiva]) VALUES (5, N'Administracion Perfiles', 1, N'url', N'DVH', 0)
INSERT [dbo].[Perfil] ([id_perfil], [nombre], [es_permiso], [url], [DVH], [es_substractiva]) VALUES (6, N'Solo Backup & Restore', 0, N'url', N'DVH', 0)
INSERT [dbo].[Perfil] ([id_perfil], [nombre], [es_permiso], [url], [DVH], [es_substractiva]) VALUES (7, N'SuperPerfil', 0, N'url', N'DVH', 0)
INSERT [dbo].[Perfil_Permisos] ([id_Perfil], [id_Permiso]) VALUES (1, 2)
INSERT [dbo].[Perfil_Permisos] ([id_Perfil], [id_Permiso]) VALUES (1, 3)
INSERT [dbo].[Perfil_Permisos] ([id_Perfil], [id_Permiso]) VALUES (1, 4)
INSERT [dbo].[Perfil_Permisos] ([id_Perfil], [id_Permiso]) VALUES (1, 5)
INSERT [dbo].[Perfil_Permisos] ([id_Perfil], [id_Permiso]) VALUES (6, 2)
INSERT [dbo].[Perfil_Permisos] ([id_Perfil], [id_Permiso]) VALUES (7, 1)
INSERT [dbo].[Perfil_Permisos] ([id_Perfil], [id_Permiso]) VALUES (7, 6)
INSERT [dbo].[Tipo_Suceso] ([id_tipo_suceso], [descripcion]) VALUES (1, N'Log in')
INSERT [dbo].[Usuarios] ([nombre], [apellido], [username], [password], [DVH], [fecha_Creacion], [intentos], [bloqueado], [id_idioma], [id_perfil], [id_usuario], [mail]) VALUES (N'Sebastian', N'Travella', N'stravella', N'GVWK/JQwfL4ZvWdyHR01f+pM+CY=', N'QZ6cSUPGI8bsWCwdeou7o9/ippU=', CAST(N'2019-05-07T20:16:13.937' AS DateTime), 0, 1, N'es-AR', 1, 1, N'sebastian.martin.travella@gmail.com')
ALTER TABLE [dbo].[Bitacora_errores]  WITH CHECK ADD  CONSTRAINT [FK_Bitacora_errores_Bitacora] FOREIGN KEY([id_Bitacora])
REFERENCES [dbo].[Bitacora] ([id_Bitacora])
GO
ALTER TABLE [dbo].[Bitacora_errores] CHECK CONSTRAINT [FK_Bitacora_errores_Bitacora]
GO
ALTER TABLE [dbo].[Idioma_etiquetas]  WITH CHECK ADD  CONSTRAINT [FK_Idioma_etiquetas_etiqueta] FOREIGN KEY([id_etiqueta])
REFERENCES [dbo].[Etiqueta] ([id_etiqueta])
GO
ALTER TABLE [dbo].[Idioma_etiquetas] CHECK CONSTRAINT [FK_Idioma_etiquetas_etiqueta]
GO
ALTER TABLE [dbo].[Idioma_etiquetas]  WITH CHECK ADD  CONSTRAINT [FK_Idioma_etiquetas_Idioma] FOREIGN KEY([id_idioma])
REFERENCES [dbo].[idiomas] ([id_idioma])
GO
ALTER TABLE [dbo].[Idioma_etiquetas] CHECK CONSTRAINT [FK_Idioma_etiquetas_Idioma]
GO
ALTER TABLE [dbo].[Perfil_Permisos]  WITH CHECK ADD  CONSTRAINT [FK_Perfil_Permisos_Perfil] FOREIGN KEY([id_Perfil])
REFERENCES [dbo].[Perfil] ([id_perfil])
GO
ALTER TABLE [dbo].[Perfil_Permisos] CHECK CONSTRAINT [FK_Perfil_Permisos_Perfil]
GO
ALTER TABLE [dbo].[Perfil_Permisos]  WITH CHECK ADD  CONSTRAINT [FK_Perfil_Permisos_Perfil_02] FOREIGN KEY([id_Permiso])
REFERENCES [dbo].[Perfil] ([id_perfil])
GO
ALTER TABLE [dbo].[Perfil_Permisos] CHECK CONSTRAINT [FK_Perfil_Permisos_Perfil_02]
GO
ALTER TABLE [dbo].[Usuarios]  WITH CHECK ADD  CONSTRAINT [FK_Usuario_Idioma] FOREIGN KEY([id_idioma])
REFERENCES [dbo].[idiomas] ([id_idioma])
GO
ALTER TABLE [dbo].[Usuarios] CHECK CONSTRAINT [FK_Usuario_Idioma]
GO
ALTER TABLE [dbo].[Usuarios]  WITH CHECK ADD  CONSTRAINT [FK_Usuario_Perfil] FOREIGN KEY([id_perfil])
REFERENCES [dbo].[Perfil] ([id_perfil])
GO
ALTER TABLE [dbo].[Usuarios] CHECK CONSTRAINT [FK_Usuario_Perfil]
GO
/****** Object:  StoredProcedure [dbo].[Bitacora_Crear]    Script Date: 16/05/2019 1:04:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Bitacora_Crear]
	@id int,
	@fecha_hora datetime,
	@id_usuario int,
	@id_tipo_suceso int,
	@valorAnterior varchar(max),
	@valorNuevo varchar(max),
	@obs varchar(max),
	@DVH varchar(max)
AS
BEGIN
INSERT INTO Bitacora
           ([id_Bitacora]
           ,[fecha_Hora]
           ,[id_usuario]
           ,[id_tipo_Suceso]
           ,[valorAnterior]
           ,[valorNuevo]
           ,[observaciones]
           ,[DVH])
     VALUES
           (@id
           ,@fecha_hora
           ,@id_usuario
           ,@id_tipo_suceso
           ,@valorAnterior
           ,@valorNuevo
           ,@obs
           ,@DVH)
END
GO
/****** Object:  StoredProcedure [dbo].[Bitacora_Listar]    Script Date: 16/05/2019 1:04:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Bitacora_Listar]

AS
BEGIN
	SELECT * FROM Bitacora
END
GO
/****** Object:  StoredProcedure [dbo].[Etiquetas_Listar]    Script Date: 16/05/2019 1:04:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Etiquetas_Listar]
	
AS
BEGIN
	SELECT * FROM Etiquetas
END
GO
/****** Object:  StoredProcedure [dbo].[Etiquetas_Obtener]    Script Date: 16/05/2019 1:04:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Etiquetas_Obtener]
	@id_etiqueta int
AS
BEGIN
	SELECT * FROM Etiquetas WHERE id_etiqueta = @id_etiqueta
END
GO
/****** Object:  StoredProcedure [dbo].[GetNextID]    Script Date: 16/05/2019 1:04:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetNextID]
    @TableName NVARCHAR(100), @PKfield varchar(50)

    AS
BEGIN
    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
    SET NOCOUNT ON;
    DECLARE @sSQL nvarchar(500);

    SELECT @sSQL = N'IF (SELECT MAX('+QUOTENAME(@PKfield)+') FROM' + QUOTENAME(@TableName)+') IS NOT NULL SELECT MAX('+QUOTENAME(@PKfield)+'+1) FROM' + QUOTENAME(@TableName)+'else select 1';
	
    EXEC sp_executesql @sSQL
	

END
GO
/****** Object:  StoredProcedure [dbo].[Idioma_Crear]    Script Date: 16/05/2019 1:04:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Idioma_Crear] 
	@id_idioma varchar(10),
	@nombre varchar(50),
	@DVH varchar(160)
AS
BEGIN
INSERT INTO [dbo].[idiomas]
           ([id_idioma]
           ,[nombre]
           ,[DVH])
     VALUES
           (@id_idioma,
		    @nombre,
			@DVH)
END
GO
/****** Object:  StoredProcedure [dbo].[Idioma_Etiquetas_Crear]    Script Date: 16/05/2019 1:04:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Idioma_Etiquetas_Crear]
	@id_idioma varchar(10),
	@id_etiqueta int,
	@traduccion int

AS
BEGIN
	INSERT INTO [dbo].[Idioma_etiquetas]
           ([id_idioma]
           ,[id_etiqueta]
           ,[traduccion])
     VALUES
           (@id_idioma,
			@id_etiqueta,
			@traduccion)
END;
GO
/****** Object:  StoredProcedure [dbo].[Idioma_Etiquetas_Listar]    Script Date: 16/05/2019 1:04:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Idioma_Etiquetas_Listar]
	
AS
BEGIN
	SELECT * FROM Idioma_etiquetas	
END
GO
/****** Object:  StoredProcedure [dbo].[Idioma_Etiquetas_ListarPorIdioma]    Script Date: 16/05/2019 1:04:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Idioma_Etiquetas_ListarPorIdioma]
	@id_idioma varchar(10)
AS
BEGIN
	SELECT etiqueta.id_etiqueta,
		   etiqueta.etiqueta,
		   Idioma_etiquetas.traduccion		   
	FROM Etiqueta
	INNER JOIN Idioma_Etiquetas ON etiqueta.id_etiqueta = Idioma_etiquetas.id_etiqueta
	WHERE idioma_etiquetas.id_idioma = @id_idioma
END
GO
/****** Object:  StoredProcedure [dbo].[Idioma_Etiquetas_Modificar]    Script Date: 16/05/2019 1:04:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Idioma_Etiquetas_Modificar]
	@id_idioma varchar(10),
	@id_etiqueta int,
	@traduccion int
AS
BEGIN
	UPDATE Idioma_etiquetas
	SET traduccion = @traduccion
	WHERE id_idioma = @id_idioma AND id_etiqueta = @id_etiqueta
END
GO
/****** Object:  StoredProcedure [dbo].[Idioma_Etiquetas_Obtener]    Script Date: 16/05/2019 1:04:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Idioma_Etiquetas_Obtener]
	@id_idioma varchar(10),
	@id_etiqueta int
AS
BEGIN
	SELECT * FROM Idioma_etiquetas
	WHERE id_idioma = @id_idioma AND id_etiqueta = @id_etiqueta
END
GO
/****** Object:  StoredProcedure [dbo].[Idioma_Listar]    Script Date: 16/05/2019 1:04:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Idioma_Listar]

AS
BEGIN
	SELECT * FROM idiomas;
END
GO
/****** Object:  StoredProcedure [dbo].[Idioma_Modificar]    Script Date: 16/05/2019 1:04:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Idioma_Modificar]
	@id_idioma varchar(10),
	@nombre varchar(50),
	@DVH varchar(160)
AS
BEGIN
	UPDATE [dbo].[idiomas]
	SET 
      nombre = @nombre,
      DVH = @DVH
	WHERE id_idioma = @id_idioma
END
GO
/****** Object:  StoredProcedure [dbo].[Idioma_Obtener]    Script Date: 16/05/2019 1:04:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Idioma_Obtener]
	@id_idioma varchar(10)
AS
BEGIN
	SELECT * FROM idiomas where id_idioma = @id_idioma;
END
GO
/****** Object:  StoredProcedure [dbo].[ListarUsuarios]    Script Date: 16/05/2019 1:04:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ListarUsuarios]
AS
BEGIN

	SET NOCOUNT ON;

    SELECT * FROM Usuarios
END
GO
/****** Object:  StoredProcedure [dbo].[ObtenerUsuario]    Script Date: 16/05/2019 1:04:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ObtenerUsuario]
	@username varchar(100), 
	@password varchar(50)
AS
BEGIN
	SET NOCOUNT ON;

	SELECT * FROM usuarios WHERE username = @username
END;


GO
/****** Object:  StoredProcedure [dbo].[Perfil_Crear]    Script Date: 16/05/2019 1:04:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[Perfil_Crear]
	@id_perfil int,
	@nombre varchar,
	@es_permiso bit,
	@DVH varchar,
	@url varchar,
	@es_substractiva bit
AS

BEGIN
	 INSERT INTO [dbo].[Perfil]
		(id_perfil,
		 nombre,
		 es_permiso,
		 DVH,
		 url,
		 es_substractiva
		)
     VALUES
           (@id_perfil
           ,@nombre
           ,@es_permiso
           ,@DVH
		   ,@url
           ,@es_substractiva)
END

;
GO
/****** Object:  StoredProcedure [dbo].[Perfil_Eliminar]    Script Date: 16/05/2019 1:04:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Perfil_Eliminar]
	@id_perfil int
AS

BEGIN
	DELETE Perfil
	WHERE id_perfil = @id_perfil

	DELETE Perfil_Permisos
	WHERE id_Perfil = @id_perfil

END;
GO
/****** Object:  StoredProcedure [dbo].[Perfil_Listar]    Script Date: 16/05/2019 1:04:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Perfil_Listar]
	
AS
	
BEGIN
	select * from Perfil; 
END
;
GO
/****** Object:  StoredProcedure [dbo].[Perfil_ListarHijos]    Script Date: 16/05/2019 1:04:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Perfil_ListarHijos]
	@id_Perfil int
AS 

BEGIN

	SELECT *
	FROM Perfil
	WHERE id_perfil IN (
		SELECT id_permiso FROM Perfil_Permisos WHERE id_Perfil = @id_Perfil
		)
	
END
GO
/****** Object:  StoredProcedure [dbo].[Perfil_Modificar]    Script Date: 16/05/2019 1:04:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[Perfil_Modificar]
	@id_perfil int,
	@nombre varchar,
	@es_permiso bit,
	@url varchar,
	@DVH varchar,
	@es_substractiva bit
AS

BEGIN
	 UPDATE [dbo].[Perfil]
     SET   nombre = @nombre
           ,es_permiso = @es_permiso
           ,url = @url
           ,dvh = @DVH
           ,es_substractiva = @es_substractiva
	 WHERE id_perfil = @id_perfil	
END
;
GO
/****** Object:  StoredProcedure [dbo].[Perfil_Obtener]    Script Date: 16/05/2019 1:04:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[Perfil_Obtener]
	@id_perfil int
AS

BEGIN
	select * from Perfil where id_perfil=@id_perfil
END;
GO
/****** Object:  StoredProcedure [dbo].[Perfil_Permisos_Crear]    Script Date: 16/05/2019 1:04:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Perfil_Permisos_Crear]
	@id_perfil int,
	@id_permiso int,
	@DVH varchar
AS
	
BEGIN
	INSERT INTO Perfil_Permisos
	VALUES (
		@id_perfil,
		@id_permiso,
		@DVH
	)
END;
GO
/****** Object:  StoredProcedure [dbo].[Perfil_Permisos_Eliminar]    Script Date: 16/05/2019 1:04:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Perfil_Permisos_Eliminar]
	@id_perfil int,
	@id_permiso int
AS

BEGIN
	DELETE Perfil_Permisos
	WHERE id_Perfil = @id_perfil AND id_Permiso = @id_permiso
END;
GO
/****** Object:  StoredProcedure [dbo].[Perfil_Permisos_Listar]    Script Date: 16/05/2019 1:04:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Perfil_Permisos_Listar]
	
AS
	
BEGIN
	select * from Perfil_Permisos; 
END

GO
/****** Object:  StoredProcedure [dbo].[Perfil_Permisos_Modificar]    Script Date: 16/05/2019 1:04:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Perfil_Permisos_Modificar]
	@id_perfil int,
	@id_permiso int,
	@DVH varchar
AS
	
BEGIN
	UPDATE Perfil_Permisos
	SET dvh = @dvh
	WHERE id_perfil = @id_perfil AND id_permiso = @id_permiso 
END;
GO
/****** Object:  StoredProcedure [dbo].[Perfil_Permisos_Obtener]    Script Date: 16/05/2019 1:04:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Perfil_Permisos_Obtener]
	@id_perfil int,
	@id_permiso int
AS

BEGIN
	SELECT * FROM Perfil_Permisos WHERE id_Perfil = @id_perfil AND id_Permiso = @id_permiso
END;
GO
/****** Object:  StoredProcedure [dbo].[TipoSuceso_Listar]    Script Date: 16/05/2019 1:04:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[TipoSuceso_Listar]

AS
BEGIN
	SELECT * FROM Tipo_Suceso
END
GO
/****** Object:  StoredProcedure [dbo].[Usuarios_Crear]    Script Date: 16/05/2019 1:04:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Usuarios_Crear]
	@nombre varchar(100),
	@apellido varchar(100),
	@username varchar(100),
	@password varchar(100),
	@DVH varchar(160),
	@fecha_Creacion datetime,
	@intentos int,
	@bloqueado int,
	@id_idioma varchar(10),
	@id_perfil int,
	@id_usuario int,
	@mail varchar(max)
AS
BEGIN
	INSERT INTO [dbo].[Usuarios]
           ([nombre]
           ,[apellido]
           ,[username]
           ,[password]
           ,[DVH]
           ,[fecha_Creacion]
           ,[intentos]
           ,[bloqueado]
           ,[id_idioma]
           ,[id_perfil]
           ,[id_usuario]
           ,[mail])
     VALUES(
			@nombre,
			@apellido,
			@username,
			@password,
			@DVH,
			@fecha_Creacion,
			@intentos,
			@bloqueado,
			@id_idioma,
			@id_perfil,
			@id_usuario,
			@mail)
END
GO
/****** Object:  StoredProcedure [dbo].[Usuarios_Modificar]    Script Date: 16/05/2019 1:04:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Usuarios_Modificar]
	@nombre varchar(100),
	@apellido varchar(100),
	@username varchar(100),
	@password varchar(100),
	@DVH varchar(160),
	@fecha_Creacion datetime,
	@intentos int,
	@bloqueado int,
	@id_idioma varchar(10),
	@id_perfil int,
	@id_usuario int,
	@mail varchar(max)
AS
BEGIN
	UPDATE Usuarios
	   SET [nombre] = @nombre
		  ,[apellido] = @apellido
		  ,[username] = @username
		  ,[password] = @password
		  ,[DVH] = @DVH
		  ,[fecha_Creacion] = @fecha_Creacion
		  ,[intentos] = @intentos
		  ,[bloqueado] = @bloqueado
		  ,[id_idioma] = @id_idioma
		  ,[id_perfil] = @id_perfil
		  ,mail = @mail
	 WHERE id_usuario = @id_usuario
END
GO
