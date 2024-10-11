IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Visitan]') AND type in (N'U'))
BEGIN

/*
 AUTOR: Luis Fernando Arbeláez Rojas
 FECHA: 09/10/2024. 
 OBSERVACIÓN: Se crea la tabla Visitan. 
*/

CREATE TABLE [dbo].[Visitan](
	[idSucursal] [int] NOT NULL,
	[idCliente] [int] NOT NULL,
	[fechaVisita][datetime] NOT NULL
 CONSTRAINT [PK_Visitan] PRIMARY KEY CLUSTERED 
(
    [idSucursal] ASC,
	[idCliente] ASC
	
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

ALTER TABLE [dbo].[Visitan]  WITH CHECK ADD  CONSTRAINT [FK_Visitan_Sucursal] FOREIGN KEY([idSucursal])
REFERENCES [dbo].[Sucursal] ([id])

ALTER TABLE [dbo].[Visitan] CHECK CONSTRAINT [FK_Visitan_Sucursal]

ALTER TABLE [dbo].[Visitan]  WITH CHECK ADD  CONSTRAINT [FK_Visitan_Cliente] FOREIGN KEY([idCliente])
REFERENCES [dbo].[Cliente] ([id])

ALTER TABLE [dbo].[Visitan] CHECK CONSTRAINT [FK_Visitan_Cliente]

END
