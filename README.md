# NumToWordsConverter
Simple web application that allows a user to convert a number input to its dollars and cents equivalent in words.
Assumptions: User can only enter a number from 0 to 999999999.99.

Step 1: Clone repo \
Step2 : Install Required Software:\
Install Visual Studio: If you haven't already, download and install Visual Studio from the official Microsoft website.\
Install .NET SDK: Make sure you have the .NET SDK installed on your machine. You can download it from the .NET website or Visual Studio Marketplace.\
Step 3: Open NumToWordsCurrency.sln\
Step 4: Run dotnet restore or Select "Restore NuGet Packages" from solution explorer\
Step 5: Build the solution by selecting "Build > Build Solution" from the Visual Studio menu, or by pressing Ctrl + Shift + B.\
Step 6: Right-click on the desired project in the Solution Explorer.Select "Set as Startup Project" and Select NumToWordsCurrency.\
Step 7: Pres F5 to run.

Step 8: Run test by navigating to Test>Run All \

Note: Unit test only written for service layer so far as its the layer where business logic resides.Further enhancement to write unit test for Controller layer and Model layer.Null checking is done in the model layer.
