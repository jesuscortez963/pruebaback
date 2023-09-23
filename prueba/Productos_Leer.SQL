CREATE PROCEDURE sp_ConsultarProductos
    @id INT = NULL,
    @nombre_producto VARCHAR(50) = NULL,
    @mostrar_eliminados BIT = 0
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
    WHERE 
        (@id IS NULL OR p.id = @id)
        AND (@nombre_producto IS NULL OR p.nombre_producto LIKE '%' + @nombre_producto + '%')
        AND (@mostrar_eliminados = 1 OR p.fecha_eliminado IS NULL);
END
