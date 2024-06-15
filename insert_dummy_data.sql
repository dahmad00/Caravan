-- Insert into Station
INSERT INTO Station (Station_Name, [Location])
VALUES 
    ('Central Station', 'City A'),
    ('West Station', 'City B'),
    ('East Station', 'City C'),
    ('North Station', 'City D'),
    ('South Station', 'City E');

-- Insert into Train
INSERT INTO Train ([Name])
VALUES 
    ('Express A'),
    ('Express B'),
    ('Local C'),
    ('Local D'),
    ('Express E');

-- Insert into Route
INSERT INTO [Route] (TrainID, Departure, Arrival, SourceSt, DestSt, Total_A, Total_B, Total_C_Both, Total_C_Seat, Fare_A, Fare_B, Fare_C_Both, Fare_C_Seat)
VALUES 
    (1, '2024-06-17 08:00:00', '2024-06-17 10:00:00', 1, 2, 100, 80, 60, 40, 50, 30, 20, 10),
    (2, '2024-06-18 09:00:00', '2024-06-18 11:00:00', 2, 3, 120, 90, 70, 50, 55, 35, 25, 15),
    (3, '2024-06-19 10:00:00', '2024-06-19 12:00:00', 3, 4, 130, 100, 80, 60, 60, 40, 30, 20),
    (4, '2024-06-20 11:00:00', '2024-06-20 13:00:00', 4, 5, 140, 110, 90, 70, 65, 45, 35, 25),
    (5, '2024-06-21 12:00:00', '2024-06-21 14:00:00', 5, 1, 150, 120, 100, 80, 70, 50, 40, 30);

-- Insert into User
INSERT INTO [User] (Email, [Password], [name])
VALUES 
    ('user1@example.com', 'password1', 'User One'),
    ('user2@example.com', 'password2', 'User Two'),
    ('user3@example.com', 'password3', 'User Three'),
    ('user4@example.com', 'password4', 'User Four'),
    ('user5@example.com', 'password5', 'User Five');
