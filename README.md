# Iac
sample project for infrastructure as code

1 - what we need to run this project ?

at first we should install posgresql and .net core 5.
then install rabbitmq on this step we didn't use rabbit but for the next step we plan to distributed services and use it cause that we need rabbitmq.
after all install pulumi cli to use this project.
you should remeber that this project is not ready for production use and has some minor bugs in structure and abbility but for prove thecnology and some learning purpose you can use it.

2 - what the technology and design patterns use in it ?
 
 we use #Cqrs #MediatR #Pulumi to build this project.
 in the service project we use DDD design pattern and we plan to use dapper for reposrting service

