# Introduction 
In Belgium, public services interact with each others directly.
For example, when a hospital need to know your National Identification Number, it's administration will access you town administration services.
This access is marked with a reason.

Privacy Passport is a portal on which any Belgian citizen can connect to with their e-ID in order to see all these accesses.

But this project is not only about that portal.
It also contains a hole system through which the public services should be going in order to communicate.
This central point redirects requests between public services and log these accesses in a private blockchain.

This project is a prototype that will be used to show the usability of this system.

It currently contains : 
* the Privacy Passport portal
* a mocked public service
* the central system connected to the blockchain (Be-road)

The following chart represents the infrastructure of Be-Road.

![Be Road Schema](Web/Web/BeRoadSchema.png "Be-Road Chart")

The blockchain genesis is stored on a VM on Azure.

The public services are able to communicate through Be-Road using contracts.
Find more about them in [the 3rd section below](#how-to-create-and-use-contracts).

# Getting Started
## Summary
1.	[Clone the code](#clone-the-code)
2.  [Blockchain implementation](#blockchain-implementation)
3.  [How to create and use contracts](#how-to-create-and-use-contracts)
3.	[Packages included](#packages-included)
4.	[Latest releases](#latest-releases)

### Clone the code
To get the websites and the central part on your machine, you just have to clone the project in your git.
The solution contains 3 projects :
* Web (the main portal)
* PublicService (the public service mock)
* MessageLog (the central part)

You will have to change the connection strings in the Web.config files in the 3 projects
```xml
<!-- Local connection string (Web.Debug.Config) -->
<connectionStrings>
    <add name="{ContextName}" connectionString="Data Source=(localdb)\MSSQLLocalDB; Initial Catalog=LogContext-20180313142602; Integrated Security=True; MultipleActiveResultSets=True; AttachDbFilename=|DataDirectory|{ContextFileName}.mdf" providerName="System.Data.SqlClient" />
</connectionStrings>

<!-- Remote connection string (Web.Release.Config) -->
<connectionStrings>
    <add name="{ContextName}" connectionString="Server=tcp:{ServerName},1433;Initial Catalog={DatabaseName};Persist Security Info=False;User ID={Username};Password={Password};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;" providerName="System.Data.SqlClient"/>
</connectionStrings>
```

### Blockchain implementation
The blockchain is a prototype too.
In the final project, that will be done later by more advanced developers, it will certainly be created from scratch.

Currently, we used Multichain to create our private blockchain.
For advanced knowledge on how to create your own blockchain with multichain, check their [website](https://www.multichain.com/).

To implement a multichain blockchain in the project, we used a NuGet package called LucidOcean.Multichain ([GitHub](https://github.com/LucidOcean/multichain)) that gathers a set of class to call the RPC API of your blockchain, and so controlling it from the outside.

Then you just have to change the data in the MessageLog\Web.Config file : 

```xml
<appSettings>
    <add key="Hostname" value="{IP address}" />
    <add key="Port" value="{port}" />
    <add key="Username" value="{rpc username (usually multichainrpc)}" />
    <add key="Password" value="{rpc password}" />
    <add key="ChainName" value="{blockchain name}" />
    <add key="BurnAddress" value="{burn address}" />
    <add key="RootNodeAddress" value="{any wallet address on your genesis}" />
</appSettings>
```

If you have problems understanding these, check [multichain documentation](https://www.multichain.com/developers/).
We have set these in all the Web.config files in order to have access to a developement and a staging blockchains.

### How to create and use contracts
Contracts are JSON bits of code serialized and deserialized in C#.
They have inputs, outputs, and a body made of queries.

They work like functions that can call other contracts.
Their inputs are parameters that the target needs to retreive the date.
The outputs are the information that was asked.
The querries are not always necessary.
You will use these those when you miss data that are stored in other services.
They call other contracts passing inouts to them.

The contracts are created in Be-Road with recommandations asked to services.

#### Example
##### Without queries

JSON :
```json
{
    "Id": "GetOwnerIdByDogId",
    "Description": "This contract is used to get the dog owner id",
    "Version": "V001",
    "Inputs": [
        {
            "Type": "String",
            "Key": "DogID",
            "Required": true,
            "Description": "The ID of the Dog"
        }
    ],
    "Queries": [],
    "Outputs": [
        {
            "Contract": "GetOwnerIdByDogId",
            "Type": "String",
            "Description": "The ID of the owner of the dog",
            "Key": "OwnerIDOfTheDog"
        }
    ]
}
```
C# :
```csharp
var GetDogOwnerContract = new BeContract()
{
    Id = "GetOwnerIdByDogId",
    Description = "This contract is used to get the dog owner id",
    Version = "V001",
    Inputs = new List<Input>()
    {
        new Input()
        {
            Key = "DogID",
            Description = "The ID of the Dog",
            Required = true,
            Type = typeof(string)
        }
    },
};
GetDogOwnerContract.Queries = new List<Query>();
GetDogOwnerContract.Outputs = new List<Output>()
{
    new Output()
    {
        Contract = GetDogOwnerContract,
        Key = "OwnerIDOfTheDog",
        Description = "The ID of the owner of the dog",
        Type = typeof(string)
    }
};
```

##### With queries

```json
{
    "Id": "GetAddressByDogId",
    "Description": "This contract is used to get the address by dog id",
    "Version": "V001",
    "Inputs": [
        {
            "Type": "String",
            "Key": "MyDogID",
            "Required": true,
            "Description": "The ID of the Dog"
        }
    ],
    "Queries": [
        {
            "Contract": "GetOwnerIdByDogId",
            "Mappings": [
                {
                    "InputKey": "DogID",
                    "Contract": "GetAddressByDogId",
                    "ContractKey": "MyDogID"
                }
            ]
        },
        {
            "Contract": "GetAddressByOwnerId",
            "Mappings": [
                {
                    "InputKey": "OwnerID",
                    "Contract": "GetOwnerIdByDogId",
                    "ContractKey": "OwnerIDOfTheDog"
                }
            ]
        }
    ],
    "Outputs": [
        {
            "Contract": "GetAddressByOwnerId",
             "Type": "String",
             "Description": "Street name",
             "Key": "Street"
        },
        {
             "Contract": "GetAddressByOwnerId",
             "Type": "Int32",
             "Description": "Street number",
             "Key": "StreetNumber"
       },
       {
             "Contract": "GetAddressByOwnerId",
             "Type": "String",
             "Description": "Country of the address",
             "Key": "Country"
       }
    ]
}
```

```csharp
var GetAddressByOwnerContract = GetAddressByOwnerId();
var GetOwnerIdByDogContract = GetOwnerIdByDogId();

var GetAddressByDogContract = new BeContract()
{
    Id = "GetAddressByDogId",
    Description = "This contract is used to get the address by dog id",
    Version = "V001",
    Inputs = new List<Input>()
    {
        new Input()
        {
            Key = "MyDogID",
            Description = "The ID of the Dog",
            Required = true,
            Type = typeof(string)
        }
    },
};

GetAddressByDogContract.Queries = new List<Query>()
{
    new Query()
    {
        Contract = GetOwnerIdByDogContract,
        Mappings = new List<Mapping>()
        {
            new Mapping()
            {
                InputKey = "DogID",
                Contract = GetAddressByDogContract,
                ContractKey = "MyDogID"
            }
        }
    },
    new Query()
    {
        Contract = GetAddressByOwnerContract,
        Mappings = new List<Mapping>()
        {
            new Mapping()
            {
                InputKey = "OwnerID",
                Contract = GetOwnerIdByDogContract,
                ContractKey = "OwnerIDOfTheDog"
            }
        }
    }
};

GetAddressByDogContract.Outputs = new List<Output>()
{
    new Output()
    {
        Contract = GetAddressByOwnerContract,
        Key = "Street",
        Description = "Street name",
        Type = typeof(string)
    },
    new Output()
    {
        Contract = GetAddressByOwnerContract,
        Key = "StreetNumber",
        Description = "Street number",
        Type = typeof(int)
    },
    new Output()
    {
        Contract = GetAddressByOwnerContract,
        Key = "Country",
        Description = "Country of the address",
        Type = typeof(string)
    }
};
```

Contracts are added in a database so that they can be used either directly or via queries.

### Packages included
We used different packages in these projects.
Here is a list of them and their utility in each project :

<table>
  <tr>
    <td>Project</td>
    <td>Package</td>
    <td>Version</td>
    <td>Description</td>
  </tr>
  <tr>
    <td rowspan="7">Web</td>
    <td>DotNetOpenAuth</td>
    <td>4.3.4.13329</td>
    <td>Add OpenID 1.1/2.0, OAuth 1.0(a), & InfoCard authentication and authorization functionality for client and server applications. This allows your application to issue identities or accept issued identites from other web applications, and even access your users' data on other services.</td>
  </tr>
  <tr>
    <td>EntityFramework</td>
    <td>6.2.0</td>
    <td>Another Tool for Language Recognition, is a language tool that provides a framework for constructing recognizers, interpreters, compilers, and translators from grammatical descriptions containing actions in a variety of target languages.</td>
  </tr>
  <tr>
    <td>materialize</td>
    <td>0.97.6</td>
    <td>Materialize, a CSS Framework based on Material Design <a href="http://materializecss.com">http://materializecss.com<a/></td>
  </tr>
  <tr>
    <td>Owin</td>
    <td>1.0.0</td>
    <td>OWIN IAppBuilder startup interface</td>
  </tr>
  <tr>
    <td>Modernizr</td>
    <td>2.8.2</td>
    <td>Modernizr adds classes to the <html> element which allow you to target specific browser functionality in your stylesheet. You don't actually need to write any Javascript to use it. Modernizr is a small and simple JavaScript library that helps you take advantage of emerging web technologies (CSS3, HTML5) while still maintaining a fine level of control over older browsers that may not yet support these new technologies.</td>
  </tr>
  <tr>
    <td>Newtonsoft.Json</td>
    <td>10.0.2</td>
    <td>Json.NET is a popular high-performance JSON framework for .NET.</td>
  </tr>
    <tr>
    <td>System.Net.Http</td>
    <td>4.3.3</td>
    <td>Provides a programming interface for modern HTTP applications, including HTTP client components that allow applications to consume web services over HTTP and HTTP components that can be used by both clients and servers for parsing HTTP headers.</td>
  </tr>
  <tr>
    <td rowspan="7">PublicService</td>
    <td>DotNetOpenAuth</td>
    <td>4.3.4.13329</td>
    <td>Add OpenID 1.1/2.0, OAuth 1.0(a), & InfoCard authentication and authorization functionality for client and server applications. This allows your application to issue identities or accept issued identites from other web applications, and even access your users' data on other services.</td>
  </tr>
  <tr>
    <td>EntityFramework</td>
    <td>6.2.0</td>
    <td>Another Tool for Language Recognition, is a language tool that provides a framework for constructing recognizers, interpreters, compilers, and translators from grammatical descriptions containing actions in a variety of target languages.</td>
  </tr>
  <tr>
    <td>bootstrap</td>
    <td>3.0.0</td>
    <td>The most popular front-end framework for developing responsive, mobile first projects on the web.</td>
  </tr>
  <tr>
    <td>Owin</td>
    <td>1.0.0</td>
    <td>OWIN IAppBuilder startup interface</td>
  </tr>
  <tr>
    <td>Modernizr</td>
    <td>2.8.2</td>
    <td>Modernizr adds classes to the <html> element which allow you to target specific browser functionality in your stylesheet. You don't actually need to write any Javascript to use it. Modernizr is a small and simple JavaScript library that helps you take advantage of emerging web technologies (CSS3, HTML5) while still maintaining a fine level of control over older browsers that may not yet support these new technologies.</td>
  </tr>
  <tr>
    <td>Microsoft.AspNet.Identity.Core</td>
    <td>2.2.1</td>
    <td>Core interfaces for ASP.NET Identity.</td>
  </tr>
    <tr>
    <td>System.Net.Http</td>
    <td>4.3.3</td>
    <td>Provides a programming interface for modern HTTP applications, including HTTP client components that allow applications to consume web services over HTTP and HTTP components that can be used by both clients and servers for parsing HTTP headers.</td>
  </tr>
  <tr>
    <td rowspan="3">MessageLog</td>
    <td>LucidOcean.Multichain</td>
    <td>0.0.0.10</td>
    <td>This library attempts to wrap the JSON-RPC calls provided by MultiChain. Where applicable, some initial design for ease of use and separation of calls has been made.</td>
  </tr>
  <tr>
    <td>Newtonsoft.Json</td>
    <td>10.0.2</td>
    <td>Json.NET is a popular high-performance JSON framework for .NET.</td>
  </tr>
    <tr>
    <td>EntityFramework</td>
    <td>6.2.0</td>
    <td>Another Tool for Language Recognition, is a language tool that provides a framework for constructing recognizers, interpreters, compilers, and translators from grammatical descriptions containing actions in a variety of target languages.</td>
  </tr>
  <tr>
    <td rowspan="2">Contracts</td>
    <td>Newtonsoft.Json</td>
    <td>10.0.2</td>
    <td>Json.NET is a popular high-performance JSON framework for .NET.</td>
  </tr>
  <tr>
    <td>NJsonSchema</td>
    <td>9.10.41</td>
    <td>JSON Schema reader, generator and validator for .NET</td>
  </tr>
</table>

### Last releases

#### 0.3.0 - 20th March 2018
##### All
* Different working enviroments : Devlopement, Staging
* User account : Added NISS

##### Message Log
* Rest API : To call the Message Log
* Database : To log the accesses
* Route : To add the data
* Hash : To hide the data
* Blockchain : To log the accesses

#### 0.2.0 - 7th March 2018
##### Public Service
* Register page : e-ID account creation
* Login page : username and password
* Account managing page : Add more details to your account
* User search page : Find the user you want to see the data about
* Reason modals : Enter the reason why you want to access the data
* User details page : See the information of the user when you gave the reason
* GDPR information page : Check the GDPR compliance of the website
* Recurring job : Export database every day
* Rest API : To make the data open
* Swagger : API documentation

#### 0.1.0 - 19th February 2018
##### Privacy Passport
* Login page : e-ID connection
* Home page : Public services data sources
* Data page : Data possessed by a the selected public service
* Details modal : Accesses on the selected piece of data