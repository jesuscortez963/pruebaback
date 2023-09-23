CREATE PROCEDURE Productos_Eliminar
    @id INT
AS
BEGIN
    SET NOCOUNT ON;

    -- Validar si el producto existe
    IF NOT EXISTS (SELECT 1 FROM Producto WHERE id = @id)
    BEGIN
        RAISERROR('Producto no existe', 16, 1);
        RETURN;
    END
    
    -- Eliminar lógicamente el producto (llenar el campo fecha_eliminado)
    UPDATE Producto 
    SET fecha_eliminado = GETDATE() 
    WHERE id = @id AND fecha_eliminado IS NULL;
    
END
