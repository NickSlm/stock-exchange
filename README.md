Prerequisites:

*   Run the provided sql script "database_setup.sql" to create the database and the needed tables.
*   Change the ConnectionString in the StockExchange/Config/appsettings.json
    "ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_INSTANCE_NAME\\SQLEXPRESS;Database=StockExchangeDb;Trusted_Connection=True;TrustServerCertificate=True;"
    }
*   Run the StockExchange solution "\StockExchange\StockExchange.sln"




Explanation / design.

First the database layer.
The database StockExchangeDb runs on an SQL server with two tables connected with one to many connection Currencies(ONE) and CurrencyPairs(MANY).
To talk to the database i decided to use EfCore it is what im familiar with and the "DataLayer" project is the one that is responsible for it.
It has the basic Models Currencies and CurrencyPairs that are the entities that work with the corresponding tables, the dbcontext (ExchangeContext) which is the class that holds the session
with the database, and responsible for all the operation with it.
And the Service itself DbService that is responsible for holding the methods that we use to run on the database using the dbcontext.
two of the methods are PullData which is the initial method that pulls data in TradeSimService(will explain it in a bit) that populates the property that holds the pairs so that we can in the viewmodel,
use it to bind the data in the view and showcase the user the changes.
And the second method is the UpdateMinMax which commits changes made but the tradingsimservice to the database.


Second layer would be the BusinessLayer.
it holds the TradeSimService which is responsible to run in loop and update the stock value with a simple random algorithm that generates double from 0-1 i scale it by multiplying it by 0.1 so that it will be between 0.0 - 0.1
next i need it to be able to be positive or negetive -0.05 - +0.05 so i deduct 0.05 from it.
and add it to the current value. (I did something similar in the past for another project if you wonder how i came up with it but i did it for random pathing)
It has 3 methods.
LoadPairs which is the initial load of the data when we start the app,
StartAsync which starts the loop cycle that runs the method update pairs that updates the paris.
Because this service is responsible of modifying the values of the stock  loading and updating i decided to have it hold the pairs property and use the INotifyPropertyChanged interface in it so that the viewmdel gets updated and the View gets notifying when the pairs change so that the UI is updated.

Last layer is the wpf application and the app itself.
I use DI to register the dbservice from DataLayer and the TradeSimService from the businesslayer so that i can inject them where needed for example the tradesimservice needs the dbservice so i can inject the dbservice directly to the tradesimService.

the pattern i use in the wpf is basic MVVM,
the view is the Mainwindow that has the datagrid,
the viewmodel holds the properties that we bind in the View,
and because it is responsible to have the data it is the process that runs the loadpairs from the tradesimservice and references the pairs that the tradesimservice holds.

About what i used for help,
I have a wpf project in my github https://github.com/NickSlm/Tovia that used for references for some syntaxes.
I dont understand understand trading and stock exchange as a topic so i had to do fast research about it.
Having done my other WPF project helped me with knowing the design i want to have and how to do it.