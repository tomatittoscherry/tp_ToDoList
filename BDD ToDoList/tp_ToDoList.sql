USE ToDo;
 
CREATE TABLE tareas (
    id int identity(1,1) primary key,
    activo bit not null,
    estado varchar(20) not null,
    titulo varchar(50) not null,
    descripcion varchar(200) not null,
    fecha_alta DATETIME not null,
    fecha_modificacion DATETIME not null
)