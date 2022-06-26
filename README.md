# transportersApi
Distribución de artículos desde centros de acopio

## Tecnologia utilizada
* Visual Studio 2022 Community
* Netcore - .Net6
* SQlite

## Ejecución en local
Como recomendación, reinstalar los paquetes de la solución por medio del comando:
* nuget restore TransportersApi.sln

o por medio de Visual Studio 
![image](https://user-images.githubusercontent.com/68788413/175826585-169c6910-423a-4c01-a212-15bd68de16ac.png)

Para la ejecucion se puede utilizar el comando:
* dotnet run --project .\TransportersApi\TransportersApi.csproj

o por medio de Visual Studio
![image](https://user-images.githubusercontent.com/68788413/175826771-283f926d-89e9-4398-a93e-cf0f2664cdb1.png)

# Documentación api
La documentación se desarrollo con la herramienta Swagger y está disponible en el ambiente de desarrollo (local) ingresando a la URL
* https://localhost:7174/swagger/index.html

De igual forma se entrega la documentación de la api como documento dentro del proyecto apunando a la nube (AWS).
La api fue desplegada por medio de Elastic Beanstalk, y el end-point es:
* http://transportersapi-env.eba-m2gntps4.us-east-1.elasticbeanstalk.com/


# Pruebas unitarias
Se realizaron las pruebas unitarias utilizando la herramienta MOQ, para simular los repositorios inyectados en la api
![image](https://user-images.githubusercontent.com/68788413/175827019-981b3147-6154-4f82-ac77-246f3732b111.png)
