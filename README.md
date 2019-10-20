> A work in progress

# Proyecto final módulo 1 (C# y BBDD)

**BBDD**:
--

Crear la BBDD de un Video-club, para ello tendréis que hacer el diagrama de E/R, el cual tendrá que ser validado por los instructores, teniendo en cuenta que:

• La BBDD almacenará películas de las cuales se quiere saber el título, Sinopsis, edad recomendada y estado.

• La BBDD almacenará Usuarios de la aplicación de los cuales se quiere almacenar: el nombre, apellido, fecha de nacimiento, email y contraseña. Los Usuarios podrán alquilar películas.

Una vez validado el diseño crear la BBDD en el SSMS (Sql Server Management Studio).

**Aplicación de Consola:**
--

Crear un programa de consola basado en un videoclub el cual tendrá las clases correspondientes a las tablas de la BBDD. Los usuarios para acceder se tendrán que loguear utilizando la BBDD. Una vez logueados, se les desplegará un menú con las siguientes opciones:

• Ver películas disponibles: Aquí tendremos que desplegar una lista de películas teniendo en cuenta la edad del usuario, si tiene menos de 18 años le aparecerán las películas recomendadas para esa edad, si tiene menos de 16, las recomendadas para menores de 16, etc.

Aparecerán todas las películas de la BBDD incluidas las que están alquiladas. Si el usuario selecciona una de ellas, se le mostrará todos los datos referentes a esa película.

• Alquilar película: aquí podrán alquilar la película que deseen siempre que estén disponibles y la edad recomendada sea la adecuada. Una vez alquilada, la película deberá pasar a modo no disponible.

• Mis alquileres: aquí podrán ver las películas que tienen alquiladas y cuando vence el plazo de alquiler. Cuando el plazo haya expirado, les tendrá que aparecer la película en color rojo. Podrán entregar la película que hayan alquilado, la cual se volverá a poner en modo disponible.

• Logout.

**Requisitos mínimos del proyecto modular:**
--

• Hacer uso de las buenas prácticas en la programación.

• Diseñar diagrama de BBDD

• Crear la BBDD

• Tratar las excepciones que puedan ocurrir.

• Loguearse

• Crear las clases correspondientes las cuales coinciden con las tablas creadas en la BBDD.

• Hacer uso correcto de la estructuras de control (condicionales, bucles...).

• Conseguir ver las películas consultando a la BBDD usando un Select y guardarlas en una lista de objetos Película.

• Conseguir ver las películas consultando a la BBDD teniendo en cuenta la edad del usuario mediante la cláusula where.

Requisitos deseables del proyecto modular:
--

> **TODO (20/10/2019):**

• Conseguir alquilar una película y cambiarle el estado realizando un update a la BBDD. 


• Tratar todas las excepciones que puedan ocurrir de tal forma que el programa no tenga que dejar de ejecutarse.

• Almacenar todos los registros que devuelve una consulta Select en objetos.

• Modificar o insertar datos de la BBDD utilizando objetos y sus atributos.
