CREATE TABLE TiposProductos(
    id INT NOT NULL IDENTITY(1,1),
	nombre_tipo_producto VARCHAR(50),
	descripcion_tipo_producto VARCHAR(200),
    fechas_registro DATETIME,
	fecha_eliminado DATETIME,
	PRIMARY KEY(id)
)

CREATE TABLE Producto (
    id INT NOT NULL IDENTITY(1,1),
	nombre_producto VARCHAR(50),
	descripcion_producto VARCHAR(200),
	precio DECIMAL(18,4),
	existencia DECIMAL(18,4),
	tipo_producto_id INT,
	fechas_registro DATETIME,
	fecha_eliminado DATETIME,
	PRIMARY KEY(id),
	FOREIGN KEY (tipo_producto_id) REFERENCES TiposProductos(id)
)