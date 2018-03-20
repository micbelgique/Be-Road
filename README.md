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
* the central system connected to the blockchain

The blockchain genesis is stored on a VM on Azure.

# Getting Started
## Summary
1.	Clone the code
2.	Packages included
3.	Latest releases

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
    <td rowspan="7">MessageLog</td>
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
</table>

### Last releases

#### 0.2.0 - 3rd March 2018
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

#### 0.1.0 - 3rd March 2018
##### Privacy Passport
* Login page : e-ID connection
* Home page : Public services data sources
* Data page : Data possessed by a the selected public service
* Details modal : Accesses on the selected piece of data