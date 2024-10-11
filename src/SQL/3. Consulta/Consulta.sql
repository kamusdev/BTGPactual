/*
 AUTOR: Luis Fernando Arbeláez Rojas
 FECHA: 09/10/2024. 
 OBSERVACIÓN: Obtener los nombres de los clientes los cuales tienen inscrito algún producto disponible sólo en las
 sucursales que visitan.
*/

-- DECLARACIÓN VARIABLES Y TABLAS.  
DECLARE @@ProductosInscriptosyDisponiblesporCliente TABLE (
       IdCliente INT,
	   IdProducto INT,
	   IdSucursal INT,
	   Cliente VARCHAR(100),
	   Producto VARCHAR (50),
	   SucursalDisponible VARCHAR (50)
)

DECLARE @@SucursalesVisitadasporCliente TABLE (
       IdCliente INT,	  
	   IdSucursal INT,
	   Cliente VARCHAR(100),	   
	   Sucursal VARCHAR (50)
)


-- Listado de los Clientes con sus Productos Inscritos y la Disponibilidad por Sucursal
INSERT INTO @@ProductosInscriptosyDisponiblesporCliente(IdCliente,IdProducto,IdSucursal,Cliente,Producto,SucursalDisponible)
SELECT 

 C.id AS IdCliente
,P.id AS IdProducto
,S.id AS IdSucursal
,ISNULL(C.nombre,'') + ' ' + ISNULL(C.apellidos,'') as Cliente
,P.nombre AS Producto
,S.nombre AS SucursalDisponible

FROM Cliente C
INNER JOIN Inscripcion I ON I.idCliente = C.id
INNER JOIN Producto P ON P.id = I.idProducto
INNER JOIN Disponibilidad D ON D.idProducto = P.id
INNER JOIN Sucursal S on S.id = D.idSucursal
ORDER BY C.nombre ASC ,P.nombre ASC

-- Listado de las Sucursales que Visitan cada uno de los Clientes
INSERT INTO @@SucursalesVisitadasporCliente (IdCliente,IdSucursal,Cliente,Sucursal)
SELECT 

 C.id AS IdCliente
,S.id AS IdSucursal
,ISNULL(C.nombre,'') + ' ' + ISNULL(C.apellidos,'') as Cliente
,S.nombre AS Sucursal

FROM Cliente C
INNER JOIN Visitan V ON V.idCliente = C.id
INNER JOIN Sucursal S ON S.id = V.idSucursal
--WHERE V.idSucursal = 4
ORDER BY C.nombre ASC, S.nombre ASC

--SELECT *
--FROM @@ProductosInscriptosyDisponiblesporCliente P
--WHERE P.IdCliente = 1

--SELECT *
--FROM @@SucursalesVisitadasporCliente S
--WHERE S.IdCliente = 1

-- Nombres de los clientes los cuales tienen inscrito algún producto disponible sólo en las sucursales que visitan.
SELECT DISTINCT P.Cliente
FROM @@ProductosInscriptosyDisponiblesporCliente P
INNER JOIN @@SucursalesVisitadasporCliente S ON P.IdCliente = S.IdCliente
                                            AND P.IdSucursal = S.IdSucursal
--WHERE P.IdCliente = 1
