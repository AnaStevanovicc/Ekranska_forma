create database Skola
use Skola

create table Ucionica
(id int identity(1,1),
 naziv nvarchar(30),
 sprat int,
 br_ucenika int,
 br_racunara int,
 projektor nvarchar(30)
)

insert into Ucionica
values('Strani jezici', 4, 22, 15, 'W7643'),
('Laboratorija', 1, 10, 1, 'QY6234'),
('Djordje Krstic', 2, 30, 15, 'U569'),
('Jovan Cvijic', 2, 28, 10, 'AS647'),
('Stevan Sremac', 3, 19, 2, 'ZV74')