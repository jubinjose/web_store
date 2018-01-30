1. Create Store.Repository project (class library)

2. Add New Item - Data - ADO.Net Entity Framework

3. Answer EF prompts as below
---------------------------------------------------------
EDMX file name --> edmx File Name => DataStore
Connection string name --> Connection string name in the config file, and also for container name of conceptual model CSDL of EDMX => DbContext
Model namespace --> Used for the namespace of above mentioned cocneptual model part of the EDMX, and also for the store model (SSDL) part with .Store appended
Custom tool namespace for the EDMX file --> Not used when using T4 generation of POCO entities. When using EF1-style built in code generation, setting this property will set the .NET namespace for all generated files.
Custom tool namespace for .Context.tt file --> namespace used in the source file for the context
Custom tool namespace for .tt file --> namespace used in the source files for the entities

If you set the .Context.tt and .tt custom namespaces to different things, then the context will be generated in a different namespace to the entity types 
and this won't compile out-of-the-box. You can update the .tt files if you want to use different namespaces here, but more often people just use the same namespace for both.

Also note that you may need to choose "Run Custom Tool" from the context menu for each .tt file after changing the properties in order for the code to be re-generated.

4. Create Store.Repository project (class library). 
   Do not add any references to Entity Framework

5. Manually create a Models.tt empty file and copy contents of the models tt file nested under the edmx in Store.Repository
   to this new file.
   
6. Edit the newly created tt file to update the edmx path from
   @"DataStore.edmx" to @"../Store.Repository/DataStore.edmx"

7. if generated files need to be having a .generated in their name, then edit the tt file to make the name change in three places (enums, entity, complex types)
   edit would be -> fileManager.StartNewFile(enumType.Name + ".generated.cs");

8. Cleaner way of doing steps above - create a new  ttinclude file and specify settings there and use those in tt file



