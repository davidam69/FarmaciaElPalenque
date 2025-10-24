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
30. En _layout.cshtml, se agrego @RenderSection("Styles", required: false) para permitir la inclusión de estilos específicos en las vistas.
31. Se modifico agregó en la carpeta models un archivo llamado Usuario.cs con las propiedades id, nombreCompleto, email, passwordHash y rol.
32. Tambien se agrego confirmarEmail y confirmarPasswordHash en Usuario.cs, se agrego un conjunto de atributos "Data Annotations" para validar las propiedades del modelo Usuario.
33. En la carpeta Controllers, se creó un archivo llamado CuentaController.cs con los metodos Registro, Login y Logout.
34. En CuentaController.cs, se implementó el método Registro que maneja el registro de nuevos usuarios, incluyendo la validación de datos y el almacenamiento seguro de contraseñas.
35. En CuentaController.cs, se implementó el método Login que maneja el inicio de sesión de usuarios, incluyendo la validación de credenciales y la autenticación.
36. En CuentaController.cs, se implementó el método Logout que maneja el cierre de sesión de usuarios.
37. En la carpeta Views se creo un directorio llamado Cuenta.
38. En la carpeta Views/Cuenta se crearon los archivos Registro.cshtml y Login.cshtml.
39. En Registro.cshtml, se creó un formulario para que los usuarios puedan registrarse, incluyendo campos para nombre completo, email, contraseña y confirmación de contraseña.
40. En Login.cshtml, se creó un formulario para que los usuarios puedan iniciar sesión, incluyendo campos para email y contraseña.
41. En _Layout.cshtml, se agregó un menú de navegación que incluye enlaces a las páginas de registro y login.
42. En _Layout.cshtml, se agregó un enlace de cierre de sesión que llama al método Logout del CuentaController.
43. En CuentaController.cs en el metodo Login se agrego un if para validar que usuario.nombreUsuario y usuario.rol no sean nulos antes de usarlos.
44. Instalación de Micerosoft.EntityFrameworkCore y configuración de la base de datos.
45. Instalación de Microsoft.EntityFrameworkCore.sqlserver.
46. Instalación de Microsoft.EntityFrameworkCore.Tools.
47. En la carpeta models se creo un archivo llamado appDbContext.cs que hereda de DbContext y contiene las propiedades DbSet<Usuario> Usuarios, DbSet<Categoria> Categorias y DbSet<Producto> Productos.
48. En models productos.cs se agrego la propiedad Categoria para establecer la relación entre Producto y Categoria y categoriaId sea una relación real de base de datos.
49. Configuración de la cadena de conexión en appsettings.json para conectar a la base de datos SQL Server.
50. En Program.cs se configuró el servicio de DbContext para usar la cadena de conexión definida en appsettings.json.
51. Creamos una migración inicial usando el comando `Add-Migration Inicial` en la consola del administrador de paquetes y luego Update-Database para aplicar la migración a la base de datos.
52. En CuentaController.cs, se modifico para usar el appDbContext para interactuar con la base de datos en lugar de una lista en memoria.
53. Tambien PrincipalController.cs se modifico para usar el appDbContext para obtener la lista de productos desde la base de datos.
54. en appDbContext.cs se agrego el método OnModelCreating para configurar las relaciones entre las entidades y establecer las restricciones de clave foránea.
55. Se elimino public static List<Usuario> listaUsuarios = new List<Usuario>(); de CuentaController.cs y se reemplazo por el uso de appDbContext para acceder a los usuarios en la base de datos.
56. Lo mismo se hizo con la lista de productos en PrincipalController.cs, eliminando la lista en memoria y utilizando appDbContext para acceder a los productos en la base de datos.
57. Se agrego semillas de ejemplos de datos en el método OnModelCreating de appDbContext.cs para crear Usuarios, categorías y productos iniciales en la base de datos al iniciar la aplicación.
58 Se ejecuto los comandos `Add-Migration Semillas` y `Update-Database` para aplicar las semillas a la base de datos.
59. Se agrego el campo Stock a la clase Producto en models/Productos.cs para representar la cantidad disponible de cada producto.
60. Se creo una nueva migracion llamada AgregarStockAProductos con el comando `Add-Migration AgregarStockAProductos` y se aplico a la base de datos con `Update-Database`.
61. Desde sql server management studio se agregaron 100 productos de ejemplo con stock a la base de datos.
62. Se borraron por error las migraciones y se volvio a crear la base de datos desde cero con el comando `Update-Database -Force` pero Stock nos marcaba error en la base de datos.
63. se soluciono el error de stock con Ctrl + Shift + R en Visual Studio para actualizar el esquema de la base de datos.

