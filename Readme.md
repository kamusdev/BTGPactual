## Desplegar infraestructura en AWS

Para desplegar la infraestructura como código en AWS, seguir los sigiuentes pasos:

### Configurar AWS cli

Eejcutar comando y proveer la ifnormación de acceso válida:

```
aws configure
```

Crear stack:

```
aws cloudformation create-stack \
  --stack-name FondosBTG \
  --template-body file://infrastructure.yaml \
  --capabilities CAPABILITY_NAMED_IAM
```

Actualizar stack:

```
aws cloudformation update-stack \
  --stack-name FondosBTG \
  --template-body file://infrastructure.yaml \
  --capabilities CAPABILITY_NAMED_IAM
```

Eliminar stack:

```
aws cloudformation delete-stack --stack-name FondosBTG
```

Ejecutar lambda para crear registros base en DynamoDB:
```
aws lambda invoke \
  --function-name SeedDynamoDBLambda \
  --log-type Tail \
  outputfile.txt
```

## Docker Compose

Para ejecutar dynamo localmente, usar el siguiente comando:

```
docker-compose up -d
```

Esto creará los siguientes contenedores:

- DynamoDB: se creará una tabla llamada **Fondos** y se insertarán registros para los 5 fondos y el único cliente en la prueba

## Ejecutar función Lambda localmente usando SAM

Los  siguientes comandos se deben ejecutar en la raíz del proyecto de la función lambda donde se enceuntran los archivos de configuración, usando commmand prompt o power-shell.

Compilar función lambda:

```
sam build --use-container --config-env dev
```

Ejecutar función lambda en el puerto indicado:
```
sam local start-api --port 3001
```

Desplegar función lambda:

Antes de desplegar la función, se debe actualizar el archivo **samconifg.toml** para actualizar la uri del repositorio de imágenes para docker.
```
sam deploy --no-confirm-changeset --no-fail-on-empty-changeset --config-env dev
```

## Despliegue Front-End Angular en AWS

Para realizar el despliegue se usa amplify, herramienta de AWS para configurar y mantener backends de aplicaciones y crear interfaces de usuario frontend por fuera de la consola de AWS.

El primero comando que se debe correr es la instalación del paquete de amplify.

```
npm install -g @aws-amplify/cli
```

El siguiente comando que se debe correr es el siguiente:

```
amplify init
```
Este comando permite configurar el proyecto, la conexión a AWS y esteblecer los parámetros neceasrios para hacer uso de los servicios disponibles.

Dado que la aplicación para este caso requiere de un servicio de hosting para poder ser desplegada, se deberá ejecutar el siguiente comando para agregar el servicio:

```
amplify add hosting
```

Una vez que se finaliza la configuración del hosting, el siguiente comando permite desplegar la aplicación en AWS, creando todo el stack necesario en CloudFormation.

```
amplify publish
```