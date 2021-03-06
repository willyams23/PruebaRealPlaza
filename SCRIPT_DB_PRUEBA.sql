USE [DB_PRUEBA]
GO
/****** Object:  StoredProcedure [dbo].[USP_Producto_BUSCAR]    Script Date: 12/08/2021 02:40:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[USP_Producto_BUSCAR]
@p_Id INT
AS
SELECT Id, Nombre, Precio, Stock, FechaRegistro FROM [dbo].[Producto]
WHERE [Id] = @p_Id

GO
/****** Object:  StoredProcedure [dbo].[USP_Producto_CREAR]    Script Date: 12/08/2021 02:40:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[USP_Producto_CREAR]
@p_Id INT,
@p_Nombre VARCHAR(100),
@p_Precio DECIMAL(18,2),
@p_Stock INT,
@p_FechaRegistro DATETIME
AS
BEGIN 
	IF(@p_Id = 0)
	BEGIN
		INSERT INTO [dbo].[Producto] (Nombre, Precio, Stock, FechaRegistro)
		VALUES (@p_Nombre, @p_Precio, @p_Stock, @p_FechaRegistro)

		SELECT Id, Nombre, Precio, Stock, FechaRegistro FROM [dbo].[Producto]
		WHERE [Id] = SCOPE_IDENTITY()
		--ORDER BY Id DESC
	END
	ELSE
	BEGIN
		UPDATE [dbo].[Producto]
		SET
		Nombre = @p_Nombre,
		Precio = @p_Precio,
		Stock = @p_Stock, 
		FechaRegistro = @p_FechaRegistro
		WHERE Id = @p_Id

		SELECT Id, Nombre, Precio, Stock, FechaRegistro FROM [dbo].[Producto]
		WHERE [Id] = @p_Id;
	END
END

GO
/****** Object:  StoredProcedure [dbo].[USP_Producto_Eliminar]    Script Date: 12/08/2021 02:40:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_Producto_Eliminar]
@p_Id INT
AS
BEGIN 
	DELETE FROM [dbo].[Producto] WHERE [Id] = @p_Id
END

GO
/****** Object:  StoredProcedure [dbo].[USP_Producto_LIST]    Script Date: 12/08/2021 02:40:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[USP_Producto_LIST]
AS
SELECT Id, Nombre, Precio, Stock, FechaRegistro FROM [dbo].[Producto]

GO
/****** Object:  Table [dbo].[Producto]    Script Date: 12/08/2021 02:40:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Producto](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](100) NULL,
	[Precio] [decimal](18, 2) NULL,
	[Stock] [int] NULL,
	[FechaRegistro] [datetime] NULL,
 CONSTRAINT [pk_Producto_Id] PRIMARY KEY NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
