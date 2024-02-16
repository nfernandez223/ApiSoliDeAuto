# Api Solicitud de Autorizacion

Api utilizada para gestionar las autorizaciones solicitadas, consultar su estado y poder autorizarlas de ser necesario.

## Instalación Dockerfile

## Requisitos Previos

Docker instalado en tu máquina.<br>
RabbitMQ instalado dentro de Docker.<br>
Sql Server instalado dentro de Docker.<br>
Verficar la existencia del archivo Dockerfile dentro del repositorio y su configuracion, siempre debe estar en la raiz del directorio.

## Crear la imagen
Para construir la imagen Docker, ejecuta el siguiente comando:

```bash
docker build -t apisolideauto .  
```
## Ejecutar el contenedor

Para ejecutar el contenedor, utiliza el siguiente comando:

```bash
docker run -d --name nombre-contenedor apisolideauto
```

## Verificacion de Instalacion

Para verficar que el servicio quedo funcionando, se puede visualizar el container el en apartado de Containers y verficiar que no haya errores en el Log.


