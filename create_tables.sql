create TABLE Station(
ID INT IDENTITY PRIMARY KEY,
Station_Name VARCHAR(30) NOT NULL,
[Location] VARCHAR(30) NOT NULL
)


CREATE TABLE Train(
ID INTEGER IDENTITY PRIMARY KEY,
[Name] Varchar(30) NOT NULL
)


CREATE Table [Route](
ID INTEGER IDENTITY PRIMARY KEY,
TrainID INT NOT NULL,
Departure DATETIME,
Arrival DATETIME,
SourceSt INT NOT NULL,
DestSt INT NOT NULL,
Total_A INT,
Purchased_A INT DEFAULT 0, 
Total_B INT,
Purchased_B INT DEFAULT 0, 
Total_C_Both INT,
Purchased_C_Both INT DEFAULT 0, 
Total_C_Seat INT,
Purchased_C_Seat INT  DEFAULT 0,
Fare_A INT,
Fare_B INT,
Fare_C_Both INT,
Fare_C_Seat INT,
FOREIGN KEY (TrainID) REFERENCES Train(ID),
FOREIGN KEY (SourceSt) REFERENCES Station(ID),
FOREIGN KEY (DestSt) REFERENCES Station(ID)
);

SELECT * FROM TRAIN;
SELECT * FROM STATION;

SELECT * FROM [Route];


create Table [User] (
Email varchar(50) PRIMARY KEY,
[Password] varchar(30) NOT NULL,
[name] varchar(30) NOT NULL
);
