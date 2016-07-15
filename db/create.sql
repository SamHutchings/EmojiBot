
    create table "Emoji" (
        Id  serial,
       Created timestamp,
       EmojiCharacters varchar(3),
       Description varchar,
       Keywords varchar,
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