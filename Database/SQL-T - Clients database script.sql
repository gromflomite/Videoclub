CREATE TABLE "clients" (
	"clientid" int IDENTITY (1,1) PRIMARY KEY,	
	"name" nvarchar (50),
	"surname" nvarchar (50),	
	"dob" datetime,
	"email" nvarchar (50),
	"password" nvarchar (max),	
)