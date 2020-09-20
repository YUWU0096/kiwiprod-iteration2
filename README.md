HopeMe - A Refugee Employment Seeking Solution
Version 2.0

Prerequisites:
  Visual Studio 2019 or later IIS 10.0 EXPRESS SQL server 2019.
  Pycharm community 2019 or higher with a Python 3.7 compiler, with sklearn and skl2onnx packages.

Installation: Simply run kiwiprod-iteration1.sln with Visual Studio 2019, than click on "Build" in the menu, then select "Build Solution". When the build is complete, click on IIS EXPRESS on the tool bar, or press "Ctrl" and "F5" at the same time, to run the project.

Delployment: The website is developed in ASP.NET and published using azure APP service. A database is hosted on the Azure server for our website. Furthermore, we set firwall for the web and change model to web model. Custom domains SSL are used in the web project. Built With: C# .NET MVC

The Machine Learning Model (under /hopMe) can be compiled using a python IDE with a Python 3.7 compiler. The outputs are englsh_proficiency_index.csv, highest_education_index.csv, and predictions.csv. The files may be imported into the web backend database for user employment opportunity predictions.

Troubling Shooting:
  If the web project fails to build, please ensure you have the prerequisites installed on your local operating system or virtual machine, and go to Tools -> NuGet Manager ->  Manage Nuget packages for solution. Click on the "Installed Tab" and click on "Restore" on the "NuGet Solution" tab for NuGet packages to be restored.
  If the Python project fails, please go to your Python IDE's package installation manager and install the above packages.

Authors: Anzhela Matveenko, Can Liang, Vaishnavi Bulbule, Yu Wu

License: This project is licensed under the Academic Free License v3.0 (AFL-3.0).
