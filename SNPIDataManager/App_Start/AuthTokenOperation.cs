using Swashbuckle.Swagger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Description;

namespace SNPIDataManager.App_Start
{
    /// <summary>
    /// Alternatieve to SwaggerConfig OAuth2. 
    /// </summary>
    public class AuthTokenOperation : IDocumentFilter
    {
        //Method inherits from IDocumentFilter
        public void Apply(SwaggerDocument swaggerDoc, SchemaRegistry schemaRegistry, IApiExplorer apiExplorer)
        {
            //** Set PATH = /token **//
            swaggerDoc.paths.Add("/token", new PathItem
            {
                //** Operation type is POST **//
                post = new Operation
                {
                    //** Tags/Category = Auth **//
                    tags = new List<string> { "Authentication" },
                    //** Form Type urlencoded to add parameters to URL **//
                    consumes = new List<string>
                    {
                        "application/x-www-form-urlencoded"
                    },
                    //** New Parameters > grant_type is set by default. Is always password **//
                    parameters = new List<Parameter>
                    { 
                        new Parameter
                        { 
                            type = "string",
                            name = "grant_type",
                            required = true,
                            //D in formData has to be a Capital D. otherwise error on name property
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