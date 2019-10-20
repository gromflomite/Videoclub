CREATE TABLE "films" (
	"filmid" int IDENTITY (1,1) PRIMARY KEY,	
	"title" nvarchar (max),
	"plot" nvarchar (max),	
	"agerate" nvarchar (2),
	"rented" nchar (1),
)