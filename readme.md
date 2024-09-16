# Market Microservices

This is a microservices project which consists in making two services exchange messages between them depending on an operation promoted by one of the microservice.

For simplicity both microservices are in the same repository, but in a real world project them could apart.

## Stack

- .NET
- RabbitMQ
- MassTransit
- Entity Framework Core
- SQL Server
- Kubernetes

## Setup

First you will need two install the following on your machine:

- [dotnet cli](https://learn.microsoft.com/pt-br/dotnet/core/tools/)
- [dotnet cli for Entity Framework](https://learn.microsoft.com/en-us/ef/core/cli/dotnet)
- [Docker Desktop](https://www.docker.com/products/docker-desktop/)

After installing Docker Desktop, you need to go to settings and enable Kubernetes Cluster.

## Running Locally

Once you have your Kubernetes Cluster up and running locally, you will have to reach to the k8s folder in the root and using the kubectl commands apply all files. Please note that the order of applying the files matters, so you must start it from the deployment files.

## Applying RabbitMQ .yaml files

`kubectl apply -f rabbitmq-dpl.yaml`

`kubectl apply -f rabbitmq-svc.yaml`

`kubectl apply -f rabbitmq-secret.yaml`

You'll note that the secret file has some data that comes from the repository secret just to show off security. In order to run locally, these values must be replaced by the following ones:

- RabbitMQPassword: guest
- RabbitMQUser: guest
- RabbitMQPort: 5672
- RabbitMQHost: The Cluster IP provided by Rabbit MQ Service\*

\*To get the Cluster ip for the RabbitMQHost as pointed below, you need to run `kubectl get services` then search for rabbitmq-svc and get the IP.

Please note, the values below must be encoded to base64, as required by Kubernetes.

## SQLServer Deployment

As I already had a SQLServer deployment running on my machine, the yaml file for it is not in the repo, but you can run both the deployment and service it using the yaml below:

```
apiVersion: apps/v1
kind: Deployment
metadata:
  name: mssql-depl
spec:
  replicas: 1
  selector:
    matchLabels:
      app: mssql
  template:
    metadata:
      labels:
        app: mssql
    spec:
      containers:
        - name: mssql
          image: mcr.microsoft.com/mssql/server:2017-latest
          ports:
            - containerPort: 1433
          env:
            - name: MSSQL_PID
              value: Express
            - name: ACCEPT_EULA
              value: "true"
            - name: SA_PASSWORD
              valueFrom:
                secretKeyRef:
                  name: mssql
                  key: SA_PASSWORD
          volumeMounts:
            - mountPath: /var/opt/mssql/data
              name: mssqldb
      volumes:
        - name: mssqldb
          persistentVolumeClaim:
            claimName: mssql-claim

---
apiVersion: v1
kind: Service
metadata:
  name: mssql-clusterip-srv
spec:
  type: ClusterIP
  selector:
    app: mssql
  ports:
    - name: mssql
      protocol: TCP
      port: 1433
      targetPort: 1433

---
apiVersion: v1
kind: Service
metadata:
  name: mssql-loadbalancer
spec:
  type: LoadBalancer
  selector:
    app: mssql
  ports:
    - protocol: TCP
      port: 1433
      targetPort: 1433
```

You'll notice the env values and not read from secret files but directly in Kubernets instead. You can achieve it by running in your terminal:

`kubectl create secret generic mssql --from-literal=SA_PASSWORD=[YOUR PASSWORD]`

Besides, you also need to define a Persistent Volume Claim yaml file as follows:

```
apiVersion: v1
kind: PersistentVolumeClaim
metadata:
  name: mssql-claim
spec:
  accessModes:
    - ReadWriteMany
  resources:
    requests:
      storage: 200Mi
```

## Applying each project k8s files

Now you have to go in Market.Inventory and Market.Sales and apply all files inside k8s folder, first the deployment ones, then the service and lastly the ingress.

## Additional Ingress configuration

In order to make API calls for the endpoint defined in each Ingress file, it's necessary to point your machine localhost to accept incoming request form acme.com - whic is just a random url I picked.

To achieve it in Windows, go to `C:\Windows\System32\drivers\etc` and in the host file add the following line:

`127.0.0.1 acme.com`


## Making API calls

After everything is set up following the instructions given above, you can call the following endpoints and watch in the terminal of each app the logs of the communication using MassTransit and other additional ones:

1) First create a project to Inventory API:

```
curl --location 'http://acme.com/api/products' \
--header 'Content-Type: application/json' \
--data '{
    "name": "tv",
    "quantity": 100,
    "category": "eletronics",
    "price": 1200
}'
```

2) Second create sale using the Sales API:

```
curl --location 'http://acme.com/api/sales' \
--header 'Content-Type: application/json' \
--data '{
    "productId": <The id of the product created in the first request>,
    "quantity": 1,
    "price": 1800
}'
```