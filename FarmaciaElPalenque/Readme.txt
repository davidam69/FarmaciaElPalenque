Instructuvo proyecto de mvc e IA
1. Creamos un proyecto en visual studio de tipo ASP.NET Core Web Application.(Modelo-Vista-Controlador)
2. Lo llamamos FarmaciaElPalenque.
3. En la pestaña GIT seleccionamos la opción de crear un repositorio local.
4. Creamos una rama cada miembro del equipo, basada en la rama main.
5. Creamos un archivo Readme.txt en la raíz del proyecto.
6. En la carpeta Models creamos un archivo llamado Categoria.cs y otro llamado Productos.cs.
7. En Categoria.cs creamos la clase Categoria con las propiedades id y nombre.
8. En Productos.cs creamos la clase Producto con las propiedades id, nombre, precio y categoriaId.
9. En la carpeta Controllers creamos un archivo llamado PrincipalController.cs.
10. En PrincipalController.cs creamos un controlador llamado PrincipalController que herede de Controller.
11. En PrincipalController.cs creamos un using para el espacio de nombres FarmaciaElPalenque.Models.
12. En PrincipalController.cs creamos un método llamado Index que retorne una vista.
13. en este metodo Index creamos una lista de productos y la pasamos a la vista.
14. En la carpeta Views creamos una carpeta llamada Principal.
15. En la carpeta Farmacia creamos un archivo llamado Index.cshtml.
16. En Index.cshtml creamos un título y una lista de productos que se muestre en la vista.
17. En la carpeta Views/Shared en el archivo llamado _Layout.cshtml cambiamos donde decia "Home" por "Principal".
18. lo mismo hacemos en el archivo Program.cs, cambiamos donde decia "Home" por "Principal".
19. una vez que anduvo bien hicimos un commit con el mensaje "primer commit".
20. Subimos la rama "master" al el repositorio remoto de Github.
21. Se agrego un metodo llamado "Detalle" en el controlador PrincipalController.cs que recibe un id de producto y retorna una vista con los detalles del producto."
22. En la carpeta Views/Principal creamos un archivo llamado Detalle.cshtml.
23. En Detalle.cshtml mostramos los detalles del producto seleccionado, incluyendo nombre, precio y categoría.
24. Se agrego en Productos.cs una propiedad llamada imagenUrl para almacenar la URL de la imagen del producto.
25. En PrincipalController.cs, se completaron las Urls de imagenes para cada producto, simulando portadas de medicamentos y perfumeria.
26. En Detalle.cshtml, se agregó un elemento <img> para mostrar la imagen del producto utilizando la propiedad imagenUrl.
27. En Index.cshtml, se agregó un elemento <img> para mostrar la imagen del producto en la lista de productos.
28. se creo el archivo estilos.css en la carpeta wwwroot/css y se agregaron estilos básicos para mejorar la apariencia de la aplicación.
29. en Index.cshtml y Detalle.cshtml se incluyó el archivo estilos.css para aplicar los estilos a las vistas.
30 En _layout.cshtml, se agrego @RenderSection("Styles", required: false) para permitir la inclusión de estilos específicos en las vistas.
