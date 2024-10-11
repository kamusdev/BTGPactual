IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Disponibilidad]') AND type in (N'U'))
BEGIN

/*
 AUTOR: Luis Fernando Arbeláez Rojas
 FECHA: 09/10/2024. 
 OBSERVACIÓN: Se crea la tabla Disponibilidad. 
*/

CREATE TABLE [dbo].[Disponibilidad](
	[idSucursal] [int] NOT NULL,
	[idProducto] [int] NOT NULL,
 CONSTRAINT [PK_Disponibilidad] PRIMARY KEY CLUSTERED 
(
    [idSucursal] ASC,
	[idProducto] ASC
	
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

ALTER TABLE [dbo].[Disponibilidad]  WITH CHECK ADD  CONSTRAINT [FK_Disponibilidad_Sucursal] FOREIGN KEY([idSucursal])
REFERENCES [dbo].[Sucursal] ([id])

ALTER TABLE [dbo].[Disponibilidad] CHECK CONSTRAINT [FK_Disponibilidad_Sucursal]

ALTER TABLE [dbo].[Disponibilidad]  WITH CHECK ADD  CONSTRAINT [FK_Disponibilidad_Producto] FOREIGN KEY([idProducto])
REFERENCES [dbo].[Producto] ([id])

ALTER TABLE [dbo].[Disponibilidad] CHECK CONSTRAINT [FK_Disponibilidad_Producto]

END
