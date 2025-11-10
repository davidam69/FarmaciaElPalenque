-- SCRIPT UNIFICADO DE LA BASE DE DATOS --


-- USUARIOS  -- 
INSERT INTO dbo.Usuarios (nombre, apellido, email, passwordHash, rol)
SELECT nombre, apellido, email, passwordHash, rol
FROM (
    VALUES
    ('Raul Ricardo', 'Alfonsin', 'raul.ricardo.alfonsin@palenque.com', 'raulito23', 'Administrador'),
    ('Mariano', 'Alderete', 'marianalderete@hotmail.com', 'Camionero23', 'Cliente'),
    ('Gustavo', 'Aguila', 'gustavoaguila23@outlook.com.ar', 'Chocolate23', 'Cliente'),
    ('Sofia', 'Martinez', 'sofia.martinez@gmail.com', 'pass123', 'Cliente'),
    ('Lucia', 'Rojas', 'lucia.rojas@gmail.com', 'clave123', 'Cliente'),
    ('Mateo', 'Ocampo', 'mateo.ocampo@hotmail.com', '1234', 'Cliente'),
    ('Nicolas', 'Gomez', 'nico.gomez@outlook.com', 'abcd1234', 'Cliente'),
    ('Valentina', 'Coronel', 'valen.coronel@yahoo.com', 'qwerty', 'Cliente'),
    ('Bruno', 'Sanchez', 'bruno.sanchez@outlook.com.ar', 'asd123', 'Cliente'),
    ('Manuela', 'Vega', 'manuela.vega@gmail.com', '1111', 'Cliente'),
    ('Dario', 'Silva', 'dario.silva@yahoo.com', 'abc123', 'Cliente'),
    ('Martina', 'Peralta', 'martina.peralta@gmail.com', 'pass22', 'Cliente'),
    ('Ramiro', 'Peña', 'ramiro.pena@outlook.com', 'clave22', 'Cliente'),
    ('Luciano', 'Mendez', 'luciano.mendez@outlook.com.ar', 'test11', 'Cliente'),
    ('Paula', 'Rios', 'paula.rios@gmail.com', 'hola123', 'Cliente'),
    ('Franco', 'Serrano', 'franco.serrano@yahoo.com', 'serrano22', 'Cliente'),
    ('Julieta', 'Aguilar', 'juli.aguilar@hotmail.com', 'pass44', 'Cliente'),
    ('Sebastian', 'Toledo', 'sebastian.toledo@gmail.com', 'clave44', 'Cliente'),
    ('Belen', 'Ruiz', 'belen.ruiz@outlook.com.ar', 'contrasena1', 'Cliente'),
    ('Alejo', 'Navarro', 'alejo.navarro@gmail.com', '321321', 'Cliente'),
    ('Facundo', 'Arce', 'facu.arce@yahoo.com', 'password1', 'Cliente'),
    ('Candela', 'Moreno', 'candela.moreno@gmail.com', 'moreno11', 'Cliente'),
    ('Melina', 'Torres', 'melina.torres@outlook.com', 'torres22', 'Cliente'),
    ('Tomas', 'Fernandez', 'tomas.fernandez@hotmail.com', 'abc987', 'Cliente'),
    ('Camila', 'Ibarra', 'camila.ibarra@gmail.com', 'pepe123', 'Cliente'),
    ('Benjamin', 'Villalba', 'benja.villalba@yahoo.com', 'villalba22', 'Cliente'),
    ('Rocio', 'Salas', 'rocio.salas@gmail.com', '123aaa', 'Cliente'),
    ('Emanuel', 'Sosa', 'emanuel.sosa@outlook.com.ar', 'sosa55', 'Cliente'),
    ('Agustina', 'Molina', 'agustina.molina@gmail.com', 'moli123', 'Cliente'),
    ('Gonzalo', 'Rivero', 'gonzalo.rivero@hotmail.com', 'rivero12', 'Cliente'),
    ('Daniela', 'Escobar', 'daniela.escobar@gmail.com', 'test55', 'Cliente'),
    ('Iara', 'Paredes', 'iara.paredes@yahoo.com', 'paredes77', 'Cliente'),
    ('Leandro', 'Dominguez', 'leandro.dominguez@outlook.com', 'domi88', 'Cliente'),
    ('Agustin', 'Cano', 'agustin.cano@gmail.com', 'cano12', 'Cliente'),
    ('Victoria', 'Lopez', 'victoria.lopez@yahoo.com', 'vicky33', 'Cliente'),
    ('Ezequiel', 'Acosta', 'ezequiel.acosta@outlook.com.ar', 'acosta44', 'Cliente'),
    ('Milagros', 'Herrera', 'milagros.herrera@gmail.com', 'herra123', 'Cliente'),
    ('Julian', 'Maidana', 'julian.maidana@hotmail.com', 'mai77', 'Cliente'),
    ('Florencia', 'Guzman', 'flor.guzman@gmail.com', 'guzman21', 'Cliente'),
    ('Rodrigo', 'Ponce', 'rodrigo.ponce@yahoo.com', 'ponce98', 'Cliente'),
    ('Celeste', 'Maidana', 'celeste.maidana@outlook.com', 'celes55', 'Cliente'),
    ('Ariana', 'Vidal', 'ariana.vidal@gmail.com', 'vidal88', 'Cliente'),
    ('Gabriel', 'Soto', 'gabriel.soto@hotmail.com', 'soto123', 'Cliente')
) AS nuevos(nombre, apellido, email, passwordHash, rol)
WHERE NOT EXISTS (
    SELECT 1 FROM dbo.Usuarios WHERE email = nuevos.email
);


-- PRODUCTOS --
-- Insertar nuevos productos en dbo.Productos (continuando desde ID 16)
INSERT INTO dbo.Productos (nombre, precio, categoriald, imagenUrl, Stock)
VALUES
(N'Gillette Mach3 Carbono Repuestos De La Máquina De Afeitar 4 Unidades', CAST(18491.00 AS Decimal(18, 2)), 3, N'https://pedidosfarma.vtexassets.com/arquivos/ids/210335-219-219?v=638585455278000000&width=219&height=219&aspect=true', 48),
(N'Paracetamol Tafirol Forte 650 mg x 70 capsulas', CAST(13000.00 AS Decimal(18, 2)), 1, N'https://tafirol.com/hubfs/Group%201000005914.png', 119),
(N'Paracetamol ISA 1 g x 24 comprimidos', CAST(5700.00 AS Decimal(18, 2)), 1, N'https://www.centraloeste.com.ar/media/catalog/product/cache/ddb703dc084a33c8cde56cf551816b8e/7/7/7790839981085.png', 110),
(N'Actron Ibuprofeno 400 mg x 20 Cápsulas', CAST(5600.00 AS Decimal(18, 2)), 1, N'https://farmacityar.vtexassets.com/arquivos/ids/263358/25940_ibuprofeno-actron-x-20-capsulas-blandas_imagen--1.jpg?v=638717930111300000', 140),
(N'Ibu 600 Ibuprofeno 600mg x 30 comprimidos', CAST(10250.00 AS Decimal(18, 2)), 1, N'https://www.farmaciasdrahorro.com.ar/wp-content/uploads/2020/11/Ibuprofeno-600-mg-x30-IBU-600-MG-X-30-COMP.png', 100),
(N'Amoxidal 500 mg  Comprimidos compuestos x16 unidades', CAST(8400.00 AS Decimal(18, 2)), 1, N'https://www.roemmers.com.ar/sites/default/files/F_000001106329.png', 80),
(N'aziatop 20 mg Omeprazol capsulas x 14 unidades', CAST(3600.00 AS Decimal(18, 2)), 1, N'https://fcityrepoimagenes.farmacity.net/imagenes/Medicamentos/Imagenes/7796285051792_1.jpg', 90),
(N'Corticas Loratadina+Betametasona Comprimidos x 10 ', CAST(8500.00 AS Decimal(18, 2)), 1, N'https://fcityrepoimagenes.farmacity.net/imagenes/Medicamentos/Imagenes/7791909432704_1.jpg', 130),
(N'Aseptobron Antigripal Forte Comp.rec.x 10', CAST(7400.00 AS Decimal(18, 2)), 1, N'https://fcityrepoimagenes.farmacity.net/imagenes/Medicamentos/Imagenes/7795337907308_1.jpg', 150),
(N'Total Magnesiano Vitamina C polvo efervescente sobres x 30', CAST(44162.00 AS Decimal(18, 2)), 1, N'https://fcityrepoimagenes.farmacity.net/imagenes/Medicamentos/Imagenes/7795337900392_1.jpg', 160),
(N'Bago Vitamina b1 b6 b12 sol.x 100 ml', CAST(23500.00 AS Decimal(18, 2)), 1, N'https://fcityrepoimagenes.farmacity.net/imagenes/Medicamentos/Imagenes/7790375245375_1.jpg', 75),
(N'Calcional calcio,citrato+vit.d3 Comprimidos x 30 ', CAST(8999.00 AS Decimal(18, 2)), 1, N'https://fcityrepoimagenes.farmacity.net/imagenes/Medicamentos/Imagenes/7795366452916_1.jpg', 65),
(N'Vitalix hierro,polimaltosato Gotas x 20 ml ', CAST(8100.00 AS Decimal(18, 2)), 1, N'https://fcityrepoimagenes.farmacity.net/imagenes/Medicamentos/Imagenes/7795345012544_1.jpg', 84),
(N'Rodinac 50 diclofenac potásico Comprimidos Recubiertos x 15 ', CAST(6845.00 AS Decimal(18, 2)), 1, N'https://fcityrepoimagenes.farmacity.net/imagenes/Medicamentos/Imagenes/7798113708014_1.jpg', 120),
(N'irix colirio x 15 ml', CAST(9209.00 AS Decimal(18, 2)), 1, N'https://fcityrepoimagenes.farmacity.net/imagenes/Medicamentos/Imagenes/7792819000595_1.jpg', 70),
(N'pulmosan forte jarabe x 120 ml', CAST(14350.00 AS Decimal(18, 2)), 1, N'https://fcityrepoimagenes.farmacity.net/imagenes/Medicamentos/Imagenes/7792369023907_1.jpg', 95),
(N'Rennie Atiacido masticable x 36 comprimidos', CAST(13300.00 AS Decimal(18, 2)), 1, N'https://fcityrepoimagenes.farmacity.net/imagenes/Medicamentos/Imagenes/7793640117230_1.jpg', 110),
(N'Adermicina A crema x 30 g', CAST(12999.00 AS Decimal(18, 2)), 1, N'https://fcityrepoimagenes.farmacity.net/imagenes/Medicamentos/Imagenes/7792819001103_1.jpg', 60),
(N'Bengue Desinflamante x 150 g', CAST(16300.00 AS Decimal(18, 2)), 1, N'https://fcityrepoimagenes.farmacity.net/imagenes/Medicamentos/Imagenes/7798140258605_1.jpg', 55),
(N'Shampoo Elvive Hidra Hialurónico x 400 ml', CAST(4899.00 AS Decimal(18, 2)), 2, N'https://farmacityar.vtexassets.com/arquivos/ids/251063-800-auto?v=638399087324970000&width=800&height=auto&aspect=true', 130),
(N'Acondicionador Dove Oleo Nutrición x200ml ', CAST(3579.00 AS Decimal(18, 2)), 2, N'https://www.siemprefarmacias.com.ar/contenido/productos/original/1620134432-4970.jpeg', 125),
(N'Shampoo Head & Shoulders Limpieza y Revitalización x 375 ml', CAST(5999.00 AS Decimal(18, 2)), 2, N'https://farmacityar.vtexassets.com/arquivos/ids/254630-800-auto?v=638495678403830000&width=800&height=auto&aspect=true', 115),
(N'Gel Fijador Classic Lord Cheseline Pote x 280 g', CAST(5999.00 AS Decimal(18, 2)), 2, N'https://farmacityar.vtexassets.com/arquivos/ids/253748-800-auto?v=638478521805170000&width=800&height=auto&aspect=true', 150),
(N'Crema Corporal Dermaglós Hidratación Inmediata x 300 gr', CAST(12999.00 AS Decimal(18, 2)), 2, N'https://farmacityar.vtexassets.com/arquivos/ids/281731-800-auto?v=638937262069100000&width=800&height=auto&aspect=true', 90),
(N'Loción Corporal Nivea Cereza Y Aceite De Jojoba 400 Ml', CAST(11899.00 AS Decimal(18, 2)), 2, N'https://www.siemprefarmacias.com.ar/contenido/productos/original/2024/09/12/1726164480-3743.webp', 110),
(N' Shakira Dance x 80 ml', CAST(32999.00 AS Decimal(18, 2)), 2, N'https://farmacityar.vtexassets.com/arquivos/ids/157125-800-auto?v=636670308052730000&width=800&height=auto&aspect=true', 40),
(N'Sens Natural Emotions Nerolí Lima x 100 ml', CAST(17999.00 AS Decimal(18, 2)), 2, N'https://farmacityar.vtexassets.com/arquivos/ids/227865-800-auto?v=637979852402300000&width=800&height=auto&aspect=true', 45),
(N'Desodorante AXE Aqua Citrus en Aerosol x 150 ml', CAST(3199.00 AS Decimal(18, 2)), 2, N'https://farmacityar.vtexassets.com/arquivos/ids/254710-800-auto?v=638496682103870000&width=800&height=auto&aspect=true', 160),
(N'Desodorante Antitranspirante Gillette Specialized Artic Ice x 82 g', CAST(6299.00 AS Decimal(18, 2)), 2, N'https://farmacityar.vtexassets.com/arquivos/ids/282331-800-auto?v=638944068452600000&width=800&height=auto&aspect=true', 150),
(N'Crema Para Manos Atrix Protección Iintensiva x150ml ', CAST(8599.00 AS Decimal(18, 2)), 2, N'https://www.siemprefarmacias.com.ar/contenido/productos/original/1662822903-9951.jpeg', 140),
(N'Jabón Líquido Protex Antibacterial Pro Hidratación x 230 ml', CAST(2299.00 AS Decimal(18, 2)), 2, N'https://farmacityar.vtexassets.com/arquivos/ids/225068-800-auto?v=637926239775530000&width=800&height=auto&aspect=true', 150),
(N'Aceite Corporal Home Spa Amour x 110 ml', CAST(3199.00 AS Decimal(18, 2)), 2, N'https://farmacityar.vtexassets.com/arquivos/ids/214668-800-auto?v=637666348872930000&width=800&height=auto&aspect=true', 70),
(N'Tónico Facial Anua Heartleaf 77 + Hyaluron Soothing x 250 ml', CAST(45999.00 AS Decimal(18, 2)), 2, N'https://farmacityar.vtexassets.com/arquivos/ids/285319-800-auto?v=638974338491000000&width=800&height=auto&aspect=true', 85),
(N'Crema Facial Vichy Liftactiv Noche Para Piel Sensible 50 Ml ', CAST(93999.00 AS Decimal(18, 2)), 2, N'https://www.siemprefarmacias.com.ar/contenido/productos/original/1645816580-6429.jpeg', 60),
(N'Pasta Dental Fluorogel Original Sabor Menta x 60 g ', CAST(8499.00 AS Decimal(18, 2)), 3, N'https://farmacityar.vtexassets.com/arquivos/ids/273037-800-auto?v=638842260340800000&width=800&height=auto&aspect=true', 180),
(N'Antonio Banderas The Secret x 100 ml', CAST(34099.00 AS Decimal(18, 2)), 2, N'https://farmacityar.vtexassets.com/arquivos/ids/175048-800-auto?v=636673674487070000&width=800&height=auto&aspect=true', 200),
(N'Enjuague Bucal Colgate Ice x500ml ', CAST(8199.00 AS Decimal(18, 2)), 3, N'https://www.siemprefarmacias.com.ar/contenido/productos/original/1706032743-1496.jpeg', 150),
(N'Hilo Dental Colgate Nylon x 50 m', CAST(3699.00 AS Decimal(18, 2)), 3, N'https://farmacityar.vtexassets.com/arquivos/ids/260684-800-auto?v=638630398824670000&width=800&height=auto&aspect=true', 140),
(N'Protector Solar Dermaglós Emulsión Fps 50 x 380 ml', CAST(20499.00 AS Decimal(18, 2)), 3, N'https://farmacityar.vtexassets.com/arquivos/ids/251575-800-auto?v=638418004207200000&width=800&height=auto&aspect=true', 65),
(N'Jabón de Tocador Espadol Dettol Original Antibacterial x 80 g x 3 unidades', CAST(2999.00 AS Decimal(18, 2)), 3, N'https://farmacityar.vtexassets.com/arquivos/ids/209500-800-auto?v=637553047600630000&width=800&height=auto&aspect=true', 170),
(N'Acf Gel De Ducha Petals Fresias & Jazmines X 250Ml ', CAST(8999.00 AS Decimal(18, 2)), 3, N'https://www.siemprefarmacias.com.ar/contenido/productos/original/1703608749-5026.jpeg', 140),
(N'Estuche Kevin Edt 60 Ml + Desodorante 150 M', CAST(24999.00 AS Decimal(18, 2)), 3, N'https://www.siemprefarmacias.com.ar/contenido/productos/original/2024/07/23/1721758984.webp', 75),
(N'Crema Depilatoria Veet Shower Sensitive x150mm', CAST(10099.00 AS Decimal(18, 2)), 3, N'https://www.siemprefarmacias.com.ar/contenido/productos/original/1671024523-1678.jpeg', 90),
(N'Discos de Algodón Estrella x 80 unidades', CAST(2899.00 AS Decimal(18, 2)), 3, N'https://farmacityar.vtexassets.com/arquivos/ids/164515-800-auto?v=636670331973070000&width=800&height=auto&aspect=true', 200),
(N'Hisopos Cotonetes Johnson & Johnson Flexibles x 150 unidades', CAST(5199.00 AS Decimal(18, 2)), 3, N'https://farmacityar.vtexassets.com/arquivos/ids/164481-800-auto?v=636670331755070000&width=800&height=auto&aspect=true', 210),
(N'Crema Corporal Bagovit Efecto Seda x350ml ', CAST(13899.00 AS Decimal(18, 2)), 3, N'https://www.siemprefarmacias.com.ar/contenido/productos/original/1627135182-2053.jpeg', 95),
(N'Protector Labial Humectante Nivea Cherry Shine x 4,8 g', CAST(5599.00 AS Decimal(18, 2)), 3, N'https://farmacityar.vtexassets.com/arquivos/ids/251621-800-auto?v=638422186196570000&width=800&height=auto&aspect=true', 180),
(N'Crema Facial Nivea Dia Fps30 Cellular Antiage x50ml', CAST(23499.00 AS Decimal(18, 2)), 3, N'https://www.siemprefarmacias.com.ar/contenido/productos/original/1663946298-1627.jpeg', 50),
(N'jabon de tocador Dove Piel Sensible 3x90 Grs X 3 Unidades', CAST(5600.00 AS Decimal(18, 2)), 3, N'https://www.centraloeste.com.ar/media/catalog/product/cache/ddb703dc084a33c8cde56cf551816b8e/7/8/7891150095700.h_1.jpg', 300);


-- PEDIDOS --
BEGIN TRANSACTION;

BEGIN TRY
    -- PASO 1: Insertar Pedidos (sin IDs específicos)
    INSERT INTO [dbo].[Pedidos] ([numero], [fecha], [usuarioId], [total])
    SELECT [numero], [fecha], [usuarioId], [total]
    FROM (
        VALUES
        (N'F-20251006-223736', CAST(N'2025-10-06T22:37:36.2038685' AS DateTime2), 6, CAST(5728.00 AS Decimal(18, 2))),
        (N'F-20251007-141157', CAST(N'2025-10-07T14:11:57.4628854' AS DateTime2), 6, CAST(23600.00 AS Decimal(18, 2))),
        (N'F-20251007-171643', CAST(N'2025-10-07T17:16:43.8831749' AS DateTime2), 6, CAST(2900.00 AS Decimal(18, 2))),
        (N'F-20251007-172211', CAST(N'2025-10-07T17:22:11.3278328' AS DateTime2), 4, CAST(18491.00 AS Decimal(18, 2))),
        (N'F-20251028-003026', CAST(N'2025-10-28T00:30:26.6679528' AS DateTime2), 6, CAST(15000.00 AS Decimal(18, 2))),
        (N'F-20251028-212522', CAST(N'2025-10-28T21:25:22.6009563' AS DateTime2), 6, CAST(18491.00 AS Decimal(18, 2))),
        (N'F-20251028-214550', CAST(N'2025-10-28T21:45:50.0253815' AS DateTime2), 19, CAST(45200.00 AS Decimal(18, 2))),
        (N'F-20251028-221935', CAST(N'2025-10-28T22:19:35.0483494' AS DateTime2), 3, CAST(13000.00 AS Decimal(18, 2))),
        (N'F-20251028-222659', CAST(N'2025-10-28T22:26:59.1192061' AS DateTime2), 6, CAST(20000.00 AS Decimal(18, 2))),
        (N'F-20251029-014532', CAST(N'2025-10-29T01:45:32.9082440' AS DateTime2), 6, CAST(15000.00 AS Decimal(18, 2))),
        (N'F-20251030-014019', CAST(N'2025-10-30T01:40:19.6113113' AS DateTime2), 6, CAST(2200.00 AS Decimal(18, 2))),
        (N'F-20251101-165501', CAST(N'2025-11-01T16:55:01.9067340' AS DateTime2), 6, CAST(57280.00 AS Decimal(18, 2))),
        (N'F-20250305-004041', CAST(N'2025-03-05T00:40:41.0731912' AS DateTime2), 8, CAST(2200.00 AS Decimal(18, 2))),
        (N'F-20250305-004337', CAST(N'2025-03-05T00:43:37.2467256' AS DateTime2), 18, CAST(2200.00 AS Decimal(18, 2))),
        (N'F-20251105-004907', CAST(N'2025-11-05T00:49:07.8792290' AS DateTime2), 6, CAST(8099.00 AS Decimal(18, 2))),
        (N'F-20251106-204943', CAST(N'2025-11-06T20:49:43.0876501' AS DateTime2), 9, CAST(5999.00 AS Decimal(18, 2))),
        (N'F-20251108-184320', CAST(N'2025-11-08T18:43:20.7419828' AS DateTime2), 6, CAST(7600.00 AS Decimal(18, 2))),
        (N'F-20251108-221229', CAST(N'2025-11-08T22:12:29.2106916' AS DateTime2), 6, CAST(3600.00 AS Decimal(18, 2))),
        (N'F-20251109-015614', CAST(N'2025-11-09T01:56:14.3896565' AS DateTime2), 6, CAST(24591.00 AS Decimal(18, 2)))
    ) AS nuevos([numero], [fecha], [usuarioId], [total])
    WHERE NOT EXISTS (
        SELECT 1 FROM [dbo].[Pedidos] WHERE [numero] = nuevos.[numero]
    );

    PRINT 'Paso 1 completado: Pedidos insertados';

    -- PASO 2: Crear Tabla Temporal MapeoPedidos de IDs
    CREATE TABLE #MapeoPedidos (
        numero VARCHAR(50),
        idViejo INT,
        idNuevo INT
    );

    INSERT INTO #MapeoPedidos (numero, idViejo, idNuevo)
    SELECT 
        p.numero,
        CASE p.numero
            WHEN 'F-20251006-223736' THEN 11
            WHEN 'F-20251007-141157' THEN 12
            WHEN 'F-20251007-171643' THEN 13
            WHEN 'F-20251007-172211' THEN 14
            WHEN 'F-20251028-003026' THEN 15
            WHEN 'F-20251028-212522' THEN 16
            WHEN 'F-20251028-214550' THEN 17
            WHEN 'F-20251028-221935' THEN 18
            WHEN 'F-20251028-222659' THEN 19
            WHEN 'F-20251029-014532' THEN 20
            WHEN 'F-20251030-014019' THEN 21
            WHEN 'F-20251101-165501' THEN 22
            WHEN 'F-20250305-004041' THEN 23
            WHEN 'F-20250305-004337' THEN 24
            WHEN 'F-20251105-004907' THEN 25
            WHEN 'F-20251106-204943' THEN 26
            WHEN 'F-20251108-184320' THEN 27
            WHEN 'F-20251108-221229' THEN 28
            WHEN 'F-20251109-015614' THEN 29
        END as idViejo,
        p.id as idNuevo
    FROM [dbo].[Pedidos] p
    WHERE p.numero IN (
        'F-20251006-223736', 'F-20251007-141157', 'F-20251007-171643', 'F-20251007-172211',
        'F-20251028-003026', 'F-20251028-212522', 'F-20251028-214550', 'F-20251028-221935',
        'F-20251028-222659', 'F-20251029-014532', 'F-20251030-014019', 'F-20251101-165501',
        'F-20250305-004041', 'F-20250305-004337', 'F-20251105-004907', 'F-20251106-204943',
        'F-20251108-184320', 'F-20251108-221229', 'F-20251109-015614'
    );
    PRINT 'Paso 2 completado: MapeoPedidos creado';
	

-- PEDIDOS DETALLES - 
    -- PASO 3: Insertar PedidoDetalles
    INSERT INTO [dbo].[PedidoDetalles] ([pedidoId], [productoId], [nombre], [precioUnitario], [cantidad])
    SELECT 
        m.idNuevo as pedidoId,
        detalle.productoId,
        detalle.nombre,
        detalle.precioUnitario,
        detalle.cantidad
    FROM (
        VALUES
        (11, 1, N'Bayaspirina', CAST(5728.00 AS Decimal(18, 2)), 1),
        (12, 11, N'Té de Hierbas Relax', CAST(3600.00 AS Decimal(18, 2)), 1),
        (12, 3, N'Shampoo Pantene', CAST(20000.00 AS Decimal(18, 2)), 1),
        (13, 6, N'Alcohol en gel', CAST(2900.00 AS Decimal(18, 2)), 1),
        (14, 18, N'Gillette Mach3 Carbono Repuestos De La Máquina De Afeitar 4 Unidades', CAST(18491.00 AS Decimal(18, 2)), 1),
        (15, 2, N'Ibu400', CAST(15000.00 AS Decimal(18, 2)), 1),
        (16, 18, N'Gillette Mach3 Carbono Repuestos De La Máquina De Afeitar 4 Unidades', CAST(18491.00 AS Decimal(18, 2)), 1),
        (17, 9, N'Perfume Hugo Boss', CAST(45200.00 AS Decimal(18, 2)), 1),
        (18, 19, N'Paracetamol Tafirol Forte 650 mg x 70 capsulas', CAST(13000.00 AS Decimal(18, 2)), 1),
        (19, 3, N'Shampoo Pantene', CAST(20000.00 AS Decimal(18, 2)), 1),
        (20, 2, N'Ibu400', CAST(15000.00 AS Decimal(18, 2)), 1),
        (21, 7, N'Cepillo Dental Oral-B', CAST(2200.00 AS Decimal(18, 2)), 1),
        (22, 1, N'Bayaspirina', CAST(5728.00 AS Decimal(18, 2)), 10),
        (23, 7, N'Cepillo Dental Oral-B', CAST(2200.00 AS Decimal(18, 2)), 1),
        (24, 7, N'Cepillo Dental Oral-B', CAST(2200.00 AS Decimal(18, 2)), 1),
        (25, 11, N'Té de Hierbas Relax', CAST(3600.00 AS Decimal(18, 2)), 1),
        (25, 30, N'Hierro x30', CAST(4499.00 AS Decimal(18, 2)), 1),
        (26, 39, N'Shampoo Head & Shoulders Limpieza y Revitalización x 375 ml', CAST(5999.00 AS Decimal(18, 2)), 1),
        (27, 8, N'Pampers Toallitas Húmedas Aroma Naturaleza 48 Unidades', CAST(7600.00 AS Decimal(18, 2)), 1),
        (28, 11, N'Té de Hierbas Relax', CAST(3600.00 AS Decimal(18, 2)), 1),
        (29, 5, N'Paracetamol Tafirol 500mg x 30 comprimidos', CAST(6100.00 AS Decimal(18, 2)), 1),
        (29, 18, N'Gillette Mach3 Carbono Repuestos De La Máquina De Afeitar 4 Unidades', CAST(18491.00 AS Decimal(18, 2)), 1)
    ) AS detalle(pedidoIdViejo, productoId, nombre, precioUnitario, cantidad)
    INNER JOIN #MapeoPedidos m ON detalle.pedidoIdViejo = m.idViejo;

    PRINT 'Paso 3 completado: PedidoDetalles insertados';

    -- LIMPIAR Tabla temporal MapeoPedidos
    DROP TABLE #MapeoPedidos;

    -- CONFIRMAR TODO
    COMMIT TRANSACTION;
    PRINT ' TODAS LAS INSERCIONES COMPLETADAS EXITOSAMENTE';

END TRY
BEGIN CATCH
    -- REVERTIR EN CASO DE ERROR
    ROLLBACK TRANSACTION;
    
    PRINT '❌ ERROR: ' + ERROR_MESSAGE();
    PRINT ' Todos los cambios fueron revertidos automáticamente';
    
    -- Limpiar tabla temporal MapeoPedidos si existe
    IF OBJECT_ID('tempdb..#MapeoPedidos') IS NOT NULL
        DROP TABLE #MapeoPedidos;
END CATCH;


-- FIN DEL SCRIPT --
