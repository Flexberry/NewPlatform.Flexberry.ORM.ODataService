# Flexberry ORM ODataService Changelog
All notable changes to this project will be documented in this file.
This project adheres to [Semantic Versioning](http://semver.org/).
## [Unreleased]

### Added
1. Helper class `DataObjectEdmModelDependencies` (it helps send named settings of Unity to class `DataObjectEdmModel`).

### Changed
1. Updated `NewPlatform.Flexberry.ORM` up to `8.0.0-beta02`.
2. Updated `NewPlatform.Flexberry.LockService` up to `4.0.0-beta01`.
3. Constructor of class `DataObjectEdmModel` (it now needs extra DI initialization).
4. Constructor of class `DefaultDataObjectEdmModelBuilder` (it is factory for class `DataObjectEdmModel`).
5. Constructor of class `DefaultOfflineManager`.
6. Constructor of class `OfflineAuditService`.

### Fixed

## [7.2.0] - 2024.03.27

### Added
1. `updateViews` parameter of `DefaultDataObjectEdmModelBuilder` class. It allows to change update views for data objects (update view is used for loading a data object during OData update requests).
2. `masterLightLoadTypes` and `masterLightLoadAllTypes` parameters of `DefaultDataObjectEdmModelBuilder` class. They allow to change loading mode of masters during OData update requests.

### Changed
1. Updated Flexberry ORM up to 7.2.0.

### Fixed
1. Fixed loading of object with crushing of already loaded masters.
2. Fixed loading of details.

## [7.1.1] - 2023.06.08

### Changed
1. Updated `NewPlatform.Flexberry.ORM` up to `7.1.1`.
2. Get properties from objects for send it to frontend always rethrow exception now.

### Fixed
1. Fixed problem with metadata when inheritance and PublishName is used.
2. Safe load details with complex type usage hierarchy.

## [7.1.0] - 2023.04.12

### Changed
1. Updated `NewPlatform.Flexberry.ORM` up to `7.1.0`.

## [7.0.0] - 2023.02.17

### Added
1. Added stubbed namespace for entities with publish name.
2. Added .net6 and .net7 as target frameworks.

### Changed
1. Upgrade `Microsoft.AspNet.OData` to `7.6.1`
2. Upgrade `NewPlatform.Flexberry.AspNetCore.OData` to `7.6.2`

## [6.2.0] - 2023.02.16

### Added
1. Support IExportStringedObjectViewService interface call for fast excel export.
2. Add DisabledDataObjectFileAccessor realization.
3. Support of actions with void response (it returns 204 No Content code).

### Changed

### Fixed
1. SafeLoadDetails for partial loaded detail.
2. Custom batch handler settings such as ODataMessageQuotas.
3. CallbackBeforeGet with count equals true.
4. Fix create detail view.
5. Fix view creation in case with filters by detail (twin master field contains, pk eq const).
6. Fix create detail view.
7. Fix user function Edm.Binary response.

## [6.1.0] - 2021.06.12

### Added

1. Netstandard 2.0 implementation.

## [6.0.0] - 2021.06.06

### Changed

 1. Update dependencies: `Microsoft.AspNet.OData` (`NewPlatform.Flexberry.AspNetCore.OData`) to `7.5.1`, `Microsoft.OData.Core` to `7.7.2`, `NewPlatform.Flexberry.ORM` to `6.0`.

## [5.2.0] - 2021.06.03

### Added
 1. Microsoft .NET Framework 4.6.1 compiled assemblies.
 2. Batch update MessageQuotas.MaxOperationsPerChangeset and MessageQuotas.MaxReceivedMessageSize parameters.

### Changed
 1. Removing files through file providers.
 2. Add additional edm mapping into model builder.

### Fixed
 1. Batch update InternalServerError event handling.
 2. SafeLoadDetails for models with TypeUsage.
 3. Batch update CallbackAfterCreate, CallbackAfterUpdate, CallbackAfterDelete call.
 4. SafeLoadDetails for partial loaded aggregator.

## [5.1.1] - 2020.08.21

### Added

1. The maximum number of top level query operations and changesets allowed in a single batch parameter, set default as 1000.

### Fixed

1. Update and delete details by batch update.
2. Exponential format for numeric values serialization issue.
3. SafeLoadDetails add details DataCopy to DataCopy DetailArray.
4. Load empty aggregators in SafeLoadDetails.

## [5.1.0] - 2020.05.03

### Added

1. Handle httpResponseException with OdataError wrapped in targetInvocationException.
2. Support $batch request for transactional update data objects.
3. Support for limits on master details.
4. Support for limits on pseudodetails.
5. Decode Excel export column name.
6. HttpConfiguretion MapDataObjectRoute() extension method.

### Changed

1. JavaScriptSerializer replaced with Newtonsoft.Json.JsonConvert for better performance.
2. [BREAKINGCHANGE] Method MapODataServiceDataObjectRoute now requires HttpServer as parameter.
3. At creation of dynamic views of the master in them are added with primary keys.
4. Use common DataObjectCache for all sql queries per http request.
5. [BREAKINGCHANGE] Details BS not apply changes in aggregator. Use BS for aggregator when details changed.
6. Refactor `DataObjectControllerActivator` to simplify overriding DOC initialization.
7. Mapping only selected properties on getting objects.
8. [BREAKINGCHANGE] The namespace of api-extensions is changed to NewPlatform.Flexberry.ORM.ODataService.WebApi.Extensions.
9. [BREAKINGCHANGE] The namespace of api-controllers is changed to NewPlatform.Flexberry.ORM.ODataService.WebApi.Controllers.
10. [BREAKINGCHANGE] The namespace of GenericCorsPolicyProvider is changed to NewPlatform.Flexberry.ORM.ODataService.Cors.
11. [BREAKINGCHANGE] HttpConfiguration MapODataServiceDataObjectRoute extension method is marked obsolete.
12. Code unification with Microsoft.AspNetCore.OData.

### Fixed

1. Fix error with POST request and header "Prefer".
2. Getting objects by primary key with using `$select` and `$expand` query options.
3. Loading masters with common DataObjectCache.
4. Naming of details when exporting data to Excel.
5. Call BS for aggregator when details changed in batch requests.
6. WebFile type support in batch requests.
7. Fix error on creation DataObject with pseudodetail field defined.
8. Loading masters with not stored property in batch requests.
9. Using ObjectStatus instead of private collection to determine if object is created.

## [5.0.0] - 2018.12.14

### Added

1. Exception handling in user functions.
2. Permissions for masters and details.
3. Export to excel with parameters.
4. The ability to export to an excel function odata.

### Changed

1. Update dependencies.


### Fixed

1. Fix error when query contains same properties.

## [4.1.0] - 2018.02.27
### Added
1. Add support user function geo.intersects.
2. Add support LoadingCustomizationStruct in user functions.
3. Add support actions.
4. Add handler, called after exception appears.
5. In user functions and actions add possibility to return collections of primitive types and enums. In actions add possibility to use primitive types and enums as parameters.

### Fixed
1. Fix reading properties of files.
2. Fix error which occured in Mono in method `DefaultODataPathHandler.Parse(IEdmModel model, string serviceRoot, string odataPath)`.
3. Fix errors in work of user functions.
4. Fix error in association object enumeration filtration.

### Changed
1. Update dependencies.
2. Update ODataService package version to according ORM package version.

## [2.0.0-beta.5] - 2017-09-02
### Added
* <README.md>
* <CHANGELOG.md>
* <LICENSE.md>
* Publish source code to GitHub public repository.

