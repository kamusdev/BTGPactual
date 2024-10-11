IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Inscripcion]') AND type in (N'U'))
BEGIN

/*
 AUTOR: Luis Fernando Arbeláez Rojas
 FECHA: 09/10/2024. 
 OBSERVACIÓN: Se crea la tabla Inscripcion. 
*/

CREATE TABLE [dbo].[Inscripcion](
	[idProducto] [int] NOT NULL,
	[idCliente] [int] NOT NULL,
 CONSTRAINT [PK_Inscripcion] PRIMARY KEY CLUSTERED 
(
	[idProducto] ASC,
	[idCliente] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

ALTER TABLE [dbo].[Inscripcion]  WITH CHECK ADD  CONSTRAINT [FK_Inscripcion_Cliente] FOREIGN KEY([idCliente])
REFERENCES [dbo].[Cliente] ([id])

ALTER TABLE [dbo].[Inscripcion] CHECK CONSTRAINT [FK_Inscripcion_Cliente]

ALTER TABLE [dbo].[Inscripcion]  WITH CHECK ADD  CONSTRAINT [FK_Inscripcion_Producto] FOREIGN KEY([idProducto])
REFERENCES [dbo].[Producto] ([id])

ALTER TABLE [dbo].[Inscripcion] CHECK CONSTRAINT [FK_Inscripcion_Producto]

END
