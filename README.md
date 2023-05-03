# Airports and Flights
This web application is build and developed in Visual Studio 2022 as IDE. ASP.NET Core Web App (Model-View-Controller) project template is used during the development.
So the best way to run the application is through Visual Studio.

Note: Make sure the command 'Update-Database' is run in the Package Manager Console (PMC), before hitting Build & Run for first time for the App.


### Most significant endpoints according to the requirements

1. /Airports - to get a list of Airports 

2. /Flights - to get a list of Flights

3. /{Airports,Flights}/Create - to create new Airport or Flight

4. /{Airports,Flights}/{Details,Edit,Delete}/{id} - CRUD operations available for both Airports and Flights

5. /Airports/GetFlightsFrom/{airportCode} - showing all flights from an airport with a specific code

6. /Airports/GetFlightsTo/{airportCode} - show all direct flights to specific airport A

7. /Airports/MostPassengers/{countryName} - return the airport with most passengers during one year for specific country (returns JSON)

8. /Flights/DirectFlights/{airportCode}/{airportCode} - showing all direct flights from airport A to airport B 


### Build and run the docker image

Note: Unfortunately, the steps below didn't bring the expected results. 

1. Navigate to the root of the project where the Dockefile is located (./AirportsAndFlights/)

2. Open terminal and run the following command to create docker image:
	- docker build -t airportsflightsdockerimg .
	
3. Once the image with name 'airportsflightsdockerimg' is created, it's time to run a container for that image. In terminal run
	- docker run -d -p 80:80 --name airportsflightscontainer airportsflightsdockerimg
	
The application should be available locally in your browser, and can be accessed at https://localhost:80
