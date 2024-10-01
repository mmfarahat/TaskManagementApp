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
  * tracking active users on the server
  * User can see online (connected users)
  * user can select a specific user and send a message
  * the message is being delivred to the correct user
  * user is removed from the connected users upon singing out

### How to use
* run the backend project (API) from visual studio
* open the vue app located in **frontend** folder in the vs code and run the following command `npm run dev`
* make sure that the **backend** in running on port **7120** and the **frontend**  on port **5173** to avoid CORS problems
* login with any of the 4 users mentioned above
* Create Tasks, view all tasks, make search and paging and edit tasks

### How to use chat
* As mentioned earlier the chat functionality is very basic and limited.
* open the frontend in multiple browsers and login with different user in each browser
* **All users must navigate to the chat page to successfully test the feature**
* in the chat page each user will see the other connected user
* select a user and send a message
* the message will be displayed in an alert in the other user's window

