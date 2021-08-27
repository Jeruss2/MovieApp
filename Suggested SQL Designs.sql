/****** Script for SelectTopNRows command from SSMS  ******/
SELECT TOP (1000) [Title]
      ,[Director]
      ,[ReleaseYear]
      ,[RentalCost]
      ,[PurchaseCost]
      ,[InStock]
  FROM [Josh].[dbo].[PurchasedMovies]

  use SomeOtherDB
  GO

  create table Movie (
       [MovieID] int identity(1,1) PRIMARY KEY,
	   [Title]
      ,[Director]
      ,[ReleaseYear]
      ,[RentalCost]
      ,[PurchaseCost]
  )

  create table users (
	userId varchar(20) PRIMARY KEY,
	userName varchar(20)
  )

  create table RentedMovies (
	userId varchar(20) FOREIGN KEY REFERENCES users(userid),
	MovieID int REFERENCES 
  )

  create table PurchasedMovies (
	userId varchar(20) FOREIGN KEY REFERENCES users(userid),
	MovieID int REFERENCES
  )

  --*((((((((((((*******************************


  create table MovieStatus (
	userId varchar(20) FOREIGN KEY REFERENCES users(userid),
	MovieID int REFERENCES,
	MovieStatus int
  )

  create table LookupMovieStatus (
	MovieStatusID int,
	[Description] varchar(50)
  )

  insert into LookupMovieStatus
  VALUES (  1, 'rented'), (2, 'purchased'), (3, 'in stock')


