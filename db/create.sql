
    create table "Emoji" (
        Id  serial,
       Created timestamp not null,
       Characters varchar(10),
       Description text,
       Name text,
       Keywords text,
       primary key (Id)
    )

    create table "User" (
        Id  serial,
       Created timestamp,
       Email varchar(255),
       Password varchar(255),
       Salt varchar(255),
       primary key (Id)
    )