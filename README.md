# TaskManagementApp
This a simple app to manage tasks, the app is created with asp.net web apis in the backend and vueJS in the frontend

### Key points
* the backend is created using domain centric architecture with CQRS, Mediator and repository pattern
* entity framework in memory database is used, so no data is presistant
* 4 users are already created by seeding the database (all passwords are: **P@ssw0rd**)
  * user1@taskManagement.com
  * user2@taskManagement.com
  * user3@taskManagement.com
  * user4@taskManagement.com
* Asp Identity with JWT is used for authentication
* Paging and Search features are implemented in server side
* Chat functionality is very basic and not all required features were implemented
  * User can see online (connected users)
  * user can select a specific user and send a message
  * the message is being delivred to the correct user
  * user is removed from the connected users upon singing out

### How to use
* run the backend project (API) from visual studio
* open the vue app located in **frontend** folder


