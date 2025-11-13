
CREATE DATABASE BD_VQUISPEH_T3;


USE BD_VQUISPEH_T3;

CREATE TABLE Usuarios (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100) NOT NULL UNIQUE,
    Contraseña NVARCHAR(100) NOT NULL
);


INSERT INTO Usuarios (Nombre, Email, Contraseña) 
VALUES ('Usuario Prueba', 'test@test.com', '123456');


CREATE TABLE Libros (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Titulo NVARCHAR(200) NOT NULL,
    Autor NVARCHAR(150) NOT NULL,
    Tema NVARCHAR(100) NOT NULL,
    Editorial NVARCHAR(100) NOT NULL,
    AnioPublicacion INT NOT NULL,
    Paginas INT NOT NULL,
    Categoria NVARCHAR(50) NOT NULL,
    Material NVARCHAR(50) NOT NULL,
    Copias INT NOT NULL
);


INSERT INTO Libros (Titulo, Autor, Tema, Editorial, AnioPublicacion, Paginas, Categoria, Material, Copias) VALUES 

SELECT * FROM Libros;
