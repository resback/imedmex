# 📌 Documentación Back End – Sistema TO-DO

## 🌐 URL Base  
```
https://localhost:7170/api
```

## 📂 Estructura de Carpetas  
- **Common/** → Clases comunes (respuestas estándar, manejo de errores, configuración JWT).  
- **Domain/**  
  - **Entities/** → Entidades principales (`Usuario`, `Tarea`).  
  - **Enums/** → Enumeraciones (`Prioridad`).  
- **DTOs/**  
  - **Auth/** → Modelos para login y respuesta del token.  
  - **Tareas/** → Modelos para crear, editar y listar tareas.  
- **Infrastructure/**  
  - **Data/** → `AppDbContext` y conexión a base de datos.  
- **Services/**  
  - **Interfaces/** → Definición de servicios (`IJwtService`, `ITareaService`).  
  - **Implementations/** → Implementación de la lógica de negocio.  
- **Controllers/** → Endpoints expuestos (`AuthController`, `TareasController`).  

## 📑 Endpoints  

### 🔑 Autenticación  
- **POST** `/auth/login`  
  Inicia sesión con usuario de prueba y devuelve un token JWT.  

### ✅ Tareas  
- **GET** `/tareas`  
  Lista todas las tareas del usuario.  

- **GET** `/tareas/{id}`  
  Obtiene una tarea por su Id.  

- **POST** `/tareas`  
  Crea una nueva tarea.  

- **PUT** `/tareas/{id}`  
  Edita una tarea existente.  

- **DELETE** `/tareas/{id}`  
  Elimina una tarea por su Id.  

## 📌 Notas  
- Autenticación vía **JWT** en el header:  
  ```http
  Authorization: Bearer {token}
  ```  

- Respuestas JSON estandar (`ApiResponse`).  

### 📥 Ejemplo de Respuesta JSON  
```json
{
  "exito": true,
  "mensaje": "Operación realizada correctamente",
  "datos": {
    "id": 1,
    "titulo": "Primera tarea",
    "descripcion": "Descripción de la tarea",
    "prioridad": "Alta",
    "completada": false
  },
  "codigoError": null
}
```  
