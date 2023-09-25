CREATE PROCEDURE sp_EditarProducto
    @id INT,
    @nombre_producto VARCHAR(50) = NULL,
    @descripcion_producto VARCHAR(200) = NULL,
    @precio DECIMAL(18,4) = NULL,
    @existencia DECIMAL(18,4) = NULL,
    @tipo_producto_id INT = NULL,
    @fechas_registro DATETIME = NULL,
    @fecha_eliminado DATETIME = NULL
AS
BEGIN
    SET NOCOUNT ON;

    -- Validar si el producto existe
    IF NOT EXISTS (SELECT 1 FROM Producto WHERE id = @id)
    BEGIN
        RAISERROR('Producto no existe', 16, 1);
        RETURN;
    END
    
    -- Validar si el tipo de producto existe
    IF @tipo_producto_id IS NOT NULL AND NOT EXISTS (SELECT 1 FROM TiposProductos WHERE id = @tipo_producto_id)
    BEGIN
        RAISERROR('Tipo de producto no existe', 16, 1);
        RETURN;
    END
    
    -- Actualizar el producto
    UPDATE Producto 
    SET nombre_producto = ISNULL(@nombre_producto, nombre_producto),
        descripcion_producto = ISNULL(@descripcion_producto, descripcion_producto),
        precio = ISNULL(@precio, precio),
        existencia = ISNULL(@existencia, existencia),
        tipo_producto_id = ISNULL(@tipo_producto_id, tipo_producto_id),
        fechas_registro = ISNULL(@fechas_registro, fechas_registro),
        fecha_eliminado = ISNULL(@fecha_eliminado, fecha_eliminado)
    WHERE id = @id;
END
