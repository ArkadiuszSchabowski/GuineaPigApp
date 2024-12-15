**Guinea Pig App - Application for Managing Guinea Pig Information**

**Demo** - To quickly experience the application, simply visit the: https://guineapigapp.azurewebsites.net - **Since this is a free plan, it may take up to a minute to fully start up the database after periods of inactivity. Thank you for your patience! 😊**


**About the Project** - Guinea Pig App is an application designed to help users manage information about their guinea pigs. Users can create and manage profiles for their pets and monitor their weight to keep track of their health. This project demonstrates the use of modern web technologies to build a full-stack application.

**Technology Stack:**
- Frontend: Angular, Angular Material
- Backend: ASP.NET Core (C#)
- Database: Azure SQL Database (demo version on a free plan)
- Communication: REST API

**Design Patterns:**
- Dependency Injection (DI)
- Repository Pattern (RP)
- Observer Pattern (OP)

**How to Run with Docker:**
- Clone the repository: git clone https://github.com/ArkadiuszSchabowski/GuineaPigApp.git
- Make sure you have Docker installed on your machine. If not, you can download Docker here. - https://www.docker.com/

**Database:**
- Use cd to navigate in terminal to the GuineaPigApp/Backend/GuineaPigApp.Server.Database directory inside the project folder: cd YourPath/GuineaPigApp/Backend/GuineaPigApp.Server.Database
- Once inside the GuineaPigApp/Backend/GuineaPigApp.Server.Database directory, build the Docker image using this command: docker build -t guineapigapp_db:1.0.0 .
- After building the image, run the container in detached mode with port mapping for 1433 using this command: docker run -d -p 1433:1433 guineapigapp_db:1.0.0
  
**Backend:**
- Use cd to navigate in terminal to the GuineaPigApp/Backend directory inside the project folder: cd YourPath/GuineaPigApp/Backend
- Once inside the GuineaPigApp/Backend directory, build the Docker image using this command: docker build -t guineapigapp_server:1.0.0 .
- After building the image, run the container in detached mode with port mapping from port 5000 on the host to port 8080 in the container using this command: docker run -d -p 5000:8080 guineapigapp_server:1.0.0

**Frontend:**
- Use cd to navigate in terminal to the Frontend/GuineaPigApp.Client directory inside the project folder: cd YourPath/GuineaPigApp/Frontend/GuineaPigApp.Client
- Once inside the Frontend/GuineaPigApp.Client directory, build the Docker image using this command: docker build -t guineapigapp_client:1.0.0 .
- After building the image, run the container in detached mode with port mapping for 4200 using this command: docker run -d -p 4200:4200 guineapigapp_client:1.0.0
- Once the last container is running, you can access the Angular application in your web browser by visiting: http://localhost:4200

**Test User Accounts** - To quickly test the application, you can use the following test user accounts:

User:
- Email: user@gmail.com
- Password: User123

Manager:
- Email: manager@gmail.com
- Password: Manager123

Admin:
- Email: admin@gmail.com
- Password: Admin123

**Contact** - Questions or feedback? Feel free to contact me at: arkadiuszschabowski@gmail.com

**Guinea Pig App** - Keep your guinea pigs happy and healthy! Manage their profiles and track their weight to monitor their health. 🐹🎉
