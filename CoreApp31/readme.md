ControllerBase, the common base class for ASP.NET Core MVC and API Controllers
Conntroller, the abstract base class for ASP.NET Core MVC Controller

==========================================================================================================================================
View Tag Helpers
1. They are the Custom Defined attributes for HTML UI Elements for Server-Side Execution for. These attributes are prefixed using 'asp-'
	- area routing
		- asp-area
	- controller redirection
		- asp-controller, accepts the copntroller name so that request to that controller can be redirected 
	- action loading
		- asp-action, redirect to action from the controller and then load and execute the action in HTTP Request
	- Collection Execution
		- asp-items, iterate over the collection received from action and generate HTML UI elemnts from it
	- route parameters
		- asp-route-<ROUTE-PARAMETERS>
	- model property binding
		- asp-for, this accepts the public proeprty from model class and bind it with HTML UI element
		- specially used for HTML input elements  
2. Microsoft.AspNetCore.Mvc.TagHelpers is the assembly where all tag helpers are defined

===========================================================================================================================================
RazorPage<TModel>, the base class for Razor View. TModel is the type that will be bind to view during the View Scafollding (generation).
Razor Page Templates
1. List
	- Generate the List View and Accepts TModel as the IEnumerable<TModel>
	- Model property will be IEnumerable<TModel>
2. Create
	- EMpty View bounf to TModel
	- Model will be new imnstance of TModel to create a new record
3. Edit
	- The View with Instance of Model class as TModel containing Data to be Edited
4. Delete
	- The View with Instance of Model class as TModel containing Data to be delete
5. Empty
	- The Free Hand Design of the View