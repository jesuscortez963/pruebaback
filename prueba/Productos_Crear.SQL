CREATE PROCEDURE Productos_Crear
    @nombre_producto VARCHAR(50),
    @descripcion_producto VARCHAR(200),
    @precio DECIMAL(18,4),
    @existencia DECIMAL(18,4),
    @tipo_producto_id INT,
    @fechas_registro DATETIME
AS
BEGIN
    SET NOCOUNT ON; 
    
    -- Validar si el tipo de producto existe
    IF NOT EXISTS (SELECT 1 FROM TiposProductos WHERE id = @tipo_producto_id)
    BEGIN
        RAISERROR('Tipo de producto no existe', 16, 1);
        RETURN;
    END

    INSERT INTO Producto (
        nombre_producto, 
        descripcion_producto, 
        precio, 
        existencia, 
        tipo_producto_id, 
        fechas_registro
    )
    VALUES (
        @nombre_producto, 
        @descripcion_producto, 
        @precio, 
        @existencia, 
        @tipo_producto_id, 
        @fechas_registro
    );
END
