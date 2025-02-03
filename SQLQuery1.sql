--CREATE DATABASE TaskManagement;
--GO 

--USE TaskManagement;
--GO

--CREATE TABLE Tasks (
--    Id INT IDENTITY(1,1) PRIMARY KEY,  
--    Title NVARCHAR(255) NOT NULL,       
--    Description NVARCHAR(MAX) NULL,     
--    IsCompleted BIT DEFAULT 0,          
--    CreatedAt DATETIME DEFAULT GETDATE() 
--);
--GO

--INSERT INTO Tasks (Title, Description, IsCompleted) VALUES
--('Complete project documentation', 'Write the full documentation for the project', 0),
--('Review pull requests', 'Check and review the latest pull requests', 0),
--('Deploy API to production', 'Ensure the API is properly deployed and tested', 0);
--GO