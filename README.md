# PerchedPeacockSkillenza
Full Stack Application for a parking lot solution company. The Application automates various operations such as finding a free parking , booking a parking slot etc.

# Technical Stack

  | Front End | Service Layer | Database| 
  |-----------|:-------------:|:--------|
  | Angular 8  | Asp.net Core 3 Web API|SQLLite Database |  
  
 ## Prerequisite 
  Developer should have below softwares installed on the dev server to execute this project <br/>
    VS 2019 <br/>
    Node 10.9 + <br/>
    Npm 6 + 
  
  ### Other Frameworks Used 
  1. xUnit.net - For Unit testing 
    xUnit.net is a free, open source, community-focused unit testing tool for the .NET Framework.
  2. Microsoft Identity4 - For Authentication & Authorization (IdentityServer4 is an OpenID Connect and OAuth 2.0 framework for ASP.NET Core)
  3.  Swashbuckle swagger - For API documentation  
      * Swashbuckle.AspNetCore.Swagger: a Swagger object model and middleware to expose SwaggerDocument objects as JSON endpoints.

      * Swashbuckle.AspNetCore.SwaggerGen: a Swagger generator that builds SwaggerDocument objects directly from your routes, controllers,    and models. It's typically combined with the Swagger endpoint middleware to automatically expose Swagger JSON.

      * Swashbuckle.AspNetCore.SwaggerUI: an embedded version of the Swagger UI tool. It interprets Swagger JSON to build a rich, customizable        experience for describing the web API functionality. It includes built-in test harnesses for the public methods.
   
  ## Project Implementation details 
   ![](PerchedPeacockArc.JPG)
  
  ## Solution Key items
     1. PerchedPeacockWebApplication.csproj 
        This project contains the rest api's and business logic for the perched peacock application
        The project has a folder called ClientApp. This folder contains the front end part of the application
        
        
  ## API Design 
      Controllers 
      1.  Bookings controller - The booking controller has the methods for managing parking lot bookings. Key operations are Post a booking, find free slots and get booking. 
      2.  Parking lot controller - The parking lot controller will take care of managing parking lot referential. key operations are adding a parking lot and removing a parking lot. 
      3.  location controller - The location controller is a referential for all parking lot locations. 
      
   ## CI/ CD
     Builds - https://dev.azure.com/remyarajan843/PerchedPeacokNewProj/_build?definitionId=1 
     
     Test reports - https://dev.azure.com/remyarajan843/PerchedPeacokNewProj/_build/results?buildId=9&view=ms.vss-test-web.build-test-results-tab
     
     Code coverage - https://dev.azure.com/remyarajan843/PerchedPeacokNewProj/_build/results?buildId=9&view=codecoverage-tab 
     
     Release pipeline - https://dev.azure.com/remyarajan843/PerchedPeacokNewProj/_releaseProgress?_a=release-pipeline-progress&releaseId=9
     
 #### Dotnet core 3 supported regions in Azure cloud
https://aspnetcoreon.azurewebsites.net/#ASP.NET%20Core%20Module
