# TALCodeBase
TAL coding challenge

Dependent Frameworks 
	.net core 3.1
	node.js

You can download the source code or fork the repository. Open the solution in Visual Studio and perform a build operation to install all the nuget packages needed.

Open nodejs commmand prompt and navigate to the sourcecode\WebUI\premiumCalcUI.
Run npm install to get all the packages installed and then perform ng build. You should not be getting any errors here. 

Steps in running the app.

1. Open SQL management studio or connect to your localdb instance from IDE. Execute the 'scripts\TalDBMain.sql' file which will create the database with required data. 

2. Run the web api in visual studio. The api sevices are configured to run in port 44308. 
3. The webapi appsetting.json contains the connection string to the database. Configure it to your local DB. 
4. It also contains the url where the angular app is running. Make sure it is the correct port, else you will receive CORS error in angular app. 

5. Run the angular app using ng serve. This should run in the default port 4200.
6. Angular app has the appsettings.json in the root folder. This contains the url to the web api. Make sure it is configured to the correct port.

7. The yaml file for building the application in Azure pipeline can be found in builds/build.yaml. 