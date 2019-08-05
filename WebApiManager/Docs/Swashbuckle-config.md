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