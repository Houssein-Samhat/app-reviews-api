To run the web Api project:
1) Install all the packages founded under Project->Dependencies->Packages

2) Create the dataBase:
 -> create a database in mysql called "usernames" with username="root" and password=''
 -> Run "add-migration" then Run "update-database" in package manager console
 -> Run "dotnet run seeddata", This will add 2 users to the dataBase

Note: 1) The "DefaultConnection" Found in appsettings under "ConnectionStrings"
      2) The AppMonsta Api_key and Password_key Found in appsettings under "AppSettings"
