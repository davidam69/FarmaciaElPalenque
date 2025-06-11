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
14. En la carpeta Views creamos una carpeta llamada Farmacia.
15. En la carpeta Farmacia creamos un archivo llamado Index.cshtml.