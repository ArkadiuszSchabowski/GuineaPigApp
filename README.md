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

**How to Run (Backend):**
- Clone the repository: git clone https://github.com/ArkadiuszSchabowski/GuineaPigApp.git
- Open the solution file GuineaPigCare.sln in Visual Studio.
- In the toolbar, select the dropdown next to the green arrow, choose the Development profile.
- Press F5 or click the green arrow to build and run the application.

**How to Run (Frontend) with Docker:**
- Make sure you have Docker installed on your machine. If not, you can download Docker here. - https://www.docker.com/
- First, clone the project repository to your local machine by running the following command: git clone https://github.com/ArkadiuszSchabowski/GuineaPigApp.git
- After cloning, use cd to navigate to the Frontend/GuineaPigApp.Client directory inside the project folder: cd <YourPath>/GuineaPigApp/Frontend/GuineaPigApp.Client
- Once inside the Frontend/GuineaPigApp.Client directory, build the Docker image using this command: docker build -t guineapigapp:1.00 .
- After building the image, run the container in detached mode with port mapping for 4200 using this command: docker run -d -p 4200:4200 guineapigapp:1.00
- Once the container is running, you can access the Angular application in your web browser by visiting: http://localhost:4200

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
