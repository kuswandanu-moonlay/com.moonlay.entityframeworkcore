# Com.Moonlay.EntityFrameworkCore
Moonlay Extensions for Microsoft.EntityFrameworkCore

## Dependencies
- Package `Microsoft.EntityFrameworkCore v2.0.0`
- Package `Com.Moonlay.Models`

## Installation
Package Manager
```
Install-Package Com.Moonlay.EntityFrameworkCore
```

.Net CLI
```
dotnet add package Com.Moonlay.EntityFrameworkCore
```

## How To Use

## Enable .Net Core EF CLI
Add this code into file (Project Name).csproj
```xml
<ItemGroup>
    <DotNetCliToolReference Include="Microsoft.VisualStudio.Web.CodeGeneration.Tools" Version="2.0.0" />
    <DotNetCliToolReference Include="Microsoft.EntityFrameworkCore.Tools.DotNet" Version="2.0.0" />
</ItemGroup>
```
Add nuget package `Microsoft.EntityFrameworkCore.Design`

### Implement Com.Moonlay.Models.StandardEntity

```cs
public class Model : StandardEntity
{
    ...
}
```

### Create your DbContext by implement `BaseDbContext`
```cs
public class MyDbContext : BaseDbContext 
{
    ...
}
```

### Run .Net Core CLI EF Migrations
Open powershell/command prompt(windows based) or bash/sh (unix based). Inside your project folder run this command:
```
dotnet ef migrations add Initial
```
You will find all class are implement `StandardEntity` generated as Tables in folder (Project Folder)/Migrations.



## License

Copyright 2017 Copyright 2017 (c) Moonlay. All rights reserved.

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

    http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.