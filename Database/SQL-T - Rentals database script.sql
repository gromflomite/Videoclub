CREATE TABLE "rentals" (
	"rentalid" int identity (1,1) PRIMARY KEY,
	"clientid" int foreign key references clients(clientid),
    "filmid" int foreign key references films(filmid),
	"rentalstartdate" datetime,
	"rentalenddate" datetime,
	"rentaltime" datetimeoffset,
)