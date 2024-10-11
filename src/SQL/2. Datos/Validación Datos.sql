/*
 AUTOR: Luis Fernando Arbeláez Rojas
 FECHA: 09/10/2024. 
 OBSERVACIÓN: Se validan los datos.
*/

SELECT *
FROM Cliente C

SELECT *
FROM Producto P

SELECT *
FROM SucurSal S

SELECT I.idCliente,I.idProducto
FROM Inscripcion I
ORDER BY I.idCliente, I.idProducto 

SELECT D.idSucursal,D.idProducto
FROM Disponibilidad D
ORDER BY D.idSucursal, D.idProducto 

SELECT V.idSucursal,V.idCliente,V.fechaVisita
FROM Visitan V
ORDER BY V.idSucursal, V.idCliente

/*
DELETE FROM Visitan
DELETE FROM Disponibilidad
DELETE FROM Inscripcion
DELETE FROM Producto
DELETE FROM Cliente
DELETE FROM Sucursal
*/

--DBCC CHECKIDENT (Producto, RESEED,0)


SELECT *
FROM SucurSal S

SELECT *
FROM Cliente C

