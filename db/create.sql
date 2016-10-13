
    create table "Emoji" (
        Id  serial,
       Created timestamp not null,
       Characters varchar(10),
       Description text,
       Name text,
       Keywords text,
       primary key (Id)
    );
	
    create table "Category" (
        Id  serial,
       Created timestamp not null,
       SortOrder int,
       Name varchar(255),
       primary key (Id)
    );

    create table "User" (
        Id  serial,
       Created timestamp,
       Email varchar(255),
       Password varchar(255),
       Salt varchar(255),
       primary key (Id)
    );

    alter table "Emoji" add "category_id" int;

    alter table "Emoji"
    add constraint fk_emoji_category
    foreign key (category_id)
    references "Category" (id)
    on delete cascade;