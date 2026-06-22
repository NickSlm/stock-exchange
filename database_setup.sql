IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'StockExchangeDb')
BEGIN
    CREATE DATABASE StockExchangeDb;
END
GO

USE StockExchangeDb;
GO

CREATE TABLE Currencies (
	CurrencyId INT IDENTITY(1,1) PRIMARY KEY,
	Country NVARCHAR(100) NOT NULL,
	CurrencyName NVARCHAR(100) NOT NULL,
	Abbreviation NCHAR(3) NOT NULL
);
GO

CREATE TABLE CurrencyPairs(
	PairId INT IDENTITY(1,1) PRIMARY KEY,
	BaseCurrencyId INT NOT NULL FOREIGN KEY REFERENCES Currencies(CurrencyId),
	QuoteCurrencyId INT NOT NULL FOREIGN KEY REFERENCES Currencies(CurrencyId),
	MinValue DECIMAL (10,4) NOT NULL,
	MaxValue DECIMAL (10,4) NOT NULL
);
GO

INSERT INTO Currencies (Country, CurrencyName, Abbreviation) VALUES
('Israel', 'Shekel', 'ILS'),
('USA', 'Dollar', 'USD'),
('Europe', 'Euro', 'EUR'),
('United Kingdom', 'Pound', 'GBP');

INSERT INTO CurrencyPairs (BaseCurrencyId, QuoteCurrencyId, MinValue, MaxValue) VALUES
(2, 1, 3.5000, 3.8000), 
(3, 2, 1.0500, 1.1200),
(3, 1, 3.8000, 4.1000)
GO