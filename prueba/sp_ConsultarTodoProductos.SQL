CREATE PROCEDURE sp_ConsultarTodoProductos

AS
BEGIN
    SET NOCOUNT ON;

SELECT 
        p.id,
        p.nombre_producto,
        p.descripcion_producto,
        p.precio,
        p.existencia,
        p.tipo_producto_id,
        tp.nombre_tipo_producto,
        p.fechas_registro,
        p.fecha_eliminado
    FROM Producto p
    INNER JOIN TiposProductos tp ON p.tipo_producto_id = tp.id
    WHERE  p.fecha_eliminado IS NULL;
END
