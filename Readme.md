#Date: 8.26.2013

#Version: v1.0.0

#Author(s): [Jesus Aguilar](http://giventocode.com)

--------------------------------------------------------------------------------

# Description

The Eventbrite Starter Kit is a XAML/C# solution containing a Windows 8 Store App, Windows Phone 8 App and two shared Portable Class Libraries (PCL). The Starter Kit demonstrates how you can use the same code base to invoke the Eventbrite REST API to obtain information about events near your location and create View Models shared across both platforms.  

#Features- 
- Invokes the Evenbrite API to obtain events near your location.
- Uses the Geolocation API for Windows 8 and Windows Phone 8.
- [Demonstrates the use of a dynamic object to desearialize the response from the API.](http://giventocode.com/deserialize-a-json-response-into-a-dynamic-object-–-building-my-apimash-starter-kit-–-part-iii) 
- Provides a baseline for a Windows 8 Store App and Windows Phone 8 App.
- Showcases the use of the HubTile and Map extensions from the Windows Phone Toolkit.

#Screenshots
Windows 8 
![](http://giventocode.com/Media/Default/StarterKitImages/Win8App.png)

Windows Phone 8

![](http://giventocode.com/Media/Default/StarterKitImages/WinP8S0.png)

#Requirements
- Windows 8
- Visual Studio 2012 Pro Update 3 or higher

>If you are using Visual Studio 2012 Express you can still use this Starter Kit, however you will need to create a class library project for each platform, and copy the source and resource files form the PCL projects into your platform specific projects. You can then update the references in the Windows 8 Store App and Windows Phone 8 App projects to point the corresponding library and remove the references to the PCL projects.
 
- [JSON.NET from Newtonsoft](http://blogs.msdn.com/b/bclteam/archive/2013/02/18/portable-httpclient-for-net-framework-and-windows-phone.aspx)
- [Portable HttpClient for .Net Framework and Windows Phone](http://blogs.msdn.com/b/bclteam/archive/2013/02/18/portable-httpclient-for-net-framework-and-windows-phone.aspx "Portable HttpClient for .Net Framework and Windows Phone")
- [Windows Phone SDK 8.0](http://go.microsoft.com/fwlink/?LinkId=265772)
- [Windows Phone Toolkit](http://phone.codeplex.com/)

#Setup

- Download the Starter Kit 
- Obtain an app key from Eventbrite. [More info here](https://www.eventbrite.com/api/key/)
- Before compiling and running any of the projects you need to update the APP\_KEY entry in the ApiResources.resx file with your Eventbrite app key. The ApiResources.resx file is located in the APIMASH\_Eventbrite\_Core project.


#DISCLAIMER:

The sample code described herein is provided on an "as is" basis, without warranty of any kind, to the fullest extent permitted by law. Both Microsoft and I do not warrant or guarantee the individual success developers may have in implementing the sample code on their development platforms or in using their own Web server configurations. Microsoft and I do not warrant, guarantee or make any representations regarding the use, results of use, accuracy, timeliness or completeness of any data or information relating to the sample code. Microsoft and I disclaim all warranties, express or implied, and in particular, disclaims all warranties of merchantability, fitness for a particular purpose, and warranties related to the code, or any service or software related thereto. 

Microsoft and I shall not be liable for any direct, indirect or consequential damages or costs of any type arising out of any action taken by you or others related to the sample code.

--------------------------------------------------------------------------------
