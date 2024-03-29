-- Use DateApp Database
USE DateApp
GO

-- Drop tables if they exists
DROP TABLE IF EXISTS dbo.LINK_person_interests
DROP TABLE IF EXISTS dbo.LINK_person_seeking
DROP TABLE IF EXISTS dbo.interests
DROP TABLE IF EXISTS dbo.seeking
DROP TABLE IF EXISTS dbo.person
DROP TABLE IF EXISTS dbo.area
DROP TABLE IF EXISTS dbo.status
DROP TABLE IF EXISTS dbo.profession
GO

-- Create the tables
CREATE TABLE profession
(
	profID int NOT NULL IDENTITY(1,1) PRIMARY KEY,
	prof NVARCHAR(60) NOT NULL 
)

CREATE TABLE status 
(
	statusID INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	status NVARCHAR(25) NOT NULL
)	

CREATE TABLE area
(
	postnumber INT NOT NULL PRIMARY KEY,
	city NVARCHAR(60) NOT NULL,
	state NVARCHAR(45) NOT NULL,
)

CREATE TABLE person
(
	personID INT IDENTITY(1,1) PRIMARY KEY,
	firstName NVARCHAR(60) NOT NULL,
	lastName NVARCHAR(60) NOT NULL,
	birthday date NOT NULL,
	gender CHAR(1) NOT NULL,
	mail NVARCHAR(60) NOT NULL,
	phone INT NULL,
	area INT FOREIGN KEY REFERENCES area(postnumber),
	profession NVARCHAR(60) FOREIGN KEY REFERENCES profession(prof),
	status NVARCHAR(25) FOREIGN KEY REFERENCES status(status),
	seeking CHAR(1) NOT NULL,
	picture IMAGE NULL
)

CREATE TABLE interests
(
	interestID INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	interest NVARCHAR(60) NOT NULL
)

-- M�ske ikke n�dvendigt
CREATE TABLE seeking
(
	seekingID INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	seeking CHAR(1) NOT NULL
)

CREATE TABLE LINK_person_interests
(
	personID INT NOT NULL FOREIGN KEY REFERENCES person(personID),
	interestID INT NOT NULL FOREIGN KEY REFERENCES interests(interestID),
	--Link PRimary key - ask help
)

-- M�ske ikke n�dvendigt
CREATE TABLE LINK_person_seeking
(
	personID INT NOT NULL FOREIGN KEY REFERENCES person(personID),
	seekingID INT NOT NULL FOREIGN KEY REFERENCES seeking(seekingID),
	--Link primary key - ask help
)

/*	Insert all data in this order:
	1. status
	2. Profession
	3. area
	4. person
	5. seeking
	6. interests.
	7. LINK_person_interests
	8. LINK_person_seeking
*/

INSERT INTO status
VALUES 
('Single'),
('Married'),
('In-Relation'),
('Complicated')

INSERT INTO profession
VALUES
('IT-Supporter'),
('Secretarian'),
('Teacher'),
('Student'),
('Doctor'),
('Musician'),
('Programm�r'),
('Leader'),
('Director'),
('Unemployed')

INSERT INTO area
VALUES
(9230, 'Svenstrup J', 'Nordjylland'),
(9000, 'Aalborg', 'Nordjylland'),
(8200, 'Aarhus N', 'Midjylland'),
(2300, 'K�benhavn S', 'Sj�lland')

INSERT INTO person
(firstName, lastName, birthday, gender, mail, phone, area, status, profession, seeking)
VALUES
('Peter', 'Pan', '1996-02-04', 'M', 'PP@test.dk', 10101010, 9230, 'Single', 'Student', 'F'),
('Henirk', 'Lauridsen', '1994-05-12', 'M' ,'HL@test.dk', 20202020, 8200, 'Single', 'Student', 'F'),
('Matilde', 'Kristensen', '1994-02-20', 'F', 'MK@test.dk', 30303030, 9000, 'Single', 'Student', 'M')

INSERT INTO interests
VALUES
('Animals'),
('Movies'),
('Relationships'),
('Adrenalin'),
('Video Games'),
('Outdoors'),
('Camping'),
('Drinking'),
('Party')