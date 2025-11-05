-- SCRIPT UNIFICADO DE LA BASE DE DATOS --

create DATABASE FarmaciaElPalenqueDB;

use FarmaciaElPalenqueDB;

CREATE TABLE Usuarios (
    id int IDENTITY(1,1) PRIMARY KEY,
    nombre varchar(15) NOT NULL,
    apellido varchar(20) NOT NULL,
    passwordHash varchar (10) NOT NULL,
    rol varchar (20) NOT NULL,
    email varchar(50) NOT NULL,    
);

CREATE TABLE Categorias(
    id int,
    nombre varchar(50)
);

-- DROP TABLE Productos; --

CREATE TABLE Productos(
    id int IDENTITY(1,1) PRIMARY KEY,
    nombre varchar (100),
    precio int,
    categoriaId int,
    imagenUrl varchar (150) NOT NULL,
    Stock int,
);

SET IDENTITY_INSERT Usuarios ON;

INSERT INTO Usuarios (id, nombre, apellido, passwordHash, rol, email) VALUES
(1,  'admin',       'General',   'admin123',   'Administrador', 'admin@palenque.com'),
(2,  'Juan',        'Perez',     '1234',       'Cliente',       'juan@gmail.com'),
(3,  'Maria',       'Garcia',    'clave123',   'Cliente',       'maria@gmail.com'),
(4,  'Carlos',      'Lopez',     'qwerty',     'Cliente',       'carlos@gmail.com'),
(5,  'Laura',       'Gonzalez',  'pass1234',   'Cliente',       'laura@gmail.com'),
(6,  'Ana',         'Fernandez', 'abc123',     'Cliente',       'ana@gmail.com'),
(7,  'Roberto',     'Alvarez',   'adminadmin', 'Administrador', 'roberto@palenque.com'),
(8,  'Camila',      'Martinez',  'cami321',    'Cliente',       'camila@gmail.com'),
(9,  'Luciano',     'Ruiz',      '123456',     'Cliente',       'luciano@gmail.com'),
(10, 'Carolina',    'Mendez',    'securepass', 'Cliente',       'carolina@gmail.com'),
(11, 'Fernanda',    'Piterzen',  '123$fer',    'Cliente',       'fernanda@gmail.com'),
(12, 'Agustin',     'Sosa',      'Sosa#456',   'Cliente',       'fer@gmail.com'),
(13, 'Admin',       'Polenta',   'ElPolenta$', 'Administrador', 'polenta@palenque.com');

SET IDENTITY_INSERT Usuarios OFF;

SELECT * FROM Usuarios WHERE id BETWEEN 1 AND 13 ORDER BY id;

-- CATEGORIAS --

DELETE FROM Categorias WHERE id IN (1,2,3);

SET IDENTITY_INSERT Categorias ON;

INSERT INTO Categorias (id, nombre) VALUES
(1, N'Medicamentos'),
(2, N'Perfumería'),
(3, N'Cuidado personal');

SET IDENTITY_INSERT Categorias OFF;

SELECT * FROM Categorias WHERE id IN (1,2,3);

-- Productos --

DELETE FROM Productos WHERE id BETWEEN 1 AND 25;

SET IDENTITY_INSERT Productos ON;

INSERT INTO Productos (id, nombre, precio, categoriaId, imagenUrl, Stock) VALUES 
(1,  N'Bayaspirina', 5728, 1, N'https://www.anikashop.com.ar/product_images/w/994/8024587__72227_zoom.jpg', 100),
(2,  N'Ibu400', 15000, 1, N'https://www.farmacialeloir.com.ar/img/articulos/2025/06/imagen1_ibu_400_rapida_accion_ibuprofeno_400mg_imagen1.jpg', 100),
(3,  N'Shampoo Pantene', 20000, 2, N'https://www.casaflorian.com.ar/wp-content/uploads/2023/03/391-525-01_C.jpg', 100),
(4,  N'Jabón Rexona', 5000, 3, N'https://industriaslitoral.com.ar/wp-content/uploads/2022/05/3011150_f.jpg', 100),
(5,  N'Paracetamol 500mg', 4300, 1, N'https://www.farmaciassanchezantoniolli.com.ar/10123-medium_default/tafirol-x30-comp.jpg', 100),
(6,  N'Alcohol en gel', 2900, 3, N'https://farmacityar.vtexassets.com/arquivos/ids/207795/220120_alcohol-en-gel-bialcohol-con-glicerina-x-250-ml_imagen-1.jpg?v=637497071230100000', 100),
(7,  N'Cepillo Dental Oral-B', 2200, 3, N'https://jumboargentina.vtexassets.com/arquivos/ids/768123/Cepillo-Dental-Oral-b-Complete-1-Un-1-223926.jpg?v=638114674058130000', 100),
(8,  N'Toallitas Húmedas Pampers', 7800, 3, N'https://www.masfarmacias.com/wp-content/uploads/7500435148443.jpg', 100),
(9,  N'Perfume Hugo Boss', 45200, 2, N'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcR-oxxY1Y9TdkG8WTow2jN6IedoE1mp_ZCMBg&s', 100),
(10, N'Desodorante Dove', 6300, 2, N'https://farmaciadelpuebloar.vtexassets.com/arquivos/ids/166590/desodorante-dove-men-care.png?v=638163070782970000', 100),
(11, N'Té de Hierbas Relax', 3600, 1, N'https://images.precialo.com/products/te-en-saquitos-green-hills-blend-relax-x-20-saquitos/3d1dbd48-bcf7-4b67-82e3-e93ca551527d.jpeg', 100),
(12, N'Crema Nivea', 5400, 2, N'https://getthelookar.vtexassets.com/arquivos/ids/180043-800-auto?v=638484443678830000&width=800&height=auto&aspect=true', 100),
(13, N'Algodón Estéril 100g', 2100, 3, N'https://jumboargentina.vtexassets.com/arquivos/ids/178407-800-600?v=636383362696400000&width=800&height=600&aspect=true', 100),
(14, N'Jarabe para la Tos', 8700, 1, N'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTusKNNE2NSzobZCKl7bKeECu4bX403oEezKg&s', 100),
(15, N'Pasta de dientes Colgate', 3900, 3, N'https://www.uomax.com.ar/16692-large_default/pasta-dental-colgate-ultra-white-x-90gr.jpg', 150),
(16, N'Caja Aspirinetas 10 mg - 28 comprimidos', 1700, 1, N'https://www.aspirinetas.com.ar/sites/g/files/vrxlpx49531/files/2021-04/header_packshot_1.png', 75),
(17, N'Caja de Curitas x 10u', 3338, 3, N'https://images-us.eucerin.com/~/media/hansaplast/media-center-items/f/a/8/ffc6efe9ec034a909286c8c026370f1f-screen.jpg', 66),
(18, N'Corrector de Ojeras Maybelline x 6ml', 29875, 3, N'https://farmacityar.vtexassets.com/arquivos/ids/272253-800-auto?v=638840314334200000&width=800&height=auto&aspect=true', 18),
(19, N'Jabón de Tocador Dove', 2000, 3, N'https://farmacityar.vtexassets.com/arquivos/ids/203904-800-auto?v=637377678118300000&width=800&height=auto&aspect=true', 12),
(20, N'Aziatop Omeprazol 20 mg x 14 Cáps', 3500, 1, N'https://farmacityar.vtexassets.com/arquivos/ids/203904-800-auto?v=637377678118300000&width=800&height=auto&aspect=true', 24),
(21, N'Pervinox Solución Tópica Iodo Povidona x 60 ml', 1100, 1, N'https://farmacityar.vtexassets.com/arquivos/ids/234096-800-auto?v=638085319301000000&width=800&height=auto&aspect=true', 9),
(22, N'Espadol Líquido Antiséptico x 250 ml', 13250, 1, N'https://farmacityar.vtexassets.com/arquivos/ids/234094-800-auto?v=638085319290200000&width=800&height=auto&aspect=true', 3),
(23, N'Sertal x 10 Comp', 4235, 1, N'https://farmacityar.vtexassets.com/arquivos/ids/260662-800-auto?v=638629740290000000&width=800&height=auto&aspect=true', 10),
(24, N'Dulcolax Laxante x 20 grageas', 21695, 1, N'https://farmacityar.vtexassets.com/arquivos/ids/233598-800-auto?v=638077688183070000&width=800&height=auto&aspect=true', 1),
(25, N'Isdin Fusion Water Magic fotoprotector solar Fps50 50ml', 31125, 3, N'https://pedidosfarma.vtexassets.com/arquivos/ids/220312-1600-1600?v=638829088436800000&width=1600&height=1600&aspect=true', 66);


SET IDENTITY_INSERT Productos OFF;
SELECT * FROM Productos ORDER BY id;

-- FIN DEL SCRIPT --