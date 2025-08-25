@echo off
setlocal enabledelayedexpansion

:: Crear carpetas
mkdir Common
mkdir Domain\Entities
mkdir Domain\Enums
mkdir DTOs\Auth
mkdir DTOs\Tareas
mkdir Infrastructure\Data
mkdir Services\Interfaces
mkdir Services\Implementations
mkdir Controllers

:: Crear archivos vacíos en Common
echo. > Common\ApiResponse.cs
echo. > Common\CodigosError.cs
echo. > Common\ErrorHandlingMiddleware.cs
echo. > Common\JwtSettings.cs

:: Crear archivos en Domain
echo. > Domain\Entities\Usuario.cs
echo. > Domain\Entities\Tarea.cs
echo. > Domain\Enums\Prioridad.cs

:: Crear archivos en DTOs
echo. > DTOs\Auth\LoginRequest.cs
echo. > DTOs\Auth\JwtTokenResponse.cs
echo. > DTOs\Tareas\TareaCrearDto.cs
echo. > DTOs\Tareas\TareaEditarDto.cs
echo. > DTOs\Tareas\TareaDto.cs

:: Crear archivos en Infrastructure
echo. > Infrastructure\Data\AppDbContext.cs

:: Crear archivos en Services
echo. > Services\Interfaces\IJwtService.cs
echo. > Services\Interfaces\ITareaService.cs
echo. > Services\Implementations\JwtService.cs
echo. > Services\Implementations\TareaService.cs

:: Crear archivos en Controllers
echo. > Controllers\AuthController.cs
echo. > Controllers\TareasController.cs

echo Estructura creada correctamente.
pause
