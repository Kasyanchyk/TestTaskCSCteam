# Test Task for SoftServe CSC-team

I used technology stack:
- ASP .Net Core (Web API)
- ORM: Entity Framework Core
- DB: MSSQL Server
- IoC: Simple Injector
- Unit tests: XUnit, dotCover
- Documentation: Swagger

## About API
Api has this structure (item is a member of the hierarchy, for example **Organization, Country, Business...**) : 
- GET: api/**item** - Get all items.
- GET: api/**item**/{id} - Get item by id.
- GET: api/**item**/**child-item**/{id} - Get child-items by id item (Child-item is the next item in the hierarchy).
- POST: api/**item** - Creates a new item (need authentification).
- DELETE: api/**item**/{id} - Deletes the item (need authentification).
- PUT: api/**item** - Updates the item (need authentification).
### Facebook Authentification
- GET: api/auth - Get authentification status (authorized or not authorized).
- GET: api/auth/sigin - Redirect to Facebook authorize page and redirect to /api/organization
- GET: api/auth/sigou - Signout and redirect to /api/organization.
### Test Data
Use GET: api/create-test-data for add 

## Json Templates
### Organization API
JSON for PUT methods :
 ```sh
 {
  "name": "str",
  "code": "str",
  "organizationType": "str",
  "owner": "string",
  "id": 0
}
 ```
 JSON for POST methods :
 ```sh
 {
  "name": "str",
  "code": "str",
  "organizationType": "str",
  "owner": "string",
}
 ```
 ### Country API
 JSON for PUT methods :
 ```sh
 {
  "name": "str",
  "code": "str",
  "parentId": 0,
  "id": 0
}
 ```
 JSON for POST methods :
 ```sh
 {
  "name": "str",
  "code": "str",
  "parentId": 0
}
 ```
 
 ### Business API
 JSON for PUT methods :
 ```sh
 {
  "name": "str",
  "parentId": 0,
  "id": 0
}
 ```
 JSON for POST methods :
 ```sh
 {
  "name": "str",
  "parentId": 0
}
 ```
 ### Family API
 JSON for PUT methods :
 ```sh
 {
  "name": "str",
  "parentId": 0,
  "id": 0
}
 ```
 JSON for POST methods :
 ```sh
 {
  "name": "str",
  "parentId": 0
}
 ```
 ### Offering API
 JSON for PUT methods :
 ```sh
 {
  "name": "str",
  "parentId": 0,
  "id": 0
}
 ```
 JSON for POST methods :
 ```sh
 {
  "name": "str",
  "parentId": 0
}
 ```
 ### Department API
 JSON for PUT methods :
 ```sh
 {
  "name": "str",
  "parentId": 0,
  "id": 0
}
 ```
 JSON for POST methods :
 ```sh
 {
  "name": "str",
  "parentId": 0
}
 ```
## Unit test
Unit tests coverage. I used dotCover by JetBrains
![Code coverage](https://i.ibb.co/R4sMTY2/image.png)
