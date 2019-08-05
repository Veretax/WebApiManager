# WebApiManager
# WebApi.Manager

# Steps to Create ASP.Net Framework WebApi Examples

# Create GitHub Repository
1. Go to GitHub.com
2. Click Plus sign to create repository
3. Name the repository, choose its visibility, select VisualStudio for GitIgnore, and choose License (default: mit)

# Cloning GitHub Repository
1. Open and Connect Visual Studio to GitHub.  (You may need to install GitHub Extension to be able to connect.)
2. Click Clone login to GitHub.com in Visual Studio
3. Set the browse folder before selecting the repository.
4. Browse for new repository, select it and click clone  (if it says it can't it may indicate a repo is already there in that folder, choose a different folder and try again.)
5. The GitHub repository clones/checks out into the folder.
6. To check on it, go to Team Explorer, Sync, click fetch or Pull, it should say the project is already up to date.

# Create Blank Solution
1. Goto File > New / Project/Solution
2. Search for Blank (C#)
3. Enter a name for the solution (should match repository unless there's a reason not to.)
4. Select the path where the repository was checked out
5. Create
6. Visual Studio creates  blank folder, in the folder you selected (should be the same as where you cloned the github repo)

# Create Web Application Project
1. On Solution View, right click on the Solution > add > new project.
2. Search for ASP.Net Web Application (.Net Framework) (C#) and click next
3. Enter a name for the Web Application Project
4. Select .Net Framework version (if matters)
5. Check project location (it should be in side the solution somewhere)
6. Click Create
7. Choose Web Api (notice MVC and Web Api are checked)
8. Click Change on Authentication and set to desired (No Authentication means none required, and should not be used for services that can be destroyed.  Suggest Individual Account, or Windows Authentication as the choice.
9. If an MSTest Project is desired click  Also Create a Unit Test Project (suggest naming it same name as project plus "MSUnitTests)
10. Click create, and after a few moments both projects should be created.

# Initial Account Creation
1. Go to [Get Postman](www.getpostman.com)
2. Download and install Postman app  then run it.
3. First call to make can be to register a new user
4. Start the web service, and get the path to localhost ex: https://localhost:44328/ (the port may be different on your box)
5. in request enter the url (changing the port/domain as necessary) (You can click on headers and add Content-Type with a value of application/json and then click send.  If you receive a message that includes json which looks like: 
```
{
    "Message": "Authorization has been denied for this request."
}
```
That means you have Authentication setup, but do not have a user yet.  SO we need to create initial account
6. Change the  request type from Get to Post, and use https://localhost:44328/api/Account/Register as the request URL.
7. Note you can find these ACcount paths, by loading  https://localhost:44328/ and clicking API, and looking at Account.
8. Make sure you have similar headers, and then click on body and then select raw, to add your JSON data for Registering an account like: 
```
{
  "Email": "yourUser@email.com", 
  "Password": "1ComplexPassword.",
  "ConfirmPassword": "1ComplexPassword"
}
```
9. Note change the Email to your user name or email address (email preferred), and set a password not so easily guessed which needs at least 1 Upper and Lowercase character, number, and a symbol, currently.
10. You should receive a 200(Ok) once it has completed indicating success.

# Getting Initial Token
1. Create a new request in Postman
2. Set URL to https://localhost:44328/token, and Verb to Get
3. Click body and select x-www-form-urlencoded
4. Click to set a key: grant_type with the value password
5. Click to set a key: username with the value you used as your Email in the previous step
6. Click to set a key: password with the value you used as Password in the previous step
7. If you did this correctly you should receive a JSON response with the following format and fields. (note email and token are different per user.
```
{
    "access_token": "Hps4KPhQgkZ1GUd2qVMu_89HCSh4_fP0k1knpDipLH53TcbcEeHbFB6CmmuqYVb3EBHQ5ZgHl1GiNXGQRSOWr0K0a6M7F5lNXsYBrmBT3BG_0MNxgKHm-tgGeiP7ztCI2OVU6nzPT-oXJiXhAfRen1lbxRb9VkY9fn3ASHW1eVCwkLLiJEy_zQwnpYkjr_bwDbeBRizGnJDK0WfiGgYcUq8qXbAZhFxiWyeNuBqpIcJRWhn6hrZ3gfKno4pBDIreuyjIaKZISj2XGbGSJLcFWJemADYOyQFC2Tgie4JNisGq58rST63zReaUOnDgaGlQ_nF-FxCdqyGzScSDoGc3WUoZYm5nt8VKfPcY7byR24WOPuM54y2CKN8n8u3ElzDVDfTBv3egaZlHhQxEmokJw11FlIX8vMRZUBV04JWOUr2uoB5S42Ta_iIvNw3G1k8SLsnoGUL53kclg9jrpyTGnphNfIx7_rD_dCcgueiIpsFPoHPoKE_JwXUW1BIsfFBq",
    "token_type": "bearer",
    "expires_in": 1209599,
    "userName": "yourUsername@email.com",
    ".issued": "Sun, 28 Jul 2019 15:43:14 GMT",
    ".expires": "Sun, 11 Aug 2019 15:43:14 GMT"
}
```
8. the key is that this access token can then be passed to requests to the api to let it know you are authorized. (the above example likely doesn't work. so don't use that one.)

# Using a Token
1. Create a new request, of type Get with location: https://localhost:44328/api/values/ (again port might change by app)
2. Create a key authorization with value of bearer (a space) and then paste the access_token from the previous request
3. Click send
4. should receive JSON response like this 
```
[
    "value1",
    "value2"
]
```
5. All you need to do is remember to periodically get the new token, and pass it in that header and your api calls will work.

# Swagger Configuration
1. Click on references, than right click > Manage NuGet Packages.
2. Select Browse, and search for Swashbuckle
3. Click Install, and Ok on license agreements.
4. Once its installed go to Updates tab of NuGet, and install everything except latest bootstrap (for now).
5. Run the app, and type /swagger at the end of the url to load swagger and look around.
6. To configure Swagger as a Single API uncomment the line at 
```
// Use "SingleApiVersion" to describe a single version API. Swagger 2.0 includes an "Info" object to
// hold additional metadata for an API. Version and title are required but you can also provide
// additional fields by chaining methods off SingleApiVersion.
//
c.SingleApiVersion("v1", "WebApi.Manager Api");1
```
7. uncomment PrettyPrint to make it look nicer.
8. For multiple versioned APIs, use the section that looks like: 
```
//c.MultipleApiVersions(
//    (apiDesc, targetApiVersion) => ResolveVersionSupportByRouteConstraint(apiDesc, targetApiVersion),
//    (vc) =>
//    {
//        vc.Version("v2", "Swashbuckle Dummy API V2");
//        vc.Version("v1", "Swashbuckle Dummy API V1");
//    });
```
9. Uncomment DescribeAllEnumsAsStrings method
```
// c.DescribeAllEnumsAsStrings();
```
10. Uncomment DocumentTitle and change it
```
//c.DocumentTitle("My Swagger UI");
```

# Enable AuthTokenOperation in Swagger
1. In App start, create file named:
``` 
using Swashbuckle.Swagger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Description;

namespace WebCrawlerManager.App_Start
{
    public class AuthTokenOperation : IDocumentFilter
    {
        public void Apply(SwaggerDocument swaggerDoc, SchemaRegistry schemaRegistry, IApiExplorer apiExplorer)
        {
            swaggerDoc.paths.Add("/token", new PathItem
            {
                post = new Operation
                {
                    tags = new List<string> { "Auth" },
                    consumes = new List<string>
                    {
                        "application/x-www-form-urlencoded"
                    },
                    parameters = new List<Parameter>
                    {
                        new Parameter
                        {
                            type = "string",
                            name = "grant_type",
                            required = true,
                            @in = "formData",
                            @default = "password"
                        },
                        new Parameter
                        {
                            type = "string",
                            name = "username",
                            required = false,
                            @in = "formData"
                        },
                        new Parameter
                        {
                            type = "string",
                            name = "password",
                            required = false,
                            @in = "formData"
                        }
                    }

                }
            });
        }
    }
}
```
2. In Swagger config add this to protect the assembly statement so it only shows up during debugging.
```
// https://stackoverflow.com/questions/51117655/how-to-use-swagger-in-asp-net-webapi-2-0-with-token-based-authentication

// to Turn Swagger UI off uncomment this precompile statement
// #if DEBUG
[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]
// #endif

```
3. then add the document filter as follows:
```
public static void Register()
        {
            var thisAssembly = typeof(SwaggerConfig).Assembly;

            GlobalConfiguration.Configuration
                .EnableSwagger(c =>
                    {
                        c.DocumentFilter<AuthTokenOperation>();
```

4. Now if you compile and start you should be able to See Auth, and even get the bearer token.

# Create AuthorizationOperationFilter
1. In App_Start create a class named AuthorizationOperationFilter.cs 
2. Make it derive from IOperationFilter (you will need to hit ctrl . to add the using statement)
3. Then implement the interface as follows: 
```
using Swashbuckle.Swagger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Description;

namespace WebApi.Manager.App_Start
{
    public class AuthorizationOperationFilter : IOperationFilter
    {
        public void Apply(Operation operation, SchemaRegistry schemaRegistry, ApiDescription apiDescription)
        {
            if (operation.parameters == null)
            {
                operation.parameters.Add(new Parameter
                {
                    name = "Authorization",
                    @in = "header",
                    description = "access token",
                    required = false,
                    type = "string"
                });
            }
        }
    }
}
```
4. In SwaggerConfig add: 
```
c.OperationFilter<AuthorizationOperationFilter>();
```


