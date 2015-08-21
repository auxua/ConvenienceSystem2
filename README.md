# Convienience System
A simple Solution for Convenience Systems

## License
* Some of the projects in this solution use the Newtonsoft Json library, which is licensed under MIT-License (https://raw.githubusercontent.com/JamesNK/Newtonsoft.Json/master/LICENSE.md)
* The Server uses the NancyFX which is licensed under MIT-License (https://github.com/NancyFx/Nancy/blob/master/license.txt)
* The Server uses the mysql-.NET adapter by Oracle (not embedded in this repository)

* The Source Code of the Convenience System solution is licensed under the MIT-License.

## Frameworks/Dependencies
This App projects uses the Xamarin-Framework (Xamarin.iOS, Xamarin.Android) and the Windows Phone SDK.
It is a Shared Project, needing at least Visual Studio 2013 Update 2. Xamarin/WindowsPhone SDK may have further requirements.

## Projects
The solution has multiple projects:
* ConvenienceSystemDataModel - The common Data Types in this solution
* ConvenienceSystemBackendServer - The high-level convenience server. Manages the Database queries and mails
* ConvenienceSystemServer - The NancyFX self-hosting server providing a module conencting to a backend server
* ConvenienceSystemConsole - Simple Console Applciation for testing the convenience backend
* ConvenienceSystemApp - The Shared project providing Xamarin.Forms Code/Pages for the mobile Apps
* ConvenienceSystemApp.Droid/.WinPhone/.iOS - The mobile Apps for the system clients (see screenshots)
* WebAdminClient - A ASP.NET (WebForms) Web application providing a UI for administrative Tasks

## Installation/Usage Requirements
At the moment the following Devices/Applications are needed for a basic Convenience System installation

* A Server hosting the backend/Server (needs .NET/mono)
* A MySQL Database for hosting the Data (Other Databases possible by changing the Backend)
* A mobile Device (tablet recommended) running the App (iOS, Windows Phone 8/10, Android possible)
* A Web Server (Optional) for the WebClient

## Some Remarks

* The system provides the possibility to use keydates. E.g. Synchronizing with other systems/Accounting lists/etc. can be marked by them and enhance the workflow.

* Some projects contain configuration files (Config/Settings). There are basic samples provided but you need to add information there.

* There is no warranty/support for the solution. But if you have question or found a bug you may use the contact/issues-functions of github and maybe there will be fixes available.

* The .sql-file in this repository provides the basic structure that is used for this system in the database.

* Everybody is welcome to use this solution or even contribute to!

## Screenshots (App)

### User Selection

WP:
![wp_screen1](https://cloud.githubusercontent.com/assets/1534703/9212604/b4dffafa-408a-11e5-820d-62141ab286dc.png)

Android:
![and_screen1](https://cloud.githubusercontent.com/assets/1534703/9212608/c07ba7d8-408a-11e5-8f36-50745112ebe2.png)

iOS:
![ios_screen1](https://cloud.githubusercontent.com/assets/1534703/9212610/c07dea34-408a-11e5-9b9e-f91a64de2785.png)

### Product Selection (After having selected user)

WP:
![wp_screen2](https://cloud.githubusercontent.com/assets/1534703/9212607/b838caa6-408a-11e5-8297-7ad04469eb87.png)

Android:
![and_screen2](https://cloud.githubusercontent.com/assets/1534703/9212609/c07cef8a-408a-11e5-9216-65e78a0435aa.png)

iOS:
![ios_screen2](https://cloud.githubusercontent.com/assets/1534703/9212611/c0820b82-408a-11e5-9b62-83b349aacd22.png)