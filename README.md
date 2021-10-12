# CQRS.Mediatr.Lite

![GitHub Workflow Status](https://img.shields.io/github/workflow/status/microsoft/CQRS.Mediatr.Lite/CI)
![Nuget](https://img.shields.io/nuget/dt/CQRS.Mediatr.Lite)
![Nuget](https://img.shields.io/nuget/v/CQRS.Mediatr.Lite)

CQRS.Mediatr.Lite is a light-weight library to implement CQRS pattern in .NET solutions. The motivation for this library comes from the popular [Mediatr library](https://github.com/jbogard/MediatR).

The library enables you to create Commands, Queries, Events and their respective handlers in a decouples fashion. The library also provides some starter code for Aggregate Root, for implementing Domain-Driven designs leveraging the CQRS pattern.

## Setup
Install the NuGet package: `Install-Package CQRS.Mediatr.Lite`.

## CQRS Basics
CQRS stands for Command Query Responsibility Segregation. It's a pattern which segregates the read and write pipeline. In complicated scenarios creating separate models and pipelines for reading and writing data help in tackling the complexity. All operations in the system can be divided into 2 caegories
 - Queries - Used only to read data from the database, and should not make any change to the state.
 - Commands - Change the state of the system by creating new data, update existing data or delete data from the system.
References to CQRS
1. [CQRS - Martin Fowler](https://martinfowler.com/bliki/CQRS.html)
2. [Apply simplified CQRS and DDD patterns in a microservice](https://docs.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/apply-simplified-microservice-cqrs-ddd-patterns)

Complex applications which can benifit from Domain-Driven-Design also draws advantage from CQRS.

## Samples
https://github.com/microsoft/CQRS.Mediatr.Lite/tree/main/samples/CQRS.Mediatr.Lite.Samples

## Contributing

This project welcomes contributions and suggestions.  Most contributions require you to agree to a
Contributor License Agreement (CLA) declaring that you have the right to, and actually do, grant us
the rights to use your contribution. For details, visit https://cla.opensource.microsoft.com.

When you submit a pull request, a CLA bot will automatically determine whether you need to provide
a CLA and decorate the PR appropriately (e.g., status check, comment). Simply follow the instructions
provided by the bot. You will only need to do this once across all repos using our CLA.

This project has adopted the [Microsoft Open Source Code of Conduct](https://opensource.microsoft.com/codeofconduct/).
For more information see the [Code of Conduct FAQ](https://opensource.microsoft.com/codeofconduct/faq/) or
contact [opencode@microsoft.com](mailto:opencode@microsoft.com) with any additional questions or comments.

## Trademarks

This project may contain trademarks or logos for projects, products, or services. Authorized use of Microsoft 
trademarks or logos is subject to and must follow 
[Microsoft's Trademark & Brand Guidelines](https://www.microsoft.com/en-us/legal/intellectualproperty/trademarks/usage/general).
Use of Microsoft trademarks or logos in modified versions of this project must not cause confusion or imply Microsoft sponsorship.
Any use of third-party trademarks or logos are subject to those third-party's policies.
